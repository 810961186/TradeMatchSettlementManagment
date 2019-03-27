using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
    /// <summary>
    ///��������Ʒ�ڻ�_��֤����� ʵ����QH_CFBailScaleValue ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    [DataContract]
    public class QH_CFBailScaleValue
    {
        public QH_CFBailScaleValue()
        { }
        #region Model
        private int _cfbailscalevalueid;
        private int? _start;
        private decimal? _bailscale;
        private int? _ends;
        private int? _breedclassid;
        private int? _deliverymonthtype;
        private int? _upperlimitifequation;
        private int? _lowerlimitifequation;
        private int? _positionbailtypeid;
        /// <summary>
        /// ��Ʒ�ڻ�-��֤�������ʶ
        /// </summary>
        [DataMember]
        public int CFBailScaleValueID
        {
            set { _cfbailscalevalueid = value; }
            get { return _cfbailscalevalueid; }
        }
        /// <summary>
        /// ��ʼ
        /// </summary>
        [DataMember]
        public int? Start
        {
            set { _start = value; }
            get { return _start; }
        }
        /// <summary>
        /// ��֤�����
        /// </summary>
        [DataMember]
        public decimal? BailScale
        {
            set { _bailscale = value; }
            get { return _bailscale; }
        }
        /// <summary>
        /// ����
        /// </summary>
        [DataMember]
        public int? Ends
        {
            set { _ends = value; }
            get { return _ends; }
        }
        /// <summary>
        /// Ʒ�ֱ�ʶ
        /// </summary>
        [DataMember]
        public int? BreedClassID
        {
            set { _breedclassid = value; }
            get { return _breedclassid; }
        }
        /// <summary>
        /// �����·����ͱ�ʶ
        /// </summary>
        [DataMember]
        public int? DeliveryMonthType
        {
            set { _deliverymonthtype = value; }
            get { return _deliverymonthtype; }
        }
        /// <summary>
        /// �����Ƿ�����
        /// </summary>
        [DataMember]
        public int? UpperLimitIfEquation
        {
            set { _upperlimitifequation = value; }
            get { return _upperlimitifequation; }
        }
        /// <summary>
        /// �����Ƿ�����
        /// </summary>
        [DataMember]
        public int? LowerLimitIfEquation
        {
            set { _lowerlimitifequation = value; }
            get { return _lowerlimitifequation; }
        }
        /// <summary>
        /// �ֲֺͱ�֤��������ͱ�ʶ
        /// </summary>
        [DataMember]
        public int? PositionBailTypeID
        {
            set { _positionbailtypeid = value; }
            get { return _positionbailtypeid; }
        }

        /// <summary>
        /// Title:������֤���¼ID
        /// Desc: ���ڼ�¼�����ļ�¼ID���ü�¼Ϊ�������ǰn����ı�֤�������
        /// Create by: ����
        /// Create Date:2010-02-01
        /// </summary>
        [DataMember]
        public int? RelationScaleID
        {
            get;
            set;
        }

        #endregion Model

    }
}
