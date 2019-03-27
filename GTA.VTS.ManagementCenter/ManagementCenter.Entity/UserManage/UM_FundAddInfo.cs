using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    /// �������ʽ���ϸ�� ʵ����UM_FundAddInfo ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
    [DataContract]
	public class UM_FundAddInfo
	{
		public UM_FundAddInfo()
		{}
        #region Model
        private int _addfundid;
        private int? _managerid;
        private int? _userid;
        private decimal? _usnumber;
        private decimal? _rmbnumber;
        private DateTime? _addtime;
        private decimal? _hknumber;
        private string _remark;
        /// <summary>
        /// ׷���ʽ���ϸ��ʶ
        /// </summary>
        [DataMember]
        public int AddFundID
        {
            set { _addfundid = value; }
            get { return _addfundid; }
        }
        /// <summary>
        /// ����Ա��ʶ
        /// </summary>
        [DataMember]
        public int? ManagerID
        {
            set { _managerid = value; }
            get { return _managerid; }
        }
        /// <summary>
        /// ����Ա��ʶ
        /// </summary>
        [DataMember]
        public int? UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// ��Ԫ
        /// </summary>
        [DataMember]
        public decimal? USNumber
        {
            set { _usnumber = value; }
            get { return _usnumber; }
        }
        /// <summary>
        /// �����
        /// </summary>
        [DataMember]
        public decimal? RMBNumber
        {
            set { _rmbnumber = value; }
            get { return _rmbnumber; }
        }
        /// <summary>
        /// ׷��ʱ��
        /// </summary>
        [DataMember]
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// ��Ԫ
        /// </summary>
        [DataMember]
        public decimal? HKNumber
        {
            set { _hknumber = value; }
            get { return _hknumber; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        [DataMember]
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

	}
}

