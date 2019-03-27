using System;
namespace ReckoningCounter.Model
{
    /// <summary>
    /// Title:�˺����ʵ����
    /// Desc.:�˺����ʵ����DB_AccountTypeClass ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// Create by:���
    /// Create date:2009-07-08
    /// </summary>
    [Serializable]
    public class DB_AccountTypeClassInfo
    {
        /// <summary>
        /// �˺����ʵ���๹�캯�� 
        /// </summary>
        public DB_AccountTypeClassInfo()
        { }
        #region Model
        private int _atcid;
        private string _atcname;
        /// <summary>
        /// �˻����ͷ���ID
        /// </summary>
        public int ATCId
        {
            set { _atcid = value; }
            get { return _atcid; }
        }
        /// <summary>
        /// �˻����ͷ�������
        /// </summary>
        public string ATCName
        {
            set { _atcname = value; }
            get { return _atcname; }
        }
        #endregion Model

    }
}

