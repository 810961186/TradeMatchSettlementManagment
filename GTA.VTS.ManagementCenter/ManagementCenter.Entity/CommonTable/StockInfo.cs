using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
    /// <summary>
    ///����:������Ϣ�� ʵ����StockInfo ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�����Ա�������� �޸ģ�����ΰ
    /// ���ڣ�2008-11-18  2009-12-01
    /// </summary>
    [DataContract]
    public class StockInfo
    {
        public StockInfo()
        { }
        #region Model
        private string _stockcode;
        private string _stockname;
        private string _paydt;
        private string _labelcommoditycode;
        private string _nindcd;

        private string _stockpinyin;
        private double _turnovervolume;
        private decimal _goerScale;
        private int _breedClassID;
        private int _codeFromSource;

        /// <summary>
        /// ��Ʊ��Ȩ֤������
        /// </summary>
        [DataMember]
        public string StockCode
        {
            set { _stockcode = value; }
            get { return _stockcode; }
        }
        /// <summary>
        /// ��Ʊ����
        /// </summary>
        [DataMember]
        public string StockName
        {
            set { _stockname = value; }
            get { return _stockname; }
        }
        /// <summary>
        /// ��Ʊ��������
        /// </summary>
        [DataMember]
        public string Paydt
        {
            set { _paydt = value; }
            get { return _paydt; }
        }
        /// <summary>
        /// Ȩ֤��Ĵ���
        /// </summary>
        [DataMember]
        public string LabelCommodityCode
        {
            set { _labelcommoditycode = value; }
            get { return _labelcommoditycode; }
        }
        /// <summary>
        /// ��ҵ��ʶ
        /// </summary>
        [DataMember]
        public string Nindcd
        {
            set { _nindcd = value; }
            get { return _nindcd; }
        }

        /// <summary>
        /// ���ƴ��
        /// </summary>
        [DataMember]
        public string StockPinYin
        {
            set { _stockpinyin = value; }
            get { return _stockpinyin; }
        }

        /// <summary>
        /// ��ͨ����/�ֲܳ������ڻ���
        /// </summary>
        [DataMember]
        public double turnovervolume
        {
            set { _turnovervolume = value; }
            get { return _turnovervolume; }
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
        /// Ʒ�ֱ�ʶ
        /// </summary>
        [DataMember]
        public int BreedClassID
        {
            set { _breedClassID = value; }
            get { return _breedClassID; }
        }

        /// <summary>
        /// ������Դ�ĸ������ڷ����ϻ�����ʱ�ã�
        /// </summary>
        [DataMember]
        public int CodeFromSource
        {
            set { _codeFromSource = value; }
            get { return _codeFromSource; }
        }
        #endregion Model

    }
}

