#region Using Namespace

using System;
using System.Collections.Generic;
using System.Linq;
using GTA.VTS.Common.CommonObject;
using CommonRealtimeMarket;
//using CommonRealtimeMarket.entity;
using CommonRealtimeMarket.factory;
using GTA.VTS.Common.CommonUtility;
using RealTime.Server.SModelData.HqData;
using ReckoningCounter.BLL.Common;
using ReckoningCounter.BLL.ManagementCenter;
using ReckoningCounter.DAL.DevolveVerifyCommonService;
using ReckoningCounter.DAL.FuturesDevolveService;
using ReckoningCounter.DAL.SpotTradingDevolveService;
using ReckoningCounter.DAL.HKTradingRulesService;

#endregion

namespace ReckoningCounter.BLL.DelegateValidate
{
    /// <summary>
    /// �ǵ���������,������뷶Χ1800-1850
    /// ���ߣ�����
    /// ���ڣ�2008-11-21
    /// Desc.:�޸ĸ۹ɵ���֤�����޼��㷽���������µ��������˷ֶμ��㴦��
    /// Update By:����
    /// Update Date:2009-12-11
    /// Desc.:��������Ʒ�ڻ��ǵ������㷽��
    /// Update By:����
    /// Update Date:2010-01-26
    /// Desc: �����˹�ָ�ڻ�"���º�Լ���������ǵ���","��Լ��������ǵ���"�ļ��㷽��
    /// Update By:����
    /// Update Date:2010-03-05
    /// </summary>
    public class HighLowRangeProcessor
    {
        private ObjectCache<string, HighLowRange> highLowRangeList = new ObjectCache<string, HighLowRange>();

        public void Reset()
        {
            highLowRangeList.Reset();
        }

        /// <summary>
        /// ������Ʒ�����ȡ�ǵ���ֵ
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="orderPrice">ί�м۸�</param>
        /// <returns>�ǵ���ֵ</returns>
        public HighLowRangeValue GetHighLowRangeValueByCommodityCode(string code, decimal orderPrice)
        {
            int? breedClassType = MCService.CommonPara.GetBreedClassTypeEnumByCommodityCode(code);
            if (!breedClassType.HasValue)
                return null;

            HighLowRangeValue value = null;

            Types.BreedClassTypeEnum breedClassTypeEnum;
            try
            {
                breedClassTypeEnum = (Types.BreedClassTypeEnum)breedClassType.Value;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString(), ex);
                return null;
            }

            switch (breedClassTypeEnum)
            {
                case Types.BreedClassTypeEnum.Stock:
                    value = GetStockHighLowRangeValueByCommodityCode(code, orderPrice);
                    break;
                case Types.BreedClassTypeEnum.CommodityFuture:
                    value = GetCommoditiesHighLowRangeValueByCommodityCode(code, orderPrice);
                    break;
                case Types.BreedClassTypeEnum.StockIndexFuture:
                    value = GetFutureHighLowRangeValueByCommodityCode(code, orderPrice);
                    break;
            }

            if (value != null)
                ProcessMinChangeValue(code, orderPrice, value);

            return value;
        }



        /// <summary>
        /// ��ȡ�۹��ǵ���(��Ч�걨)
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="orderPrice">ί�м۸�</param>
        /// <param name="priceType">��������</param>
        /// <param name="tranType">������</param>
        /// <returns></returns>
        public HighLowRangeValue GetHKStockHighLowRangeValueByCommodityCode(string code, decimal orderPrice, Types.HKPriceType priceType, Types.TransactionDirection tranType)
        {
            HighLowRangeValue value = null;
            try
            {
                HKRangeValue lr = new HKRangeValue();
                lr = GetHKBuyHighLowRangeValue(code, orderPrice, priceType);
                value = new HighLowRangeValue();
                value.RangeType = Types.HighLowRangeType.HongKongPrice;
                value.HongKongRangeValue = lr;
                switch (tranType)
                {
                    case Types.TransactionDirection.Buying:
                        value.HighRangeValue = lr.BuyHighRangeValue;
                        value.LowRangeValue = lr.BuyLowRangeValue;
                        break;
                    case Types.TransactionDirection.Selling:
                        value.HighRangeValue = lr.SellHighRangeValue;
                        value.LowRangeValue = lr.SellLowRangeValue;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString(), ex);
                return null;
            }
            return value;

        }
        /// <summary>
        /// ������С�䶯��λ�����������봦��
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="orderPrice">ί�м۸�</param>
        /// <param name="value">�ǵ���ԭʼ����</param>
        private void ProcessMinChangeValue(string code, decimal orderPrice, HighLowRangeValue value)
        {
            decimal min = GetMinChangePrice(code, orderPrice);

            if (min == 0)
                return;

            string minStr = Convert.ToString(min);
            string[] strs = minStr.Split('.');
            if (strs.Length < 2)
                return;
            string round = strs[1];

            while (round[round.Length - 1] == '0')
            {
                round = round.Substring(0, round.Length - 1);
            }

            int len = round.Length;
            if (len <= 0)
                return;

            value.HighRangeValue = Math.Round(value.HighRangeValue, len, MidpointRounding.AwayFromZero);
            value.LowRangeValue = Math.Round(value.LowRangeValue, len, MidpointRounding.AwayFromZero);

            if (value.HongKongRangeValue != null)
            {
                value.HongKongRangeValue.BuyHighRangeValue = Math.Round(value.HongKongRangeValue.BuyHighRangeValue, len);
                value.HongKongRangeValue.BuyLowRangeValue = Math.Round(value.HongKongRangeValue.BuyLowRangeValue, len);
                value.HongKongRangeValue.SellHighRangeValue = Math.Round(value.HongKongRangeValue.SellHighRangeValue, len);
                value.HongKongRangeValue.SellLowRangeValue = Math.Round(value.HongKongRangeValue.SellLowRangeValue, len);
            }
        }

        #region �ֻ��ǵ�������

        /// <summary>
        /// �����ֻ���Ʒ�����ȡ�ǵ���ֵ
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="orderPrice">ί�м۸�</param>
        /// <returns>�ǵ���ֵ</returns>
        private HighLowRangeValue GetStockHighLowRangeValueByCommodityCode(string code, decimal orderPrice)
        {
            HighLowRange highLowRange = GetStockHighLowRangeByCommodityCode(code);

            if (highLowRange == null)
                return null;
            LogHelper.WriteDebug("Debug_Test-005:��ǰ��Ʊ��" + code + "�ǵ�������:" + highLowRange.RangeType.ToString());

            switch (highLowRange.RangeType)
            {
                case Types.HighLowRangeType.YesterdayCloseScale:
                    return GetType1(code, highLowRange);
                case Types.HighLowRangeType.RecentDealScale:
                    return GetType2(code, highLowRange);
                case Types.HighLowRangeType.Buy1Sell1Scale:
                    return GetType3(code, highLowRange);
                case Types.HighLowRangeType.RightPermitHighLow:
                    return GetType4(highLowRange);
                case Types.HighLowRangeType.HongKongPrice:
                    return GetType5(code, orderPrice, highLowRange);
                case Types.HighLowRangeType.RecentDealNumber:
                    return GetType6(code, highLowRange);
            }

            return null;
        }

        /// <summary>
        /// ���ֻ�ָ����������ǵ�������
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <returns>�ǵ�������</returns>
        private HighLowRange GetStockHighLowRangeByCommodityCode(string code)
        {
            HighLowRange result = null;

            CM_BreedClass breedClass = MCService.CommonPara.GetBreedClassByCommodityCode(code);

            if (breedClass == null)
            {
                LogHelper.WriteInfo("����" + code + "�޷��ҵ���Ӧ��BreedClass.");
                return null;
            }

            XH_SpotHighLowControlType controlType =
                MCService.SpotTradeRules.GetSpotHighLowControlTypeByBreedClassID(breedClass.BreedClassID);

            if (controlType == null)
            {
                LogHelper.WriteInfo("����" + code + "�޷��ҵ���Ӧ��XH_SpotHighLowControlType.");
                return null;
            }

            XH_SpotTradeRules rules = MCService.SpotTradeRules.GetSpotTradeRulesByBreedClassID(breedClass.BreedClassID);
            if (rules == null)
            {
                LogHelper.WriteInfo("����" + code + "�޷��ҵ���Ӧ��XH_SpotTradeRules.");
                return null;
            }

            if (!rules.BreedClassHighLowID.HasValue)
            {
                LogHelper.WriteInfo("����" + code + "���Ӧ��XH_SpotTradeRules.BreedClassHighLowID��ֵ.");
                return null;
            }

            IList<XH_SpotHighLowValue> values =
                MCService.SpotTradeRules.GetSpotHighLowValueByBreedClassHighLowID(rules.BreedClassHighLowID.Value);

            if (Utils.IsNullOrEmpty(values))
            {
                LogHelper.WriteInfo("����" + code + "���Ӧ��XH_SpotHighLowValue��ֵ.");
                return null;
            }

            int highLowTypeID = controlType.HighLowTypeID;

            Types.XHSpotHighLowControlType controlTypeEnum;
            try
            {
                controlTypeEnum = (Types.XHSpotHighLowControlType)highLowTypeID;
            }
            catch (Exception ex)
            {
                //string errCode = "GT-8101";
                //string errMsg = "�޷����ݽ�����ƷƷ�ֱ���ӹ������Ļ�ȡ�ǵ�����";
                //throw new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(ex.ToString(), ex);
                return null;
            }
            LogHelper.WriteDebug("Debug_Test-001:��ǰ��Ʊ��" + code + "Ʒ���ǵ�����������:" + controlTypeEnum.ToString());

            switch (controlTypeEnum)
            {
                /// �¹����У��������У���ͣ��ʼ���׺���������(������Ʊ,ST��Ʊ)
                case Types.XHSpotHighLowControlType.NewThighAddFatStopAfterOrOtherDate:
                    result = GetHighLowValueType1(code, values);
                    break;
                /// ���ǵ�������
                case Types.XHSpotHighLowControlType.NotHighLowControl:
                    result = GetHighLowValueType2(code, values);
                    break;
                /// Ȩ֤�ǵ���
                case Types.XHSpotHighLowControlType.RightPermitHighLow:
                    result = GetHighLowValueType3(code, values);
                    break;
                /// �»������У��������У���ͣ��ʼ���׺���������
                case Types.XHSpotHighLowControlType.NewFundAddFatStopAfterOrOtherDate:
                    result = GetHighLowValueType4(code, values);
                    break;
            }


            return result;
        }

