using System;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title:��������ʵ����
    /// Desc.:��������ʵ����BD_FreezeType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create by:���
    /// Create date:2009-07-08
	/// </summary>
	[Serializable]
	public class BD_FreezeTypeInfo
	{
        /// <summary>
        /// ��������ʵ���๹�캯�� 
        /// </summary>
        public BD_FreezeTypeInfo()
		{}
		#region Model
		private int _freezetypelogo;
		private string _freezedescribe;
		/// <summary>
		/// ������������ID
		/// </summary>
		public int FreezeTypeLogo
		{
			set{ _freezetypelogo=value;}
			get{return _freezetypelogo;}
		}
		/// <summary>
		/// ��������˵�������ƣ�
		/// </summary>
		public string FreezeDescribe
		{
			set{ _freezedescribe=value;}
			get{return _freezedescribe;}
		}
		#endregion Model

	}
}

