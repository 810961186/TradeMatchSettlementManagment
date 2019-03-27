using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///����������������_����ʱ��� ʵ����CM_TradeTime ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class CM_TradeTime
	{
		public CM_TradeTime()
		{}
		#region Model
		private int _tradetimeid;
		private DateTime? _starttime;
		private DateTime? _endtime;
		private int? _boursetypeid;
		/// <summary>
		/// ����������_����ʱ���ʶ
		/// </summary>
        [DataMember]
		public int TradeTimeID
		{
			set{ _tradetimeid=value;}
			get{return _tradetimeid;}
		}
		/// <summary>
		/// ��ʼʱ��
		/// </summary>
        [DataMember]
		public DateTime? StartTime
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
        [DataMember]
		public DateTime? EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
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

