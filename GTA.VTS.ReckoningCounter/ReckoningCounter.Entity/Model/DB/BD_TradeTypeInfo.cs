using System;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title:�ɽ�����
    /// Desc.:�ɽ�����ʵ����(�������ɽ��������ɽ��ȣ�BD_TradeType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create by:���
    /// Create date:2009-07-08
	/// </summary>
	[Serializable]
	public class BD_TradeTypeInfo
	{
        /// <summary>
        /// �ɽ����͹��캯�� 
        /// </summary>
        public BD_TradeTypeInfo()
		{}
		#region Model
		private int _tradetypeid;
		private string _tradetypename;
		/// <summary>
		/// �ɽ�����ID
		/// </summary>
		public int TradeTypeId
		{
			set{ _tradetypeid=value;}
			get{return _tradetypeid;}
		}
		/// <summary>
		/// �ɽ���������
		/// </summary>
		public string TradeTypeName
		{
			set{ _tradetypename=value;}
			get{return _tradetypename;}
		}
		#endregion Model

	}
}

