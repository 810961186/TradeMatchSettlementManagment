using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///������Ʒ��_�ֻ�_���׷���_�ɽ���_���������� ʵ����XH_SpotRangeCost ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class XH_SpotRangeCost
	{
		public XH_SpotRangeCost()
		{}
		#region Model
		private decimal? _value;
        //private int _fieldrangeid;
		private int _breedclassid;
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
		/// �ֶ�_��Χ��ʶ
		/// </summary>
        //[DataMember]
        //public int FieldRangeID
        //{
        //    set{ _fieldrangeid=value;}
        //    get{return _fieldrangeid;}
        //}
		/// <summary>
		/// Ʒ�ֱ�ʶ
		/// </summary>
        [DataMember]
		public int BreedClassID
		{
			set{ _breedclassid=value;}
			get{return _breedclassid;}
		}
		#endregion Model

	}
}

