using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
    /// <summary>
    ///�������ڻ�_Ʒ��_���׹��� ʵ����QH_FuturesTradeRules ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����: 2008-12-13
    /// Desc.: ���ӽ������ǵ��� DeliveryMonthHighLowStopValue
    /// Update by������
    /// Update date:2010-01-20
    /// </summary>
    [DataContract]
    public class QH_FuturesTradeRules
    {
        public QH_FuturesTradeRules()
        {
        }

        #region Model

        private decimal? _unitmultiple;
        private decimal? _leastchangeprice;
        private int _ifcontaincnewyear;
        private int? _agreementdeliveryinstitution;
        private int? _funddeliveryinstitution;
        //private int? _TradeUnit;
        private int _breedclassid;
        private int? _lasttradingdayid;
        private int? _consignquantumid;
        private int _isslew;
       
       // private int? _unitpricing;
        private int? _highlowstopscopeid;
        private decimal? _highlowstopscopevalue;
        private decimal _newBreedFuturesPactHighLowStopValue;
        private decimal _newMonthFuturesPactHighLowStopValue;
        private int? _unitsid;
        private int _priceUnit;
        private int _marketUnitID;
        private string _futruesCode;

        /// <summary>
        /// ���׵�λ�Ƽ۵�λ����
        /// </summary>
        [DataMember]
        public decimal? UnitMultiple
        {
            set { _unitmultiple = value; }
            get { return _unitmultiple; }
        }

        /// <summary>
        /// ��С�䶯��λ
        /// </summary>
        [DataMember]
        public decimal? LeastChangePrice
        {
            set { _leastchangeprice = value; }
            get { return _leastchangeprice; }
        }

        /// <summary>
        /// ��Լ�������Ƿ���������·�
        /// </summary>
        [DataMember]
        public int IfContainCNewYear
        {
            set { _ifcontaincnewyear = value; }
            get { return _ifcontaincnewyear; }
        }

        /// <summary>
        /// ��Լ�Ľ����ƶ�
        /// </summary>
        [DataMember]
        public int? AgreementDeliveryInstitution
        {
            set { _agreementdeliveryinstitution = value; }
            get { return _agreementdeliveryinstitution; }
        }

        /// <summary>
        /// �ʽ�Ľ����ƶ�
        /// </summary>
        [DataMember]
        public int? FundDeliveryInstitution
        {
            set { _funddeliveryinstitution = value; }
            get { return _funddeliveryinstitution; }
        }

      
        /// <summary>
        /// Ʒ�ֱ�ʶ
        /// </summary>
        [DataMember]
        public int BreedClassID
        {
            set { _breedclassid = value; }
            get { return _breedclassid; }
        }

        /// <summary>
        /// ������ձ�ʶ
        /// </summary>
        [DataMember]
        public int? LastTradingDayID
        {
            set { _lasttradingdayid = value; }
            get { return _lasttradingdayid; }
        }

        /// <summary>
        /// ���׹���ί������ʶ
        /// </summary>
        [DataMember]
        public int? ConsignQuantumID
        {
            set { _consignquantumid = value; }
            get { return _consignquantumid; }
        }

        /// <summary>
        /// �Ƿ�����ת
        /// </summary>
        [DataMember]
        public int IsSlew
        {
            set { _isslew = value; }
            get { return _isslew; }
        }

        /// <summary>
        /// �ǵ�ͣ��������ͱ�ʶ
        /// </summary>
        [DataMember]
        public int? HighLowStopScopeID
        {
            set { _highlowstopscopeid = value; }
            get { return _highlowstopscopeid; }
        }

        /// <summary>
        /// �ǵ�ͣ�����
        /// </summary>
        [DataMember]
        public decimal? HighLowStopScopeValue
        {
            set { _highlowstopscopevalue = value; }
            get { return _highlowstopscopevalue; }
        }

        /// <summary>
        /// ��Ʒ���ڻ���Լ���е����ǵ�ͣ�����
        /// </summary>
        [DataMember]
        public decimal NewBreedFuturesPactHighLowStopValue
        {
            set { _newBreedFuturesPactHighLowStopValue = value; }
            get { return _newBreedFuturesPactHighLowStopValue; }
        }

        /// <summary>
        /// ���·��ڻ���Լ���е����ǵ�ͣ�����
        /// </summary>
        [DataMember]
        public decimal NewMonthFuturesPactHighLowStopValue
        {
            set { _newMonthFuturesPactHighLowStopValue = value; }
            get { return _newMonthFuturesPactHighLowStopValue; }
        }

        /// <summary>
        /// �������ǵ���
        /// </summary>
        [DataMember]
        public decimal? DeliveryMonthHighLowStopValue
        {
            get;
            set;
        }

        /// <summary>
        /// ���׵�λ
        /// </summary>
        [DataMember]
        public int? UnitsID
        {
            set { _unitsid = value; }
            get { return _unitsid; }
        }

        /// <summary>
        /// �Ƽ۵�λ
        /// </summary>
        [DataMember]
        public int PriceUnit
        {
            set { _priceUnit = value; }
            get { return _priceUnit; }
        }

        /// <summary>
        /// ����ɽ�����λ
        /// </summary>
        [DataMember]
        public int MarketUnitID
        {
            set { _marketUnitID = value; }
            get { return _marketUnitID; }
        }

        /// <summary>
        /// �ڻ����״���
        /// </summary>
        [DataMember]
        public string FutruesCode
        {
            set { _futruesCode = value; }
            get {return  _futruesCode; }
        }

        #endregion Model
    }
}