using System;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title:�ʽ�ת������ʵ����
    /// Desc.:�ʽ�ת������ʵ����BD_TransferTypeTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create by:���
    /// Create date:2009-07-08
	/// </summary>
	[Serializable]
	public class BD_TransferTypeTableInfo
	{
        /// <summary>
        /// �ʽ�ת������ʵ���๹�캯�� 
        /// </summary>
        public BD_TransferTypeTableInfo()
		{}
		#region Model
		private int _transfertypelogo;
		private string _transfertypename;
		private string _remarks;
		/// <summary>
		/// ת������ID
		/// </summary>
		public int TransferTypeLogo
		{
			set{ _transfertypelogo=value;}
			get{return _transfertypelogo;}
		}
		/// <summary>
		/// ת����������
		/// </summary>
		public string TransferTypeName
		{
			set{ _transfertypename=value;}
			get{return _transfertypename;}
		}
		/// <summary>
		/// ת���������Ʊ�ע
		/// </summary>
		public string Remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		#endregion Model

	}
}

