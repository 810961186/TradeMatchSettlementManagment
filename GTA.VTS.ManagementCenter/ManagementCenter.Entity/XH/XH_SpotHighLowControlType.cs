using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///�������ֻ�_Ʒ��_�ǵ���_�������� ʵ����XH_SpotHighLowControlType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class XH_SpotHighLowControlType
	{
        public XH_SpotHighLowControlType()
        { }
        #region Model
        private int _highlowtypeid;
        private int _breedclasshighlowid;
        /// <summary>
        /// �ǵ������ͱ�ʶ
        /// </summary>
        [DataMember]
        public int HighLowTypeID
        {
            set { _highlowtypeid = value; }
            get { return _highlowtypeid; }
        }
        /// <summary>
        /// Ʒ���ǵ�����ʶ
        /// </summary>
        [DataMember]
        public int BreedClassHighLowID
        {
            set { _breedclasshighlowid = value; }
            get { return _breedclasshighlowid; }
        }
        #endregion Model

	}
}

