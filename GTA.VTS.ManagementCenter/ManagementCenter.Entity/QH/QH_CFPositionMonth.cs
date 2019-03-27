using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�������ڻ�_Ʒ��_�����·� ʵ����QH_CFPositionMonth ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����: 2008-12-13
    /// </summary>
    [DataContract]
	public class QH_CFPositionMonth
	{
		public QH_CFPositionMonth()
		{}
		#region Model
		private int _deliverymonthtypeid;
		private string _deliverymonthname;
		/// <summary>
		/// �����·����ͱ�ʶ
		/// </summary>
        [DataMember]
		public int DeliveryMonthTypeID
		{
			set{ _deliverymonthtypeid=value;}
			get{return _deliverymonthtypeid;}
		}
		/// <summary>
		/// �����·�����
		/// </summary>
        [DataMember]
		public string DeliveryMonthName
		{
			set{ _deliverymonthname=value;}
			get{return _deliverymonthname;}
		}
		#endregion Model

	}
}

