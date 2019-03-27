using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ڻ�����ί��ʵ����
    /// Desc: �ڻ�����ί��ʵ����QH_TodayEntrustTableInfo
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    [DataContract]
    public class QH_TodayEntrustTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public QH_TodayEntrustTableInfo()
        { }

        #region EntrustNumber ί�е���(����)
        private string entrustNumber;
        /// <summary>
        /// ί�е���(����)
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
        #region EntrustAmount ί������
        private int entrustAmount;
        /// <summary>
        /// ί������
        /// </summary>
        [DataMember]
        public int EntrustAmount
        {
            get
            {
                return entrustAmount;
            }
            set
            {
                entrustAmount = value;
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
        #region CancelLogo �ɳ���ʶ(1Ϊ�ɳ���0Ϊ���ɳ�, -100Ϊ��ʼֵ)
        private bool cancelLogo;
        /// <summary>
        /// �ɳ���ʶ(1Ϊ�ɳ���0Ϊ���ɳ�, -100Ϊ��ʼֵ)
        /// </summary>
        [DataMember]
        public bool CancelLogo
        {
            get
            {
                return cancelLogo;
            }
            set
            {
                cancelLogo = value;
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
        #region ContractCode ί�к�Լ(��Ʒ)ID(����)
        private string contractCode;
        /// <summary>
        /// ί�к�Լ(��Ʒ)ID(����)
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
        #region TradeAccount �ڻ��ֲ��ʻ�(����֤ȯ�ɶ�����/��Ʒ�ڻ����ױ���)(���UA_UserAccountAllocationTable)
        private string tradeAccount;
        /// <summary>
        /// �ڻ��ֲ��ʻ�(����֤ȯ�ɶ�����/��Ʒ�ڻ����ױ���)(���UA_UserAccountAllocationTable)
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
        #region CapitalAccount �ڻ�ί���ʽ��ʻ�(����ָ�ڻ��ʽ��ʺ�/��Ʒ�ڻ��ʽ��ʺ�)(���UA_UserAccountAllocationTable)
        private string capitalAccount;
        /// <summary>
        /// �ڻ�ί���ʽ��ʻ�(����ָ�ڻ��ʽ��ʺ�/��Ʒ�ڻ��ʽ��ʺ�)(���UA_UserAccountAllocationTable)
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
        #region OrderMessage ί�е���Ϣ
        private string orderMessage;
        /// <summary>
        /// ί�е���Ϣ
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
        #region CallbackChannelId �ص�ע��ͨ����ʶ
        private string callbackChannelId;
        /// <summary>
        /// �ص�ע��ͨ����ʶ
        /// </summary>
        [DataMember]
        public string CallbackChannelId
        {
            get
            {
                return callbackChannelId;
            }
            set
            {
                callbackChannelId = value;
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
        #region CloseFloatProfitLoss ƽ��ӯ��������
        private decimal closeFloatProfitLoss;
        /// <summary>
        /// ƽ��ӯ��������
        /// </summary>
        [DataMember]
        public decimal CloseFloatProfitLoss
        {
            get
            {
                return closeFloatProfitLoss;
            }
            set
            {
                closeFloatProfitLoss = value;
            }
        }
        #endregion
        #region CloseMarketProfitLoss ƽ��ӯ��������
        private decimal closeMarketProfitLoss;
        /// <summary>
        /// ƽ��ӯ��������
        /// </summary>
        [DataMember]
        public decimal CloseMarketProfitLoss
        {
            get
            {
                return closeMarketProfitLoss;
            }
            set
            {
                closeMarketProfitLoss = value;
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
        #region EntrustTime ί��ʱ��
        private DateTime entrustTime;
        /// <summary>
        /// ί��ʱ��
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

