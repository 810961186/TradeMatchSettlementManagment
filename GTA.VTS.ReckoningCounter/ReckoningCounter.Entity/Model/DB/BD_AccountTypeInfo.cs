using System;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title:�ʻ�����ʵ����
    /// Desc.:�ʻ�����ʵ����BD_AccountType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create by:���
    /// Create date:2009-07-08
	/// </summary>
	[Serializable]
	public class BD_AccountTypeInfo
	{
        /// <summary>
        ///�ʻ�����ʵ���๹�캯�� 
        /// </summary>
		public BD_AccountTypeInfo()
		{}
		#region Model
		private int _accounttypelogo;
		private string _accounttypename;
		private string _remarks;
		private int? _atcid;
		private int? _relationaccountid;
		/// <summary>
		/// �˻���������ID
		/// </summary>
		public int AccountTypeLogo
		{
			set{ _accounttypelogo=value;}
			get{return _accounttypelogo;}
		}
		/// <summary>
		/// �˻���������
		/// </summary>
		public string AccountTypeName
		{
			set{ _accounttypename=value;}
			get{return _accounttypename;}
		}
		/// <summary>
		/// �˻����ͱ�ע
		/// </summary>
		public string Remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		/// <summary>
		/// �˻�������������ID�����DB_AccountTypeClass��
		/// </summary>
		public int? ATCId
		{
			set{ _atcid=value;}
			get{return _atcid;}
		}
		/// <summary>
		/// �˻��������˺�ID����֤ȯ�ʽ��˻������֤��ɶ����룩
		/// </summary>
		public int? RelationAccountId
		{
			set{ _relationaccountid=value;}
			get{return _relationaccountid;}
		}
		#endregion Model

	}
}

