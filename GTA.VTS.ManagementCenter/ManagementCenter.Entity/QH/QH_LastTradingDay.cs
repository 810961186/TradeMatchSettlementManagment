using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///������������� ʵ����QH_LastTradingDay ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����: 2008-12-13
    /// </summary>
    [DataContract]
	public class QH_LastTradingDay
	{
		public QH_LastTradingDay()
		{}
		#region Model
		private int _lasttradingdayid;
		private int? _whatday;
		private int? _whatweek;
		private int? _week;
		private int  _sequence;
		private int? _lasttradingdaytypeid;
		/// <summary>
		/// ������ձ�ʶ
		/// </summary>
        [DataMember]
		public int LastTradingDayID
		{
			set{ _lasttradingdayid=value;}
			get{return _lasttradingdayid;}
		}
		/// <summary>
		/// �ڼ���
		/// </summary>
        [DataMember]
		public int? WhatDay
		{
			set{ _whatday=value;}
			get{return _whatday;}
		}
		/// <summary>
		/// �ڼ���
		/// </summary>
        [DataMember]
		public int? WhatWeek
		{
			set{ _whatweek=value;}
			get{return _whatweek;}
		}
		/// <summary>
		/// ���ڼ�
		/// </summary>
        [DataMember]
		public int? Week
		{
			set{ _week=value;}
			get{return _week;}
		}
		/// <summary>
		/// ˳������
		/// </summary>
        [DataMember]
		public int Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// ����������ͱ�ʶ
		/// </summary>
        [DataMember]
		public int? LastTradingDayTypeID
		{
			set{ _lasttradingdaytypeid=value;}
			get{return _lasttradingdaytypeid;}
		}
		#endregion Model

	}
}

