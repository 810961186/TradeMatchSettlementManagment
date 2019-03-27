using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ֺ�Ǽ�ʵ��
    /// Desc: �ֺ�Ǽ�ʵ��XH_MelonCutRegisterTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    [DataContract]
    public class XH_MelonCutRegisterTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public XH_MelonCutRegisterTableInfo()
        { }
        #region Model
        private int _meloncutregisterid;
        private DateTime _registerdate;
        private DateTime _cutdate;
        private string _useraccountdistributelogo;
        private int _tradecurrencytype;
        private string _code;
        private decimal _registeramount;
        private int _cuttype;
        private int _currencytypeid;
        /// <summary>
        /// �ֺ�ǼǱ�
        /// </summary>
        [DataMember]
        public int MelonCutRegisterID
        {
            set { _meloncutregisterid = value; }
            get { return _meloncutregisterid; }
        }
        /// <summary>
        /// ��Ȩ�Ǽ���(�������ڹ������ĵ�StockMelonCash-->StockRightRegisterDate)
        /// </summary>
        [DataMember]
        public DateTime RegisterDate
        {
            set { _registerdate = value; }
            get { return _registerdate; }
        }
        /// <summary>
        /// ��Ȩ��׼��(�������ڹ������ĵ�StockMelonCash-->StockRightLogoutDatumDate)
        /// </summary>
        [DataMember]
        public DateTime CutDate
        {
            set { _cutdate = value; }
            get { return _cutdate; }
        }
        /// <summary>
        /// �û��ֲ��˺�(���XH_AccountHoldTable->UserAccountDistributeLogo)
        /// </summary>
        [DataMember]
        public string UserAccountDistributeLogo
        {
            set { _useraccountdistributelogo = value; }
            get { return _useraccountdistributelogo; }
        }
        /// <summary>
        /// ���׻�������(������XH_AccountHoldTable->CurrencyTypeId)
        /// </summary>
        [DataMember]
        public int TradeCurrencyType
        {
            set { _tradecurrencytype = value; }
            get { return _tradecurrencytype; }
        }
        /// <summary>
        /// �ɱ���Ʒ���(�������ڹ������ĵ�StockMelonCash-->StockCode)
        /// </summary>
        [DataMember]
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// �ǼǷֺ�����(�����ڵ�ǰ�ֱֲ��п�������XH_AccountHoldTable->AvailableAmount)
        /// </summary>
        [DataMember]
        public decimal RegisterAmount
        {
            set { _registeramount = value; }
            get { return _registeramount; }
        }
        /// <summary>
        /// �ֺ�����(1-�ֽ�ֺ�,2-��Ʊ�ֺ�)
        /// </summary>
        [DataMember]
        public int CutType
        {
            set { _cuttype = value; }
            get { return _cuttype; }
        }
        /// <summary>
        /// ��ǰ�ֺ��������(���BD_CurrencyType)
        /// </summary>
        [DataMember]
        public int CurrencyTypeId
        {
            set { _currencytypeid = value; }
            get { return _currencytypeid; }
        }
        #endregion Model

    }
}

