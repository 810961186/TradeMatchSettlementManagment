using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///������Ʒ��_�ֻ�_���׷��� ʵ����XH_SpotCosts ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class XH_SpotCosts
	{
		public XH_SpotCosts()
		{}
		#region Model
		private int? _getvaluetypeid;
        private decimal _stampduty;
        private decimal _stampdutystartingpoint;
		private decimal? _commision;
        private decimal _transfertoll;
        private decimal? _monitoringfee;
        private decimal? _transfertollstartingpoint;
        private decimal? _clearingfees;
        private decimal _commisionstartingpoint;
        private decimal? _poundagesinglevalue;
        private decimal? _systemtoll;
		private int _breedclassid;
		private int? _currencytypeid;
		private int? _stampdutytypeid;
	    private int _transferTollTypeID;
		/// <summary>
		/// ���׹���_ȡֵ���ͱ�ʶ(��ֵ���Ƿ�Χֵ)
		/// </summary>
        [DataMember]
		public int? GetValueTypeID
		{
			set{ _getvaluetypeid=value;}
			get{return _getvaluetypeid;}
		}
		/// <summary>
		/// ӡ��˰
		/// </summary>
        [DataMember]
        public decimal StampDuty
		{
			set{ _stampduty=value;}
			get{return _stampduty;}
		}
		/// <summary>
		/// ӡ��˰���
		/// </summary>
        [DataMember]
        public decimal StampDutyStartingpoint
		{
			set{ _stampdutystartingpoint=value;}
			get{return _stampdutystartingpoint;}
		}
		/// <summary>
		/// Ӷ��
		/// </summary>
        [DataMember]
		public decimal? Commision
		{
			set{ _commision=value;}
			get{return _commision;}
		}
		/// <summary>
		/// ������
		/// </summary>
        [DataMember]
        public decimal TransferToll
		{
			set{ _transfertoll=value;}
			get{return _transfertoll;}
		}
		/// <summary>
		/// ��ܷ�
		/// </summary>
        [DataMember]
        public decimal? MonitoringFee
		{
			set{ _monitoringfee=value;}
			get{return _monitoringfee;}
		}
		/// <summary>
		/// ���������
		/// </summary>
        [DataMember]
        public decimal? TransferTollStartingpoint
		{
			set{ _transfertollstartingpoint=value;}
			get{return _transfertollstartingpoint;}
		}

        /// <summary>
        /// ������ȡֵ���ͱ�ʶ
        /// </summary>
        [DataMember]
	    public int TransferTollTypeID
	    {
            set { _transferTollTypeID = value; }
            get { return _transferTollTypeID; }
	    }

		/// <summary>
		/// �����
		/// </summary>
        [DataMember]
        public decimal? ClearingFees
		{
			set{ _clearingfees=value;}
			get{return _clearingfees;}
		}
		/// <summary>
		/// Ӷ�����
		/// </summary>
        [DataMember]
        public decimal CommisionStartingpoint
		{
			set{ _commisionstartingpoint=value;}
			get{return _commisionstartingpoint;}
		}
		/// <summary>
		/// ���������ѵ�ֵ
		/// </summary>
        [DataMember]
        public decimal? PoundageSingleValue
		{
			set{ _poundagesinglevalue=value;}
			get{return _poundagesinglevalue;}
		}
		/// <summary>
		/// ����ϵͳʹ�÷�
		/// </summary>
        [DataMember]
        public decimal? SystemToll
		{
			set{ _systemtoll=value;}
			get{return _systemtoll;}
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
		/// ���׻������ͱ�ʶ
		/// </summary>
        [DataMember]
		public int? CurrencyTypeID
		{
			set{ _currencytypeid=value;}
			get{return _currencytypeid;}
		}
		/// <summary>
		/// ӡ��˰���߻���˫��ȡֵ
		/// </summary>
        [DataMember]
		public int? StampDutyTypeID
		{
			set{ _stampdutytypeid=value;}
			get{return _stampdutytypeid;}
		}
		#endregion Model

	}
}

