using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�������۶�_ʱ��α�ʶ ʵ����CM_FuseTimesection ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class CM_FuseTimesection
	{
		public CM_FuseTimesection()
		{}
		#region Model
		private int _timesectionid;
		private string _commoditycode;
		private DateTime? _starttime;
		private DateTime? _endtime;
		/// <summary>
		/// ʱ��α�ʾ
		/// </summary>
        [DataMember]
		public int TimesectionID
		{
			set{ _timesectionid=value;}
			get{return _timesectionid;}
		}
		/// <summary>
		/// ��Ʒ����
		/// </summary>
        [DataMember]
		public string CommodityCode
		{
			set{ _commoditycode=value;}
			get{return _commoditycode;}
		}
		/// <summary>
		/// ������ʼʱ��
		/// </summary>
        [DataMember]
		public DateTime? StartTime
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
		/// �����ֹʱ��
		/// </summary>
        [DataMember]
		public DateTime? EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		#endregion Model

	}
}

