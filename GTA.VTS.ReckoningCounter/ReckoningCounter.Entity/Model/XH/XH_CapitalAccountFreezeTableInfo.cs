using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ֻ��ʽ��˻�����ʵ����
    /// Desc: �ֻ��ʽ��˻�����ʵ����XH_CapitalAccountFreezeTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    [DataContract]
    public class XH_CapitalAccountFreezeTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public XH_CapitalAccountFreezeTableInfo()
        { }
        #region CapitalFreezeLogoId �ֻ��ʽ��˺Ŷ�����ϸID(����)
        private int capitalFreezeLogoId;
        /// <summary>
        /// �ֻ��ʽ��˺Ŷ�����ϸID(����)
        /// </summary>
        [DataMember]
        public int CapitalFreezeLogoId
        {
            get
            {
                return capitalFreezeLogoId;
            }
            set
            {
                capitalFreezeLogoId = value;
            }
        }
        #endregion

        #region CapitalAccountLogo �ֻ��ʽ��˺ű��ID(���QH_CapitalAccountTable)
        private int capitalAccountLogo;
        /// <summary>
        /// �ֻ��ʽ��˺ű��ID(���QH_CapitalAccountTable)
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

        #region EntrustNumber �ֻ�ί�е���
        private string entrustNumber;
        /// <summary>
        /// �ֻ�ί�е���
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

        #region FreezeCapitalAmount ����Ԥ�ɽ����
        private decimal freezeCapitalAmount;
        /// <summary>
        /// ����Ԥ�ɽ����
        /// </summary>
        [DataMember]
        public decimal FreezeCapitalAmount
        {
            get
            {
                return freezeCapitalAmount;
            }
            set
            {
                freezeCapitalAmount = value;
            }
        }
        #endregion

        #region FreezeCost ����Ԥ�ɽ�����(������������)
        private decimal freezeCost;
        /// <summary>
        /// ����Ԥ�ɽ�����(������������)
        /// </summary>
        [DataMember]
        public decimal FreezeCost
        {
            get
            {
                return freezeCost;
            }
            set
            {
                freezeCost = value;
            }
        }
        #endregion

        #region OweCosting Ƿ�ɷ���(���׷��� - ԭʼ�����ʽ����)
        private decimal oweCosting;
        /// <summary>
        /// Ƿ�ɷ���(���׷��� - ԭʼ�����ʽ����)
        /// </summary>
        [DataMember]
        public decimal OweCosting
        {
            get
            {
                return oweCosting;
            }
            set
            {
                oweCosting = value;
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

    /// <summary>
    /// Title: �ֻ��ʽ��˻�����ϼ�ʵ����
    /// Desc: �ֻ��ʽ��˻�����ϼ�ʵ����XH_CapitalAccountFreezeSum ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    public class XH_CapitalAccountFreezeSum
    {
        #region CapitalAccountLogo �ֻ��ʽ��˺ű��ID(���QH_CapitalAccountTable)
        private int capitalAccountLogo;
        /// <summary>
        /// �ֻ��ʽ��˺ű��ID(���QH_CapitalAccountTable)
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

        #region FreezeCapitalSum �����ܽ��
        private decimal freezeCapitalSum;
        /// <summary>
        /// ����Ԥ�ɽ����
        /// </summary>
        [DataMember]
        public decimal FreezeCapitalSum
        {
            get
            {
                return freezeCapitalSum;
            }
            set
            {
                freezeCapitalSum = value;
            }
        }
        #endregion
    }
}

