using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///����������ί����  ʵ����QH_SingleRequestQuantity ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����: 2008-12-13
    /// </summary>
    [DataContract]
	public class QH_SingleRequestQuantity
	{
		public QH_SingleRequestQuantity()
		{}
		#region Model
		private int _singlerequestquantityid;
		private int? _MaxConsignQuanturm;
		private int? _consignquantumid;
		private int? _consigninstructiontypeid;
		/// <summary>
		/// ����ί������ʶ
		/// </summary>
        [DataMember]
		public int SingleRequestQuantityID
		{
			set{ _singlerequestquantityid=value;}
			get{return _singlerequestquantityid;}
		}
		/// <summary>
		/// ί����
		/// </summary>
        [DataMember]
		public int? MaxConsignQuanturm
		{
			set{ _MaxConsignQuanturm=value;}
			get{return _MaxConsignQuanturm;}
		}
		/// <summary>
		/// ���׹���ί������ʶ
		/// </summary>
        [DataMember]
		public int? ConsignQuantumID
		{
			set{ _consignquantumid=value;}
			get{return _consignquantumid;}
		}
		/// <summary>
		/// ί��ָ�����ͱ�ʶ
		/// </summary>
        [DataMember]
		public int? ConsignInstructionTypeID
		{
			set{ _consigninstructiontypeid=value;}
			get{return _consigninstructiontypeid;}
		}
		#endregion Model

	}
}

