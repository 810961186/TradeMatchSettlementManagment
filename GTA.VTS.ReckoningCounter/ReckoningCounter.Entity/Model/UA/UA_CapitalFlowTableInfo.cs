using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ʽ�ת��ʵ��
    /// Desc: �ʽ�ת��ʵ��ʵ����UA_CapitalFlowTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    [DataContract]
    public class UA_CapitalFlowTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public UA_CapitalFlowTableInfo()
        { }
        #region Model
        private int _capitalflowlogo;
        private string _fromcapitalaccount;
        private string _tocapitalaccount;
        private decimal _transferamount;
        private DateTime _transfertime;
        private int _tradecurrencytype;
        private int _transfertypelogo;
        /// <summary>
        /// ת����ϸ����ID
        /// </summary>
        [DataMember]
        public int CapitalFlowLogo
        {
            set { _capitalflowlogo = value; }
            get { return _capitalflowlogo; }
        }
        /// <summary>
        /// ת���ʽ��˺�
        /// </summary>
        [DataMember]
        public string FromCapitalAccount
        {
            set { _fromcapitalaccount = value; }
            get { return _fromcapitalaccount; }
        }
        /// <summary>
        /// ת����˺�
        /// </summary>
        [DataMember]
        public string ToCapitalAccount
        {
            set { _tocapitalaccount = value; }
            get { return _tocapitalaccount; }
        }
        /// <summary>
        /// ת���ܽ��
        /// </summary>
        [DataMember]
        public decimal TransferAmount
        {
            set { _transferamount = value; }
            get { return _transferamount; }
        }
        /// <summary>
        /// ת��ʱ��
        /// </summary>
        [DataMember]
        public DateTime TransferTime
        {
            set { _transfertime = value; }
            get { return _transfertime; }
        }
        /// <summary>
        /// ת�˻�������ID(���BD_CurrencyType)
        /// </summary>
        [DataMember]
        public int TradeCurrencyType
        {
            set { _tradecurrencytype = value; }
            get { return _tradecurrencytype; }
        }
        /// <summary>
        /// ת������ID(���BD_TransferTypeTable)
        /// </summary>
        [DataMember]
        public int TransferTypeLogo
        {
            set { _transfertypelogo = value; }
            get { return _transfertypelogo; }
        }
        #endregion Model

    }
}

