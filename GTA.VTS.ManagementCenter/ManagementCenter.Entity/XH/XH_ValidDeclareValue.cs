using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// ��������Ч�걨ȡֵ ʵ����XH_ValidDeclareValue ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2008-12-2  �޸����ڣ�2009-7-23
    /// </summary>
    [DataContract]
	public class XH_ValidDeclareValue
	{
		public XH_ValidDeclareValue()
		{}
		#region Model
		private int _validdeclarevalueid;
		private decimal? _upperlimit;
		private decimal? _lowerlimit;
        //private int? _ismarketnewday;
		private int? _breedclassvalidid;
	    private decimal? _newDayUpperLimit;
	    private decimal? _newDayLowerLimit;
		/// <summary>
		/// ��Ч�걨ȡֵ��ʶ
		/// </summary>
        [DataMember]
		public int ValidDeclareValueID
		{
			set{ _validdeclarevalueid=value;}
			get{return _validdeclarevalueid;}
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
        ///// <summary>
        ///// �Ƿ�����������
        ///// </summary>
        //[DataMember]
        //public int? IsMarketNewDay
        //{
        //    set{ _ismarketnewday=value;}
        //    get{return _ismarketnewday;}
        //}
        /// <summary>
        /// ������������
        /// </summary>
        [DataMember]
        public decimal? NewDayUpperLimit
        {
            set { _newDayUpperLimit = value; }
            get { return _newDayUpperLimit; }
        }
        /// <summary>
        /// ������������
        /// </summary>
        [DataMember]
        public decimal? NewDayLowerLimit
        {
            set { _newDayLowerLimit = value; }
            get { return _newDayLowerLimit; }
        }
		/// <summary>
		/// Ʒ����Ч�걨��ʶ
		/// </summary>
        [DataMember]
		public int? BreedClassValidID
		{
			set{ _breedclassvalidid=value;}
			get{return _breedclassvalidid;}
		}
		#endregion Model

	}
}

