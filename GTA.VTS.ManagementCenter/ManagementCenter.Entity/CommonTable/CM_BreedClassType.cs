using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///������������ƷƷ�����ͱ� ʵ����CM_BreedClassType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class CM_BreedClassType
	{
		public CM_BreedClassType()
		{}
		#region Model
		private int _breedclasstypeid;
		private string _breedclasstypename;
		/// <summary>
		/// Ʒ�����ͱ�ʶ
		/// </summary>
        [DataMember]
		public int BreedClassTypeID
		{
			set{ _breedclasstypeid=value;}
			get{return _breedclasstypeid;}
		}
		/// <summary>
		/// Ʒ����������
		/// </summary>
        [DataMember]
		public string BreedClassTypeName
		{
			set{ _breedclasstypename=value;}
			get{return _breedclasstypename;}
		}
		#endregion Model

	}
}

