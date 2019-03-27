using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///����: ʵ����ZFInfo ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�����Ա��������
    /// ���ڣ�2008-11-18
    /// </summary>
    [DataContract]
	public class ZFInfo
	{
		public ZFInfo()
		{}
		#region Model
		private string _stkcd;
		private double? _roprc;
        private double? _zfgs;
		private string _paydt;
		/// <summary>
		/// ��Ʊ����
		/// </summary>
        [DataMember]
		public string stkcd
		{
			set{ _stkcd=value;}
			get{return _stkcd;}
		}
		/// <summary>
		/// ��������
		/// </summary>
        [DataMember]
        public double? roprc
		{
			set{ _roprc=value;}
			get{return _roprc;}
		}
		/// <summary>
		/// ��������
		/// </summary>
        [DataMember]
        public double? zfgs
		{
			set{ _zfgs=value;}
			get{return _zfgs;}
		}
		/// <summary>
		/// ��������
		/// </summary>
        [DataMember]
		public string paydt
		{
			set{ _paydt=value;}
			get{return _paydt;}
		}
		#endregion Model

	}
}

