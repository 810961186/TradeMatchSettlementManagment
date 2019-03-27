#region Using Namespace

using System;
using System.Collections.Generic;
using System.Data.Common;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReckoningCounter.BLL.Common;
using ReckoningCounter.BLL.DelegateValidate.Cost;
using ReckoningCounter.BLL.ManagementCenter;
using ReckoningCounter.DAL.CustomDataAccess;
using ReckoningCounter.DAL.Data;
using ReckoningCounter.DAL.Data.HK;
using ReckoningCounter.DAL.MatchCenterOrderDealRpt;
using ReckoningCounter.Entity;
using ReckoningCounter.Entity.Model.HK;
using ReckoningCounter.MemoryData;
using ReckoningCounter.MemoryData.HK.Capital;
using ReckoningCounter.MemoryData.HK.Hold;
using ReckoningCounter.MemoryData.XH.Capital;
using ReckoningCounter.MemoryData.XH.Hold;
using ReckoningCounter.Model;
using ReckoningCounter.DAL.DevolveVerifyCommonService;

#endregion

namespace ReckoningCounter.BLL.Reckoning.Logic.HK
{
    /// <summary>
    /// �۹����㴦��Ԫ���������2701-2790
    /// Create By:���
    /// Create date:2009-10-26
    /// </summary>
    public class HKReckonUnit : ReckonUnitBase<HK_TodayEntrustInfo, HKDealBackEntity, HK_TodayTradeInfo>
    {
        /// <summary>
        /// �Ƿ��Ѿ���ɵ�ǰί�����㣨�ɽ�+������
        /// </summary>
        protected bool hasDoneDeal;

        #region Overrides of ReckonUnitBase<HKTodayEntrustTable,HKDealBackEntity>

        /// <summary>
        /// �ڲ����ճɽ��ر�������
        /// </summary>
        /// <param name="message">�ɽ��ر����߳����ر�</param>
        protected override bool InternalInsertMessage(object message)
        {
            if (hasDoneDeal)
            {
                LogHelper.WriteDebug("HKReckonUnit.InternalInsertMessage:ί���Ѿ���ɣ��ر�����" + message);
                return false;
            }

            string id = string.Empty;
            string orderID = string.Empty;
            string desc = string.Empty;
            decimal dealAmount = 0;
            if (message is HKDealBackEntity)
            {
                HKDealBackEntity ido = (HKDealBackEntity)message;
                id = ido.ID;
                orderID = ido.OrderNo;
                desc = ido.DescInfo();
                dealAmount = ido.DealAmount;
            }
            else if (message is CancelOrderEntity)
            {
                CancelOrderEntity coe = (CancelOrderEntity)message;
                id = coe.Id;
                orderID = coe.OrderNo;
                desc = coe.DescInfo();
                dealAmount = coe.OrderVolume;
            }

            if (id == string.Empty)
            {
                LogHelper.WriteDebug("HKReckonUnit.InternalInsertMessage: ��ID");
                return false;
            }

            if (HasAddId(id))
            {
                LogHelper.WriteDebug("HKReckonUnit.InternalInsertMessage: �����ID" + id);
                return false;
            }

            string strMessage = string.Empty;
            bool isSuccess = InitializeOrderCache(orderID, ref strMessage);

            if (!isSuccess)
            {
                LogHelper.WriteInfo(strMessage);
            }

            if (HasAddReckoned(id))
            {
                LogHelper.WriteDebug("HKReckonUnit.InternalInsertMessage: �Ѵ���ID" + id);
                return false;
            }

            AddID(id);

            string entrusNum = "[EntrustNumber=" + EntrustNumber + "]";
            LogHelper.WriteDebug("HKReckonUnit.InternalInsertMessage" + desc + entrusNum);

            //��ӵ��ڲ��Ļ����б�
            if (message is HKDealBackEntity)
            {
                HKDealBackEntity ido = (HKDealBackEntity)message;
                dealBackList.Add(ido);
            }
            else if (message is CancelOrderEntity)
            {
                CancelOrderEntity coe = (CancelOrderEntity)message;
                cancelBackList.Add(coe);
            }


            if ((dealAmount + TradeAmount + CancelAmount) == EntrustAmount)
                return true;

            return false;
        }

        /// <summary>
        /// ����֪ͨ���շ���,������ԭ����MessageQueue_QueueItemProcessEvent���� 
        /// </summary>
        protected override void ReckonCommitCheck()
        {
            ProcessDealCommit();
            ProcessCancelCommit();
        }

        /// <summary>
        /// �����ݿ��������������
        /// </summary>
        protected override void LoadReckonedIDList()
        {
            var trades = HKDataAccess.GetTodayTradeListByEntrustNumber(EntrustNumber);
            if (trades == null)
                return;

            foreach (HK_TodayTradeInfo trade in trades)
            {
                AddReckonedID(trade.TradeNumber.Trim());

                if (trade.TradeTypeId == (int)Entity.Contants.Types.DealRptType.DRTDealed)
                {
                    string format = "[��TradeAmount{0}=��TradeAmount{1}+��ǰ�ɽ���{2}]";
                    string msg = string.Format(format, TradeAmount + trade.TradeAmount, TradeAmount,
                                               trade.TradeAmount);
                    LogHelper.WriteDebug("HKReckonUnit.LoadReckonedIDList" + msg);
                    TradeAmount += trade.TradeAmount;
                }

                if (trade.TradeTypeId == (int)Entity.Contants.Types.DealRptType.DRTCanceled)
                {
                    string format = "[��CancelAmount{0}=��CancelAmount{1}+��ǰ�ɽ���{2}]";
                    string msg = string.Format(format, CancelAmount + trade.TradeAmount, CancelAmount,
                                               trade.TradeAmount);
                    LogHelper.WriteDebug("HKReckonUnit.LoadReckonedIDList" + msg);
                    CancelAmount += trade.TradeAmount;
                }
            }

            HasLoadReckonedID = true;
        }

        protected override CounterCache GetCounterCache()
        {
            return HKCounterCache.Instance;
        }

        protected override void GetCurrencyType()
        {
            //===Update Date:2009-10-26 ���
            //var currOjb = MCService.SpotTradeRules.GetCurrencyTypeByCommodityCode(Code);
            var currOjb = MCService.CommonPara.GetCurrencyTypeByCommodityCode(Types.BreedClassTypeEnum.HKStock, Code);
            //=========
            if (currOjb == null)
            {
                return;
            }

            CurrencyType = currOjb.CurrencyTypeID;
        }

        protected override void GetAccountID()
        {
            CapitalAccountId = MemoryDataManager.HKCapitalMemoryList.GetCapitalAccountLogo(CapitalAccount, CurrencyType);

            HoldingAccountId = MemoryDataManager.HKHoldMemoryList.GetAccountHoldLogoId(HoldingAccount, Code, CurrencyType);
        }

        #endregion

        #region ��������

        /// <summary>
        /// �Ƿ���Խ��г�������
        /// </summary>
        /// <returns></returns>
        private bool CanProcessCancelCommit(out CancelOrderEntity coe)
        {
            string entrustNumString = "[EntrustNumber=" + EntrustNumber + "]";

            var list = cancelBackList.GetAllAndClear();
            if (Utils.IsNullOrEmpty(list))
            {
                coe = null;
                return false;
            }

            #region �Ƿ��Ѿ��������

            var hasDealedAmount = TradeAmount + CancelAmount;

            if (hasDealedAmount == EntrustAmount)
            {
                string msg = "CanProcessCancelCommit����ǰί���Ѿ�������ϣ������ر���Ч�������" + entrustNumString;
                LogHelper.WriteDebug(msg);

                //��ʾ����Ҫ������
                coe = null;
                return false;
            }

            #endregion

            #region �Ƿ����ڲ�����

            //���̺󣬻ᱻ����һ���ڲ��������󣬴�ʱֱ�ӷ����ڲ���������
            //��������ⲿ����������ô���� 
            foreach (var cancelOrderEntity in list)
            {
                if (cancelOrderEntity is CancelOrderEntityEx)
                {
                    CancelOrderEntityEx coeEx = cancelOrderEntity as CancelOrderEntityEx;
                    if (coeEx.IsInternalCancelOrder)
                    {
                        coe = coeEx;
                        return true;
                    }
                }
            }

            #endregion

            //��Ϊ������Type2�ĵ����ᵼ�¶����������
            //if (list.Count > 1)
            //{
            //    LogHelper.WriteDebug("HKReckonUnit.ProcessCancelCommit: �յ���������ر���ֻ�����һ����������������");
            //}

            coe = list[0];

            //����Ǵ���۸�ĳ���������ִ��
            if (coe.OrderVolume == -1)
                return true;

            decimal cancelSum = 0;
            foreach (var cancelOrderEntity in list)
            {
                cancelSum += cancelOrderEntity.OrderVolume;
            }

            //���TradeAmount+CancelAmount+cancelSum!=EntrustAmount,
            //��ô��Ϊ��Ȼ�յ��˳����ر������ǻ��в��ֳɽ��ر���û��������ô
            //�Ȳ����г������������еĳɽ��ر����������ˣ�����ٽ��г�������
            decimal allAmount = TradeAmount + CancelAmount + cancelSum;
            if (allAmount < EntrustAmount)
            {
                string format2 =
                    "HKReckonUnit.ProcessCancelCommit���в��ֳɽ��ر�δ�յ���δ����[EntrustAmount={0},TradeAmount={1},CancelAmount={2},CancelBackAmount={3}]";
                string info = string.Format(format2, EntrustAmount, TradeAmount, CancelAmount, coe.OrderVolume);
                //LogHelper.WriteInfo(info);

                //���гɽ��ر�û�л������������볷���б�ȴ��´γ�������
                foreach (var cancelOrderEntity in list)
                {
                    cancelBackList.Add(cancelOrderEntity);
                }

                return false;
            }

            //�����Type2�ĸĵ�����ô�����еĳ������ϳ�һ����������
            if (list.Count > 1)
            {
                var coe2 = new CancelOrderEntityEx();
                coe2.IsMultiCancelOrder = true;
                coe2.ChannelNo = list[0].ChannelNo;
                coe2.IsSuccess = list[0].IsSuccess;
                coe2.Message = "";
                coe2.OrderNo = list[0].OrderNo;
                coe2.IDList = new List<string>();
                //add ��� 2009-11-07============
                coe2.OrderVolumeList = new List<decimal>();
                //======
                string cancelDesc = "";
                string cancelDescFormat = "(ID={0},Volume={1})";

                foreach (var cancelOrderEntity in list)
                {
                    coe2.OrderVolume += cancelOrderEntity.OrderVolume;
                    coe2.IDList.Add(cancelOrderEntity.Id);
                    //add ��� 2009-11-07============
                    coe2.OrderVolumeList.Add(cancelOrderEntity.OrderVolume);
                    //=======
                    cancelDesc += string.Format(cancelDescFormat, cancelOrderEntity.Id, cancelOrderEntity.OrderVolume);
                }

                string format = "HKReckonUnit.CanProcessCancelCommit����2�ĵ��������еĳ������ϳ�һ����������[EntrustNumber={0}][{1}]";
                string desc = string.Format(format, EntrustNumber, cancelDesc);
                LogHelper.WriteDebug(desc);

                coe = coe2;
            }

            return true;
        }

