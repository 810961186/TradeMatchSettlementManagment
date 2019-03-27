using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
    /// <summary>
    /// ����:������ı� ʵ����RC_MatchCenter ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
    [DataContract]
    public class RC_MatchCenter
    {
        public RC_MatchCenter()
        { }
        #region Model
        private int _matchcenterid;
        private string _matchcentername;
        private string _ip;
        private int? _port;
        private int? _state;
        private string _xiadanservice;
        private string _cuoheservice;
        /// <summary>
        /// �������ID
        /// </summary>
        [DataMember]
        public int MatchCenterID
        {
            set { _matchcenterid = value; }
            get { return _matchcenterid; }
        }
        /// <summary>
        /// �����������
        /// </summary>
        [DataMember]
        public string MatchCenterName
        {
            set { _matchcentername = value; }
            get { return _matchcentername; }
        }
        /// <summary>
        /// IP
        /// </summary>
        [DataMember]
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// �˿�
        /// </summary>
        [DataMember]
        public int? Port
        {
            set { _port = value; }
            get { return _port; }
        }
        /// <summary>
        /// ����״̬
        /// </summary>
        [DataMember]
        public int? State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// ��Ϸ���
        /// </summary>
        [DataMember]
        public string CuoHeService
        {
            set { _cuoheservice = value; }
            get { return _cuoheservice; }
        }
        /// <summary>
        /// �µ�����
        /// </summary>
        [DataMember]
        public string XiaDanService
        {
            set { _xiadanservice = value; }
            get { return _xiadanservice; }
        }


        #endregion Model

    }
}