        /// <summary>
        /// �������ǵ������Ƶ���Ʒ
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <returns>�ǵ�������</returns>
        private HighLowRange ProcessNoLimit(string code)
        {
            HighLowRange result = new HighLowRange();

            CM_BreedClass breedClass = MCService.CommonPara.GetBreedClassByCommodityCode(code);
            if (breedClass == null)
                return null;

            XH_ValidDeclareType validDeclareType =
                MCService.SpotTradeRules.GetValidDeclareTypeByBreedClassID(breedClass.BreedClassID);
            IList<XH_ValidDeclareValue> validDeclareValues =
                MCService.SpotTradeRules.GetValidDeclareValueByBreedClassID(breedClass.BreedClassID);

            if (Utils.IsNullOrEmpty(validDeclareValues))
            {
                LogHelper.WriteInfo("����" + code + "���Ӧ��XH_ValidDeclareValue��ֵ.");
                return null;
            }

            Types.XHValidDeclareType declareType;
            try
            {
                declareType = (Types.XHValidDeclareType)validDeclareType.ValidDeclareTypeID;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString(), ex);
                return null;
            }

            switch (declareType)
            {
                //1 ����ɽ��۵����°ٷֱ�
                case Types.XHValidDeclareType.BargainPriceUpperDownScale:
                    result = ProcessNoLimitType1(code, validDeclareValues);
                    break;
                //2 �����ڼ�ʱ��ʾ����������۸�İٷֱ��Ҳ����ڼ�ʱ��ʾ���������۸�İٷֱ�
                case Types.XHValidDeclareType.NotHighSalePriceScaleAndNotLowBuyPriceScale:
                    result = ProcessNoLimitType2(code, validDeclareValues);
                    break;
                //3 ������һ�۵ĸ���λ����һ��֮��������һ���������һ�۵ĸ���λ֮��
                case Types.XHValidDeclareType.DownBuyOneAndSaleOne:
                    result = ProcessNoLimitType3(code, validDeclareValues);
                    break;
                //4 ����ɽ������¸�����Ԫ
                case Types.XHValidDeclareType.BargainPriceOnDownMoney:
                    result = ProcessNoLimitType4(code, validDeclareValues);
                    break;
            }
            LogHelper.WriteDebug("Debug_Test-02:��ǰ��Ʊ��" + code + "�������ǵ������Ƶ���Ʒ" + validDeclareType.ValidDeclareTypeID);
            return result;
        }

        /// <summary>
        /// ���Ѿ��������Ѿ�û�з�Χֵ�˱���ȡ�۹���ָ����׼��λ���¸������ٸ���λ��ֵ
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="orderPrice">ί�м۸�</param>
        /// <param name="basePrice">��׼��λ</param>
        /// <param name="priceCount">��λ���¸���������,�����ϣ�������</param>
        /// <returns>��λ</returns>
        public decimal GetHKRangeValue(string code, decimal orderPrice, decimal basePrice, decimal priceCount)
        {

            decimal result = -1;
            //CM_BreedClass breedClass = MCService.CommonPara.GetBreedClassByCommodityCode(code);
            //if (breedClass == null)
            //    return -1;

            //IList<XH_MinChangePriceValue> m_MinChangePriceValue =
            //    MCService.SpotTradeRules.GetMinChangePriceValueByBreedClassID(breedClass.BreedClassID);
            //if (Utils.IsNullOrEmpty(m_MinChangePriceValue))
            //    return -1;

            //foreach (XH_MinChangePriceValue changePriceValue in m_MinChangePriceValue)
            //{
            //    if (changePriceValue.Value.HasValue)
            //    {
            //        decimal fValue = changePriceValue.Value.Value;
            //        CM_FieldRange fieldRange = GetFieldRange(changePriceValue.FieldRangeID);

            //        //�Ƿ��ڵ�ǰ�ֶη�Χ��
            //        bool isFind = MCService.CheckFieldRange(orderPrice, fieldRange);
            //        if (isFind)
            //        {
            //            decimal min = changePriceValue.Value.Value;
            //            decimal change = min * priceCount;

            //            return basePrice + change;
            //        }
            //    }
            //}


            return result;
        }

        //private CM_FieldRange GetFieldRange(int fieldRangeID)
        //{
        //    return MCService.CommonPara.GetFieldRangeByFieldRangeID(fieldRangeID);
        //}

        #region NoLimit Process

        /// <summary>
        /// 1 ����ɽ��۵����°ٷֱ�RecentDealScale
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="values">�ǵ���ʵ������б�</param>
        /// <returns>�ǵ�������</returns>
        private HighLowRange ProcessNoLimitType1(string code, IList<XH_ValidDeclareValue> values)
        {
            if (values.Count == 0)
                return null;

            HighLowRange result = highLowRangeList.GetByKey(code);
            if (result != null)
                return result;
            result = new HighLowRange();

            result.HighRange = values[0].UpperLimit.Value;
            result.LowRange = values[0].LowerLimit.Value;
            result.RangeType = Types.HighLowRangeType.RecentDealScale;

            highLowRangeList.Add(result, code);

            return result;
        }

        /// <summary>
        /// 2 �����ڼ�ʱ��ʾ����������۸�İٷֱ��Ҳ����ڼ�ʱ��ʾ���������۸�İٷֱ�Buy1Sell1Scale
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="values">�ǵ���ʵ������б�</param>
        /// <returns>�ǵ�������</returns>
        private HighLowRange ProcessNoLimitType2(string code, IList<XH_ValidDeclareValue> values)
        {
            if (values.Count == 0)
                return null;

            HighLowRange result = highLowRangeList.GetByKey(code);
            if (result != null)
                return result;
            result = new HighLowRange();

            result.HighRange = values[0].UpperLimit.Value;
            result.LowRange = values[0].LowerLimit.Value;
            result.RangeType = Types.HighLowRangeType.Buy1Sell1Scale;

            highLowRangeList.Add(result, code);

            return result;
        }

        /// <summary>
        /// 3 ������һ�۵ĸ���λ����һ��֮��������һ���������һ�۵ĸ���λ֮��
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="values">�ǵ���ʵ������б�</param>
        /// <returns>�ǵ�������</returns>
        private HighLowRange ProcessNoLimitType3(string code, IList<XH_ValidDeclareValue> values)
        {
            if (values.Count == 0)
                return null;

            HighLowRange result = highLowRangeList.GetByKey(code);
            if (result != null)
                return result;
            result = new HighLowRange();

            HKRange hkRange = new HKRange();

            //����ʱ�ļ�λ��ȡ���е�UpperLimit�ֶ�
            //high:��һ��                                    highrange=0     ref=��һ��
            hkRange.BuyHighRange = 0;

            //low:��һ�� - 24����λ(UpperLimit)          lowrange = 24;  ref=��һ��
            hkRange.BuyLowRange = values[0].UpperLimit.Value;


            //����ʱ�ļ�λ��ȡ���е�LowerLimit�ֶ�
            //high:��һ�� + 24����λ(LowerLimit)     highrange = 24  ref=��һ��
            hkRange.SellHighRange = values[1].LowerLimit.Value;

            //low:��һ��                                lowrange = 0    ref=��һ��
            hkRange.SellLowRange = 0;

            result.HongKongRange = hkRange;
            result.RangeType = Types.HighLowRangeType.HongKongPrice;

            highLowRangeList.Add(result, code);

            return result;
        }

        /// <summary>
        /// 4 ����ɽ������¸�����Ԫ
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="values">�ǵ���ʵ������б�</param>
        /// <returns>�ǵ�������</returns>
        private HighLowRange ProcessNoLimitType4(string code, IList<XH_ValidDeclareValue> values)
        {
            if (values.Count == 0)
                return null;

            bool isNew = MCService.IsNewStock(code);
            if (!isNew)
                isNew = MCService.IsZF(code);

            HighLowRange result = highLowRangeList.GetByKey(code);
            if (result != null)
                return result;
            result = new HighLowRange();

            decimal high = 0;
            decimal low = 0;

            if (isNew)
            {
                //��������
                //var q = from value in values
                //        //where value.IsMarketNewDay.Value == (int)Types.IsYesOrNo.Yes
                //        select value;

                //if(q.Count() == 0 )
                //    return null;

                //high = q.First().NewDayUpperLimit.Value;
                //low = q.First().NewDayLowerLimit.Value;
                high = values[0].NewDayUpperLimit.Value;
                low = values[0].NewDayLowerLimit.Value;
            }
            else
            {
                //����������
                //var q = from value in values
                //        //where value.IsMarketNewDay.Value == (int)Types.IsYesOrNo.No
                //        select value;

                //if (q.Count() == 0)
                //    return null;

                //high = q.First().UpperLimit.Value;
                //low = q.First().LowerLimit.Value;

                high = values[0].UpperLimit.Value;
                low = values[0].LowerLimit.Value;
            }

            result.HighRange = high;
            result.LowRange = low;
            result.RangeType = Types.HighLowRangeType.RecentDealNumber;

            highLowRangeList.Add(result, code);

            return result;
        }

        #endregion

        #region ControlType Process

        /// <summary>
        /// 1.�¹����У��������У���ͣ��ʼ���׺���������(������Ʊ,ST��Ʊ)
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="values">�ǵ���ʵ������б�</param>
        /// <returns>�ǵ�������</returns>
        private HighLowRange GetHighLowValueType1(string code, IList<XH_SpotHighLowValue> values)
        {
            IRealtimeMarketService service = RealTimeMarketUtil.GetRealMarketService();//RealtimeMarketServiceFactory.GetService();
            HqExData data = service.GetStockHqData(code);

            LogHelper.WriteDebug("Debug_Test-002:��ǰ��Ʊ��" + code + "�¹����У���������" + DateTime.Now);

            //�¹����У��������У���ͣ��ʼ����
            bool isNew = MCService.IsNewStock(code);
            if (!isNew)
            {
                LogHelper.WriteDebug("Debug_Test-004:��ǰ��Ʊ��" + code + "�ж��¹����У��������У����ж��Ƿ�Ϊ��������");

                isNew = MCService.IsZF(code);
            }

            if (isNew)
            {
                LogHelper.WriteDebug("Debug_Test-01:��ǰ��Ʊ��" + code + "�¹�����" + DateTime.Now);
                return ProcessNoLimit(code);
            }

            HighLowRange result = highLowRangeList.GetByKey(code);
            if (result != null)
                return result;
            result = new HighLowRange();

            if (data == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }
            bool isST = false;
            if (data.Name.ToLower().Contains("st"))
            {
                isST = true;
            }
            else
            {

                isST = false;
            }


            decimal scale = 0;
            if (isST)
            {
                scale = values[0].StValue.Value;
            }
            else
            {
                scale = values[0].NormalValue.Value;
            }

            result.HighRange = result.LowRange = scale;
            result.RangeType = Types.HighLowRangeType.YesterdayCloseScale;

            highLowRangeList.Add(result, code);
            return result;
        }

        /// <summary>
        /// 2.���ǵ������ƣ������棩
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="values"></param>
        /// <returns>�ǵ�������</returns>
        private HighLowRange GetHighLowValueType2(string code, IList<XH_SpotHighLowValue> values)
        {
            return ProcessNoLimit(code);
        }

        /// <summary>
        /// 3.Ȩ֤�ǵ���
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="values">�ǵ���ʵ������б�</param>
        /// <returns>�ǵ�������</returns>
        private HighLowRange GetHighLowValueType3(string code, IList<XH_SpotHighLowValue> values)
        {
            HighLowRange result = new HighLowRange();

            bool isNew = MCService.IsNewStock(code);
            if (!isNew)
                isNew = MCService.IsZF(code);

            IRealtimeMarketService service = RealTimeMarketUtil.GetRealMarketService(); //RealtimeMarketServiceFactory.GetService();
            HqExData data = service.GetStockHqData(code);
            if (data == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            HqExData exData = data;
            if (exData == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            HqData hqData = exData.HqData;
            if (hqData == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }


            decimal yesterDayClosePrice = 0;
            if (isNew)
            {
                yesterDayClosePrice = (decimal)hqData.Open;
            }
            else
            {
                yesterDayClosePrice = (decimal)exData.YClose;
            }



            CM_Commodity commodity = MCService.CommonPara.GetCommodityByCommodityCode(code);
            if (commodity == null)
            {
                return null;
            }

            //���㹫ʽ�еı�����125%��
            decimal scale = values[0].RightHighLowScale.Value / 100;

            //��Ȩ����
            decimal goerScale = commodity.GoerScale;

            //��Ĵ���
            string code2 = commodity.LabelCommodityCode;

            HqExData data2 = service.GetStockHqData(code2);
            HqExData exData2 = data2;

            //��Ĵ����������̼�
            decimal yesterDayClosePrice2 = (decimal)exData2.YClose;

            HighLowRange highLowRange = GetStockHighLowRangeByCommodityCode(code2);
            if (highLowRange.RangeType != Types.HighLowRangeType.YesterdayCloseScale)
                return null;

            //��Ĵ����ǵ�������
            decimal rangeScale = highLowRange.HighRange / 100;

            decimal num = yesterDayClosePrice2 * rangeScale * scale * goerScale;


            //����=Ȩ֤ǰһ�����̼۸�+(���֤ȯǰ�����̼ۡ����֤ȯ�۸��ǵ���������150%)����Ȩ����(����)
            //����=Ȩ֤ǰһ�����̼۸�-(���֤ȯǰ�����̼ۡ����֤ȯ�۸��ǵ���������150%)����Ȩ����(����)


            //20090310�޸�
            //Ȩ֤�Ƿ��۸�Ȩ֤ǰһ�����̼۸�(���֤ȯ�����Ƿ��۸�-���֤ȯǰһ�����̼�)��125%����Ȩ������
            decimal numHigh = (yesterDayClosePrice2 * (1 + rangeScale) - yesterDayClosePrice2) * scale * goerScale;
            decimal highPrice = yesterDayClosePrice + numHigh;

            //Ȩ֤�����۸�Ȩ֤ǰһ�����̼۸�-(���֤ȯǰһ�����̼�-���֤ȯ���յ����۸�)��125%����Ȩ������
            decimal numLow = (yesterDayClosePrice2 - yesterDayClosePrice2 * (1 - rangeScale)) * scale * goerScale;
            decimal lowPrice = yesterDayClosePrice - numLow;

            result.HighRange = highPrice;
            result.LowRange = lowPrice;
            result.RangeType = Types.HighLowRangeType.RightPermitHighLow;

            return result;
        }

        /// <summary>
        /// 4.�»������У��������У���ͣ��ʼ���׺���������
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="values">�ǵ���ʵ������б�</param>
        /// <returns>�ǵ�������</returns>
        private HighLowRange GetHighLowValueType4(string code, IList<XH_SpotHighLowValue> values)
        {
            bool isNew = MCService.IsNewStock(code);
            if (!isNew)
                isNew = MCService.IsZF(code);

            if (isNew)
                return ProcessNoLimit(code);

            //�����������̼۵ģ���ô����һ���ڶ�����仯��ֱ�ӷ��뻺�棬�´λ�ȡʱ���ټ���
            HighLowRange result = highLowRangeList.GetByKey(code);
            if (result != null)
                return result;
            result = new HighLowRange();

            result.HighRange = result.LowRange = values[0].FundYestClosePriceScale.Value;
            result.RangeType = Types.HighLowRangeType.YesterdayCloseScale;

            highLowRangeList.Add(result, code);
            return result;
        }

        #endregion

        #region ��ȡʵ�ʵ��ǵ���ֵ

        /// <summary>
        /// 1.�������̼۵����°ٷֱ�
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="highLowRange">�ǵ�������</param>
        /// <returns>�ǵ���ֵ����</returns>
        private HighLowRangeValue GetType1(string code, HighLowRange highLowRange)
        {
            decimal highRange = highLowRange.HighRange;
            decimal lowRange = highLowRange.LowRange;

            /*IRealtimeMarketService service = RealtimeMarketServiceFactory.GetService();
            HqExData data = service.GetStockHqData(code);
            HqExData exData = data;

            if (data == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            if (exData == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }*/



            //float yClose = exData.YClose;
            //decimal yestPrice = (decimal) yClose;

            decimal yestPrice = RealTimeMarketUtil.GetInstance().GetStockYClose(code);
            if (yestPrice == -1)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            decimal high = yestPrice * (1 + highRange / 100);
            decimal low = yestPrice * (1 - lowRange / 100);

            HighLowRangeValue value = new HighLowRangeValue();
            value.HighRangeValue = high;
            value.LowRangeValue = low;
            value.RangeType = Types.HighLowRangeType.YesterdayCloseScale;

            return value;
        }

        /// <summary>
        /// 2.����ɽ��۵����°ٷֱ�
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="highLowRange">�ǵ�������</param>
        /// <returns>�ǵ���ֵ����</returns>
        private HighLowRangeValue GetType2(string code, HighLowRange highLowRange)
        {
            decimal highRange = highLowRange.HighRange;
            decimal lowRange = highLowRange.LowRange;

            IRealtimeMarketService service = RealTimeMarketUtil.GetRealMarketService(); //RealtimeMarketServiceFactory.GetService();
            HqExData data = service.GetStockHqData(code);

            if (data == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            HqExData exData = data;

            if (exData == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            HqData hqData = exData.HqData;
            if (hqData == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            float rPrice = hqData.Lasttrade;
            if (rPrice <= 0)
            {
                string errCode = "GT-1802";
                string errMsg = "�޷���ȡ�ֻ�����ɽ��ۣ�code=" + code;

                LogHelper.WriteInfo(errMsg + ",LastTrade=" + rPrice);
                throw new VTException(errCode, errMsg);
            }

            decimal recentPrice = (decimal)rPrice;
            LogHelper.WriteDebug("Debug_Test-03:��ǰ��Ʊ��" + code + "���ɽ���:" + recentPrice);
            decimal high = recentPrice * (1 + highRange / 100);
            decimal low = recentPrice * (1 - lowRange / 100);

            HighLowRangeValue value = new HighLowRangeValue();
            value.HighRangeValue = high;
            value.LowRangeValue = low;
            value.RangeType = Types.HighLowRangeType.RecentDealScale;

            return value;
        }

        /// <summary>
        /// 3.��һ��һ�ٷֱ�
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="highLowRange">�ǵ�������</param>
        /// <returns>�ǵ���ֵ����</returns>
        private HighLowRangeValue GetType3(string code, HighLowRange highLowRange)
        {
            decimal highRange = highLowRange.HighRange;
            decimal lowRange = highLowRange.LowRange;

            IRealtimeMarketService service = RealTimeMarketUtil.GetRealMarketService(); //RealtimeMarketServiceFactory.GetService();
            HqExData data = service.GetStockHqData(code);

            if (data == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            HqExData exData = data;

            if (exData == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            HqData hqData = exData.HqData;

            if (hqData == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }


            float b1 = hqData.Buyprice1;
            decimal buy1 = (decimal)b1;

            float s1 = hqData.Sellprice1;
            decimal sell1 = (decimal)s1;

            decimal high = sell1 * highRange / 100;
            decimal low = buy1 * lowRange / 100;

            HighLowRangeValue value = new HighLowRangeValue();
            value.HighRangeValue = high;
            value.LowRangeValue = low;
            value.RangeType = Types.HighLowRangeType.Buy1Sell1Scale;

            return value;
        }

        /// <summary>
        /// 4.Ȩ֤�ǵ���
        /// </summary>
        /// <param name="highLowRange">�ǵ�������</param>
        /// <returns>�ǵ���ֵ����</returns>
        private HighLowRangeValue GetType4(HighLowRange highLowRange)
        {
            decimal high = highLowRange.HighRange;
            decimal low = highLowRange.LowRange;

            HighLowRangeValue value = new HighLowRangeValue();
            value.HighRangeValue = high;
            value.LowRangeValue = low;
            value.RangeType = Types.HighLowRangeType.RightPermitHighLow;

            return value;
        }

        /// <summary>
        /// 5.�۹�������λ
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="orderPrice">ί�м۸�</param>
        /// <param name="highLowRange">�ǵ�������</param>
        /// <returns>�ǵ���ֵ����</returns>
        private HighLowRangeValue GetType5(string code, decimal orderPrice, HighLowRange highLowRange)
        {
            HKRange hkRange = highLowRange.HongKongRange;

            decimal buyHighRange = hkRange.BuyHighRange;
            decimal buyLowRange = hkRange.BuyLowRange;
            decimal sellHighRange = hkRange.SellHighRange;
            decimal sellLowRange = hkRange.SellLowRange;

            IRealtimeMarketService service = RealTimeMarketUtil.GetRealMarketService(); //RealtimeMarketServiceFactory.GetService();
            HqExData data = service.GetStockHqData(code);

            if (data == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            HqExData exData = data;

            if (exData == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            HqData hqData = exData.HqData;

            if (hqData == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }


            float b1 = hqData.Buyprice1;
            decimal buy1 = (decimal)b1;

            float s1 = hqData.Sellprice1;
            decimal sell1 = (decimal)s1;

            decimal buyH = sell1;
            decimal buyL = GetHKRangeValue(code, orderPrice, buy1, -buyLowRange);

            decimal sellH = GetHKRangeValue(code, orderPrice, sell1, sellHighRange);
            decimal sellL = buy1;

            //decimal high = 0;
            //decimal low = 0;

            HighLowRangeValue value = new HighLowRangeValue();

            HKRangeValue hkRangeValue = new HKRangeValue();
            hkRangeValue.BuyHighRangeValue = buyH;
            hkRangeValue.BuyLowRangeValue = buyL;
            hkRangeValue.SellHighRangeValue = sellH;
            hkRangeValue.SellLowRangeValue = sellL;

            value.HongKongRangeValue = hkRangeValue;
            value.RangeType = Types.HighLowRangeType.HongKongPrice;

            return value;
        }

        /// <summary>
        /// 6.����ɽ������¸�����Ԫ
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="highLowRange">�ǵ�������</param>
        /// <returns>�ǵ���ֵ����</returns>
        private HighLowRangeValue GetType6(string code, HighLowRange highLowRange)
        {
            decimal highRange = highLowRange.HighRange;
            decimal lowRange = highLowRange.LowRange;

            IRealtimeMarketService service = RealTimeMarketUtil.GetRealMarketService(); //RealtimeMarketServiceFactory.GetService();
            HqExData data = service.GetStockHqData(code);

            if (data == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            HqExData exData = data;

            if (exData == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            HqData hqData = exData.HqData;
            if (hqData == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�ֻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            float rPrice = hqData.Lasttrade;
            if (rPrice <= 0)
            {
                string errCode = "GT-1802";
                string errMsg = "�޷���ȡ�ֻ�����ɽ��ۣ�code=" + code;

                LogHelper.WriteInfo(errMsg + ",LastTrade=" + rPrice);
                throw new VTException(errCode, errMsg);
            }

            decimal recentPrice = (decimal)rPrice;

            decimal high = recentPrice + highRange;
            decimal low = recentPrice - lowRange;

            HighLowRangeValue value = new HighLowRangeValue();
            value.HighRangeValue = high;
            value.LowRangeValue = low;
            value.RangeType = Types.HighLowRangeType.RecentDealNumber;

            return value;
        }

        #endregion

        #endregion

        #region �ڻ��ǵ�������

        /// <summary>
        /// ���ݹ�ָ�ڻ������ȡ�ǵ���ֵ
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <returns>�ǵ���ֵ</returns>
        private HighLowRangeValue GetFutureHighLowRangeValueByCommodityCode(string code, decimal orderPrice)
        {
            HighLowRange highLowRange = GetFutureHighLowRangeByCommodityCode(code);
            switch (highLowRange.RangeType)
            {
                case Types.HighLowRangeType.YesterdayBalanceScale:
                    return GetType7(code, highLowRange);
                case Types.HighLowRangeType.YesterdayBalanceNumber:
                    return GetType8(code, highLowRange);
            }

            return null;
        }

        /// <summary>
        /// �Թ�ָ�ڻ�ָ����������ǵ�������
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <returns>�ǵ�������</returns>
        private HighLowRange GetFutureHighLowRangeByCommodityCode(string code)
        {
            HighLowRange result = highLowRangeList.GetByKey(code);
            if (result != null)
                return result;
            result = new HighLowRange();

            CM_BreedClass breedClass = MCService.CommonPara.GetBreedClassByCommodityCode(code);

            if (breedClass == null)
                return null;

            QH_FuturesTradeRules tradeRules = MCService.FuturesTradeRules.GetFuturesTradeRulesByBreedClassID(breedClass.BreedClassID);

            if (tradeRules == null)
            {
                return null;
            }

            int? hlID = tradeRules.HighLowStopScopeID;
            if (!hlID.HasValue)
            {
                return null;
            }

            #region "���º�Լ���������ǵ���","��Լ��������ǵ���"�жϣ� add by ���� 2010-03-05
            if (MCService.IsLastTradingDayContract(code))
            {
                //��Լ��������ǵ���
                result.HighRange = result.LowRange = tradeRules.HighLowStopScopeValue.Value * tradeRules.NewMonthFuturesPactHighLowStopValue;
            }
            else if (MCService.IsNewQuarterMonthContract(code))
            {
                //���º�Լ���������ǵ���
                result.HighRange = result.LowRange = tradeRules.HighLowStopScopeValue.Value * tradeRules.NewBreedFuturesPactHighLowStopValue;
            }
            else
            {
                //һ�����
                result.HighRange = result.LowRange = tradeRules.HighLowStopScopeValue.Value;
            }
            #endregion

            switch (hlID.Value)
            {
                //YesterdayBalanceScale
                case (int)Types.QHHighLowStopScopeType.NoMoreAgoTradDayClearPrice:
                    result.RangeType = Types.HighLowRangeType.YesterdayBalanceScale;
                    break;
                //YesterdayBalanceNumber
                case (int)Types.QHHighLowStopScopeType.TonNotHighOrLowAgoTradDayClearPrice:
                    result.RangeType = Types.HighLowRangeType.YesterdayBalanceNumber;
                    break;
            }

            highLowRangeList.Add(result, code);

            return result;
        }

        /// <summary>
        /// 7.(��ָ�ڻ�)��һ�����ս���۵����°ٷֱ�
        /// </summary>
        /// <param name="code"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        private HighLowRangeValue GetType7(string code, HighLowRange range)
        {
            IRealtimeMarketService service = RealTimeMarketUtil.GetRealMarketService(); //RealtimeMarketServiceFactory.GetService();
            FutData futData = service.GetFutData(code);

            if (futData == null)
            {
                string errCode = "GT-1801";
                string errMsg = "�޷���ȡ�ڻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }


            //decimal yesterdayBalance = (decimal) futData.PreSettlementPrice;

            decimal yesterdayBalance = 0;
            string msg = "";
            bool canGetPrice = MCService.GetFutureYesterdayPreSettlementPrice(code, out yesterdayBalance, ref msg);
            if (!canGetPrice)
            {
                LogHelper.WriteDebug(msg);
                //return false;
            }
            LogHelper.WriteDebug(code + "���ս����=" + yesterdayBalance);

            decimal highRange = range.HighRange;
            decimal lowRange = range.LowRange;

            decimal high = yesterdayBalance * (1 + highRange / 100);
            decimal low = yesterdayBalance * (1 - lowRange / 100);

            HighLowRangeValue value = new HighLowRangeValue();
            value.HighRangeValue = high;
            value.LowRangeValue = low;
            value.RangeType = Types.HighLowRangeType.YesterdayBalanceScale;

            return value;
        }

        /// <summary>
        /// (��ָ�ڻ�)��һ�����ս���۵������޵�����(����Ǯ)
        /// </summary>
        /// <param name="code"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        private HighLowRangeValue GetType8(string code, HighLowRange range)
        {
            IRealtimeMarketService service = RealTimeMarketUtil.GetRealMarketService(); //RealtimeMarketServiceFactory.GetService();
            FutData futData = service.GetFutData(code);

            if (futData == null)
            {
                string errCode = "GT-1801";
                string errMsg = "�޷���ȡ�ڻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            decimal yesterdayBalance = (decimal)futData.PreSettlementPrice;

            decimal highRange = range.HighRange;
            decimal lowRange = range.LowRange;

            decimal high = yesterdayBalance + highRange;
            decimal low = yesterdayBalance - lowRange;

            HighLowRangeValue value = new HighLowRangeValue();
            value.HighRangeValue = high;
            value.LowRangeValue = low;
            value.RangeType = Types.HighLowRangeType.YesterdayBalanceNumber;

            return value;
        }

        /// <summary>
        /// Desc: ������Ʒ�ڻ������ȡ�ǵ���ֵ
        /// Create by: ����
        /// Create date: 2010-01-26
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <returns>�ǵ���ֵ</returns>
        private HighLowRangeValue GetCommoditiesHighLowRangeValueByCommodityCode(string code, decimal orderPrice)
        {
            HighLowRange highLowRange = GetCommoditiesHighLowRangeByCommodityCode(code);
            switch (highLowRange.RangeType)
            {
                case Types.HighLowRangeType.YesterdayBalanceScale:
                    return GetCommoditiesHighLowRangeValueByScale(code, highLowRange);
                case Types.HighLowRangeType.YesterdayBalanceNumber:
                    return GetCommoditiesHighLowRangeValueByAmount(code, highLowRange);
            }
            return null;
        }

        /// <summary>
        /// Desc: ����Ʒ�ڻ�ָ����������ǵ�������
        /// Create by: ����
        /// Create date: 2010-01-26
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <returns>�ǵ�������</returns>
        private HighLowRange GetCommoditiesHighLowRangeByCommodityCode(string code)
        {
            HighLowRange result = highLowRangeList.GetByKey(code);
            if (result != null)
                return result;
            result = new HighLowRange();

            CM_BreedClass breedClass = MCService.CommonPara.GetBreedClassByCommodityCode(code);

            if (breedClass == null)
                return null;

            QH_FuturesTradeRules tradeRules = MCService.FuturesTradeRules.GetFuturesTradeRulesByBreedClassID(breedClass.BreedClassID);

            if (tradeRules == null)
            {
                return null;
            }

            int? hlID = tradeRules.HighLowStopScopeID;
            if (!hlID.HasValue)
            {
                return null;
            }

            //1����Ʒ�ֺ�Լ���е����ǵ���
            if (MCService.IsNewBreedClassByCode(code))
            {
                result.HighRange = result.LowRange = (tradeRules.NewBreedFuturesPactHighLowStopValue * tradeRules.HighLowStopScopeValue.Value);
            }
            //2�����·ݺ�Լ���е����ǵ���
            else if (MCService.IsNewMonthBreedClassByCode(code))
            {
                result.HighRange = result.LowRange = (tradeRules.NewMonthFuturesPactHighLowStopValue * tradeRules.HighLowStopScopeValue.Value);
            }
            //3���������ǵ���
            else if (MCService.IsDeliveryMonth(code) && tradeRules.DeliveryMonthHighLowStopValue.HasValue)
            {
                result.HighRange = result.LowRange = tradeRules.DeliveryMonthHighLowStopValue.Value;
            }
            //4���ǵ���
            else
            {
                result.HighRange = result.LowRange = tradeRules.HighLowStopScopeValue.Value;
            }

            switch (hlID.Value)
            {
                //YesterdayBalanceScale
                case (int)Types.QHHighLowStopScopeType.NoMoreAgoTradDayClearPrice:
                    result.RangeType = Types.HighLowRangeType.YesterdayBalanceScale;
                    break;
                //YesterdayBalanceNumber
                case (int)Types.QHHighLowStopScopeType.TonNotHighOrLowAgoTradDayClearPrice:
                    result.RangeType = Types.HighLowRangeType.YesterdayBalanceNumber;
                    break;
            }

            highLowRangeList.Add(result, code);

            return result;
        }

        /// <summary>
        /// Desc: (��Ʒ�ڻ�)���ٷֱȼ����ǵ���ֵ
        /// Create by: ����
        /// Create date: 2010-01-26
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="range">�ǵ���ֵ</param>
        /// <returns></returns>
        private HighLowRangeValue GetCommoditiesHighLowRangeValueByScale(string code, HighLowRange range)
        {
            IRealtimeMarketService service = RealTimeMarketUtil.GetRealMarketService();
            MerFutData futData = service.GetMercantileFutData(code);

            if (futData == null)
            {
                string errCode = "GT-1801";
                string errMsg = "�޷���ȡ�ڻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            decimal yesterdayBalance = 0;
            yesterdayBalance = (decimal)futData.PreClearPrice;

            decimal highRange = range.HighRange;
            decimal lowRange = range.LowRange;

            decimal high = yesterdayBalance * (1 + highRange / 100);
            decimal low = yesterdayBalance * (1 - lowRange / 100);

            HighLowRangeValue value = new HighLowRangeValue();
            value.HighRangeValue = high;
            value.LowRangeValue = low;
            value.RangeType = Types.HighLowRangeType.YesterdayBalanceScale;

            return value;
        }

        /// <summary>
        /// Desc: (��Ʒ�ڻ�)�����������ǵ���ֵ
        /// Create by: ����
        /// Create date: 2010-01-26
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="range">�ǵ���ֵ</param>
        /// <returns></returns>
        private HighLowRangeValue GetCommoditiesHighLowRangeValueByAmount(string code, HighLowRange range)
        {
            IRealtimeMarketService service = RealTimeMarketUtil.GetRealMarketService();
            MerFutData futData = service.GetMercantileFutData(code);

            if (futData == null)
            {
                string errCode = "GT-1801";
                string errMsg = "�޷���ȡ�ڻ����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }

            decimal yesterdayBalance = (decimal)futData.PreClearPrice;

            decimal highRange = range.HighRange;
            decimal lowRange = range.LowRange;

            decimal high = yesterdayBalance + highRange;
            decimal low = yesterdayBalance - lowRange;

            HighLowRangeValue value = new HighLowRangeValue();
            value.HighRangeValue = high;
            value.LowRangeValue = low;
            value.RangeType = Types.HighLowRangeType.YesterdayBalanceNumber;

            return value;
        }

        #endregion

        #region �۹��ǵ�������Ч�걨������

        /// <summary>
        /// 5.�۹����λ��������--�ǵ�����
        /// </summary>
        /// <param name="code">��Ʒ����</param>
        /// <param name="orderPrice">ί�м۸�</param>
        /// <param name="priceType">ί�м۸�����</param>
        /// <returns>�ǵ���ֵ����</returns>
        private HKRangeValue GetHKBuyHighLowRangeValue(string code, decimal orderPrice, Types.HKPriceType priceType)
        {

            IRealtimeMarketService service = RealTimeMarketUtil.GetRealMarketService();
            HKStock data = service.GetHKStockData(code);

            if (data == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�۹����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }
            HKStock hqData = data;
            if (hqData == null)
            {
                string errCode = "GT-1800";
                string errMsg = "�޷���ȡ�۹����飬code=" + code;
                throw new VTException(errCode, errMsg);
            }


            //��������
            decimal buyHigh = 0.000M;
            //��������
            decimal buyLow = 0.000M;
            //����������
            decimal sellHigh = 0.000M;
            //����������
            decimal sellLow = 0.000M;

            decimal buy1 = (decimal)hqData.Buyprice1;//������һ��
            decimal buyvol1 = (decimal)hqData.Buyvol1;//������һ��

            decimal sell1 = (decimal)hqData.Sellprice1;//������һ��
            decimal sellvol1 = (decimal)hqData.Sellvol1;//������һ��
            decimal yesterClosePrice = (decimal)hqData.PreClosePrice;//�������м�
            decimal highTradePrice = (decimal)hqData.High;//��߳ɽ���
            decimal lowTradePrice = (decimal)hqData.Low;//��ͳɽ���
            decimal lastTradePrice = (decimal)hqData.Lasttrade;//���ɽ���(���³ɽ��ۣ�
            decimal nominalPrice = (decimal)hqData.NominalPrice; // 0;//���̼�

            #region new Create by:���� 2009-12-11
            //����
            decimal upperLimit = nominalPrice * 9;
            //����
            decimal lowerLimit = (decimal)nominalPrice / 9;
            #endregion

            Types.HKValidPriceType validPriceType = new Types.HKValidPriceType();

            //�õ���С�䶯��λ ���������³ɽ��������� ʯǧ���Ѿ�ȷ��2009-10-30
            decimal minPrice;
            HK_MinPriceFieldRange orderMinPrice = GetHKMinPrice(lastTradePrice);
            if (orderMinPrice == null)
            {
                string errCode = "GT-1252";
                string errMsg = "��ȡ�۹���С�䶯��λ����ʧ�ܣ�code=" + code;
                throw new VTException(errCode, errMsg);
            }
            minPrice = orderMinPrice.Value.Value;

            //д��־�ַ���
            string txt = "��һ�ۣ�" + buy1 + "����һ�ۣ�" + sell1 + "���������мۣ�" + yesterClosePrice + "����߳ɽ��ۣ�" + highTradePrice + "����ͳɽ��ۣ�" + lowTradePrice + "";
            txt += "���ɽ��ۣ�" + lastTradePrice + "�����̼ۣ�" + nominalPrice + "����С�䶯��λ:" + minPrice + "";
            string txtHL = "";
            //string errorMsg = "";

            #region new Create by:���� 2009-12-11
            string lowerLog = "";
            string upperLog = "";
            #endregion

            switch (priceType)
            {
                //���еĲ���ƫ�밴�̼۵ľű������ϣ��ù�ʽ�������ǣ���-���̼�*9�����̼�*9��
                //��������ޣ���ȡ -���̼�*9�����޾�ȡ ���̼�*9
                //  �����ޣ����ޡ�

                case Types.HKPriceType.LO://�޼���
                    #region ���ִ����̼����̵����
                    //���ִ����̼����̵����
                    if (buy1 > 0 && sell1 > 0)
                    {
                        txtHL += "==���޼��̡����ִ����̼����̵����======";
                        #region ����
                        //����
                        //�۽�����ڵ�ʱ���̼۶�ʮ�ĸ���λ����ʱ���̼�֮��ļ۸�
                        //��������̼�-24��λ��������̼ۡ�

                        #region old Comment by:���� 2009-12-11
                        //buyLow = buy1 - 24 * minPrice;//����
                        #endregion

                        #region new Create by:���� 2009-12-11
                        buyLow = GetSegmentedLowerPrice(buy1, 24, ref lowerLog);
                        #endregion

                        buyHigh = sell1;//����

                        //txtHL += "���̡�������̼�-24��λ��������̼ۡ�==���ޣ�{0}=" + buy1 + " -  24*" + minPrice + "  ���ޣ�{1}=" + sell1;

                        #endregion

                        #region ����
                        //����
                        //�۽�����ڵ�ʱ���̼۶�ʮ�ĸ���λ����ʱ���̼�֮��ļ۸�
                        //��������̼ۣ�������̼�+24��λ��
                        sellLow = buy1;//���� 

                        #region old Comment by:���� 2009-12-11
                        //sellHigh = sell1 + 24 * minPrice;//����
                        #endregion

                        #region new Create by:���� 2009-12-11
                        sellHigh = GetSegmentedUpperPrice(sell1, 24, ref upperLog);
                        #endregion

                        //txtHL += "���̡�������̼ۣ�������̼�+24��λ��==���ޣ�{2}=" + buy1 + "  ���ޣ�{3}=" + sell1 + " + 24*" + minPrice;
                        HighLowIntersectionCalculate(ref buyLow, ref buyHigh, ref sellLow, ref sellHigh, ref lowerLimit, ref upperLimit);
                        txtHL += "���̡�������̼�-24��λ��������̼ۡ�==���ޣ�{0}=" + lowerLog + "  ���ޣ�{1}=" + buyHigh;
                        txtHL += "���̡�������̼ۣ�������̼�+24��λ��==���ޣ�{2}=" + sellLow + "  ���ޣ�{3}=" + upperLog;

                        #endregion
                        validPriceType = Types.HKValidPriceType.SellAndBuy;
                    }
                    #endregion

                    #region û���ִ����̵����
                    //û���ִ����̵����
                    else if (buy1 <= 0 && sell1 > 0)
                    {
                        txtHL += "==���޼��̡�û���ִ����̵����======";

                        #region ����
                        //����
                        //������̼��Ǹߵģ����������ޣ� ��ͼ�-24��λ��С�ģ�����������
                        //�۽����ʱ���̼ۣ��뼰��ʱ���̼ۡ��������мۼ�������ͳɽ��������е���ͼ��ٵͶ�ʮ�ĸ���λ֮��ļ۸�
                        //��min(������̼ۣ��������мۣ�������ͳɽ���)-24��λ��������̼ۡ�

                        #region �Ƚ��ڸ��۸����
                        decimal k = ComparePrice(yesterClosePrice, sell1, lowTradePrice, false);
                        #endregion

                        #region old Comment by:���� 2009-12-11
                        //buyLow = k - minPrice * 24;//������
                        #endregion
                        #region new Create by:���� 2009-12-11
                        //������
                        buyLow = GetSegmentedLowerPrice(k, 24, ref lowerLog);
                        #endregion
                        buyHigh = sell1;//������

                        //txtHL += "���̡�min(������̼ۣ��������мۣ�������ͳɽ���)-24��λ��������̼ۡ�==���ޣ�{0}=" + k + " -  24*" + minPrice + "  ���ޣ�{1}=" + sell1;
                        #endregion

                        #region ����
                        //����
                        //�۵��ڻ���ڵ�ʱ���̼ۼӶ�ʮ�ĸ���λ�ļ۸�
                        //��0��������̼�+24��λ��
                        //�����̼�/9����������̼�+24��λ���۸񲻿�ƫ�ư��̼�9��

                        sellLow = nominalPrice / 9;//������ 
                        #region old Comment by:���� 2009-12-11
                        //sellHigh = sell1 + 24 * minPrice;//������
                        #endregion
                        #region new Create by:���� 2009-12-11
                        //������
                        sellHigh = GetSegmentedUpperPrice(sell1, 24, ref upperLog);
                        #endregion
                        //txtHL += "���̡����̼�/9����������̼�+24��λ��==���ޣ�{2}=" + nominalPrice + "/ 9   ���ޣ�{3}=" + sell1 + " +  24 *" + minPrice;
                        HighLowIntersectionCalculate(ref buyLow, ref buyHigh, ref sellLow, ref sellHigh, ref lowerLimit, ref upperLimit);
                        txtHL += "���̡�min(������̼ۣ��������мۣ�������ͳɽ���)-24��λ��������̼ۡ�==���ޣ�{0}=" + lowerLog + "  ���ޣ�{1}=" + buyHigh;
                        txtHL += "���̡����̼�/9����������̼�+24��λ��==���ޣ�{2}=" + nominalPrice + "/ 9   ���ޣ�{3}=" + upperLog;
                        #endregion
                        validPriceType = Types.HKValidPriceType.NoBuy;
                    }
                    #endregion

                    #region û���ִ����̵����
                    //û���ִ����̵����
                    else if (buy1 > 0 && sell1 <= 0)
                    {
                        txtHL += "==���޼��̡�û���ִ����̵����======";
                        #region ����
                        //����
                        //�۸��ڻ���ڵ�ʱ���̼ۼ���ʮ�ĸ���λ�ļ۸�
                        //��������̼�-24��λ��9*���̼ۡ�
                        #region old Comment by:���� 2009-12-11
                        //buyLow = buy1 - minPrice * 24;
                        #endregion
                        #region new Create by:���� 2009-12-11
                        buyLow = GetSegmentedLowerPrice(buy1, 24, ref lowerLog);
                        #endregion
                        buyHigh = nominalPrice * 9;//9*���̼�
                        //txtHL += "���̡�������̼�-24��λ��9*���̼ۡ�==���ޣ�{0}=" + buy1 + " -  24*" + minPrice + "  ���ޣ�{1}=" + nominalPrice + "*" + 9;

                        #endregion

                        #region ����
                        //����
                        //�۽����ʱ���̼ۣ��뼰��ʱ���̼ۡ��������мۼ�������߳ɽ��������е���߼��ٸ߶�ʮ�ĸ���λ֮��ļ۸�
                        //��������̼ۣ�max(������̼ۣ��������мۣ�������߳ɽ���)+24��λ��
                        sellLow = buy1;//���� 
                        #region �Ƚ��ڸ��۸����
                        decimal k = ComparePrice(yesterClosePrice, buy1, highTradePrice, true);
                        #endregion
                        #region old Comment by:���� 2009-12-11
                        //sellHigh = k + minPrice * 24;//����
                        #endregion
                        #region new Create by:���� 2009-12-11
                        sellHigh = GetSegmentedUpperPrice(k, 24, ref upperLog);
                        #endregion
                        //txtHL += "���̡�������̼ۣ�max(������̼ۣ��������мۣ�������߳ɽ���)+24��λ��==���ޣ�{2}=" + buy1 + "  ���ޣ�{3}=" + k + " + 24*" + minPrice;
                        HighLowIntersectionCalculate(ref buyLow, ref buyHigh, ref sellLow, ref sellHigh, ref lowerLimit, ref upperLimit);
                        txtHL += "���̡�������̼�-24��λ��9*���̼ۡ�==���ޣ�{0}=" + lowerLog + "  ���ޣ�{1}=" + nominalPrice + "*" + 9;
                        txtHL += "���̡�������̼ۣ�max(������̼ۣ��������мۣ�������߳ɽ���)+24��λ��==���ޣ�{2}=" + sellLow + "  ���ޣ�{3}=" + upperLog;

                        #endregion
                        validPriceType = Types.HKValidPriceType.NoSell;
                    }
                    #endregion

                    #region û���ִ������̵����
                    //û���ִ������̵����
                    else if (buy1 <= 0 && sell1 <= 0)
                    {
                        txtHL += "==���޼��̡�û���ִ������̵����======";
                        #region ����
                        //����
                        //����۸��ڻ����������̼ۡ��������мۼ�������ͳɽ��������е���ͼ��ٵͶ�ʮ�ĸ���λ�ļ۸�
                        //��û���������мۼ�������ͳɽ��ۣ�����ۿɸ��ڻ���ڻ����������̼�(������������̼����ƣ�
                        //��min(�����̼ۣ��������мۣ�������ͳɽ���)-24��λ��9*���̼ۡ�
                        if (yesterClosePrice <= 0 && lowTradePrice <= 0)
                        {
                            buyLow = nominalPrice / 9;
                            txtHL += "���̡�min(�����̼ۣ��������мۣ�������ͳɽ���)-24��λ��9*���̼ۡ�==���ޣ�{0}=" + nominalPrice + "/" + 9;
                        }
                        else
                        {
                            #region �Ƚ��ڸ��۸����
                            decimal k = ComparePrice(yesterClosePrice, lastTradePrice, lowTradePrice, false);
                            #endregion
                            #region old Comment by:���� 2009-12-11
                            //buyLow = k - minPrice * 24;
                            #endregion
                            #region new Create by:���� 2009-12-11
                            buyLow = GetSegmentedLowerPrice(k, 24, ref lowerLog);
                            #endregion
                            //txtHL += "���̡�min(�����̼ۣ��������мۣ�������ͳɽ���)-24��λ��9*���̼ۡ�==���ޣ�{0}=" + k + " -  24 *" + minPrice;
                            txtHL += "���̡�min(�����̼ۣ��������мۣ�������ͳɽ���)-24��λ��9*���̼ۡ�==���ޣ�{0}=" + lowerLog;
                        }
                        buyHigh = nominalPrice * 9;
                        txtHL += "  ���ޣ�{1}=" + nominalPrice * 9;

                        #endregion

                        #region ����
                        //����
                        //����۵��ڻ����������̼ۡ��������мۼ�������߳ɽ�����������߼��ٸ߶�ʮ�ĸ���λ�ļ۸�
                        //��û���������мۼ�������߳ɽ��ۣ�����ۿɵ��ڻ���ڻ����������̼�
                        //��0��max(������̼ۣ��������мۣ�������߳ɽ���)+24��λ��
                        sellLow = nominalPrice / 9;
                        txtHL += "���̣����̼�/9��max(������̼ۣ��������мۣ�������߳ɽ���)+24��λ��==���ޣ�{2}=" + nominalPrice / 9;

                        if (yesterClosePrice <= 0 && highTradePrice <= 0)
                        {
                            sellHigh = nominalPrice * 9;
                            txtHL += "  ���ޣ�{2}=" + nominalPrice + "*" + 9;

                        }
                        else
                        {
                            #region �Ƚ��ڸ��۸����
                            decimal t = ComparePrice(yesterClosePrice, lastTradePrice, highTradePrice, true);
                            #endregion
                            #region old Comment by:���� 2009-12-11
                            //sellHigh = t + minPrice * 24;
                            #endregion
                            #region new Create by:���� 2009-12-11
                            sellHigh = GetSegmentedUpperPrice(t, 24, ref upperLog);
                            #endregion
                            txtHL += "  ���ޣ�{3}=" + upperLog;
                        }

                        #endregion
                        validPriceType = Types.HKValidPriceType.NoSellAndBuy;
                    }
                    #endregion
                    break;
                case Types.HKPriceType.ELO:


                    #region ���ִ����̼����̵����
                    //���ִ����̼����̵����
                    if (buy1 > 0 && sell1 > 0)
                    {
                        txtHL += "==����ǿ�޼��̡����ִ����̼����̵����======";
                        #region ����
                        //����
                        //����۽�����ڵ�ʱ���̼۶�ʮ�ĸ���λ�뼰���ڵ�ʱ���̼��ĸ���λ
                        // ��������̼�-24��λ��������̼�+4��λ��
                        #region old Comment by:���� 2009-12-11
                        //buyLow = buy1 - 24 * minPrice;
                        //buyHigh = sell1 + 4 * minPrice;
                        #endregion
                        #region new Create by:���� 2009-12-11
                        buyLow = GetSegmentedLowerPrice(buy1, 24, ref lowerLog);
                        buyHigh = GetSegmentedUpperPrice(sell1, 4, ref upperLog);
                        #endregion
                        //txtHL += "���̡�������̼�-24��λ��������̼�+4��λ��==���ޣ�{0}=" + buy1 + " -  24 *" + minPrice + "  ���ޣ�{1}=" + sell1 + "+  4*" + minPrice;
                        txtHL += "���̡�������̼�-24��λ��������̼�+4��λ��==���ޣ�{0}=" + lowerLog + "  ���ޣ�{1}=" + upperLog;

                        #endregion

                        #region ����
                        //����
                        //����۽�����ڵ�ʱ���̼۶�ʮ�ĸ���λ�뼰���ڵ�ʱ���̼��ĸ���λ
                        //��������̼�-4��λ��������̼�+24��λ��
                        #region old Comment by:���� 2009-12-11
                        //sellLow = buy1 - minPrice * 4;
                        //sellHigh = sell1 + 24 * minPrice;
                        #endregion
                        #region new Create by:���� 2009-12-11
                        sellLow = GetSegmentedLowerPrice(buy1, 4, ref lowerLog);
                        sellHigh = GetSegmentedUpperPrice(sell1, 24, ref upperLog);
                        #endregion
                        //txtHL += "���̡�������̼�-4��λ��������̼�+24��λ��==���ޣ�{2}=" + buy1 + " -  4 *" + minPrice + "  ���ޣ�{3}=" + sell1 + " +  24 *" + minPrice;
                        txtHL += "���̡�������̼�-4��λ��������̼�+24��λ��==���ޣ�{2}=" + lowerLog + "  ���ޣ�{3}=" + upperLog;

                        #endregion
                        validPriceType = Types.HKValidPriceType.SellAndBuy;
                    }
                    #endregion

                    #region û���ִ����̵����
                    //û���ִ����̵����
                    else if (buy1 <= 0 && sell1 > 0)
                    {
                        txtHL += "==����ǿ�޼��̡�û���ִ����̵����======";
                        #region ����
                        //����
                        //����۽�����ڵ�ʱ���̼��ĸ���λ���뼰��ʱ���̼ۡ��������мۼ�������ͳɽ��������е���ͼ��ٵͶ�ʮ�ĸ���λ֮��ļ۸�
                        //��min(������̼ۣ��������мۣ�������ͳɽ���)-24��λ��������̼�+4��λ��

                        #region �Ƚ��ڸ��۸����
                        decimal k = ComparePrice(yesterClosePrice, sell1, lowTradePrice, false);
                        #endregion
                        #region old Comment by:���� 2009-12-11
                        //buyLow = k - minPrice * 24;
                        //buyHigh = sell1 + minPrice * 4;
                        #endregion
                        #region new Create by:���� 2009-12-11
                        buyLow = GetSegmentedLowerPrice(k, 24, ref lowerLog);
                        buyHigh = GetSegmentedUpperPrice(sell1, 4, ref upperLog);
                        #endregion
                        //txtHL += "���̡�min(������̼ۣ��������мۣ�������ͳɽ���)-24��λ��������̼�+4��λ��==���ޣ�{0}=" + k + " -  24 *" + minPrice + "  ���ޣ�{2}=" + sell1 + " +  4 *" + minPrice;
                        txtHL += "���̡�min(������̼ۣ��������мۣ�������ͳɽ���)-24��λ��������̼�+4��λ��==���ޣ�{0}=" + lowerLog + "  ���ޣ�{2}=" + upperLog;

                        #endregion

                        #region ����
                        //����
                        //����۵��ڻ���ڵ�ʱ���̼ۼӶ�ʮ�ĸ���λ�ļ۸�
                        //�����̼�/9����������̼�+24��λ��
                        sellLow = nominalPrice / 9;
                        #region old Comment by:���� 2009-12-11
                        //sellHigh = sell1 + 24 * minPrice;
                        #endregion
                        #region new Create by:���� 2009-12-11
                        sellHigh = GetSegmentedUpperPrice(sell1, 24, ref upperLog);
                        #endregion
                        //txtHL += "���̡����̼�/9����������̼�+24��λ��==���ޣ�{2}=" + nominalPrice + "/" + 9 + "  ���ޣ�{3}=" + sell1 + " +  24 *" + minPrice;
                        txtHL += "���̡����̼�/9����������̼�+24��λ��==���ޣ�{2}=" + nominalPrice + "/" + 9 + "  ���ޣ�{3}=" + upperLog;

                        #endregion
                        validPriceType = Types.HKValidPriceType.NoBuy;
                    }
                    #endregion

                    #region û���ִ����̵����
                    //û���ִ����̵����
                    else if (buy1 > 0 && sell1 <= 0)
                    {
                        txtHL += "==����ǿ�޼��̡�û���ִ����̵����======";
                        #region ����
                        //����
                        //����۸��ڻ���ڵ�ʱ���̼ۼ���ʮ�ĸ���λ�ļ۸�
                        //��������̼�-24��λ��9*���̼ۡ�
                        #region old Comment by:���� 2009-12-11
                        //buyLow = buy1 - minPrice * 24;//����
                        #endregion
                        #region new Create by:���� 2009-12-11
                        buyLow = GetSegmentedLowerPrice(buy1, 24, ref lowerLog);
                        #endregion
                        buyHigh = nominalPrice * 9;//����
                        //txtHL += "���̡�������̼�-24��λ��9*���̼ۡ�==���ޣ�{0}=" + buy1 + " -  4*" + minPrice + "  ���ޣ�{1}=" + nominalPrice + "*" + 9;
                        txtHL += "���̡�������̼�-24��λ��9*���̼ۡ�==���ޣ�{0}=" + lowerLog + "  ���ޣ�{1}=" + nominalPrice + "*" + 9;

                        #endregion

                        #region ����
                        //����
                        //����۽�����ڵ�ʱ���̼��ĸ���λ���뼰��ʱ���̼ۡ��������мۼ�������߳ɽ��������е���߼��ٸ߶�ʮ�ĸ���λ֮��ļ۸�
                        //��������̼�-4��λ��max(������̼ۣ��������мۣ�������߳ɽ���)+24��λ��
                        //������ ������̼� - �ĸ���λ
                        //������ XXX��߼� + 24��λ

                        #region �Ƚ��ڸ��۸����
                        decimal k = ComparePrice(yesterClosePrice, buy1, highTradePrice, true);
                        #endregion

                        #region old Comment by:���� 2009-12-11
                        //sellLow = buy1 - minPrice * 4;//������
                        //sellHigh = k + minPrice * 24; //������
                        #endregion
                        #region new Create by:���� 2009-12-11
                        sellLow = GetSegmentedLowerPrice(buy1, 4, ref lowerLog);
                        sellHigh = GetSegmentedUpperPrice(k, 24, ref upperLog);
                        #endregion
                        //txtHL += "���̡�������̼�-4��λ��max(������̼ۣ��������мۣ�������߳ɽ���)+24��λ��==���ޣ�{2}=" + buy1 + " -  4*" + minPrice + "  ���ޣ�{3}=" + k + " +  24*" + minPrice;
                        txtHL += "���̡�������̼�-4��λ��max(������̼ۣ��������мۣ�������߳ɽ���)+24��λ��==���ޣ�{2}=" + lowerLog + "  ���ޣ�{3}=" + upperLog;

                        #endregion
                        validPriceType = Types.HKValidPriceType.NoSell;
                    }
                    #endregion

                    #region û���ִ������̵����
                    //û���ִ������̵����(ʾ��
                    else if (buy1 <= 0 && sell1 <= 0)
                    {
                        txtHL += "==����ǿ�޼��̡�û���ִ������̵����======";
                        #region ����
                        //����
                        //����۸��ڻ���������̼ۡ��������мۼ�������ͳɽ��������е���ͼ��ٵͶ�ʮ�ĸ���λ�ļ۸�
                        //��û���������мۼ�������ͳɽ��ۣ�����ۿɸ��ڻ���ڻ���������̼�(������������̼����ƣ�
                        //��min(�����̼ۣ��������мۣ�������ͳɽ���)-24��λ��9*���̼ۡ�

                        if (yesterClosePrice <= 0 && lowTradePrice <= 0)
                        {
                            buyLow = nominalPrice / 9;
                            txtHL += "���̡�min(�����̼ۣ��������мۣ�������ͳɽ���)-24��λ��9*���̼ۡ�==���ޣ�{0}=" + nominalPrice + "/" + 9;

                        }
                        else
                        {
                            #region �Ƚ��ڸ��۸����
                            decimal k = ComparePrice(yesterClosePrice, lastTradePrice, lowTradePrice, false);
                            #endregion
                            #region old Comment by:���� 2009-12-11
                            //buyLow = k - minPrice * 24;
                            #endregion
                            #region new Create by:���� 2009-12-11
                            buyLow = GetSegmentedLowerPrice(k, 24, ref lowerLog);
                            #endregion
                            //txtHL += "���̡�min(�����̼ۣ��������мۣ�������ͳɽ���)-24��λ��9*���̼ۡ�==���ޣ�{0}=" + k + " -  24*" + minPrice;
                            txtHL += "���̡�min(�����̼ۣ��������мۣ�������ͳɽ���)-24��λ��9*���̼ۡ�==���ޣ�{0}=" + lowerLog;

                        }
                        buyHigh = nominalPrice * 9;

                        txtHL += "  ���ޣ�{1}=" + nominalPrice + "*" + 9;
                        #endregion

                        #region ����
                        //����
                        //����۵��ڻ����������̼ۡ��������мۼ�������߳ɽ�����������߼��ٸ߶�ʮ�ĸ���λ�ļ۸�
                        // ��û���������мۼ�������߳ɽ��ۣ�����ۿɵ��ڻ���ڻ����������̼�
                        //�����̼�/9��max(������̼ۣ��������мۣ�������߳ɽ���)+24��λ��
                        sellLow = nominalPrice / 9; ;
                        txtHL += "���̣����̼�/9��max(������̼ۣ��������мۣ�������߳ɽ���)+24��λ��==���ޣ�{2}=" + nominalPrice + "/" + 9;
                        if (yesterClosePrice <= 0 && highTradePrice <= 0)
                        {
                            sellHigh = nominalPrice * 9;
                            txtHL += "  ���ޣ�{3}=" + nominalPrice + "*" + 9;
                        }
                        else
                        {
                            #region �Ƚ��ڸ��۸����
                            decimal t = ComparePrice(yesterClosePrice, lastTradePrice, highTradePrice, true);
                            #endregion
                            #region old Comment by:���� 2009-12-11
                            //sellHigh = t + minPrice * 24;
                            #endregion
                            #region new Create by:���� 2009-12-11
                            sellHigh = GetSegmentedUpperPrice(t, 24, ref upperLog);
                            #endregion
                            //txtHL += "  ���ޣ�{3}=" + t + " +  24*" + minPrice;
                            txtHL += "  ���ޣ�{3}=" + upperLog;
                        }
                        #endregion
                        validPriceType = Types.HKValidPriceType.NoSellAndBuy;
                    }
                    #endregion
                    break;
                case Types.HKPriceType.SLO:


                    #region ���ִ����̼����̵����
                    //���ִ����̼����̵����
                    if (buy1 > 0 && sell1 > 0)
                    {
                        txtHL += "==���ر��޼��̡����ִ����̼����̵����======";
                        #region ����
                        //����
                        // ��������̼ۣ�9*���̼ۡ�
                        buyLow = sell1;
                        buyHigh = nominalPrice * 9;
                        txtHL += "���̡�������̼ۣ�9*���̼ۡ�==���ޣ�{0}=" + sell1 + "  ���ޣ�{1}=" + nominalPrice + "*" + 9; ;

                        #endregion

                        #region ����
                        //����
                        //(9/���̼ۣ�������̼ۡ�
                        sellLow = nominalPrice / 9;
                        sellHigh = buy1;
                        txtHL += "���̡�9/���̼ۣ�9*���̼ۡ�==���ޣ�{2}=" + nominalPrice + "/" + 9 + "  ���ޣ�{3}=" + buy1;

                        #endregion
                        validPriceType = Types.HKValidPriceType.SellAndBuy;
                    }
                    #endregion

                    #region û���ִ����̵����
                    //û���ִ����̵����
                    else if (buy1 <= 0 && sell1 > 0)
                    {
                        txtHL += "==���ر��޼��̡�û���ִ����̵����======";
                        #region ����
                        //����
                        //��������̼ۣ�9*���̼ۡ�
                        buyLow = sell1; ;
                        buyHigh = nominalPrice * 9;
                        txtHL += "���̡�������̼ۣ�9*���̼ۡ�==���ޣ�{0}=" + sell1 + "  ���ޣ�{1}=" + nominalPrice + "*" + 9; ;

                        #endregion

                        #region ����(��)
                        ////����
                        ////���̼�����9��
                        sellLow = nominalPrice / 9;//����
                        sellHigh = nominalPrice * 9;//����
                        txtHL += "���̡�9/���̼ۣ�9*���̼ۡ�==���ޣ�{2}=" + nominalPrice + "/" + 9 + "  ���ޣ�{3}=" + nominalPrice + "*" + 9; ;
                        #endregion
                        validPriceType = Types.HKValidPriceType.NoBuy;
                    }
                    #endregion

                    #region û���ִ����̵����
                    //û���ִ����̵����
                    else if (buy1 > 0 && sell1 <= 0)
                    {
                        txtHL += "==���ر��޼��̡�û���ִ����̵����======";
                        #region ����
                        //����
                        ////���̼�����9��
                        buyLow = nominalPrice / 9;//����
                        buyHigh = nominalPrice * 9;//����

                        txtHL += "���̡�9/���̼ۣ�9*���̼ۡ�==���ޣ�{0}=" + nominalPrice + "/" + 9 + "  ���ޣ�{1}=" + nominalPrice + "*" + 9;
                        #endregion

                        #region ����
                        //����
                        //(9/���̼ۣ�������̼ۡ�
                        sellLow = nominalPrice / 9;//������ 9/���̼�
                        sellHigh = buy1; //������
                        txtHL += "���̡�9/���̼ۣ�9*���̼ۡ�==���ޣ�{2}=" + nominalPrice + "/" + 9 + "  ���ޣ�{3}=" + buy1;

                        #endregion
                        validPriceType = Types.HKValidPriceType.NoSell;
                    }
                    #endregion

                    #region û���ִ������̵����
                    ////û���ִ������̵����
                    else if (buy1 <= 0 && sell1 <= 0)
                    {
                        txtHL += "==���ر��޼��̡�û���ִ������̵����======";
                        validPriceType = Types.HKValidPriceType.NoSellAndBuy;
                        #region ���̣��ޣ�
                        //����
                        ////���̼�����9��
                        buyLow = nominalPrice / 9;//����
                        buyHigh = nominalPrice * 9;//����
                        txtHL += "���̡�9/���̼ۣ�9*���̼ۡ�==���ޣ�{0}=" + nominalPrice / 9 + "  ���ޣ�{1}=" + nominalPrice * 9; ;

                        #endregion

                        #region ����
                        //����
                        sellLow = nominalPrice / 9;//����
                        sellHigh = nominalPrice * 9;//����
                        txtHL += "���̡�9/���̼ۣ�9*���̼ۡ�==���ޣ�{2}=" + nominalPrice + "/" + 9 + "  ���ޣ�{3}=" + nominalPrice + "*" + 9; ;
                        #endregion

                    }
                    #endregion
                    break;
                default:
                    break;
            }

            //new Create by : ���� 2009-12-11
            //����۸�Χ����
            HighLowIntersectionCalculate(ref buyLow, ref buyHigh, ref sellLow, ref sellHigh, ref lowerLimit, ref upperLimit);

            HKRangeValue hkRangeValue = new HKRangeValue();
            hkRangeValue.BuyHighRangeValue = buyHigh;
            hkRangeValue.BuyLowRangeValue = buyLow;
            hkRangeValue.SellHighRangeValue = sellHigh;
            hkRangeValue.SellLowRangeValue = sellLow;
            hkRangeValue.HKValidPriceType = validPriceType;

            LogHelper.WriteDebug(DateTime.Now.ToString() + txt + string.Format(txtHL, buyLow, buyHigh, sellLow, sellHigh));

            return hkRangeValue;
        }

        /// <summary>
        /// �۹ɸ��ݼ۸��ȡ�۹���С�䶯��λʵ��
        /// </summary>
        /// <param name="orderPrice"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        private HK_MinPriceFieldRange GetHKMinPrice(decimal price)
        {
            IList<HK_MinPriceFieldRange> m_MinPriceFiledRange = MCService.HKTradeRulesProxy.GetAllHKMinPriceFieldRange();

            foreach (HK_MinPriceFieldRange item in m_MinPriceFiledRange)
            {
                //��λ�����󲻿�ǰ������Ԫ��
                if (item.LowerLimit.Value < price && price <= item.UpperLimit.Value)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// �Ƚ��������ݴ�С������������С(isLargest)
        /// </summary>
        /// <param name="yesterClosePrice"></param>
        /// <param name="price"></param>
        /// <param name="tradePrice"></param>
        /// <param name="isLargest">������������С(true���ش�false����С)</param>
        /// <returns></returns>
        private decimal ComparePrice(decimal yesterClosePrice, decimal price, decimal tradePrice, bool isLargest)
        {
            decimal k;
            if (isLargest)
            {
                if (price > yesterClosePrice)
                {
                    k = price;
                }
                else
                {
                    k = yesterClosePrice;
                }
                if (k < tradePrice)
                {
                    k = tradePrice;
                }
            }
            else
            {
                if (price < yesterClosePrice)
                {
                    k = price;
                }
                else
                {
                    k = yesterClosePrice;
                }
                if (k > tradePrice)
                {
                    k = tradePrice;
                }
            }
            return k;
        }

        #region ��ȡ�ֶμ���ļ۸��ϡ�����



        /// <summary>
        /// ����۸������޵Ľ���
        /// </summary>
        /// <param name="buyLow">��������</param>
        /// <param name="buyHigh">��������</param>
        /// <param name="sellLow">����������</param>
        /// <param name="sellHigh">����������</param>
        /// <param name="lowerLimit">9�����̼�����</param>
        /// <param name="upperLimit">9�����̼�����</param>
        public void HighLowIntersectionCalculate(ref decimal buyLow, ref decimal buyHigh, ref decimal sellLow, ref decimal sellHigh, ref decimal lowerLimit, ref decimal upperLimit)
        {
            buyLow = (buyLow > lowerLimit) ? buyLow : lowerLimit;
            buyHigh = (buyHigh < upperLimit) ? buyHigh : upperLimit;
            sellLow = (sellLow > lowerLimit) ? sellLow : lowerLimit;
            sellHigh = (sellHigh < upperLimit) ? sellHigh : upperLimit;
        }

        /// <summary>
        /// ��ȡ�ֶμ���ļ۸�����
        /// </summary>
        /// <param name="price">��Ѽ۸�</param>
        /// <param name="level">��λ��</param>
        /// <param name="log">���㹫ʽ����־�ı�</param>
        /// <returns>�ֶμ���ļ۸�����</returns>
        /// 
        public decimal GetSegmentedUpperPrice(decimal price, decimal level, ref string log)
        {
            //��ǰ�۸�����
            HK_MinPriceFieldRange range = GetHKMinPrice(price);
            if (range == null)
            {
                log = "���ݼ۸�" + price + "��ȡ��С�䶯��λΪnull";
                return 0;
            }
            //��һ���۸�����
            HK_MinPriceFieldRange rangeNext = GetHKMinPrice(range.UpperLimit.Value + range.Value.Value);
            if (rangeNext == null)
            {
                log = "���ݼ۸�" + range.LowerLimit.Value + "-" + range.Value.Value + "��ȡ��С�䶯��λΪnull";
                return 0;
            }
            //���㵱ǰ����ɼ�ȥ�ļ�λ��
            decimal n1 = (range.UpperLimit.Value - price) / range.Value.Value;
            if (level <= n1)
            {
                log = price + " + " + level + " * " + range.Value.Value;
                return price + level * range.Value.Value;
            }
            else
            {
                log = price + " + " + n1 + " * " + range.Value.Value + " + (" + level + " - " + n1 + ") * " + rangeNext.Value.Value;
                return price + n1 * range.Value.Value + (level - n1) * rangeNext.Value.Value;
            }
        }


        /// <summary>
        /// ��ȡ�ֶμ���ļ۸�����
        /// Update By:���
        /// UPdate Date:2010-05-26
        /// Desc.:�޸ķ��ؼ۸�����Ϊ��ʱ����Ӧ�Ĵ���
        /// </summary>
        /// <param name="price">��Ѽ۸�</param>
        /// <param name="level">��λ��</param>
        /// <param name="log">���㹫ʽ����־�ı�</param>
        /// <returns>�ֶμ���ļ۸�����</returns>
        public decimal GetSegmentedLowerPrice(decimal price, decimal level, ref string log)
        {
            //��ǰ�۸�����
            HK_MinPriceFieldRange range = GetHKMinPrice(price);
            if (range == null)
            {
                log = "���ݼ۸�" + price + "��ȡ��С�䶯��λΪnull";
                return 0;
            }
            //��һ���۸�����
            HK_MinPriceFieldRange rangeLast = GetHKMinPrice(range.LowerLimit.Value - range.Value.Value);

            if (rangeLast == null)
            {
                log = "���ݼ۸�" + range.LowerLimit.Value + "-" + range.Value.Value + "��ȡ��С�䶯��λΪnull";
                return 0;
            }
            //���㵱ǰ����ɼ�ȥ�ļ�λ��
            decimal n1 = (price - range.LowerLimit.Value) / range.Value.Value;
            if (level <= n1)
            {
                log = price + " - " + level + " * " + range.Value.Value;
                return price - level * range.Value.Value;
            }
            else
            {
                log = price + " - " + n1 + " * " + range.Value.Value + " - (" + level + " - " + n1 + ") * " + rangeLast.Value.Value;
                return price - n1 * range.Value.Value - (level - n1) * rangeLast.Value.Value;
            }
        }

        #endregion

        #endregion



        #region ���ܺ���

        /// <summary>
        /// ��ȡ�ֻ���С�䶯��λ
        /// </summary>
        /// <param name="code">�ֻ�����</param>
        /// <param name="orderPrice">ί�м۸�</param>
        /// <returns>��С�䶯��λ</returns>
        public decimal GetMinChangePrice(string code, decimal orderPrice)
        {
            CM_BreedClass breedClass = MCService.CommonPara.GetBreedClassByCommodityCode(code);

            if (breedClass == null)
                return 0;

            XH_SpotHighLowControlType controlType =
                MCService.SpotTradeRules.GetSpotHighLowControlTypeByBreedClassID(breedClass.BreedClassID);

            if (controlType == null)
                return 0;

            XH_SpotTradeRules rules = MCService.SpotTradeRules.GetSpotTradeRulesByBreedClassID(breedClass.BreedClassID);
            if (rules == null)
                return 0;

            int? min = rules.ValueTypeMinChangePrice;
            if (!min.HasValue)
            {
                return 0;
            }

            int minValueType = min.Value;
            //decimal orderPrice = 0;
            switch (minValueType)
            {
                case (int)Types.GetValueTypeEnum.Single:
                    decimal? minSingle = rules.MinChangePrice;
                    if (minSingle.HasValue)
                    {
                        decimal minSingleValue = minSingle.Value;
                        return minSingleValue;
                        ////�Ƿ�������С�䶯��λ
                        //if (orderPrice % minSingleValue == 0)
                        //{
                        //    result = true;
                        //    strMessage = "";
                        //}
                    }

                    break;
                case (int)Types.GetValueTypeEnum.Scope:

                    //if (orderPrice == 0)
                    //    return 0;

                    //var minChangePriceValue = MCService.SpotTradeRules.GetMinChangePriceValueByBreedClassID(breedClass.BreedClassID);
                    //foreach (XH_MinChangePriceValue changePriceValue in minChangePriceValue)
                    //{
                    //    if (changePriceValue.Value.HasValue)
                    //    {
                    //        decimal fValue = changePriceValue.Value.Value;
                    //        CM_FieldRange fieldRange = MCService.GetFieldRange(changePriceValue.FieldRangeID);

                    //        //�Ƿ��ڵ�ǰ�ֶη�Χ��
                    //        bool isIn = MCService.CheckFieldRange(orderPrice, fieldRange);
                    //        if (isIn)
                    //        {
                    //            //�Ƿ�������С�䶯��λ
                    //            //if (orderPrice % fValue == 0)
                    //            //{
                    //            //    strMessage = "";

                    //            //    return true;
                    //            //}
                    //            return fValue;
                    //        }
                    //    }
                    //}
                    break;
            }


            return 0;
        }

        #endregion
    }
}
