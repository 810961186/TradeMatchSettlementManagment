using System;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title:δ����ĳɽ���¼
    /// Desc.:δ����ĳɽ���¼ʵ����BD_UnReckonedDealTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create by:���
    /// Create date:2009-07-08
	/// </summary>
	[Serializable]
	public class BD_UnReckonedDealTableInfo
	{
        /// <summary>
        /// δ����ĳɽ���¼���캯�� 
        /// </summary>
        public BD_UnReckonedDealTableInfo()
		{}
		#region Model
		private string _id;
		private int _entitytype;
		private string _orderno;
		private decimal? _price;
		private int? _amount;
		private DateTime? _time;
		private string _message;
		private bool _issuccess;
		private string _counterid;
		/// <summary>
		/// ��¼�����ʶ(�����Դ�����Ļش���ʵ��ID��ʶ)
		/// </summary>
		public string id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ��ϻر�����(������붨��)
		/// </summary>
		public int EntityType
		{
			set{ _entitytype=value;}
			get{return _entitytype;}
		}
		/// <summary>
		/// ί�е���
		/// </summary>
		public string OrderNo
		{
			set{ _orderno=value;}
			get{return _orderno;}
		}
		/// <summary>
		/// �ɽ��۸�
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// �ɽ�����
		/// </summary>
		public int? Amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// �ɽ�ʱ��
		/// </summary>
		public DateTime? Time
		{
			set{ _time=value;}
			get{return _time;}
		}
		/// <summary>
		/// ���ص���Ϣ
		/// </summary>
		public string Message
		{
			set{ _message=value;}
			get{return _message;}
		}
		/// <summary>
		/// ����Ƿ�ɹ�(Ŀǰ����û��ʲô����)
		/// </summary>
		public bool IsSuccess
		{
			set{ _issuccess=value;}
			get{return _issuccess;}
		}
		/// <summary>
		/// ������Ļ���ͨ���Ż��߹�̨��
		/// </summary>
		public string CounterID
		{
			set{ _counterid=value;}
			get{return _counterid;}
		}
		#endregion Model

	}
}

