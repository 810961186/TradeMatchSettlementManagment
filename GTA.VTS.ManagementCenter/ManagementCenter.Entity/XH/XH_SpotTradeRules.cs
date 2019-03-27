using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
    /// <summary>
    ///�������ֻ�_Ʒ��_���׹��� ʵ����XH_SpotTradeRules ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ �޸�
    ///����:2008-11-25
    /// </summary>
    [DataContract]
    public class XH_SpotTradeRules
    {
        public XH_SpotTradeRules()
        {
        }

        #region Model

        private int _breedclassid;
        private decimal? _minchangeprice;
        private int? _funddeliveryinstitution;
        private int? _stockdeliveryinstitution;
        private int _isslew;
        private int? _maxleavequantity;
        private int? _valuetypeminchangeprice;
        private int _marketUnitID;
        private int _priceUnit;
        private int _maxLeaveQuantityUnit;
        private int _minVolumeMultiples;
        private int? _breedClassValidID;
        private int? _breedClassHighLowID;
        /// Ʒ�ֱ�ʶ
        /// </summary>
        [DataMember]
        public int BreedClassID
        {
            set { _breedclassid = value; }
            get { return _breedclassid; }
        }

        /// <summary>
        /// ��С�䶯��λ
        /// </summary>
        [DataMember]
        public decimal? MinChangePrice
        {
            set { _minchangeprice = value; }
            get { return _minchangeprice; }
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
        /// ��Ʊ�Ľ����ƶ�
        /// </summary>
        [DataMember]
        public int? StockDeliveryInstitution
        {
            set { _stockdeliveryinstitution = value; }
            get { return _stockdeliveryinstitution; }
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
        /// ÿ�����ί����
        /// </summary>
        [DataMember]
        public int? MaxLeaveQuantity
        {
            set { _maxleavequantity = value; }
            get { return _maxleavequantity; }
        }

        /// <summary>
        /// ��С�䶯��λȡֵ����-��ֵ,��ֵ
        /// </summary>
        [DataMember]
        public int? ValueTypeMinChangePrice
        {
            set { _valuetypeminchangeprice = value; }
            get { return _valuetypeminchangeprice; }
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
        /// �Ƽ۵�λ
        /// </summary>
        [DataMember]
        public int PriceUnit
        {
            set { _priceUnit = value; }
            get { return _priceUnit; }
        }

        /// <summary>
        /// ÿ�����ί������λ
        /// </summary>
        [DataMember]
        public int MaxLeaveQuantityUnit
        {
            set { _maxLeaveQuantityUnit = value; }
            get { return _maxLeaveQuantityUnit; }
        }

        /// <summary>
        /// ��С���׵�λ����
        /// </summary>
        [DataMember]
        public int MinVolumeMultiples
        {
            set { _minVolumeMultiples = value; }
            get { return _minVolumeMultiples; }
        }

        /// <summary>
        /// Ʒ����Ч�걨��ʶ
        /// </summary>
        [DataMember]
        public int? BreedClassValidID
        {
            set { _breedClassValidID = value; }
            get { return _breedClassValidID; }
        }

        /// <summary>
        /// Ʒ���ǵ�����ʶ
        /// </summary>
        [DataMember]
        public int? BreedClassHighLowID
        {
            set { _breedClassHighLowID = value; }
            get { return _breedClassHighLowID; }
        }


        #endregion Model
    }
}