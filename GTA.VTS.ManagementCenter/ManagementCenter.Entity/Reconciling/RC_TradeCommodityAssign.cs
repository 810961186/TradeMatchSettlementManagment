using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
	/// ��������ϻ��������� ʵ����RC_TradeCommodityAssign ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ������� �޸ģ�����ΰ
    /// ���ڣ�2008-11-18 �޸����ڣ�2009-10-22
    /// </summary>
    [DataContract]
	public class RC_TradeCommodityAssign
	{
		public RC_TradeCommodityAssign()
		{}
		#region Model
		private string _commoditycode;
		private int _matchmachineid;
        private int  codeformsource;

		/// <summary>
		/// ��Ʒ����
		/// </summary>
        [DataMember]
		public string CommodityCode
		{
			set{ _commoditycode=value;}
			get{return _commoditycode;}
		}
		/// <summary>
		/// ��ϻ����
		/// </summary>
        [DataMember]
		public int MatchMachineID
		{
			set{ _matchmachineid=value;}
			get{return _matchmachineid;}
        }

        #region 2009.10.22 �����ֶ�

        #endregion
        /// <summary>
        /// ������Դ�Ǹ���
        /// </summary>
        [DataMember]
        public int CodeFormSource
        {
            set { codeformsource = value; }
            get { return codeformsource; }
        }
        #endregion Model

    }
}

