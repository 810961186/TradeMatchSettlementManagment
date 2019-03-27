using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�������ֻ�_Ʒ��_�ǵ��� ʵ����XH_SpotHighLowValue ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class XH_SpotHighLowValue
	{
		public XH_SpotHighLowValue()
		{}
		#region Model
		private int _hightlowvalueid;
        private decimal? _fundYestclosepricescale;
		private decimal? _righthighlowscale;
		private decimal? _normalvalue;
		private decimal? _stvalue;
		private int? _breedclasshighlowid;
		/// <summary>
		/// �ǵ���ȡֵ��ʶ
		/// </summary>
        [DataMember]
		public int HightLowValueID
		{
			set{ _hightlowvalueid=value;}
			get{return _hightlowvalueid;}
		}
		/// <summary>
		/// �����������̼۵����°ٷֱ���
		/// </summary>
        [DataMember]
        public decimal? FundYestClosePriceScale
		{
			set{ _fundYestclosepricescale=value;}
			get{return _fundYestclosepricescale;}
		}
		/// <summary>
		/// Ȩ֤�ǵ�������
		/// </summary>
        [DataMember]
		public decimal? RightHighLowScale
		{
			set{ _righthighlowscale=value;}
			get{return _righthighlowscale;}
		}
		/// <summary>
		/// ������ҵ
		/// </summary>
        [DataMember]
		public decimal? NormalValue
		{
			set{ _normalvalue=value;}
			get{return _normalvalue;}
		}
		/// <summary>
		/// ST��ҵ
		/// </summary>
        [DataMember]
		public decimal? StValue
		{
			set{ _stvalue=value;}
			get{return _stvalue;}
		}
		/// <summary>
		/// Ʒ���ǵ�����ʶ
		/// </summary>
        [DataMember]
		public int? BreedClassHighLowID
		{
			set{ _breedclasshighlowid=value;}
			get{return _breedclasshighlowid;}
		}
		#endregion Model

	}
}

