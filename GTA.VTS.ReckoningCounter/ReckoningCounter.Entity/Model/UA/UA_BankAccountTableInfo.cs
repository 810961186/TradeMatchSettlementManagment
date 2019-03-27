using System;
using System.Runtime.Serialization;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title: �����˻���Ϣʵ��
    /// Desc: �����˻���Ϣʵ����UA_BankAccountTable ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create BY�����
    /// Create Date��2009-10-15
	/// </summary>
    [DataContract]
	public class UA_BankAccountTableInfo
	{
        /// <summary>
        /// ���캯��
        /// </summary>
        public UA_BankAccountTableInfo()
		{}
		#region Model
		private decimal _capitalremainamount;
		private int _tradecurrencytypelogo;
		private string _useraccountdistributelogo;
		private decimal _balanceoftheday;
		private decimal _todayoutincapital;
		private decimal _freezecapital;
		private decimal _availablecapital;
		/// <summary>
		/// ʣ���ʽ��ܶ�
		/// </summary>
        [DataMember]
		public decimal CapitalRemainAmount
		{
			set{ _capitalremainamount=value;}
			get{return _capitalremainamount;}
		}
		/// <summary>
		/// ���׻�������(���BD_CurrencyType)
		/// </summary>
        [DataMember]
		public int TradeCurrencyTypeLogo
		{
			set{ _tradecurrencytypelogo=value;}
			get{return _tradecurrencytypelogo;}
		}
		/// <summary>
		/// �����˺�ID(���UA_UserAccountAllocationTable)
		/// </summary>
        [DataMember]
		public string UserAccountDistributeLogo
		{
			set{ _useraccountdistributelogo=value;}
			get{return _useraccountdistributelogo;}
		}
		/// <summary>
		/// ���ս��
		/// </summary>
        [DataMember]
		public decimal BalanceOfTheDay
		{
			set{ _balanceoftheday=value;}
			get{return _balanceoftheday;}
		}
		/// <summary>
		/// ���ճ�����
		/// </summary>
        [DataMember]
		public decimal TodayOutInCapital
		{
			set{ _todayoutincapital=value;}
			get{return _todayoutincapital;}
		}
		/// <summary>
		/// �����ܶ�
		/// </summary>
        [DataMember]
		public decimal FreezeCapital
		{
			set{ _freezecapital=value;}
			get{return _freezecapital;}
		}
		/// <summary>
		/// �����ʽ�
		/// </summary>
        [DataMember]
		public decimal AvailableCapital
		{
			set{ _availablecapital=value;}
			get{return _availablecapital;}
		}
		#endregion Model

	}
}

