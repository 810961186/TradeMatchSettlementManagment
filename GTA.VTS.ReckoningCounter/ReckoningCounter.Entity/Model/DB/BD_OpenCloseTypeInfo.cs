using System;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title:�ڻ���ƽ������ʵ��
    /// Desc.:�ڻ���ƽ������BD_OpenCloseType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create by:���
    /// Create date:2009-07-08
	/// </summary>
	[Serializable]
	public class BD_OpenCloseTypeInfo
	{
        /// <summary>
        /// �ڻ���ƽ������ʵ�幹�캯�� 
        /// </summary>
        public BD_OpenCloseTypeInfo()
		{}
		#region Model
		private int _octid;
		private string _octname;
		/// <summary>
		/// �ڻ��������ͣ�ƽ���֣�ID
		/// </summary>
		public int OCTId
		{
			set{ _octid=value;}
			get{return _octid;}
		}
		/// <summary>
		/// �ڻ�������������
		/// </summary>
		public string OCTName
		{
			set{ _octname=value;}
			get{return _octname;}
		}
		#endregion Model

	}
}

