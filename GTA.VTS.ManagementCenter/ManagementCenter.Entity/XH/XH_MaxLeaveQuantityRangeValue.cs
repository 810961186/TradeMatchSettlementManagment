using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///���������׹���_���ί����_��Χ_ֵ ʵ����XH_MaxLeaveQuantityRangeValue ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class XH_MaxLeaveQuantityRangeValue
	{
		public XH_MaxLeaveQuantityRangeValue()
		{}
		#region Model
		private decimal? _value;
		private int _breedclassid;
		private int _fieldrangeid;
		/// <summary>
		/// ֵ
		/// </summary>
        [DataMember]
		public decimal? Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		/// <summary>
		/// Ʒ�ֱ�ʶ
		/// </summary>
        [DataMember]
		public int BreedClassID
		{
			set{ _breedclassid=value;}
			get{return _breedclassid;}
		}
		/// <summary>
		/// �ֶ�_��Χ��ʶ
		/// </summary>
        [DataMember]
		public int FieldRangeID
		{
			set{ _fieldrangeid=value;}
			get{return _fieldrangeid;}
		}
		#endregion Model

	}
}

