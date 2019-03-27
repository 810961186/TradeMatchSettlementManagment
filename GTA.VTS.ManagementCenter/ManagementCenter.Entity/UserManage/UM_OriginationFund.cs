using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// ��������ʼ�ʽ�� ʵ����UM_OriginationFund ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
    [DataContract]
	public class UM_OriginationFund
	{
		public UM_OriginationFund()
		{}
		#region Model
        private decimal   _fundmoney;
		private string _remark;
		private int _originationfundid;
		private int? _transactioncurrencytypeid;
		private string _dealeraccoutid;
		/// <summary>
		/// �ʽ���
		/// </summary>
        [DataMember]
        public decimal FundMoney
		{
			set{ _fundmoney=value;}
			get{return _fundmoney;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
        [DataMember]
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// ��ʼ�ʽ��ʶ
		/// </summary>
        [DataMember]
		public int OriginationFundID
		{
			set{ _originationfundid=value;}
			get{return _originationfundid;}
		}
		/// <summary>
		/// ���׻������ͱ�ʶ
		/// </summary>
        [DataMember]
		public int? TransactionCurrencyTypeID
		{
			set{ _transactioncurrencytypeid=value;}
			get{return _transactioncurrencytypeid;}
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

