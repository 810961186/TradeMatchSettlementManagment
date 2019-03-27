using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// �������ⶳԭ��� ʵ����UM_ThawReason ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
	/// </summary>
    [DataContract]
	public class UM_ThawReason
	{
		public UM_ThawReason()
		{}
		#region Model
		private int _thawreasonid;
		private string _thawreason;
		private DateTime? _thawreasontime;
		private string _dealeraccoutid;
		/// <summary>
		/// �ⶳԭ���ʶ
		/// </summary>
        [DataMember]
		public int ThawReasonID
		{
			set{ _thawreasonid=value;}
			get{return _thawreasonid;}
		}
		/// <summary>
		/// �ⶳԭ��
		/// </summary>
        [DataMember]
		public string ThawReason
		{
			set{ _thawreason=value;}
			get{return _thawreason;}
		}
		/// <summary>
		/// �ⶳʱ��
		/// </summary>
        [DataMember]
		public DateTime? ThawReasonTime
		{
			set{ _thawreasontime=value;}
			get{return _thawreasontime;}
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
		#endregion Model

	}
}

