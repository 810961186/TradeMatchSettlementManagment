using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�������ֲֺͱ�֤��������� ʵ����QH_PositionBailType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    [DataContract]
	public class QH_PositionBailType
	{
		public QH_PositionBailType()
		{}
		#region Model
		private int _positionbailtypeid;
		private string _positionbailtypename;
		/// <summary>
		/// �ֲֺͱ�֤��������ͱ�ʶ
		/// </summary>
        [DataMember]
		public int PositionBailTypeID
		{
			set{ _positionbailtypeid=value;}
			get{return _positionbailtypeid;}
		}
		/// <summary>
		/// �ֲֺͱ�֤����������
		/// </summary>
        [DataMember]
		public string PositionBailTypeName
		{
			set{ _positionbailtypename=value;}
			get{return _positionbailtypename;}
		}
		#endregion Model

	}
}

