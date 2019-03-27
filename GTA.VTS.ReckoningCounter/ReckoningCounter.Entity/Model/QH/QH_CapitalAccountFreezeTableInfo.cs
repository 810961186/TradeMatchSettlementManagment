using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title: �ڻ��ʽ��˻�����ʵ��
    /// Desc: �ڻ��ʽ��˻�����ʵ����QH_CapitalAccountFreezeTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    [DataContract]
    public class QH_CapitalAccountFreezeTableInfo
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public QH_CapitalAccountFreezeTableInfo()
        { }
        #region CapitalFreezeLogoId �ڻ��ʽ��˺Ŷ�����ϸID(����)
        private int capitalFreezeLogoId;
        /// <summary>
        /// �ڻ��ʽ��˺Ŷ�����ϸID(����)
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

        #region CapitalAccountLogo �ڻ��ʽ��˻����ID(���QH_CapitalAccountTable)
        private int capitalAccountLogo;
        /// <summary>
        /// �ڻ��ʽ��˻����ID(���QH_CapitalAccountTable)
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

        #region FreezeAmount �����ܶ�
        private decimal freezeAmount;
        /// <summary>
        /// �����ܶ�
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

        #region FreezeCost ������������
        private decimal freezeCost;
        /// <summary>
        /// ������������
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
    /// Title: �۹��ʽ��˻�����ͳ��ʵ����
    /// Desc: �۹��ʽ��˻�����ͳ��ʵ����QH_CapitalAccountFreezeSum 
    /// Create By: ���
    /// Create Date: 2009-10-15
    /// </summary>
    public class QH_CapitalAccountFreezeSum
    {
        #region CapitalAccountLogo �ڻ��ʽ��˺ű��ID(���QH_CapitalAccountTable)
        private int capitalAccountLogo;
        /// <summary>
        /// �ڻ��ʽ��˺ű��ID(���QH_CapitalAccountTable)
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

