using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�������ɽ�����Ʒ_�۶ϱ� ʵ����CM_CommodityFuse ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
	/// </summary>
    [DataContract]
	public class CM_CommodityFuse
	{
		public CM_CommodityFuse()
		{}
		#region Model
        //private int? _noallowstarttime;
        private decimal _triggeringscale;
		private int _triggeringduration;
		private int _fusedurationlimit;
		private int _fusetimeofday;
		private string _commoditycode;
		/// <summary>
		/// ����������ʱ������ʱ��(����)
		/// </summary>
        //public int? NoAllowStartTime
        //{
        //    set{ _noallowstarttime=value;}
        //    get{return _noallowstarttime;}
        //}
		/// <summary>
		/// ��������
		/// </summary>
        [DataMember]
		public decimal TriggeringScale
		{
			set{ _triggeringscale=value;}
			get{return _triggeringscale;}
		}
		/// <summary>
		/// ��������ʱ������(����)
		/// </summary>
        [DataMember]
		public int TriggeringDuration
		{
			set{ _triggeringduration=value;}
			get{return _triggeringduration;}
		}
		/// <summary>
		/// �۶ϳ���ʱ������(����)
		/// </summary>
        [DataMember]
		public int FuseDurationLimit
		{
			set{ _fusedurationlimit=value;}
			get{return _fusedurationlimit;}
		}
		/// <summary>
		/// �۶ϴ���(��/��)
		/// </summary>
        [DataMember]
		public int FuseTimeOfDay
		{
			set{ _fusetimeofday=value;}
			get{return _fusetimeofday;}
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
		#endregion Model

	}
}

