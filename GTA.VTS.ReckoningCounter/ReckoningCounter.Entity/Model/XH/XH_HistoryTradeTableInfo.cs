using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ֻ���ʷ�ɽ�ʵ����
    /// Desc: �ֻ���ʷ�ɽ�ʵ����XH_HistoryTradeTableInfo ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    [DataContract]
    public class XH_HistoryTradeTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public XH_HistoryTradeTableInfo()
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
        #region StampTax ӡ��˰
        private decimal stampTax;
        /// <summary>
        /// ӡ��˰
        /// </summary>
        [DataMember]
        public decimal StampTax
        {
            get
            {
                return stampTax;
            }
            set
            {
                stampTax = value;
            }
        }
        #endregion
        #region Commission Ӷ��
        private decimal commission;
        /// <summary>
        /// Ӷ��
        /// </summary>
        [DataMember]
        public decimal Commission
        {
            get
            {
                return commission;
            }
            set
            {
                commission = value;
            }
        }
        #endregion
        #region SpotCode �ɽ���Ʒ���(����������ĵ�CM_Commodity��Ӧ)
        private string spotCode;
        /// <summary>
        /// �ɽ���Ʒ���(����������ĵ�CM_Commodity��Ӧ)
        /// </summary>
        [DataMember]
        public string SpotCode
        {
            get
            {
                return spotCode;
            }
            set
            {
                spotCode = value;
            }
        }
        #endregion
        #region TransferAccountFee ������
        private decimal transferAccountFee;
        /// <summary>
        /// ������
        /// </summary>
        [DataMember]
        public decimal TransferAccountFee
        {
            get
            {
                return transferAccountFee;
            }
            set
            {
                transferAccountFee = value;
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
        #region MonitoringFee ��ܷ�
        private decimal monitoringFee;
        /// <summary>
        /// ��ܷ�
        /// </summary>
        [DataMember]
        public decimal MonitoringFee
        {
            get
            {
                return monitoringFee;
            }
            set
            {
                monitoringFee = value;
            }
        }
        #endregion
        #region TradingSystemUseFee ����ϵͳʹ�÷�
        private decimal tradingSystemUseFee;
        /// <summary>
        /// ����ϵͳʹ�÷�
        /// </summary>
        [DataMember]
        public decimal TradingSystemUseFee
        {
            get
            {
                return tradingSystemUseFee;
            }
            set
            {
                tradingSystemUseFee = value;
            }
        }
        #endregion
        #region ClearingFee �����
        private decimal clearingFee;
        /// <summary>
        /// �����
        /// </summary>
        [DataMember]
        public decimal ClearingFee
        {
            get
            {
                return clearingFee;
            }
            set
            {
                clearingFee = value;
            }
        }
        #endregion
        #region StockAccount ���ڳɽ��Ľ����˻�(���ֻ��ֲ��ʻ�--����֤ȯ�ɶ�����/�۹ɹɶ�����))(���UA_UserAccountAllocationTable)
        private string stockAccount;
        /// <summary>
        /// ���ڳɽ��Ľ����˻�(���ֻ��ֲ��ʻ�--����֤ȯ�ɶ�����/�۹ɹɶ�����))(���UA_UserAccountAllocationTable)
        /// </summary>
        [DataMember]
        public string StockAccount
        {
            get
            {
                return stockAccount;
            }
            set
            {
                stockAccount = value;
            }
        }
        #endregion
        #region CapitalAccount �ֻ��ɽ��ʽ��ʻ�(��֤ȯ�ʽ��ʻ�/�۹��ʽ��ʻ�)
        private string capitalAccount;
        /// <summary>
        /// �ֻ��ɽ��ʽ��ʻ�(��֤ȯ�ʽ��ʻ�/�۹��ʽ��ʻ�)
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

    }
}

