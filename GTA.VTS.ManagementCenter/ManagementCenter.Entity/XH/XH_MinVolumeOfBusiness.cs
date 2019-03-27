using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///���������׹���_���׷���_���׵�λ_������(��С���׵�λ) ʵ����XH_MinVolumeOfBusiness ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class XH_MinVolumeOfBusiness
	{
		public XH_MinVolumeOfBusiness()
		{}
		#region Model
		private int _minvolumeofbusinessid;
		private int? _volumeofbusiness;
		private int? _unitid;
		private int? _tradewayid;
		private int? _breedclassid;
		/// <summary>
		/// ��С���׵�λ��ʶ
		/// </summary>
        [DataMember]
		public int MinVolumeOfBusinessID
		{
			set{ _minvolumeofbusinessid=value;}
			get{return _minvolumeofbusinessid;}
		}
		/// <summary>
		/// �ɽ�����
		/// </summary>
        [DataMember]
		public int? VolumeOfBusiness
		{
			set{ _volumeofbusiness=value;}
			get{return _volumeofbusiness;}
		}
		/// <summary>
		/// ��λ��ʶ
		/// </summary>
        [DataMember]
		public int? UnitID
		{
			set{ _unitid=value;}
			get{return _unitid;}
		}
		/// <summary>
		/// ���׷����ʶ
		/// </summary>
        [DataMember]
		public int? TradeWayID
		{
			set{ _tradewayid=value;}
			get{return _tradewayid;}
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

