using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�������ڻ�_�ֲ����� ʵ����QH_PositionLimitValue ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// Desc.:���ӳֲ���������PositionLimitType
    /// Update by������
    /// Update date:2010-01-19
    /// </summary>
    [DataContract]
	public class QH_PositionLimitValue
	{
		public QH_PositionLimitValue()
		{}
		#region Model
		private int? _start;
		private decimal? _positionvalue;
		private int _deliverymonthtypeid;
		private int _positionlimitvalueid;
		private int? _ends;
		private int? _breedclassid;
		private int? _upperlimitifequation;
		private int? _lowerlimitifequation;
		private int? _positionbailtypeid;
		private int? _positionvaluetypeid;
		/// <summary>
		/// ��ʼ
		/// </summary>
        [DataMember]
		public int? Start
		{
			set{ _start=value;}
			get{return _start;}
		}
		/// <summary>
		/// �ֲ�
		/// </summary>
        [DataMember]
		public decimal? PositionValue
		{
			set{ _positionvalue=value;}
			get{return _positionvalue;}
		}
		/// <summary>
		/// �����·����ͱ�ʶ
        /// 1�������·� 2��һ���·� 3�������·�ǰһ���� 4�������·�ǰ������ 5�������·�ǰ������
		/// </summary>
        [DataMember]
		public int DeliveryMonthType
		{
			set{ _deliverymonthtypeid=value;}
			get{return _deliverymonthtypeid;}
		}
		/// <summary>
		/// �ڻ�-�ֲ����Ʊ�ʶ
		/// </summary>
        [DataMember]
		public int PositionLimitValueID
		{
			set{ _positionlimitvalueid=value;}
			get{return _positionlimitvalueid;}
		}
		/// <summary>
		/// ����
		/// </summary>
        [DataMember]
		public int? Ends
		{
			set{ _ends=value;}
			get{return _ends;}
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
		/// �����Ƿ�����
		/// </summary>
        [DataMember]
		public int? UpperLimitIfEquation
		{
			set{ _upperlimitifequation=value;}
			get{return _upperlimitifequation;}
		}
		/// <summary>
		/// �����Ƿ�����
		/// </summary>
        [DataMember]
		public int? LowerLimitIfEquation
		{
			set{ _lowerlimitifequation=value;}
			get{return _lowerlimitifequation;}
		}
		/// <summary>
		/// �ֲֺͱ�֤��������ͱ�ʶ
		/// </summary>
        [DataMember]
		public int? PositionBailTypeID
		{
			set{ _positionbailtypeid=value;}
			get{return _positionbailtypeid;}
		}
		/// <summary>
		/// ��Ʒ�ڻ�_�ֲ�ȡֵ��ʶ
		/// </summary>
        [DataMember]
		public int? PositionValueTypeID
		{
			set{ _positionvaluetypeid=value;}
			get{return _positionvaluetypeid;}
		}

        ///// <summary>
        ///// �ֲ���������
        ///// 1��һ���·ݳֲ��޶� 2���ٽ������³ֲ��޶� 3�������³ֲ��޶� 4����С���λ�������޶�
        ///// </summary>
        //[DataMember]
        //public int PositionLimitType
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// ��С���λ�������޶�
        /// </summary>
        [DataMember]
        public int? MinUnitLimit
        {
            get;
            set;
        }

		#endregion Model

	}
}

