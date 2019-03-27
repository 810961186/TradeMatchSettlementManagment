using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
    /// <summary>
    ///���������������ͱ� ʵ����CM_BourseType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
    public class CM_BourseType
    {
        public CM_BourseType()
        { }
        #region Model
        private int _boursetypeid;
        private string _boursetypename;
        private DateTime? _receivingconsignstarttime;
        private DateTime? _receivingconsignendtime;
        private DateTime? _counterfromsubmitstarttime;
        private DateTime? _counterfromsubmitendtime;
        private int? _issysdefaultboursetype;
        private int? _deletestate;
        /// <summary>
        /// ���������ͱ�ʶ
        /// </summary>
        [DataMember]
        public int BourseTypeID
        {
            set { _boursetypeid = value; }
            get { return _boursetypeid; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        [DataMember]
        public string BourseTypeName
        {
            set { _boursetypename = value; }
            get { return _boursetypename; }
        }
        /// <summary>
        /// ��Ͻ���ί�п�ʼʱ��
        /// </summary>
        [DataMember]
        public DateTime? ReceivingConsignStartTime
        {
            set { _receivingconsignstarttime = value; }
            get { return _receivingconsignstarttime; }
        }
        /// <summary>
        /// ��Ͻ���ί�н���ʱ��
        /// </summary>
        [DataMember]
        public DateTime? ReceivingConsignEndTime
        {
            set { _receivingconsignendtime = value; }
            get { return _receivingconsignendtime; }
        }
        /// <summary>
        /// ��̨����ί�п�ʼʱ��
        /// </summary>
        [DataMember]
        public DateTime? CounterFromSubmitStartTime
        {
            set { _counterfromsubmitstarttime = value; }
            get { return _counterfromsubmitstarttime; }
        }
        /// <summary>
        /// ��̨����ί�н���ʱ��
        /// </summary>
        [DataMember]
        public DateTime? CounterFromSubmitEndTime
        {
            set { _counterfromsubmitendtime = value; }
            get { return _counterfromsubmitendtime; }
        }

        #region 2009.10.22 �����ֶ�
        /// <summary>
        /// �Ƿ���ϵͳĬ�Ͻ�����
        /// </summary>
        [DataMember]
        public int? ISSysDefaultBourseType
        {
            set { _issysdefaultboursetype = value; }
            get { return _issysdefaultboursetype; }
        }

        /// <summary>
        /// ɾ��״̬
        /// </summary>
        [DataMember]
        public int? DeleteState
        {
            set { _deletestate = value; }
            get { return _deletestate; }
        }

        /// <summary>
        /// ������� 1��6λ���֣�2��5λ���֣�3��Ʒ�ִ���+2λ���+2λ�·ݣ�4��Ʒ�ִ���+1λ���+2λ�·�
        /// </summary>
        [DataMember]
        public int? CodeRulesType
        {
            get;
            set;
        }
        #endregion
        #endregion Model

    }
}

