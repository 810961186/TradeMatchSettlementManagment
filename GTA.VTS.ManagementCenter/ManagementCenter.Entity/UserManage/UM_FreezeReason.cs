using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// �������ʽ𶳽�� ʵ����UM_FreezeReason ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
    [DataContract]
	public class UM_FreezeReason
	{
		public UM_FreezeReason()
		{}
		#region Model
		private int _freezereasonid;
		private string _freezereason;
		private DateTime? _freezereasontime;
		private DateTime? _thawreasontime;
		private string _dealeraccoutid;
		private int? _isauto;
		/// <summary>
		/// ����ԭ���ʶ
		/// </summary>
        [DataMember]
		public int FreezeReasonID
		{
			set{ _freezereasonid=value;}
			get{return _freezereasonid;}
		}
		/// <summary>
		/// ����ԭ��
		/// </summary>
        [DataMember]
		public string FreezeReason
		{
			set{ _freezereason=value;}
			get{return _freezereason;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
        [DataMember]
		public DateTime? FreezeReasonTime
		{
			set{ _freezereasontime=value;}
			get{return _freezereasontime;}
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
		/// <summary>
		/// �Ƿ��Զ��ⶳ
		/// </summary>
        [DataMember]
		public int? IsAuto
		{
			set{ _isauto=value;}
			get{return _isauto;}
		}
		#endregion Model

	}
}