        private void ProcessCancelCommit()
        {
            CancelOrderEntity coe;
            if (!CanProcessCancelCommit(out coe))
            {
                return;
            }

            string strMessage = "";
            HK_TodayEntrustInfo tet;
            List<HK_TodayTradeInfo> trades;
            bool isSuccess = false;

            //�Ƿ��Ƿϵ�
            //bool isBadOrder = false;

            CrashManager.GetInstance().AddReckoningID(coe.Id);

            //����۸�ĳ���
            if (coe.OrderVolume == -1)
            {
                isSuccess = InstantReckon_CancelOrder(coe, out tet, out trades, true, ref strMessage);

                ////û�гɹ�����������һ��
                //if (!isSuccess)
                //    isSuccess = InstantReckon_CancelOrder(coe, out tet, out trades, false);
            }
            else
            {
                //��������
                isSuccess = InstantReckon_CancelOrder(coe, out tet, out trades, false, ref strMessage);

                ////û�гɹ�����������һ��
                //if (!isSuccess)
                //    isSuccess = InstantReckon_CancelOrder(coe, out tet, out trades, true);
            }

            bool isMultiCancelOrder = false;
            CancelOrderEntityEx coeEx = null;
            if (coe is CancelOrderEntityEx)
            {
                coeEx = coe as CancelOrderEntityEx;

                if (coeEx.IsMultiCancelOrder)
                    isMultiCancelOrder = true;
            }

            if (!isSuccess)
            {
                //�������û�гɹ�����ô�ӱ���Ļر��б���ɾ�����´�CrashManger�����ٴη��ʹ˻ر�����
                LogHelper.WriteInfo(
                    "#####################HKReckonUnit.ProcessCancelCommit��������ʧ�ܣ�CancelOrderEntity[ID=" + coe.Id +
                    "], Message=" + strMessage);

                if (isMultiCancelOrder)
                {
                    foreach (var id in coeEx.IDList)
                    {
                        RemoveID(id);
                    }
                }
                else
                {
                    RemoveID(coe.Id);
                }
            }

            if (!string.IsNullOrEmpty(strMessage))
                HKDataAccess.UpdateEntrustOrderMessage(EntrustNumber, strMessage);

            if (isMultiCancelOrder)
            {
                foreach (var id in coeEx.IDList)
                {
                    CrashManager.GetInstance().RemoveReckoningID(id);
                }
            }
            else
            {
                CrashManager.GetInstance().RemoveReckoningID(coe.Id);
            }

            if (isSuccess)
            {
                ReckonEndObject<HK_TodayEntrustInfo, HK_TodayTradeInfo> cancelEndObject =
                    new ReckonEndObject<HK_TodayEntrustInfo, HK_TodayTradeInfo>();
                cancelEndObject.TradeID = TradeID;
                cancelEndObject.EntrustTable = tet;
                cancelEndObject.TradeTableList = trades;
                cancelEndObject.IsSuccess = true;
                cancelEndObject.Message = strMessage;

                if (tet != null && trades != null)
                    OnEndCancelEvent(cancelEndObject);
            }

            //����Ƿ��Ѿ�ȫ��������
            if ((TradeAmount + CancelAmount) == EntrustAmount)
            {
                hasDoneDeal = true;
            }

            if (hasDoneDeal)
            {
                bool isCheckSuccess = LastCheckFreezeMoney(EntrustNumber, CapitalAccountId);
                if (!isCheckSuccess)
                {
                    RescueManager.Instance.Record_HK_LastCheckFreezeMoney(EntrustNumber, CapitalAccountId);
                }

                //����Ѿ�ȫ�������꣬��ô��֤һ���Ƿ���Type2�ĸĵ�������ǵĻ�����ôҪ�޸ĵ�ǰί�еĸ��������Ϣ
                HKModifyOrderRequest hkModifyRequest1;
                if (ModifyOrderProcessor.Instance.IsExistType2Request(EntrustNumber, out hkModifyRequest1))
                {
                    ModifyOrderProcessor.Instance.ProcessType2ModifyOrder(EntrustNumber);
                }
                //update 2009-11-07 ��� 
                //����������ȸĵ����������٣�Ȼ�������ӽ����������Բ��ܷ����ж�,�����б����
                //else
                //======
                //{
                //�������Type2�ĸĵ�����ô����֤һ���Ƿ���Type3�ĸĵ�������ǵĻ�����ôҪ�����µ�ί�У�
                //���޸ĵ�ǰί�еĸ��������Ϣ
                HKModifyOrderRequest hkModifyRequest;
                if (ModifyOrderProcessor.Instance.IsExistType3Request(EntrustNumber, out hkModifyRequest))
                {
                    ModifyOrderProcessor.Instance.ProcessType3NewOrder(hkModifyRequest);
                }
                // }
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="rde">�����ر�</param>
        /// <param name="tet">ί��</param>
        /// <param name="trades">����ĳɽ��б�</param>
        /// <param name="isErrorPriceCancelRpt">�Ƿ��Ǵ���۸�ĳ���</param>
        /// <param name="strMessage">������Ϣ</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public bool InstantReckon_CancelOrder(CancelOrderEntity rde,
                                              out HK_TodayEntrustInfo tet, out List<HK_TodayTradeInfo> trades,
                                              bool isErrorPriceCancelRpt, ref string strMessage)
        {
            #region ��ʼ������

            tet = null;
            trades = null;

            if (!InitializeOrderCache(rde.OrderNo, ref strMessage))
            {
                LogHelper.WriteInfo(strMessage);
                return false;
            }

            string entrustNumString = "[EntrustNumber=" + EntrustNumber + "]";

            string canDesc = "";
            CancelOrderEntityEx rdeEx = null;
            bool isMultiCancelOrder = false;
            if (rde is CancelOrderEntityEx)
            {
                rdeEx = rde as CancelOrderEntityEx;

                if (rdeEx.IsMultiCancelOrder)
                {
                    isMultiCancelOrder = true;
                    canDesc = "��������2����";
                }

                if (rdeEx.IsInternalCancelOrder)
                {
                    canDesc = "�����ڲ�����";
                }
            }

            LogHelper.WriteDebug(
                "------xxxxxx------��ʼ���и۹��ⲿ����(��Ϸ��س����ر�[���������м۸������])HKReckonUnit.InstantReckon_CancelOrder2" +
                rde.DescInfo() + entrustNumString + canDesc);



            //�۹ɵ���ί�лر�
            //tet = HKDataAccess.GetTodayEntrustTable(EntrustNumber);
            //��ΪĳЩ�쳣��������ʧ�ܣ����ǵ���ί�б�ת����ʷί�У��Ժ���������Ҳ���ί�У��������ڸ�Ϊ
            //�ڵ����Ҳ�������ʷ����
            tet = HKDataAccess.GetAllEntrustTable(EntrustNumber);
            if (tet == null)
            {
                strMessage = "GT-2701:[�۹ɳ�������]�޷���ȡί�ж���ί�е���=" + EntrustNumber;
                LogHelper.WriteDebug(strMessage);
                return false;
            }

            //ȡ�����ӦƷ�ֵĽ��ױ���
            if (CurrencyType == -1)
            {
                //===Update Date:2009-10-26 ���
                // CurrencyType = MCService.SpotTradeRules.GetCurrencyTypeByCommodityCode(tet.Code).CurrencyTypeID;
                CurrencyType = MCService.CommonPara.GetCurrencyTypeByCommodityCode(Types.BreedClassTypeEnum.HKStock, tet.Code).CurrencyTypeID;
                //==============
            }

            var hasDealedAmount = TradeAmount + CancelAmount;

            if (hasDealedAmount == EntrustAmount)
            {
                string msg = "�����ر����󣺵�ǰί���Ѿ�������ϣ������ر���Ч��" + entrustNumString;
                LogHelper.WriteDebug(msg);

                //��������id���뵽������id�б���
                if (isMultiCancelOrder)
                {
                    foreach (var id in rdeEx.IDList)
                    {
                        AddReckonedID(id);
                        //������ݿ��б���ĳɽ��ر�
                        CrashManager.GetInstance().DeleteEntity(id);
                    }
                }
                else
                {
                    AddReckonedID(rde.Id);
                    //������ݿ��б���ĳɽ��ر�
                    CrashManager.GetInstance().DeleteEntity(rde.Id);
                }

                //��ʾ���㻹�ǳɹ��ˣ���ʵ��ڹ��ϻָ�ʱ����Ҫ���·���
                return true;
            }

            if (rde.OrderVolume != -1)
            {
                if ((hasDealedAmount + rde.OrderVolume) > EntrustAmount)
                {
                    string msg = "�����ر����󣺳����ر���������+�����������>��ǰί������" + entrustNumString;
                    LogHelper.WriteDebug(msg);

                    //��������id���뵽������id�б���
                    if (isMultiCancelOrder)
                    {
                        foreach (var id in rdeEx.IDList)
                        {
                            AddReckonedID(id);
                            //������ݿ��б���ĳɽ��ر�
                            CrashManager.GetInstance().DeleteEntity(id);
                        }
                    }
                    else
                    {
                        AddReckonedID(rde.Id);
                        //������ݿ��б���ĳɽ��ر�
                        CrashManager.GetInstance().DeleteEntity(rde.Id);
                    }

                    //��ʾ���㻹�ǳɹ��ˣ���ʵ��ڹ��ϻָ�ʱ����Ҫ���·���
                    return true;
                }
            }

            bool isSuccess = false;

            #endregion

            #region ��������

            //�������ⲿ��InstantReckon_CancelOrder������Ϊ1.1�Ľṹ�޸ģ��ⲿ�������ڲ������߼�����һ��(6����)
            //1.�ʽ𶳽ᴦ��
            //���ʽ𶳽��¼��Ľ��ͷ���ȫ�������ʽ��ɾ�������¼(ʵ����ֻ���㣬��ɾ�����̺�ͳһɾ��
            //��1.0�������ڱ����޸��У�ȷ�������ر����������д�������������Ȼ��������гɽ�û�л����Ļ�����ô������
            //1.0������˭�ȵ��ʹ���˭�����ִ����߼��ᵼ�¶����ʽ����ӻ��������ڵ��޸ģ����������ʱ��ȷ�������ر�
            //�ŵ������������滹�гɽ��ر�û�л�������ô�ȴ���ֱ�����еĳɽ��ر����յ��������Ž��г������㣬������
            //�����ʽ�Ĵ���ͼ򵥻��ˣ���Ϊ�����һ�β��������Բ�����Ҫ���㻹Ҫ�����ٶ����ʽ��ڶ������Ⱥ���ĳɽ��ر���
            //���㴦�����Ǽ򵥵İѶ����¼���㡣

            //2.�ʽ���
            //�Ѵ��ʽ𶳽��¼�������Ľ��ͷ��üӵ������ʽ𣬲���ȥ�ܶ����ʽ�

            //3.�ֲֶ��ᴦ��(���벻����
            //�ѳֲֶ����¼�еĶ����������ֱֲ�ɾ�������¼(ʵ����ֻ���㣬��ɾ�����̺�ͳһɾ��

            //4.�ֲִ������벻����
            //�Ѵӳֲֶ����¼�������ĳֲ����ӵ����óֲ֣�����ȥ�ܶ���ֲ�

            //5.ί�д���
            //    ����ί��״̬���ɽ������������Լ�ί����Ϣ(OrderMessage)

            //6.����һ���ɽ���¼���������ͣ�

            //ʵ�ʵĴ�������

            #region 1.�ʽ𶳽ᴦ����ȡ������Ͷ�����ã�����ȡ�����¼��id

            //������
            decimal preFreezeCapital = 0;
            //�������
            decimal preFreezeCost = 0;

            //�ֲֶ����id
            int holdFreezeLogoId = -1;
            //�ʽ𶳽��id 
            int capitalFreezeLogoId = -1;

            var ca = HKDataAccess.GetCapitalAccountFreeze(EntrustNumber, Entity.Contants.Types.FreezeType.DelegateFreeze);
            if (ca == null)
            {
                string msg = "[�۹ɳ�������]�ʽ𶳽��¼������." + entrustNumString;
                LogHelper.WriteInfo(msg);
                //�Ҳ����ʽ𶳽ᣬһ��������������������ʽ�ȫ��Ϊ0
                //InternalCancelOrderFailureProcess(EntrustNumber, 0, 0, 0, strErrorMessage);
                //return false;
            }
            else
            {
                preFreezeCapital = ca.FreezeCapitalAmount;
                preFreezeCost = ca.FreezeCost;
                capitalFreezeLogoId = ca.CapitalFreezeLogoId;
            }

            #endregion

            #region 2.�ʽ�Ԥ�����Ѵ��ʽ𶳽��¼�������Ľ��ͷ��üӵ������ʽ𣬲���ȥ�ܶ����ʽ𣨷ź����ύ��

            HK_CapitalAccount_DeltaInfo capitalDelta = new HK_CapitalAccount_DeltaInfo();
            capitalDelta.CapitalAccountLogo = CapitalAccountId;
            decimal delta = preFreezeCapital + preFreezeCost;

            HKCapitalMemoryTable capMemory = null;
            if (delta != 0)
            {
                capitalDelta.AvailableCapitalDelta = delta;
                capitalDelta.FreezeCapitalTotalDelta = -delta;

                capMemory = MemoryDataManager.HKCapitalMemoryList.GetByCapitalAccountLogo(CapitalAccountId);
                if (capMemory == null)
                {
                    strMessage = "GT-2702:[�۹ɳ�������]�ʽ��ʻ�������:" + CapitalAccount;
                    LogHelper.WriteDebug(strMessage);
                    return false;
                }
            }

            #endregion

            decimal preFreezeHoldAmount = 0;
            HK_AccountHoldInfo_Delta holdDelta = new HK_AccountHoldInfo_Delta();
            HKHoldMemoryTable holdMemory = null;
            if (tet.BuySellTypeID == (int)Types.TransactionDirection.Selling)
            {
                #region 3.�ֲֶ��ᴦ��(���벻���㣩,��ȡ�ֲֶ����¼�еĶ������Ͷ���id

                var hold = HKDataAccess.GetHoldAccountFreeze(EntrustNumber,
                                                             Entity.Contants.Types.FreezeType.DelegateFreeze);
                if (hold == null)
                {
                    string msg = "[�۹ɳ�������]�ֲֶ��᲻����";
                    LogHelper.WriteInfo(msg);
                    //�ֲֶ��᲻���ڣ�һ�����г����������ֲֶ�����Ϊ0
                    //InternalCancelOrderFailureProcess(EntrustNumber, -preFreezeCapital, -preFreezeCost, 0, strErrorMessage);
                    //return false;
                }
                else
                {
                    preFreezeHoldAmount = hold.PrepareFreezeAmount;
                    holdFreezeLogoId = hold.HoldFreezeLogoId;
                }

                #region ��鳷����

                decimal cancelOrderVolume = 0;
                if (isErrorPriceCancelRpt)
                {
                    cancelOrderVolume = EntrustAmount;
                }
                else
                {
                    cancelOrderVolume = rde.OrderVolume;
                }

                if (cancelOrderVolume != preFreezeHoldAmount)
                {
                    string format =
                        "�۹ɳ�������-�ر������������ݿⶳ���������[EntrustNumber={0},CancelOrderVolume={1},FreezeHoldAmount={2}]";
                    string desc = string.Format(format, EntrustNumber, cancelOrderVolume, preFreezeHoldAmount);
                    preFreezeHoldAmount = cancelOrderVolume;
                    LogHelper.WriteDebug(desc);
                }

                #endregion

                holdMemory = MemoryDataManager.HKHoldMemoryList.GetByAccountHoldLogoId(HoldingAccountId);
                if (holdMemory == null)
                {
                    holdMemory = HKCommonLogic.GetHoldMemoryTable(HoldingAccount, Code, CurrencyType);
                }

                if (holdMemory == null)
                {
                    strMessage = "GT-2703:[�۹ɳ�������]�ֲ��˻�������:" + HoldingAccount;
                    return false;
                }

                var holdData = holdMemory.Data;
                holdDelta.Data = holdData;
                holdDelta.AccountHoldLogoId = holdData.AccountHoldLogoID;
                holdDelta.AvailableAmountDelta += preFreezeHoldAmount;
                holdDelta.FreezeAmountDelta -= preFreezeHoldAmount;

                #endregion
            }

            tet.CancelAmount = (int)rde.OrderVolume;
            SetOrderState(tet);
            if (isErrorPriceCancelRpt)
            {
                tet.CancelAmount = (int)EntrustAmount;
                tet.OrderMessage = rde.Message;
            }

            //ֻҪ�д�����Ϣ����д��ί��
            if (!string.IsNullOrEmpty(rde.Message))
            {
                tet.OrderMessage = rde.Message;
            }

            HK_TodayTradeInfo trade = null;

            List<HK_TodayTradeInfo> multi_Trade = new List<HK_TodayTradeInfo>();

            #region ���ݿ��ύ����

            isSuccess = false;
            var tet2 = tet;
            Database db = DatabaseFactory.CreateDatabase();
            try
            {
                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        ReckoningTransaction tm = new ReckoningTransaction();
                        tm.Database = db;
                        tm.Transaction = transaction;

                        HKDataAccess.ClearCapitalFreeze(capitalFreezeLogoId, db, transaction);

                        if (tet.BuySellTypeID == (int)Types.TransactionDirection.Selling)
                        {
                            HKDataAccess.ClearHoldFreeze(holdFreezeLogoId, db, transaction);
                        }

                        HKDataAccess.UpdateEntrustTable(tet2, db, transaction);

                        //����ҲҪ���뵱�ճɽ�����
                        ReckoningTransaction rt = new ReckoningTransaction();
                        rt.Database = db;
                        rt.Transaction = transaction;

                        bool isInternalCancelOrder = false;
                        if (rde is CancelOrderEntityEx)
                        {
                            var rde2 = (CancelOrderEntityEx)rde;
                            isInternalCancelOrder = rde2.IsInternalCancelOrder;
                        }
                        #region update by:��� update date:2009-11-6
                        #region update=================old code=========
                        //update=================old code=========
                        //HKCommonLogic.BuildHKCancelRpt(tet2, rde, rt, out trade, isInternalCancelOrder);

                        //if (isMultiCancelOrder)
                        //{
                        //    foreach (var id in rdeEx.IDList)
                        //    {
                        //        //������ɺ�������ݿ��б���ĳɽ��ر�
                        //        CrashManager.GetInstance().DeleteEntity(id, rt);
                        //    }
                        //}
                        //else
                        //{
                        //    //������ɺ�������ݿ��б���ĳɽ��ر�
                        //    CrashManager.GetInstance().DeleteEntity(rde.Id, rt);
                        //}
                        //update=================old code=========
                        #endregion

                        if (isMultiCancelOrder)
                        {
                            for (int i = 0; i < rdeEx.IDList.Count; i++)
                            {
                                CancelOrderEntity newRde = new CancelOrderEntity();
                                newRde = rdeEx;
                                newRde.Id = rdeEx.IDList[i]; //��Ϊ����������ID�Ѿ���Ϊ�����е�
                                newRde.OrderVolume = rdeEx.OrderVolumeList[i];//�������Ҫ�ֶ�ʼ�¼��Ҫ��¼֮ǰ�ĳ�����
                                HK_TodayTradeInfo mtrade = null;

                                //���ɳɽ���¼��Ⲣ������ɽ��ر�����
                                HKCommonLogic.BuildHKCancelRpt(tet2, newRde, rt, out mtrade, isInternalCancelOrder);

                                if (mtrade != null)
                                {
                                    multi_Trade.Add(mtrade);
                                }

                                //������ɺ�������ݿ��б���ĳɽ��ر�
                                CrashManager.GetInstance().DeleteEntity(rdeEx.IDList[i], rt);
                            }
                        }
                        else
                        {
                            //���ɳɽ���¼��Ⲣ������ɽ��ر�����
                            HKCommonLogic.BuildHKCancelRpt(tet2, rde, rt, out trade, isInternalCancelOrder);
                            //������ɺ�������ݿ��б���ĳɽ��ر�
                            CrashManager.GetInstance().DeleteEntity(rde.Id, rt);
                        }
                        #endregion
                        //�ʽ����
                        if (delta != 0)
                        {
                            capMemory.AddDeltaToDB(capitalDelta, db, transaction);
                        }

                        //�ֲֲ���
                        if (tet.BuySellTypeID == (int)Types.TransactionDirection.Selling)
                        {
                            if (preFreezeHoldAmount != 0)
                            {
                                holdMemory.AddDeltaToDB(holdDelta, db, transaction);
                            }
                        }

                        transaction.Commit();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        LogHelper.WriteError(ex.Message, ex);
                        strMessage = "GT-2704:[�۹ɳ�������]�ύ�����ݿ�ʧ��";

                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.Message, ex);
                strMessage = "GT-2704:[�۹ɳ�������]�ύ�����ݿ�ʧ��";
            }

            if (isSuccess)
            {
                if (delta != 0)
                {
                    capMemory.AddDeltaToMemory(capitalDelta);
                }

                if (tet.BuySellTypeID == (int)Types.TransactionDirection.Selling)
                {
                    if (preFreezeHoldAmount != 0)
                    {
                        holdMemory.AddDeltaToMemory(holdDelta);

                        holdMemory.ReadAndWrite(h =>
                                                    {
                                                        if (h.FreezeAmount < 0)
                                                        {
                                                            h.FreezeAmount = 0;
                                                        }

                                                        return h;
                                                    });
                    }
                }
            }
            else
            {
                return false;
            }

            #endregion

            //������ɺ󽫴�id���뵽������id�б���
            if (isMultiCancelOrder)
            {
                foreach (var id in rdeEx.IDList)
                {
                    AddReckonedID(id);
                }
            }
            else
            {
                AddReckonedID(rde.Id);
            }

            if (rde.OrderVolume == -1)
            {
                CancelAmount = EntrustAmount;
                strMessage = rde.Message;
            }
            else
            {
                CancelAmount += rde.OrderVolume;
            }


            trades = new List<HK_TodayTradeInfo>();
            if (isMultiCancelOrder)
            {
                trades = multi_Trade;
            }
            else
            {
                if (trade != null)
                {
                    trades.Add(trade);
                }
            }

            return true;

            #endregion
        }

