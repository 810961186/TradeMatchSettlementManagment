using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�������ֻ�_Ʒ��_���׵�λ����� ʵ����CM_UnitConversion ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class CM_UnitConversion
	{
		public CM_UnitConversion()
		{}
		#region Model

	    private int _unitConversionID;
		private int? _breedclassid;
		private int? _value;
		private int? _unitidto;
		private int? _unitidfrom;

        /// <summary>
        /// ���׵�λ�����ʶ
        /// </summary>
        [DataMember]
	    public int UnitConversionID
	    {
            set { _unitConversionID = value; }
            get { return _unitConversionID; }
	    }

		/// <summary>
		/// Ʒ�ֱ�ʶ
		/// </summary>
        [DataMember]
		public int? BreedClassID
		{
			set{ _breedclassid=value;}
			get{return _breedclassid;}
		}
		/// <summary>
		/// ��
		/// </summary>
        [DataMember]
		public int? Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		/// <summary>
		/// ��λ��ʶ2
		/// </summary>
        [DataMember]
		public int? UnitIDTo
		{
			set{ _unitidto=value;}
			get{return _unitidto;}
		}
		/// <summary>
		/// ��λ��ʶ1
		/// </summary>
        [DataMember]
		public int? UnitIDFrom
		{
			set{ _unitidfrom=value;}
			get{return _unitidfrom;}
		}
		#endregion Model

	}
}

