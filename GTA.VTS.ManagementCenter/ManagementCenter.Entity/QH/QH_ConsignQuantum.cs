using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///���������׹���ί���� ʵ����QH_ConsignQuantum ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����: 2008-12-13
    /// </summary>
    [DataContract]
	public class QH_ConsignQuantum
	{
		public QH_ConsignQuantum()
		{}
		#region Model
		private int _consignquantumid;
		private int? _minconsignquantum;
		/// <summary>
		/// ���׹���ί������ʶ
		/// </summary>
        [DataMember]
		public int ConsignQuantumID
		{
			set{ _consignquantumid=value;}
			get{return _consignquantumid;}
		}
		/// <summary>
		/// ��Сί����
		/// </summary>
        [DataMember]
		public int? MinConsignQuantum
		{
			set{ _minconsignquantum=value;}
			get{return _minconsignquantum;}
		}
		#endregion Model

	}
}