        #endregion

        #region �ɽ�����

        private void ProcessDealCommit()
        {
            var list = dealBackList.GetAllAndClear();
            if (Utils.IsNullOrEmpty(list))
                return;

            string format = "�۹ɳɽ�����[EntrustNumber={0},DealBackCount={1}";
            string info = string.Format(format, EntrustNumber, list.Count);
            LogHelper.WriteInfo(info);

            string strMessage = "";
            HK_TodayEntrustInfo tet;
            List<HK_TodayTradeInfo> tradeList;

            bool isSuccess = false;

            List<string> ids = GetIdList(list);

            CrashManager.GetInstance().AddReckoningIDList(ids);

            isSuccess = InstantReckon_Deal(list, ref strMessage, out tet, out tradeList);

            if (!string.IsNullOrEmpty(strMessage))
                HKDataAccess.UpdateEntrustOrderMessage(EntrustNumber, strMessage);

            //ʧ�ܣ���ô���й��ϻָ�����
            if (!isSuccess)
            {
                //�������û�гɹ�����ô�ӱ���Ļر��б���ɾ�����´�CrashManger�����ٴη��ʹ˻ر�����
                LogHelper.WriteInfo("#####################HKReckonUnit.ProcessDealCommit�ɽ�����ʧ��:" + strMessage);
                //"��HKDealBackEntity[ID=" + ido.Id +"]");
                RemoveIDList(ids);
            }
            CrashManager.GetInstance().RemoveReckoningIDList(ids);

            if (isSuccess)
            {
                ReckonEndObject<HK_TodayEntrustInfo, HK_TodayTradeInfo> reckonEndObject =
                    new ReckonEndObject<HK_TodayEntrustInfo, HK_TodayTradeInfo>();
                reckonEndObject.IsSuccess = true;
                reckonEndObject.EntrustTable = tet;
                reckonEndObject.TradeTableList = tradeList;
                reckonEndObject.TradeID = TradeID;
                reckonEndObject.Message = "";

                if (tet != null && tradeList != null)
                    OnEndReckoningEvent(reckonEndObject);
            }

            //����Ƿ��Ѿ�ȫ��������
            if ((TradeAmount + CancelAmount) == EntrustAmount)
            {
                hasDoneDeal = true;
            }

            if (hasDoneDeal)
            {
                bool isCheckSuccess = LastCheckFreezeMoney(EntrustNumber, CapitalAccountId);
                if (!isCheckSuccess)
                {
                    RescueManager.Instance.Record_HK_LastCheckFreezeMoney(EntrustNumber, CapitalAccountId);
                }
            }
            else
            {
                if (isSuccess)
                {
                    CheckFreezeMoney();
                }
            }
        }

