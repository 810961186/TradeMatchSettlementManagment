using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///���������׷���� ʵ����CM_TradeWay ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class CM_TradeWay
	{
		public CM_TradeWay()
		{}
		#region Model
		private int _tradewayid;
		private string _tradewayname;
		/// <summary>
		/// ���׷����ʶ
		/// </summary>
        [DataMember]
		public int TradeWayID
		{
			set{ _tradewayid=value;}
			get{return _tradewayid;}
		}
		/// <summary>
		/// ���׷�������
		/// </summary>
        [DataMember]
		public string TradeWayName
		{
			set{ _tradewayname=value;}
			get{return _tradewayname;}
		}
		#endregion Model

	}
}

