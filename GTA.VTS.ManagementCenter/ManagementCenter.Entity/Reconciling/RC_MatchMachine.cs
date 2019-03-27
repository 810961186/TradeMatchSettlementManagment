using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
	/// ��������ϻ��� ʵ����RC_MatchMachine ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
    [DataContract]
	public class RC_MatchMachine
	{
		public RC_MatchMachine()
		{}
		#region Model
		private int _matchmachineid;
		private string _matchmachinename;
		private int? _boursetypeid;
		private int? _matchcenterid;
		/// <summary>
		/// ��ϻ����
		/// </summary>
        [DataMember]
		public int MatchMachineID
		{
			set{ _matchmachineid=value;}
			get{return _matchmachineid;}
		}
		/// <summary>
		/// ��ϻ�����
		/// </summary>
        [DataMember]
		public string MatchMachineName
		{
			set{ _matchmachinename=value;}
			get{return _matchmachinename;}
		}
		/// <summary>
		/// ���������ͱ�ʶ
		/// </summary>
        [DataMember]
		public int? BourseTypeID
		{
			set{ _boursetypeid=value;}
			get{return _boursetypeid;}
		}
		/// <summary>
		/// ������ı��
		/// </summary>
        [DataMember]
		public int? MatchCenterID
		{
			set{ _matchcenterid=value;}
			get{return _matchcenterid;}
		}
		#endregion Model

	}
}

