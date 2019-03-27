using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�������·� ʵ����QH_Month ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����: 2008-12-13
    /// </summary>
    [DataContract]
	public class QH_Month
	{
		public QH_Month()
		{}
		#region Model
		private int _monthid;
		private string _monthname;
		/// <summary>
		/// �·ݱ�ʶ
		/// </summary>
        [DataMember]
		public int MonthID
		{
			set{ _monthid=value;}
			get{return _monthid;}
		}
		/// <summary>
		/// �·�����
		/// </summary>
        [DataMember]
		public string MonthName
		{
			set{ _monthname=value;}
			get{return _monthname;}
		}
		#endregion Model

	}
}

