using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
    /// <summary>
    /// �������ʺ����� ʵ����UM_AccountType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
    [DataContract]
    public class UM_AccountType
    {
        public UM_AccountType()
        { }
        #region Model
        private int _accounttypeid;
        private string _accountname;
        private int? _accountattributiontype;
        private string _remark;
        private int? _connectholdid; 
        /// <summary>
        /// �ʻ�����ID
        /// </summary>
        [DataMember]
        public int AccountTypeID
        {
            set { _accounttypeid = value; }
            get { return _accounttypeid; }
        }
        /// <summary>
        /// �ʻ�����
        /// </summary>
        [DataMember]
        public string AccountName
        {
            set { _accountname = value; }
            get { return _accountname; }
        }
        /// <summary>
        /// �ʻ��������ͣ��ֻ��ʽ�ͳֲ֣��ڻ��ʽ�ͳֲ֣������ʺ�
        /// </summary>
        [DataMember]
        public int? AccountAttributionType
        {
            set { _accountattributiontype = value; }
            get { return _accountattributiontype; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        [DataMember]
        public string ReMark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        /// <summary>
        /// ��Ӧ�ֲ��ʺ�����ID
        /// </summary>
        [DataMember]
        public int? ConnectHoldID
        {
            set { _connectholdid = value; }
            get { return _connectholdid; }
        }
        #endregion Model

    }
}

