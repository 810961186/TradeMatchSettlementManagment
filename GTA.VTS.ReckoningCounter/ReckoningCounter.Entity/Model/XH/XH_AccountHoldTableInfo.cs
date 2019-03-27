using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ֻ��ֲ��˻�����ӵ�гֲ���Ϣ��ϸʵ��
    /// Desc: XH_AccountHoldTable-�ֻ��ֲ��˻�����ӵ�гֲ���Ϣ��ϸʵ�塣(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    [DataContract]
    public class XH_AccountHoldTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public XH_AccountHoldTableInfo()
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

        #region CurrencyTypeId ��ǰ���׻�������(���BD_CurrencyType)
        private int currencyTypeId;
        /// <summary>
        /// ��ǰ���׻�������(���BD_CurrencyType)
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

        #region Code �ֲ���Ʒ����(�����������CM_Commodity��ID��Ӧ)
        private string code;
        /// <summary>
        /// �ֲ���Ʒ����(�����������CM_Commodity��ID��Ӧ)
        /// </summary>
        [DataMember]
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }
        #endregion

        #region AvailableAmount �ֲ���Ʒ��������
        private decimal availableAmount;
        /// <summary>
        /// �ֲ���Ʒ��������
        /// </summary>
        [DataMember]
        public decimal AvailableAmount
        {
            get
            {
                return availableAmount;
            }
            set
            {
                availableAmount = value;
            }
        }
        #endregion

        #region FreezeAmount �ֲ���Ʒ��������
        private decimal freezeAmount;
        /// <summary>
        /// �ֲ���Ʒ��������
        /// </summary>
        [DataMember]
        public decimal FreezeAmount
        {
            get
            {
                return freezeAmount;
            }
            set
            {
                freezeAmount = value;
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

        #region HoldAveragePrice �ֲ־���
        private decimal holdAveragePrice;
        /// <summary>
        /// �ֲ־���
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

    }

    /// <summary>
    /// �ֲֵı仯����
    /// </summary>
    public class XH_AccountHoldTableInfo_Delta
    {
        /// <summary>
        /// �ֻ��ֲ�ʵ��
        /// </summary>
        public XH_AccountHoldTableInfo Data;

        /// <summary>
        /// �ֲ��˻�Id
        /// </summary>
        public int AccountHoldLogoId;

        /// <summary>
        /// ���óֱֲ仯��
        /// </summary>
        public decimal AvailableAmountDelta;

        /// <summary>
        /// ����仯��
        /// </summary>
        public decimal FreezeAmountDelta;
    }
}

