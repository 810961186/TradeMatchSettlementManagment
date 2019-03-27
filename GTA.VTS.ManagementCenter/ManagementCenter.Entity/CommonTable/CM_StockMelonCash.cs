using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///��������Ʊ�ֺ��¼_�ֽ�� ʵ����CM_StockMelonCash ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class CM_StockMelonCash
	{
		public CM_StockMelonCash()
		{}
		#region Model
		private int _stockmeloncuttingcashid;
		private DateTime? _stockrightregisterdate;
		private DateTime? _stockrightlogoutdatumdate;
		private string _commoditycode;
		private double? _ratioofcashdividend;
		/// <summary>
		/// ��Ʊ�ֺ��¼_�ֽ��ʶ
		/// </summary>
        [DataMember]
		public int StockMelonCuttingCashID
		{
			set{ _stockmeloncuttingcashid=value;}
			get{return _stockmeloncuttingcashid;}
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
		/// ��Ʒ����
		/// </summary>
        [DataMember]
		public string CommodityCode
		{
			set{ _commoditycode=value;}
			get{return _commoditycode;}
		}
		/// <summary>
		/// �ֽ�ֺ����
		/// </summary>
        [DataMember]
        public double? RatioOfCashDividend
		{
			set{ _ratioofcashdividend=value;}
			get{return _ratioofcashdividend;}
		}
		#endregion Model

	}
}

