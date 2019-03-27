using System;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title: �˻�����ʵ��
    /// Desc: �˻�����ʵ��UA_UserAccountAllocationTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
	/// </summary>
	[Serializable]
	public class UA_UserAccountAllocationTableInfo
	{
        /// <summary>
        /// ���캯��
        /// </summary>
        public UA_UserAccountAllocationTableInfo()
		{}
		#region Model
		private string _useraccountdistributelogo;
		private bool _whetheravailable;
		private string _userid;
		private int _accounttypelogo;
		/// <summary>
		/// �û��˻�ID(����)
		/// </summary>
		public string UserAccountDistributeLogo
		{
			set{ _useraccountdistributelogo=value;}
			get{return _useraccountdistributelogo;}
		}
		/// <summary>
		/// �˻��Ƿ����
		/// </summary>
		public bool WhetherAvailable
		{
			set{ _whetheravailable=value;}
			get{return _whetheravailable;}
		}
		/// <summary>
		/// �û�ID(���UA_UserBasicInformationTable)
		/// </summary>
		public string UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// �˻����ͱ�ʶID(���BD_AccountType)
		/// </summary>
		public int AccountTypeLogo
		{
			set{ _accounttypelogo=value;}
			get{return _accounttypelogo;}
		}
		#endregion Model

	}
}

