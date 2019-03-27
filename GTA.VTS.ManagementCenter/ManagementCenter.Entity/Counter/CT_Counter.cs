using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
    /// <summary>
    /// ����:��̨�� ʵ����CT_Counter ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
    [DataContract]
    public class CT_Counter
    {
        public CT_Counter()
        { }
        #region Model
        private int _couterid;
        private string _name;
        private string _ip;
        private int? _xiaDanServicePort;//Ϊ����ʶ��_port����Ϊ_XiaDanServicePort
        private int? _maxvalues;
        private int? _state;
        private string _xiadanservicename;
        private string _queryservicename;
        private string _accountservicename;
        //2009.04.07�������ֶ�
        private int _accountServicePort;
        private int _queryServicePort;
        private string _sendServiceName;
        private int _sendPort;

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int CouterID
        {
            set { _couterid = value; }
            get { return _couterid; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// �µ�����˿�
        /// </summary>
        [DataMember]
        public int? XiaDanServicePort
        {
            set { _xiaDanServicePort = value; }
            get { return _xiaDanServicePort; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? MaxValues
        {
            set { _maxvalues = value; }
            get { return _maxvalues; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string XiaDanServiceName
        {
            set { _xiadanservicename = value; }
            get { return _xiadanservicename; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string QueryServiceName
        {
            set { _queryservicename = value; }
            get { return _queryservicename; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string AccountServiceName
        {
            set { _accountservicename = value; }
            get { return _accountservicename; }
        }

        /// <summary>
        /// �ʺŷ���˿�
        /// </summary>
        [DataMember]
        public int AccountServicePort
        {
            set { _accountServicePort = value; }
            get { return _accountServicePort; }
        }

        /// <summary>
        /// ��ѯ�˿�
        /// </summary>
        [DataMember]
        public int QueryServicePort
        {
            set { _queryServicePort = value; }
            get { return _queryServicePort; }
        }

        /// <summary>
        /// ���ͷ�������
        /// </summary>
        [DataMember]
        public string SendServiceName
        {
            set { _sendServiceName = value; }
            get { return _sendServiceName; }
        }

        /// <summary>
        /// ���Ͷ˿�
        /// </summary>
        [DataMember]
        public int SendPort
        {
            set { _sendPort = value; }
            get { return _sendPort; }
        }

        #endregion Model

    }
}

