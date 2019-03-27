using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// �������ʺű� ʵ����UM_DealerAccount ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
    [DataContract]
	public class UM_DealerAccount
	{
		public UM_DealerAccount()
		{}
		#region Model
		private bool _isdo;
		private int _userid;
		private string _dealeraccoutid;
		private int? _accounttypeid;

	    private int? _accountattributiontype;
		/// <summary>
		/// �Ƿ����
		/// </summary>
        [DataMember]
		public bool IsDo
		{
			set{ _isdo=value;}
			get{return _isdo;}
		}
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
		/// ����Ա�˺ŷ�����ʶ
		/// </summary>
        [DataMember]
		public string DealerAccoutID
		{
			set{ _dealeraccoutid=value;}
			get{return _dealeraccoutid;}
		}
		/// <summary>
		/// �˺����ͱ�ʶ
		/// </summary>
        [DataMember]
		public int? AccountTypeID
		{
			set{ _accounttypeid=value;}
			get{return _accounttypeid;}
		}

        /// <summary>
        /// �ʻ��������ͣ��ֻ��ʽ�ͳֲ֣��ڻ��ʽ�ͳֲ֣������ʺ�
        /// </summary>
        [DataMember]
        public int? AccountAttributionType
        {
            set { _accountattributiontype = value; }
            get { return _accountattributiontype; }
        }
		#endregion Model

	}
}

