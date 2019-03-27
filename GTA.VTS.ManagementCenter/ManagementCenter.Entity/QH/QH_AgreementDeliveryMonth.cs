using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///��������Լ�����·� ʵ����QH_AgreementDeliveryMonth ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    [DataContract]
	public class QH_AgreementDeliveryMonth
	{
		public QH_AgreementDeliveryMonth()
		{}
		#region Model
		private int _agreementdeliverymonthid;
		private int? _monthid;
		private int? _breedclassid;
		/// <summary>
		/// ��Լ�����·ݱ�ʶ
		/// </summary>
        [DataMember]
		public int AgreementDeliveryMonthID
		{
			set{ _agreementdeliverymonthid=value;}
			get{return _agreementdeliverymonthid;}
		}
		/// <summary>
		/// �·ݱ�ʶ
		/// </summary>
        [DataMember]
		public int? MonthID
		{
			set{ _monthid=value;}
			get{return _monthid;}
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
		#endregion Model

	}
}

