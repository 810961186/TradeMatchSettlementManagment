using System;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title:���׵�λ(����-Hand����-Thigh���ȣ�
    /// Desc.:���׵�λʵ����(����-Hand����-Thigh���ȣ�BD_TradeUnit ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create by:���
    /// Create date:2009-07-08
	/// </summary>
	[Serializable]
	public class BD_TradeUnitInfo
	{
        /// <summary>
        /// ���׵�λ(����-Hand����-Thigh���ȣ����캯�� 
        /// </summary>
        public BD_TradeUnitInfo()
		{}
		#region Model
		private int _tradeunitid;
		private string _tradeunitname;
		/// <summary>
		/// �ɽ���λID
		/// </summary>
		public int TradeUnitId
		{
			set{ _tradeunitid=value;}
			get{return _tradeunitid;}
		}
		/// <summary>
		/// �ɽ���λ����
		/// </summary>
		public string TradeUnitName
		{
			set{ _tradeunitname=value;}
			get{return _tradeunitname;}
		}
		#endregion Model

	}
}

