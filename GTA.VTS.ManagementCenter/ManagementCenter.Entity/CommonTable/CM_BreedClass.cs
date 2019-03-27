using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
    /// <summary>
    ///������Ʒ�ֱ�ʵ����CM_BreedClass ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
    public class CM_BreedClass
    {
        public CM_BreedClass()
        { }
        #region Model
        private int _breedclassid;
        private string _breedclassname;
        private int? _accounttypeidfund;
        private int? _breedclasstypeid;
        private int? _accounttypeidhold;
        private int? _boursetypeid;
        private int? _issysdefaultbreed;
        private int? _ishkbreedclasstype;
        private int? _deletestate;
        /// <summary>
        /// Ʒ��ID
        /// </summary>
        [DataMember]
        public int BreedClassID
        {
            set { _breedclassid = value; }
            get { return _breedclassid; }
        }
        /// <summary>
        /// Ʒ������
        /// </summary>
        [DataMember]
        public string BreedClassName
        {
            set { _breedclassname = value; }
            get { return _breedclassname; }
        }
        /// <summary>
        /// �ʽ��ʺ�����
        /// </summary>
        [DataMember]
        public int? AccountTypeIDFund
        {
            set { _accounttypeidfund = value; }
            get { return _accounttypeidfund; }
        }
        /// <summary>
        /// Ʒ������ID
        /// </summary>
        [DataMember]
        public int? BreedClassTypeID
        {
            set { _breedclasstypeid = value; }
            get { return _breedclasstypeid; }
        }
        /// <summary>
        /// �ֲ��ʺ�����
        /// </summary>
        [DataMember]
        public int? AccountTypeIDHold
        {
            set { _accounttypeidhold = value; }
            get { return _accounttypeidhold; }
        }
        /// <summary>
        /// ������ID
        /// </summary>
        [DataMember]
        public int? BourseTypeID
        {
            set { _boursetypeid = value; }
            get { return _boursetypeid; }
        }

        #region 2009.10.22 �����ֶ�
        /// <summary>
        /// �Ƿ���ϵͳĬ��Ʒ��
        /// </summary>
        [DataMember]
        public int? ISSysDefaultBreed
        {
            set { _issysdefaultbreed = value; }
            get { return _issysdefaultbreed; }
        }

        /// <summary>
        /// �Ƿ�۹�Ʒ������
        /// </summary>
        [DataMember]
        public int? ISHKBreedClassType
        {
            set { _ishkbreedclasstype = value; }
            get { return _ishkbreedclasstype; }
        }

        /// <summary>
        /// ɾ��״̬
        /// </summary>
        [DataMember]
        public int? DeleteState
        {
            set { _deletestate = value; }
            get { return _deletestate; }
        }
        #endregion

        #endregion Model

    }
}

