using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///����������������_�ǽ������� ʵ����CM_NotTradeDate ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2008-11-26
    /// </summary>
    [DataContract]
	public class CM_NotTradeDate
	{
		public CM_NotTradeDate()
		{}
		#region Model
		private int _nottradedateid;
		//private int? _nottradedatemonth;
		private DateTime? _nottradeday;
		private int? _boursetypeid;
		/// <summary>
		/// �ǽ������ڱ�ʶ
		/// </summary>
        [DataMember]
		public int NotTradeDateID
		{
			set{ _nottradedateid=value;}
			get{return _nottradedateid;}
		}
		/// <summary>
		/// �ǽ����·�
		/// </summary>
        //public int? NotTradeDateMonth
        //{
        //    set{ _nottradedatemonth=value;}
        //    get{return _nottradedatemonth;}
        //}
		/// <summary>
		/// �ǽ�����
		/// </summary>
        [DataMember]
		public DateTime? NotTradeDay
		{
			set{ _nottradeday=value;}
			get{return _nottradeday;}
		}
		/// <summary>
		/// ���������ͱ�ʶ
		/// </summary>
        [DataMember]
		public int? BourseTypeID
		{
			set{ _boursetypeid=value;}
			get{return _boursetypeid;}
		}
		#endregion Model

	}
}

