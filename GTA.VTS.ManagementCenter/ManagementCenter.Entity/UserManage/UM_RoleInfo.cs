using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// ��������ɫ��Ϣ�� ʵ����UM_RoleInfo ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
	/// </summary>
    [DataContract]
	public class UM_RoleInfo
	{
		public UM_RoleInfo()
		{}
		#region Model
		private int _roleid;
		private string _rolename;
		/// <summary>
		/// ��ɫ��Ϣ��ID��
		/// </summary>
        [DataMember]
		public int RoleID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// ��ɫ����
		/// </summary>
        [DataMember]
		public string RoleName
		{
			set{ _rolename=value;}
			get{return _rolename;}
		}
		#endregion Model

	}
}

