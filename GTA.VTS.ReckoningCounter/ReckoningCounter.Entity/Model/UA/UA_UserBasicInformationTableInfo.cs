using System;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title: �û�������Ϣʵ��
    /// Desc: �û�������Ϣʵ����UA_UserBasicInformationTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
	/// </summary>
	[Serializable]
	public class UA_UserBasicInformationTableInfo
	{
        /// <summary>
        /// ���캯��
        /// </summary>
        public UA_UserBasicInformationTableInfo()
		{}
		#region Model
		private string _userid;
		private string _password;
		private int _rolenumber;
		/// <summary>
		/// �û�ID(����)
		/// </summary>
		public string UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// �û�����
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// �û���ɫ(���BD_UserRoleType)
		/// </summary>
		public int RoleNumber
		{
			set{ _rolenumber=value;}
			get{return _rolenumber;}
		}
		#endregion Model

	}
}