        /// <summary>
        /// ���һ������ʱ��Ҫ�Ѷ����¼����
        /// </summary>
        public static bool LastCheckFreezeMoney(string entrustNumber, int capitalAccountId)
        {
            if (string.IsNullOrEmpty(entrustNumber))
                return true;

            if (capitalAccountId == -1)
                return true;

            //�����һ������ʱ��Ҫ�Ѷ����¼���㣬�����ǮҪ�����ʽ���У���������Ҫ�ˡ������ܶ����ʽ�Ҫ��ȥ�ⲿ��Ǯ;

            var caf = HKDataAccess.GetCapitalAccountFreeze(entrustNumber,
                                                           Entity.Contants.Types.FreezeType.DelegateFreeze);

            if (caf == null)
                return false;

            //��Ҫ�����ʽ���е�Ǯ
            decimal needAddMoney = 0;
            if (caf.FreezeCapitalAmount != 0)
            {
                needAddMoney = caf.FreezeCapitalAmount;
            }

            if (caf.FreezeCost != 0)
            {
                needAddMoney += caf.FreezeCost;
            }

            if (caf.FreezeCost == 0 && caf.FreezeCapitalAmount == 0)
                return true;

            HK_CapitalAccount_DeltaInfo delta = new HK_CapitalAccount_DeltaInfo();
            delta.CapitalAccountLogo = capitalAccountId;
            delta.AvailableCapitalDelta = needAddMoney;
            delta.FreezeCapitalTotalDelta = -needAddMoney;

            var capMemory = MemoryDataManager.HKCapitalMemoryList.GetByCapitalAccountLogo(capitalAccountId);
            if (capMemory == null)
            {
                return false;
            }

            bool isSuccess = false;
            Database database = DatabaseFactory.CreateDatabase();
            try
            {
                using (DbConnection connection = database.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        ReckoningTransaction tm = new ReckoningTransaction();
                        tm.Database = database;
                        tm.Transaction = transaction;
                        //1.����ʽ𶳽��¼
                        HKDataAccess.ClearCapitalFreeze(caf.CapitalFreezeLogoId, database, transaction);

                        //2.�޸��ʽ��
                        capMemory.AddDeltaToDB(delta, database, transaction);

                        transaction.Commit();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        LogHelper.WriteError(ex.Message, ex);
                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.Message, ex);
            }

            if (isSuccess)
            {
                capMemory.AddDeltaToMemory(delta);
            }

            return isSuccess;
        }

        /// <summary>
        /// ��鵱ǰί�е��ʽ𶳽�������Ƿ���Ϊ����
        /// </summary>
        private void CheckFreezeMoney()
        {
            if (string.IsNullOrEmpty(EntrustNumber))
                return;

            if (CapitalAccountId == -1)
                return;

            var caf = HKDataAccess.GetCapitalAccountFreeze(EntrustNumber,
                                                           Entity.Contants.Types.FreezeType.DelegateFreeze);

            //��Ҫ���ʽ���п�ȥ��Ǯ
            decimal needRemoveMoney = 0;
            if (caf != null)
            {
                if (caf.FreezeCapitalAmount < 0)
                {
                    needRemoveMoney = -caf.FreezeCapitalAmount;
                    caf.FreezeCapitalAmount = 0;
                }

                if (caf.FreezeCost < 0)
                {
                    needRemoveMoney += (-caf.FreezeCost);
                    caf.FreezeCost = 0;
                }
            }

            var capMemory = MemoryDataManager.HKCapitalMemoryList.GetByCapitalAccountLogo(CapitalAccountId);
            if (capMemory == null)
            {
                return;
            }

            HK_CapitalAccount_DeltaInfo delta = new HK_CapitalAccount_DeltaInfo();
            delta.CapitalAccountLogo = CapitalAccountId;
            delta.AvailableCapitalDelta = -needRemoveMoney;
            delta.FreezeCapitalTotalDelta = needRemoveMoney;

            bool isSuccess = capMemory.AddDelta(delta);
            if (!isSuccess)
            {
                isSuccess = capMemory.AddDelta(delta);
            }

            if (!isSuccess)
            {
            }
        }


        private bool ComputePreprocCapital(HKDealBackEntity ido, string code, Types.TransactionDirection buySellType,
                                           ref string strMessage, ref decimal dealCapital,
                                           ref decimal dealCost, out HKCostResult xhcr)
        {
            bool result = false;
            xhcr = null;

            try
            {
                //��ϵ�λ
                Types.UnitType utMatch = MCService.GetMatchUnitType(code, Types.BreedClassTypeEnum.HKStock);
                //��ϵ�λ -> �Ƽ۵�λ ����
                decimal unitMultiple = MCService.GetTradeUnitScale(Types.BreedClassTypeEnum.HKStock, code, utMatch);
                //��ǰ�ɽ����ö���
                xhcr = MCService.ComputeHKCost(code, Decimal.ToSingle(ido.DealPrice),
                                               decimal.ToInt32(ido.DealAmount), utMatch, buySellType);
                //�ɽ����
                dealCapital = ido.DealAmount * unitMultiple * ido.DealPrice;
                //��������
                dealCapital = Utils.Round(dealCapital);

                //�ɽ�����
                dealCost = xhcr.CoseSum;
                //��������
                dealCost = Utils.Round(dealCost);

                result = true;
            }
            catch (Exception ex)
            {
                strMessage = "GT-2705:[�۹ɳɽ�����]��ί�гɽ�����ɽ������ü����쳣.";
                LogHelper.WriteError(ex.Message, ex);
            }
            return result;
        }

        public bool InstantReckon_Deal(List<HKDealBackEntity> idoList, ref string strMessage,
                                       out HK_TodayEntrustInfo tet, out List<HK_TodayTradeInfo> tradeList)
        {
            #region ==��ʼ������ ==

            //bool result = false;
            strMessage = string.Empty;
            tet = null;
            tradeList = null;
            List<string> ids = GetIdList(idoList);

            string entrustNum = "[EntrustNumber=" + EntrustNumber + "]";

            string format = "��ʱ����HKRecikonUnit.InstantReckon_Deal[�ʽ��ʻ�={0},�ֲ��ʻ�={1},��̨ί�е���={2}]";
            string msg = string.Format(format, CapitalAccount, HoldingAccount, EntrustNumber);
            LogHelper.WriteDebug(msg);

            var hasDealedAmount = TradeAmount + CancelAmount;
            if (hasDealedAmount == EntrustAmount)
            {
                msg = "��ʱ����HKRecikonUnit.InstantReckon_Deal-�ɽ��ر����󣺵�ǰί���Ѿ�������ϣ��ɽ��ر���Ч��EntrustNumber=" +
                      EntrustNumber;
                LogHelper.WriteDebug(msg);

                //��������idList���뵽������id�б���
                AddReckonedIDList(ids);

                //������ݿ��б���ĳɽ��ر�
                foreach (var id in ids)
                {
                    CrashManager.GetInstance().DeleteEntity(id);
                }

                //��ʾ���㻹�ǳɹ��ˣ���ʵ��ڹ��ϻָ�ʱ����Ҫ���·���
                return true;
            }

            List<HKDealBackEntityEx> idoexList;
            HKDealSum dealSum;
            bool isSuccess = ComputeHKDealBacks(idoList, out idoexList, out dealSum, ref strMessage);
            if (!isSuccess)
            {
                strMessage = "GT-2723:[�۹ɳɽ�����]�ɽ��ر�����ʧ��.";
                return false;
            }

            if ((hasDealedAmount + dealSum.AmountSum) > EntrustAmount)
            {
                msg = "��ʱ����HKRecikonUnit.InstantReckon_Deal-�ɽ��ر����󣺳ɽ��ر��ɽ�����{0}+�����������{1}>��ǰί����{2}��EntrustNumber=" +
                      EntrustNumber;

                LogHelper.WriteDebug(msg);
                //��������id���뵽������id�б���
                AddReckonedIDList(ids);

                //������ݿ��б���ĳɽ��ر�
                foreach (var id in ids)
                {
                    CrashManager.GetInstance().DeleteEntity(id);
                }

                //��ʾ���㻹�ǳɹ��ˣ���ʵ��ڹ��ϻָ�ʱ����Ҫ���·���
                return true;
            }

            //ȡ�����ӦƷ�ֵĽ��ױ���
            if (CurrencyType == -1)
            {
                //===Update Date:2009-10-26 ���
                //   CurrencyType = MCService.SpotTradeRules.GetCurrencyTypeByCommodityCode(Code).CurrencyTypeID;
                CM_CurrencyType cm_cur = MCService.CommonPara.GetCurrencyTypeByCommodityCode(Types.BreedClassTypeEnum.HKStock, Code);
                if (cm_cur == null)
                {
                    strMessage = "GT-2723:[�۹ɳɽ�����]���ݴ����ȡ���׻�������ʧ��.";
                    return false;
                }
                CurrencyType = cm_cur.CurrencyTypeID;
                //================
            }


            //�����ƶȻ��ȡ(�˷���û���õ����ݿ�)
            if (CaptitalTradingRule == -1 || HoldingTradingRule == -1)
            {
                int captitalTradingRule;
                int holdingTradingRule;

                #region update by ��� 2009-10-29
                //if (!MCService.SpotTradeRules.GetDeliveryInstitution(Code, out captitalTradingRule,
                //                                                     out holdingTradingRule, ref strMessage))
                //{
                //    strMessage = "GT-2706:[�۹ɳɽ�����]�޷���ȡ�����ƶ�.";

                //    HKDataAccess.UpdateEntrustOrderMessage(entrustNum, strMessage);
                //    return false;
                //}
                if (!MCService.HKTradeRulesProxy.GetHKDeliveryInstitution(Code, out captitalTradingRule,
                                                     out holdingTradingRule, ref strMessage))
                {
                    strMessage = "GT-2706:[�۹ɳɽ�����]�޷���ȡ�����ƶ�.";
                    HKDataAccess.UpdateEntrustOrderMessage(entrustNum, strMessage);
                    return false;
                }
                #endregion

                CaptitalTradingRule = captitalTradingRule;
                HoldingTradingRule = holdingTradingRule;
            }

            //�۹ɵ���ί�лر�
            tet = HKDataAccess.GetTodayEntrustTable(EntrustNumber);
            if (tet == null)
            {
                strMessage = "GT-2707:[�۹ɳɽ�����]�޷���ȡί�ж���ί�е���=" + EntrustNumber;
                LogHelper.WriteDebug(strMessage);
                return false;
            }

            #endregion

            if (BuySellType == Types.TransactionDirection.Buying)
            {
                return HKBuy_InstantReckon_Deal(idoexList, dealSum, ref strMessage, out tet, out tradeList);
            }

            return HKSell_InstantReckon_Deal(idoexList, dealSum, ref strMessage, out tet, out tradeList);
        }

        #endregion

        #region �۹������㴦��

