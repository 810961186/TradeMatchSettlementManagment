using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ڻ��ʽ��˻�ʵ����
    /// Desc: �ڻ��ʽ��˻�ʵ����ʵ����QH_CapitalAccountTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create By: ���
    /// Create Date: 2009-10-15
    /// </summary>
    [DataContract]
    public class QH_CapitalAccountTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public QH_CapitalAccountTableInfo()
        { }

        #region CapitalAccountLogoId �ڻ��ʽ��˻�ID(����)
        private int capitalAccountLogoId;
        /// <summary>
        /// �ڻ��ʽ��˻�ID(����)
        /// </summary>
        [DataMember]
        public int CapitalAccountLogoId
        {
            get
            {
                return capitalAccountLogoId;
            }
            set
            {
                capitalAccountLogoId = value;
            }
        }
        #endregion

        #region UserAccountDistributeLogo �û��ڻ��ʽ��˺�ID(���UA_UserAccountAllocationTable)
        private string userAccountDistributeLogo;
        /// <summary>
        /// �û��ڻ��ʽ��˺�ID(���UA_UserAccountAllocationTable)
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

        #region BalanceOfTheDay ���ս����
        private decimal balanceOfTheDay;
        /// <summary>
        /// ���ս����
        /// </summary>
        [DataMember]
        public decimal BalanceOfTheDay
        {
            get
            {
                return balanceOfTheDay;
            }
            set
            {
                balanceOfTheDay = value;
            }
        }
        #endregion

        #region TodayOutInCapital ��������/֧���ʽ��ܶ�
        private decimal todayOutInCapital;
        /// <summary>
        /// ��������/֧���ʽ��ܶ�
        /// </summary>
        [DataMember]
        public decimal TodayOutInCapital
        {
            get
            {
                return todayOutInCapital;
            }
            set
            {
                todayOutInCapital = value;
            }
        }
        #endregion

        #region AvailableCapital �����ʽ�
        private decimal availableCapital;
        /// <summary>
        /// �����ʽ�
        /// </summary>
        [DataMember]
        public decimal AvailableCapital
        {
            get
            {
                return availableCapital;
            }
            set
            {
                availableCapital = value;
            }
        }
        #endregion

        #region FreezeCapitalTotal ���춳���ʽ��ܶ�
        private decimal freezeCapitalTotal;
        /// <summary>
        /// ���춳���ʽ��ܶ�
        /// </summary>
        [DataMember]
        public decimal FreezeCapitalTotal
        {
            get
            {
                return freezeCapitalTotal;
            }
            set
            {
                freezeCapitalTotal = value;
            }
        }
        #endregion

        #region CapitalBalance �ʽ�����([FreezeCapitalTotal]+[AvailableCapital])����ǰӵ���ܽ��
        private decimal capitalBalance;
        /// <summary>
        /// �ʽ�����([FreezeCapitalTotal]+[AvailableCapital])����ǰӵ���ܽ��
        /// </summary>
        [DataMember]
        public decimal CapitalBalance
        {
            get
            {
                return capitalBalance;
            }
            set
            {
                capitalBalance = value;
            }
        }
        #endregion

        #region MarginTotal ��֤���ܶ�
        private decimal marginTotal;
        /// <summary>
        /// ��֤���ܶ�
        /// </summary>
        [DataMember]
        public decimal MarginTotal
        {
            get
            {
                return marginTotal;
            }
            set
            {
                marginTotal = value;
            }
        }
        #endregion

        #region TradeCurrencyType ��ǰ�ʽ��˺ŵ����ɽ�(����)��������(���BD_CurrencyType)
        private int tradeCurrencyType;
        /// <summary>
        /// ��ǰ�ʽ��˺ŵ����ɽ�(����)��������(���BD_CurrencyType)
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

        #region CloseFloatProfitLossTotal ��ƽ��ӯ��������
        private decimal closeFloatProfitLossTotal;
        /// <summary>
        /// ��ƽ��ӯ��������
        /// </summary>
        [DataMember]
        public decimal CloseFloatProfitLossTotal
        {
            get
            {
                return closeFloatProfitLossTotal;
            }
            set
            {
                closeFloatProfitLossTotal = value;
            }
        }
        #endregion

        #region CloseMarketProfitLossTotal ��ƽ��ӯ��������
        private decimal closeMarketProfitLossTotal;
        /// <summary>
        /// ��ƽ��ӯ��������
        /// </summary>
        [DataMember]
        public decimal CloseMarketProfitLossTotal
        {
            get
            {
                return closeMarketProfitLossTotal;
            }
            set
            {
                closeMarketProfitLossTotal = value;
            }
        }
        #endregion

    }
}

