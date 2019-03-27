using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///����:��ҵ�� ʵ����Profession ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�����Ա��������
    /// ���ڣ�2008-11-18
	/// </summary>
    [DataContract]
	public class Profession
	{
		public Profession()
		{}
		#region Model
		private string _nindcd;
		private string _nindnme;
		private string _ennindnme;
		/// <summary>
		/// ��ҵ��ʶ
		/// </summary>
        [DataMember]
		public string Nindcd
		{
			set{ _nindcd=value;}
			get{return _nindcd;}
		}
		/// <summary>
		/// ��ҵ��������
		/// </summary>
        [DataMember]
		public string Nindnme
		{
			set{ _nindnme=value;}
			get{return _nindnme;}
		}
		/// <summary>
		/// ��ҵӢ������
		/// </summary>
        [DataMember]
		public string EnNindnme
		{
			set{ _ennindnme=value;}
			get{return _ennindnme;}
		}
		#endregion Model

	}
}

