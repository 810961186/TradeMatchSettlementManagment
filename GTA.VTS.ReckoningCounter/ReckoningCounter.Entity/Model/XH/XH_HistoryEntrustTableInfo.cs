using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ֻ���ʷί�е�ʵ����
    /// Desc: �ֻ���ʷί�е�ʵ����XH_HistoryEntrustTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    [DataContract]
    public class XH_HistoryEntrustTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public XH_HistoryEntrustTableInfo()
        { }

        #region EntrustNumber �ֻ�ί�е���(����)
        private string entrustNumber;
        /// <summary>
        /// �ֻ�ί�е���(����)
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
        #region EntrustMount ί������
        private int entrustMount;
        /// <summary>
        /// ί������
        /// </summary>
        [DataMember]
        public int EntrustMount
        {
            get
            {
                return entrustMount;
            }
            set
            {
                entrustMount = value;
            }
        }
        #endregion
        #region SpotCode ί����ƷID(����)
        private string spotCode;
        /// <summary>
        /// ί����ƷID(����)
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
        #region TradeAveragePrice ί�гɽ�ƽ���۸�
        private decimal tradeAveragePrice;
        /// <summary>
        /// ί�гɽ�ƽ���۸�
        /// </summary>
        [DataMember]
        public decimal TradeAveragePrice
        {
            get
            {
                return tradeAveragePrice;
            }
            set
            {
                tradeAveragePrice = value;
            }
        }
        #endregion
        #region IsMarketValue �Ƿ����м�
        private bool isMarketValue;
        /// <summary>
        /// �Ƿ����м�
        /// </summary>
        [DataMember]
        public bool IsMarketValue
        {
            get
            {
                return isMarketValue;
            }
            set
            {
                isMarketValue = value;
            }
        }
        #endregion
        #region CancelAmount ��������
        private int cancelAmount;
        /// <summary>
        /// ��������
        /// </summary>
        [DataMember]
        public int CancelAmount
        {
            get
            {
                return cancelAmount;
            }
            set
            {
                cancelAmount = value;
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
        #region StockAccount ����ί�еĽ����˻�(���ֻ��ֲ��ʻ�--����֤ȯ�ɶ�����/�۹ɹɶ�����))(���UA_UserAccountAllocationTable)
        private string stockAccount;
        /// <summary>
        /// ����ί�еĽ����˻�(���ֻ��ֲ��ʻ�--����֤ȯ�ɶ�����/�۹ɹɶ�����))(���UA_UserAccountAllocationTable)
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
        #region CapitalAccount ί���ֻ��ʽ��ʻ�(��֤ȯ�ʽ��ʻ�/�۹��ʽ��ʻ�)(���UA_UserAccountAllocationTable)
        private string capitalAccount;
        /// <summary>
        /// ί���ֻ��ʽ��ʻ�(��֤ȯ�ʽ��ʻ�/�۹��ʽ��ʻ�)(���UA_UserAccountAllocationTable)
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
        #region OrderStatusId ί�е�״̬(���DB_OrderStatus)
        private int orderStatusId;
        /// <summary>
        /// ί�е�״̬(���DB_OrderStatus)
        /// </summary>
        [DataMember]
        public int OrderStatusId
        {
            get
            {
                return orderStatusId;
            }
            set
            {
                orderStatusId = value;
            }
        }
        #endregion
        #region OrderMessage
        private string orderMessage;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string OrderMessage
        {
            get
            {
                return orderMessage;
            }
            set
            {
                orderMessage = value;
            }
        }
        #endregion
        #region McOrderId ί�е���MC����������뵥��
        private string mcOrderId;
        /// <summary>
        /// ί�е���MC����������뵥��
        /// </summary>
        [DataMember]
        public string McOrderId
        {
            get
            {
                return mcOrderId;
            }
            set
            {
                mcOrderId = value;
            }
        }
        #endregion
        #region HasDoneProfit ��ʵ��ӯ��
        private decimal hasDoneProfit;
        /// <summary>
        /// ��ʵ��ӯ��
        /// </summary>
        [DataMember]
        public decimal HasDoneProfit
        {
            get
            {
                return hasDoneProfit;
            }
            set
            {
                hasDoneProfit = value;
            }
        }
        #endregion
        #region OfferTime ί�б���ʱ��
        private DateTime? offerTime;
        /// <summary>
        /// ί�б���ʱ��
        /// </summary>
        [DataMember]
        public DateTime? OfferTime
        {
            get
            {
                return offerTime;
            }
            set
            {
                offerTime = value;
            }
        }
        #endregion
        #region EntrustTime �ֻ�ί��ʱ��
        private DateTime entrustTime;
        /// <summary>
        /// �ֻ�ί��ʱ��
        /// </summary>
        [DataMember]
        public DateTime EntrustTime
        {
            get
            {
                return entrustTime;
            }
            set
            {
                entrustTime = value;
            }
        }
        #endregion

    }
}

