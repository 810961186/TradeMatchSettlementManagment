using System;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title:�û���ɫʵ����
    /// Desc.:�û���ɫʵ����BD_UserRoleType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create by:���
    /// Create date:2009-07-08
    /// </summary>
    [Serializable]
    public class BD_UserRoleTypeInfo
    {
        /// <summary>
        /// �û���ɫʵ���๹�캯�� 
        /// </summary>
        public BD_UserRoleTypeInfo()
        { }
        #region Model
        private int _rolenumber;
        private string _rolename;
        private string _remarks;
        /// <summary>
        /// ��ɫID��������
        /// </summary>
        public int RoleNumber
        {
            set { _rolenumber = value; }
            get { return _rolenumber; }
        }
        /// <summary>
        /// ��ɫ����
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// ��ɫ���Ʊ�ע
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        #endregion Model

    }
}

