using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
    /// <summary>
    /// ��������Ʒ�ڻ�_�ֲ�ȡֵ���� ʵ����QH_PositionValueType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    [DataContract]
    public class QH_PositionValueType
    {
        public QH_PositionValueType()
        {
        }

        #region Model

        private int _positionvaluetypeid;
        private string _positionvaluename;

        /// <summary>
        /// ��Ʒ�ڻ�_�ֲ�ȡֵ��ʶ
        /// </summary>
        [DataMember]
        public int PositionValueTypeID
        {
            set { _positionvaluetypeid = value; }
            get { return _positionvaluetypeid; }
        }

        /// <summary>
        /// ȡֵ��������
        /// </summary>
        [DataMember]
        public string PositionValueName
        {
            set { _positionvaluename = value; }
            get { return _positionvaluename; }
        }

        #endregion Model
    }
}