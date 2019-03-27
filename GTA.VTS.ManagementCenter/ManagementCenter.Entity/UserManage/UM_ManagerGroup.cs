using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// ����������Ա�� ʵ����UM_ManagerGroup ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
    [DataContract]
	public class UM_ManagerGroup
	{
		public UM_ManagerGroup()
		{}
		#region Model
		private int _managergroupid;
		private string _managergroupname;
		/// <summary>
		/// ����Ա��ID��
		/// </summary>
        [DataMember]
		public int ManagerGroupID
		{
			set{ _managergroupid=value;}
			get{return _managergroupid;}
		}
		/// <summary>
		/// ����Ա������
		/// </summary>
        [DataMember]
		public string ManagerGroupName
		{
			set{ _managergroupname=value;}
			get{return _managergroupname;}
		}
		#endregion Model

	}
}

