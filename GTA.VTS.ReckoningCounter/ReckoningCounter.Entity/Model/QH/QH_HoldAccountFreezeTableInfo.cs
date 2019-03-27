using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ڻ��ֲֶ����¼ʵ����
    /// Desc: �ڻ��ֲֶ����¼ʵ����QH_HoldAccountFreezeTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    [DataContract]
    public class QH_HoldAccountFreezeTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public QH_HoldAccountFreezeTableInfo()
        { }

        #region HoldFreezeLogoId �ڻ��ֲ��˺Ŷ����¼����
        private int holdFreezeLogoId;
        /// <summary>
        /// �ڻ��ֲ��˺Ŷ����¼����
        /// </summary>
        [DataMember]
        public int HoldFreezeLogoId
        {
            get
            {
                return holdFreezeLogoId;
            }
            set
            {
                holdFreezeLogoId = value;
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

        #region FreezeAmount ��������
        private int freezeAmount;
        /// <summary>
        /// ��������
        /// </summary>
        [DataMember]
        public int FreezeAmount
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

        #region AccountHoldLogo ��ӵ�гֲ�ID(���QH_HoldAccountTable)�����Ƕ����ֲֵĶ���
        private int accountHoldLogo;
        /// <summary>
        /// ��ӵ�гֲ�ID(���QH_HoldAccountTable)�����Ƕ����ֲֵĶ���
        /// </summary>
        [DataMember]
        public int AccountHoldLogo
        {
            get
            {
                return accountHoldLogo;
            }
            set
            {
                accountHoldLogo = value;
            }
        }
        #endregion

        #region FreezeTypeLogo ��������ID(���BD_FreezeType)
        private int freezeTypeLogo;
        /// <summary>
        /// ��������ID(���BD_FreezeType)
        /// </summary>
        [DataMember]
        public int FreezeTypeLogo
        {
            get
            {
                return freezeTypeLogo;
            }
            set
            {
                freezeTypeLogo = value;
            }
        }
        #endregion

        #region ThawTime �ⶳʱ��
        private DateTime? thawTime;
        /// <summary>
        /// �ⶳʱ��
        /// </summary>
        [DataMember]
        public DateTime? ThawTime
        {
            get
            {
                return thawTime;
            }
            set
            {
                thawTime = value;
            }
        }
        #endregion

        #region FreezeTime ����ʱ��
        private DateTime freezeTime;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [DataMember]
        public DateTime FreezeTime
        {
            get
            {
                return freezeTime;
            }
            set
            {
                freezeTime = value;
            }
        }
        #endregion
    }
}

