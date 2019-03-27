using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
    /// <summary>
    /// ������Ȩ֤�ǵ����۸� ʵ����XH_RightHightLowPrices ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// ���ߣ�����ΰ
    /// ���ڣ�2008-11-26
    /// </summary>
    [DataContract]
    public class XH_RightHightLowPrices
    {
        public XH_RightHightLowPrices()
        {
        }

        #region Model

        private int _righthightlowpriceid;
        private decimal? _rightfrontdaycloseprice;
        private decimal? _stockfrontdaycloseprice;
        private decimal? _setscale;
        private int? _hightlowid;

        /// <summary>
        /// Ȩ֤�ǵ����۸��ʶ
        /// </summary>
        [DataMember]
        public int RightHightLowPriceID
        {
            set { _righthightlowpriceid = value; }
            get { return _righthightlowpriceid; }
        }

        /// <summary>
        /// Ȩ֤ǰһ�����̼۸�
        /// </summary>
        [DataMember]
        public decimal? RightFrontDayClosePrice
        {
            set { _rightfrontdaycloseprice = value; }
            get { return _rightfrontdaycloseprice; }
        }

        /// <summary>
        /// ���֤ȯǰ�����̼�
        /// </summary>
        [DataMember]
        public decimal? StockFrontDayClosePrice
        {
            set { _stockfrontdaycloseprice = value; }
            get { return _stockfrontdaycloseprice; }
        }

        /// <summary>
        /// ���ñ���
        /// </summary>
        [DataMember]
        public decimal? SetScale
        {
            set { _setscale = value; }
            get { return _setscale; }
        }

        /// <summary>
        /// �ǵ�����ʶ
        /// </summary>
        [DataMember]
        public int? HightLowID
        {
            set { _hightlowid = value; }
            get { return _hightlowid; }
        }

        #endregion Model
    }
}