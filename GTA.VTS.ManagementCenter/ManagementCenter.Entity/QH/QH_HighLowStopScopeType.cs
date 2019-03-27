using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�������ǵ�ͣ��������� ʵ����QH_HighLowStopScopeType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����: 2008-12-13
    /// </summary>
    [DataContract]
	public class QH_HighLowStopScopeType
	{
		public QH_HighLowStopScopeType()
		{}
		#region Model
		private int _highlowstopscopeid;
		private string _highlowstopscopename;
		/// <summary>
		/// �ǵ�ͣ��������ͱ�ʶ
		/// </summary>
        [DataMember]
		public int HighLowStopScopeID
		{
			set{ _highlowstopscopeid=value;}
			get{return _highlowstopscopeid;}
		}
		/// <summary>
		/// �ǵ�ͣ�������������
		/// </summary>
        [DataMember]
		public string HighLowStopScopeName
		{
			set{ _highlowstopscopename=value;}
			get{return _highlowstopscopename;}
		}
		#endregion Model

	}
}

