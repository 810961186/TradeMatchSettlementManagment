using System;
namespace ReckoningCounter.Model
{
	/// <summary>
    /// Title:���׻�������
    /// Desc.:���׻�������ʵ��(�۱ң�����ҡ��ȣ�BD_CurrencyType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create by:���
    /// Create date:2009-07-08
	/// </summary>
	[Serializable]
	public class BD_CurrencyTypeInfo
	{
        /// <summary>
        /// ���׻������͹��캯�� 
        /// </summary>
        public BD_CurrencyTypeInfo()
		{}
		#region Model
		private int _currencytypeid;
		private string _currencyname;
		/// <summary>
		/// ��������ID
		/// </summary>
		public int CurrencyTypeId
		{
			set{ _currencytypeid=value;}
			get{return _currencytypeid;}
		}
		/// <summary>
		/// ������������
		/// </summary>
		public string CurrencyName
		{
			set{ _currencyname=value;}
			get{return _currencyname;}
		}
		#endregion Model

	}
}

