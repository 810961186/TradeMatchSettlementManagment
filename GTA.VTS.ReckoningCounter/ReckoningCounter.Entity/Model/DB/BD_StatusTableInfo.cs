using System;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title:ϵͳ״̬���͹����
    /// Desc.:ϵͳ״̬���͹����BD_StatusTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create by:���
    /// Create date:2009-07-08
	/// </summary>
	[Serializable]
	public class BD_StatusTableInfo
	{
        /// <summary>
        /// ϵͳ״̬���͹�����캯�� 
        /// </summary>
        public BD_StatusTableInfo()
		{}
		#region Model
		private string _name;
		private string _value;
		/// <summary>
		/// ״̬�������ƣ�������
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// ״̬����ֵ
		/// </summary>
		public string value
		{
			set{ _value=value;}
			get{return _value;}
		}
		#endregion Model

	}
}

