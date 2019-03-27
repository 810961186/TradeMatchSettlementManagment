using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ڻ��ֲ�ʵ����
    /// Desc: QH_HoldAccountTable�ڻ��ֲ��˻�����ӵ�еĳֲ���Ϣ��ϸʵ���� ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create By: ���
    /// Create Date: 2009-10-15
    /// </summary>
    [DataContract]
    public class QH_HoldAccountTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public QH_HoldAccountTableInfo()
        { }
        #region AccountHoldLogoId �ֲ�ID����
        private int accountHoldLogoId;
        /// <summary>
        /// �ֲ�ID����
        /// </summary>
        [DataMember]
        public int AccountHoldLogoId
        {
            get
            {
                return accountHoldLogoId;
            }
            set
            {
                accountHoldLogoId = value;
            }
        }
        #endregion

        #region UserAccountDistributeLogo �ֲ��˺�(���UA_UserAccountAllocationTable)
        private string userAccountDistributeLogo;
        /// <summary>
        /// �ֲ��˺�(���UA_UserAccountAllocationTable)
        /// </summary>
        [DataMember]
        public string UserAccountDistributeLogo
        {
            get
            {
                return userAccountDistributeLogo;
            }
            set
            {
                userAccountDistributeLogo = value;
            }
        }
        #endregion

        #region TradeCurrencyType �ֲֽ��׻�������(���BD_CurrencyType)
        private int tradeCurrencyType;
        /// <summary>
        /// �ֲֽ��׻�������(���BD_CurrencyType)
        /// </summary>
        [DataMember]
        public int TradeCurrencyType
        {
            get
            {
                return tradeCurrencyType;
            }
            set
            {
                tradeCurrencyType = value;
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

        #region HistoryHoldAmount ��ʷ�ֲ���������ʷ������
        private decimal historyHoldAmount;
        /// <summary>
        /// ��ʷ���óֲ�����
        /// </summary>
        [DataMember]
        public decimal HistoryHoldAmount
        {
            get
            {
                return historyHoldAmount;
            }
            set
            {
                historyHoldAmount = value;
            }
        }
        #endregion

        #region HistoryFreezeAmount ��ʷ��������
        private decimal historyFreezeAmount;
        /// <summary>
        /// ��ʷ��������
        /// </summary>
        [DataMember]
        public decimal HistoryFreezeAmount
        {
            get
            {
                return historyFreezeAmount;
            }
            set
            {
                historyFreezeAmount = value;
            }
        }
        #endregion

        #region HoldAveragePrice �ֲ�ƽ���۸�
        private decimal holdAveragePrice;
        /// <summary>
        /// �ֲ�ƽ���۸�
        /// </summary>
        [DataMember]
        public decimal HoldAveragePrice
        {
            get
            {
                return holdAveragePrice;
            }
            set
            {
                holdAveragePrice = value;
            }
        }
        #endregion

        #region TodayHoldAmount ����ֲ�����
        private decimal todayHoldAmount;
        /// <summary>
        /// ������óֲ�����
        /// </summary>
        [DataMember]
        public decimal TodayHoldAmount
        {
            get
            {
                return todayHoldAmount;
            }
            set
            {
                todayHoldAmount = value;
            }
        }
        #endregion

        #region TodayHoldAveragePrice ����ֲ�ƽ���۸�
        private decimal todayHoldAveragePrice;
        /// <summary>
        /// ����ֲ�ƽ���۸�
        /// </summary>
        [DataMember]
        public decimal TodayHoldAveragePrice
        {
            get
            {
                return todayHoldAveragePrice;
            }
            set
            {
                todayHoldAveragePrice = value;
            }
        }
        #endregion

        #region TodayFreezeAmount ���춳������
        private decimal todayFreezeAmount;
        /// <summary>
        /// ���춳������
        /// </summary>
        [DataMember]
        public decimal TodayFreezeAmount
        {
            get
            {
                return todayFreezeAmount;
            }
            set
            {
                todayFreezeAmount = value;
            }
        }
        #endregion

        #region Contract �ֲֺ�Լ(��Ʒ)����(�����������CM_Commodity��ID��Ӧ)
        private string contract;
        /// <summary>
        /// �ֲֺ�Լ(��Ʒ)����(�����������CM_Commodity��ID��Ӧ)
        /// </summary>
        [DataMember]
        public string Contract
        {
            get
            {
                return contract;
            }
            set
            {
                contract = value;
            }
        }
        #endregion

        #region CostPrice �ɱ��۸�
        private decimal costPrice;
        /// <summary>
        /// �ɱ��۸�
        /// </summary>
        [DataMember]
        public decimal CostPrice
        {
            get
            {
                return costPrice;
            }
            set
            {
                costPrice = value;
            }
        }
        #endregion

        #region BreakevenPrice �����۸�
        private decimal breakevenPrice;
        /// <summary>
        /// �����۸�
        /// </summary>
        [DataMember]
        public decimal BreakevenPrice
        {
            get
            {
                return breakevenPrice;
            }
            set
            {
                breakevenPrice = value;
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

        #region ProfitLoss �������
        private decimal profitLoss;
        /// <summary>
        /// �������
        /// </summary>
        [DataMember]
        public decimal ProfitLoss
        {
            get
            {
                return profitLoss;
            }
            set
            {
                profitLoss = value;
            }
        }
        #endregion

        #region OpenAveragePrice ���־���
        private decimal openAveragePrice;
        /// <summary>
        /// ���־���
        /// </summary>
        [DataMember]
        public decimal OpenAveragePrice
        {
            get
            {
                return openAveragePrice;
            }
            set
            {
                openAveragePrice = value;
            }
        }
        #endregion

    }

    /// <summary>
    /// �ڻ��ֱֲ仯��
    /// </summary>
    public class QH_HoldAccountTableInfo_Delta
    {
        /// <summary>
        /// �ڻ��ֲ�ʵ��
        /// </summary>
        public QH_HoldAccountTableInfo Data;

        /// <summary>
        /// �ֲ��˻�Id
        /// </summary>
        public int AccountHoldLogoId;

        /// <summary>
        /// ��ʷ�ֱֲ仯��
        /// </summary>
        public decimal HistoryHoldAmountDelta;

        /// <summary>
        /// ��ʷ����仯��
        /// </summary>
        public decimal HistoryFreezeAmountDelta;

        /// <summary>
        /// ���ճֱֲ仯��
        /// </summary>
        public decimal TodayHoldAmountDelta;

        /// <summary>
        /// ���ն���仯��
        /// </summary>
        public decimal TodayFreezeAmountDelta;

        /// <summary>
        /// ��֤��仯��
        /// </summary>
        public decimal MarginDelta;
    }
}

