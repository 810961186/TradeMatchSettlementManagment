using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///���������׹���_ȡֵ���ͱ� ʵ����CM_ValueType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class CM_ValueType
	{
		public CM_ValueType()
		{}
		#region Model
		private string _name;
		private int _valuetypeid;
		/// <summary>
		/// ����
		/// </summary>
        [DataMember]
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// ���׹���_ȡֵ���ͱ�ʶ
		/// </summary>
		[DataMember]
		public int ValueTypeID
		{
			set{ _valuetypeid=value;}
			get{return _valuetypeid;}
		}
		#endregion Model

	}
}

