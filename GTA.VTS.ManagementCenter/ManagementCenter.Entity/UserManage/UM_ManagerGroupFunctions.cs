using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// ����������Ա����ù��ܱ� ʵ����UM_ManagerGroupFunctions ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
    [DataContract]
	public class UM_ManagerGroupFunctions
	{
		public UM_ManagerGroupFunctions()
		{}
		#region Model
		private int _managegroupfuntctiosid;
		private int? _functionid;
		private int? _managergroupid;

	    private string _functionname;
		/// <summary>
		/// ��������ù��ܱ�ʶ
		/// </summary>
        [DataMember]
		public int ManageGroupFuntctiosID
		{
			set{ _managegroupfuntctiosid=value;}
			get{return _managegroupfuntctiosid;}
		}
		/// <summary>
		/// �û����õĹ��ܱ�ID��
		/// </summary>
        [DataMember]
		public int? FunctionID
		{
			set{ _functionid=value;}
			get{return _functionid;}
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

        /// <summary>
        /// ��������
        /// </summary>
        [DataMember]
        public string FunctionName
        {
            set { _functionname = value; }
            get { return _functionname; }
        }
		#endregion Model

	}
}

