using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// ������Ȩ�޹��ܱ� ʵ����UM_Functions ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
    [DataContract]
	public class UM_Functions
	{
		public UM_Functions()
		{}
		#region Model
		private int _functionid;
		private string _functionname;
		/// <summary>
		/// �û����õĹ��ܱ�ID��
		/// </summary>
        [DataMember]
		public int FunctionID
		{
			set{ _functionid=value;}
			get{return _functionid;}
		}
		/// <summary>
		/// ��������
		/// </summary>
        [DataMember]
		public string FunctionName
		{
			set{ _functionname=value;}
			get{return _functionname;}
		}
		#endregion Model

	}
}

