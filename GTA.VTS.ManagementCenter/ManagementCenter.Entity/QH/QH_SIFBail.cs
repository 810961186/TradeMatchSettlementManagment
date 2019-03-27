using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///������Ʒ��_��ָ�ڻ�_��֤�� ʵ����QH_SIFBail ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����: 2008-12-13
    /// </summary>
    [DataContract]
	public class QH_SIFBail
	{
		public QH_SIFBail()
		{}
		#region Model
		private decimal _bailscale;
		private int _breedclassid;
		/// <summary>
		/// ��֤�����
		/// </summary>
        [DataMember]
		public decimal BailScale
		{
			set{ _bailscale=value;}
			get{return _bailscale;}
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

