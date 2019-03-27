using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ڻ���ʷ�ɽ�ʵ����
    /// Desc: �ڻ���ʷ�ɽ�ʵ����QH_HistoryTradeTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    [DataContract]
    public class QH_HistoryTradeTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public QH_HistoryTradeTableInfo()
        { }
        #region TradeNumber �ɽ���������
        private string tradeNumber;
        /// <summary>
        /// �ɽ���������
        /// </summary>
        [DataMember]
        public string TradeNumber
        {
            get
            {
                return tradeNumber;
            }
            set
            {
                tradeNumber = value;
            }
        }
        #endregion
        #region EntrustNumber ί�е���
        private string entrustNumber;
        /// <summary>
        /// ί�е���
        /// </summary>
        [DataMember]
        public string EntrustNumber
        {
            get
            {
                return entrustNumber;
            }
            set
            {
                entrustNumber = value;
            }
        }
        #endregion
        #region PortfolioLogo Ͷ���ʶ
        private string portfolioLogo;
        /// <summary>
        /// Ͷ���ʶ
        /// </summary>
        [DataMember]
        public string PortfolioLogo
        {
            get
            {
                return portfolioLogo;
            }
            set
            {
                portfolioLogo = value;
            }
        }
        #endregion
        #region TradePrice �ɽ��۸�
        private decimal tradePrice;
        /// <summary>
        /// �ɽ��۸�
        /// </summary>
        [DataMember]
        public decimal TradePrice
        {
            get
            {
                return tradePrice;
            }
            set
            {
                tradePrice = value;
            }
        }
        #endregion
        #region EntrustPrice ί�м۸�
        private decimal entrustPrice;
        /// <summary>
        /// ί�м۸�
        /// </summary>
        [DataMember]
        public decimal EntrustPrice
        {
            get
            {
                return entrustPrice;
            }
            set
            {
                entrustPrice = value;
            }
        }
        #endregion
        #region TradeAmount �ɽ�����
        private int tradeAmount;
        /// <summary>
        /// �ɽ�����
        /// </summary>
        [DataMember]
        public int TradeAmount
        {
            get
            {
                return tradeAmount;
            }
            set
            {
                tradeAmount = value;
            }
        }
        #endregion
        #region TradeProceduresFee ����������
        private decimal tradeProceduresFee;
        /// <summary>
        /// ����������
        /// </summary>
        [DataMember]
        public decimal TradeProceduresFee
        {
            get
            {
                return tradeProceduresFee;
            }
            set
            {
                tradeProceduresFee = value;
            }
        }
        #endregion
        #region Margin ��֤��
        private decimal margin;
        /// <summary>
        /// ��֤��
        /// </summary>
        [DataMember]
        public decimal Margin
        {
            get
            {
                return margin;
            }
            set
            {
                margin = value;
            }
        }
        #endregion
        #region ContractCode �ɽ���Լ(��Ʒ)���(����������ĵ�CM_Commodity��Ӧ)
        private string contractCode;
        /// <summary>
        /// �ɽ���Լ(��Ʒ)���(����������ĵ�CM_Commodity��Ӧ)
        /// </summary>
        [DataMember]
        public string ContractCode
        {
            get
            {
                return contractCode;
            }
            set
            {
                contractCode = value;
            }
        }
        #endregion
        #region TradeAccount ���ڳɽ��Ľ����˻�(���ڻ��ֲ��ʻ�--����֤ȯ�ɶ�����/��Ʒ�ڻ����ױ���))(���UA_UserAccountAllocationTable)
        private string tradeAccount;
        /// <summary>
        /// ���ڳɽ��Ľ����˻�(���ڻ��ֲ��ʻ�--����֤ȯ�ɶ�����/��Ʒ�ڻ����ױ���))(���UA_UserAccountAllocationTable)
        /// </summary>
        [DataMember]
        public string TradeAccount
        {
            get
            {
                return tradeAccount;
            }
            set
            {
                tradeAccount = value;
            }
        }
        #endregion
        #region CapitalAccount �ڻ��ɽ��ʽ��ʻ�(����ָ�ڻ��ʽ��ʺ�/��Ʒ�ڻ��ʽ��ʺ�)
        private string capitalAccount;
        /// <summary>
        /// �ڻ��ɽ��ʽ��ʻ�(����ָ�ڻ��ʽ��ʺ�/��Ʒ�ڻ��ʽ��ʺ�)
        /// </summary>
        [DataMember]
        public string CapitalAccount
        {
            get
            {
                return capitalAccount;
            }
            set
            {
                capitalAccount = value;
            }
        }
        #endregion
        #region BuySellTypeId ��������(���BD_BuySellType)
        private int buySellTypeId;
        /// <summary>
        /// ��������(���BD_BuySellType)
        /// </summary>
        [DataMember]
        public int BuySellTypeId
        {
            get
            {
                return buySellTypeId;
            }
            set
            {
                buySellTypeId = value;
            }
        }
        #endregion
        #region OpenCloseTypeId ��ƽ������ID(���BD_OpenCloseType)
        private int openCloseTypeId;
        /// <summary>
        /// ��ƽ������ID(���BD_OpenCloseType)
        /// </summary>
        [DataMember]
        public int OpenCloseTypeId
        {
            get
            {
                return openCloseTypeId;
            }
            set
            {
                openCloseTypeId = value;
            }
        }
        #endregion
        #region TradeUnitId ί�е����׵�λID(���BD_TradeUnit)
        private int tradeUnitId;
        /// <summary>
        /// ί�е����׵�λID(���BD_TradeUnit)
        /// </summary>
        [DataMember]
        public int TradeUnitId
        {
            get
            {
                return tradeUnitId;
            }
            set
            {
                tradeUnitId = value;
            }
        }
        #endregion
        #region TradeTypeId �ɽ�����ID(���BD_TradeType)
        private int tradeTypeId;
        /// <summary>
        /// �ɽ�����ID(���BD_TradeType)
        /// </summary>
        [DataMember]
        public int TradeTypeId
        {
            get
            {
                return tradeTypeId;
            }
            set
            {
                tradeTypeId = value;
            }
        }
        #endregion
        #region CurrencyTypeId ί�е����׻�������ID(���BD_CurrencyType)
        private int currencyTypeId;
        /// <summary>
        /// ί�е����׻�������ID(���BD_CurrencyType)
        /// </summary>
        [DataMember]
        public int CurrencyTypeId
        {
            get
            {
                return currencyTypeId;
            }
            set
            {
                currencyTypeId = value;
            }
        }
        #endregion
        #region TradeTime �ɽ�ʱ��
        private DateTime tradeTime;
        /// <summary>
        /// �ɽ�ʱ��
        /// </summary>
        [DataMember]
        public DateTime TradeTime
        {
            get
            {
                return tradeTime;
            }
            set
            {
                tradeTime = value;
            }
        }
        #endregion
        #region MarketProfitLoss ����ӯ��
        private decimal marketProfitLoss;
        /// <summary>
        /// ����ӯ��
        /// </summary>
        [DataMember]
        public decimal MarketProfitLoss
        {
            get
            {
                return marketProfitLoss;
            }
            set
            {
                marketProfitLoss = value;
            }
        }
        #endregion
    }
}

