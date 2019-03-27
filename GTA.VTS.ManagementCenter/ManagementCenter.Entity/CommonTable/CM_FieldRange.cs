using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�������ֶ�_��Χ�� ʵ����CM_FieldRange ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class CM_FieldRange
	{
		public CM_FieldRange()
		{}
		#region Model
        private int _fieldrangeid;
		private decimal? _upperlimit;
		private decimal? _lowerlimit;
		private int _upperlimitifequation;
		private int _lowerlimitifequation;
        //private string _propertymarket;
		/// <summary>
		/// �ֶ�_��Χ��ʶ
		/// </summary>
        [DataMember]
		public int FieldRangeID
		{
			set{ _fieldrangeid=value;}
			get{return _fieldrangeid;}
		}
		/// <summary>
		/// ����
		/// </summary>
        [DataMember]
		public decimal? UpperLimit
		{
			set{ _upperlimit=value;}
			get{return _upperlimit;}
		}
		/// <summary>
		/// ����
		/// </summary>
        [DataMember]
		public decimal? LowerLimit
		{
			set{ _lowerlimit=value;}
			get{return _lowerlimit;}
		}
		/// <summary>
		/// �����Ƿ�����
		/// </summary>
        [DataMember]
		public int UpperLimitIfEquation
		{
			set{ _upperlimitifequation=value;}
			get{return _upperlimitifequation;}
		}
		/// <summary>
		/// �����Ƿ�����
		/// </summary>
        [DataMember]
		public int LowerLimitIfEquation
		{
			set{ _lowerlimitifequation=value;}
			get{return _lowerlimitifequation;}
		}
	
		#endregion Model

	}
}

