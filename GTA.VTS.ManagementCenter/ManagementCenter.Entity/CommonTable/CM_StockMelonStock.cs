using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///��������Ʊ�ֺ��¼_��Ʊ�� ʵ����CM_StockMelonStock ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class CM_StockMelonStock
	{
		public CM_StockMelonStock()
		{}
		#region Model
		private int _stockmelonstockid;
		private DateTime? _stockrightregisterdate;
		private DateTime? _stockrightlogoutdatumdate;
        private double? _sentstockratio;
		private string _commoditycode;
		/// <summary>
		/// ��Ʊ�ֺ��¼_��Ʊ��ʶ
		/// </summary>
        [DataMember]
		public int StockMelonStockID
		{
			set{ _stockmelonstockid=value;}
			get{return _stockmelonstockid;}
		}
		/// <summary>
		/// ��Ȩ�Ǽ���
		/// </summary>
        [DataMember]
		public DateTime? StockRightRegisterDate
		{
			set{ _stockrightregisterdate=value;}
			get{return _stockrightregisterdate;}
		}
		/// <summary>
		/// ��Ȩ��׼��
		/// </summary>
        [DataMember]
		public DateTime? StockRightLogoutDatumDate
		{
			set{ _stockrightlogoutdatumdate=value;}
			get{return _stockrightlogoutdatumdate;}
		}
		/// <summary>
		/// �͹ɱ���
		/// </summary>
        [DataMember]
        public double? SentStockRatio
		{
			set{ _sentstockratio=value;}
			get{return _sentstockratio;}
		}
		/// <summary>
		/// ��Ʒ����
		/// </summary>
        [DataMember]
		public string CommodityCode
		{
			set{ _commoditycode=value;}
			get{return _commoditycode;}
		}
		#endregion Model

	}
}