        /// <summary>
        /// �۹��������ύ���
        /// </summary>
        /// <param name="idoExList">��չ�ɽ��ر��б�</param>
        /// <param name="dealSum">�۹ɳɽ��ر�����</param>
        /// <param name="strMessage">������Ϣ</param>
        /// <param name="tet">ί��</param>
        /// <param name="tradeList">�ɽ��б�</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        private bool HKBuy_InstantReckon_Deal(List<HKDealBackEntityEx> idoExList, HKDealSum dealSum,
                                              ref string strMessage, out HK_TodayEntrustInfo tet,
                                              out List<HK_TodayTradeInfo> tradeList)
        {
            //�����㣺
            //1.�ʽ𼰶��ᴦ��
            //(1) ���ݳɽ����ͷ��ã����ʽ𶳽��¼��ȥ��Ӧ��ֵ�������ʽ���ܶ����ʽ��м�ȥ;
            //(2) �������¼��Ǯ������ʱ��ֱ�Ӵ��ʽ��ۣ���������Ҫ������
            //(3) �����һ�γɽ�����ʱ��Ҫ�Ѷ����¼���㣬�����ǮҪ�����ʽ���У������ܶ����ʽ�Ҫ��ȥ�ⲿ��Ǯ;
            //2.�ֲּ����ᴦ��
            //    ���ݳֲֽ������
            //    a.T+0: ���ɽ�������ֱֲ����³ɱ��ۡ������ۣ�����ֲ־��۵ȵȣ���ǣ��ֲֶ����
            //    b.T+N:���ɽ������뵽�ֲֶ�������³ֱֲ�ĳɱ��ۡ������ۣ�����ֲ־��۵ȵȣ����³ֱֲ���ܶ�������
            //3.ί�б���
            //    ���¸���״̬���ɽ�����״̬�ȣ�


            tet = null;
            tradeList = new List<HK_TodayTradeInfo>();

            //�ܳɽ�������capital��cost��
            decimal dealMoneySum = dealSum.CapitalSum + dealSum.CostSum;

            #region 1.�ʽ��Ԥ����

            //�ȼ���ʽ𶳽���еĶ�����ͷ����Ƿ񹻿�

            HK_CapitalAccountFreezeInfo caf;
            if (!GetCapitalFreezeTable(out caf))
            {
                strMessage = "GT-2724:[�۹ɳɽ�����]�޷���ȡ�ʽ𶳽��¼.";

                return false;
            }

            #region �ʽ�����ʽ𶳽Ჹ���߼�����Ϊ�˼򻯴������ٽ��в�����ͳһ��Ϊ�����������м�鲹��

            /*
            decimal freezeCapital = caf.FreezeCapitalAmount;
            decimal freezeCost = caf.FreezeCost;

            //��Ҫ�����ʽ�
            decimal removeCapital = 0;

            //��Ҫ���ķ���
            decimal removeCost = 0;

            //��Ҫ��ȥ�Ķ����ʽ�
            decimal removeFreeze = 0;

            freezeCapital -= dealSum.CapitalSum;
            if (freezeCapital < 0)
            {
                removeCapital = -freezeCapital;
                removeFreeze += caf.FreezeCapitalAmount;
                caf.FreezeCapitalAmount = 0;
            }
            else
            {
                removeFreeze += dealSum.CapitalSum;
                caf.FreezeCapitalAmount -= dealSum.CapitalSum;
            }


            freezeCost -= dealSum.CostSum;
            if (freezeCost < 0)
            {
                removeCost = -freezeCost;
                removeFreeze += caf.FreezeCost;
                caf.FreezeCost = 0;
            }
            else
            {
                removeFreeze += dealSum.CostSum;
                caf.FreezeCost -= dealSum.CostSum;
            }

            //��Ҫ�����ܽ��
            decimal removeTotal = removeCapital + removeCost;

            //��Ҫ�˵�Ǯ
            decimal tuiAll = 0;
            if (isLastReckon)
            {
                //��Ҫ�˵Ľ��
                decimal tuiCapital = caf.FreezeCapitalAmount;
                caf.FreezeCapitalAmount = 0;

                //��Ҫ�˵ķ���
                decimal tuiCost = caf.FreezeCost;
                caf.FreezeCost = 0;

                tuiAll = tuiCapital + tuiCost;
            }
            */

            #endregion

            var capMemory = MemoryDataManager.HKCapitalMemoryList.GetByCapitalAccountLogo(CapitalAccountId);
            if (capMemory == null)
            {
                strMessage = "GT-2708:[�۹ɳɽ�����]�ʽ��ʻ�������:" + CapitalAccount;

                return false;
            }

            HK_CapitalAccount_DeltaInfo capitalDelta = HKBuy_InstantReckon_PreCapitalProcess(dealSum, caf,
                                                                                                  dealMoneySum);
            capitalDelta.CapitalAccountLogo = capMemory.Data.CapitalAccountLogo;
            //�ʽ���������ύ���ȴ���������ɹ�������ֲ�һ���ύ
            //capMemory.AddDelta(capitalDelta);

            #endregion

            #region 2.�ֱֲ�Ԥ����

            bool isSuccess = false;

            HKHoldMemoryTable holdMemory = null;
            holdMemory = HKBuy_InstantReckon_PreHoldingProcess(ref isSuccess, ref strMessage);

            if (!isSuccess)
            {
                //�ٻ�ȡһ��
                holdMemory = HKBuy_InstantReckon_PreHoldingProcess(ref isSuccess, ref strMessage);
            }

            if (!isSuccess)
            {
                strMessage = "GT-2790:[�۹ɳɽ�����]�޷���ȡ�ֲּ�¼.";
                LogHelper.WriteDebug("XHBuy_InstantReckon_Deal_!isSuccess " + strMessage);
                return false;
            }

            if (holdMemory == null)
            {
                strMessage = "GT-2790:[�۹ɳɽ�����]�޷���ȡ�ֲּ�¼.";
                LogHelper.WriteDebug("XHBuy_InstantReckon_Deal_holdMemory == null " + strMessage);

                return false;
            }

            #endregion

            //�����ʽ𶳽�ͳֲֶ���,�Լ�ί����Ϣ
            HK_AcccountHoldFreezeInfo holdFreeze = null;
            bool isFirstHold = false;
            if (HoldingTradingRule != 0)
            {
                holdFreeze = HKBuy_InstantReckon_HoldingFreezeProcess(dealSum, ref isFirstHold);
            }

            tet = HKDataAccess.GetTodayEntrustTable(EntrustNumber);
            var tet2 = tet;

            HKBuy_InstantReckon_EntrustProcess(dealSum, tet2);

            var tradeList2 = tradeList;

            List<string> ids = GetIDList2(idoExList);

            #region ���ݿ��ύ����

            isSuccess = false;
            Database database = DatabaseFactory.CreateDatabase();
            try
            {
                using (DbConnection connection = database.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    ReckoningTransaction tm = new ReckoningTransaction();
                    tm.Database = database;
                    tm.Transaction = transaction;
                    try
                    {
                        //1.�����ʽ𶳽�
                        HK_CapitalAccountFreezeDal cafDal =
                            new HK_CapitalAccountFreezeDal();
                        cafDal.Update(caf, tm);

                        //2.���³ֲֶ���
                        if (HoldingTradingRule != 0)
                        {
                            HK_AcccountHoldFreezeDal holdDal =
                                new HK_AcccountHoldFreezeDal();
                            if (isFirstHold)
                                holdDal.Add(holdFreeze, tm.Database, tm.Transaction);
                            else
                                holdDal.Update(holdFreeze, tm.Database, tm.Transaction);
                        }

                        //3.����ί�б�
                        HKDataAccess.UpdateEntrustTable(tet2, tm.Database,
                                                        tm.Transaction);
                        //�ɽ�ʱ��
                        //tet2.OfferTime = ido.DealTime;

                        //4.���ɳɽ���¼
                        BuildTradeList(tet2, tm, idoExList, tradeList2);

                        //5.������ɺ�������ݿ��б���ĳɽ��ر�
                        CrashManager.GetInstance().DeleteEntityList(ids, tm);

                        //6.�����ʽ��
                        capMemory.AddDeltaToDB(capitalDelta, tm.Database, tm.Transaction);

                        transaction.Commit();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        LogHelper.WriteError(ex.Message, ex);
                        strMessage = "GT-2709:[�۹ɳɽ�����]���ݿ��ύʧ��";

                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.Message, ex);
                strMessage = "GT-2709:[�۹ɳɽ�����]���ݿ��ύʧ��";
            }

            if (isSuccess)
            {
                capMemory.AddDeltaToMemory(capitalDelta);
            }
            else
            {
                return false;
            }

            #endregion

            #region 4.�ֱֲ������ύ����

            //֮���Էŵ����ִ�У�����Ϊ����ʱ�ֱֲ�Ĳ���̫�࣬���Իع������Էŵ���󣬱�֤����ִ�У�

            isSuccess = HKBuy_InstantReckon_HoldingProcess(holdMemory, dealSum, HoldingTradingRule);

            if (!isSuccess)
            {
                //���ɹ�����ִ��3��
                for (int i = 0; i < 3; i++)
                {
                    //���ɹ�����ִ��һ��
                    isSuccess = HKBuy_InstantReckon_HoldingProcess(holdMemory, dealSum, HoldingTradingRule);

                    if (isSuccess)
                        break;
                }
            }

            if (!isSuccess)
            {
                //һ��Ҫִ�У���¼�������´�ִ��
                RescueManager.Instance.Record_HKBuy_InstantReckon_HoldingProcess(HoldingAccountId, dealSum,
                                                                                 HoldingTradingRule);
            }

            #endregion

            #region �ʽ�������ύ����(����)

            //isSuccess = capMemory.AddDelta(capitalDelta);
            //if (!isSuccess)
            //{
            //    //���ɹ�����ִ��3��
            //    for (int i = 0; i < 3; i++)
            //    {
            //        //���ɹ�����ִ��һ��
            //        isSuccess = capMemory.AddDelta(capitalDelta);

            //        if (isSuccess)
            //            break;
            //    }
            //}

            //if (!isSuccess)
            //{
            //    RescueManager.Instance.DoXHReckonCapitalFailureProcess(capitalDelta);
            //}

            #endregion

            //������ɺ󽫴�id���뵽������id�б���
            AddReckonedIDList(ids);
            strMessage = string.Empty;


            if (tet.EntrustAmount < (TradeAmount + dealSum.AmountSum) || tet.EntrustAmount < TradeAmount)
            {
                LogHelper.WriteInfo("XHOrderLogicFlow.InstantReckon_Deal2����ɽ�������");
            }
            else
            {
                TradeAmount += dealSum.AmountSum;
            }

            return true;
        }

        /// <summary>
        /// ������-ί�б���
        /// </summary>
        /// <param name="dealSum"></param>
        /// <param name="tet2"></param>
        private void HKBuy_InstantReckon_EntrustProcess(HKDealSum dealSum, HK_TodayEntrustInfo tet2)
        {
            //��ϵ�λ
            Types.UnitType utMatch = MCService.GetMatchUnitType(tet2.Code, Types.BreedClassTypeEnum.HKStock);
            //��ϵ�λ -> �Ƽ۵�λ ����
            decimal unitMultiple = MCService.GetTradeUnitScale(Types.BreedClassTypeEnum.HKStock, tet2.Code, utMatch);

            //�ɽ����ۣ��۸��Ǳ��۵�λ�������Ǵ�ϵ�λ��������ʱ��Ҫͳһ�����Ѵ�ϵ�λתΪ���۵�λ��
            decimal tradeAveragePrice =
                (tet2.TradeAveragePrice * tet2.TradeAmount * unitMultiple +
                 dealSum.CapitalSum) / (tet2.TradeAmount * unitMultiple + dealSum.AmountSum * unitMultiple);

            tradeAveragePrice = Utils.Round(tradeAveragePrice);

            tet2.TradeAveragePrice = tradeAveragePrice;
            //�ɽ���
            tet2.TradeAmount =
                Convert.ToInt32(tet2.TradeAmount + dealSum.AmountSum);

            SetOrderState(tet2);

            tet2.OrderMessage = "";
        }

        /// <summary>
        /// ������-�ֲֶ��ᴦ��
        /// </summary>
        /// <param name="dealSum"></param>
        /// <param name="isFirstHold"></param>
        /// <returns></returns>
        private HK_AcccountHoldFreezeInfo HKBuy_InstantReckon_HoldingFreezeProcess(HKDealSum dealSum,
                                                                                        ref bool isFirstHold)
        {
            HK_AcccountHoldFreezeInfo holdFreeze;
            holdFreeze = HKDataAccess.GetHoldAccountFreeze(EntrustNumber,
                                                           Entity.Contants.Types.FreezeType.DelegateFreeze);
            if (holdFreeze == null)
            {
                holdFreeze = new HK_AcccountHoldFreezeInfo();
                //��������
                holdFreeze.PrepareFreezeAmount = Convert.ToInt32(dealSum.AmountSum);
                //����ʱ��
                holdFreeze.FreezeTime = DateTime.Now;
                //�ⶳʱ��
                holdFreeze.ThawTime = DateTime.Now.AddDays(HoldingTradingRule);
                //ί�е���
                holdFreeze.EntrustNumber = EntrustNumber;
                //��������
                holdFreeze.FreezeTypeID = (int)Entity.Contants.Types.FreezeType.ReckoningFreeze;
                //�۹��ʻ��ֱֲ�ʶ
                holdFreeze.AccountHoldLogo = HoldingAccountId;
                isFirstHold = true;
            }
            else
            {
                //֮ǰ���ڶ���
                //��������
                holdFreeze.PrepareFreezeAmount += Convert.ToInt32(dealSum.AmountSum);
            }
            return holdFreeze;
        }

        /// <summary>
        /// ������-�ʽ���ʽ𶳽��Ԥ����
        /// </summary>
        /// <param name="dealSum"></param>
        /// <param name="caf"></param>
        /// <param name="dealMoneySum"></param>
        private HK_CapitalAccount_DeltaInfo HKBuy_InstantReckon_PreCapitalProcess(HKDealSum dealSum,
                                                                                       HK_CapitalAccountFreezeInfo
                                                                                           caf, decimal dealMoneySum)
        {
            //ע�⣺Ϊ�˼򻯴����������ڼ䲻�ٽ��ж����ʽ�Ĳ������㼰�����ٲ��ļ��㣬ͳһ�ڱ������������
            //���ʽ𶳽����м��У�飬�����������ڲ���ֱ�����ʽ𶳽���ϼ�ȥ�ɽ����ͳɽ�����
            caf.FreezeCapitalAmount -= dealSum.CapitalSum;
            caf.FreezeCost -= dealSum.CostSum;


            //******************�κεط�ִ��ʧ�ܣ�����Ҫ�ع��Ĳ���1********************
            //�ʽ����ܶ������ȥ���γɽ��Ľ��ͷ��úͣ���Ϊ�ʽ𶳽����ȥ����ô��Ǯ��
            HK_CapitalAccount_DeltaInfo capitalDelta = new HK_CapitalAccount_DeltaInfo();
            capitalDelta.FreezeCapitalTotalDelta = -dealMoneySum;
            capitalDelta.CapitalAccountLogo = CapitalAccountId;

            //�ʽ������ִ�и��£��ں���ͳһ����
            //capMemory.AddDelta(capitalDelta);

            return capitalDelta;
        }

        /// <summary>
        /// ��-�ֱֲ�Ԥ����
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        private HKHoldMemoryTable HKBuy_InstantReckon_PreHoldingProcess(ref bool isSuccess, ref string strMessage)
        {
            HKHoldMemoryTable holdMemory = null;
            try
            {
                holdMemory = MemoryDataManager.HKHoldMemoryList.GetByHoldAccountAndCurrencyType(HoldingAccount, Code,
                                                                                                CurrencyType);
                //holdMemory = MemoryDataManager.HKHoldMemoryList.GetByAccountHoldLogoId(HoldingAccountId);
                //����ֲ�Ϊ�գ���ô�ȴ����ݿ���أ����ǲ����ڴ��û�м���
                if (holdMemory == null)
                {
                    HK_AccountHoldInfo hold = GetHoldFromDB(HoldingAccount, Code, CurrencyType);
                    //������ݿ��У���ôֱ�Ӽ��ص��ڴ����
                    if (hold != null)
                    {
                        string format =
                            "XHBuy_InstantReckon_PreHoldingProcess���ݿ���ڳֲ֣�ֱ�Ӽ��ص��ڴ����[Code={0},HoldAccount={1}]";
                        string desc = string.Format(format, Code, HoldingAccount);
                        LogHelper.WriteDebug(desc);

                        isSuccess = MemoryDataManager.HKHoldMemoryList.AddAccountHoldTableToMemory(hold);

                        if (!isSuccess)
                        {
                            isSuccess = MemoryDataManager.HKHoldMemoryList.AddAccountHoldTableToMemory(hold);
                        }

                        if (!isSuccess)
                        {
                            desc += "-����ʧ�ܣ��Ѵ���";
                            LogHelper.WriteDebug(desc);
                        }
                    }
                    else
                    {
                        //������ݿ�Ҳû�У���ô�����һ�����룬�½�һ���յĳֲ֣������뵽���ݿ���ڴ����
                        string format =
                            "XHBuy_InstantReckon_PreHoldingProcess���ݿⲻ���ڳֲ֣��½�һ���յĳֲ֣������뵽���ݿ���ڴ����[Code={0},HoldAccount={1}]";
                        string desc = string.Format(format, Code, HoldingAccount);
                        LogHelper.WriteDebug(desc);

                        hold = new HK_AccountHoldInfo();

                        hold.UserAccountDistributeLogo = HoldingAccount;
                        hold.Code = Code;
                        hold.AvailableAmount = 0;
                        hold.FreezeAmount = 0;
                        hold.CurrencyTypeID = CurrencyType;

                        isSuccess = MemoryDataManager.HKHoldMemoryList.AddAccountHoldTable(hold);

                        if (!isSuccess)
                        {
                            isSuccess = MemoryDataManager.HKHoldMemoryList.AddAccountHoldTable(hold);
                        }

                        if (!isSuccess)
                        {
                            desc += "-�½�ʧ�ܣ��Ѵ���";
                            LogHelper.WriteDebug(desc);
                        }
                    }

                    holdMemory = MemoryDataManager.HKHoldMemoryList.GetByHoldAccountAndCurrencyType(HoldingAccount, Code,
                                                                                                    CurrencyType);

                    if (holdMemory == null)
                    {
                        int holdId = MemoryDataManager.HKHoldMemoryList.GetAccountHoldLogoId(HoldingAccount, Code,
                                                                                             CurrencyType);
                        if (holdId != -1)
                            holdMemory = MemoryDataManager.HKHoldMemoryList.GetByAccountHoldLogoId(holdId);
                    }

                    if (holdMemory == null)
                    {
                        hold = GetHoldFromDB(HoldingAccount, Code, CurrencyType);
                        isSuccess = MemoryDataManager.HKHoldMemoryList.AddAccountHoldTableToMemory(hold);
                    }

                    holdMemory = MemoryDataManager.HKHoldMemoryList.GetByHoldAccountAndCurrencyType(HoldingAccount, Code,
                                                                                                    CurrencyType);


                    if (holdMemory == null)
                    {
                        strMessage = "GT-2725:[�۹ɳɽ�����]�޷���ȡ�ֲּ�¼.";
                        LogHelper.WriteDebug("HKBuy_InstantReckon_PreHoldingProcess" + strMessage);
                        isSuccess = false;
                        return null;
                    }
                }

                HoldingAccountId = holdMemory.Data.AccountHoldLogoID;
                isSuccess = true;
            }
            catch (Exception ex)
            {
                strMessage = "GT-2720:[�۹ɳɽ�����]�ֲִ����쳣:" + ex;
                LogHelper.WriteError(ex.Message, ex);
            }

            return holdMemory;
        }

        /// <summary>
        /// �����ݿ��ȡ�ֲ���Ϣ
        /// </summary>
        /// <param name="holdingAccount"></param>
        /// <param name="code"></param>
        /// <param name="currencyType"></param>
        /// <returns></returns>
        private HK_AccountHoldInfo GetHoldFromDB(string holdingAccount, string code, int currencyType)
        {
            HK_AccountHoldInfo hold = null;

            HK_AccountHoldDal dal = new HK_AccountHoldDal();
            hold = dal.GetHKAccountHoldInfo(holdingAccount, code, currencyType);

            return hold;
        }


        /// <summary>
        /// ������-�ֲִ���
        /// </summary>
        /// <param name="holdMemory"></param>
        /// <param name="dealSum"></param>
        /// <param name="holdingTradingRule"></param>
        private static bool HKBuy_InstantReckon_HoldingProcess(HKHoldMemoryTable holdMemory, HKDealSum dealSum,
                                                               int holdingTradingRule)
        {
            //0813:�������ۣ���λת�������±�׼���У�
            //1.�۸���أ��ԼƼ۵�λΪ׼����ɽ��ۣ����ۣ��ɱ��ۣ������۵ȵ�
            //2.�ֲ���أ��Խ��׵�λΪ׼����ɽ������ֲ����ȵ�
            //����ί�У�
            //ծȯ   ί����=1 ί�м�=103  �ɽ���=1 �ɽ���103
            //����ί�������ɽ����ĵ�λ���֣�ί�мۡ��ɽ��۵ĵ�λ���ţ�1��=10��
            //�ܽ��=ί������ί�м� ��Ϊ��/�۵ĵ�λ��ƥ�䣬������Ҫ����ת��
            //�ܽ��=(1��10)��103=1030������= 1030/(1��10)=103����ÿ��103Ԫ
            //���浽���ݿ��е��ֶΣ�Ҳ�ǰ��������򱣴棬����/�۸��в�ͬ�ĵ�λ

            //return Internal_XHBuy_InstantReckon_HoldingProcess(holdMemory, dealSum, holdingTradingRule);

            string code = holdMemory.Data.Code;
            //��ϵ�λ
            Types.UnitType utMatch = MCService.GetMatchUnitType(code, Types.BreedClassTypeEnum.HKStock);
            //��ϵ�λ -> �Ƽ۵�λ ����
            decimal unitMultiple = MCService.GetTradeUnitScale(Types.BreedClassTypeEnum.HKStock, code, utMatch);

            bool isSuccess = holdMemory.ReadAndWrite(hold =>
                                                         {
                                                             decimal holdAmount = hold.AvailableAmount +
                                                                                  hold.FreezeAmount;
                                                             decimal costPrice = (holdAmount * hold.CostPrice * unitMultiple +
                                                                                  dealSum.CapitalSum + dealSum.CostSum) /
                                                                                 (holdAmount * unitMultiple +
                                                                                  dealSum.AmountSum * unitMultiple);
                                                             //��������
                                                             costPrice = Utils.Round(costPrice);

                                                             decimal holdPrice = MCService.GetHoldPrice(code, costPrice,
                                                                                                        (holdAmount *
                                                                                                         unitMultiple +
                                                                                                         dealSum.
                                                                                                             AmountSum *
                                                                                                         unitMultiple));
                                                             //��������
                                                             holdPrice = Utils.Round(holdPrice);

                                                             hold.CostPrice = costPrice;
                                                             hold.BreakevenPrice = holdPrice;

                                                             //�ֲ־���=���ֲ־���*�ֲ���+����ɽ���*����ۣ�/(�ֲ���+����ɽ���)
                                                             decimal holdAveragePrice = (hold.HoldAveragePrice *
                                                                                         holdAmount * unitMultiple +
                                                                                         dealSum.CapitalSum) /
                                                                                        (holdAmount * unitMultiple +
                                                                                         dealSum.AmountSum * unitMultiple);
                                                             //��������
                                                             holdAveragePrice = Utils.Round(holdAveragePrice);
                                                             hold.HoldAveragePrice = holdAveragePrice;

                                                             //T+0
                                                             if (holdingTradingRule == 0)
                                                             {
                                                                 hold.AvailableAmount += dealSum.AmountSum;
                                                             }
                                                             else
                                                             {
                                                                 hold.FreezeAmount += dealSum.AmountSum;
                                                             }

                                                             return hold;
                                                         });
            return isSuccess;
        }

        /// <summary>
        /// ��RescueManager�ָ��ֳ���ֲֲ���ʱ����
        /// </summary>
        /// <param name="holdingAccoutnId"></param>
        /// <param name="dealSum"></param>
        /// <returns></returns>
        public static bool DoHKBuy_HoldingRescue(int holdingAccoutnId, HKDealSum dealSum, int holdingTradingRule)
        {
            HKHoldMemoryTable holdMemory = MemoryDataManager.HKHoldMemoryList.GetByAccountHoldLogoId(holdingAccoutnId);
            if (holdMemory == null)
            {
                holdMemory = HKCommonLogic.GetHoldMemoryTable(holdingAccoutnId);
            }

            if (holdMemory == null)
            {
                return true;
            }

            return HKBuy_InstantReckon_HoldingProcess(holdMemory, dealSum, holdingTradingRule);
        }

        #endregion

        #region �۹������㴦��

        /// <summary>
        /// �۹��������ύ���
        /// </summary>
        /// <param name="idoExList">��չ�ɽ��ر��б�</param>
        /// <param name="dealSum">�۹ɳɽ��ر�����</param>
        /// <param name="strMessage">������Ϣ</param>
        /// <param name="tet">ί��</param>
        /// <param name="tradeList">�ɽ��б�</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        private bool HKSell_InstantReckon_Deal(List<HKDealBackEntityEx> idoExList, HKDealSum dealSum,
                                               ref string strMessage, out HK_TodayEntrustInfo tet,
                                               out List<HK_TodayTradeInfo> tradeList)
        {
            //�����㣺
            //1.�ʽ𼰶��ᴦ��
            //(1) ���ݳɽ����ͷ��ã����ʽ𶳽��¼��ȥ��Ӧ��ֵ�������ʽ���ܶ����ʽ��м�ȥ;
            //(2) �������¼��Ǯ������ʱ��ֱ�Ӵ��ʽ������ʽ��пۣ���������Ҫ������
            //(3) �����һ������ʱ��Ҫ�Ѷ����¼���㣬�����ǮҪ�����ʽ���У���������Ҫ�ˡ������ܶ����ʽ�Ҫ��ȥ�ⲿ��Ǯ;
            //    (4)�����ʽ𽻸����
            //a.T+0: �ɽ��������ʽ������ʽ�
            //        b.T+N:����һ���ʽ𶳽��¼���������ʽ���ܶ����¼�м��ϡ�
            //    (5)�������������ʵ��ӯ�����뵽�ʽ���ۼ���ʵ��ӯ���С�
            //2.�ֲּ����ᴦ��
            //    ���ɽ����ӳֱֲ���ܶ������ϼ�ȥ�����һ�Ҫ�ӳֲֶ����м�ȥ��
            //3.ί�б���
            //    (1)���¸���״̬���ɽ�����״̬�ȣ���
            //    (2)�������������ʵ��ӯ�����뵽��ʵ��ӯ����


            tet = null;
            tradeList = new List<HK_TodayTradeInfo>();

            #region 1.�ʽ��Ԥ����

            //�ȼ���ʽ𶳽���еĶ�����ͷ����Ƿ񹻿�
            HK_CapitalAccountFreezeInfo caf;
            if (!GetCapitalFreezeTable(out caf))
            {
                strMessage = "GT-2724:[�۹ɳɽ�����]�޷���ȡ�ʽ𶳽��¼.";

                return false;
            }

            var capMemory = MemoryDataManager.HKCapitalMemoryList.GetByCapitalAccountLogo(CapitalAccountId);
            if (capMemory == null)
            {
                strMessage = "GT-2721:[�۹ɳɽ�����]�ʽ��ʻ�������:" + CapitalAccount;

                return false;
            }

            //ע�⣺Ϊ�˼򻯴����������ڼ䲻�ٽ��ж����ʽ�Ĳ������㼰�����ٲ��ļ��㣬ͳһ�ڱ������������
            //���ʽ𶳽����м��У�飬�����������ڲ���ֱ�����ʽ𶳽���ϼ�ȥ�ɽ����ͳɽ�����
            HK_CapitalAccount_DeltaInfo capitalDelta = HKSell_InstantReckon_PreCapitalProcess(caf, dealSum);
            capitalDelta.CapitalAccountLogo = capMemory.Data.CapitalAccountLogo;

            #endregion

            #region 2.�ֱֲ�Ԥ����

            bool isSuccess = false;
            HKHoldMemoryTable holdMemory = null;
            holdMemory = HKSell_InstantReckon_PreHoldingProcess(holdMemory, ref isSuccess, ref strMessage);

            if (!isSuccess)
            {
                strMessage = "GT-2790:[�۹ɳɽ�����]�޷���ȡ�ֲּ�¼.";
                LogHelper.WriteDebug("XHSell_InstantReckon_Deal_!isSuccess " + strMessage);

                return false;
            }

            if (holdMemory == null)
            {
                strMessage = "GT-2790:[�۹ɳɽ�����]�޷���ȡ�ֲּ�¼.";
                LogHelper.WriteDebug("XHSell_InstantReckon_Deal_holdMemory == null " + strMessage);

                return false;
            }

            decimal holdAveragePrice = holdMemory.Data.HoldAveragePrice;

            HK_AccountHoldInfo_Delta holdDelta = new HK_AccountHoldInfo_Delta();
            var holdData = holdMemory.Data;

            string holdFormat =
                "XHSell_InstantReckon_Deal��ȡ�ֲ־���[AccountHoldLogoId={0},UserAccountDistributeLogo={1},Code={2},HoldAveragePrice={3}]";
            string holdDesc = string.Format(holdFormat, holdData.AccountHoldLogoID, holdData.UserAccountDistributeLogo,
                                            holdData.Code,
                                            holdData.HoldAveragePrice);
            LogHelper.WriteInfo(holdDesc);

            holdDelta.AccountHoldLogoId = holdData.AccountHoldLogoID;
            holdDelta.FreezeAmountDelta -= dealSum.AmountSum;

            #endregion

            #region 3.�ʽ𶳽�ͳֲֶ��ᴦ��

            //�����ʽ𶳽�ͳֲֶ���,�Լ�ί����Ϣ
            HK_AcccountHoldFreezeInfo holdFreeze = null;

            holdFreeze = HKDataAccess.GetHoldAccountFreeze(EntrustNumber,
                                                           Entity.Contants.Types.FreezeType.DelegateFreeze);
            if (holdFreeze == null)
            {
                strMessage = "GT-2789:[�۹ɳɽ�����]�޷���ȡ�ֲֶ����¼.";
                LogHelper.WriteInfo(strMessage);
                //�ֲֶ��᲻���ڣ�һ�����г����������ٴ���ֲֶ����ֻ�Գֱֲ�Ķ���������
                //return false;
            }
            else
            {
                holdFreeze.PrepareFreezeAmount -= Convert.ToInt32(dealSum.AmountSum);
                if (holdFreeze.PrepareFreezeAmount < 0)
                {
                    holdFreeze.PrepareFreezeAmount = 0;
                }
            }


            tet = HKDataAccess.GetTodayEntrustTable(EntrustNumber);
            var tet2 = tet;

            decimal hasDoneProfit = HKSell_InstantReckon_EntrustProcess(dealSum, tet2, holdAveragePrice);

            //�ڸ۹��ʽ���������ۼ���ʵ��ӯ�����ֶ�
            capitalDelta.HasDoneProfitLossTotalDelta = hasDoneProfit;

            var tradeList2 = tradeList;
            List<string> ids = GetIDList2(idoExList);

            #endregion

            #region 4.���ݿ��ύ����

            isSuccess = false;
            Database database = DatabaseFactory.CreateDatabase();
            try
            {
                using (DbConnection connection = database.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    ReckoningTransaction tm = new ReckoningTransaction();
                    tm.Database = database;
                    tm.Transaction = transaction;
                    try
                    {
                        //��T+0ʱ�����ʽ𶳽�
                        HKSell_InstantReckon_CapitalFreezeProcess(tet2, dealSum, tm);

                        //1.�����ʽ𶳽�
                        HK_CapitalAccountFreezeDal cafDal =
                            new HK_CapitalAccountFreezeDal();
                        cafDal.Update(caf, tm);

                        //2.���³ֲֶ���
                        if (holdFreeze != null)
                        {
                            HK_AcccountHoldFreezeDal holdDal = new HK_AcccountHoldFreezeDal();
                            holdDal.Update(holdFreeze, tm.Database, tm.Transaction);
                        }

                        //3.����ί�б�
                        HKDataAccess.UpdateEntrustTable(tet2, tm.Database,
                                                        tm.Transaction);

                        //4.���ɳɽ���¼
                        BuildTradeList(tet2, tm, idoExList, tradeList2);

                        //5.�ʽ�����
                        capMemory.AddDeltaToDB(capitalDelta, tm.Database, tm.Transaction);

                        //6.�ֱֲ����
                        holdMemory.AddDeltaToDB(holdDelta, tm.Database, tm.Transaction);

                        //7.������ɺ�������ݿ��б���ĳɽ��ر�
                        CrashManager.GetInstance().DeleteEntityList(ids, tm);

                        transaction.Commit();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        LogHelper.WriteError(ex.Message, ex);
                        strMessage = "GT-2722:[�۹ɳɽ�����]���ݿ��ύʧ��";

                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.Message, ex);
                strMessage = "GT-2722:[�۹ɳɽ�����]���ݿ��ύʧ��";
            }

            if (isSuccess)
            {
                capMemory.AddDeltaToMemory(capitalDelta);
                holdMemory.AddDeltaToMemory(holdDelta);
                holdMemory.ReadAndWrite(h =>
                                            {
                                                if (h.FreezeAmount < 0)
                                                {
                                                    h.FreezeAmount = 0;
                                                }

                                                return h;
                                            });
            }
            else
            {
                return false;
            }

            #endregion

            //������ɺ󽫴�id���뵽������id�б���
            AddReckonedIDList(ids);
            strMessage = string.Empty;


            if (tet.EntrustAmount < (TradeAmount + dealSum.AmountSum) || tet.EntrustAmount < TradeAmount)
            {
                LogHelper.WriteInfo("XHOrderLogicFlow.InstantReckon_Deal2����ɽ�������");
            }
            else
            {
                TradeAmount += dealSum.AmountSum;
            }

            return true;
        }

        /// <summary>
        /// ������-�ʽ𶳽ᴦ��
        /// </summary>
        /// <param name="tet2"></param>
        /// <param name="dealSum"></param>
        /// <param name="tm"></param>
        private void HKSell_InstantReckon_CapitalFreezeProcess(HK_TodayEntrustInfo tet2, HKDealSum dealSum,
                                                               ReckoningTransaction tm)
        {
            //1.��T+0ʱ�����ʽ𶳽�
            if (CaptitalTradingRule != 0)
            {
                HK_CapitalAccountFreezeDal cafDal =
                    new HK_CapitalAccountFreezeDal();

                //��ǰ�ɽ�����
                var newCaft = new HK_CapitalAccountFreezeInfo();
                //ί�е���
                newCaft.EntrustNumber = tet2.EntrustNumber;
                //���� Ԥ�ɽ����
                newCaft.FreezeCapitalAmount = dealSum.CapitalSum;
                //���� Ԥ�ɽ�����
                newCaft.FreezeCost = 0;
                //����ʱ��
                newCaft.FreezeTime = DateTime.Now;
                //�ⶳʱ��
                newCaft.ThawTime = DateTime.Now.AddDays(CaptitalTradingRule);
                //��������
                newCaft.FreezeTypeLogo =
                    (int)Entity.Contants.Types.FreezeType.ReckoningFreeze;

                newCaft.CapitalAccountLogo = CapitalAccountId;

                cafDal.AddRecord(newCaft, tm.Database, tm.Transaction);
            }
        }

        /// <summary>
        /// ������-ί�д���
        /// </summary>
        /// <param name="dealSum"></param>
        /// <param name="tet2"></param>
        /// <param name="holdAveragePrice"></param>
        /// <returns></returns>
        private decimal HKSell_InstantReckon_EntrustProcess(HKDealSum dealSum, HK_TodayEntrustInfo tet2,
                                                            decimal holdAveragePrice)
        {
            //��ϵ�λ
            Types.UnitType utMatch = MCService.GetMatchUnitType(tet2.Code, Types.BreedClassTypeEnum.HKStock);
            //��ϵ�λ -> �Ƽ۵�λ ����
            decimal unitMultiple = MCService.GetTradeUnitScale(Types.BreedClassTypeEnum.HKStock, tet2.Code, utMatch);

            //�ɽ����ۣ��۸��Ǳ��۵�λ�������Ǵ�ϵ�λ��������ʱ��Ҫͳһ�����Ѵ�ϵ�λתΪ���۵�λ��
            decimal tradeAveragePrice =
                (tet2.TradeAveragePrice * tet2.TradeAmount * unitMultiple +
                 dealSum.CapitalSum) /
                (tet2.TradeAmount * unitMultiple + dealSum.AmountSum * unitMultiple);
            //��������
            tradeAveragePrice = Utils.Round(tradeAveragePrice);
            tet2.TradeAveragePrice = tradeAveragePrice;

            //�ɽ���
            tet2.TradeAmount =
                Convert.ToInt32(tet2.TradeAmount + dealSum.AmountSum);

            //��ʵ��ӯ��=��������-�ֲ־��ۣ�*������
            decimal hasDoneProfit = dealSum.CapitalSum -
                                    holdAveragePrice * dealSum.AmountSum * unitMultiple;
            //��������
            hasDoneProfit = Utils.Round(hasDoneProfit);

            string format =
                "XHSell_InstantReckon_EntrustProcess�۹���ʵ��ӯ��[EntrustNumber={0},Code={1}][��ʵ��ӯ��{2}=�ɽ����{3}-�ֲ־���{4}*�ɽ���{5}��({6})]";
            string desc = string.Format(format, tet2.EntrustNumber, tet2.Code, hasDoneProfit, dealSum.CapitalSum,
                                        holdAveragePrice, dealSum.AmountSum * unitMultiple, holdAveragePrice * dealSum.AmountSum * unitMultiple);
            LogHelper.WriteInfo(desc);

            tet2.HasDoneProfit += hasDoneProfit;


            SetOrderState(tet2);
            tet2.OrderMessage = "";

            return hasDoneProfit;
        }

        /// <summary>
        /// ������-�ʽ���ʽ𶳽��Ԥ����
        /// </summary>
        /// <param name="caf"></param>
        /// <param name="dealSum"></param>
        /// <returns></returns>
        private HK_CapitalAccount_DeltaInfo HKSell_InstantReckon_PreCapitalProcess(
            HK_CapitalAccountFreezeInfo caf, HKDealSum dealSum)
        {
            //caf.FreezeCapitalAmount -= 0; �����ᶳ���ʽ�
            caf.FreezeCost -= dealSum.CostSum;

            //T+0:�ʽ����ܶ������ȥ���γɽ��ķ��úͣ���Ϊ�ʽ𶳽����ȥ����ô��Ǯ��,�����ʽ���ϱ��γɽ����
            HK_CapitalAccount_DeltaInfo capitalDelta = new HK_CapitalAccount_DeltaInfo();
            capitalDelta.CapitalAccountLogo = CapitalAccountId;
            if (CaptitalTradingRule == 0)
            {
                capitalDelta.AvailableCapitalDelta = dealSum.CapitalSum;
                capitalDelta.FreezeCapitalTotalDelta = -dealSum.CostSum;
                //capMemory.AddDelta(dealSum.CapitalSum, -dealSum.CostSum, 0, 0);
            }
            else
            {
                //T+N:�����ʽ𶳽��¼���ʽ����ܶ�����Ҫ��+�ɽ���-�ɽ����á�
                capitalDelta.FreezeCapitalTotalDelta = dealSum.CapitalSum - dealSum.CostSum;
                //capMemory.AddDelta(0, dealSum.CapitalSum - dealSum.CostSum, 0, 0);
            }

            return capitalDelta;
        }

        /// <summary>
        /// ��-�ֱֲ�Ԥ����
        /// </summary>
        /// <param name="holdMemory"></param>
        /// <param name="isSuccess"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        private HKHoldMemoryTable HKSell_InstantReckon_PreHoldingProcess(HKHoldMemoryTable holdMemory,
                                                                         ref bool isSuccess,
                                                                         ref string strMessage)
        {
            try
            {
                //holdMemory = MemoryDataManager.HKHoldMemoryList.GetByHoldAccountAndCurrencyType(HoldingAccount, Code,
                //                                                                                CurrencyType);
                holdMemory = MemoryDataManager.HKHoldMemoryList.GetByAccountHoldLogoId(HoldingAccountId);
                //����ֲ�Ϊ�գ���ô�ȴ����ݿ���أ����ǲ����ڴ��û�м���
                if (holdMemory == null)
                {
                    HK_AccountHoldInfo hold = GetHoldFromDB(HoldingAccount, Code, CurrencyType);
                    //������ݿ��У���ôֱ�Ӽ��ص��ڴ����
                    if (hold != null)
                    {
                        isSuccess = MemoryDataManager.HKHoldMemoryList.AddAccountHoldTableToMemory(hold);
                    }
                    else
                    {
                        //������ݿ�Ҳû�У���ô�����޳ֲ�
                        return null;
                    }

                    holdMemory = MemoryDataManager.HKHoldMemoryList.GetByHoldAccountAndCurrencyType(HoldingAccount, Code,
                                                                                                    CurrencyType);

                    if (holdMemory == null)
                    {
                        strMessage = "GT-2725:[�۹ɳɽ�����]�޷���ȡ�ֲּ�¼.";
                        LogHelper.WriteDebug("HKSell_InstantReckon_PreHoldingProcess-" + strMessage);
                        isSuccess = false;
                        return null;
                    }
                }

                HoldingAccountId = holdMemory.Data.AccountHoldLogoID;
                isSuccess = true;
            }
            catch (Exception ex)
            {
                strMessage = "GT-2720:[�۹ɳɽ�����]�ֲִ����쳣:" + ex;
                LogHelper.WriteError("HKSell_InstantReckon_PreHoldingProcess-" + ex.Message, ex);
            }

            return holdMemory;
        }

