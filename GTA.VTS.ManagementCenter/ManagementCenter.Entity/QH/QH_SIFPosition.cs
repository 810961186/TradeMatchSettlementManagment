using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///��������ָ�ڻ��ֲ����� ʵ����QH_SIFPosition ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����: 2008-12-13
    /// </summary>
    [DataContract]
	public class QH_SIFPosition
	{
		public QH_SIFPosition()
		{}
		#region Model
		private int? _unilateralpositions;
		private int _breedclassid;
		/// <summary>
		/// ���ֲ߳���
		/// </summary>
        [DataMember]
		public int? UnilateralPositions
		{
			set{ _unilateralpositions=value;}
			get{return _unilateralpositions;}
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

