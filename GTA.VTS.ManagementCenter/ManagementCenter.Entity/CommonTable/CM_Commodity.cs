using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
    /// <summary>
    ///����:������Ʒ�� ʵ����CM_Commodity ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:�޸����ڣ�2009-7-22
    /// </summary>
    [DataContract]
    public class CM_Commodity
    {
        public CM_Commodity()
        {
        }

        #region Model

        private string _commoditycode;
        private string _commodityname;
        private int? _breedclassid;
        private string _labelcommoditycode;
        private decimal _goerScale;
        private DateTime _marketdate;
        private string _stockpinyin;
        private double? _turnovervolume;
        private int _isExpired;
        private int _iSSysDefaultCode;
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        [DataMember]
        public string CommodityCode
        {
            set { _commoditycode = value; }
            get { return _commoditycode; }
        }

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        [DataMember]
        public string CommodityName
        {
            set { _commodityname = value; }
            get { return _commodityname; }
        }

        /// <summary>
        /// Ʒ�ֱ�ʶ
        /// </summary>
        [DataMember]
        public int? BreedClassID
        {
            set { _breedclassid = value; }
            get { return _breedclassid; }
        }

        /// <summary>
        /// �����Ʒ����
        /// </summary>
        [DataMember]
        public string LabelCommodityCode
        {
            set { _labelcommoditycode = value; }
            get { return _labelcommoditycode; }
        }

        /// <summary>
        /// ��Ȩ����
        /// </summary>
        [DataMember]
        public decimal GoerScale
        {
            set { _goerScale = value; }
            get { return _goerScale; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [DataMember]
        public DateTime MarketDate
        {
            set { _marketdate = value; }
            get { return _marketdate; }
        }

        /// <summary>
        /// ��ƱȨ֤ƴ�����
        /// </summary>
        [DataMember]
        public string StockPinYin
        {
            set { _stockpinyin = value; }
            get { return _stockpinyin; }
        }

        /// <summary>
        /// ��ͨ�������ֲܳ���
        /// </summary>
        [DataMember]
        public double? turnovervolume
        {
            set { _turnovervolume = value; }
            get { return _turnovervolume; }
        }

        /// <summary>
        /// �ڻ������Ƿ����
        /// </summary>
        [DataMember]
        public int IsExpired
        {
            set { _isExpired = value; }
            get { return _isExpired; }
        }

        /// <summary>
        /// �Ƿ���ϵͳĬ�ϴ���
        /// </summary>
        [DataMember]
        public int ISSysDefaultCode
        {
            set { _iSSysDefaultCode = value; }
            get { return _iSSysDefaultCode; }
        }

        #endregion Model
    }
}