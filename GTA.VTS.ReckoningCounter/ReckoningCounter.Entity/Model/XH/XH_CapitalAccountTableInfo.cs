using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ֻ��ʽ��ʵ����
    /// Desc: �ֻ��ʽ��XH_CapitalAccountTableʵ���� ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    [DataContract]
    public class XH_CapitalAccountTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public XH_CapitalAccountTableInfo()
        { }
        #region CapitalAccountLogo �ֻ��ʽ��˻���ID(����)
        private int capitalAccountLogo;
        /// <summary>
        /// �ֻ��ʽ��˻���ID(����)
        /// </summary>
        [DataMember]
        public int CapitalAccountLogo
        {
            get
            {
                return capitalAccountLogo;
            }
            set
            {
                capitalAccountLogo = value;
            }
        }
        #endregion

        #region UserAccountDistributeLogo �û��ֻ��ʽ��˺�ID(���UA_UserAccountAllocationTable)
        private string userAccountDistributeLogo;
        /// <summary>
        /// �û��ֻ��ʽ��˺�ID(���UA_UserAccountAllocationTable)
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

        #region FreezeCapitalTotal ���춳���ʽ���
        private decimal freezeCapitalTotal;
        /// <summary>
        /// ���춳���ʽ���
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

        #region HasDoneProfitLossTotal �ۼ���ʵ��ӯ��
        private decimal hasDoneProfitLossTotal;
        /// <summary>
        /// �ۼ���ʵ��ӯ��
        /// </summary>
        [DataMember]
        public decimal HasDoneProfitLossTotal
        {
            get
            {
                return hasDoneProfitLossTotal;
            }
            set
            {
                hasDoneProfitLossTotal = value;
            }
        }
        #endregion

        
    }
}

