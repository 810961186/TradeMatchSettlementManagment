using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// �������������ͱ� ʵ����UM_QuestionType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
	/// </summary>
    [DataContract]
	public class UM_QuestionType
	{
		public UM_QuestionType()
		{}
		#region Model
		private int _questionid;
		private string _content;
		/// <summary>
		/// �����ʶ
		/// </summary>
        [DataMember]
		public int QuestionID
		{
			set{ _questionid=value;}
			get{return _questionid;}
		}
		/// <summary>
		/// ��������
		/// </summary>
        [DataMember]
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		#endregion Model

	}
}

