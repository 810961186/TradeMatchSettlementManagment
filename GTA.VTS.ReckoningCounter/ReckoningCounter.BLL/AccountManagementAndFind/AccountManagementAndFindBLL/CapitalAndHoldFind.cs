using System;
using System.Collections.Generic;
using System.Text;
using CommonRealtimeMarket;
//using CommonRealtimeMarket.entity;
using GTA.VTS.Common.CommonUtility;
using ReckoningCounter.BLL.AccountManagementAndFind.AccountManagementAndFindBLL;
using ReckoningCounter.BLL.ManagementCenter;
using ReckoningCounter.DAL;
using GTA.VTS.Common.CommonObject;
using ReckoningCounter.DAL.DevolveVerifyCommonService;
using ReckoningCounter.Entity;
using ReckoningCounter.Entity.AccountManagementAndFindEntity;
using ReckoningCounter.DAL.AccountManagementAndFindDAL;
using ReckoningCounter.Model;
using ReckoningCounter.DAL.Data;
using ReckoningCounter.BLL.Common;
using ReckoningCounter.Entity.Model.QueryFilter;
using ReckoningCounter.Entity.Model;
using ReckoningCounter.MemoryData;
using ReckoningCounter.MemoryData.QH.Capital;
using ReckoningCounter.MemoryData.XH.Hold;
using ReckoningCounter.MemoryData.QH.Hold;
using ReckoningCounter.MemoryData.XH.Capital;
using ReckoningCounter.DAL.Data.QH;
using CommonObject = GTA.VTS.Common.CommonObject;
using RealTime.Server.SModelData.HqData;

