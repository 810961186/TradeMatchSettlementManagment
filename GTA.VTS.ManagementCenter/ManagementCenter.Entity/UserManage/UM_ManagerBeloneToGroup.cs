using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// ����������Ա_������� ʵ����UM_ManagerBeloneToGroup ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
	/// </summary>
    [DataContract]
	public class UM_ManagerBeloneToGroup
	{
		public UM_ManagerBeloneToGroup()
		{}
		#region Model
		private int _userid;
		private int? _managergroupid;
		/// <summary>
		/// �û���ID��
		/// </summary>
        [DataMember]
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// ����Ա��ID��
		/// </summary>
        [DataMember]
		public int? ManagerGroupID
		{
			set{ _managergroupid=value;}
			get{return _managergroupid;}
		}
		#endregion Model

	}
}

