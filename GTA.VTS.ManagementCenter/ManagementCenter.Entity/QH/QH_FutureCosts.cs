using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
    /// <summary>
    ///������Ʒ��_�ڻ�_���׷��� ʵ����QH_FutureCosts ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����: 2008-12-13
    /// </summary>
    [DataContract]
    public class QH_FutureCosts
    {
        public QH_FutureCosts()
        { }
        #region Model
        //private decimal? _TradeUnitCharge;
        private decimal _turnoverrateofservicecharge;
        private int? _currencytypeid;
        private int _costType;
        private int _breedclassid;
        /// <summary>
        /// ÿ�ֽ���������
        /// </summary>
        //[DataMember]
        //public decimal? TradeUnitCharge
        //{
        //    set { _TradeUnitCharge = value; }
        //    get { return _TradeUnitCharge; }
        //}
        /// <summary>
        /// ��λ��ɽ�������������
        /// </summary>
        [DataMember]
        public decimal TurnoverRateOfServiceCharge
        {
            set { _turnoverrateofservicecharge = value; }
            get { return _turnoverrateofservicecharge; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        [DataMember]
        public int CostType
        {
            set { _costType = value; }
            get { return _costType; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        [DataMember]
        public int? CurrencyTypeID
        {
            set { _currencytypeid = value; }
            get { return _currencytypeid; }
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
        #endregion Model

    }
}

