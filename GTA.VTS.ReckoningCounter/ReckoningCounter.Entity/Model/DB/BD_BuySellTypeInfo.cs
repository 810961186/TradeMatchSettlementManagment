using System;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title:���׷�������ʵ��
    /// Desc.:���׷�������ʵ�壨������BD_BuySellType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create by:���
    /// Create date:2009-07-08
	/// </summary>
	[Serializable]
	public class BD_BuySellTypeInfo
	{
        /// <summary>
        /// �׷�������ʵ���๹�캯�� 
        /// </summary>
        public BD_BuySellTypeInfo()
		{}
		#region Model
		private int _buysellid;
		private string _buysellname;
		/// <summary>
		/// ��������ID
		/// </summary>
		public int BuysellId
		{
			set{ _buysellid=value;}
			get{return _buysellid;}
		}
		/// <summary>
		/// ������������
		/// </summary>
		public string BuysellName
		{
			set{ _buysellname=value;}
			get{return _buysellname;}
		}
		#endregion Model

	}
}

