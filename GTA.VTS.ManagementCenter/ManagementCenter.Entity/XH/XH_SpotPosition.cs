using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�������ֻ�_������ƷƷ��_�ֲ����� ʵ����XH_SpotPosition ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class XH_SpotPosition
	{
		public XH_SpotPosition()
		{}
		#region Model
		private decimal? _rate;
		private int _breedclassid;
		/// <summary>
		/// �ֱֲ���
		/// </summary>
        [DataMember]
		public decimal? Rate
		{
			set{ _rate=value;}
			get{return _rate;}
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