namespace ReckoningCounter.BLL.AccountManagementAndFind.AccountManagementAndFindBLL
{
    /// <summary>
    /// ���ã���̨���ʽ��˻���ϸ��ѯ�͸��ֲ��˻���ϸ��ѯ�������� �����ʽ���ϸ��ѯ���ֻ��ʽ���ϸ��ѯ�� �ڻ��ʽ���ϸ��ѯ���ֻ��ֲֲ�ѯ�� �ڻ��ֲֲ�ѯ��
    /// ���ߣ���ƺ�
    /// ���ڣ�2008-10-30
    /// Update BY�����
    /// Update Date:2009-07-22
    /// Desc.:�޸���ص�DAL��������ط������߼��Լ�ҵ���߼����������һЩ���ݴӻ����л�ȡ�����û��˺ź��ʽ𡢳ֲֵ���Ϣ
    /// </summary>
    public class CapitalAndHoldFindBLL
    {
        # region ͨ������ԱID��øý���Ա�������˺�
        /// <summary>
        ///  ͨ������ԱID��øý���Ա�������˺�
        /// </summary>
        /// <param name="userId">����ԱID</param>
        /// <returns></returns>
        string GetBankAccountByUserId(string userId)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                #region oldcode
                // var userAccountAllocationTable = DataRepository.UaUserAccountAllocationTableProvider.Find(
                //   string.Format("UserID='{0}' AND AccountTypeLogo='1'", userId));
                #endregion
                #region ��ȡ�û������˺�
                #region �ӻ����л�ȡ�û������˺�
                UA_UserAccountAllocationTableInfo account = AccountManager.Instance.GetAccountByUserIDAndAccountType(userId, (int)Types.AccountType.BankAccount);
                #endregion
                //UA_UserAccountAllocationTableDal dal = new UA_UserAccountAllocationTableDal();
                //List<UA_UserAccountAllocationTableInfo> userAccountAllocationTable = dal.GetListArray(string.Format(" UserID='{0}' AND AccountTypeLogo='1'", userId));
                //foreach (UA_UserAccountAllocationTableInfo item in userAccountAllocationTable)
                //{
                //    result = item.UserAccountDistributeLogo;
                //}
                #endregion
            }
            return result;
        }
        # endregion ͨ���ʽ��˺Ż����ֵ

        # region ͨ������Ա��ӵ�еĽ����˺Ż�øý���ԱID
        /// <summary>
        ///  ͨ������Ա��ӵ�еĽ����˺Ż�øý���ԱID
        ///  Update Date:2009-07-15
        ///  Update By:���
        ///  Desc.:�޸Ĳ������ݲ㷽�������ʵ��
        /// </summary>
        /// <param name="TradeAccount">�����˺�</param>
        /// <returns></returns>
        public string GetUserIdByTradeAccount(string TradeAccount)
        {
            string userId = string.Empty;

            if (!string.IsNullOrEmpty(TradeAccount))
            {
                #region old code
                //var userAccountAllocationTable = DataRepository.UaUserAccountAllocationTableProvider.Find(
                //    string.Format("UserAccountDistributeLogo='{0}'", TradeAccount));
                #endregion
                #region �ӻ����л�ȡ�û��˺���Ϣ
                UA_UserAccountAllocationTableInfo userInfo = AccountManager.Instance.GetUserByAccount(TradeAccount);
                if (userInfo != null)
                {
                    userId = userInfo.UserID;
                }
                //UA_UserAccountAllocationTableDal dal = new UA_UserAccountAllocationTableDal();
                //List<UA_UserAccountAllocationTableInfo> userAccountAllocationTable = dal.GetListArray(string.Format(" UserAccountDistributeLogo='{0}'", TradeAccount));
                //foreach (UA_UserAccountAllocationTableInfo item in userAccountAllocationTable)
                //{
                //    userId = item.UserID;
                //}
                #endregion
            }
            return userId;
        }
        # endregion

        # region ͨ���ֻ��ʽ��˺źͱ��ֻ�ø��ʽ��˺��µĸñ��ֵĳֲ���ֵ
        /// <summary>
        ///  ͨ���ֻ��ʽ��˺źͱ��ֻ�ø��ʽ��˺��µĸñ��ֵĳֲ���ֵ
        ///  Update Date:2009-07-15
        ///  Update By:���
        ///  Desc.:�޸Ĳ������ݲ㷽�������ʵ��,������out�ܸ���ӯ����δʵ��ӯ����ͳ�Ʋ���
        /// </summary>
        /// <param name="strFundAccount">�ֻ��ʽ��˻�</param>
        /// <param name="currencyType">��ѯ���׻�������</param>
        /// <param name="floatProfitLoss">�ܸ���ӯ��</param>
        /// <returns>�������гֲ�����ֵ</returns>
        decimal GetMarketValueByXhFundAccount(string strFundAccount, Types.CurrencyType currencyType, out decimal floatProfitLoss)
        {
            #region ==��������==
            decimal result = 0;
            floatProfitLoss = 0;
            //string errorMsg = "";

            #endregion
            #region �ӻ����и����ֻ��ʽ��˻���ȡ�����ĳֲ��˻���Ϣ
            UA_UserAccountAllocationTableInfo userInfo = AccountManager.Instance.GetHoldAccountByCapitalAccount(strFundAccount);
            //UA_UserAccountAllocationTableDal userDal = new UA_UserAccountAllocationTableDal();
            ////�����ֻ��ʽ��˻���ȡ�����ĳֲ��˻���Ϣ
            //UA_UserAccountAllocationTableInfo userInfo = userDal.GetUserHoldAccountByUserCapitalAccount(strFundAccount);
            #endregion
            if (userInfo != null)
            {
                result = GetMarketValueByXH_HoldAccount(userInfo.UserAccountDistributeLogo, currencyType, out floatProfitLoss);
            }

            #region update 2009-07-14 ���
            //string strHoldAccount = CommonDataAgent.GetRealtionAccountIdByAccountId(strFundAccount);
            //if (!string.IsNullOrEmpty(strHoldAccount))
            //{
            //    var holdList = DataRepository.XhAccountHoldTableProvider.Find(
            //        string.Format(" UserAccountDistributeLogo='{0}' AND CurrencyTypeId='{1}'", strHoldAccount, (int)currencyType));
            //    foreach (var item in holdList)
            //    {
            //        HqExData vhe = CommonDataAgent.RealtimeService.GetStockHqData(item.Code);
            //        if (vhe != null)
            //        {
            //            result += (item.AvailableAmount.Value + item.FreezeAmount.Value) *
            //                      Convert.ToDecimal(vhe.HqData.Lasttrade);
            //        }
            //    }
            //}
            #endregion

            return result;
        }
        /// <summary>
        /// ͨ���ֻ��ֲ��˺źͱ��ֻ�ø��ֻ��ֲ��˺��µĸñ��ֵ��ֻ��ֲ���ֵ���ܸ���ӯ��
        /// Create by:���
        /// Create Date:2009-07-15
        /// </summary>
        /// <param name="holdAccount">�ֻ��ֲ��˺�</param>
        /// <param name="currencyType">���׻�������</param>
        /// <param name="floatProfitLoss">�ܸ���ӯ��</param>
        /// <returns></returns>
        decimal GetMarketValueByXH_HoldAccount(string holdAccount, Types.CurrencyType currencyType, out decimal floatProfitLoss)
        {
            decimal marketValue = 0;
            floatProfitLoss = 0;
            string errorMsg = "";
            #region ��ȡ��ǰ�ֲ��˻������гֲ�
            List<XH_AccountHoldTableInfo> list = QueryXH_AccountHoldByAccount(holdAccount, (QueryType.QueryCurrencyType)((int)currencyType), out errorMsg);
            #endregion

            #region �������гֲ���Ϣͳ��
            foreach (XH_AccountHoldTableInfo item in list)
            {
                string codeStr = item.Code;
                //ͨ�������������ȡ��ǰ�ֻ�����
                HqExData vhe = CommonDataAgent.RealtimeService.GetStockHqData(codeStr);

                #region ������Ʒ�����ȡ��ϣ������׵�λ����λת���ɼƼ۵�λ�ı���
                //������Ʒ�����ȡ��ϵ�λ
                Types.UnitType utMatch = MCService.GetMatchUnitType(codeStr);
                //���ݴ�ϵ�λת���ɼƼ۵�λ��ȡ��ת���ı���
                decimal unitMultiple = MCService.GetTradeUnitScale(codeStr, utMatch);
                #endregion

                # region ��ȡ�ֲ���������ֵ
                decimal amount = item.AvailableAmount + item.FreezeAmount;
                //�Ѵ�ϣ������ף���λ�ֲ�����ת��Ϊ�Ƽ۵�λ�ĳֲ���������Ϊ֮ǰ�洢�����ݿ��е���ֲ����й�
                //�Ķ��ǽ��׵�λ�����۸���صĶ��ǼƼ۵�λ��
                amount = amount * unitMultiple;
                #endregion

                # region ��ȡ��ǰ�۲���ֵ �����ȡ���������������³ɽ��������������̼ۼ�������������̼�Ϊ0�ٰѵ�ǰ���Գֲ־������������
                //��ȡ��ǰ�۲���ֵ �����ȡ���������������³ɽ��������������̼ۼ���
                //����������̼�Ϊ0�ٰѵ�ǰ���Գֲ־������������
                decimal realtimePrice = 0.00M;
                if (vhe != null)
                {
                    if (vhe.HqData.Lasttrade == 0)
                    {
                        decimal yesterPrice = MCService.CommonPara.GetClosePriceByCode(codeStr);
                        if (yesterPrice <= 0)
                        {
                            errorMsg = "���ֻ��ʽ�ͳ�ơ��������³ɽ���Ϊ0,�������̼�ҲΪ0,��ǰ��¼ʹ�óֲ־��ۼ���";
                            realtimePrice = item.HoldAveragePrice;
                        }
                        else
                        {
                            errorMsg = "���ֻ��ʽ�ͳ�ơ��������³ɽ���Ϊ0,��ǰ��¼ʹ���������̼ۼ���";
                            realtimePrice = yesterPrice;
                        }
                        LogHelper.WriteDebug("�ֲ��˺�:" + holdAccount + " ���룺" + codeStr + errorMsg);
                    }
                    else
                    {
                        realtimePrice = Convert.ToDecimal(vhe.HqData.Lasttrade);
                    }
                }
                else
                {
                    decimal yesterPrice = MCService.CommonPara.GetClosePriceByCode(codeStr);
                    if (yesterPrice <= 0)
                    {
                        errorMsg = "���ֻ��ʽ�ͳ�ơ�δ�ܻ�ȡ����,�������̼�ҲΪ0,��ǰ��¼ʹ�óֲ־��ۼ���";
                        realtimePrice = item.HoldAveragePrice;
                    }
                    else
                    {
                        errorMsg = "���ֻ��ʽ�ͳ�ơ�δ�ܻ�ȡ����,��ǰ��¼ʹ���������̼ۼ���";
                        realtimePrice = yesterPrice;
                    }
                    LogHelper.WriteDebug("�ֲ��˺�:" + holdAccount + " ���룺" + codeStr + errorMsg);
                }
                #endregion

                #region ͳ���ܸ���ӯ��
                //����ӯ��=�ֲ�����*����ǰ��-�ֲ־��ۣ�
                floatProfitLoss += amount * (realtimePrice - item.HoldAveragePrice);
                #endregion

                #region ����ֵ
                marketValue += amount * realtimePrice;
                #endregion

            }
            #endregion

            return marketValue;
        }
        # endregion

        # region ͨ���ڻ��ʽ��˺źͱ��ֻ�ø��ʽ��˺��µĸñ��ֵĳֲ���ֵ
        /// <summary>
        ///  ͨ���ڻ��ʽ��˺źͱ��ֻ�ø��ʽ��˺��µĸñ��ֵĳֲ���ֵ
        ///  Update Date:2009-07-15
        ///  Update By:���
        ///  Desc.:�޸Ĳ������ݲ㷽�������ʵ��,������out�ֲָܳ���ӯ��ͳ��,�ֲֶܳ���ӯ������
        /// </summary>
        /// <param name="strFundAccount">�ڻ��ʽ��˻�</param>
        /// <param name="currencyType">���׻�������</param>
        /// <param name="floatProfitLossTotal">�ֲָܳ���ӯ��</param>
        /// <param name="marketProfitLossTotal">�ֲֶܳ���ӯ��</param>
        /// <returns></returns>
        decimal GetMarketValueByQhFundAccount(string strFundAccount, Types.CurrencyType currencyType, out decimal floatProfitLossTotal, out decimal marketProfitLossTotal)
        {
            #region ==��������==
            decimal result = 0;
            floatProfitLossTotal = 0;//�ֲָܳ���ӯ��
            marketProfitLossTotal = 0;//�ֲֶܳ���ӯ��
            //string errorMsg = "";
            #endregion
            #region �ӻ����и����ֻ��ʽ��˻���ȡ�����ĳֲ��˻���Ϣ
            UA_UserAccountAllocationTableInfo userInfo = AccountManager.Instance.GetHoldAccountByCapitalAccount(strFundAccount);
            //UA_UserAccountAllocationTableDal userDal = new UA_UserAccountAllocationTableDal();
            ////�����ֻ��ʽ��˻���ȡ�����ĳֲ��˻���Ϣ
            // UA_UserAccountAllocationTableInfo userInfo = userDal.GetUserHoldAccountByUserCapitalAccount(strFundAccount);
            #endregion
            if (userInfo != null)
            {
                result = GetMarketValueByQH_HoldAccount(userInfo.UserAccountDistributeLogo, currencyType, out floatProfitLossTotal, out marketProfitLossTotal);
            }
            #region update 2009-07-14 ���
            //string strHoldAccount = CommonDataAgent.GetRealtionAccountIdByAccountId(strFundAccount);
            //if (!string.IsNullOrEmpty(strHoldAccount))
            //{
            //    //=================modify by xiongxl ====================
            //    //�޸��ֶ�����
            //    var holdList = DataRepository.QhHoldAccountTableProvider.Find(
            //        string.Format(" UserAccountDistributeLogo='{0}' AND TradeCurrencyType='{1}'", strHoldAccount, (int)currencyType));
            //    //====================end================================
            //    foreach (var item in holdList)
            //    {
            //        VTFutData vte = CommonDataAgent.RealtimeService.GetFutData(item.Contract);
            //        if (vte != null)
            //        {
            //            result += (item.HistoryHoldAmount.Value + item.HistoryFreezeAmount.Value + item.TodayHoldAmount.Value + item.TodayFreezeAmount.Value) *
            //                      Convert.ToDecimal(vte.Lasttrade);//LasttradeΪ��ǰ�ۣ��мۣ�
            //        }
            //    }
            //}
            #endregion

            return result;
        }
        /// <summary>
        /// ͨ���ڻ��ֲ��˺źͱ��ֻ�ø��ʽ��˺��µĸñ��ֵĳֲ���ֵ���ܸ���ӯ�����ܶ���ӯ��
        /// Create by:���
        /// Create Date:2009-07-15
        /// </summary>
        /// <param name="holdAccount">�ֲ��˺�</param>
        /// <param name="currencyType">���׻�������</param>
        /// <param name="floatProfitLossTotal">�ܸ���ӯ��</param>
        /// <param name="marketProfitLossTotal">�ܶ���ӯ��</param>
        /// <returns></returns>
        decimal GetMarketValueByQH_HoldAccount(string holdAccount, Types.CurrencyType currencyType, out decimal floatProfitLossTotal, out decimal marketProfitLossTotal)
        {
            #region ==��������==
            decimal marketValue = 0;
            floatProfitLossTotal = 0;//�ֲָܳ���ӯ��
            marketProfitLossTotal = 0;//�ֲֶܳ���ӯ��
            string errorMsg = "";
            #endregion

            #region ��ȡ��ǰ�ֲ��˻������гֲ�
            List<QH_HoldAccountTableInfo> list = QueryQH_HoldAccountByAccount(holdAccount, (QueryType.QueryCurrencyType)((int)currencyType), out errorMsg);
            #endregion

            #region �������гֲ���Ϣͳ��
            foreach (QH_HoldAccountTableInfo item in list)
            {
                string contract = item.Contract;
                //ͨ�������������ȡ��ǰ�ֻ�����
                int? breedID = MCService.CommonPara.GetBreedClassTypeEnumByCommodityCode(contract);
                //������
                double preSettlementPrice = 0;
                //������
                double settlementPrice = 0;
                //���¼�
                double lasttrade = 0;
                decimal realtimePrice = 0.00M;

                bool isGetRealtime = false;
                if (breedID.HasValue)
                {
                    switch ((CommonObject.Types.BreedClassTypeEnum)breedID.Value)
                    {

                        case GTA.VTS.Common.CommonObject.Types.BreedClassTypeEnum.StockIndexFuture:
                            FutData vte = CommonDataAgent.RealtimeService.GetFutData(contract);
                            if (vte == null)
                            {
                                errorMsg = "����ָ�ڻ��ʽ�ͳ�ơ�δ��ȡ���ù�ָ�ڻ����������,��ǰ��¼ʹ�óֲ־��ۼ���.";
                                LogHelper.WriteDebug("�ֲ��˺�:" + item.UserAccountDistributeLogo + " ���룺" + item.Contract + errorMsg);
                                realtimePrice = item.HoldAveragePrice;
                            }
                            else
                            {
                                preSettlementPrice = vte.PreSettlementPrice;
                                settlementPrice = vte.SettlementPrice;
                                lasttrade = vte.Lasttrade;
                                isGetRealtime = true;
                            }
                            break;
                        case GTA.VTS.Common.CommonObject.Types.BreedClassTypeEnum.CommodityFuture:
                            MerFutData mfvte = CommonDataAgent.RealtimeService.GetMercantileFutData(contract);
                            if (mfvte == null)
                            {
                                errorMsg = "����Ʒ�ڻ��ʽ�ͳ�ơ�δ��ȡ������Ʒ�ڻ����������,��ǰ��¼ʹ�óֲ־��ۼ���.";
                                LogHelper.WriteDebug("�ֲ��˺�:" + item.UserAccountDistributeLogo + " ���룺" + contract + errorMsg);
                                realtimePrice = item.HoldAveragePrice;
                            }
                            else
                            {
                                preSettlementPrice = mfvte.PreClearPrice;
                                settlementPrice = mfvte.ClearPrice;
                                lasttrade = mfvte.Lasttrade;
                                isGetRealtime = true;
                            }
                            break;
                    }

                }


                #region ������Ʒ�����ȡ��ϣ������׵�λ����λת���ɼƼ۵�λ�ı���
                ////������Ʒ�����ȡ��ϵ�λ
                //Types.UnitType utMatch = MCService.GetMatchUnitType(contract);
                ////���ݴ�ϵ�λת���ɼƼ۵�λ��ȡ��ת���ı���
                //decimal unitMultiple = MCService.GetTradeUnitScale(contract, utMatch);
                //������Ʒ�����ȡ�ڻ����׵�λ�Ƽ۵�λ����
                decimal scale = MCService.GetFutureTradeUntiScale(contract);
                #endregion

                # region ��ȡ�ֲ���������ֵ
                decimal amount = item.HistoryHoldAmount + item.HistoryFreezeAmount + item.TodayHoldAmount + item.TodayFreezeAmount;
                //�Ѵ�ϣ������ף���λ�ֲ�����ת��Ϊ�Ƽ۵�λ�ĳֲ���������Ϊ֮ǰ�洢�����ݿ��е���ֲ����й�
                //�Ķ��ǽ��׵�λ�����۸���صĶ��ǼƼ۵�λ��
                amount = amount * scale;
                #endregion

                # region ��ȡ��ǰ�۲���ֵ �����ȡ�����������ݰѵ�ǰ���Գֲ־������������
                //  �ֲ�ӯ�����������ֲֺ�Լ�ĳֲ־������г��ɽ���֮��Ĳ�����棬
                //�ڵ������̽���ǰ������Ϊ�������棬���ս���󣬶���ӯ��תΪʵ�ʵ�ӯ����
                //����ֲ�
                //�ֲ�ӯ��������=���г��ɽ���-����ֲ־��ۣ�*����ֲ�����*��Լ����
                //�����ֲ�
                //�ֲ�ӯ��������=�������ֲ־���-�г��ɽ��ۣ�*�����ֲ�����*��Լ����

                //����00��00��00�����տ���ǰ���г��ɽ���=���ս����
                //���տ������������̣��г��ɽ���=���µ�һ�ʳɽ��۸�Ҳ��Ϊ��ǰ��
                //�������̺�������23��59��59���г��ɽ���=���ս����
                //����ֲ־����������ֲ־����������������Ҫ��ɵ��ս���ۡ�
              

               if(isGetRealtime)
                {
                    int k = MCService.IsNowTimeMarket(Types.BreedClassTypeEnum.StockIndexFuture, contract);

                    switch (k)
                    {
                        case 0:
                            #region ��ǰʱ��00��00��00�����տ���ǰ
                            if (preSettlementPrice == 0)
                            {
                                errorMsg = "���ڻ��ʽ�ͳ�ơ���ȡ���ڻ�������������ս����Ϊ0,��ǰ��¼ʹ�óֲ־��ۼ���";
                                LogHelper.WriteDebug("�ֲ��˺�:" + holdAccount + " ���룺" + contract + errorMsg);
                                realtimePrice = item.HoldAveragePrice;
                            }
                            else
                            {
                                //����� PreSettlementPrice  //����� SettlementPrice
                                realtimePrice = Convert.ToDecimal(preSettlementPrice);
                            }
                            #endregion
                            break;
                        case 1:
                            #region ��ǰʱ�俪������������
                            if (lasttrade == 0)
                            {
                                errorMsg = "���ڻ��ʽ�ͳ�ơ���ȡ���ڻ�������������³ɽ���Ϊ0,��ǰ��¼ʹ�óֲ־��ۼ���";
                                LogHelper.WriteDebug("�ֲ��˺�:" + holdAccount + " ���룺" + contract + errorMsg);
                                realtimePrice = item.HoldAveragePrice;
                            }
                            else
                            {
                                //����� PreSettlementPrice  //����� SettlementPrice
                                realtimePrice = Convert.ToDecimal(lasttrade);
                            }
                            #endregion
                            break;
                        case 2:
                            #region �������̺�������23��59��59--2
                            if (settlementPrice == 0)
                            {
                                errorMsg = "���ڻ��ʽ�ͳ�ơ���ȡ���ڻ������������ս����Ϊ0,��ǰ��¼ʹ�óֲ־��ۼ���";
                                LogHelper.WriteDebug("�ֲ��˺�:" + holdAccount + " ���룺" + contract + errorMsg);
                                realtimePrice = item.HoldAveragePrice;
                            }
                            else
                            {
                                //����� PreSettlementPrice  //����� SettlementPrice
                                realtimePrice = Convert.ToDecimal(settlementPrice);
                            }
                            #endregion
                            break;
                    }
                }

                #region old code
                //if (vte != null)
                //{
                //    if (vte.Lasttrade == 0)
                //    {
               //        errorMsg = "���ڻ��ʽ�ͳ�ơ���ȡ���ֻ�������������³ɽ���Ϊ0,��ǰ��¼ʹ�óֲ־��ۼ���";
                //        LogHelper.WriteDebug("�ֲ��˺�:" + holdAccount + " ���룺" + contract + errorMsg);
                //        realtimePrice = item.HoldAveragePrice;
                //    }
                //    else
                //    {
                //        //����� PreSettlementPrice
                //        //����� SettlementPrice
                //        realtimePrice = Convert.ToDecimal(vte.Lasttrade);
                //    }
                //}
                //else
                //{
                //    errorMsg = "���ڻ��ʽ�ͳ�ơ�δ��ȡ�����ֻ����������,��ǰ��¼ʹ�óֲ־��ۼ���.";
                //    LogHelper.WriteDebug("�ֲ��˺�:" + holdAccount + " ���룺" + contract + errorMsg);
                //    realtimePrice = item.HoldAveragePrice;
                //}
                #endregion

                #endregion

                # region ��ȡ������ӯ��������ӯ��������ֵ

                if (item.BuySellTypeId == (int)CommonObject.Types.TransactionDirection.Buying)
                {
                    // ����Ķ���ӯ��=����Ķ���ӯ��=[�ֲ�����={0}*(��ǰ��={1}-�ֲ־���={2})*���׵�λ����={3}]
                    // marketProfitLossTotal += amount * (realtimePrice - item.HoldAveragePrice) * scale;
                    //ǰ�������Ѿ�תΪ�Ƽ۵�λ�������������ﲻ����*���׵�λ����
                    marketProfitLossTotal += amount * (realtimePrice - item.HoldAveragePrice);
                    //����ĸ���ӯ��=����ĸ���ӯ��=[�ֲ�����={0}*(��ǰ��={1}-���־���={2})*���׵�λ����={3}]
                    floatProfitLossTotal += amount * (realtimePrice - item.OpenAveragePrice);
                }
                else
                {
                    //������Ķ���ӯ��=������Ķ���ӯ��=[�ֲ�����={0}*(�ֲ־���={1}-��ǰ��={2})*���׵�λ����={3}]
                    marketProfitLossTotal += amount * (item.HoldAveragePrice - realtimePrice);
                    //������ĸ���ӯ��=������ĸ���ӯ��=[�ֲ�����={0}*(���־���={1}-��ǰ��={2})*���׵�λ����={3}]
                    floatProfitLossTotal += amount * (item.OpenAveragePrice - realtimePrice);
                }
                # endregion

                #region ����ֵ
                marketValue += amount * realtimePrice;
                #endregion

            }
            #endregion

            return marketValue;
        }
        # endregion

        #region �ֻ��ʽ���ϸ��ѯ(ͨ���ʽ��˺źͱ��֣�
        /// <summary>
        /// �ֻ��ʽ���ϸ��ѯ(ͨ���ʽ��˺źͱ��֣�
        /// </summary>
        /// <param name="strFundAccountId">���ʽ��˺�</param>
        /// <param name="currencyType">��������</param>
        /// <param name="userPassword">�û�����</param>
        /// <param name="strErrorMessage">�쳣��Ϣ</param>
        /// <returns></returns>
        public SpotCapitalEntity SpotCapitalFind(string strFundAccountId, Types.CurrencyType currencyType, string userPassword, ref string strErrorMessage)
        {
            SpotCapitalEntity result = null;
            try
            {
                #region update 2009-07-14 ���
                #region old Code
                //var capitalAccount = DataRepository.XhCapitalAccountTableProvider.Find(
                //    string.Format("UserAccountDistributeLogo='{0}' AND TradeCurrencyType ='{1}'", strFundAccountId,
                //                  (int)currencyType));
                #endregion
                List<XH_CapitalAccountTableInfo> capitalAccount = QueryXH_CapitalAccountByAccount(strFundAccountId, (QueryType.QueryCurrencyType)((int)currencyType), out strErrorMessage);
                #endregion
                //if (capitalAccount != null && capitalAccount.Count > 0)
                if (!Utils.IsNullOrEmpty(capitalAccount))
                {
                    XH_CapitalAccountTableInfo ca = capitalAccount[0];

                    #region ��ȡ���׻�����������
                    CM_CurrencyType cmCurrencyType = MCService.CommonPara.GetCurrencyTypeByID(ca.TradeCurrencyType);
                    string cName = "";
                    if (cmCurrencyType != null)
                        cName = cmCurrencyType.CurrencyName;
                    #endregion

                    decimal notDoneProfitLossTotal = 0;//δʵ��ӯ��
                    decimal marketValue = GetMarketValueByXhFundAccount(strFundAccountId, currencyType, out notDoneProfitLossTotal);
                    result = new SpotCapitalEntity(ca, marketValue, cName, notDoneProfitLossTotal);
                }
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                LogHelper.WriteError("�ֻ��ʽ���ϸ��ѯSpotCapitalFind()�쳣��" + strFundAccountId + "   CurrencyType:" + (int)currencyType, ex);
            }
            return result;
        }

        #endregion �ֻ��ʽ��ѯ(ͨ���ʽ��˺źͱ��֣�

        #region �ڻ��ʽ���ϸ��ѯ��ͨ���ʽ��˺źͱ��֣�
        public FuturesCapitalEntity FuturesCapitalFind(string strFundAccountId, Types.CurrencyType currencyType, string userPassword, ref string strErrorMessage)
        {
            FuturesCapitalEntity result = null;
            try
            {
                #region oldCode
                //var capitalAccount = DataRepository.QhCapitalAccountTableProvider.Find(
                //    string.Format("UserAccountDistributeLogo='{0}' AND TradeCurrencyType ='{1}'", strFundAccountId,
                //                  (int)currencyType));
                #endregion
                List<QH_CapitalAccountTableInfo> capitalAccount = QueryQH_CapitalAccountByAccount(strFundAccountId, (QueryType.QueryCurrencyType)((int)currencyType), out strErrorMessage);

                //if (capitalAccount != null && capitalAccount.Count > 0)
                if (!Utils.IsNullOrEmpty(capitalAccount))
                {
                    QH_CapitalAccountTableInfo ca = capitalAccount[0];

                    #region ��ȡ���׻�����������
                    CM_CurrencyType cmCurrencyType = MCService.CommonPara.GetCurrencyTypeByID(ca.TradeCurrencyType);
                    string cName = "";
                    if (cmCurrencyType != null)
                        cName = cmCurrencyType.CurrencyName;
                    #endregion
                    decimal floatProfitLossTotal = 0;//�ֲָܳ���ӯ��
                    decimal marketProfitLossTotal = 0;//�ֲֶܳ���ӯ�� 
                    decimal marketValue = GetMarketValueByQhFundAccount(strFundAccountId, currencyType, out  floatProfitLossTotal, out marketProfitLossTotal);
                    result = new FuturesCapitalEntity(ca, marketValue, cName, floatProfitLossTotal, marketProfitLossTotal);
                }
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                LogHelper.WriteError("�ڻ��ʽ���ϸ��ѯFuturesCapitalFind()�쳣��" + strFundAccountId + "   CurrencyType:" + (int)currencyType, ex);

            }
            return result;
        }
        #endregion

        # region �ֻ��ֲֲ�ѯ��������
        /// <summary>
        /// �ֻ��ֲֲ�ѯ��������
        ///  Update Date:2009-07-15
        ///  Update By:���
        ///  Desc.:�޸Ĳ�����װ������ʽ
        /// </summary>
        /// <param name="_findConditions">��������ʵ�����</param>
        /// <returns></returns>
        string BuildXhHoldQueryWhere(SpotHoldConditionFindEntity _findConditions)
        {
            string result = "1=1 ";

            # region  0.�ɶ�����
            //0.�ɶ�����
            if (!string.IsNullOrEmpty(_findConditions.SpotHoldAccount))
            {
                result += string.Format(" And UserAccountDistributeLogo='{0}'", _findConditions.SpotHoldAccount);
            }
            # endregion

            # region 2.���ָ�ֵ
            if (_findConditions.CurrencyType != 0)
            {
                result += string.Format(" AND CurrencyTypeId='{0}'", _findConditions.CurrencyType);
            }
            # endregion

            # region 3.�ֻ�����
            if (!string.IsNullOrEmpty(_findConditions.SpotCode))
            {
                result += string.Format(" AND Code='{0}'", _findConditions.SpotCode);
            }
            # endregion
            return result;
        }
        # endregion �ֻ��ֲֲ�ѯ��������

        # region ��NEW���ڻ��ֲֲ�ѯ��������
        /// <summary>
        /// �ڻ��ֲֲ�ѯ��������
        /// </summary>
        /// <param name="_findCoditions">��������ʵ�����</param>
        /// <returns></returns>
        string BuildQhHoldQueryWhere(FuturesHoldConditionFindEntity _findCoditions)
        {
            string result = "1=1 ";

            # region  1.�ڻ����ױ���
            if (!string.IsNullOrEmpty(_findCoditions.FuturesHoldAccount))
            {
                result += string.Format(" And  UserAccountDistributeLogo='{0}'", _findCoditions.FuturesHoldAccount);
            }
            # endregion

            # region 2.���ָ�ֵ
            if (_findCoditions.CurrencyType != 0)
            {

                result += string.Format(" AND CurrencyTypeId='{0}'", _findCoditions.CurrencyType);
            }
            # endregion

            # region 3.��Լ����
            if (!string.IsNullOrEmpty(_findCoditions.ContractCode))
            {

                result += string.Format(" AND Contract='{0}'", _findCoditions.ContractCode);
            }
            # endregion
            return result;
        }
        # endregion �ֻ��ֲֲ�ѯ��������

        # region �ֻ��ֲֲ�ѯ �������ʽ��˺š����뼰��ѯ��������ѯ,���в�ѯ�����а���ί�е��ţ�
        /// <summary>
        /// �ֻ��ֲֲ�ѯ �������ʽ��˺š����뼰��ѯ��������ѯ,���в�ѯ�����а���ί�е��ţ�
        ///  Update Date:2009-07-15
        ///  Update By:���
        ///  Desc.:�޸Ĳ������ݲ㷽�������ʵ��
        /// </summary>
        /// <param name="capitalAccount">�ʽ��˺�</param>
        /// <param name="strPassword">����Ա����</param>
        /// <param name="findCondition">��ѯ����ʵ�����</param>
        /// <param name="start"></param>
        /// <param name="pageLength"></param>
        /// <param name="count"></param>
        /// <param name="strErrorMessage"></param>
        /// <returns></returns>
        public List<SpotHoldFindResultEntity> SpotHoldFind(string capitalAccount, string strPassword,
                                                      SpotHoldConditionFindEntity findCondition, int start, int pageLength, out int count, ref string strErrorMessage)
        {
            List<XH_AccountHoldTableInfo> tempt = null;
            List<SpotHoldFindResultEntity> result = new List<SpotHoldFindResultEntity>();
            //SpotHoldFindResultEntity
            count = 0;
            try
            {
                #region ==old code==
                // ���ʽ��˺Ų������Ӧ�ĳֲ��˺�
                ////public static  string GetRealtionAccountIdByAccountId(string strAccountId)
                // string XhHoldAccount = CommonDataAgent.GetRealtionAccountIdByAccountId(capitalAccount);
                #endregion

                #region �����ֻ��ʽ��˻���ȡ�����ĳֲ��˻���Ϣ
                UA_UserAccountAllocationTableInfo userInfo = AccountManager.Instance.GetHoldAccountByCapitalAccount(capitalAccount);
                //UA_UserAccountAllocationTableDal userDal = new UA_UserAccountAllocationTableDal();
                ////�����ֻ��ʽ��˻���ȡ�����ĳֲ��˻���Ϣ
                //UA_UserAccountAllocationTableInfo userInfo = userDal.GetUserHoldAccountByUserCapitalAccount(capitalAccount);
                if (userInfo == null)
                {
                    strErrorMessage = "��ѯ��������˻���Ϣ";
                    return result;
                }
                #endregion

                #region ��װ����
                string whereCondition = "";
                if (findCondition != null)
                {
                    //���ɶ����루�ֲ��˺ţ������ѯ������
                    findCondition.SpotHoldAccount = userInfo.UserAccountDistributeLogo;
                    whereCondition = BuildXhHoldQueryWhere(findCondition);
                    //tempt = DataRepository.XhAccountHoldTableProvider.GetPaged(whereCondition, "AccountHoldLogoId ASC", start, pageLength, out count);
                }
                else
                {
                    whereCondition = string.Format(" UserAccountDistributeLogo='{0}'", userInfo.UserAccountDistributeLogo);
                    //var holdAccount = Common.Utils.GetXHHoldAccountByCapitalAccount(capitalAccount);
                    //tempt = DataRepository.XhAccountHoldTableProvider.GetByUserAccountDistributeLogo(holdAccount.UserAccountDistributeLogo);
                }
                #endregion

                #region ��ҳ��ѯ�����Ϣ
                PagingProceduresInfo prcoInfo = new PagingProceduresInfo();
                if (start <= 1)
                {
                    prcoInfo.IsCount = false;
                }
                {
                    prcoInfo.IsCount = true;
                }
                prcoInfo.PageNumber = start;
                prcoInfo.PageSize = pageLength;
                prcoInfo.Fields = "  AccountHoldLogoId,AvailableAmount,FreezeAmount,UserAccountDistributeLogo,CostPrice,Code,BreakevenPrice,CurrencyTypeId,HoldAveragePrice ";
                prcoInfo.PK = "AccountHoldLogoId";
                prcoInfo.Sort = " AvailableAmount asc ";
                prcoInfo.Tables = " XH_AccountHoldTable ";

                #region ��װ�������
                prcoInfo.Filter = whereCondition;
                #endregion


                #endregion

                XH_AccountHoldTableDal xh_AccHolDal = new XH_AccountHoldTableDal();
                CommonDALOperate<XH_AccountHoldTableInfo> com = new CommonDALOperate<XH_AccountHoldTableInfo>();
                // CommonDALOperate<XH_AccountHoldTableInfo>.DataReaderBind bind = xh_AccHolDal.ReaderBind;
                //tempt = com.PagingQueryProcedures(prcoInfo, out count, bind);
                tempt = com.PagingQueryProcedures(prcoInfo, out count, xh_AccHolDal.ReaderBind);


                foreach (XH_AccountHoldTableInfo _XhAccountHold in tempt)
                {
                    strErrorMessage = "";

                    SpotHoldFindResultEntity sfre = new SpotHoldFindResultEntity();
                    sfre.HoldFindResult = _XhAccountHold;

                    #region ������Ʒ�����ȡ��Ʒ����ʵ��
                    CM_Commodity _CM_Commodity = MCService.CommonPara.GetCommodityByCommodityCode(_XhAccountHold.Code);
                    #endregion

                    # region  ��ȡƷ����𲢸�ֵ
                    //Ʒ����� 
                    #region old code
                    // int BreedClassId = Convert.ToInt32(MCService.CommonPara.GetBreedClassIdByCommodityCode(_XhAccountHold.Code));

                    //sfre.VarietyCategories = BreedClassId.ToString();
                    #endregion

                    if (_CM_Commodity != null)
                    {
                        if (findCondition != null && !string.IsNullOrEmpty(findCondition.VarietyCode))
                        {
                            if (_CM_Commodity.BreedClassID.Value.ToString().Trim() != findCondition.VarietyCode.Trim())
                            {
                                continue;
                            }
                        }
                        sfre.VarietyCategories = _CM_Commodity.BreedClassID.Value.ToString();
                    }

                    # endregion

                    # region  ��ȡ�����г�����ֵ
                    //�����г�
                    CM_BourseType _CM_BourseType = MCService.CommonPara.GetBourseTypeByCommodityCode(_XhAccountHold.Code);
                    if (_CM_BourseType != null)
                    {
                        sfre.BelongMarket = _CM_BourseType.BourseTypeName;
                    }
                    else
                    {
                        strErrorMessage = "������Ʒ����δ��ȡ����Ʒ�����г� ��";
                    }
                    # endregion

                    # region  ��ȡ�ֻ����Ʋ���ֵ
                    //�ֻ�����
                    if (_CM_Commodity != null)
                    {
                        sfre.SpotName = _CM_Commodity.CommodityName;
                    }
                    else
                    {
                        strErrorMessage = "������Ʒ����δ��ȡ���ֻ����� ��";
                    }
                    # endregion

                    # region ��ȡ�������Ʋ���ֵ
                    string _currencyName = MCService.CommonPara.GetCurrencyTypeByID(_XhAccountHold.CurrencyTypeId).CurrencyName;
                    if (!string.IsNullOrEmpty(_currencyName))
                    {
                        sfre.CurrencyName = _currencyName;
                    }
                    else
                    {
                        strErrorMessage = "�ֻ��ֱֲ��еı������ʹ洢����";
                    }
                    # endregion

                    # region ��ȡ�ֲ���������ֵ
                    sfre.HoldSumAmount = Convert.ToInt32(_XhAccountHold.AvailableAmount + _XhAccountHold.FreezeAmount);
                    # endregion

                    # region ��ȡ��ǰ�۲���ֵ
                    HqExData vhe = CommonDataAgent.RealtimeService.GetStockHqData(_XhAccountHold.Code);
                    if (vhe != null)
                    {
                        if (vhe.HqData.Lasttrade == 0)
                        {
                            decimal yesterPrice = MCService.CommonPara.GetClosePriceByCode(_XhAccountHold.Code);
                            if (yesterPrice <= 0)
                            {
                                strErrorMessage = "���ֻ��ʽ�ͳ�ơ��������³ɽ���Ϊ0,�������̼�ҲΪ0,��ǰ��¼ʹ�óֲ־��ۼ���";
                                sfre.RealtimePrice = _XhAccountHold.HoldAveragePrice;
                            }
                            else
                            {
                                strErrorMessage = "���ֻ��ʽ�ͳ�ơ��������³ɽ���Ϊ0,��ǰ��¼ʹ���������̼ۼ���";
                                sfre.RealtimePrice = yesterPrice;
                            }
                            LogHelper.WriteDebug("�ֲ��˺�:" + _XhAccountHold.UserAccountDistributeLogo + " ���룺" + _XhAccountHold.Code + strErrorMessage);
                        }
                        else
                        {
                            sfre.RealtimePrice = Convert.ToDecimal(vhe.HqData.Lasttrade);
                        }
                    }
                    else
                    {
                        decimal yesterPrice = MCService.CommonPara.GetClosePriceByCode(_XhAccountHold.Code);
                        if (yesterPrice <= 0)
                        {
                            strErrorMessage = "���ֻ��ʽ�ͳ�ơ�δ�ܻ�ȡ����,�������̼�ҲΪ0,��ǰ��¼ʹ�óֲ־��ۼ���";
                            sfre.RealtimePrice = _XhAccountHold.HoldAveragePrice;
                        }
                        else
                        {
                            strErrorMessage = "���ֻ��ʽ�ͳ�ơ�δ�ܻ�ȡ����,��ǰ��¼ʹ���������̼ۼ���";
                            sfre.RealtimePrice = yesterPrice;
                        }
                        LogHelper.WriteDebug("�ֲ��˺�:" + _XhAccountHold.UserAccountDistributeLogo + " ���룺" + _XhAccountHold.Code + strErrorMessage);
                    }

                    # endregion

                    #region ������Ʒ�����ȡ��ϣ������׵�λ����λת���ɼƼ۵�λ�ı���
                    //������Ʒ�����ȡ��ϵ�λ
                    Types.UnitType utMatch = MCService.GetMatchUnitType(_XhAccountHold.Code);
                    //���ݴ�ϵ�λת���ɼƼ۵�λ��ȡ��ת���ı���
                    decimal unitMultiple = MCService.GetTradeUnitScale(_XhAccountHold.Code, utMatch);
                    #endregion

                    # region ��ȡ��ֵ����ֵ
                    //��Ϊ�ֲּ�¼���Ǵ�ϵ�λ������������������Ҫת���ɼƼ۵�λ������ȷ
                    //sfre.MarketValue = sfre.HoldSumAmount * sfre.RealtimePrice;
                    sfre.MarketValue = sfre.HoldSumAmount * unitMultiple * sfre.RealtimePrice;
                    # endregion

                    # region ��ȡ����ӯ������ֵ
                    //����ӯ��=�ֲ�����*����ǰ��-�ֲ־��ۣ�
                    //��Ϊ�ֲּ�¼���Ǵ�ϵ�λ������������������Ҫת���ɼƼ۵�λ������ȷ
                    //sfre.FloatProfitLoss = sfre.HoldSumAmount * (sfre.RealtimePrice - _XhAccountHold.HoldAveragePrice);
                    sfre.FloatProfitLoss = sfre.HoldSumAmount * unitMultiple * (sfre.RealtimePrice - _XhAccountHold.HoldAveragePrice);
                    # endregion

                    # region ��ȡ����ԱID����ֵ
                    sfre.TraderId = userInfo.UserID;
                    #region old code
                    //string _userId = string.Empty;
                    //_userId = GetUserIdByTradeAccount(capitalAccount);
                    //if (!string.IsNullOrEmpty(_userId))
                    //{
                    //    sfre.TraderId = _userId;
                    //}
                    //else
                    //    strErrorMessage = "δ��ȡ�����ʽ��˺�����Ӧ�Ľ���ԱID ��";
                    #endregion
                    # endregion

                    # region ��ȡ����ԭ�򲢸�ֵ
                    if (!string.IsNullOrEmpty(strErrorMessage))
                    {
                        string errtxt = strErrorMessage;
                        sfre.ErroReason = errtxt;
                    }
                    # endregion

                    # region ��ȡ����Ų���ֵ����δʵ�֣���ʱ��Ϊ��ֵ��
                    sfre.ErroNumber = string.Empty;
                    # endregion

                    result.Add(sfre);
                }
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
                LogHelper.WriteError("ReckoningCounter.BLL.AccountManagementAndFind.AccountManagementAndFindBLL--SpotHoldFind()��ѯ�쳣", ex);
            }
            return result;
        }

        # endregion �ֻ��ֲֲ�ѯ

        #region ��ȡ��ǰ����۸�
        /// <summary>
        /// ��ȡ��ǰ����۸�
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private decimal ReturnRealtimePrice(string code)
        {
            try
            {
                decimal realtimePrice = 0;
                FutData vte = CommonDataAgent.RealtimeService.GetFutData(code);
                if (vte != null)
                {
                    realtimePrice = Convert.ToDecimal(vte.Lasttrade);

                    if (realtimePrice <= 0) //��������ȡ�ĵ�ǰ��Ϊ0ʱ������ȡ3��
                    {
                        vte = CommonDataAgent.RealtimeService.GetFutData(code);
                        realtimePrice = Convert.ToDecimal(vte.Lasttrade);
                        if (realtimePrice <= 0)
                        {
                            vte = CommonDataAgent.RealtimeService.GetFutData(code);
                            realtimePrice = Convert.ToDecimal(vte.Lasttrade);

                        }
                        if (realtimePrice <= 0)
                        {
                            vte = CommonDataAgent.RealtimeService.GetFutData(code);
                            realtimePrice = Convert.ToDecimal(vte.Lasttrade);

                        }
                    }
                }
                else
                {
                    //������Ϊ��ʱ������ȡ3������
                    vte = CommonDataAgent.RealtimeService.GetFutData(code);
                    if (vte == null)
                    {
                        vte = CommonDataAgent.RealtimeService.GetFutData(code);
                    }
                    if (vte == null)
                    {
                        vte = CommonDataAgent.RealtimeService.GetFutData(code);

                    }
                    if (vte != null)
                    {
                        realtimePrice = Convert.ToDecimal(vte.Lasttrade);

                        if (realtimePrice <= 0) //��������ȡ�ĵ�ǰ��Ϊ0ʱ������ȡ3��
                        {
                            vte = CommonDataAgent.RealtimeService.GetFutData(code);
                            realtimePrice = Convert.ToDecimal(vte.Lasttrade);
                            if (realtimePrice <= 0)
                            {
                                vte = CommonDataAgent.RealtimeService.GetFutData(code);
                                realtimePrice = Convert.ToDecimal(vte.Lasttrade);

                            }
                            if (realtimePrice <= 0)
                            {
                                vte = CommonDataAgent.RealtimeService.GetFutData(code);
                                realtimePrice = Convert.ToDecimal(vte.Lasttrade);

                            }
                        }
                    }
                }
                return realtimePrice;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.Message, ex);
                return 0;
            }

        }
        #endregion

        # region �ڻ��ֲֲ�ѯ �������ʽ��˺š����뼰��ѯ��������ѯ,���в�ѯ�����а���ί�е��ţ�
        /// <summary>
        /// �ڻ��ֲֲ�ѯ �������ʽ��˺š����뼰��ѯ��������ѯ,���в�ѯ�����а���ί�е��ţ�
        /// </summary>
        /// <param name="capitalAccount">�ʽ��˺�</param>
        /// <param name="strPassword">����Ա����</param>
        /// <param name="findCondition">��ѯ����ʵ�����</param>
        /// <param name="start"></param>
        /// <param name="pageLength"></param>
        /// <param name="count"></param>
        /// <param name="strErrorMessage"></param>
        /// <returns></returns>
        public List<FuturesHoldFindResultEntity> FuturesHoldFind(string capitalAccount, string strPassword,
                                                      FuturesHoldConditionFindEntity findCondition, int start, int pageLength, out int count, ref string strErrorMessage)
        {
            strErrorMessage = string.Empty;
            string format = "�ڻ��ֲֲ�ѯ[capitalAccount={0},strPassword={1},findCondition={2}]-������Ϣ��";
            string desc = string.Format(format, capitalAccount, strPassword, findCondition);
            List<QH_HoldAccountTableInfo> tempt = null;
            var result = new List<FuturesHoldFindResultEntity>();

            count = 0;
            try
            {
                #region ==old code==
                // ���ʽ��˺Ų������Ӧ�ĳֲ��˺�
                //public static  string GetRealtionAccountIdByAccountId(string strAccountId)
                //string QhHoldAccount = CommonDataAgent.GetRealtionAccountIdByAccountId(capitalAccount);
                #endregion

                #region �����ֻ��ʽ��˻���ȡ�����ĳֲ��˻���Ϣ
                UA_UserAccountAllocationTableInfo userInfo = AccountManager.Instance.GetHoldAccountByCapitalAccount(capitalAccount);
                //UA_UserAccountAllocationTableDal userDal = new UA_UserAccountAllocationTableDal();
                ////�����ֻ��ʽ��˻���ȡ�����ĳֲ��˻���Ϣ
                //UA_UserAccountAllocationTableInfo userInfo = userDal.GetUserHoldAccountByUserCapitalAccount(capitalAccount);
                if (userInfo == null)
                {
                    strErrorMessage = "��ѯ��������˻���Ϣ";
                    return result;
                }
                #endregion

                #region ��װ��ѯ�������
                //�����ױ��루�ֲ��˺ţ������ѯ������
                string whereCondition = "";
                if (findCondition != null)
                {
                    //���ɶ����루�ֲ��˺ţ������ѯ������
                    findCondition.FuturesHoldAccount = userInfo.UserAccountDistributeLogo;
                    whereCondition = BuildQhHoldQueryWhere(findCondition);
                }
                else
                {
                    whereCondition = string.Format(" UserAccountDistributeLogo='{0}'", userInfo.UserAccountDistributeLogo);
                }
                #endregion

                #region ��ҳ��ѯ�����Ϣ
                PagingProceduresInfo prcoInfo = new PagingProceduresInfo();
                if (start <= 1)
                {
                    prcoInfo.IsCount = false;
                }
                {
                    prcoInfo.IsCount = true;
                }
                prcoInfo.PageNumber = start;
                prcoInfo.PageSize = pageLength;
                prcoInfo.Fields = "  AccountHoldLogoId,HistoryHoldAmount,HistoryFreezeAmount,HoldAveragePrice,TodayHoldAmount,TradeCurrencyType,TodayHoldAveragePrice,UserAccountDistributeLogo,BuySellTypeId,TodayFreezeAmount,Contract,CostPrice,BreakevenPrice,Margin,ProfitLoss,OpenAveragePrice ";
                prcoInfo.PK = "AccountHoldLogoId";
                prcoInfo.Sort = " TodayHoldAmount asc ";
                prcoInfo.Tables = " QH_HoldAccountTable ";

                #region ��װ�������
                prcoInfo.Filter = whereCondition;
                #endregion


                #endregion

                #region ִ�в�ѯ
                QH_HoldAccountTableDal xh_AccHolDal = new QH_HoldAccountTableDal();
                CommonDALOperate<QH_HoldAccountTableInfo> com = new CommonDALOperate<QH_HoldAccountTableInfo>();
                //CommonDALOperate<QH_HoldAccountTableInfo>.DataReaderBind bind = xh_AccHolDal.ReaderBind;
                //tempt = com.PagingQueryProcedures(prcoInfo, out count, bind);
                tempt = com.PagingQueryProcedures(prcoInfo, out count, xh_AccHolDal.ReaderBind);

                //  tempt = DataRepository.QhHoldAccountTableProvider.GetPaged(whereCondition, "AccountHoldLogoId ASC", start, pageLength, out count);
                if (tempt.Count == 0)
                {
                    strErrorMessage = "�˽���Ա�ĳֲ��ʻ���¼������.";
                    LogHelper.WriteDebug(desc + strErrorMessage);
                    return null;
                }
                #endregion

                #region ������װ����������ݶ��� ��������صĲ�ѯ��Ӧ�����ļ�¼ ��Ʒ�����
                foreach (QH_HoldAccountTableInfo _QhAccountHold in tempt)
                {
                    strErrorMessage = "";
                    FuturesHoldFindResultEntity sfre = new FuturesHoldFindResultEntity();

                    sfre.HoldFindResult = _QhAccountHold;

                    #region ������Ʒ�����ȡ��Ʒ����ʵ��
                    CM_Commodity _CM_Commodity = MCService.CommonPara.GetCommodityByCommodityCode(_QhAccountHold.Contract);
                    #endregion

                    # region  ��ȡƷ����𲢸�ֵ
                    //Ʒ����� 
                    #region old code
                    //int BreedClassId = Convert.ToInt32(MCService.CommonPara.GetBreedClassIdByCommodityCode(_QhAccountHold.Contract));
                    //sfre.VarietyCategories = BreedClassId.ToString();
                    #endregion
                    if (_CM_Commodity != null)
                    {
                        if (findCondition != null && !string.IsNullOrEmpty(findCondition.VarietyCode))
                        {
                            if (_CM_Commodity.BreedClassID.Value.ToString().Trim() != findCondition.VarietyCode.Trim())
                            {
                                continue;
                            }
                        }
                        sfre.VarietyCategories = _CM_Commodity.BreedClassID.Value.ToString();
                    }
                    # endregion

                    # region  ��ȡ�����г�����ֵ
                    //�����г�
                    CM_BourseType _CM_BourseType = MCService.CommonPara.GetBourseTypeByCommodityCode(_QhAccountHold.Contract);
                    if (_CM_BourseType != null)
                    {
                        sfre.BelongMarket = _CM_BourseType.BourseTypeName;
                    }
                    else
                    {
                        strErrorMessage = "������Ʒ����δ��ȡ����Ʒ�����г� ��";
                        LogHelper.WriteDebug(desc + strErrorMessage);
                    }
                    # endregion

                    # region  ��ȡ��Լ���Ʋ���ֵ
                    //CM_Commodity _CM_Commodity = MCService.CommonPara.GetCommodityByCommodityCode(_QhAccountHold.Contract);
                    if (_CM_Commodity != null)
                    {
                        sfre.ContractName = _CM_Commodity.CommodityName;
                    }
                    else
                    {
                        strErrorMessage = "������Ʒ����δ��ȡ���ֻ����� ��";
                        LogHelper.WriteDebug(desc + strErrorMessage);

                    }
                    # endregion

                    # region ��ȡ�������Ʋ���ֵ
                    string _currencyName = MCService.CommonPara.GetCurrencyTypeByID(_QhAccountHold.TradeCurrencyType).CurrencyName;
                    if (!string.IsNullOrEmpty(_currencyName))
                    {
                        sfre.CurrencyName = _currencyName;
                    }
                    else
                    {
                        strErrorMessage = "�ڻ��ֱֲ��еı������ʹ洢����";
                        LogHelper.WriteDebug(desc + strErrorMessage);
                    }
                    # endregion

                    # region ��ȡ�񿪲�������ֵ
                    sfre.HoldSumAmount = Convert.ToInt32(_QhAccountHold.TodayHoldAmount);
                    # endregion

                    # region ��ȡ�ֲ���������ֵ
                    //if (_QhAccountHold.BuySellTypeId ==1)
                    //{
                    //    sfre.HoldSumAmount = Convert.ToInt32(_QhAccountHold.HistoryHoldAmount + _QhAccountHold.HistoryFreezeAmount + _QhAccountHold.TodayHoldAmount  + _QhAccountHold.TodayFreezeAmount);
                    //}
                    sfre.HoldSumAmount = Convert.ToInt32(_QhAccountHold.HistoryHoldAmount + _QhAccountHold.HistoryFreezeAmount + _QhAccountHold.TodayHoldAmount + _QhAccountHold.TodayFreezeAmount);
                    # endregion

                    # region ��ȡ������������ֵ
                    sfre.AvailableTotalAmount = Convert.ToInt32(_QhAccountHold.HistoryHoldAmount + _QhAccountHold.TodayHoldAmount);
                    # endregion

                    # region ��ȡ������������ֵ
                    sfre.FreezeTotalAmount = Convert.ToInt32(_QhAccountHold.HistoryFreezeAmount + _QhAccountHold.TodayFreezeAmount);
                    # endregion

                    # region ��ȡ��ǰ�۲���ֵ
                    //��ȡ��ָ�ڻ�����
                    //VTFutData GetFutData(string strCode);
                    //VTFutData vte = CommonDataAgent.RealtimeService.GetFutData(_QhAccountHold.Contract);
                    //if (vte != null)
                    //{
                    //    sfre.RealtimePrice = Convert.ToDecimal(vte.Lasttrade);
                    //}
                    //else
                    //    strErrorMessage = "δ��ȡ�����ڻ���������飡";

                    //decimal realtimePrice = ReturnRealtimePrice(_QhAccountHold.Contract);
                    int? breedID = MCService.CommonPara.GetBreedClassTypeEnumByCommodityCode(_QhAccountHold.Contract);
                    //������
                    double preSettlementPrice = 0;
                    //������
                    double settlementPrice = 0;
                    //���¼�
                    double lasttrade = 0;
                    bool isGetRealtime = false;
                    if (breedID.HasValue)
                    {
                        switch ((CommonObject.Types.BreedClassTypeEnum)breedID.Value)
                        {

                            case GTA.VTS.Common.CommonObject.Types.BreedClassTypeEnum.StockIndexFuture:
                                FutData vte = CommonDataAgent.RealtimeService.GetFutData(_QhAccountHold.Contract);
                                if (vte == null)
                                {
                                    strErrorMessage = "����ָ�ڻ��ʽ�ͳ�ơ�δ��ȡ���ù�ָ�ڻ����������,��ǰ��¼ʹ�óֲ־��ۼ���.";
                                    LogHelper.WriteDebug("�ֲ��˺�:" + _QhAccountHold.UserAccountDistributeLogo + " ���룺" + _QhAccountHold.Contract + strErrorMessage);
                                    sfre.RealtimePrice = _QhAccountHold.HoldAveragePrice;
                                }
                                else
                                {
                                    preSettlementPrice = vte.PreSettlementPrice;
                                    settlementPrice = vte.SettlementPrice;
                                    lasttrade = vte.Lasttrade;
                                    isGetRealtime = true;
                                }
                                break;
                            case GTA.VTS.Common.CommonObject.Types.BreedClassTypeEnum.CommodityFuture:
                                MerFutData mfvte = CommonDataAgent.RealtimeService.GetMercantileFutData(_QhAccountHold.Contract);
                                if (mfvte == null)
                                {
                                    strErrorMessage = "����Ʒ�ڻ��ʽ�ͳ�ơ�δ��ȡ������Ʒ�ڻ����������,��ǰ��¼ʹ�óֲ־��ۼ���.";
                                    LogHelper.WriteDebug("�ֲ��˺�:" + _QhAccountHold.UserAccountDistributeLogo + " ���룺" + _QhAccountHold.Contract + strErrorMessage);
                                    sfre.RealtimePrice = _QhAccountHold.HoldAveragePrice;
                                }
                                else
                                {
                                    preSettlementPrice = mfvte.PreClearPrice;
                                    settlementPrice = mfvte.ClearPrice;
                                    lasttrade = mfvte.Lasttrade;
                                    isGetRealtime = true;
                                }
                                break;
                        }

                    }


                    if (isGetRealtime)
                    {
                        int k = MCService.IsNowTimeMarket(Types.BreedClassTypeEnum.StockIndexFuture, _QhAccountHold.Contract);

                        switch (k)
                        {
                            case 0:
                                #region ��ǰʱ��00��00��00�����տ���ǰ
                                if (preSettlementPrice == 0)
                                {
                                    strErrorMessage = "���ڻ��ʽ�ͳ�ơ���ȡ���ڻ�������������ս����Ϊ0,��ǰ��¼ʹ�óֲ־��ۼ���";
                                    LogHelper.WriteDebug("�ֲ��˺�:" + _QhAccountHold.UserAccountDistributeLogo + " ���룺" + _QhAccountHold.Contract + strErrorMessage);
                                    sfre.RealtimePrice = _QhAccountHold.HoldAveragePrice;
                                }
                                else
                                {
                                    //����� PreSettlementPrice  //����� SettlementPrice
                                    sfre.RealtimePrice = Convert.ToDecimal(preSettlementPrice);
                                }
                                #endregion
                                break;
                            case 1:
                                #region ��ǰʱ�俪������������
                                if (lasttrade == 0)
                                {
                                    strErrorMessage = "���ڻ��ʽ�ͳ�ơ���ȡ���ڻ�������������³ɽ���Ϊ0,��ǰ��¼ʹ�óֲ־��ۼ���";
                                    LogHelper.WriteDebug("�ֲ��˺�:" + _QhAccountHold.UserAccountDistributeLogo + " ���룺" + _QhAccountHold.Contract + strErrorMessage);
                                    sfre.RealtimePrice = _QhAccountHold.HoldAveragePrice;
                                }
                                else
                                {
                                    //����� PreSettlementPrice  //����� SettlementPrice
                                    sfre.RealtimePrice = Convert.ToDecimal(lasttrade);
                                }
                                #endregion
                                break;
                            case 2:
                                #region �������̺�������23��59��59--2
                                if (settlementPrice == 0)
                                {
                                    strErrorMessage = "���ڻ��ʽ�ͳ�ơ���ȡ���ڻ������������ս����Ϊ0,��ǰ��¼ʹ�óֲ־��ۼ���";
                                    LogHelper.WriteDebug("�ֲ��˺�:" + _QhAccountHold.UserAccountDistributeLogo + " ���룺" + _QhAccountHold.Contract + strErrorMessage);
                                    sfre.RealtimePrice = _QhAccountHold.HoldAveragePrice;
                                }
                                else
                                {
                                    //����� PreSettlementPrice  //����� SettlementPrice
                                    sfre.RealtimePrice = Convert.ToDecimal(settlementPrice);
                                }
                                #endregion
                                break;
                        }
                    }

                    //if (vte != null)
                    //{
                    //    if (vte.Lasttrade == 0)
                    //    {
                    //        strErrorMessage = "���ڻ��ʽ�ͳ�ơ���ȡ���ڻ�������������³ɽ���Ϊ0,��ǰ��¼ʹ�óֲ־��ۼ���";
                    //        sfre.RealtimePrice = _QhAccountHold.HoldAveragePrice;
                    //        LogHelper.WriteDebug("�ֲ��˺�:" + _QhAccountHold.UserAccountDistributeLogo + " ���룺" + _QhAccountHold.Contract + strErrorMessage);

                    //    }
                    //    else
                    //    {
                    //        sfre.RealtimePrice = Convert.ToDecimal(vte.Lasttrade);
                    //    }
                    //}
                    //else
                    //{
                    //    strErrorMessage = "���ڻ��ʽ�ͳ�ơ�δ��ȡ�����ڻ����������,��ǰ��¼ʹ�óֲ־��ۼ���.";
                    //    sfre.RealtimePrice = _QhAccountHold.HoldAveragePrice;
                    //    LogHelper.WriteDebug("�ֲ��˺�:" + _QhAccountHold.UserAccountDistributeLogo + " ���룺" + _QhAccountHold.Contract + strErrorMessage);
                    //}
                    # endregion

                    # region ��ȡ���ս���۲���ֵ
                    if (isGetRealtime)
                    {
                        sfre.YesterdayClearingPrice = Convert.ToDecimal(preSettlementPrice);//Yclose);
                    }
                    else
                    {
                        strErrorMessage = "�ڻ�δ��ȡ�����ڻ����������--���ս���ۣ�";
                        LogHelper.WriteDebug(desc + strErrorMessage);
                    }
                    # endregion

                    # region ��ȡ�������򲢸�ֵ
                    if (_QhAccountHold.BuySellTypeId == 1)
                        sfre.BuySellDirection = "��";
                    else
                        sfre.BuySellDirection = "��";
                    # endregion

                    //# region ��ȡ����ӯ��
                    // sfre.CloseMarketProfitLoss = sfre.HoldSumAmount * (sfre.RealtimePrice - sfre.HoldAveragePrice);
                    //# endregion

                    # region ��ȡ����ӯ��������ӯ������ֵ
                    //sfre.CloseFloatProfitLoss = sfre.HoldSumAmount * (sfre.RealtimePrice - _QhAccountHold.CostPrice.Value);

                    #region ������Ʒ�����ȡ��ϣ������׵�λ����λת���ɼƼ۵�λ�ı���
                    //�Ѵ�ϣ������ף���λ�ֲ�����ת��Ϊ�Ƽ۵�λ�ĳֲ���������Ϊ֮ǰ�洢�����ݿ��е���ֲ����й�
                    //�Ķ��ǽ��׵�λ�����۸���صĶ��ǼƼ۵�λ��
                    decimal scale = MCService.GetFutureTradeUntiScale(_QhAccountHold.Contract);
                    #endregion

                    //����Ķ���ӯ��
                    string buyingMarketProfitLoss = "����Ķ���ӯ��=[�ֲ�����={0}*(��ǰ��={1}-�ֲ־���={2})*���׵�λ����={3}]";
                    //����ĸ���ӯ��
                    string buyingFloatProfitLoss = "����ĸ���ӯ��=[�ֲ�����={0}*(��ǰ��={1}-���־���={2})*���׵�λ����={3}]";
                    //������Ķ���ӯ��
                    string sellMarketProfitLoss = "������Ķ���ӯ��=[�ֲ�����={0}*(�ֲ־���={1}-��ǰ��={2})*���׵�λ����={3}]";
                    //������ĸ���ӯ��
                    string sellFloatProfitLoss = "������ĸ���ӯ��=[�ֲ�����={0}*(���־���={1}-��ǰ��={2})*���׵�λ����={3}]";
                    //����Ƽ۳ֲ�����
                    decimal computeTotal = sfre.HoldSumAmount * scale;

                    if (_QhAccountHold.BuySellTypeId == (int)CommonObject.Types.TransactionDirection.Buying)
                    {
                        //sfre.CloseMarketProfitLoss = sfre.HoldSumAmount * (sfre.RealtimePrice - sfre.HoldAveragePrice) * scale;
                        //sfre.FloatProfitLoss = sfre.HoldSumAmount * (sfre.RealtimePrice - _QhAccountHold.CostPrice) * scale;
                        // ����Ķ���ӯ��=����Ķ���ӯ��=[�ֲ�����={0}*(��ǰ��={1}-�ֲ־���={2})*���׵�λ����={3}]
                        //sfre.MarketProfitLoss = computeTotal * (sfre.RealtimePrice - _QhAccountHold.HoldAveragePrice) * scale;
                        //���������Ѿ�תΪ�Ƽ۵�λ�������Բ����ٳ��Ա���
                        sfre.MarketProfitLoss = computeTotal * (sfre.RealtimePrice - _QhAccountHold.HoldAveragePrice);
                        //����ĸ���ӯ��=����ĸ���ӯ��=[�ֲ�����={0}*(��ǰ��={1}-���־���={2})*���׵�λ����={3}]
                        sfre.FloatProfitLoss = computeTotal * (sfre.RealtimePrice - _QhAccountHold.OpenAveragePrice);

                        //��ӡ����Ķ���ӯ��
                        string buyMarketP = string.Format(buyingMarketProfitLoss, computeTotal, sfre.RealtimePrice, _QhAccountHold.HoldAveragePrice, scale);
                        LogHelper.WriteDebug(buyMarketP);
                        //��ӡ����ĸ���ӯ��
                        string buyFloatP = string.Format(buyingFloatProfitLoss, computeTotal, sfre.RealtimePrice, _QhAccountHold.OpenAveragePrice, scale);
                        LogHelper.WriteDebug(buyFloatP);
                    }
                    else
                    {
                        //������Ķ���ӯ��=������Ķ���ӯ��=[�ֲ�����={0}*(�ֲ־���={1}-��ǰ��={2})*���׵�λ����={3}]
                        sfre.MarketProfitLoss = computeTotal * (_QhAccountHold.HoldAveragePrice - sfre.RealtimePrice);
                        //������ĸ���ӯ��=������ĸ���ӯ��=[�ֲ�����={0}*(���־���={1}-��ǰ��={2})*���׵�λ����={3}]
                        sfre.FloatProfitLoss = computeTotal * (_QhAccountHold.OpenAveragePrice - sfre.RealtimePrice);

                        //��ӡ������Ķ���ӯ��
                        string sellMarketP = string.Format(sellMarketProfitLoss, computeTotal, _QhAccountHold.HoldAveragePrice, sfre.RealtimePrice, scale);
                        LogHelper.WriteDebug(sellMarketP);
                        //��ӡ������ĸ���ӯ��
                        string sellFloatP = string.Format(sellFloatProfitLoss, computeTotal, _QhAccountHold.OpenAveragePrice, sfre.RealtimePrice, scale);
                        LogHelper.WriteDebug(sellFloatP);

                    }

                    # endregion

                    # region ��ȡ�ֲ־��۲���ֵ
                    sfre.HoldAveragePrice = _QhAccountHold.HoldAveragePrice;
                    # endregion

                    # region ��ȡ���ղ־��۲���ֵ
                    sfre.TodayOpenAveragePrice = _QhAccountHold.TodayHoldAveragePrice;
                    # endregion

                    # region ��ȡ����ԱID����ֵ
                    string _userId = string.Empty;
                    _userId = GetUserIdByTradeAccount(capitalAccount);
                    if (!string.IsNullOrEmpty(_userId))
                    {
                        sfre.TraderId = _userId;
                    }
                    else
                    {
                        strErrorMessage = "δ��ȡ�����ʽ��˺�����Ӧ�Ľ���ԱID ��";
                        LogHelper.WriteDebug(desc + strErrorMessage);
                    }
                    # endregion

                    # region ��ȡ����ԭ�򲢸�ֵ
                    if (!string.IsNullOrEmpty(strErrorMessage))
                    {
                        string errtxt = strErrorMessage;
                        sfre.ErroReason = errtxt;
                    }
                    # endregion

                    # region ��ȡ����Ų���ֵ����δʵ�֣���ʱ��Ϊ��ֵ��
                    sfre.ErroNumber = string.Empty;
                    # endregion

                    #region �ɱ���,��֤��,���տ�����,������(liushuwei)
                    //�ɱ���
                    sfre.CostPrice = Convert.ToDecimal(_QhAccountHold.CostPrice);
                    //��֤��
                    sfre.MarginAmount = Convert.ToDecimal(_QhAccountHold.Margin);
                    //���տ�����
                    sfre.TodayAOpenAmount = Convert.ToInt32(_QhAccountHold.TodayHoldAmount);
                    //������
                    sfre.BreakevenPrice = Convert.ToDecimal(_QhAccountHold.BreakevenPrice);
                   
                    #endregion
                    //���׵�λ�Ƽ۵�λ���� add by ���� 2010-05-04
                    sfre.UnitMultiple = scale;

                    result.Add(sfre);
                }
                #endregion
            }
            catch (Exception ex)
            {
                strErrorMessage = "�ڻ��ֲֲ�ѯ,����ԭ��" + ex.ToString();
                // LogHelper.WriteDebug(desc + strErrorMessage);
                LogHelper.WriteError(strErrorMessage, ex);
            }
            return result;
        }

        # endregion �ֻ��ֲֲ�ѯ

        # region �����ʽ���ϸ��ѯ
        /// <summary>
        /// �����ʽ���ϸ��ѯ
        /// </summary>
        /// <param name="userId">����ԱID</param>
        /// <param name="bankAccount">�����˺�</param>
        /// <param name="outMessage">�������</param>
        /// <returns></returns>
        public List<UA_BankAccountTableInfo> BankCapitalFind(string userId, string bankAccount, out string outMessage)
        {
            List<UA_BankAccountTableInfo> list = null;
            outMessage = null;
            try
            {
                string _bankAccount = string.Empty;
                // ͨ������ԱID��øý���Ա�������˺�
                _bankAccount = GetBankAccountByUserId(userId);
                if (string.IsNullOrEmpty(bankAccount) || bankAccount == _bankAccount)
                {
                    #region old code
                    //string whereCondition = string.Format("UserAccountDistributeLogo ='{0}'", _bankAccount);
                    //result = DataRepository.UaBankAccountTableProvider.GetPaged(whereCondition, "TradeCurrencyTypeLogo ASC", 0, 15, out count);
                    #endregion
                    UA_BankAccountTableDal dal = new UA_BankAccountTableDal();
                    list = dal.GetListArray(string.Format("UserAccountDistributeLogo ='{0}'", _bankAccount));
                }
                else
                {
                    outMessage = "�Բ��𣬲�ѯʧ�ܣ�ʧ��ԭ��Ϊ������Ա�������˺����벻��ȷ ��";
                }
            }
            catch (Exception ex)
            {
                outMessage = ex.ToString();
            }
            return list;
        }
        # endregion �����ʽ���ϸ��ѯ

        # region  ��NEW�������ֻ��ʽ��˺��µ��ʲ����ܲ�ѯ�������ʽ��˺Ų�ѯ��
        /// <summary>
        /// �����ֻ��ʽ��˺��µ��ʲ����ܲ�ѯ�������ʽ��˺Ų�ѯ��
        /// Update By:���
        /// Update Date:2009-07-15
        /// Desc.:�޸���صĲ����߼����������
        /// </summary>
        /// <returns></returns>
        public List<AssetSummaryEntity> SpotAssetSummaryFind(string capitalAccount, string strPassword, out string strErrorMessage)
        {
            #region ��������
            List<AssetSummaryEntity> list = new List<AssetSummaryEntity>();
            strErrorMessage = string.Empty;
            AssetSummaryEntity temp = new AssetSummaryEntity();
            #endregion

            # region try....catch ���
            try
            {
                #region �����ֻ��ʽ��˻���ȡ�����ĳֲ��˻���Ϣ
                UA_UserAccountAllocationTableInfo userInfo = AccountManager.Instance.GetHoldAccountByCapitalAccount(capitalAccount);
                //UA_UserAccountAllocationTableDal userDal = new UA_UserAccountAllocationTableDal();
                //UA_UserAccountAllocationTableInfo userInfo = userDal.GetUserHoldAccountByUserCapitalAccount(capitalAccount);
                if (userInfo == null)
                {
                    strErrorMessage = "��ѯ������Ӧ�ʽ��˺�����Ӧ�Ľ���Ա�����Ϣ��";
                    return list;
                }
                #endregion

                #region �����ֻ��ʽ��˺ŷ����ֻ��ʽ��˺���ӵ�е��ʽ���Ϣ
                List<XH_CapitalAccountTableInfo> xh_AccountInfo_list = QueryXH_CapitalAccountByAccount(capitalAccount, QueryType.QueryCurrencyType.ALL, out strErrorMessage);
                #endregion

                #region ��ȡ���׻���Ϊ��RMB��������ֵ���ܸ���ӯ��(δʵ��ӯ��)
                decimal RMBSum;
                decimal floatRMBSum;
                RMBSum = GetMarketValueByXH_HoldAccount(userInfo.UserAccountDistributeLogo, Types.CurrencyType.RMB, out floatRMBSum);
                #endregion

                #region ��ȡ���׻���Ϊ��HK��������ֵ���ܸ���ӯ��(δʵ��ӯ��)
                decimal HKSum;
                decimal floatHKSum;
                HKSum = GetMarketValueByXH_HoldAccount(userInfo.UserAccountDistributeLogo, Types.CurrencyType.HK, out floatHKSum);
                #endregion

                #region ��ȡ���׻���Ϊ��US��������ֵ���ܸ���ӯ��(δʵ��ӯ��)
                decimal USSum;
                decimal floatUSSum;
                USSum = GetMarketValueByXH_HoldAccount(userInfo.UserAccountDistributeLogo, Types.CurrencyType.US, out floatUSSum);
                #endregion

                #region �����ʽ��˻���Ϣ��ѯ��ؽ��׻��ҷ����������ʲ�
                foreach (XH_CapitalAccountTableInfo item in xh_AccountInfo_list)
                {
                    switch ((Types.CurrencyType)item.TradeCurrencyType)
                    {
                        case Types.CurrencyType.RMB:
                            {
                                temp.RMBAvailable = RMBSum + item.AvailableCapital;
                                temp.RMBFreeze = item.FreezeCapitalTotal;
                                //// RMBδʵ��ӯ��(����ӯ��)=�������гֱֲ��еĸ���ӯ��
                                //temp.RMBNotDoneProfitLossTotal = floatRMBSum;
                                ////RMB��ӯ��=�ۼ���ʵ��ӯ��+δʵ��ӯ��
                                //temp.RMBProfitLossTotal = floatRMBSum + item.HasDoneProfitLossTotal;
                            }
                            break;
                        case Types.CurrencyType.HK:
                            {
                                temp.HKAvailable = HKSum + item.AvailableCapital;
                                temp.HKFreeze = item.FreezeCapitalTotal;
                                ////HKδʵ��ӯ��(����ӯ��)=�������гֱֲ��еĸ���ӯ��
                                //temp.HKNotDoneProfitLossTotal = floatHKSum;
                                ////HK��ӯ��=�ۼ���ʵ��ӯ��+δʵ��ӯ��
                                //temp.HKProfitLossTotal = floatHKSum + item.HasDoneProfitLossTotal;
                            }
                            break;
                        case Types.CurrencyType.US:
                            {
                                temp.USAvailable = USSum + item.AvailableCapital;
                                temp.USFreeze = item.FreezeCapitalTotal;
                                ////USδʵ��ӯ��(����ӯ��)=�������гֱֲ��еĸ���ӯ��
                                //temp.USNotDoneProfitLossTotal = floatUSSum;
                                ////US��ӯ��=�ۼ���ʵ��ӯ��+δʵ��ӯ��
                                //temp.USProfitLossTotal = floatUSSum + item.HasDoneProfitLossTotal;
                            }
                            break;
                        default:
                            break;
                    }

                }
                #endregion
                list.Add(temp);

                #region old Code

                ////SpotCapitalEntity _SpotCapitalEntity= new SpotCapitalEntity( );
                //var temp = new AssetSummaryEntity();
                ////temp.CapitalAccount = capitalAccount;

                ////1.��ø��ʽ��˺��µ�RMB(����ң��ĳֲ���ֵ
                //decimal RMBSum;
                //decimal floatRMBSum;//����ӯ��
                //RMBSum = GetMarketValueByXhFundAccount(capitalAccount, Types.CurrencyType.RMB, out floatRMBSum);

                ////�ֻ��ʽ���ϸ��ѯ(ͨ���ʽ��˺źͱ��֣�
                ////public SpotCapitalEntity SpotCapitalFind(string strFundAccountId,Types.CurrencyType currencyType, string userPassword,ref string strErrorMessage)
                //var _capitalAccountRMB = DataRepository.XhCapitalAccountTableProvider.Find(
                //    string.Format("UserAccountDistributeLogo='{0}' AND TradeCurrencyType ='{1}'", capitalAccount, 1));
                //decimal rmb = _capitalAccountRMB[0].AvailableCapital.Value;
                //temp.RMBAvailable = RMBSum + rmb;
                //temp.RMBFreeze = _capitalAccountRMB[0].FreezeCapitalTotal.Value;

                ////2.��ø��ʽ��˺��µ�HK���۱ң��ĳֲ���ֵ
                //decimal HKSum;
                //decimal floatHKSum;//����ӯ��
                //HKSum = GetMarketValueByXhFundAccount(capitalAccount, Types.CurrencyType.HK, out floatHKSum);
                //var _capitalAccountHK = DataRepository.XhCapitalAccountTableProvider.Find(
                //   string.Format("UserAccountDistributeLogo='{0}' AND TradeCurrencyType ='{1}'", capitalAccount, 2));
                //decimal hk = _capitalAccountHK[0].AvailableCapital.Value;
                //temp.HKAvailable = HKSum + hk;
                //temp.HKFreeze = _capitalAccountHK[0].FreezeCapitalTotal.Value;

                ////3.��ø��ʽ��˺��µ�US���۱ң��ĳֲ���ֵ
                //decimal USSum;
                //decimal floatUSSum;//����ӯ��
                //USSum = GetMarketValueByXhFundAccount(capitalAccount, Types.CurrencyType.US, out floatUSSum);
                //var _capitalAccountUS = DataRepository.XhCapitalAccountTableProvider.Find(
                //   string.Format("UserAccountDistributeLogo='{0}' AND TradeCurrencyType ='{1}'", capitalAccount, 3));
                //decimal US = _capitalAccountUS[0].AvailableCapital.Value;
                //temp.USAvailable = USSum + US;
                //temp.USFreeze = _capitalAccountUS[0].FreezeCapitalTotal.Value;

                //list.Add(temp);
                #endregion
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.ToString();
            }
            # endregion try....catch ���
            return list;
        }
        # endregion

        # region  ��NEW�������ڻ��ʽ��˺��µ��ʲ����ܲ�ѯ�������ʽ��˺Ų�ѯ��
        /// <summary>
        /// �����ڻ��ʽ��˺��µ��ʲ����ܲ�ѯ�������ʽ��˺Ų�ѯ��
        /// Update By:���
        /// Update Date:2009-07-15
        /// Desc.:�޸���صĲ����߼����������
        /// </summary>
        /// <param name="capitalAccount">�ڻ��ʽ��˺�</param>
        /// <param name="strErrorMessage">ͳ���쳣</param>
        /// <param name="strPassword">�û�����</param>
        /// <returns></returns>
        public List<AssetSummaryEntity> FuturesAssetSummaryFind(string capitalAccount, string strPassword, out string strErrorMessage)
        {
            #region ��������
            List<AssetSummaryEntity> list = new List<AssetSummaryEntity>();
            strErrorMessage = string.Empty;
            AssetSummaryEntity temp = new AssetSummaryEntity();
            #endregion

            # region try....catch ���
            try
            {

                #region �����ڻ��ʽ��˻���ȡ�����ĳֲ��˻���Ϣ
                #region �ӻ����л�ȡ�û��˺���Ϣ
                //UA_UserAccountAllocationTableDal userDal = new UA_UserAccountAllocationTableDal();
                //UA_UserAccountAllocationTableInfo userInfo = userDal.GetUserHoldAccountByUserCapitalAccount(capitalAccount);
                UA_UserAccountAllocationTableInfo userInfo = AccountManager.Instance.GetHoldAccountByCapitalAccount(capitalAccount);
                #endregion
                if (userInfo == null)
                {
                    strErrorMessage = "��ѯ������Ӧ�ʽ��˺�����Ӧ�Ľ���Ա�����Ϣ��";
                    return list;
                }
                #endregion

                #region �����ڻ��ʽ��˺ŷ����ڻ��ʽ��˺���ӵ�е��ʽ���Ϣ �����ڲ������Ѿ�ʵ�ִӻ����л�ȡ����
                List<QH_CapitalAccountTableInfo> qh_AccountInfo_list = QueryQH_CapitalAccountByAccount(capitalAccount, QueryType.QueryCurrencyType.ALL, out strErrorMessage);
                #endregion

                #region ��ȡ���׻���Ϊ��RMB��������ֵ���ܸ���ӯ�����ܶ���ӯ��
                decimal RMBSum;//RMB�������͵�����ֵ
                decimal floatProfitLossTotalRMB = 0;//�ܸ���ӯ��
                decimal marketProfitLossTotalRMB = 0;//�ܶ���ӯ��
                RMBSum = GetMarketValueByQH_HoldAccount(userInfo.UserAccountDistributeLogo, Types.CurrencyType.RMB, out floatProfitLossTotalRMB, out marketProfitLossTotalRMB);
                #endregion

                #region ��ȡ���׻���Ϊ��HK��������ֵ���ܸ���ӯ�����ܶ���ӯ��
                decimal HKSum;//HK�������͵�����ֵ
                decimal floatProfitLossTotalHK = 0;
                decimal marketProfitLossTotalHK = 0;
                HKSum = GetMarketValueByQH_HoldAccount(userInfo.UserAccountDistributeLogo, Types.CurrencyType.HK, out floatProfitLossTotalHK, out marketProfitLossTotalHK);
                #endregion

                #region ��ȡ���׻���Ϊ��US��������ֵ���ܸ���ӯ�����ܶ���ӯ��
                decimal USSum;//US�������͵�����ֵ
                decimal floatProfitLossTotalUS = 0;
                decimal marketProfitLossTotalUS = 0;
                USSum = GetMarketValueByQH_HoldAccount(userInfo.UserAccountDistributeLogo, Types.CurrencyType.US, out floatProfitLossTotalUS, out marketProfitLossTotalUS);
                #endregion

                #region �����ʽ��˻���Ϣ��ѯ��ؽ��׻��ҷ����������ʲ�
                foreach (QH_CapitalAccountTableInfo item in qh_AccountInfo_list)
                {
                    switch ((Types.CurrencyType)item.TradeCurrencyType)
                    {
                        case Types.CurrencyType.RMB:
                            {
                                temp.RMBAvailable = RMBSum + item.AvailableCapital;
                                temp.RMBFreeze = item.FreezeCapitalTotal;
                                ////����RMB���͵��ܸ���ӯ��
                                //temp.FloatProfitLossTotalRMB = floatProfitLossTotalRMB;
                                ////����RMB���͵��ܶ���ӯ��
                                //temp.MarketProfitLossTotalRMB = marketProfitLossTotalRMB;
                            }
                            break;
                        case Types.CurrencyType.HK:
                            {
                                temp.HKAvailable = HKSum + item.AvailableCapital;
                                temp.HKFreeze = item.FreezeCapitalTotal;
                                ////����HK���͵��ܸ���ӯ��
                                //temp.FloatProfitLossTotalHK = floatProfitLossTotalHK;
                                ////����HK���͵��ܶ���ӯ��
                                //temp.MarketProfitLossTotalHK = marketProfitLossTotalHK;
                            }
                            break;
                        case Types.CurrencyType.US:
                            {
                                temp.USAvailable = USSum + item.AvailableCapital;
                                temp.USFreeze = item.FreezeCapitalTotal;
                                ////����US���͵��ܸ���ӯ��
                                //temp.FloatProfitLossTotalUS = floatProfitLossTotalUS;
                                ////����US���͵��ܶ���ӯ��
                                //temp.MarketProfitLossTotalUS = marketProfitLossTotalUS;
                            }
                            break;
                        default:
                            break;
                    }

                }
                #endregion

                list.Add(temp);

                #region old Cdoe
                //var temp = new AssetSummaryEntity();
                ////temp.CapitalAccount = capitalAccount;

                ////1.��ø��ʽ��˺��µ�RMB(����ң��ĳֲ���ֵ
                //decimal RMBSum;
                //decimal floatProfitLossTotalRMB = 0;
                //decimal marketProfitLossTotalRMB = 0;
                //RMBSum = GetMarketValueByQhFundAccount(capitalAccount, Types.CurrencyType.RMB, out floatProfitLossTotalRMB, out marketProfitLossTotalRMB);

                ////�ֻ��ʽ���ϸ��ѯ(ͨ���ʽ��˺źͱ��֣�
                ////public SpotCapitalEntity SpotCapitalFind(string strFundAccountId,Types.CurrencyType currencyType, string userPassword,ref string strErrorMessage)
                //var _capitalAccountRMB = DataRepository.QhCapitalAccountTableProvider.Find(
                //    string.Format("UserAccountDistributeLogo='{0}' AND TradeCurrencyType ='{1}'", capitalAccount, 1));
                //decimal rmb = _capitalAccountRMB[0].AvailableCapital.Value;
                //temp.RMBAvailable = RMBSum + rmb;
                //temp.RMBFreeze = _capitalAccountRMB[0].FreezeCapitalTotal.Value;

                ////2.��ø��ʽ��˺��µ�HK���۱ң��ĳֲ���ֵ
                //decimal HKSum;
                //decimal floatProfitLossTotalHK = 0;
                //decimal marketProfitLossTotalHK = 0;
                //HKSum = GetMarketValueByQhFundAccount(capitalAccount, Types.CurrencyType.HK, out floatProfitLossTotalHK, out marketProfitLossTotalHK);
                //var _capitalAccountHK = DataRepository.QhCapitalAccountTableProvider.Find(
                //   string.Format("UserAccountDistributeLogo='{0}' AND TradeCurrencyType ='{1}'", capitalAccount, 2));
                //decimal hk = _capitalAccountHK[0].AvailableCapital.Value;
                //temp.HKAvailable = HKSum + hk;
                //temp.HKFreeze = _capitalAccountHK[0].FreezeCapitalTotal.Value;

                ////3.��ø��ʽ��˺��µ�US���۱ң��ĳֲ���ֵ
                //decimal USSum;
                //decimal floatProfitLossTotalUS = 0;
                //decimal marketProfitLossTotalUS = 0;
                //USSum = GetMarketValueByQhFundAccount(capitalAccount, Types.CurrencyType.US, out floatProfitLossTotalUS, out marketProfitLossTotalUS);
                //var _capitalAccountUS = DataRepository.QhCapitalAccountTableProvider.Find(
                //   string.Format("UserAccountDistributeLogo='{0}' AND TradeCurrencyType ='{1}'", capitalAccount, 3));
                //decimal US = _capitalAccountUS[0].AvailableCapital.Value;
                //temp.USAvailable = USSum + US;
                //temp.USFreeze = _capitalAccountUS[0].FreezeCapitalTotal.Value;

                //result.Add(temp);
                #endregion
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.ToString();
            }
            # endregion
            return list;
        }
        # endregion

        # region  ��NEW���ʲ����ܲ�ѯ�����ݽ���ԱID��ѯ��
        /// <summary>
        /// �ʲ����ܲ�ѯ
        /// Update by:���
        /// Update date:2009-07-28
        /// Desc.:�޸�һЩDAL��������ص�Bug,�Լ������߼��淶,���޸Ļ�ȡ�û���Ϣ�����ݻ����л�ȡ
        /// </summary>
        /// <param name="findAccount">Ҫ�����ʲ���ѯ�˻���ʵ��</param>
        /// <returns></returns>
        public List<AssetSummaryEntity> AssetSummaryFind(FindAccountEntity findAccount, out  string outMessage)
        {
            outMessage = string.Empty;

            #region ��������
            List<AssetSummaryEntity> list = new List<AssetSummaryEntity>();
            AssetSummaryEntity model = new AssetSummaryEntity();
            AccountManager am = AccountManager.Instance;
            decimal RMBAvailble = 0;
            decimal HKAvailble = 0;
            decimal USAvailble = 0;
            decimal RBMFreeze = 0;
            decimal HKFreeze = 0;
            decimal USFreeze = 0;
            #endregion

            #region ��øý���Ա�������ʽ��˺�
            #region �ӻ����л�ȡ����
            //��øý���Ա�������ʽ��˺�
            var _bankCapitalAccount = am.GetAccountByUserIDAndAccountTypeClass(findAccount.UserID, Types.AccountAttributionType.BankAccount);

            //��øý���Ա�������ֻ��ʽ��˺�
            var _XH_CapitalAccount = am.GetAccountByUserIDAndAccountTypeClass(findAccount.UserID, Types.AccountAttributionType.SpotCapital);

            //��øý���Ա�������ڻ��ʽ��˺�
            var _QH_CapitalAccount = am.GetAccountByUserIDAndAccountTypeClass(findAccount.UserID, Types.AccountAttributionType.FuturesCapital);
            #endregion

            #region �����ݿ��л�ȡ����
            ////��øý���Ա�������ʽ��˺�
            //string _bankCapitalAccount = aa.Find_UserBankCapitalAccount(findAccount.UserID);

            ////��øý���Ա�������ֻ��ʽ��˺�
            //string[] _XH_CapitalAccount = aa.Find_UserXHCapitalAccount(findAccount.UserID);

            ////��øý���Ա�������ڻ��ʽ��˺�
            //string[] _QH_CapitalAccount = aa.Find_UserQHCapitalAccount(findAccount.UserID);
            #endregion
            #endregion

            # region �����ʽ����
            if (!Utils.IsNullOrEmpty(_bankCapitalAccount))
            {
                var Bank = BankCapitalFind(findAccount.UserID, _bankCapitalAccount[0].UserAccountDistributeLogo, out outMessage);
                foreach (UA_BankAccountTableInfo item in Bank)
                {
                    if (item.TradeCurrencyTypeLogo == 1)
                    {
                        RMBAvailble += item.AvailableCapital;
                        RBMFreeze += item.FreezeCapital;
                    }
                    else if (item.TradeCurrencyTypeLogo == 2)
                    {
                        HKAvailble += item.AvailableCapital;
                        HKFreeze += item.FreezeCapital;
                    }
                    else
                    {
                        USAvailble += item.AvailableCapital;
                        USFreeze += item.FreezeCapital;
                    }
                }
            }
            # endregion

            # region �ֻ��ʽ����
            foreach (UA_UserAccountAllocationTableInfo xhcapitalAccount in _XH_CapitalAccount)
            {
                var Xh = SpotAssetSummaryFind(xhcapitalAccount.UserAccountDistributeLogo, "", out outMessage);
                foreach (AssetSummaryEntity item in Xh)
                {
                    RMBAvailble += item.RMBAvailable;
                    RBMFreeze += item.RMBFreeze;

                    HKAvailble += item.HKAvailable;
                    HKFreeze += item.HKFreeze;

                    USAvailble += item.USAvailable;
                    USFreeze += item.USFreeze;
                }
            }
            # endregion

            # region �ڻ��ʽ����
            foreach (UA_UserAccountAllocationTableInfo qhCapitalAccount in _QH_CapitalAccount)
            {
                var Qh = FuturesAssetSummaryFind(qhCapitalAccount.UserAccountDistributeLogo, "", out outMessage);
                foreach (AssetSummaryEntity item in Qh)
                {
                    RMBAvailble += item.RMBAvailable;
                    RBMFreeze += item.RMBFreeze;

                    HKAvailble += item.HKAvailable;
                    HKFreeze += item.HKFreeze;

                    USAvailble += item.USAvailable;
                    USFreeze += item.USFreeze;
                }
            }
            # endregion

            #region �������������װʵ�巵��
            //����ԱID
            model.UserID = findAccount.UserID;

            //RMB�Ļ������
            model.RMBAvailable = RMBAvailble;
            model.RMBFreeze = RBMFreeze;

            //�۱ҵĻ������
            model.HKAvailable = HKAvailble;
            model.HKFreeze = HKFreeze;

            //��Ԫ�Ļ������
            model.USAvailable = USAvailble;
            model.USFreeze = USFreeze;

            list.Add(model);
            #endregion
            return list;
        }
        # endregion

        #region Create by :��� 2009-07-12

        #region �ֻ��ֲ֡��ֲֶ����ѯ

        #region �ֻ��ֲ���ϸ��ѯ

        #region ���ݡ��û�ID��ѯ���û���ӵ�е��ֻ��ֲ��˺���ϸ
        /// <summary>
        /// ���ݡ��û�ID��ѯ���û���ӵ�е�public���ֲ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="accountType">�˺�����(���ݿ���BD_AccountType�ľ���) �����Ϊ0ʱ��ѯ��Ӧ���ֻ��ֲ��˺����(3--֤ȯ�ɶ�����,9--�۹ɹɶ�����)</param>
        /// <param name="currencytype">Ҫ��ѯ�Ļ�������</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<XH_AccountHoldTableInfo> QueryXH_AccountHoldByUserID(string userID, int accountType, QueryType.QueryCurrencyType currencytype, out string errorMsg)
        {
            errorMsg = "";
            List<XH_AccountHoldTableInfo> list = new List<XH_AccountHoldTableInfo>();

            #region �����ݿ���ȡ����
            //try
            //{ 
            //    XH_AccountHoldTableDal dal = new XH_AccountHoldTableDal();
            //    list = dal.GetListByUserID(userID, type);
            //}
            //catch (Exception ex)
            //{
            //    errorMsg = ex.Message;
            //    LogHelper.WriteError(ex.ToString(), ex);
            //}
            #endregion
            try
            {
                #region ��ͨ���û�IDȡ���û����ֻ��ֲ��˺�
                // UA_UserAccountAllocationTableDal dal = new UA_UserAccountAllocationTableDal();
                List<UA_UserAccountAllocationTableInfo> userAccountInfo = new List<UA_UserAccountAllocationTableInfo>();

                #region ���Ϊ0�Ͳ�ѯ����µ����п����������˺�

                #region �ӻ����л�ȡ�˺�
                userAccountInfo = AccountManager.Instance.GetUserHoldAccountFormUserCache(userID, accountType, 1);
                #endregion

                #region ֱ�Ӵ����ݿ��л�ȡ
                //if (accountType == 0)
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountTypeClass(userID, Types.AccountAttributionType.SpotHold);
                //}
                //else
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountType(userID, accountType);
                //}
                #endregion
                #endregion
                #endregion

                #region ���ڴ���л�ȡ����
                foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
                {
                    list.AddRange(GetXH_AccountHoldInfoListFromMemory(item.UserAccountDistributeLogo, currencytype, out errorMsg));
                }
                #endregion
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;

        }
        #endregion

        #region ���ݡ��û�ID�����롿��ѯ�û���ӵ�е��ֻ��ֲ��˺���ϸ
        /// <summary>
        /// ���ݡ��û�ID�����롿��ѯ�û���ӵ�е��ֻ��ֲ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="pwd">�û�������Ա������</param>
        /// <param name="accountType">�˺�����(���ݿ���BD_AccountType�ľ���) �����Ϊ0ʱ��ѯ��Ӧ���ֻ��ֲ��˺����(3--֤ȯ�ɶ�����,9--�۹ɹɶ�����)</param>
        /// <param name="currencytype">Ҫ��ѯ�Ļ�������</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<XH_AccountHoldTableInfo> QueryXH_AccountHoldByUserIDAndPwd(string userID, string pwd, int accountType, QueryType.QueryCurrencyType currencytype, out string errorMsg)
        {
            errorMsg = "";
            List<XH_AccountHoldTableInfo> list = new List<XH_AccountHoldTableInfo>();

            #region �����ݿ��л�ȡ����
            //try
            //{
            //    XH_AccountHoldTableDal dal = new XH_AccountHoldTableDal();
            //    list = dal.GetListByUserIDAndPwd(userID, pwd, type);
            //}
            //catch (Exception ex)
            //{
            //    errorMsg = ex.Message;
            //    LogHelper.WriteError(ex.ToString(), ex);
            //}
            #endregion
            try
            {
                #region ����û�����
                //update 2009-11-25 ���
                //var userDal = new UA_UserBasicInformationTableDal();
                //if (!userDal.Exists(userID, pwd))
                //{
                //    errorMsg = "�û����벻��ȷ������û�д��û������Ϣ��";
                //    return list;
                //}
                //==============
                //====new===
                if (!AccountManager.Instance.ExistsBasicUserPWDByUserId(userID, pwd))
                {
                    errorMsg = "�û����벻��ȷ������û�д��û������Ϣ��";
                    return list;
                }
                //==========end==========

                #endregion

                #region ��ͨ���û�IDȡ���û����ֻ��ֲ��ʽ��˺�
                // UA_UserAccountAllocationTableDal dal = new UA_UserAccountAllocationTableDal();
                List<UA_UserAccountAllocationTableInfo> userAccountInfo = new List<UA_UserAccountAllocationTableInfo>();
                #region ���Ϊ0�Ͳ�ѯ����µ����п����������˺�
                #region �ӻ����л�ȡ�˺�
                userAccountInfo = AccountManager.Instance.GetUserHoldAccountFormUserCache(userID, accountType, 1);
                #endregion
                #endregion

                #region ֱ�Ӵ����ݿ��л�ȡ
                //if (accountType == 0)
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountTypeClass(userID, Types.AccountAttributionType.SpotHold);
                //}
                //else
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountType(userID, accountType);
                //}
                #endregion
                #endregion

                #region ���ڴ���л�ȡ����
                foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
                {
                    list.AddRange(GetXH_AccountHoldInfoListFromMemory(item.UserAccountDistributeLogo, currencytype, out errorMsg));
                }
                #endregion

            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }

            return list;
        }

        #endregion

        #region ���ݡ��ֻ��ֲ��˺š���ѯ�ֻ��ֲ��˺���ϸ
        /// <summary>
        /// ���ݡ��ֻ��ֲ��˺š���ѯ�ֻ��ֲ��˺���ϸ
        /// </summary>
        ///<param name="xh_hold_Account">�ڻ��ֲ��˺�</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<XH_AccountHoldTableInfo> QueryXH_AccountHoldByAccount(string xh_hold_Account, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<XH_AccountHoldTableInfo> list = new List<XH_AccountHoldTableInfo>();

            #region �����ݿ��л�ȡ����
            //try
            //{ 
            //    XH_AccountHoldTableDal dal = new XH_AccountHoldTableDal();
            //    list = dal.GetListByAccount(xh_Cap_Account, type);
            //}
            //catch (Exception ex)
            //{
            //    errorMsg = ex.Message;
            //    LogHelper.WriteError(ex.ToString(), ex);
            //}
            #endregion

            #region ���ڴ���л�ȡ����
            list = GetXH_AccountHoldInfoListFromMemory(xh_hold_Account, currencyType, out errorMsg);
            #endregion
            return list;
        }
        #endregion

        #region �����ֻ��ֲ��˺źͲ�ѯ���׻������ʹ��ڴ���л�ȡ�ֻ��ֱֲ������б�
        /// <summary>
        /// �����ֻ��ֲ��˺źͲ�ѯ���׻������ʹ��ڴ���л�ȡ�ֻ��ֱֲ������б�
        /// </summary>
        /// <param name="xh_Hold_Account">�ֻ��ֲ��˺�</param>
        /// <param name="currencyType">���׻�������</param>
        /// <param name="errorMsg">��ѯ�쳣</param>
        /// <returns></returns>
        List<XH_AccountHoldTableInfo> GetXH_AccountHoldInfoListFromMemory(string xh_Hold_Account, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<XH_AccountHoldTableInfo> list = new List<XH_AccountHoldTableInfo>();
            if (!ServerConfig.IsLoadAllData)
            {
                #region û�м��ػ���ֲ֣������ݿ��в�ѯ
                try
                {
                    //string messStr = "GetXH_AccountHoldInfoListFromMemory===���ڲ�ѯ�ֻ��˺�" + xh_Hold_Account + "�ĳֲ�,��Ϊû�м��سֱֲ��������ڴ����ݿ��в�ѯ����ѯʱ��" + DateTime.Now.ToString();
                    //LogHelper.WriteInfo(messStr);

                    XH_AccountHoldTableDal dal = new XH_AccountHoldTableDal();
                    list = dal.GetListByAccount(xh_Hold_Account, currencyType);
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                    LogHelper.WriteError(ex.ToString(), ex);
                }
                #endregion
            }
            else
            {
                #region ֱ�Ӵӻ����в�ѯ
                var manager = MemoryDataManager.XHHoldMemoryList;
                if (manager == null)
                {
                    errorMsg = "��û�г�ʼ���������ݶ���";
                    return list;
                }
                IList<int> listID = manager.GetAccountHoldLogoID(xh_Hold_Account);
                if (Utils.IsNullOrEmpty(listID))
                {
                    return list;
                }
                if (currencyType == QueryType.QueryCurrencyType.ALL)
                {
                    foreach (int item in listID)
                    {
                        XHHoldMemoryTable table = manager.GetByAccountHoldLogoId(item);
                        if (table != null)
                        {
                            var info = table.Data;
                            list.Add(info);
                        }
                    }
                }
                else
                {
                    foreach (int item in listID)
                    {
                        XHHoldMemoryTable table = manager.GetByAccountHoldLogoId(item);

                        if (table != null)
                        {
                            var info = table.Data;
                            if (info.CurrencyTypeId == (int)currencyType)
                            {
                                list.Add(info);
                            }
                        }
                    }
                }
                #endregion
            }

            return list;
        }
        #endregion
        #endregion

        #region �ֻ��ֲֶ�����ϸ��ѯ

        #region ����ί�б�Ų�ѯ���ֻ��ֲֶ������ϸ
        /// <summary>
        /// ����ί�б�Ų�ѯ���ֻ��ֲֶ������ϸ
        /// </summary>
        /// <param name="entrustNo">ί�б��</param>
        /// <param name="freezeType">��������</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<XH_AcccountHoldFreezeTableInfo> QueryXH_AcccountHoldFreezeByEntrustNo(string entrustNo, QueryType.QueryFreezeType freezeType, out string errorMsg)
        {
            errorMsg = "";
            List<XH_AcccountHoldFreezeTableInfo> list = null;
            if (string.IsNullOrEmpty(entrustNo))
            {
                errorMsg = "ί�б�Ų���Ϊ�գ�";
                return list;
            }
            XH_AcccountHoldFreezeTableDal dal = new XH_AcccountHoldFreezeTableDal();
            try
            {
                StringBuilder sb = new StringBuilder("  EntrustNumber='" + entrustNo.Trim() + "'");
                //�����ѯ�Ķ������Ͳ��ǲ�ѯ����ʱ��������
                if (freezeType != QueryType.QueryFreezeType.ALL)
                {
                    sb.AppendFormat("  AND FreezeTypeLogo='{0}'", (int)freezeType);
                }
                list = dal.GetListArray(sb.ToString());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
        }
        #endregion

        #region �����û��ֲ��˺źͲ�ѯ�Ľ��׵Ļ������͡���ѯʱ��β�ѯ���ֻ��ֲֶ������ϸ��Ϣ
        /// <summary>
        /// Title�������û��ֲ��˺źͲ�ѯ�Ľ��׵Ļ������͡���ѯʱ��β�ѯ���ֻ��ֲֶ������ϸ��Ϣ
        /// Desc.�������ʼʱ����߽���ʱ��Ϊnull�����ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        ///        �����ʼʱ����ڽ���ʱ�伴���ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        /// </summary>
        /// <param name="account">�ֲ��˺�</param>
        /// <param name="startTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="currencyType">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="freezeType">��������</param>
        /// <param name="pageInfo">��ҳ��Ϣ</param>
        /// <param name="total">��ҳ��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<XH_AcccountHoldFreezeTableInfo> PagingQueryXH_AcccountHoldFreezeByAccount(string holdAccount, DateTime? startTime, DateTime? endTime, QueryType.QueryCurrencyType currencyType, QueryType.QueryFreezeType freezeType, PagingInfo pageInfo, out int total, out string errorMsg)
        {
            #region ��ʼ������
            List<XH_AcccountHoldFreezeTableInfo> list = null;
            XH_AcccountHoldFreezeTableDal dal = new XH_AcccountHoldFreezeTableDal();
            errorMsg = "";
            total = 0;
            #endregion

            #region ����ֲ��˺Ż��߷�ҳ����ʵ��Ϊ��ֱ�ӷ����󴫵ݲ�����ʾ �б�Ϊnull
            if (string.IsNullOrEmpty(holdAccount))
            {
                errorMsg = "�ֲ��˺Ų���Ϊ�գ�";
                return list;
            }
            if (pageInfo == null)
            {
                errorMsg = "��ҳ��Ϣ����Ϊ��!";
                return null;
            }
            #endregion

            #region ��ҳ��ѯ�����Ϣ
            PagingProceduresInfo prcoInfo = new PagingProceduresInfo();
            prcoInfo.IsCount = pageInfo.IsCount;
            prcoInfo.PageNumber = pageInfo.CurrentPage;
            prcoInfo.PageSize = pageInfo.PageLength;
            prcoInfo.Fields = "  f.HoldFreezeLogoId,f.EntrustNumber,f.prepareFreezeAmount,f.FreezeTypeLogo,f.AccountHoldLogo,f.ThawTime,f.FreezeTime ";
            prcoInfo.PK = "f.HoldFreezeLogoId";
            if (pageInfo.Sort == 0)
            {
                prcoInfo.Sort = " f.FreezeTime asc ";
            }
            else
            {
                prcoInfo.Sort = " f.FreezeTime desc ";
            }
            prcoInfo.Tables = "XH_AcccountHoldFreezeTable as f  inner join XH_AccountHoldTable as a on a.AccountHoldLogoId=f.AccountHoldLogo";

            #region ��װ�������
            StringBuilder sb = new StringBuilder();
            //1=1 and a.UserAccountDistributeLogo='010000000406' and a.TradeCurrencyType=1
            sb.Append(" a.UserAccountDistributeLogo='" + holdAccount + "'  ");

            if (currencyType != QueryType.QueryCurrencyType.ALL)
            {
                sb.Append(" and a.CurrencyTypeId='" + (int)currencyType + "'");
            }
            sb.AppendFormat(CommonDALBulidSQLScript.BuildWhereQueryBetwennTime(startTime, endTime, 30), "f.FreezeTime");
            //�����ѯ�Ķ������Ͳ��ǲ�ѯ����ʱ��������
            if (freezeType != QueryType.QueryFreezeType.ALL)
            {
                sb.AppendFormat("  AND f.FreezeTypeLogo='{0}'", (int)freezeType);
            }
            #endregion

            prcoInfo.Filter = sb.ToString();
            #endregion

            #region ִ�в�ѯ
            try
            {
                CommonDALOperate<XH_AcccountHoldFreezeTableInfo> com = new CommonDALOperate<XH_AcccountHoldFreezeTableInfo>();
                list = com.PagingQueryProcedures(prcoInfo, out total, dal.ReaderBind);
                //list = dal.PagingXH_AcccountHoldFreezeByFilter(prcoInfo, out pageTotal);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
            #endregion

        }

        #endregion

        #region �����û�ID�Ͳ�ѯ�Ľ��׵Ļ������͡���ѯʱ��β�ѯ���ֻ��ֲֶ������ϸ
        /// <summary>
        /// Title�������û�ID�Ͳ�ѯ�Ľ��׵Ļ������͡���ѯʱ��β�ѯ���ֻ��ֲֶ������ϸ��Ϣ
        /// Desc.�������ʼʱ����߽���ʱ��Ϊnull�����ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        ///        �����ʼʱ����ڽ���ʱ�伴���ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        /// </summary>
        /// <param name="userID">�ֲ��˺�</param>
        /// <param name="startTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="accountType">�˺�����(���ݿ���BD_AccountType�ľ���) �����Ϊ0ʱ��ѯ��Ӧ���ֻ��ֲ��˺����(3--֤ȯ�ɶ�����,9--�۹ɹɶ�����)</param>
        /// <param name="currencytype">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="freezeType">��������</param>
        /// <param name="pageInfo">��ҳ��Ϣ</param>
        /// <param name="total">��ҳ��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<XH_AcccountHoldFreezeTableInfo> PagingQueryXH_AcccountHoldFreezeByUserID(string userID, int accountType, DateTime? startTime, DateTime? endTime, QueryType.QueryCurrencyType currencyType, QueryType.QueryFreezeType freezeType, PagingInfo pageInfo, out int total, out string errorMsg)
        {
            #region ��ʼ������
            List<XH_AcccountHoldFreezeTableInfo> list = null;
            XH_AcccountHoldFreezeTableDal dal = new XH_AcccountHoldFreezeTableDal();
            errorMsg = "";
            total = 0;
            #endregion

            #region ����û�ID���߷�ҳ����ʵ��Ϊ��ֱ�ӷ����󴫵ݲ�����ʾ �б�Ϊnull
            if (string.IsNullOrEmpty(userID))
            {
                errorMsg = "�û�ID����Ϊ�գ�";
                return list;
            }
            //����պ�Ҫ�����û���������֤�ڴ˼���
            if (pageInfo == null)
            {
                errorMsg = "��ҳ��Ϣ����Ϊ��!";
                return null;
            }
            #endregion

            #region ��ҳ��ѯ�����Ϣ
            PagingProceduresInfo prcoInfo = new PagingProceduresInfo();
            prcoInfo.IsCount = pageInfo.IsCount;
            prcoInfo.PageNumber = pageInfo.CurrentPage;
            prcoInfo.PageSize = pageInfo.PageLength;
            prcoInfo.Fields = "  f.HoldFreezeLogoId,f.EntrustNumber,f.prepareFreezeAmount,f.FreezeTypeLogo,f.AccountHoldLogo,f.ThawTime,f.FreezeTime ";
            prcoInfo.PK = "f.HoldFreezeLogoId";
            if (pageInfo.Sort == 0)
            {
                prcoInfo.Sort = " f.FreezeTime asc ";
            }
            else
            {
                prcoInfo.Sort = " f.FreezeTime desc ";
            }
            prcoInfo.Tables = "XH_AcccountHoldFreezeTable as f  inner join XH_AccountHoldTable as a on a.AccountHoldLogoId=f.AccountHoldLogo";

            #region ��װ�������
            StringBuilder sb = new StringBuilder();

            #region �ӻ����л�ȡ�˺�

            List<UA_UserAccountAllocationTableInfo> userAccountInfo = AccountManager.Instance.GetUserHoldAccountFormUserCache(userID, accountType, 1);
            #region �������
            string userIDstr = "";
            foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
            {
                userIDstr += ",   '" + item.UserAccountDistributeLogo + "'";
            }
            if (!string.IsNullOrEmpty(userIDstr))
            {
                userIDstr = userIDstr.Substring(userIDstr.IndexOf(',') + 1);
            }

            #endregion
            #endregion

            #region ֱ�Ӵ����ݿ��л�ȡ
            if (!string.IsNullOrEmpty(userIDstr))
            {
                if (userIDstr.Split(',').Length > 1)
                {
                    sb.AppendFormat("  a.UserAccountDistributeLogo  in ( {0} )", userIDstr);
                }
                else
                {
                    sb.AppendFormat("  a.UserAccountDistributeLogo = {0} ", userIDstr);
                }
            }
            else //����ڻ����л�ȡ��������ֱ�Ӵ����ݿ��л�ȡ����
            {

                //�Ҳ����û��������ݿ��в�����,���ﲻ�ٹ���
                sb.Append("  a.UserAccountDistributeLogo=''  ");//����������Ϊ�˺��������
                //if (accountType == 0)
                //{
                //    sb.Append("  a.UserAccountDistributeLogo  in ( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo in ( select accounttypelogo from BD_AccountType where atcid='" + (int)CommonObject.Types.AccountAttributionType.SpotHold + "')  and userid='" + userID + "' )");
                //}
                //else
                //{
                //    sb.Append("  a.UserAccountDistributeLogo  in ( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo='" + accountType + "' and userid='" + userID + "' )");
                //}
            }
            #endregion

            if (currencyType != QueryType.QueryCurrencyType.ALL)
            {
                sb.Append(" and a.CurrencyTypeId='" + (int)currencyType + "'");
            }
            sb.AppendFormat(CommonDALBulidSQLScript.BuildWhereQueryBetwennTime(startTime, endTime, 30), "f.FreezeTime");
            //�����ѯ�Ķ������Ͳ��ǲ�ѯ����ʱ��������
            if (freezeType != QueryType.QueryFreezeType.ALL)
            {
                sb.AppendFormat("  AND f.FreezeTypeLogo='{0}'", (int)freezeType);
            }
            #endregion

            prcoInfo.Filter = sb.ToString();
            #endregion

            #region ִ�в�ѯ
            try
            {
                CommonDALOperate<XH_AcccountHoldFreezeTableInfo> com = new CommonDALOperate<XH_AcccountHoldFreezeTableInfo>();
                list = com.PagingQueryProcedures(prcoInfo, out total, dal.ReaderBind);
                //list = dal.PagingXH_AcccountHoldFreezeByFilter(prcoInfo, out pageTotal);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
            #endregion
        }
        #endregion
        #endregion

        #endregion

        #region �ڻ��ֲ֡��ֲֶ����ѯ

        #region �ڻ��ֲ���ϸ��ѯ

        #region ���ݡ��û�ID��ѯ���û���ӵ�е��ڻ��ֲ��˺���ϸ
        /// <summary>
        /// ���ݡ��û�ID��ѯ���û���ӵ�е�public���ֲ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="accountType">�˺�����(���ݿ���BD_AccountType�ľ���) �����Ϊ0ʱ��ѯ��Ӧ���ֻ��ֲ��˺����(3--֤ȯ�ɶ�����,9--�۹ɹɶ�����)</param>
        /// <param name="currencytype">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<QH_HoldAccountTableInfo> QueryQH_HoldAccountByUserID(string userID, int accountType, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<QH_HoldAccountTableInfo> list = new List<QH_HoldAccountTableInfo>();

            #region �����ݿ��л�ȡ����
            //try
            //{
            //    QH_HoldAccountTableDal dal = new QH_HoldAccountTableDal();
            //    list = dal.GetListByUserID(userID, type);
            //}
            //catch (Exception ex)
            //{
            //    errorMsg = ex.Message;
            //    LogHelper.WriteError(ex.ToString(), ex);
            //}
            #endregion

            try
            {
                #region ��ͨ���û�IDȡ���û����ڻ��ֲ��ʽ��˺�
                // UA_UserAccountAllocationTableDal dal = new UA_UserAccountAllocationTableDal();
                List<UA_UserAccountAllocationTableInfo> userAccountInfo = new List<UA_UserAccountAllocationTableInfo>();
                #region �ӻ����л�ȡ�˺�
                userAccountInfo = AccountManager.Instance.GetUserHoldAccountFormUserCache(userID, accountType, 0);
                #endregion
                #region �����ݿ��л�ȡ�˺�
                //if (accountType == 0)
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountTypeClass(userID, Types.AccountAttributionType.FuturesHold);
                //}
                //else
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountType(userID, accountType);
                //}
                #endregion
                #endregion

                #region ���ڴ���л�ȡ����
                foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
                {
                    list.AddRange(GetQH_HoldAccountInfoListFromMemory(item.UserAccountDistributeLogo, currencyType, out errorMsg));
                }
                #endregion

            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;

        }
        #endregion

        #region ���ݡ��û�ID�����롿��ѯ�û���ӵ�е��ڻ��ֲ��˺���ϸ
        /// <summary>
        /// ���ݡ��û�ID�����롿��ѯ�û���ӵ�е��ڻ��ֲ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="pwd">�û�������Ա������</param>
        /// <param name="accountType">�˺�����(���ݿ���BD_AccountType�ľ���) �����Ϊ0ʱ��ѯ��Ӧ���ֻ��ֲ��˺����(3--֤ȯ�ɶ�����,9--�۹ɹɶ�����)</param>
        /// <param name="currencytype">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<QH_HoldAccountTableInfo> QueryQH_HoldAccountByUserIDAndPwd(string userID, string pwd, int accountType, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<QH_HoldAccountTableInfo> list = new List<QH_HoldAccountTableInfo>();
            #region �����ݿ��л�ȡ����
            //try
            //{
            //    QH_HoldAccountTableDal dal = new QH_HoldAccountTableDal();
            //    list = dal.GetListByUserIDAndPwd(userID, pwd, type);
            //}
            //catch (Exception ex)
            //{
            //    errorMsg = ex.Message;
            //    LogHelper.WriteError(ex.ToString(), ex);
            //}
            #endregion
            try
            {
                #region ����û�����

                //update 2009-11-25 ���
                //var userDal = new UA_UserBasicInformationTableDal();
                //if (!userDal.Exists(userID, pwd))
                //{
                //    errorMsg = "�û����벻��ȷ������û�д��û������Ϣ��";
                //    return list;
                //}
                //==============
                //====new===
                if (!AccountManager.Instance.ExistsBasicUserPWDByUserId(userID, pwd))
                {
                    errorMsg = "�û����벻��ȷ������û�д��û������Ϣ��";
                    return list;
                }
                //==========end==========

                #endregion

                #region ��ͨ���û�IDȡ���û����ڻ��ֲ��ʽ��˺�
                // UA_UserAccountAllocationTableDal dal = new UA_UserAccountAllocationTableDal();
                List<UA_UserAccountAllocationTableInfo> userAccountInfo = new List<UA_UserAccountAllocationTableInfo>();
                #region �ӻ����л�ȡ�˺�
                userAccountInfo = AccountManager.Instance.GetUserHoldAccountFormUserCache(userID, accountType, 0);
                #endregion
                #region �����ݿ��л�ȡ�˺�
                //if (accountType == 0) //Ϊ0ʱ��������ض���˻��۹ɻ���֤ȯ
                //{
                //    dal.GetUserAccountByUserIDAndPwdAndAccountTypeClass(userID, Types.AccountAttributionType.FuturesHold);
                //}
                //else //��Ϊ0ʱ��ѯһ���˻��������Ϣ
                //{
                //    dal.GetUserAccountByUserIDAndPwdAndAccountType(userID, accountType);
                //}
                #endregion
                #endregion

                #region ���ڴ���л�ȡ����
                foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
                {
                    list.AddRange(GetQH_HoldAccountInfoListFromMemory(item.UserAccountDistributeLogo, currencyType, out errorMsg));
                }
                #endregion

            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
        }

        #endregion

        #region ���ݡ��ڻ��ֲ��˺š���ѯ�ڻ��ֲ��˺���ϸ
        /// <summary>
        /// ���ݡ��ڻ��ֲ��˺š���ѯ�ڻ��ֲ��˺���ϸ
        /// </summary>
        ///<param name="qh_Cap_Account">�ڻ��ֲ��˺�</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<QH_HoldAccountTableInfo> QueryQH_HoldAccountByAccount(string qh_Cap_Account, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<QH_HoldAccountTableInfo> list = new List<QH_HoldAccountTableInfo>();
            #region �����ݿ���ȡ����
            //try
            //{
            //    QH_HoldAccountTableDal dal = new QH_HoldAccountTableDal();
            //    list = dal.GetListByAccount(xh_Cap_Account, type);
            //}
            //catch (Exception ex)
            //{
            //    errorMsg = ex.Message;
            //    LogHelper.WriteError(ex.ToString(), ex);
            //}
            #endregion

            #region ���ڴ���л�ȡ����

            list = GetQH_HoldAccountInfoListFromMemory(qh_Cap_Account, currencyType, out errorMsg);

            #endregion
            return list;
        }
        #endregion

        #region �����ڻ��ֲ��˺źͲ�ѯ���׻������ʹ��ڴ���л�ȡ�ڻ��ֱֲ������б�
        /// <summary>
        /// �����ڻ��ֲ��˺źͲ�ѯ���׻������ʹ��ڴ���л�ȡ�ڻ��ֱֲ������б�
        /// </summary>
        /// <param name="qh_Hold_Account">�ڻ��ֲ��˺�</param>
        /// <param name="currencyType">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="errorMsg">��ѯ�쳣</param>
        /// <returns></returns>
        List<QH_HoldAccountTableInfo> GetQH_HoldAccountInfoListFromMemory(string qh_Hold_Account, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<QH_HoldAccountTableInfo> list = new List<QH_HoldAccountTableInfo>();
            if (!ServerConfig.IsLoadAllData)
            {
                #region û�м��ػ���ֲ֣������ݿ��в�ѯ
                try
                {
                    //string messStr = "GetQH_HoldAccountInfoListFromMemory===���ڲ�ѯ�ڻ��˺�" + qh_Hold_Account + "�ĳֲ�,��Ϊû�м��سֱֲ��������ڴ����ݿ��в�ѯ����ѯʱ��" + DateTime.Now.ToString();
                    //LogHelper.WriteInfo(messStr);

                    QH_HoldAccountTableDal dal = new QH_HoldAccountTableDal();
                    list = dal.GetListByAccount(qh_Hold_Account, currencyType);
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                    LogHelper.WriteError(ex.ToString(), ex);
                }
                #endregion
            }
            else
            {

                #region ֱ�Ӵӻ����в�ѯ
                var manager = MemoryDataManager.QHHoldMemoryList;
                if (manager == null)
                {
                    errorMsg = "��û�г�ʼ���������ݶ���";
                    return list;
                }
                IList<int> listID = manager.GetAccountHoldLogoID(qh_Hold_Account);
                if (Utils.IsNullOrEmpty(listID))
                {
                    return list;
                }
                if (currencyType == QueryType.QueryCurrencyType.ALL)
                {
                    foreach (int item in listID)
                    {
                        QHHoldMemoryTable table = manager.GetByAccountHoldLogoId(item);
                        if (table != null)
                        {
                            var info = table.Data;
                            list.Add(info);
                        }
                    }
                }
                else
                {
                    foreach (int item in listID)
                    {
                        QHHoldMemoryTable table = manager.GetByAccountHoldLogoId(item);
                        if (table != null)
                        {
                            var info = table.Data;
                            if (info.TradeCurrencyType == (int)currencyType)
                            {
                                list.Add(info);
                            }
                        }
                    }
                }
                #endregion
            }
            return list;
        }
        #endregion
        #endregion

        #region �ڻ��ֲֶ�����ϸ��ѯ
        #region ����ί�б�Ų�ѯ���ڻ��ֲֶ������ϸ
        /// <summary>
        /// ����ί�б�Ų�ѯ���ڻ��ֲֶ������ϸ
        /// </summary>
        /// <param name="entrustNo">ί�б��</param>
        /// <param name="freezeType">��������</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<QH_HoldAccountFreezeTableInfo> QueryQH_HoldAccountFreezeByEntrustNo(string entrustNo, QueryType.QueryFreezeType freezeType, out string errorMsg)
        {
            errorMsg = "";
            List<QH_HoldAccountFreezeTableInfo> list = null;
            if (string.IsNullOrEmpty(entrustNo))
            {
                errorMsg = "ί�б�Ų���Ϊ�գ�";
                return list;
            }
            QH_HoldAccountFreezeTableDal dal = new QH_HoldAccountFreezeTableDal();
            try
            {
                StringBuilder sb = new StringBuilder("  EntrustNumber='" + entrustNo.Trim() + "'");
                //�����ѯ�Ķ������Ͳ��ǲ�ѯ����ʱ��������
                if (freezeType != QueryType.QueryFreezeType.ALL)
                {
                    sb.AppendFormat("  AND FreezeTypeLogo='{0}'", (int)freezeType);
                }
                list = dal.GetListArray(sb.ToString());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
        }
        #endregion

        #region �����û��ֲ��˺źͲ�ѯ�Ľ��׵Ļ������͡���ѯʱ��β�ѯ���ڻ��ֲֶ������ϸ��Ϣ
        /// <summary>
        /// Title�������û��ֲ��˺źͲ�ѯ�Ľ��׵Ļ������͡���ѯʱ��β�ѯ���ڻ��ֲֶ������ϸ��Ϣ
        /// Desc.�������ʼʱ����߽���ʱ��Ϊnull�����ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        ///        �����ʼʱ����ڽ���ʱ�伴���ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        /// </summary>
        /// <param name="account">�ֲ��˺�</param>
        /// <param name="startTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="currencyType">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="freezeType">��������</param>
        /// <param name="pageInfo">��ҳ��Ϣ</param>
        /// <param name="total">��ҳ��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<QH_HoldAccountFreezeTableInfo> PagingQueryQH_HoldAccountFreezeByAccount(string holdAccount, DateTime? startTime, DateTime? endTime, QueryType.QueryCurrencyType currencyType, QueryType.QueryFreezeType freezeType, PagingInfo pageInfo, out int total, out string errorMsg)
        {
            #region ��ʼ������
            List<QH_HoldAccountFreezeTableInfo> list = null;
            QH_HoldAccountFreezeTableDal dal = new QH_HoldAccountFreezeTableDal();
            errorMsg = "";
            total = 0;
            #endregion

            #region ����ֲ��˺Ż��߷�ҳ����ʵ��Ϊ��ֱ�ӷ����󴫵ݲ�����ʾ �б�Ϊnull
            if (string.IsNullOrEmpty(holdAccount))
            {
                errorMsg = "�ֲ��˺Ų���Ϊ�գ�";
                return list;
            }
            if (pageInfo == null)
            {
                errorMsg = "��ҳ��Ϣ����Ϊ��!";
                return null;
            }
            #endregion

            #region ��ҳ��ѯ�����Ϣ
            PagingProceduresInfo prcoInfo = new PagingProceduresInfo();
            prcoInfo.IsCount = pageInfo.IsCount;
            prcoInfo.PageNumber = pageInfo.CurrentPage;
            prcoInfo.PageSize = pageInfo.PageLength;
            prcoInfo.Fields = " f.HoldFreezeLogoId,f.EntrustNumber,f.FreezeAmount,f.AccountHoldLogo,f.FreezeTypeLogo,f.ThawTime,f.FreezeTime ";
            prcoInfo.PK = "f.HoldFreezeLogoId";
            if (pageInfo.Sort == 0)
            {
                prcoInfo.Sort = " f.FreezeTime asc ";
            }
            else
            {
                prcoInfo.Sort = " f.FreezeTime desc ";
            }
            prcoInfo.Tables = "QH_HoldAccountFreezeTable as f  inner join QH_HoldAccountTable as a on a.AccountHoldLogoId=f.AccountHoldLogo ";

            #region ��װ�������
            StringBuilder sb = new StringBuilder();
            //1=1 and a.UserAccountDistributeLogo='010000000406' and a.TradeCurrencyType=1
            sb.Append(" a.UserAccountDistributeLogo='" + holdAccount + "'  ");

            if (currencyType != QueryType.QueryCurrencyType.ALL)
            {
                sb.Append(" and a.TradeCurrencyType='" + (int)currencyType + "'");
            }
            sb.AppendFormat(CommonDALBulidSQLScript.BuildWhereQueryBetwennTime(startTime, endTime, 30), "f.FreezeTime");
            //�����ѯ�Ķ������Ͳ��ǲ�ѯ����ʱ��������
            if (freezeType != QueryType.QueryFreezeType.ALL)
            {
                sb.AppendFormat("  AND f.FreezeTypeLogo='{0}'", (int)freezeType);
            }
            #endregion

            prcoInfo.Filter = sb.ToString();
            #endregion

            #region ִ�в�ѯ
            try
            {
                CommonDALOperate<QH_HoldAccountFreezeTableInfo> com = new CommonDALOperate<QH_HoldAccountFreezeTableInfo>();
                list = com.PagingQueryProcedures(prcoInfo, out total, dal.ReaderBind);
                // list = dal.PagingQH_HoldAccountFreezeByFilter(prcoInfo, out pageTotal);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
            #endregion

        }
        #endregion

        #region �����û�ID�Ͳ�ѯ�Ľ��׵Ļ������͡���ѯʱ��β�ѯ���ڻ��ֲֶ������ϸ
        /// <summary>
        /// Title�������û�ID�Ͳ�ѯ�Ľ��׵Ļ������͡���ѯʱ��β�ѯ���ڻ��ֲֶ������ϸ��Ϣ
        /// Desc.�������ʼʱ����߽���ʱ��Ϊnull�����ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        ///        �����ʼʱ����ڽ���ʱ�伴���ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="startTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="accountType">�˺�����(���ݿ���BD_AccountType�ľ���) �����Ϊ0ʱ��ѯ��Ӧ���ֻ��ֲ��˺����(3--֤ȯ�ɶ�����,9--�۹ɹɶ�����)</param>
        /// <param name="currencytype">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="freezeType">��������</param>
        /// <param name="pageInfo">��ҳ��Ϣ</param>
        /// <param name="total">��ҳ��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<QH_HoldAccountFreezeTableInfo> PagingQueryQH_HoldAccountFreezeByUserID(string userID, int accountType, DateTime? startTime, DateTime? endTime, QueryType.QueryCurrencyType currencyType, QueryType.QueryFreezeType freezeType, PagingInfo pageInfo, out int total, out string errorMsg)
        {
            #region ��ʼ������
            List<QH_HoldAccountFreezeTableInfo> list = null;
            QH_HoldAccountFreezeTableDal dal = new QH_HoldAccountFreezeTableDal();
            errorMsg = "";
            total = 0;
            #endregion

            #region ����û�ID���߷�ҳ����ʵ��Ϊ��ֱ�ӷ����󴫵ݲ�����ʾ �б�Ϊnull
            if (string.IsNullOrEmpty(userID))
            {
                errorMsg = "�û�ID����Ϊ�գ�";
                return list;
            }
            //����պ�Ҫ�����û���������֤�ڴ˼���
            if (pageInfo == null)
            {
                errorMsg = "��ҳ��Ϣ����Ϊ��!";
                return null;
            }
            #endregion

            #region ��ҳ��ѯ�����Ϣ
            PagingProceduresInfo prcoInfo = new PagingProceduresInfo();
            prcoInfo.IsCount = pageInfo.IsCount;
            prcoInfo.PageNumber = pageInfo.CurrentPage;
            prcoInfo.PageSize = pageInfo.PageLength;
            prcoInfo.Fields = " f.HoldFreezeLogoId,f.EntrustNumber,f.FreezeAmount,f.AccountHoldLogo,f.FreezeTypeLogo,f.ThawTime,f.FreezeTime ";
            prcoInfo.PK = "f.HoldFreezeLogoId";
            if (pageInfo.Sort == 0)
            {
                prcoInfo.Sort = " f.FreezeTime asc ";
            }
            else
            {
                prcoInfo.Sort = " f.FreezeTime desc ";
            }
            prcoInfo.Tables = "QH_HoldAccountFreezeTable as f  inner join QH_HoldAccountTable as a on a.AccountHoldLogoId=f.AccountHoldLogo ";


            #region ��װ�������
            StringBuilder sb = new StringBuilder();
            #region �ӻ����л�ȡ�ֲ��˺�
            List<UA_UserAccountAllocationTableInfo> userAccountInfo = AccountManager.Instance.GetUserHoldAccountFormUserCache(userID, accountType, 0);
            #region �������
            string userIDstr = "";
            foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
            {
                userIDstr += ",   '" + item.UserAccountDistributeLogo + "'";
            }
            if (!string.IsNullOrEmpty(userIDstr))
            {
                userIDstr = userIDstr.Substring(userIDstr.IndexOf(',') + 1);
            }

            #endregion
            #endregion

            #region ֱ�Ӵ����ݿ��л�ȡ
            if (!string.IsNullOrEmpty(userIDstr))
            {
                if (userIDstr.Split(',').Length > 1)
                {
                    sb.AppendFormat("  a.UserAccountDistributeLogo  in ( {0} )", userIDstr);
                }
                else
                {
                    sb.AppendFormat("  a.UserAccountDistributeLogo = {0} ", userIDstr);
                }
            }
            else //����ڻ����л�ȡ��������ֱ�Ӵ����ݿ��л�ȡ����
            {
                sb.Append("  a.UserAccountDistributeLogo =''");
                //if (accountType == 0)
                //{
                //    sb.Append("  a.UserAccountDistributeLogo  in ( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo in ( select accounttypelogo from BD_AccountType where atcid='" + (int)CommonObject.Types.AccountAttributionType.FuturesHold + "')  and userid='" + userID + "' )");
                //}
                //else
                //{
                //    sb.Append("  a.UserAccountDistributeLogo  in ( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo='" + accountType + "'  and userid='" + userID + "' )");
                //}
            }
            #endregion

            if (currencyType != QueryType.QueryCurrencyType.ALL)
            {
                sb.Append(" and a.TradeCurrencyType='" + (int)currencyType + "'");
            }
            sb.AppendFormat(CommonDALBulidSQLScript.BuildWhereQueryBetwennTime(startTime, endTime, 30), "f.FreezeTime");
            //�����ѯ�Ķ������Ͳ��ǲ�ѯ����ʱ��������
            if (freezeType != QueryType.QueryFreezeType.ALL)
            {
                sb.AppendFormat("  AND f.FreezeTypeLogo='{0}'", (int)freezeType);
            }
            #endregion

            prcoInfo.Filter = sb.ToString();
            #endregion

            #region ִ�в�ѯ
            try
            {
                CommonDALOperate<QH_HoldAccountFreezeTableInfo> com = new CommonDALOperate<QH_HoldAccountFreezeTableInfo>();
                list = com.PagingQueryProcedures(prcoInfo, out total, dal.ReaderBind);
                //list = dal.PagingQH_HoldAccountFreezeByFilter(prcoInfo, out pageTotal);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
            #endregion
        }
        #endregion
        #endregion

        #endregion

        #region �ڻ��ʽ��ʽ𶳽��ѯ

        #region �ڻ��ʽ���ϸ��ѯ

        #region �����û�ID��ѯ�û���ӵ�е��ڻ��ʽ��˺���ϸ
        /// <summary>
        /// �����û�ID��ѯ�û���ӵ�е��ڻ��ʽ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="accountType">�˺�����(���ݿ���BD_AccountType�ľ���) �����Ϊ0ʱ��ѯ��Ӧ���ֻ��ֲ��˺����(3--֤ȯ�ɶ�����,9--�۹ɹɶ�����)</param>
        /// <param name="currencytype">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<QH_CapitalAccountTableInfo> QueryQH_CapitalAccountByUserID(string userID, int accountType, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<QH_CapitalAccountTableInfo> list = new List<QH_CapitalAccountTableInfo>();
            #region ȡ���ݿ������
            //QH_CapitalAccountTableDal dal = new QH_CapitalAccountTableDal();
            //try
            //{
            //    list = dal.GetListByUserID(userID, type);
            //    //if (!Utils.IsNullOrEmpty(list))
            //    //{
            //    //    errorMsg = "���û�û���������������ص��û�ID�Ƿ���ȷ��";
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    errorMsg = ex.Message;
            //    LogHelper.WriteError(ex.ToString(), ex);
            //}
            #endregion
            try
            {
                #region ��ͨ���û�IDȡ���û����ڻ��ʽ��˺�
                // UA_UserAccountAllocationTableDal dal = new UA_UserAccountAllocationTableDal();
                List<UA_UserAccountAllocationTableInfo> userAccountInfo = new List<UA_UserAccountAllocationTableInfo>();
                #region �ӻ����л�ȡ�ʽ��˺�
                userAccountInfo = AccountManager.Instance.GetUserCapitalAccountFormUserCache(userID, accountType, 0);
                #endregion
                #region �����ݿ��л�ȡ�ʽ��˺�
                //if (accountType == 0)
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountTypeClass(userID, Types.AccountAttributionType.FuturesCapital);
                //}
                //else
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountType(userID, accountType);
                //}
                #endregion
                #endregion

                #region ���ڴ���л�ȡ����
                foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
                {
                    list.AddRange(GetQH_CapitalAccountInfoListFromMemory(item.UserAccountDistributeLogo, currencyType, out errorMsg));
                }
                #endregion
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }

            return list;
        }
        #endregion

        #region �����û�ID�������ѯ�û���ӵ�е��ڻ��ʽ��˺���ϸ
        /// <summary>
        /// �����û�ID�������ѯ�û���ӵ�е��ڻ��ʽ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="pwd">�û�������Ա������</param>
        /// <param name="accountType">�˺�����(���ݿ���BD_AccountType�ľ���) �����Ϊ0ʱ��ѯ��Ӧ���ֻ��ֲ��˺����(3--֤ȯ�ɶ�����,9--�۹ɹɶ�����)</param>
        /// <param name="currencytype">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<QH_CapitalAccountTableInfo> QueryQH_CapitalAccountByUserIDAndPwd(string userID, string pwd, int accountType, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<QH_CapitalAccountTableInfo> list = new List<QH_CapitalAccountTableInfo>();

            #region �����ݿ����л�ȡ����
            //try
            //{
            //    QH_CapitalAccountTableDal dal = new QH_CapitalAccountTableDal();
            //    list = dal.GetListByUserIDAndPwd(userID, pwd, type);
            //    //if (!Utils.IsNullOrEmpty(list))
            //    //{
            //    //    errorMsg = "���û�û���������������ص��û�ID�Ƿ���ȷ��";
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    errorMsg = ex.Message;
            //    LogHelper.WriteError(ex.ToString(), ex);
            //}
            #endregion

            try
            {
                #region ����û�����
                //update 2009-11-25 ���
                //var userDal = new UA_UserBasicInformationTableDal();
                //if (!userDal.Exists(userID, pwd))
                //{
                //    errorMsg = "�û����벻��ȷ������û�д��û������Ϣ��";
                //    return list;
                //}
                //==============
                //====new===
                if (!AccountManager.Instance.ExistsBasicUserPWDByUserId(userID, pwd))
                {
                    errorMsg = "�û����벻��ȷ������û�д��û������Ϣ��";
                    return list;
                }
                //==========end==========

                #endregion

                #region ��ͨ���û�IDȡ���û����ڻ��ʽ��˺�
                //UA_UserAccountAllocationTableDal dal = new UA_UserAccountAllocationTableDal();
                List<UA_UserAccountAllocationTableInfo> userAccountInfo = new List<UA_UserAccountAllocationTableInfo>();
                #region �ӻ����л�ȡ�ʽ��˺�
                userAccountInfo = AccountManager.Instance.GetUserCapitalAccountFormUserCache(userID, accountType, 0);
                #endregion
                #region �����ݿ��л�ȡ�ʽ��˺�
                //if (accountType == 0)
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountTypeClass(userID, Types.AccountAttributionType.FuturesCapital);
                //}
                //else
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountType(userID, accountType);
                //}
                #endregion
                #endregion

                #region ���ڴ���л�ȡ����

                foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
                {
                    list.AddRange(GetQH_CapitalAccountInfoListFromMemory(item.UserAccountDistributeLogo, currencyType, out errorMsg));
                }
                #endregion
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
        }
        #endregion

        #region �����ڻ��ʽ��˺Ų�ѯ�ڻ��ʽ��˺���ϸ
        /// <summary>
        /// �����ڻ��ʽ��˺Ų�ѯ�ڻ��ʽ��˺���ϸ
        /// </summary>
        ///<param name="qh_Cap_Account">�ڻ��ʽ��˺�</param>
        /// <param name="currencytype">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<QH_CapitalAccountTableInfo> QueryQH_CapitalAccountByAccount(string qh_Cap_Account, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<QH_CapitalAccountTableInfo> list = new List<QH_CapitalAccountTableInfo>();

            #region �����ݿ��л�ȡ����
            //try
            //{
            //    QH_CapitalAccountTableDal dal = new QH_CapitalAccountTableDal();
            //    list = dal.GetListByAccount(qh_Cap_Account, type);
            //    //if (!Utils.IsNullOrEmpty(list))
            //    //{
            //    //    errorMsg = "���û�û���������������ص��û�ID�Ƿ���ȷ��";
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    errorMsg = ex.Message;
            //    LogHelper.WriteError(ex.ToString(), ex);
            //}
            #endregion

            #region ���ڴ���л�ȡ����
            list = GetQH_CapitalAccountInfoListFromMemory(qh_Cap_Account, currencyType, out errorMsg);
            #endregion
            return list;
        }
        #endregion

        #region �����ʽ��˺źͲ�ѯ���׻������ʹ��ڴ���л�ȡ�ڻ��ʽ������б�
        /// <summary>
        /// �����ʽ��˺źͲ�ѯ���׻������ʹ��ڴ���л�ȡ�ڻ��ʽ������б�
        /// </summary>
        /// <param name="qh_Cap_Account">�ʽ��˺�</param>
        /// <param name="currencyType">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="errorMsg">��ѯ�쳣</param>
        /// <returns></returns>
        List<QH_CapitalAccountTableInfo> GetQH_CapitalAccountInfoListFromMemory(string qh_Cap_Account, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<QH_CapitalAccountTableInfo> list = new List<QH_CapitalAccountTableInfo>();
            var manager = MemoryDataManager.QHCapitalMemoryList;
            if (manager == null)
            {
                errorMsg = "��û�г�ʼ���������ݶ���";
                return list;
            }
            if (currencyType != QueryType.QueryCurrencyType.ALL)
            {
                QHCapitalMemoryTable table = manager.GetByCapitalAccountAndCurrencyType(qh_Cap_Account, (int)currencyType);
                if (table != null)
                {
                    var cap = table.Data;
                    list.Add(cap);
                }
            }
            else
            {
                for (int i = 1; i < 4; i++)
                {
                    QHCapitalMemoryTable table = manager.GetByCapitalAccountAndCurrencyType(qh_Cap_Account, i);
                    if (table != null)
                    {
                        var cap = table.Data;
                        list.Add(cap);
                    }
                }
            }
            return list;
        }
        #endregion

        #endregion

        #region �ڻ��ʽ𶳽���ϸ��ѯ
        #region ����ί�б�Ų�ѯ�����ʽ���ϸ
        /// <summary>
        /// ����ί�б�Ų�ѯ�����ʽ���ϸ
        /// </summary>
        /// <param name="entrustNo">ί�б��</param>
        /// <param name="freezeType">��ѯ��������</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<QH_CapitalAccountFreezeTableInfo> QueryQH_CapitalAccountFreezeByEntrustNo(string entrustNo, QueryType.QueryFreezeType freezeType, out string errorMsg)
        {
            errorMsg = "";
            List<QH_CapitalAccountFreezeTableInfo> list = null;
            if (string.IsNullOrEmpty(entrustNo))
            {
                errorMsg = "ί�б�Ų���Ϊ�գ�";
                return list;
            }
            QH_CapitalAccountFreezeTableDal dal = new QH_CapitalAccountFreezeTableDal();
            try
            {
                StringBuilder sb = new StringBuilder(" EntrustNumber='" + entrustNo.Trim() + "'");
                //�����ѯ�Ķ������Ͳ��ǲ�ѯ����ʱ��������
                if (freezeType != QueryType.QueryFreezeType.ALL)
                {
                    sb.Append("  AND FreezeTypeLogo='" + (int)freezeType + "'");
                }
                list = dal.GetListArray(sb.ToString());
                //if (!Utils.IsNullOrEmpty(list))
                //{
                //    errorMsg = "��ί�б��û���������������ص�ί�б���Ƿ���ȷ��";
                //}
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
        }
        #endregion

        #region �����û��ʽ��˺źͲ�ѯ�Ľ��׵Ļ������Ͳ�ѯ�ʽ𶳽����ϸ��Ϣ
        /// <summary>
        /// Title�������û��ʽ��˺źͲ�ѯ�Ľ��׵Ļ������Ͳ�ѯ�ʽ𶳽����ϸ��Ϣ
        /// Desc.�������ʼʱ����߽���ʱ��Ϊnull�����ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        ///        �����ʼʱ����ڽ���ʱ�伴���ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        /// </summary>
        /// <param name="account">�ʽ��˺�</param>
        /// <param name="startTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="currencyType">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="freezeType">��ѯ��������</param>
        /// <param name="pageInfo">��ҳ��Ϣ</param>
        /// <param name="total">��ҳ��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<QH_CapitalAccountFreezeTableInfo> PagingQueryQH_CapitalAccountFreezeByAccount(string account, DateTime? startTime, DateTime? endTime, QueryType.QueryCurrencyType currencyType, QueryType.QueryFreezeType freezeType, PagingInfo pageInfo, out int total, out string errorMsg)
        {
            #region ��ʼ������
            List<QH_CapitalAccountFreezeTableInfo> list = null;
            QH_CapitalAccountFreezeTableDal dal = new QH_CapitalAccountFreezeTableDal();
            errorMsg = "";
            total = 0;
            #endregion

            #region ����ʽ��˺Ż��߷�ҳ����ʵ��Ϊ��ֱ�ӷ����󴫵ݲ�����ʾ �б�Ϊnull
            if (string.IsNullOrEmpty(account))
            {
                errorMsg = "�ʽ��˺Ų���Ϊ�գ�";
                return list;
            }
            if (pageInfo == null)
            {
                errorMsg = "��ҳ��Ϣ����Ϊ��!";
                return null;
            }
            #endregion

            #region ��ҳ��ѯ�����Ϣ
            PagingProceduresInfo prcoInfo = new PagingProceduresInfo();
            prcoInfo.IsCount = pageInfo.IsCount;
            prcoInfo.PageNumber = pageInfo.CurrentPage;
            prcoInfo.PageSize = pageInfo.PageLength;
            prcoInfo.Fields = " f.CapitalFreezeLogoId,CapitalAccountLogo,FreezeTypeLogo,EntrustNumber,FreezeAmount,OweCosting,FreezeCost,ThawTime,FreezeTime ";
            prcoInfo.PK = "f.CapitalFreezeLogoId";
            if (pageInfo.Sort == 0)
            {
                prcoInfo.Sort = " f.FreezeTime asc ";
            }
            else
            {
                prcoInfo.Sort = " f.FreezeTime desc ";
            }
            prcoInfo.Tables = "dbo.QH_CapitalAccountFreezeTable as f  inner join QH_CapitalAccountTable as a on a.CapitalAccountLogoId=f.CapitalAccountLogo";

            #region ��װ�������
            StringBuilder sb = new StringBuilder();
            //1=1 and a.UserAccountDistributeLogo='010000000406' and a.TradeCurrencyType=1
            sb.Append(" a.UserAccountDistributeLogo='" + account + "'  ");
            //�����ѯ�Ļ������Ͳ��ǲ�ѯ����ʱ��������
            if (currencyType != QueryType.QueryCurrencyType.ALL)
            {
                sb.Append(" and a.TradeCurrencyType='" + (int)currencyType + "'");
            }
            sb.AppendFormat(CommonDALBulidSQLScript.BuildWhereQueryBetwennTime(startTime, endTime, 30), "f.FreezeTime");
            //�����ѯ�Ķ������Ͳ��ǲ�ѯ����ʱ��������
            if (freezeType != QueryType.QueryFreezeType.ALL)
            {
                sb.AppendFormat("  AND f.FreezeTypeLogo='{0}'", (int)freezeType);
            }
            #endregion

            prcoInfo.Filter = sb.ToString();
            #endregion

            #region ִ�в�ѯ
            try
            {
                CommonDALOperate<QH_CapitalAccountFreezeTableInfo> com = new CommonDALOperate<QH_CapitalAccountFreezeTableInfo>();
                list = com.PagingQueryProcedures(prcoInfo, out total, dal.ReaderBind);
                //list = dal.PagingQH_CapitalAccountFreezeByFilter(prcoInfo, out pageTotal);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
            #endregion

        }
        #endregion

        #region �����û��ʽ��˺źͲ�ѯ�Ľ��׵Ļ������Ͳ�ѯ�ʽ𶳽����ϸ��Ϣ
        /// <summary>
        /// Title�������û��ʽ��˺źͲ�ѯ�Ľ��׵Ļ������Ͳ�ѯ�ʽ𶳽����ϸ��Ϣ
        /// Desc.�������ʼʱ����߽���ʱ��Ϊnull�����ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        ///        �����ʼʱ����ڽ���ʱ�伴���ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="accountType">�˺�����(���ݿ���BD_AccountType�ľ���) �����Ϊ0ʱ��ѯ��Ӧ���ֻ��ֲ��˺����(3--֤ȯ�ɶ�����,9--�۹ɹɶ�����)</param>
        /// <param name="startTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="currencyType">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="freezeType">��ѯ��������</param>
        /// <param name="pageInfo">��ҳ��Ϣ</param>
        /// <param name="total">��ҳ��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<QH_CapitalAccountFreezeTableInfo> PagingQueryQH_CapitalAccountFreezeByUserID(string userID, int accountType, DateTime? startTime, DateTime? endTime, QueryType.QueryCurrencyType currencyType, QueryType.QueryFreezeType freezeType, PagingInfo pageInfo, out int total, out string errorMsg)
        {
            #region ��ʼ������
            List<QH_CapitalAccountFreezeTableInfo> list = null;
            QH_CapitalAccountFreezeTableDal dal = new QH_CapitalAccountFreezeTableDal();
            errorMsg = "";
            total = 0;
            #endregion

            #region ����û�ID���߷�ҳ����ʵ��Ϊ��ֱ�ӷ����󴫵ݲ�����ʾ �б�Ϊnull
            if (string.IsNullOrEmpty(userID))
            {
                errorMsg = "�û�ID����Ϊ�գ�";
                return list;
            }
            //����պ�Ҫ�����û���������֤�ڴ˼���
            if (pageInfo == null)
            {
                errorMsg = "��ҳ��Ϣ����Ϊ��!";
                return null;
            }
            #endregion

            #region ��ҳ��ѯ�����Ϣ
            PagingProceduresInfo prcoInfo = new PagingProceduresInfo();
            prcoInfo.IsCount = pageInfo.IsCount;
            prcoInfo.PageNumber = pageInfo.CurrentPage;
            prcoInfo.PageSize = pageInfo.PageLength;
            prcoInfo.Fields = " f.CapitalFreezeLogoId,CapitalAccountLogo,FreezeTypeLogo,EntrustNumber,FreezeAmount,OweCosting,FreezeCost,ThawTime,FreezeTime ";
            prcoInfo.PK = "f.CapitalFreezeLogoId";
            if (pageInfo.Sort == 0)
            {
                prcoInfo.Sort = " f.FreezeTime asc ";
            }
            else
            {
                prcoInfo.Sort = " f.FreezeTime desc ";
            }
            prcoInfo.Tables = "dbo.QH_CapitalAccountFreezeTable as f  inner join QH_CapitalAccountTable as a on a.CapitalAccountLogoId=f.CapitalAccountLogo";

            #region ��װ�������
            StringBuilder sb = new StringBuilder();
            #region �ӻ����л�ȡ�ֲ��˺�
            List<UA_UserAccountAllocationTableInfo> userAccountInfo = AccountManager.Instance.GetUserCapitalAccountFormUserCache(userID, accountType, 0);
            #region �������
            string userIDstr = "";
            foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
            {
                userIDstr += ",   '" + item.UserAccountDistributeLogo + "'";
            }
            if (!string.IsNullOrEmpty(userIDstr))
            {
                userIDstr = userIDstr.Substring(userIDstr.IndexOf(',') + 1);
            }

            #endregion
            #endregion

            #region ֱ�Ӵ����ݿ��л�ȡ
            if (!string.IsNullOrEmpty(userIDstr))
            {
                if (userIDstr.Split(',').Length > 1)
                {
                    sb.AppendFormat("  a.UserAccountDistributeLogo  in ( {0} )", userIDstr);
                }
                else
                {
                    sb.AppendFormat("  a.UserAccountDistributeLogo ={0} ", userIDstr);
                }
            }
            else //����ڻ����л�ȡ��������ֱ�Ӵ����ݿ��л�ȡ����
            {
                //���ﲻ�ٲ������ݿ�
                sb.Append("  a.UserAccountDistributeLogo ='' ");
                //if (accountType == 0)
                //{
                //    sb.Append("  a.UserAccountDistributeLogo  in ( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo in ( select accounttypelogo from BD_AccountType where atcid='" + (int)CommonObject.Types.AccountAttributionType.FuturesCapital + "')  and userid='" + userID + "' )");
                //}
                //else
                //{
                //    sb.Append("  a.UserAccountDistributeLogo  in ( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo='" + accountType + "'  and userid='" + userID + "' )");
                //}
            }

            #endregion

            //�����ѯ�Ļ������Ͳ��ǲ�ѯ����ʱ��������
            if (currencyType != QueryType.QueryCurrencyType.ALL)
            {
                sb.Append(" and a.TradeCurrencyType='" + (int)currencyType + "'");
            }
            sb.AppendFormat(CommonDALBulidSQLScript.BuildWhereQueryBetwennTime(startTime, endTime, 30), "f.FreezeTime");
            //�����ѯ�Ķ������Ͳ��ǲ�ѯ����ʱ��������
            if (freezeType != QueryType.QueryFreezeType.ALL)
            {
                sb.AppendFormat("  AND f.FreezeTypeLogo='{0}'", (int)freezeType);
            }
            #endregion

            prcoInfo.Filter = sb.ToString();
            #endregion

            #region ִ�в�ѯ
            try
            {
                CommonDALOperate<QH_CapitalAccountFreezeTableInfo> com = new CommonDALOperate<QH_CapitalAccountFreezeTableInfo>();
                list = com.PagingQueryProcedures(prcoInfo, out total, dal.ReaderBind);
                //list = dal.PagingQH_CapitalAccountFreezeByFilter(prcoInfo, out pageTotal);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
            #endregion

        }
        #endregion
        #endregion

        #endregion

        #region �ֻ��ʽ��ʽ𶳽��ѯ

        #region �ֻ��ʽ���ϸ��ѯ
        #region ���ݡ��û�ID��ѯ���û���ӵ�е��ֻ��ʽ��˺���ϸ
        /// <summary>
        /// ���ݡ��û�ID��ѯ���û���ӵ�е�public���ʽ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="accountType">�˺�����(���ݿ���BD_AccountType�ľ���) �����Ϊ0ʱ��ѯ��Ӧ���ֻ��ֲ��˺����(3--֤ȯ�ɶ�����,9--�۹ɹɶ�����)</param>
        /// <param name="currencyType">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<XH_CapitalAccountTableInfo> QueryXH_CapitalAccountByUserID(string userID, int accountType, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<XH_CapitalAccountTableInfo> list = new List<XH_CapitalAccountTableInfo>();
            #region �����ݿ��л�ȡ����
            //try
            //{
            //    XH_CapitalAccountTableDal dal = new XH_CapitalAccountTableDal();
            //    list = dal.GetListByUserID(userID, type);
            //}
            //catch (Exception ex)
            //{
            //    errorMsg = ex.Message;
            //    LogHelper.WriteError(ex.ToString(), ex);
            //}
            #endregion
            try
            {
                #region ��ͨ���û�IDȡ���û����ֻ��ʽ��˺�
                //UA_UserAccountAllocationTableDal dal = new UA_UserAccountAllocationTableDal();
                List<UA_UserAccountAllocationTableInfo> userAccountInfo = new List<UA_UserAccountAllocationTableInfo>();
                #region �ӻ����л�ȡ�ʽ��˺�
                userAccountInfo = AccountManager.Instance.GetUserCapitalAccountFormUserCache(userID, accountType, 1);
                #endregion
                #region �����ݿ��л�ȡ�ʽ��˺�
                //if (accountType == 0)
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountTypeClass(userID, Types.AccountAttributionType.SpotCapital);
                //}
                //else
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountType(userID, accountType);
                //}
                #endregion
                #endregion
                #region ���ڴ���л�ȡ����
                foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
                {
                    list.AddRange(GetXH_CapitalAccountInfoListFromMemory(item.UserAccountDistributeLogo, currencyType, out errorMsg));
                }
                #endregion
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;

        }
        #endregion

        #region ���ݡ��û�ID�����롿��ѯ�û���ӵ�е��ֻ��ʽ��˺���ϸ
        /// <summary>
        /// ���ݡ��û�ID�����롿��ѯ�û���ӵ�е��ֻ��ʽ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="pwd">�û�������Ա������</param>
        /// <param name="accountType">�˺�����(���ݿ���BD_AccountType�ľ���) �����Ϊ0ʱ��ѯ��Ӧ���ֻ��ֲ��˺����(3--֤ȯ�ɶ�����,9--�۹ɹɶ�����)</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<XH_CapitalAccountTableInfo> QueryXH_CapitalAccountByUserIDAndPwd(string userID, string pwd, int accountType, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<XH_CapitalAccountTableInfo> list = new List<XH_CapitalAccountTableInfo>();
            #region �����ݿ��л�ȡ����
            //try
            // { 
            //     XH_CapitalAccountTableDal dal = new XH_CapitalAccountTableDal();
            //     list = dal.GetListByUserIDAndPwd(userID, pwd, type);
            // }
            // catch (Exception ex)
            // {
            //     errorMsg = ex.Message;
            //     LogHelper.WriteError(ex.ToString(), ex);
            // }
            #endregion

            try
            {
                #region ����ü�����
                //update 2009-11-25 ���
                //var userDal = new UA_UserBasicInformationTableDal();
                //if (!userDal.Exists(userID, pwd))
                //{
                //    errorMsg = "�û����벻��ȷ������û�д��û������Ϣ��";
                //    return list;
                //}
                //==============
                //====new===
                if (!AccountManager.Instance.ExistsBasicUserPWDByUserId(userID, pwd))
                {
                    errorMsg = "�û����벻��ȷ������û�д��û������Ϣ��";
                    return list;
                }
                //==========end==========

                #endregion

                #region ��ͨ���û�IDȡ���û����ֻ��ʽ��˺�
                //UA_UserAccountAllocationTableDal dal = new UA_UserAccountAllocationTableDal();
                List<UA_UserAccountAllocationTableInfo> userAccountInfo = new List<UA_UserAccountAllocationTableInfo>();
                #region �ӻ����л�ȡ�ʽ��˺�
                userAccountInfo = AccountManager.Instance.GetUserCapitalAccountFormUserCache(userID, accountType, 1);
                #endregion
                #region �����ݿ��л�ȡ�ʽ��˺�
                //if (accountType == 0)
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountTypeClass(userID, Types.AccountAttributionType.SpotCapital);
                //}
                //else
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountType(userID, accountType);
                //}
                #endregion
                #endregion

                #region ���ڴ���л�ȡ����

                foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
                {
                    list.AddRange(GetXH_CapitalAccountInfoListFromMemory(item.UserAccountDistributeLogo, currencyType, out errorMsg));
                }
                #endregion
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
        }

        #endregion

        #region ���ݡ��ֻ��ʽ��˺š���ѯ�ֻ��ʽ��˺���ϸ
        /// <summary>
        /// ���ݡ��ֻ��ʽ��˺š���ѯ�ֻ��ʽ��˺���ϸ
        /// </summary>
        ///<param name="xh_Cap_Account">�ֻ��ʽ��˺�</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<XH_CapitalAccountTableInfo> QueryXH_CapitalAccountByAccount(string xh_Cap_Account, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<XH_CapitalAccountTableInfo> list = new List<XH_CapitalAccountTableInfo>();

            #region �����ݿ��л�ȡ����
            //try
            //{
            //    XH_CapitalAccountTableDal dal = new XH_CapitalAccountTableDal();
            //    list = dal.GetListByAccount(xh_Cap_Account, type);
            //}
            //catch (Exception ex)
            //{
            //    errorMsg = ex.Message;
            //    LogHelper.WriteError(ex.ToString(), ex);
            //}
            #endregion
            try
            {
                #region ���ڴ���л�ȡ����
                list = GetXH_CapitalAccountInfoListFromMemory(xh_Cap_Account, currencyType, out errorMsg);
                #endregion
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
        }
        #endregion

        #region �����ʽ��˺źͲ�ѯ���׻������ʹ��ڴ���л�ȡ�ֻ��ʽ������б�
        /// <summary>
        /// �����ʽ��˺źͲ�ѯ���׻������ʹ��ڴ���л�ȡ�����б�
        /// </summary>
        /// <param name="qh_Cap_Account">�ʽ��˺�</param>
        /// <param name="currencyType">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="errorMsg">��ѯ�쳣</param>
        /// <returns></returns>
        List<XH_CapitalAccountTableInfo> GetXH_CapitalAccountInfoListFromMemory(string xh_Cap_Account, QueryType.QueryCurrencyType currencyType, out string errorMsg)
        {
            errorMsg = "";
            List<XH_CapitalAccountTableInfo> list = new List<XH_CapitalAccountTableInfo>();
            var manager = MemoryDataManager.XHCapitalMemoryList;
            if (manager == null)
            {
                errorMsg = "��û�г�ʼ���������ݶ���";
                return list;
            }
            if (currencyType != QueryType.QueryCurrencyType.ALL)
            {
                XHCapitalMemoryTable table = manager.GetByCapitalAccountAndCurrencyType(xh_Cap_Account, (int)currencyType);
                if (table != null)
                {
                    var cap = table.Data;
                    list.Add(cap);
                }
            }
            else
            {
                for (int i = 1; i < 4; i++)
                {
                    XHCapitalMemoryTable table = manager.GetByCapitalAccountAndCurrencyType(xh_Cap_Account, i);
                    if (table != null)
                    {
                        var cap = table.Data;
                        list.Add(cap);
                    }
                }
            }
            return list;
        }
        #endregion

        #endregion

        #region �ֻ��ʽ𶳽���ϸ��ѯ

        #region ����ί�б�Ų�ѯ���ֻ��ʽ𶳽����ϸ
        /// <summary>
        /// ����ί�б�Ų�ѯ���ֻ��ʽ𶳽����ϸ
        /// </summary>
        /// <param name="entrustNo">ί�б��</param>
        /// <param name="freezeType">��������</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<XH_CapitalAccountFreezeTableInfo> QueryXH_CapitalAccountFreezeByEntrustNo(string entrustNo, QueryType.QueryFreezeType freezeType, out string errorMsg)
        {
            errorMsg = "";
            List<XH_CapitalAccountFreezeTableInfo> list = null;
            if (string.IsNullOrEmpty(entrustNo))
            {
                errorMsg = "ί�б�Ų���Ϊ�գ�";
                return list;
            }
            XH_CapitalAccountFreezeTableDal dal = new XH_CapitalAccountFreezeTableDal();
            try
            {
                StringBuilder sb = new StringBuilder("  EntrustNumber='" + entrustNo.Trim() + "'");
                //�����ѯ�Ķ������Ͳ��ǲ�ѯ����ʱ��������
                if (freezeType != QueryType.QueryFreezeType.ALL)
                {
                    sb.AppendFormat("  AND FreezeTypeLogo='{0}'", (int)freezeType);
                }
                list = dal.GetListArray(sb.ToString());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
        }
        #endregion

        #region �����û��ʽ��˺źͲ�ѯ�Ľ��׵Ļ������͡���ѯʱ��β�ѯ���ֻ��ʽ𶳽����ϸ��Ϣ
        /// <summary>
        /// Title�������û��ʽ��˺źͲ�ѯ�Ľ��׵Ļ������͡���ѯʱ��β�ѯ���ֻ��ʽ𶳽����ϸ��Ϣ
        /// Desc.�������ʼʱ����߽���ʱ��Ϊnull�����ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        ///        �����ʼʱ����ڽ���ʱ�伴���ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        /// </summary>
        /// <param name="account">�ʽ��˺�</param>
        /// <param name="startTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="currencyType">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="freezeType">��������</param>
        /// <param name="pageInfo">��ҳ��Ϣ</param>
        /// <param name="total">��ҳ��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<XH_CapitalAccountFreezeTableInfo> PagingQueryXH_CapitalAccountFreezeByAccount(string account, DateTime? startTime, DateTime? endTime, QueryType.QueryCurrencyType currencyType, QueryType.QueryFreezeType freezeType, PagingInfo pageInfo, out int total, out string errorMsg)
        {
            #region ��ʼ������
            List<XH_CapitalAccountFreezeTableInfo> list = null;
            XH_CapitalAccountFreezeTableDal dal = new XH_CapitalAccountFreezeTableDal();
            errorMsg = "";
            total = 0;
            #endregion

            #region ����ʽ��˺Ż��߷�ҳ����ʵ��Ϊ��ֱ�ӷ����󴫵ݲ�����ʾ �б�Ϊnull
            if (string.IsNullOrEmpty(account))
            {
                errorMsg = "�ʽ��˺Ų���Ϊ�գ�";
                return list;
            }
            if (pageInfo == null)
            {
                errorMsg = "��ҳ��Ϣ����Ϊ��!";
                return null;
            }
            #endregion

            #region ��ҳ��ѯ�����Ϣ
            PagingProceduresInfo prcoInfo = new PagingProceduresInfo();
            prcoInfo.IsCount = pageInfo.IsCount;
            prcoInfo.PageNumber = pageInfo.CurrentPage;
            prcoInfo.PageSize = pageInfo.PageLength;
            prcoInfo.Fields = " f.CapitalFreezeLogoId,f.CapitalAccountLogo,f.FreezeTypeLogo,f.EntrustNumber,f.FreezeCapitalAmount,f.FreezeCost,f.OweCosting,f.ThawTime,f.FreezeTime ";
            prcoInfo.PK = "f.CapitalFreezeLogoId";
            if (pageInfo.Sort == 0)
            {
                prcoInfo.Sort = " f.FreezeTime asc ";
            }
            else
            {
                prcoInfo.Sort = " f.FreezeTime desc ";
            }
            prcoInfo.Tables = "dbo.XH_CapitalAccountFreezeTable as f  inner join XH_CapitalAccountTable as a on a.CapitalAccountLogo=f.CapitalAccountLogo";

            #region ��װ�������
            StringBuilder sb = new StringBuilder();
            //1=1 and a.UserAccountDistributeLogo='010000000406' and a.TradeCurrencyType=1
            sb.Append(" a.UserAccountDistributeLogo='" + account + "'  ");

            if (currencyType != QueryType.QueryCurrencyType.ALL)
            {
                sb.Append(" and a.TradeCurrencyType='" + (int)currencyType + "'");
            }
            sb.AppendFormat(CommonDALBulidSQLScript.BuildWhereQueryBetwennTime(startTime, endTime, 30), "f.FreezeTime");
            //�����ѯ�Ķ������Ͳ��ǲ�ѯ����ʱ��������
            if (freezeType != QueryType.QueryFreezeType.ALL)
            {
                sb.AppendFormat("  AND f.FreezeTypeLogo='{0}'", (int)freezeType);
            }
            #endregion

            prcoInfo.Filter = sb.ToString();
            #endregion

            #region ִ�в�ѯ
            try
            {
                CommonDALOperate<XH_CapitalAccountFreezeTableInfo> com = new CommonDALOperate<XH_CapitalAccountFreezeTableInfo>();
                list = com.PagingQueryProcedures(prcoInfo, out total, dal.ReaderBind);
                //list = dal.PagingXH_CapitalAccountFreezeByFilter(prcoInfo, out pageTotal);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
            #endregion

        }

        #endregion

        #region �����û�ID�Ͳ�ѯ�Ľ��׵Ļ������͡���ѯʱ��β�ѯ���ֻ��ʽ𶳽����ϸ
        /// <summary>
        /// Title�������û�ID�Ͳ�ѯ�Ľ��׵Ļ������͡���ѯʱ��β�ѯ���ֻ��ʽ𶳽����ϸ��Ϣ
        /// Desc.�������ʼʱ����߽���ʱ��Ϊnull�����ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        ///        �����ʼʱ����ڽ���ʱ�伴���ص�ǰʱ����ǰһ���£�30�죩��ʱ��β�ѯ
        /// </summary>
        /// <param name="userID">�ʽ��˺�</param>
        /// <param name="accountType">�˺�����(���ݿ���BD_AccountType�ľ���) �����Ϊ0ʱ��ѯ��Ӧ���ֻ��ֲ��˺����(3--֤ȯ�ɶ�����,9--�۹ɹɶ�����)</param>
        /// <param name="startTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="currencyType">���Ҳ�ѯ���ͣ�ALL-0��RMB-1��HK-2��US-3��</param>
        /// <param name="freezeType">��������</param>
        /// <param name="pageInfo">��ҳ��Ϣ</param>
        /// <param name="total">��ҳ��</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<XH_CapitalAccountFreezeTableInfo> PagingQueryXH_CapitalAccountFreezeByUserID(string userID, int accountType, DateTime? startTime, DateTime? endTime, QueryType.QueryCurrencyType currencyType, QueryType.QueryFreezeType freezeType, PagingInfo pageInfo, out int total, out string errorMsg)
        {
            #region ��ʼ������
            List<XH_CapitalAccountFreezeTableInfo> list = null;
            XH_CapitalAccountFreezeTableDal dal = new XH_CapitalAccountFreezeTableDal();
            errorMsg = "";
            total = 0;
            #endregion

            #region ����û�ID���߷�ҳ����ʵ��Ϊ��ֱ�ӷ����󴫵ݲ�����ʾ �б�Ϊnull
            if (string.IsNullOrEmpty(userID))
            {
                errorMsg = "�û�ID����Ϊ�գ�";
                return list;
            }
            //����պ�Ҫ�����û���������֤�ڴ˼���
            if (pageInfo == null)
            {
                errorMsg = "��ҳ��Ϣ����Ϊ��!";
                return null;
            }
            #endregion

            #region ��ҳ��ѯ�����Ϣ
            PagingProceduresInfo prcoInfo = new PagingProceduresInfo();
            prcoInfo.IsCount = pageInfo.IsCount;
            prcoInfo.PageNumber = pageInfo.CurrentPage;
            prcoInfo.PageSize = pageInfo.PageLength;
            prcoInfo.Fields = " f.CapitalFreezeLogoId,f.CapitalAccountLogo,f.FreezeTypeLogo,f.EntrustNumber,f.FreezeCapitalAmount,f.FreezeCost,f.OweCosting,f.ThawTime,f.FreezeTime ";
            prcoInfo.PK = "f.CapitalFreezeLogoId";
            if (pageInfo.Sort == 0)
            {
                prcoInfo.Sort = " f.FreezeTime asc ";
            }
            else
            {
                prcoInfo.Sort = " f.FreezeTime desc ";
            }
            prcoInfo.Tables = "dbo.XH_CapitalAccountFreezeTable as f  inner join XH_CapitalAccountTable as a on a.CapitalAccountLogo=f.CapitalAccountLogo";


            #region ��װ�������
            StringBuilder sb = new StringBuilder();
            #region �ӻ����л�ȡ�ֲ��˺�
            List<UA_UserAccountAllocationTableInfo> userAccountInfo = AccountManager.Instance.GetUserCapitalAccountFormUserCache(userID, accountType, 1);
            #region �������
            string userIDstr = "";
            foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
            {
                userIDstr += ",   '" + item.UserAccountDistributeLogo + "'";
            }
            if (!string.IsNullOrEmpty(userIDstr))
            {
                userIDstr = userIDstr.Substring(userIDstr.IndexOf(',') + 1);
            }

            #endregion
            #endregion

            #region ֱ�Ӵ����ݿ��л�ȡ
            if (!string.IsNullOrEmpty(userIDstr))
            {
                if (userIDstr.Split(',').Length > 1)
                {
                    sb.AppendFormat("  a.UserAccountDistributeLogo  in ( {0} )", userIDstr);
                }
                else
                {
                    sb.AppendFormat("  a.UserAccountDistributeLogo ={0} ", userIDstr);
                }
            }
            else //����ڻ����л�ȡ��������ֱ�Ӵ����ݿ��л�ȡ����
            {
                //���ﲻ�ٲ������ݿ�
                sb.Append("  a.UserAccountDistributeLogo ='' ");
                //if (accountType == 0)
                //{
                //    sb.Append("  a.UserAccountDistributeLogo  in ( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo in ( select accounttypelogo from BD_AccountType where atcid='" + (int)CommonObject.Types.AccountAttributionType.SpotCapital + "')  and userid='" + userID + "' )");
                //}
                //else
                //{
                //    sb.Append("  a.UserAccountDistributeLogo  in ( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo='" + accountType + "'  and userid='" + userID + "' )");
                //}
            }
            #endregion


            if (currencyType != QueryType.QueryCurrencyType.ALL)
            {
                sb.Append(" and a.TradeCurrencyType='" + (int)currencyType + "'");
            }
            sb.AppendFormat(CommonDALBulidSQLScript.BuildWhereQueryBetwennTime(startTime, endTime, 30), "f.FreezeTime");
            //�����ѯ�Ķ������Ͳ��ǲ�ѯ����ʱ��������
            if (freezeType != QueryType.QueryFreezeType.ALL)
            {
                sb.AppendFormat("  AND f.FreezeTypeLogo='{0}'", (int)freezeType);
            }
            #endregion

            prcoInfo.Filter = sb.ToString();
            #endregion

            #region ִ�в�ѯ
            try
            {
                CommonDALOperate<XH_CapitalAccountFreezeTableInfo> com = new CommonDALOperate<XH_CapitalAccountFreezeTableInfo>();
                list = com.PagingQueryProcedures(prcoInfo, out total, dal.ReaderBind);
                // list = dal.PagingXH_CapitalAccountFreezeByFilter(prcoInfo, out pageTotal);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }
            return list;
            #endregion
        }
        #endregion
        #endregion

        #endregion

        #region �����û�ID��ѯ������ϸ
        /// <summary>
        /// �����û�ID��ѯ������ϸ 
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="errorMsg">��ѯ����������ʾ��Ϣ</param>
        /// <returns></returns>
        public List<UA_BankAccountTableInfo> UA_BankAccountByUserID(string userId, out string errorMsg)
        {
            List<UA_BankAccountTableInfo> result = null;
            errorMsg = null;
            try
            {
                StringBuilder sb = new StringBuilder("");
                if (string.IsNullOrEmpty(userId))
                {
                    errorMsg = "������Ҫ��ѯ���û�ID��";
                }
                else
                {
                    #region �ӻ����л�ȡ�û������˺�
                    UA_UserAccountAllocationTableInfo account = AccountManager.Instance.GetAccountByUserIDAndAccountType(userId, (int)Types.AccountType.BankAccount);
                    #endregion

                    #region �����ݿ��в�ѯ
                    if (account != null)
                    {
                        result = UA_BankAccountByBankAccount(account.UserAccountDistributeLogo, out errorMsg);
                        //sb.AppendFormat("   [UserAccountDistributeLogo] ='{0}' ", account.UserAccountDistributeLogo);
                    }
                    else
                    {
                        errorMsg = "�Բ��𣬲�ѯʧ�ܣ�ʧ��ԭ��Ϊ������Ա��Ϣ�����ڣ�";
                        return null;
                    }
                    //sb.Append("   [UserAccountDistributeLogo] in( ");
                    //sb.Append(" select useraccountdistributelogo from UA_UserAccountAllocationTable ");
                    //sb.Append("  where accounttypelogo=1 and userid='" + userId.Trim() + "')   ");
                    //UA_BankAccountTableDal dal = new UA_BankAccountTableDal();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
            }
            return result;
        }
        #endregion

        #region ���������˺Ų�ѯ������ϸ
        /// <summary>
        /// ���������˺Ų�ѯ������ϸ 
        /// </summary>
        /// <param name="bankAccount">�����˺�</param>
        /// <param name="errorMsg">��ѯ����������ʾ��Ϣ</param>
        /// <returns></returns>
        public List<UA_BankAccountTableInfo> UA_BankAccountByBankAccount(string bankAccount, out string errorMsg)
        {
            List<UA_BankAccountTableInfo> result = null;
            errorMsg = null;
            try
            {
                if (string.IsNullOrEmpty(bankAccount))
                {
                    errorMsg = "������Ҫ��ѯ�������˻���";
                }
                else
                {
                    UA_BankAccountTableDal dal = new UA_BankAccountTableDal();
                    result = dal.GetListArray("  UserAccountDistributeLogo='" + bankAccount.Trim() + "'");
                    if (Utils.IsNullOrEmpty(result))
                    {
                        errorMsg = "�Բ��𣬲�ѯʧ�ܣ�ʧ��ԭ��Ϊ������Ա�Ĳ����������˺���Ϣ  ��";
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
            }
            return result;
        }
        #endregion

        #endregion

        #region �ڻ��ʽ���ˮ���̺����㣩��ѯ
        /// <summary>
        /// Title:�����û�ID�������ѯ�ڻ���Ӧ�̺���ʽ�������ˮ
        /// Desc.:�˷�����ѯ���ص����̺�������ʽ���ˮ����.
        ///       �����ʼ���ںͽ�������Ϊnull/"",Ĭ�ϲ�ѯ��ǰ��־ǰһ�����ڵ������ʽ���ˮ��
        ///       �����ʼ���ںͽ������ڶ�Ϊ�Ƿ�������ͬ��Ĭ�ϲ�ѯ��ǰ��־ǰһ�����ڵ������ʽ���ˮ��
        ///       Ϊ���ṩ��ѯЧ�ʣ��������Ϊ��ʵ�ַ�ҳ���ڲ�ѯʱ����Ĭ�ϵ�
        ///       �Ƿ񷵻ط�ҳ������Ϊfalse����������ҳ��
        /// Create By:���
        /// Create Date:2009-12-04
        /// Desc: ���Ӳ����˻�����ID���������ֹ�ָ�ڻ�����Ʒ�ڻ�
        /// Update by: ����
        /// Update Date: 2010-03-09
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="startTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="currencyType">��������</param>
        /// <param name="pageInfo">��ҳ��Ϣʵ��</param>
        /// <param name="accountType">�˻�����ID</param>
        /// <param name="total">��ҳ����</param>
        /// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        /// <returns></returns>
        public List<QH_TradeCapitalFlowDetailInfo> PagingQueryQH_CapitalFlowDetailByAccount(string userID, string pwd, DateTime? startTime, DateTime? endTime, QueryType.QueryCurrencyType currencyType, PagingInfo pageInfo, int accountType, out int total, out string errorMsg)
        {
            #region ��ʼ������
            List<QH_TradeCapitalFlowDetailInfo> list = new List<QH_TradeCapitalFlowDetailInfo>();
            errorMsg = "";
            total = 0;
            #endregion

            try
            {
                #region ����û�ID���߷�ҳ����ʵ��Ϊ��ֱ�ӷ����󴫵ݲ�����ʾ �б�Ϊnull
                if (string.IsNullOrEmpty(userID))
                {
                    errorMsg = "�û�ID����Ϊ�գ�";
                    return list;
                }
                //����պ�Ҫ�����û���������֤�ڴ˼���
                if (pageInfo == null)
                {
                    errorMsg = "��ҳ��Ϣ����Ϊ��!";
                    return null;
                }
                #endregion

                #region ����û�����
                if (!AccountManager.Instance.ExistsBasicUserPWDByUserId(userID, pwd))
                {
                    errorMsg = "�û����벻��ȷ������û�д��û������Ϣ��";
                    return list;
                }
                #endregion

                #region ��ͨ���û�IDȡ���û����ڻ��ʽ��˺�
                List<UA_UserAccountAllocationTableInfo> userAccountInfo = new List<UA_UserAccountAllocationTableInfo>();
                #region ���Ϊ0�Ͳ�ѯ����µ����п����������˺�
                #region �ӻ����л�ȡ�˺�
                //userAccountInfo = AccountManager.Instance.GetUserCapitalAccountFormUserCache(userID, (int)Types.AccountType.StockFuturesCapital, 0);
                userAccountInfo = AccountManager.Instance.GetUserCapitalAccountFormUserCache(userID, accountType, 0);
                #endregion
                #endregion

                #region ֱ�Ӵ����ݿ��л�ȡ
                //if (accountType == 0)
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountTypeClass(userID, Types.AccountAttributionType.FuturesCapital);
                //}
                //else
                //{
                //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountType(userID, accountType);
                //}
                #endregion
                #endregion


                #region ��ҳ��ѯ�����Ϣ
                PagingProceduresInfo prcoInfo = new PagingProceduresInfo();
                prcoInfo.IsCount = pageInfo.IsCount;
                prcoInfo.PageNumber = pageInfo.CurrentPage;
                prcoInfo.PageSize = pageInfo.PageLength;
                prcoInfo.Fields = " TradeID,UserCapitalAccount,FlowTypes,Margin,TradeProceduresFee,ProfitLoss,OtherCose,FlowTotal,CurrencyType,CreateDateTime ";
                prcoInfo.PK = "TradeID";
                if (pageInfo.Sort == 0)
                {
                    prcoInfo.Sort = " CreateDateTime asc ";
                }
                else
                {
                    prcoInfo.Sort = " CreateDateTime desc ";
                }
                prcoInfo.Tables = " dbo.QH_TradeCapitalFlowDetail ";

                #region ��װ�������
                StringBuilder sb = new StringBuilder();
                string userIDstr = "";
                foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
                {
                    userIDstr += ",   '" + item.UserAccountDistributeLogo + "'";
                }
                if (!string.IsNullOrEmpty(userIDstr))
                {
                    userIDstr = userIDstr.Substring(userIDstr.IndexOf(',') + 1);
                }



                if (!string.IsNullOrEmpty(userIDstr))
                {
                    if (userIDstr.Split(',').Length > 1)
                    {
                        sb.AppendFormat("  UserCapitalAccount  in ( {0} )", userIDstr);
                    }
                    else
                    {
                        sb.AppendFormat("  UserCapitalAccount ={0} ", userIDstr);
                    }
                }
                else //����ڻ����л�ȡ��������ֱ�Ӵ����ݿ��л�ȡ����
                {
                    //���ﲻ�ٲ������ݿ�
                    sb.Append("  UserCapitalAccount ='' ");
                    //if (accountType == 0)
                    //{
                    //    sb.Append("  a.UserAccountDistributeLogo  in ( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo in ( select accounttypelogo from BD_AccountType where atcid='" + (int)CommonObject.Types.AccountAttributionType.SpotCapital + "')  and userid='" + userID + "' )");
                    //}
                    //else
                    //{
                    //    sb.Append("  a.UserAccountDistributeLogo  in ( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo='" + accountType + "'  and userid='" + userID + "' )");
                    //}
                }

                if (currencyType != QueryType.QueryCurrencyType.ALL)
                {
                    sb.Append(" and CurrencyType='" + (int)currencyType + "'");
                }
                sb.AppendFormat(CommonDALBulidSQLScript.BuildWhereQueryBetwennTime(startTime, endTime, 30), "CreateDateTime");

                prcoInfo.Filter = sb.ToString();
                #endregion

                #endregion

                #region ִ�в�ѯ
                try
                {
                    QH_TradeCapitalFlowDetailDal dal = new QH_TradeCapitalFlowDetailDal();
                    CommonDALOperate<QH_TradeCapitalFlowDetailInfo> com = new CommonDALOperate<QH_TradeCapitalFlowDetailInfo>();
                    list = com.PagingQueryProcedures(prcoInfo, out total, dal.ReaderBind);
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                    LogHelper.WriteError(ex.ToString(), ex);
                }
                return list;
                #endregion

            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.WriteError(ex.ToString(), ex);
            }

            return list;
        }

        ///// <summary>
        ///// ��������Ʒ�ڻ��ʽ���ˮ��ѯ
        ///// ���ߣ�Ҷ��
        ///// ʱ�䣺2010-01-28
        ///// </summary>
        ///// <param name="userID">�û�ID</param>
        ///// <param name="startTime">��ʼʱ��</param>
        ///// <param name="endTime">����ʱ��</param>
        ///// <param name="currencyType">��������</param>
        ///// <param name="pageInfo">��ҳ��Ϣʵ��</param>
        ///// <param name="total">��ҳ����</param>
        ///// <param name="errorMsg">��ѯ�쳣��Ϣ</param>
        ///// <returns></returns>
        //public List<QH_TradeCapitalFlowDetailInfo> PagingQuerySPQH_CapitalFlowDetailByAccount(string userID, string pwd, DateTime? startTime, DateTime? endTime, QueryType.QueryCurrencyType currencyType, PagingInfo pageInfo, out int total, out string errorMsg)
        //{
        //    #region ��ʼ������
        //    List<QH_TradeCapitalFlowDetailInfo> list = new List<QH_TradeCapitalFlowDetailInfo>();
        //    errorMsg = "";
        //    total = 0;
        //    #endregion

        //    try
        //    {
        //        #region ����û�ID���߷�ҳ����ʵ��Ϊ��ֱ�ӷ����󴫵ݲ�����ʾ �б�Ϊnull
        //        if (string.IsNullOrEmpty(userID))
        //        {
        //            errorMsg = "�û�ID����Ϊ�գ�";
        //            return list;
        //        }
        //        //����պ�Ҫ�����û���������֤�ڴ˼���
        //        if (pageInfo == null)
        //        {
        //            errorMsg = "��ҳ��Ϣ����Ϊ��!";
        //            return null;
        //        }
        //        #endregion

        //        #region ����û�����
        //        if (!AccountManager.Instance.ExistsBasicUserPWDByUserId(userID, pwd))
        //        {
        //            errorMsg = "�û����벻��ȷ������û�д��û������Ϣ��";
        //            return list;
        //        }
        //        #endregion

        //        #region ��ͨ���û�IDȡ���û����ڻ��ʽ��˺�
        //        List<UA_UserAccountAllocationTableInfo> userAccountInfo = new List<UA_UserAccountAllocationTableInfo>();
        //        #region ���Ϊ0�Ͳ�ѯ����µ����п����������˺�
        //        #region �ӻ����л�ȡ�˺�
        //        userAccountInfo = AccountManager.Instance.GetUserCapitalAccountFormUserCache(userID, (int)Types.AccountType.CommodityFuturesCapital, 0);
        //        #endregion
        //        #endregion

        //        #region ֱ�Ӵ����ݿ��л�ȡ
        //        //if (accountType == 0)
        //        //{
        //        //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountTypeClass(userID, Types.AccountAttributionType.FuturesCapital);
        //        //}
        //        //else
        //        //{
        //        //    userAccountInfo = dal.GetUserAccountByUserIDAndPwdAndAccountType(userID, accountType);
        //        //}
        //        #endregion
        //        #endregion


        //        #region ��ҳ��ѯ�����Ϣ
        //        PagingProceduresInfo prcoInfo = new PagingProceduresInfo();
        //        prcoInfo.IsCount = pageInfo.IsCount;
        //        prcoInfo.PageNumber = pageInfo.CurrentPage;
        //        prcoInfo.PageSize = pageInfo.PageLength;
        //        prcoInfo.Fields = " TradeID,UserCapitalAccount,FlowTypes,Margin,TradeProceduresFee,ProfitLoss,OtherCose,FlowTotal,CurrencyType,CreateDateTime ";
        //        prcoInfo.PK = "TradeID";
        //        if (pageInfo.Sort == 0)
        //        {
        //            prcoInfo.Sort = " CreateDateTime asc ";
        //        }
        //        else
        //        {
        //            prcoInfo.Sort = " CreateDateTime desc ";
        //        }
        //        prcoInfo.Tables = " dbo.QH_TradeCapitalFlowDetail ";

        //        #region ��װ�������
        //        StringBuilder sb = new StringBuilder();
        //        string userIDstr = "";
        //        foreach (UA_UserAccountAllocationTableInfo item in userAccountInfo)
        //        {
        //            userIDstr += ",   '" + item.UserAccountDistributeLogo + "'";
        //        }
        //        if (!string.IsNullOrEmpty(userIDstr))
        //        {
        //            userIDstr = userIDstr.Substring(userIDstr.IndexOf(',') + 1);
        //        }



        //        if (!string.IsNullOrEmpty(userIDstr))
        //        {
        //            if (userIDstr.Split(',').Length > 1)
        //            {
        //                sb.AppendFormat("  UserCapitalAccount  in ( {0} )", userIDstr);
        //            }
        //            else
        //            {
        //                sb.AppendFormat("  UserCapitalAccount ={0} ", userIDstr);
        //            }
        //        }
        //        else //����ڻ����л�ȡ��������ֱ�Ӵ����ݿ��л�ȡ����
        //        {
        //            //���ﲻ�ٲ������ݿ�
        //            sb.Append("  UserCapitalAccount ='' ");
        //            //if (accountType == 0)
        //            //{
        //            //    sb.Append("  a.UserAccountDistributeLogo  in ( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo in ( select accounttypelogo from BD_AccountType where atcid='" + (int)CommonObject.Types.AccountAttributionType.SpotCapital + "')  and userid='" + userID + "' )");
        //            //}
        //            //else
        //            //{
        //            //    sb.Append("  a.UserAccountDistributeLogo  in ( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo='" + accountType + "'  and userid='" + userID + "' )");
        //            //}
        //        }

        //        if (currencyType != QueryType.QueryCurrencyType.ALL)
        //        {
        //            sb.Append(" and CurrencyType='" + (int)currencyType + "'");
        //        }
        //        sb.AppendFormat(CommonDALBulidSQLScript.BuildWhereQueryBetwennTime(startTime, endTime, 30), "CreateDateTime");

        //        prcoInfo.Filter = sb.ToString();
        //        #endregion

        //        #endregion

        //        #region ִ�в�ѯ
        //        try
        //        {
        //            QH_TradeCapitalFlowDetailDal dal = new QH_TradeCapitalFlowDetailDal();
        //            CommonDALOperate<QH_TradeCapitalFlowDetailInfo> com = new CommonDALOperate<QH_TradeCapitalFlowDetailInfo>();
        //            list = com.PagingQueryProcedures(prcoInfo, out total, dal.ReaderBind);
        //        }
        //        catch (Exception ex)
        //        {
        //            errorMsg = ex.Message;
        //            LogHelper.WriteError(ex.ToString(), ex);
        //        }
        //        return list;
        //        #endregion

        //    }
        //    catch (Exception ex)
        //    {
        //        errorMsg = ex.Message;
        //        LogHelper.WriteError(ex.ToString(), ex);
        //    }

        //    return list;
        //}
        #endregion


    }
}