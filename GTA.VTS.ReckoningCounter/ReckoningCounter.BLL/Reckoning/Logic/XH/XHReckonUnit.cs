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
using ReckoningCounter.DAL.MatchCenterOrderDealRpt;
using ReckoningCounter.MemoryData;
using ReckoningCounter.MemoryData.XH.Capital;
using ReckoningCounter.MemoryData.XH.Hold;
using ReckoningCounter.Model;

#endregion

namespace ReckoningCounter.BLL.Reckoning.Logic.XH
{
    /// <summary>
    /// �ֻ����㴦��Ԫ���������2501-2590
    /// Create By:����
    /// Create date:2009-01-26
    /// </summary>
    public class XHReckonUnit : ReckonUnitBase<XH_TodayEntrustTableInfo, StockDealBackEntity, XH_TodayTradeTableInfo>
    {
        /// <summary>
        /// �Ƿ��Ѿ���ɵ�ǰί�����㣨�ɽ�+������
        /// </summary>
        protected bool hasDoneDeal;

        #region Overrides of ReckonUnitBase<XhTodayEntrustTable,StockDealBackEntity>

        /// <summary>
        /// �ڲ����ճɽ��ر�������
        /// </summary>
        /// <param name="message">�ɽ��ر����߳����ر�</param>
        protected override bool InternalInsertMessage(object message)
        {
            if (hasDoneDeal)
            {
                LogHelper.WriteDebug("XHReckonUnit.InternalInsertMessage:ί���Ѿ���ɣ��ر�����" + message);
                return false;
            }

            string id = string.Empty;
            string orderID = string.Empty;
            string desc = string.Empty;
            decimal dealAmount = 0;
            if (message is StockDealBackEntity)
            {
                StockDealBackEntity ido = (StockDealBackEntity) message;
                id = ido.Id;
                orderID = ido.OrderNo;
                desc = ido.DescInfo();
                dealAmount = ido.DealAmount;
            }
            else if (message is CancelOrderEntity)
            {
                CancelOrderEntity coe = (CancelOrderEntity) message;
                id = coe.Id;
                orderID = coe.OrderNo;
                desc = coe.DescInfo();
                dealAmount = coe.OrderVolume;
            }

            if (id == string.Empty)
            {
                LogHelper.WriteDebug("XHReckonUnit.InternalInsertMessage: ��ID");
                return false;
            }

            if (HasAddId(id))
            {
                LogHelper.WriteDebug("XHReckonUnit.InternalInsertMessage: �����ID" + id);
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
                LogHelper.WriteDebug("XHReckonUnit.InternalInsertMessage: �Ѵ���ID" + id);
                return false;
            }

            AddID(id);

            string entrusNum = "[EntrustNumber=" + EntrustNumber + "]";
            LogHelper.WriteDebug("XHReckonUnit.InternalInsertMessage" + desc + entrusNum);

            //��ӵ��ڲ��Ļ����б�
            if (message is StockDealBackEntity)
            {
                StockDealBackEntity ido = (StockDealBackEntity) message;
                dealBackList.Add(ido);
            }
            else if (message is CancelOrderEntity)
            {
                CancelOrderEntity coe = (CancelOrderEntity) message;
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
            var trades = XHDataAccess.GetTodayTradeListByEntrustNumber(EntrustNumber);
            if (trades == null)
                return;

            foreach (XH_TodayTradeTableInfo trade in trades)
            {
                AddReckonedID(trade.TradeNumber.Trim());

                if (trade.TradeTypeId == (int) Entity.Contants.Types.DealRptType.DRTDealed)
                {
                    string format = "[��TradeAmount{0}=��TradeAmount{1}+��ǰ�ɽ���{2}]";
                    string msg = string.Format(format, TradeAmount + trade.TradeAmount, TradeAmount,
                                               trade.TradeAmount);
                    LogHelper.WriteDebug("XHReckonUnit.LoadReckonedIDList" + msg);
                    TradeAmount += trade.TradeAmount;
                }

                if (trade.TradeTypeId == (int) Entity.Contants.Types.DealRptType.DRTCanceled)
                {
                    string format = "[��CancelAmount{0}=��CancelAmount{1}+��ǰ�ɽ���{2}]";
                    string msg = string.Format(format, CancelAmount + trade.TradeAmount, CancelAmount,
                                               trade.TradeAmount);
                    LogHelper.WriteDebug("XHReckonUnit.LoadReckonedIDList" + msg);
                    CancelAmount += trade.TradeAmount;
                }
            }

            HasLoadReckonedID = true;
        }

        protected override CounterCache GetCounterCache()
        {
            return XHCounterCache.Instance;
        }

        protected override void GetCurrencyType()
        {
            var currOjb = MCService.SpotTradeRules.GetCurrencyTypeByCommodityCode(Code);
            if (currOjb == null)
            {
                return;
            }

            CurrencyType = currOjb.CurrencyTypeID;
        }

        protected override void GetAccountID()
        {
            CapitalAccountId = MemoryDataManager.XHCapitalMemoryList.GetCapitalAccountLogo(CapitalAccount, CurrencyType);


            HoldingAccountId = MemoryDataManager.XHHoldMemoryList.GetAccountHoldLogoId(HoldingAccount, Code,
                                                                                       CurrencyType);
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

            if (list.Count > 1)
            {
                LogHelper.WriteDebug("XHReckonUnit.ProcessCancelCommit: �յ���������ر���ֻ�����һ����������������");
            }

            coe = list[0];

            //����Ǵ���۸�ĳ���������ִ��
            if (coe.OrderVolume == -1)
                return true;

            //���TradeAmount+CancelAmount+coe.Amount!=EntrustAmount,
            //��ô��Ϊ��Ȼ�յ��˳����ر������ǻ��в��ֳɽ��ر���û��������ô
            //�Ȳ����г������������еĳɽ��ر����������ˣ�����ٽ��г�������
            decimal allAmount = TradeAmount + CancelAmount + coe.OrderVolume;
            if (allAmount < EntrustAmount)
            {
                string format2 =
                    "XHReckonUnit.ProcessCancelCommit���в��ֳɽ��ر�δ�յ���δ����[EntrustAmount={0},TradeAmount={1},CancelAmount={2},CancelBackAmount={3}]";
                string info = string.Format(format2, EntrustAmount, TradeAmount, CancelAmount, coe.OrderVolume);
                //LogHelper.WriteInfo(info);

                //���гɽ��ر�û�л������������볷���б�ȴ��´γ�������
                cancelBackList.Add(coe);

                return false;
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
            XH_TodayEntrustTableInfo tet;
            List<XH_TodayTradeTableInfo> trades;
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

            if (!isSuccess)
            {
                //�������û�гɹ�����ô�ӱ���Ļر��б���ɾ�����´�CrashManger�����ٴη��ʹ˻ر�����
                LogHelper.WriteInfo(
                    "#####################XHReckonUnit.ProcessCancelCommit��������ʧ�ܣ�CancelOrderEntity[ID=" + coe.Id +
                    "], Message=" + strMessage);
                RemoveID(coe.Id);
            }

            if (!string.IsNullOrEmpty(strMessage))
                XHDataAccess.UpdateEntrustOrderMessage(EntrustNumber, strMessage);

            CrashManager.GetInstance().RemoveReckoningID(coe.Id);

            if (isSuccess)
            {
                ReckonEndObject<XH_TodayEntrustTableInfo, XH_TodayTradeTableInfo> cancelEndObject =
                    new ReckonEndObject<XH_TodayEntrustTableInfo, XH_TodayTradeTableInfo>();
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
                    RescueManager.Instance.Record_XH_LastCheckFreezeMoney(EntrustNumber, CapitalAccountId);
                }
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
                                              out XH_TodayEntrustTableInfo tet, out List<XH_TodayTradeTableInfo> trades,
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
            LogHelper.WriteDebug(
                "------xxxxxx------��ʼ�����ֻ��ⲿ����(��Ϸ��س����ر�[���������м۸������])XHOrderLogicFlow.InstantReckon_CancelOrder2" +
                rde.DescInfo() + entrustNumString);

            //�ֻ�����ί�лر�
            //tet = XHDataAccess.GetTodayEntrustTable(EntrustNumber);
            //��ΪĳЩ�쳣��������ʧ�ܣ����ǵ���ί�б�ת����ʷί�У��Ժ���������Ҳ���ί�У��������ڸ�Ϊ
            //�ڵ����Ҳ�������ʷ����
            tet = XHDataAccess.GetAllEntrustTable(EntrustNumber);
            if (tet == null)
            {
                strMessage = "GT-2501:[�ֻ���������]�޷���ȡί�ж���ί�е���=" + EntrustNumber;
                LogHelper.WriteDebug(strMessage);
                return false;
            }

            //ȡ�����ӦƷ�ֵĽ��ױ���
            if (CurrencyType == -1)
                CurrencyType = MCService.SpotTradeRules.GetCurrencyTypeByCommodityCode(tet.SpotCode).CurrencyTypeID;

            var hasDealedAmount = TradeAmount + CancelAmount;

            if (hasDealedAmount == EntrustAmount)
            {
                string msg = "�����ر����󣺵�ǰί���Ѿ�������ϣ������ر���Ч��" + entrustNumString;
                LogHelper.WriteDebug(msg);

                //��������id���뵽������id�б���
                AddReckonedID(rde.Id);

                //������ݿ��б���ĳɽ��ر�
                CrashManager.GetInstance().DeleteEntity(rde.Id);

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
                    AddReckonedID(rde.Id);

                    //������ݿ��б���ĳɽ��ر�
                    CrashManager.GetInstance().DeleteEntity(rde.Id);

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

            var ca = XHDataAccess.GetCapitalAccountFreeze(EntrustNumber, Entity.Contants.Types.FreezeType.DelegateFreeze);
            if (ca == null)
            {
                string msg = "[�ֻ���������]�ʽ𶳽��¼������." + entrustNumString;
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

            XH_CapitalAccountTable_DeltaInfo capitalDelta = new XH_CapitalAccountTable_DeltaInfo();
            capitalDelta.CapitalAccountLogo = CapitalAccountId;
            decimal delta = preFreezeCapital + preFreezeCost;

            XHCapitalMemoryTable capMemory = null;
            if (delta != 0)
            {
                capitalDelta.AvailableCapitalDelta = delta;
                capitalDelta.FreezeCapitalTotalDelta = -delta;

                capMemory = MemoryDataManager.XHCapitalMemoryList.GetByCapitalAccountLogo(CapitalAccountId);
                if (capMemory == null)
                {
                    strMessage = "GT-2502:[�ֻ���������]�ʽ��ʻ�������:" + CapitalAccount;
                    LogHelper.WriteDebug(strMessage);
                    return false;
                }
            }

            #endregion

            decimal preFreezeHoldAmount = 0;
            XH_AccountHoldTableInfo_Delta holdDelta = new XH_AccountHoldTableInfo_Delta();
            XHHoldMemoryTable holdMemory = null;
            if (tet.BuySellTypeId == (int) Types.TransactionDirection.Selling)
            {
                #region 3.�ֲֶ��ᴦ��(���벻���㣩,��ȡ�ֲֶ����¼�еĶ������Ͷ���id

                var hold = XHDataAccess.GetHoldAccountFreeze(EntrustNumber,
                                                             Entity.Contants.Types.FreezeType.DelegateFreeze);
                if (hold == null)
                {
                    string msg = "[�ֻ���������]�ֲֶ��᲻����";
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
                        "�ֻ���������-�ر������������ݿⶳ���������[EntrustNumber={0},CancelOrderVolume={1},FreezeHoldAmount={2}]";
                    string desc = string.Format(format, EntrustNumber, cancelOrderVolume, preFreezeHoldAmount);
                    preFreezeHoldAmount = cancelOrderVolume;
                    LogHelper.WriteDebug(desc);
                }

                #endregion

                holdMemory = MemoryDataManager.XHHoldMemoryList.GetByAccountHoldLogoId(HoldingAccountId);
                if (holdMemory == null)
                {
                    holdMemory = XHCommonLogic.GetHoldMemoryTable(HoldingAccount, Code, CurrencyType);
                }

                if (holdMemory == null)
                {
                    strMessage = "GT-2503:[�ֻ���������]�ֲ��˻�������:" + HoldingAccount;
                    return false;
                }

                var holdData = holdMemory.Data;
                holdDelta.Data = holdData;
                holdDelta.AccountHoldLogoId = holdData.AccountHoldLogoId;
                holdDelta.AvailableAmountDelta += preFreezeHoldAmount;
                holdDelta.FreezeAmountDelta -= preFreezeHoldAmount;

                #endregion
            }

            tet.CancelAmount = (int) rde.OrderVolume;
            SetOrderState(tet);
            if (isErrorPriceCancelRpt)
            {
                tet.CancelAmount = (int) EntrustAmount;
                tet.OrderMessage = rde.Message;
            }

            //ֻҪ�д�����Ϣ����д��ί��
            if (!string.IsNullOrEmpty(rde.Message))
            {
                tet.OrderMessage = rde.Message;
            }

            XH_TodayTradeTableInfo trade = null;

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

                        XHDataAccess.ClearCapitalFreeze(capitalFreezeLogoId, db, transaction);

                        if (tet.BuySellTypeId == (int) Types.TransactionDirection.Selling)
                        {
                            XHDataAccess.ClearHoldFreeze(holdFreezeLogoId, db, transaction);
                        }

                        XHDataAccess.UpdateEntrustTable(tet2, db, transaction);

                        //����ҲҪ���뵱�ճɽ�����
                        ReckoningTransaction rt = new ReckoningTransaction();
                        rt.Database = db;
                        rt.Transaction = transaction;

                        bool isInternalCancelOrder = false;
                        if (rde is CancelOrderEntityEx)
                        {
                            var rde2 = (CancelOrderEntityEx) rde;
                            isInternalCancelOrder = rde2.IsInternalCancelOrder;
                        }

                        XHCommonLogic.BuildXHCancelRpt(tet2, rde, rt, out trade,
                                                       isInternalCancelOrder);

                        //������ɺ�������ݿ��б���ĳɽ��ر�
                        CrashManager.GetInstance().DeleteEntity(rde.Id, rt);

                        //�ʽ����
                        if (delta != 0)
                        {
                            capMemory.AddDeltaToDB(capitalDelta, db, transaction);
                        }

                        //�ֲֲ���
                        if (tet.BuySellTypeId == (int) Types.TransactionDirection.Selling)
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
                        strMessage = "GT-2504:[�ֻ���������]�ύ�����ݿ�ʧ��";

                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.Message, ex);
                strMessage = "GT-2504:[�ֻ���������]�ύ�����ݿ�ʧ��";
            }

            if (isSuccess)
            {
                if (delta != 0)
                {
                    capMemory.AddDeltaToMemory(capitalDelta);
                }

                if (tet.BuySellTypeId == (int) Types.TransactionDirection.Selling)
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
            AddReckonedID(rde.Id);

            if (rde.OrderVolume == -1)
            {
                CancelAmount = EntrustAmount;
                strMessage = rde.Message;
            }
            else
                CancelAmount += rde.OrderVolume;

            trades = new List<XH_TodayTradeTableInfo>();
            if (trade != null)
                trades.Add(trade);


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

            string format = "�ֻ��ɽ�����[EntrustNumber={0},DealBackCount={1}";
            string info = string.Format(format, EntrustNumber, list.Count);
            LogHelper.WriteInfo(info);

            string strMessage = "";
            XH_TodayEntrustTableInfo tet;
            List<XH_TodayTradeTableInfo> tradeList;

            bool isSuccess = false;

            List<string> ids = GetIdList(list);

            CrashManager.GetInstance().AddReckoningIDList(ids);

            isSuccess = InstantReckon_Deal(list, ref strMessage, out tet, out tradeList);

            if (!string.IsNullOrEmpty(strMessage))
                XHDataAccess.UpdateEntrustOrderMessage(EntrustNumber, strMessage);

            //ʧ�ܣ���ô���й��ϻָ�����
            if (!isSuccess)
            {
                //�������û�гɹ�����ô�ӱ���Ļر��б���ɾ�����´�CrashManger�����ٴη��ʹ˻ر�����
                LogHelper.WriteInfo("#####################XHReckonUnit.ProcessDealCommit�ɽ�����ʧ��:" + strMessage);
                //"��StockDealBackEntity[ID=" + ido.Id +"]");
                RemoveIDList(ids);
            }
            CrashManager.GetInstance().RemoveReckoningIDList(ids);

            if (isSuccess)
            {
                ReckonEndObject<XH_TodayEntrustTableInfo, XH_TodayTradeTableInfo> reckonEndObject =
                    new ReckonEndObject<XH_TodayEntrustTableInfo, XH_TodayTradeTableInfo>();
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
                    RescueManager.Instance.Record_XH_LastCheckFreezeMoney(EntrustNumber, CapitalAccountId);
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

            var caf = XHDataAccess.GetCapitalAccountFreeze(entrustNumber,
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

            XH_CapitalAccountTable_DeltaInfo delta = new XH_CapitalAccountTable_DeltaInfo();
            delta.CapitalAccountLogo = capitalAccountId;
            delta.AvailableCapitalDelta = needAddMoney;
            delta.FreezeCapitalTotalDelta = -needAddMoney;

            var capMemory = MemoryDataManager.XHCapitalMemoryList.GetByCapitalAccountLogo(capitalAccountId);
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
                        XHDataAccess.ClearCapitalFreeze(caf.CapitalFreezeLogoId, database, transaction);

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

            var caf = XHDataAccess.GetCapitalAccountFreeze(EntrustNumber,
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

            var capMemory = MemoryDataManager.XHCapitalMemoryList.GetByCapitalAccountLogo(CapitalAccountId);
            if (capMemory == null)
            {
                return;
            }

            XH_CapitalAccountTable_DeltaInfo delta = new XH_CapitalAccountTable_DeltaInfo();
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


        private bool ComputePreprocCapital(StockDealBackEntity ido, string code, Types.TransactionDirection buySellType,
                                           ref string strMessage, ref decimal dealCapital,
                                           ref decimal dealCost, out XHCostResult xhcr)
        {
            bool result = false;
            xhcr = null;

            try
            {
                //��ϵ�λ
                Types.UnitType utMatch = MCService.GetMatchUnitType(code);
                //��ϵ�λ -> �Ƽ۵�λ ����
                decimal unitMultiple = MCService.GetTradeUnitScale(code, utMatch);
                //��ǰ�ɽ����ö���
                xhcr = MCService.ComputeXHCost(code, Decimal.ToSingle(ido.DealPrice),
                                               decimal.ToInt32(ido.DealAmount), utMatch, buySellType);
                //�ɽ����
                dealCapital = ido.DealAmount*unitMultiple*ido.DealPrice;
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
                strMessage = "GT-2505:[�ֻ��ɽ�����]��ί�гɽ�����ɽ������ü����쳣.";
                LogHelper.WriteError(ex.Message, ex);
            }
            return result;
        }

        public bool InstantReckon_Deal(List<StockDealBackEntity> idoList, ref string strMessage,
                                       out XH_TodayEntrustTableInfo tet, out List<XH_TodayTradeTableInfo> tradeList)
        {
            #region ==��ʼ������ ==

            //bool result = false;
            strMessage = string.Empty;
            tet = null;
            tradeList = null;
            List<string> ids = GetIdList(idoList);

            string entrustNum = "[EntrustNumber=" + EntrustNumber + "]";

            string format = "��ʱ����XHRecikonUnit.InstantReckon_Deal[�ʽ��ʻ�={0},�ֲ��ʻ�={1},��̨ί�е���={2}]";
            string msg = string.Format(format, CapitalAccount, HoldingAccount, EntrustNumber);
            LogHelper.WriteDebug(msg);

            var hasDealedAmount = TradeAmount + CancelAmount;
            if (hasDealedAmount == EntrustAmount)
            {
                msg = "��ʱ����XHRecikonUnit.InstantReckon_Deal-�ɽ��ر����󣺵�ǰί���Ѿ�������ϣ��ɽ��ر���Ч��EntrustNumber=" +
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

            List<StockDealBackEntityEx> idoexList;
            XHDealSum dealSum;
            bool isSuccess = ComputeXHDealBacks(idoList, out idoexList, out dealSum, ref strMessage);
            if (!isSuccess)
            {
                strMessage = "GT-2523:[�ֻ��ɽ�����]�ɽ��ر�����ʧ��.";
                return false;
            }

            if ((hasDealedAmount + dealSum.AmountSum) > EntrustAmount)
            {
                msg = "��ʱ����XHRecikonUnit.InstantReckon_Deal-�ɽ��ر����󣺳ɽ��ر��ɽ�����{0}+�����������{1}>��ǰί����{2}��EntrustNumber=" +
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
                CurrencyType =
                    MCService.SpotTradeRules.GetCurrencyTypeByCommodityCode(Code).CurrencyTypeID;


            //�����ƶȻ��ȡ(�˷���û���õ����ݿ�)
            if (CaptitalTradingRule == -1 || HoldingTradingRule == -1)
            {
                int captitalTradingRule;
                int holdingTradingRule;
                if (!MCService.SpotTradeRules.GetDeliveryInstitution(Code, out captitalTradingRule,
                                                                     out holdingTradingRule, ref strMessage))
                {
                    strMessage = "GT-2506:[�ֻ��ɽ�����]�޷���ȡ�����ƶ�.";

                    XHDataAccess.UpdateEntrustOrderMessage(entrustNum, strMessage);
                    return false;
                }

                CaptitalTradingRule = captitalTradingRule;
                HoldingTradingRule = holdingTradingRule;
            }

            //�ֻ�����ί�лر�
            tet = XHDataAccess.GetTodayEntrustTable(EntrustNumber);
            if (tet == null)
            {
                strMessage = "GT-2507:[�ֻ��ɽ�����]�޷���ȡί�ж���ί�е���=" + EntrustNumber;
                LogHelper.WriteDebug(strMessage);
                return false;
            }

            #endregion

            if (BuySellType == Types.TransactionDirection.Buying)
            {
                return XHBuy_InstantReckon_Deal(idoexList, dealSum, ref strMessage, out tet, out tradeList);
            }

            return XHSell_InstantReckon_Deal(idoexList, dealSum, ref strMessage, out tet, out tradeList);
        }

        #endregion

        #region �ֻ������㴦��

        /// <summary>
        /// �ֻ��������ύ���
        /// </summary>
        /// <param name="idoExList">��չ�ɽ��ر��б�</param>
        /// <param name="dealSum">�ֻ��ɽ��ر�����</param>
        /// <param name="strMessage">������Ϣ</param>
        /// <param name="tet">ί��</param>
        /// <param name="tradeList">�ɽ��б�</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        private bool XHBuy_InstantReckon_Deal(List<StockDealBackEntityEx> idoExList, XHDealSum dealSum,
                                              ref string strMessage, out XH_TodayEntrustTableInfo tet,
                                              out List<XH_TodayTradeTableInfo> tradeList)
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
            tradeList = new List<XH_TodayTradeTableInfo>();

            //�ܳɽ�������capital��cost��
            decimal dealMoneySum = dealSum.CapitalSum + dealSum.CostSum;

            #region 1.�ʽ��Ԥ����

            //�ȼ���ʽ𶳽���еĶ�����ͷ����Ƿ񹻿�

            XH_CapitalAccountFreezeTableInfo caf;
            if (!GetCapitalFreezeTable(out caf))
            {
                strMessage = "GT-2524:[�ֻ��ɽ�����]�޷���ȡ�ʽ𶳽��¼.";

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

            var capMemory = MemoryDataManager.XHCapitalMemoryList.GetByCapitalAccountLogo(CapitalAccountId);
            if (capMemory == null)
            {
                strMessage = "GT-2508:[�ֻ��ɽ�����]�ʽ��ʻ�������:" + CapitalAccount;

                return false;
            }

            XH_CapitalAccountTable_DeltaInfo capitalDelta = XHBuy_InstantReckon_PreCapitalProcess(dealSum, caf,
                                                                                                  dealMoneySum);
            capitalDelta.CapitalAccountLogo = capMemory.Data.CapitalAccountLogo;
            //�ʽ���������ύ���ȴ���������ɹ�������ֲ�һ���ύ
            //capMemory.AddDelta(capitalDelta);

            #endregion

            #region 2.�ֱֲ�Ԥ����

            bool isSuccess = false;

            XHHoldMemoryTable holdMemory = null;
            holdMemory = XHBuy_InstantReckon_PreHoldingProcess(ref isSuccess, ref strMessage);

            if (!isSuccess)
            {
                //�ٻ�ȡһ��
                holdMemory = XHBuy_InstantReckon_PreHoldingProcess(ref isSuccess, ref strMessage);
            }

            if (!isSuccess)
            {
                strMessage = "GT-2590:[�ֻ��ɽ�����]�޷���ȡ�ֲּ�¼.";
                LogHelper.WriteDebug("XHBuy_InstantReckon_Deal_!isSuccess " + strMessage);
                return false;
            }

            if (holdMemory == null)
            {
                strMessage = "GT-2590:[�ֻ��ɽ�����]�޷���ȡ�ֲּ�¼.";
                LogHelper.WriteDebug("XHBuy_InstantReckon_Deal_holdMemory == null " + strMessage);

                return false;
            }

            #endregion

            //�����ʽ𶳽�ͳֲֶ���,�Լ�ί����Ϣ
            XH_AcccountHoldFreezeTableInfo holdFreeze = null;
            bool isFirstHold = false;
            if (HoldingTradingRule != 0)
            {
                holdFreeze = XHBuy_InstantReckon_HoldingFreezeProcess(dealSum, ref isFirstHold);
            }

            tet = XHDataAccess.GetTodayEntrustTable(EntrustNumber);
            var tet2 = tet;

            XHBuy_InstantReckon_EntrustProcess(dealSum, tet2);

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
                        XH_CapitalAccountFreezeTableDal cafDal =
                            new XH_CapitalAccountFreezeTableDal();
                        cafDal.Update(caf, tm);

                        //2.���³ֲֶ���
                        if (HoldingTradingRule != 0)
                        {
                            XH_AcccountHoldFreezeTableDal holdDal =
                                new XH_AcccountHoldFreezeTableDal();
                            if (isFirstHold)
                                holdDal.Add(holdFreeze, tm.Database, tm.Transaction);
                            else
                                holdDal.Update(holdFreeze, tm.Database, tm.Transaction);
                        }

                        //3.����ί�б�
                        XHDataAccess.UpdateEntrustTable(tet2, tm.Database,
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
                        strMessage = "GT-2509:[�ֻ��ɽ�����]���ݿ��ύʧ��";

                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.Message, ex);
                strMessage = "GT-2509:[�ֻ��ɽ�����]���ݿ��ύʧ��";
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

            isSuccess = XHBuy_InstantReckon_HoldingProcess(holdMemory, dealSum, HoldingTradingRule);

            if (!isSuccess)
            {
                //���ɹ�����ִ��3��
                for (int i = 0; i < 3; i++)
                {
                    //���ɹ�����ִ��һ��
                    isSuccess = XHBuy_InstantReckon_HoldingProcess(holdMemory, dealSum, HoldingTradingRule);

                    if (isSuccess)
                        break;
                }
            }

            if (!isSuccess)
            {
                //һ��Ҫִ�У���¼�������´�ִ��
                RescueManager.Instance.Record_XHBuy_InstantReckon_HoldingProcess(HoldingAccountId, dealSum,
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
        private void XHBuy_InstantReckon_EntrustProcess(XHDealSum dealSum, XH_TodayEntrustTableInfo tet2)
        {
            //��ϵ�λ
            Types.UnitType utMatch = MCService.GetMatchUnitType(tet2.SpotCode);
            //��ϵ�λ -> �Ƽ۵�λ ����
            decimal unitMultiple = MCService.GetTradeUnitScale(tet2.SpotCode, utMatch);

            //�ɽ����ۣ��۸��Ǳ��۵�λ�������Ǵ�ϵ�λ��������ʱ��Ҫͳһ�����Ѵ�ϵ�λתΪ���۵�λ��
            decimal tradeAveragePrice =
                (tet2.TradeAveragePrice*tet2.TradeAmount*unitMultiple +
                 dealSum.CapitalSum)/(tet2.TradeAmount*unitMultiple + dealSum.AmountSum*unitMultiple);

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
        private XH_AcccountHoldFreezeTableInfo XHBuy_InstantReckon_HoldingFreezeProcess(XHDealSum dealSum,
                                                                                        ref bool isFirstHold)
        {
            XH_AcccountHoldFreezeTableInfo holdFreeze;
            holdFreeze = XHDataAccess.GetHoldAccountFreeze(EntrustNumber,
                                                           Entity.Contants.Types.FreezeType.DelegateFreeze);
            if (holdFreeze == null)
            {
                holdFreeze = new XH_AcccountHoldFreezeTableInfo();
                //��������
                holdFreeze.PrepareFreezeAmount = Convert.ToInt32(dealSum.AmountSum);
                //����ʱ��
                holdFreeze.FreezeTime = DateTime.Now;
                //�ⶳʱ��
                holdFreeze.ThawTime = DateTime.Now.AddDays(HoldingTradingRule);
                //ί�е���
                holdFreeze.EntrustNumber = EntrustNumber;
                //��������
                holdFreeze.FreezeTypeLogo = (int) Entity.Contants.Types.FreezeType.ReckoningFreeze;
                //�ֻ��ʻ��ֱֲ�ʶ
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
        private XH_CapitalAccountTable_DeltaInfo XHBuy_InstantReckon_PreCapitalProcess(XHDealSum dealSum,
                                                                                       XH_CapitalAccountFreezeTableInfo
                                                                                           caf, decimal dealMoneySum)
        {
            //ע�⣺Ϊ�˼򻯴����������ڼ䲻�ٽ��ж����ʽ�Ĳ������㼰�����ٲ��ļ��㣬ͳһ�ڱ������������
            //���ʽ𶳽����м��У�飬�����������ڲ���ֱ�����ʽ𶳽���ϼ�ȥ�ɽ����ͳɽ�����
            caf.FreezeCapitalAmount -= dealSum.CapitalSum;
            caf.FreezeCost -= dealSum.CostSum;


            //******************�κεط�ִ��ʧ�ܣ�����Ҫ�ع��Ĳ���1********************
            //�ʽ����ܶ������ȥ���γɽ��Ľ��ͷ��úͣ���Ϊ�ʽ𶳽����ȥ����ô��Ǯ��
            XH_CapitalAccountTable_DeltaInfo capitalDelta = new XH_CapitalAccountTable_DeltaInfo();
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
        private XHHoldMemoryTable XHBuy_InstantReckon_PreHoldingProcess(ref bool isSuccess, ref string strMessage)
        {
            XHHoldMemoryTable holdMemory = null;
            try
            {
                holdMemory = MemoryDataManager.XHHoldMemoryList.GetByHoldAccountAndCurrencyType(HoldingAccount, Code,
                                                                                                CurrencyType);
                //holdMemory = MemoryDataManager.XHHoldMemoryList.GetByAccountHoldLogoId(HoldingAccountId);
                //����ֲ�Ϊ�գ���ô�ȴ����ݿ���أ����ǲ����ڴ��û�м���
                if (holdMemory == null)
                {
                    XH_AccountHoldTableInfo hold = GetHoldFromDB(HoldingAccount, Code, CurrencyType);
                    //������ݿ��У���ôֱ�Ӽ��ص��ڴ����
                    if (hold != null)
                    {
                        string format =
                            "XHBuy_InstantReckon_PreHoldingProcess���ݿ���ڳֲ֣�ֱ�Ӽ��ص��ڴ����[Code={0},HoldAccount={1}]";
                        string desc = string.Format(format, Code, HoldingAccount);
                        LogHelper.WriteDebug(desc);

                        isSuccess = MemoryDataManager.XHHoldMemoryList.AddXHAccountHoldTableToMemory(hold);

                        if (!isSuccess)
                        {
                            isSuccess = MemoryDataManager.XHHoldMemoryList.AddXHAccountHoldTableToMemory(hold);
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

                        hold = new XH_AccountHoldTableInfo();

                        hold.UserAccountDistributeLogo = HoldingAccount;
                        hold.Code = Code;
                        hold.AvailableAmount = 0;
                        hold.FreezeAmount = 0;
                        hold.CurrencyTypeId = CurrencyType;

                        isSuccess = MemoryDataManager.XHHoldMemoryList.AddXHAccountHoldTable(hold);

                        if (!isSuccess)
                        {
                            isSuccess = MemoryDataManager.XHHoldMemoryList.AddXHAccountHoldTable(hold);
                        }

                        if (!isSuccess)
                        {
                            desc += "-�½�ʧ�ܣ��Ѵ���";
                            LogHelper.WriteDebug(desc);
                        }
                    }

                    holdMemory = MemoryDataManager.XHHoldMemoryList.GetByHoldAccountAndCurrencyType(HoldingAccount, Code,
                                                                                                    CurrencyType);

                    if (holdMemory == null)
                    {
                        int holdId = MemoryDataManager.XHHoldMemoryList.GetAccountHoldLogoId(HoldingAccount, Code,
                                                                                             CurrencyType);
                        if (holdId != -1)
                            holdMemory = MemoryDataManager.XHHoldMemoryList.GetByAccountHoldLogoId(holdId);
                    }

                    if (holdMemory == null)
                    {
                        hold = GetHoldFromDB(HoldingAccount, Code, CurrencyType);
                        isSuccess = MemoryDataManager.XHHoldMemoryList.AddXHAccountHoldTableToMemory(hold);
                    }

                    holdMemory = MemoryDataManager.XHHoldMemoryList.GetByHoldAccountAndCurrencyType(HoldingAccount, Code,
                                                                                                    CurrencyType);


                    if (holdMemory == null)
                    {
                        strMessage = "GT-2525:[�ֻ��ɽ�����]�޷���ȡ�ֲּ�¼.";
                        LogHelper.WriteDebug("XHBuy_InstantReckon_PreHoldingProcess" + strMessage);
                        isSuccess = false;
                        return null;
                    }
                }

                HoldingAccountId = holdMemory.Data.AccountHoldLogoId;
                isSuccess = true;
            }
            catch (Exception ex)
            {
                strMessage = "GT-2520:[�ֻ��ɽ�����]�ֲִ����쳣:" + ex;
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
        private XH_AccountHoldTableInfo GetHoldFromDB(string holdingAccount, string code, int currencyType)
        {
            XH_AccountHoldTableInfo hold = null;

            XH_AccountHoldTableDal dal = new XH_AccountHoldTableDal();
            hold = dal.GetXhAccountHoldTable(holdingAccount, code, currencyType);

            return hold;
        }


        /// <summary>
        /// ������-�ֲִ���
        /// </summary>
        /// <param name="holdMemory"></param>
        /// <param name="dealSum"></param>
        /// <param name="holdingTradingRule"></param>
        private static bool XHBuy_InstantReckon_HoldingProcess(XHHoldMemoryTable holdMemory, XHDealSum dealSum,
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
            Types.UnitType utMatch = MCService.GetMatchUnitType(code);
            //��ϵ�λ -> �Ƽ۵�λ ����
            decimal unitMultiple = MCService.GetTradeUnitScale(code, utMatch);

            bool isSuccess = holdMemory.ReadAndWrite(hold =>
                                                         {
                                                             decimal holdAmount = hold.AvailableAmount +
                                                                                  hold.FreezeAmount;
                                                             decimal costPrice = (holdAmount*hold.CostPrice*unitMultiple +
                                                                                  dealSum.CapitalSum + dealSum.CostSum)/
                                                                                 (holdAmount*unitMultiple +
                                                                                  dealSum.AmountSum*unitMultiple);
                                                             //��������
                                                             costPrice = Utils.Round(costPrice);

                                                             decimal holdPrice = MCService.GetHoldPrice(code, costPrice,
                                                                                                        (holdAmount*
                                                                                                         unitMultiple +
                                                                                                         dealSum.
                                                                                                             AmountSum*
                                                                                                         unitMultiple));
                                                             //��������
                                                             holdPrice = Utils.Round(holdPrice);

                                                             hold.CostPrice = costPrice;
                                                             hold.BreakevenPrice = holdPrice;

                                                             //�ֲ־���=���ֲ־���*�ֲ���+����ɽ���*����ۣ�/(�ֲ���+����ɽ���)
                                                             decimal holdAveragePrice = (hold.HoldAveragePrice*
                                                                                         holdAmount*unitMultiple +
                                                                                         dealSum.CapitalSum)/
                                                                                        (holdAmount*unitMultiple +
                                                                                         dealSum.AmountSum*unitMultiple);
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
        public static bool DoXHBuy_HoldingRescue(int holdingAccoutnId, XHDealSum dealSum, int holdingTradingRule)
        {
            XHHoldMemoryTable holdMemory = MemoryDataManager.XHHoldMemoryList.GetByAccountHoldLogoId(holdingAccoutnId);
            if (holdMemory == null)
            {
                holdMemory = XHCommonLogic.GetHoldMemoryTable(holdingAccoutnId);
            }

            if (holdMemory == null)
            {
                return true;
            }

            return XHBuy_InstantReckon_HoldingProcess(holdMemory, dealSum, holdingTradingRule);
        }

        #endregion

        #region �ֻ������㴦��

        /// <summary>
        /// �ֻ��������ύ���
        /// </summary>
        /// <param name="idoExList">��չ�ɽ��ر��б�</param>
        /// <param name="dealSum">�ֻ��ɽ��ر�����</param>
        /// <param name="strMessage">������Ϣ</param>
        /// <param name="tet">ί��</param>
        /// <param name="tradeList">�ɽ��б�</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        private bool XHSell_InstantReckon_Deal(List<StockDealBackEntityEx> idoExList, XHDealSum dealSum,
                                               ref string strMessage, out XH_TodayEntrustTableInfo tet,
                                               out List<XH_TodayTradeTableInfo> tradeList)
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
            tradeList = new List<XH_TodayTradeTableInfo>();

            #region 1.�ʽ��Ԥ����

            //�ȼ���ʽ𶳽���еĶ�����ͷ����Ƿ񹻿�
            XH_CapitalAccountFreezeTableInfo caf;
            if (!GetCapitalFreezeTable(out caf))
            {
                strMessage = "GT-2524:[�ֻ��ɽ�����]�޷���ȡ�ʽ𶳽��¼.";

                return false;
            }

            var capMemory = MemoryDataManager.XHCapitalMemoryList.GetByCapitalAccountLogo(CapitalAccountId);
            if (capMemory == null)
            {
                strMessage = "GT-2521:[�ֻ��ɽ�����]�ʽ��ʻ�������:" + CapitalAccount;

                return false;
            }

            //ע�⣺Ϊ�˼򻯴����������ڼ䲻�ٽ��ж����ʽ�Ĳ������㼰�����ٲ��ļ��㣬ͳһ�ڱ������������
            //���ʽ𶳽����м��У�飬�����������ڲ���ֱ�����ʽ𶳽���ϼ�ȥ�ɽ����ͳɽ�����
            XH_CapitalAccountTable_DeltaInfo capitalDelta = XHSell_InstantReckon_PreCapitalProcess(caf, dealSum);
            capitalDelta.CapitalAccountLogo = capMemory.Data.CapitalAccountLogo;

            #endregion

            #region 2.�ֱֲ�Ԥ����

            bool isSuccess = false;
            XHHoldMemoryTable holdMemory = null;
            holdMemory = XHSell_InstantReckon_PreHoldingProcess(holdMemory, ref isSuccess, ref strMessage);

            if (!isSuccess)
            {
                strMessage = "GT-2590:[�ֻ��ɽ�����]�޷���ȡ�ֲּ�¼.";
                LogHelper.WriteDebug("XHSell_InstantReckon_Deal_!isSuccess " + strMessage);

                return false;
            }

            if (holdMemory == null)
            {
                strMessage = "GT-2590:[�ֻ��ɽ�����]�޷���ȡ�ֲּ�¼.";
                LogHelper.WriteDebug("XHSell_InstantReckon_Deal_holdMemory == null " + strMessage);

                return false;
            }

            decimal holdAveragePrice = holdMemory.Data.HoldAveragePrice;

            XH_AccountHoldTableInfo_Delta holdDelta = new XH_AccountHoldTableInfo_Delta();
            var holdData = holdMemory.Data;

            string holdFormat =
                "XHSell_InstantReckon_Deal��ȡ�ֲ־���[AccountHoldLogoId={0},UserAccountDistributeLogo={1},Code={2},HoldAveragePrice={3}]";
            string holdDesc = string.Format(holdFormat, holdData.AccountHoldLogoId, holdData.UserAccountDistributeLogo,
                                            holdData.Code,
                                            holdData.HoldAveragePrice);
            LogHelper.WriteInfo(holdDesc);

            holdDelta.AccountHoldLogoId = holdData.AccountHoldLogoId;
            holdDelta.FreezeAmountDelta -= dealSum.AmountSum;

            #endregion

            #region 3.�ʽ𶳽�ͳֲֶ��ᴦ��

            //�����ʽ𶳽�ͳֲֶ���,�Լ�ί����Ϣ
            XH_AcccountHoldFreezeTableInfo holdFreeze = null;

            holdFreeze = XHDataAccess.GetHoldAccountFreeze(EntrustNumber,
                                                           Entity.Contants.Types.FreezeType.DelegateFreeze);
            if (holdFreeze == null)
            {
                strMessage = "GT-2589:[�ֻ��ɽ�����]�޷���ȡ�ֲֶ����¼.";
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


            tet = XHDataAccess.GetTodayEntrustTable(EntrustNumber);
            var tet2 = tet;

            decimal hasDoneProfit = XHSell_InstantReckon_EntrustProcess(dealSum, tet2, holdAveragePrice);

            //���ֻ��ʽ���������ۼ���ʵ��ӯ�����ֶ�
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
                        XHSell_InstantReckon_CapitalFreezeProcess(tet2, dealSum, tm);

                        //1.�����ʽ𶳽�
                        XH_CapitalAccountFreezeTableDal cafDal =
                            new XH_CapitalAccountFreezeTableDal();
                        cafDal.Update(caf, tm);

                        //2.���³ֲֶ���
                        if (holdFreeze != null)
                        {
                            XH_AcccountHoldFreezeTableDal holdDal = new XH_AcccountHoldFreezeTableDal();
                            holdDal.Update(holdFreeze, tm.Database, tm.Transaction);
                        }

                        //3.����ί�б�
                        XHDataAccess.UpdateEntrustTable(tet2, tm.Database,
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
                        strMessage = "GT-2522:[�ֻ��ɽ�����]���ݿ��ύʧ��";

                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.Message, ex);
                strMessage = "GT-2522:[�ֻ��ɽ�����]���ݿ��ύʧ��";
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
        private void XHSell_InstantReckon_CapitalFreezeProcess(XH_TodayEntrustTableInfo tet2, XHDealSum dealSum,
                                                               ReckoningTransaction tm)
        {
            //1.��T+0ʱ�����ʽ𶳽�
            if (CaptitalTradingRule != 0)
            {
                XH_CapitalAccountFreezeTableDal cafDal =
                    new XH_CapitalAccountFreezeTableDal();

                //��ǰ�ɽ�����
                var newCaft = new XH_CapitalAccountFreezeTableInfo();
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
                    (int) Entity.Contants.Types.FreezeType.ReckoningFreeze;

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
        private decimal XHSell_InstantReckon_EntrustProcess(XHDealSum dealSum, XH_TodayEntrustTableInfo tet2,
                                                            decimal holdAveragePrice)
        {
            //��ϵ�λ
            Types.UnitType utMatch = MCService.GetMatchUnitType(tet2.SpotCode);
            //��ϵ�λ -> �Ƽ۵�λ ����
            decimal unitMultiple = MCService.GetTradeUnitScale(tet2.SpotCode, utMatch);

            //�ɽ����ۣ��۸��Ǳ��۵�λ�������Ǵ�ϵ�λ��������ʱ��Ҫͳһ�����Ѵ�ϵ�λתΪ���۵�λ��
            decimal tradeAveragePrice =
                (tet2.TradeAveragePrice*tet2.TradeAmount*unitMultiple +
                 dealSum.CapitalSum)/
                (tet2.TradeAmount*unitMultiple + dealSum.AmountSum*unitMultiple);
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
                "XHSell_InstantReckon_EntrustProcess�ֻ���ʵ��ӯ��[EntrustNumber={0},Code={1}][��ʵ��ӯ��{2}=�ɽ����{3}-�ֲ־���{4}*�ɽ���{5}��({6})]";
            string desc = string.Format(format, tet2.EntrustNumber, tet2.SpotCode, hasDoneProfit, dealSum.CapitalSum,
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
        private XH_CapitalAccountTable_DeltaInfo XHSell_InstantReckon_PreCapitalProcess(
            XH_CapitalAccountFreezeTableInfo caf, XHDealSum dealSum)
        {
            //caf.FreezeCapitalAmount -= 0; �����ᶳ���ʽ�
            caf.FreezeCost -= dealSum.CostSum;

            //T+0:�ʽ����ܶ������ȥ���γɽ��ķ��úͣ���Ϊ�ʽ𶳽����ȥ����ô��Ǯ��,�����ʽ���ϱ��γɽ����
            XH_CapitalAccountTable_DeltaInfo capitalDelta = new XH_CapitalAccountTable_DeltaInfo();
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
        private XHHoldMemoryTable XHSell_InstantReckon_PreHoldingProcess(XHHoldMemoryTable holdMemory,
                                                                         ref bool isSuccess,
                                                                         ref string strMessage)
        {
            try
            {
                //holdMemory = MemoryDataManager.XHHoldMemoryList.GetByHoldAccountAndCurrencyType(HoldingAccount, Code,
                //                                                                                CurrencyType);
                holdMemory = MemoryDataManager.XHHoldMemoryList.GetByAccountHoldLogoId(HoldingAccountId);
                //����ֲ�Ϊ�գ���ô�ȴ����ݿ���أ����ǲ����ڴ��û�м���
                if (holdMemory == null)
                {
                    XH_AccountHoldTableInfo hold = GetHoldFromDB(HoldingAccount, Code, CurrencyType);
                    //������ݿ��У���ôֱ�Ӽ��ص��ڴ����
                    if (hold != null)
                    {
                        isSuccess = MemoryDataManager.XHHoldMemoryList.AddXHAccountHoldTableToMemory(hold);
                    }
                    else
                    {
                        //������ݿ�Ҳû�У���ô�����޳ֲ�
                        return null;
                    }

                    holdMemory = MemoryDataManager.XHHoldMemoryList.GetByHoldAccountAndCurrencyType(HoldingAccount, Code,
                                                                                                    CurrencyType);

                    if (holdMemory == null)
                    {
                        strMessage = "GT-2525:[�ֻ��ɽ�����]�޷���ȡ�ֲּ�¼.";
                        LogHelper.WriteDebug("XHSell_InstantReckon_PreHoldingProcess-" + strMessage);
                        isSuccess = false;
                        return null;
                    }
                }

                HoldingAccountId = holdMemory.Data.AccountHoldLogoId;
                isSuccess = true;
            }
            catch (Exception ex)
            {
                strMessage = "GT-2520:[�ֻ��ɽ�����]�ֲִ����쳣:" + ex;
                LogHelper.WriteError("XHSell_InstantReckon_PreHoldingProcess-" + ex.Message, ex);
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
        private void BuildTradeList(XH_TodayEntrustTableInfo tet2, ReckoningTransaction tm,
                                    List<StockDealBackEntityEx> idoExList, List<XH_TodayTradeTableInfo> tradeList2)
        {
            foreach (var idoEx in idoExList)
            {
                var ido = idoEx.DealBack;
                var costResult = idoEx.CostResult;

                var trade = XHCommonLogic.BuildXHDealRpt(tet2, ido,
                                                         costResult,
                                                         idoEx.DealCapital,
                                                         tm);
                if (trade != null)
                    tradeList2.Add(trade);
            }
        }

        private bool GetCapitalFreezeTable(out XH_CapitalAccountFreezeTableInfo caf)
        {
            caf = XHDataAccess.GetCapitalAccountFreeze(EntrustNumber,
                                                       Entity.Contants.Types.FreezeType.DelegateFreeze);
            if (caf == null)
            {
                //��ô��Ϊ����Ľ��ͷ��ö��Ѿ�Ϊ0������һ���յĶ����¼�Թ�����ʹ��
                try
                {
                    XHCommonLogic.InsertNullCapitalFreeze(CapitalAccountId, EntrustNumber);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex.Message, ex);
                    return false;
                }

                caf = XHDataAccess.GetCapitalAccountFreeze(EntrustNumber,
                                                           Entity.Contants.Types.FreezeType.DelegateFreeze);
            }
            return true;
        }

        /// <summary>
        /// ��ȡ�ɽ��ر��б����е�ID
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<string> GetIdList(List<StockDealBackEntity> list)
        {
            var ids = new List<string>();
            foreach (var ido in list)
            {
                ids.Add(ido.Id);
            }
            return ids;
        }

        /// <summary>
        /// �����ֻ��ɽ��ر�������Ϣ
        /// </summary>
        /// <returns>�ֻ��ɽ��ر����ܶ���</returns>
        private bool ComputeXHDealBacks(List<StockDealBackEntity> idos, out List<StockDealBackEntityEx> idoexs,
                                        out XHDealSum dealSum, ref string strMessage)
        {
            idoexs = new List<StockDealBackEntityEx>();
            dealSum = new XHDealSum();
            foreach (var ido in idos)
            {
                StockDealBackEntityEx ex = new StockDealBackEntityEx(ido);
                XHCostResult costResult = null;
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
        private void SetOrderState(XH_TodayEntrustTableInfo tet)
        {
            if (tet.OrderStatusId == (int) Entity.Contants.Types.OrderStateType.DOSCanceled)
                return;

            //����۸�ķϵ�
            if (tet.CancelAmount == -1)
            {
                tet.OrderStatusId = (int) Entity.Contants.Types.OrderStateType.DOSCanceled;
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
                        tet.OrderStatusId = (int) Entity.Contants.Types.OrderStateType.DOSRemoved;
                    }
                    else
                    {
                        tet.OrderStatusId = (int) Entity.Contants.Types.OrderStateType.DOSPartRemoved;
                    }
                }
                    //������û�з����������ѳ�   
                else
                {
                    tet.OrderStatusId = (int) Entity.Contants.Types.OrderStateType.DOSDealed;
                }
            }
                //ί����>�ɽ���+������ ���ֳɽ�
            else
            {
                //������ڳɽ��Ĺ����У��յ������������ô�ͱ��ֲ��ɴ���״̬��ֱ��������ѳɻ��ѳ�
                if (tet.OrderStatusId == (int) Entity.Contants.Types.OrderStateType.DOSPartDealRemoveSoon)
                    return;

                //���򲻹���û�г�������ͳһ����Ϊ���ɣ���Ϊ��Ͽ����ȷ��س��������Դ˴�״̬�������ã�
                if (tet.TradeAmount > 0)
                    tet.OrderStatusId = (int) Entity.Contants.Types.OrderStateType.DOSPartDealed;
            }
        }

        private List<string> GetIDList2(List<StockDealBackEntityEx> list)
        {
            List<string> ids = new List<string>();
            foreach (var entityEx in list)
            {
                ids.Add(entityEx.DealBack.Id);
            }

            return ids;
        }

        private XH_AccountHoldTableInfo GetCloneHold(XH_AccountHoldTableInfo info)
        {
            XH_AccountHoldTableInfo newInfo = new XH_AccountHoldTableInfo();
            newInfo.AccountHoldLogoId = info.AccountHoldLogoId;
            newInfo.AvailableAmount = info.AvailableAmount;
            newInfo.BreakevenPrice = info.BreakevenPrice;
            newInfo.Code = info.Code;
            newInfo.CostPrice = info.CostPrice;
            newInfo.CurrencyTypeId = info.CurrencyTypeId;
            newInfo.FreezeAmount = info.FreezeAmount;
            newInfo.HoldAveragePrice = info.HoldAveragePrice;
            newInfo.UserAccountDistributeLogo = info.UserAccountDistributeLogo;

            return newInfo;
        }

        #endregion
    }

    /// <summary>
    /// �ֻ��ɽ��ر���չ��
    /// </summary>
    public class StockDealBackEntityEx : ReckonDealBackEx<StockDealBackEntity, XHCostResult>
    {
        public StockDealBackEntityEx(StockDealBackEntity dealBack)
            : base(dealBack)
        {
        }
    }
}