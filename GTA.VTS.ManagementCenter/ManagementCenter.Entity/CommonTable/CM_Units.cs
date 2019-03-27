using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///��������λ�� ʵ����CM_Units ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
	/// </summary>
    [DataContract]
	public class CM_Units
	{
		public CM_Units()
		{}
		#region Model
		private int _unitsid;
		private string _unitsname;
		/// <summary>
		/// ��λ��ʶ
		/// </summary>
        [DataMember]
		public int UnitsID
		{
			set{ _unitsid=value;}
			get{return _unitsid;}
		}
		/// <summary>
		/// ��λ����
		/// </summary>
        [DataMember]
		public string UnitsName
		{
			set{ _unitsname=value;}
			get{return _unitsname;}
		}
		#endregion Model

	}
}

