using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///����:����֮��һ����ͱ� ʵ����CM_CurrencyExchange ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class CM_CurrencyExchange
	{
		public CM_CurrencyExchange()
		{}
		#region Model
		private int _currencyexchangeid;
		private string _describe;
		/// <summary>
		/// ����֮��һ����ͱ�ʶ
		/// </summary>
        [DataMember]
		public int CurrencyExchangeID
		{
			set{ _currencyexchangeid=value;}
			get{return _currencyexchangeid;}
		}
		/// <summary>
		/// �һ���ʽ����
		/// </summary>
        [DataMember]
		public string Describe
		{
			set{ _describe=value;}
			get{return _describe;}
		}
		#endregion Model

	}
}

