using System;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title:��ί�е�״̬������,ʵ����DB_OrderStatus ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Desc.: ί�е�״̬�����������ݿ��Ӧ
    /// Create by:���
    /// Create date:2009-07-08
	/// </summary>
	[Serializable]
	public class DB_OrderStatusInfo
	{
        /// <summary>
        /// ί�е�״̬������,ʵ���๹�캯�� 
        /// </summary>
        public DB_OrderStatusInfo()
		{}
		#region Model
		private int _orderstatusid;
		private string _orderstatusname;
		/// <summary>
		/// ����״̬ID
		/// </summary>
		public int OrderStatusId
		{
			set{ _orderstatusid=value;}
			get{return _orderstatusid;}
		}
		/// <summary>
		/// ����״̬����
		/// </summary>
		public string OrderStatusName
		{
			set{ _orderstatusname=value;}
			get{return _orderstatusname;}
		}
		#endregion Model

	}
}

