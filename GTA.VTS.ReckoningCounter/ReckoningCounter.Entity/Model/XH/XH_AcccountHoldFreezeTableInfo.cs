using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ֻ��ֲ��˺ŵ����ֲֶ�����ϸʵ����
    /// Desc: XH_AcccountHoldFreezeTable�ֻ��ֲ��˺ŵ����ֲֶ�����ϸ ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    [DataContract]
    public class XH_AcccountHoldFreezeTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public XH_AcccountHoldFreezeTableInfo()
        { }
        #region HoldFreezeLogoId �ֻ��ֲ��˺Ŷ����¼����
        private int holdFreezeLogoId;
        /// <summary>
        /// �ֻ��ֲ��˺Ŷ����¼����
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

        #region PrepareFreezeAmount ׼����������
        private int prepareFreezeAmount;
        /// <summary>
        /// ׼����������
        /// </summary>
        [DataMember]
        public int PrepareFreezeAmount
        {
            get
            {
                return prepareFreezeAmount;
            }
            set
            {
                prepareFreezeAmount = value;
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

        #region AccountHoldLogo ��ӵ�гֲ�ID(XH_AccountHoldTable)�����Ƕ����ֲֵĶ���
        private int accountHoldLogo;
        /// <summary>
        /// ��ӵ�гֲ�ID(XH_AccountHoldTable)�����Ƕ����ֲֵĶ���
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