        #endregion

        #region ���ܷ���

        /// <summary>
        /// ���ɳɽ���¼
        /// </summary>
        /// <param name="tet2"></param>
        /// <param name="tm"></param>
        /// <param name="idoExList"></param>
        /// <param name="tradeList2"></param>
        private void BuildTradeList(HK_TodayEntrustInfo tet2, ReckoningTransaction tm,
                                    List<HKDealBackEntityEx> idoExList, List<HK_TodayTradeInfo> tradeList2)
        {
            foreach (var idoEx in idoExList)
            {
                var ido = idoEx.DealBack;
                var costResult = idoEx.CostResult;

                var trade = HKCommonLogic.BuildHKDealRpt(tet2, ido,
                                                         costResult,
                                                         idoEx.DealCapital,
                                                         tm);
                if (trade != null)
                    tradeList2.Add(trade);
            }
        }

        private bool GetCapitalFreezeTable(out HK_CapitalAccountFreezeInfo caf)
        {
            caf = HKDataAccess.GetCapitalAccountFreeze(EntrustNumber,
                                                       Entity.Contants.Types.FreezeType.DelegateFreeze);
            if (caf == null)
            {
                //��ô��Ϊ����Ľ��ͷ��ö��Ѿ�Ϊ0������һ���յĶ����¼�Թ�����ʹ��
                try
                {
                    HKCommonLogic.InsertNullCapitalFreeze(CapitalAccountId, EntrustNumber);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex.Message, ex);
                    return false;
                }

                caf = HKDataAccess.GetCapitalAccountFreeze(EntrustNumber,
                                                           Entity.Contants.Types.FreezeType.DelegateFreeze);
            }
            return true;
        }

        /// <summary>
        /// ��ȡ�ɽ��ر��б����е�ID
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<string> GetIdList(List<HKDealBackEntity> list)
        {
            var ids = new List<string>();
            foreach (var ido in list)
            {
                ids.Add(ido.ID);
            }
            return ids;
        }

        /// <summary>
        /// ����۹ɳɽ��ر�������Ϣ
        /// </summary>
        /// <returns>�۹ɳɽ��ر����ܶ���</returns>
        private bool ComputeHKDealBacks(List<HKDealBackEntity> idos, out List<HKDealBackEntityEx> idoexs,
                                        out HKDealSum dealSum, ref string strMessage)
        {
            idoexs = new List<HKDealBackEntityEx>();
            dealSum = new HKDealSum();
            foreach (var ido in idos)
            {
                HKDealBackEntityEx ex = new HKDealBackEntityEx(ido);
                HKCostResult costResult = null;
                decimal dealCapital = 0;
                decimal dealCost = 0;
                bool isSuccess = ComputePreprocCapital(ido, Code, BuySellType, ref strMessage, ref dealCapital,
                                                       ref dealCost, out costResult);

                if (!isSuccess)
                    return false;

                ex.CostResult = costResult;
                ex.DealCapital = dealCapital;
                ex.DealCost = dealCost;

                dealSum.AmountSum += ido.DealAmount;
                dealSum.CapitalSum += dealCapital;
                dealSum.CostSum += dealCost;

                idoexs.Add(ex);
            }

            return true;
        }

        //����ί��״̬
        private void SetOrderState(HK_TodayEntrustInfo tet)
        {
            if (tet.OrderStatusID == (int)Entity.Contants.Types.OrderStateType.DOSCanceled)
                return;

            //����۸�ķϵ�
            if (tet.CancelAmount == -1)
            {
                tet.OrderStatusID = (int)Entity.Contants.Types.OrderStateType.DOSCanceled;
                return;
            }

            //ί����==�ɽ���+������ ȫ���ɽ�
            if (tet.EntrustAmount == (tet.TradeAmount + tet.CancelAmount))
            {
                hasDoneDeal = true;
                //��������ɹ���������0
                if (tet.CancelAmount > 0)
                {
                    //��������ɹ���������ί�е�������ô����ȫ��,�����ǲ���
                    if (tet.EntrustAmount == tet.CancelAmount)
                    {
                        tet.OrderStatusID = (int)Entity.Contants.Types.OrderStateType.DOSRemoved;
                    }
                    else
                    {
                        tet.OrderStatusID = (int)Entity.Contants.Types.OrderStateType.DOSPartRemoved;
                    }
                }
                //������û�з����������ѳ�   
                else
                {
                    tet.OrderStatusID = (int)Entity.Contants.Types.OrderStateType.DOSDealed;
                }
            }
            //ί����>�ɽ���+������ ���ֳɽ�
            else
            {
                //������ڳɽ��Ĺ����У��յ������������ô�ͱ��ֲ��ɴ���״̬��ֱ��������ѳɻ��ѳ�
                if (tet.OrderStatusID == (int)Entity.Contants.Types.OrderStateType.DOSPartDealRemoveSoon)
                    return;

                //���򲻹���û�г�������ͳһ����Ϊ���ɣ���Ϊ��Ͽ����ȷ��س��������Դ˴�״̬�������ã�
                if (tet.TradeAmount > 0)
                    tet.OrderStatusID = (int)Entity.Contants.Types.OrderStateType.DOSPartDealed;
            }
        }

        private List<string> GetIDList2(List<HKDealBackEntityEx> list)
        {
            List<string> ids = new List<string>();
            foreach (var entityEx in list)
            {
                ids.Add(entityEx.DealBack.ID);
            }

            return ids;
        }

        private HK_AccountHoldInfo GetCloneHold(HK_AccountHoldInfo info)
        {
            HK_AccountHoldInfo newInfo = new HK_AccountHoldInfo();
            newInfo.AccountHoldLogoID = info.AccountHoldLogoID;
            newInfo.AvailableAmount = info.AvailableAmount;
            newInfo.BreakevenPrice = info.BreakevenPrice;
            newInfo.Code = info.Code;
            newInfo.CostPrice = info.CostPrice;
            newInfo.CurrencyTypeID = info.CurrencyTypeID;
            newInfo.FreezeAmount = info.FreezeAmount;
            newInfo.HoldAveragePrice = info.HoldAveragePrice;
            newInfo.UserAccountDistributeLogo = info.UserAccountDistributeLogo;

            return newInfo;
        }

        #endregion
    }

    /// <summary>
    /// �۹ɳɽ��ر���չ��
    /// </summary>
    public class HKDealBackEntityEx : ReckonDealBackEx<HKDealBackEntity, HKCostResult>
    {
        public HKDealBackEntityEx(HKDealBackEntity dealBack)
            : base(dealBack)
        {
        }
    }
}