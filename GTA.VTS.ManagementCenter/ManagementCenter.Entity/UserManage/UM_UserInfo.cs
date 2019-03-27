using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// �������û�������Ϣ�� ʵ����UM_UserInfo ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
	/// </summary>
    [DataContract]
	public class UM_UserInfo
	{
		public UM_UserInfo()
		{}
		#region Model
		private string _username;
		private string _loginname;
		private string _password;
		private int _userid;
		private int? _certificatestyle;
		private string _postalcode;
		private int? _roleid;
		private string _certificateno;
		private string _telephone;
		private string _address;
		private string _email;
		private int? _questionid;
		private string _answer;
		private int? _couterid;
		private string _remark;
	    private int _addtype;
	    private DateTime _addtime;

        //��̨����(���ֶν���ѯʱ��)
	    private string _name;
		/// <summary>
		/// ��ʵ����
		/// </summary>
        [DataMember]
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// ��½����
		/// </summary>
        [DataMember]
		public string LoginName
		{
			set{ _loginname=value;}
			get{return _loginname;}
		}
		/// <summary>
		/// ����
		/// </summary>
        [DataMember]
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// �û���ID��
		/// </summary>
        [DataMember]
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// ֤������
		/// </summary>
        [DataMember]
		public int? CertificateStyle
		{
			set{ _certificatestyle=value;}
			get{return _certificatestyle;}
		}
		/// <summary>
		/// ��������
		/// </summary>
        [DataMember]
		public string Postalcode
		{
			set{ _postalcode=value;}
			get{return _postalcode;}
		}
		/// <summary>
		/// ��ɫ��Ϣ��ID��
		/// </summary>
        [DataMember]
		public int? RoleID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// ֤������
		/// </summary>
        [DataMember]
		public string CertificateNo
		{
			set{ _certificateno=value;}
			get{return _certificateno;}
		}
		/// <summary>
		/// ��ϵ�绰
		/// </summary>
        [DataMember]
		public string Telephone
		{
			set{ _telephone=value;}
			get{return _telephone;}
		}
		/// <summary>
		/// ͨѶ��ַ
		/// </summary>
        [DataMember]
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// ��������
		/// </summary>
        [DataMember]
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// ��ʾ����
		/// </summary>
        [DataMember]
		public int? QuestionID
		{
			set{ _questionid=value;}
			get{return _questionid;}
		}
		/// <summary>
		/// ��ʾ�����
		/// </summary>
        [DataMember]
		public string Answer
		{
			set{ _answer=value;}
			get{return _answer;}
		}
		/// <summary>
		/// �����̨ID��
		/// </summary>
        [DataMember]
		public int? CouterID
		{
			set{ _couterid=value;}
			get{return _couterid;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
        [DataMember]
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
	    /// <summary>
	    /// �������
	    /// </summary>
        [DataMember]
	    public int AddType
	    {
            set { _addtype = value; }
            get { return _addtype; }
	    }
	    /// <summary>
	    /// ���ʱ��
	    /// </summary>
        [DataMember]
	    public DateTime AddTime
	    {
             set { _addtime = value; }
            get { return _addtime; }
	    }

        /// <summary>
        /// ��̨����(���ֶν���ѯʱ��)
        /// </summary>
        [DataMember]
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
		#endregion Model

	}
}

