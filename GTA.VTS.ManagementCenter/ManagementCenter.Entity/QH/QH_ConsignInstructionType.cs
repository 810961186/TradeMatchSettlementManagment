using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///������ί��ָ������ ʵ����QH_ConsignInstructionType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����: 2008-12-13
    /// </summary>
    [DataContract]
	public class QH_ConsignInstructionType
	{
		public QH_ConsignInstructionType()
		{}
		#region Model
		private int _consigninstructiontypeid;
		private string _typename;
		/// <summary>
		/// ί��ָ�����ͱ�ʶ
		/// </summary>
        [DataMember] 
		public int ConsignInstructionTypeID
		{
			set{ _consigninstructiontypeid=value;}
			get{return _consigninstructiontypeid;}
		}
		/// <summary>
		/// ��������
		/// </summary>
        [DataMember]
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		#endregion Model

	}
}

