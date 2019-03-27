using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�������г�����ȱ� ʵ����CM_MarketParticipation ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class CM_MarketParticipation
	{
		public CM_MarketParticipation()
		{}
		#region Model
		private float _participation;
		private int _breedclassid;
		/// <summary>
		/// �����
		/// </summary>
        [DataMember]
		public float Participation
		{
			set{ _participation=value;}
			get{return _participation;}
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
		#endregion Model

	}
}

