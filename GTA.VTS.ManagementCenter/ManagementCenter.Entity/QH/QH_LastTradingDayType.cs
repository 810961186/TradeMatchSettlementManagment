using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�����������������  ʵ����QH_LastTradingDayType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����: 2008-12-13
    /// </summary>
    [DataContract]
	public class QH_LastTradingDayType
	{
		public QH_LastTradingDayType()
		{}
		#region Model
		private int _lasttradingdaytypeid;
		private string _typename;
		/// <summary>
		/// ����������ͱ�ʶ
		/// </summary>
        [DataMember]
		public int LastTradingDayTypeID
		{
			set{ _lasttradingdaytypeid=value;}
			get{return _lasttradingdaytypeid;}
		}
		/// <summary>
		/// ������
		/// </summary>
        [DataMember]
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		#endregion Model

	}
}

