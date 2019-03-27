using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///���������ʱ� ʵ����CM_ExchangeRatio ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class CM_ExchangeRatio
	{
		public CM_ExchangeRatio()
		{}
		#region Model
		private decimal? _ratio;
		private string _remarks;
		private int _currencyexchangeid;
		/// <summary>
		/// ����
		/// </summary>
		[DataMember]
		public decimal? Ratio
		{
			set{ _ratio=value;}
			get{return _ratio;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
        [DataMember]
		public string Remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		/// <summary>
		/// ����֮��һ����ͱ�ʶ
		/// </summary>
        [DataMember]
		public int CurrencyExchangeID
		{
			set{ _currencyexchangeid=value;}
			get{return _currencyexchangeid;}
		}
		#endregion Model

	}
}

