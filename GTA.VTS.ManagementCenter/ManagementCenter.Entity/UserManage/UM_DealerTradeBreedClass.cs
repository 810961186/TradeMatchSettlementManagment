using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// ������Ʒ������Ȩ�ޱ� ʵ����UM_DealerTradeBreedClass ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
    [DataContract]
	public class UM_DealerTradeBreedClass
	{
		public UM_DealerTradeBreedClass()
		{}
		#region Model
		private int? _breedclassid;
		private int? _userid;
		private int _dealertradebreedclassid;
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
		/// �û���ID��
		/// </summary>
        [DataMember]
		public int? UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// ����Ա�ɽ���Ʒ���б��ʶ
		/// </summary>
        [DataMember]
		public int DealerTradeBreedClassID
		{
			set{ _dealertradebreedclassid=value;}
			get{return _dealertradebreedclassid;}
		}
		#endregion Model

	}
}

