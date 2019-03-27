using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using ManagementCenter.Model;
using ManagementCenter.Model.CommonClass;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Maticsoft.DBUtility;//�����������
namespace ManagementCenter.DAL
{
    /// <summary>
    /// �������û�������Ϣ�� ���ݷ�����UM_UserInfoDAL��
    /// ���ߣ�������        
    /// ���ڣ�2008-11-18         
    /// �޸ģ�Ҷ��
    /// �޸����ڣ�2009-12-23
    /// </summary>
    public class UM_UserInfoDAL
    {
        public UM_UserInfoDAL()
        { }
        //=================================================������Ա����==========================================

        #region �õ����ID
        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(UserID)+1 from UM_UserInfo";
            Database db = DatabaseFactory.CreateDatabase();
            object obj = db.ExecuteScalar(CommandType.Text, strsql);
            if (obj != null && obj != DBNull.Value)
            {
                return int.Parse(obj.ToString());
            }
            return 1;
        }
        #endregion

        #region �Ƿ���ڸü�¼
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int UserID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UM_UserInfo where UserID=@UserID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
            int cmdresult;
            object obj = db.ExecuteScalar(dbCommand);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region ����һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.UM_UserInfo model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UM_UserInfo(");
            strSql.Append("UserName,LoginName,Password,CertificateStyle,Postalcode,RoleID,CertificateNo,Telephone,Address,Email,QuestionID,Answer,CouterID,Remark,AddType,AddTime)");

            strSql.Append(" values (");
            strSql.Append("@UserName,@LoginName,@Password,@CertificateStyle,@Postalcode,@RoleID,@CertificateNo,@Telephone,@Address,@Email,@QuestionID,@Answer,@CouterID,@Remark,@AddType,@AddTime)");
            strSql.Append(";select @@IDENTITY");

            if (db == null) db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserName", DbType.String, model.UserName);
            db.AddInParameter(dbCommand, "LoginName", DbType.String, model.LoginName);
            db.AddInParameter(dbCommand, "Password", DbType.String, UtilityClass.DesEncrypt(model.Password, string.Empty));
            db.AddInParameter(dbCommand, "CertificateStyle", DbType.Int32, model.CertificateStyle);
            db.AddInParameter(dbCommand, "Postalcode", DbType.String, model.Postalcode);
            db.AddInParameter(dbCommand, "RoleID", DbType.Int32, model.RoleID);
            db.AddInParameter(dbCommand, "CertificateNo", DbType.String, model.CertificateNo);
            db.AddInParameter(dbCommand, "Telephone", DbType.String, model.Telephone);
            db.AddInParameter(dbCommand, "Address", DbType.String, model.Address);
            db.AddInParameter(dbCommand, "Email", DbType.String, model.Email);
            db.AddInParameter(dbCommand, "QuestionID", DbType.Int32, model.QuestionID);
            db.AddInParameter(dbCommand, "Answer", DbType.String, model.Answer);
            db.AddInParameter(dbCommand, "CouterID", DbType.Int32, model.CouterID);
            db.AddInParameter(dbCommand, "Remark", DbType.String, model.Remark);
            db.AddInParameter(dbCommand, "AddType", DbType.Int32, model.AddType);
            db.AddInParameter(dbCommand, "AddTime", DbType.DateTime, System.DateTime.Now);
            int result;
            object obj;
            if (tran == null)
            {
                obj = db.ExecuteScalar(dbCommand);
            }
            else
            {
                obj = db.ExecuteScalar(dbCommand, tran);
            }
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }
        /// <summary>
        /// ����û���Ϣ
        /// </summary>
        /// <param name="model">�û���Ϣʵ��</param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.UM_UserInfo model)
        {
            return Add(model, null, null);
        }
        #endregion

        #region ����һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.UM_UserInfo model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UM_UserInfo set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("LoginName=@LoginName,");
            strSql.Append("Password=@Password,");
            strSql.Append("CertificateStyle=@CertificateStyle,");
            strSql.Append("Postalcode=@Postalcode,");
            strSql.Append("RoleID=@RoleID,");
            strSql.Append("CertificateNo=@CertificateNo,");
            strSql.Append("Telephone=@Telephone,");
            strSql.Append("Address=@Address,");
            strSql.Append("Email=@Email,");
            strSql.Append("QuestionID=@QuestionID,");
            strSql.Append("Answer=@Answer,");
            strSql.Append("CouterID=@CouterID,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("AddType=@AddType,");
            strSql.Append("AddTime=@AddTime");
            strSql.Append(" where UserID=@UserID ");

            if (db == null) db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserName", DbType.String, model.UserName);
            db.AddInParameter(dbCommand, "LoginName", DbType.String, model.LoginName);
            db.AddInParameter(dbCommand, "Password", DbType.String, model.Password);
            db.AddInParameter(dbCommand, "UserID", DbType.Int32, model.UserID);
            db.AddInParameter(dbCommand, "CertificateStyle", DbType.Int32, model.CertificateStyle);
            db.AddInParameter(dbCommand, "Postalcode", DbType.String, model.Postalcode);
            db.AddInParameter(dbCommand, "RoleID", DbType.Int32, model.RoleID);
            db.AddInParameter(dbCommand, "CertificateNo", DbType.String, model.CertificateNo);
            db.AddInParameter(dbCommand, "Telephone", DbType.String, model.Telephone);
            db.AddInParameter(dbCommand, "Address", DbType.String, model.Address);
            db.AddInParameter(dbCommand, "Email", DbType.String, model.Email);
            db.AddInParameter(dbCommand, "QuestionID", DbType.Int32, model.QuestionID);
            db.AddInParameter(dbCommand, "Answer", DbType.String, model.Answer);
            db.AddInParameter(dbCommand, "CouterID", DbType.Int32, model.CouterID);
            db.AddInParameter(dbCommand, "Remark", DbType.String, model.Remark);
            db.AddInParameter(dbCommand, "AddType", DbType.Int32, model.AddType);
            db.AddInParameter(dbCommand, "AddTime", DbType.DateTime, model.AddTime);
            if (tran == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
        }

        /// <summary>
        /// �����û���Ϣ
        /// </summary>
        /// <param name="model">�û���Ϣʵ��</param>
        public void Update(ManagementCenter.Model.UM_UserInfo model)
        {
            Update(model, null, null);
        }

        /// <summary>
        /// �����û�ID�����û���Ϣ
        /// </summary>
        /// <param name="UserID">�û�ID</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        public void UpdateDelState(int UserID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UM_UserInfo set ");
            strSql.Append("AddType=@AddType ");
            strSql.Append(" where UserID=@UserID AND RoleID=@RoleID AND AddType=@OldType");

            if (db == null) db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AddType", DbType.Int32, (int)ManagementCenter.Model.CommonClass.Types.AddTpyeEnum.FrontTarnDelState);
            db.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
            db.AddInParameter(dbCommand, "RoleID", DbType.Int32, (int)ManagementCenter.Model.CommonClass.Types.RoleTypeEnum.Transaction);
            db.AddInParameter(dbCommand, "OldType", DbType.Int32, (int)ManagementCenter.Model.CommonClass.Types.AddTpyeEnum.FrontTaransaction);

            if (tran == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
        }
        #endregion

        #region ɾ��һ������
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int UserID, DbTransaction tran, Database db)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete UM_UserInfo ");
            strSql.Append(" where UserID=@UserID ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
            if (tran == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
        }

        /// <summary>
        /// �����û�IDɾ���û���Ϣ
        /// </summary>
        /// <param name="UserID">�û�ID</param>
        public void Delete(int UserID)
        {
            Delete(UserID, null, null);
        }
        #endregion

        #region �õ�һ������ʵ��
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.UM_UserInfo GetModel(int UserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserName,LoginName,Password,UserID,CertificateStyle,Postalcode,RoleID,CertificateNo,Telephone,Address,Email,QuestionID,Answer,CouterID,Remark,AddType,AddTime from UM_UserInfo ");
            strSql.Append(" where UserID=@UserID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
            ManagementCenter.Model.UM_UserInfo model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }
        #endregion

        #region
        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserName,LoginName,Password,UserID,CertificateStyle,Postalcode,RoleID,CertificateNo,Telephone,Address,Email,QuestionID,Answer,CouterID,Remark,AddType,AddTime ");
            strSql.Append(" FROM UM_UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        /*
        /// <summary>
        /// ��ҳ��ȡ�����б�
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("UP_GetRecordByPage");
            db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "UM_UserInfo");
            db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "ID");
            db.AddInParameter(dbCommand, "PageSize", DbType.Int32, PageSize);
            db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
            db.AddInParameter(dbCommand, "IsReCount", DbType.Boolean, 0);
            db.AddInParameter(dbCommand, "OrderType", DbType.Boolean, 0);
            db.AddInParameter(dbCommand, "strWhere", DbType.AnsiString, strWhere);
            return db.ExecuteDataSet(dbCommand);
        }*/
        #endregion

        #region ��������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�

        /// <summary>
        /// ��������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        public List<ManagementCenter.Model.UM_UserInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserName,LoginName,Password,UserID,CertificateStyle,Postalcode,RoleID,CertificateNo,Telephone,Address,Email,QuestionID,Answer,CouterID,Remark,AddType,AddTime ");
            strSql.Append(" FROM UM_UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.UM_UserInfo> list = new List<ManagementCenter.Model.UM_UserInfo>();
            Database db = DatabaseFactory.CreateDatabase();
            using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, strSql.ToString()))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderBind(dataReader));
                }
            }
            return list;
        }
        #endregion

        #region ����ʵ�������
        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.UM_UserInfo ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.UM_UserInfo model = new ManagementCenter.Model.UM_UserInfo();
            object ojb;
            model.UserName = dataReader["UserName"].ToString();
            model.LoginName = dataReader["LoginName"].ToString();
            model.Password = dataReader["Password"].ToString();
            ojb = dataReader["UserID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UserID = (int)ojb;
            }
            ojb = dataReader["CertificateStyle"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CertificateStyle = (int)ojb;
            }
            model.Postalcode = dataReader["Postalcode"].ToString();
            ojb = dataReader["RoleID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RoleID = (int)ojb;
            }
            model.CertificateNo = dataReader["CertificateNo"].ToString();
            model.Telephone = dataReader["Telephone"].ToString();
            model.Address = dataReader["Address"].ToString();
            model.Email = dataReader["Email"].ToString();
            ojb = dataReader["QuestionID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.QuestionID = (int)ojb;
            }
            model.Answer = dataReader["Answer"].ToString();
            ojb = dataReader["CouterID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CouterID = (int)ojb;
            }
            ojb = dataReader["AddTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AddTime = (DateTime)ojb;
            }
            ojb = dataReader["AddType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AddType = (int)ojb;
            }
            model.Remark = dataReader["Remark"].ToString();
            return model;
        }
        #endregion

        //=================================================����Ա==========================================

        #region  ����Ա��ҳ��ѯ

        /// <summary>
        /// ��ҳ��ѯ�û�
        /// </summary>
        /// <param name="userInfo">��ѯ���� �û�ʵ��</param>
        /// <param name="pageNo">ҳ��</param>
        /// <param name="pageSize">ÿҳ��¼��</param>
        /// <param name="rowCount">��ҳ��</param>
        /// <returns></returns>
        public DataSet GetPagingUser(Model.UM_UserInfo userInfo, int pageNo, int pageSize, out int rowCount)
        {
            // string SQL_SELECT_CUSTOMER =
            //@"select UserName,LoginName,Password,UserID,CertificateStyle,Postalcode,RoleID,CertificateNo,Telephone,Address,Email,QuestionID,Answer,CouterID,Remark,AddType,AddTime  FROM UM_UserInfo where 1=1 ";
            string SQL_SELECT_CUSTOMER =
            @"select a.*,b.name from UM_UserInfo a,CT_Counter b where b.CouterID=a.CouterID ";
            if (userInfo.LoginName != null && !string.IsNullOrEmpty(userInfo.LoginName))
            {
                SQL_SELECT_CUSTOMER += "AND LoginName LIKE  '%' + @LoginName + '%' ";
            }
            if (userInfo.UserName != null && !string.IsNullOrEmpty(userInfo.UserName))
            {
                SQL_SELECT_CUSTOMER += "AND UserName LIKE  '%' + @UserName + '%' ";
            }
            //if (userInfo.CouterID != int.MaxValue)
            //{
            //    SQL_SELECT_CUSTOMER += "AND CouterID=@CouterID ";
            //}
            if (userInfo.UserID != int.MaxValue)
            {
                SQL_SELECT_CUSTOMER += "AND UserID=@UserID ";
            }
            if (userInfo.RoleID != int.MaxValue)
            {
                SQL_SELECT_CUSTOMER += "AND RoleID=@RoleID ";
            }
            if (userInfo.Name != null && !string.IsNullOrEmpty(userInfo.Name)) //��̨����
            {
                SQL_SELECT_CUSTOMER += "AND name LIKE  '%' + @name + '%' ";
            }
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_CUSTOMER);


            if (userInfo.LoginName != null && !string.IsNullOrEmpty(userInfo.LoginName))
            {
                database.AddInParameter(dbCommand, "LoginName", DbType.String, userInfo.LoginName);
            }
            if (userInfo.UserName != null && !string.IsNullOrEmpty(userInfo.UserName))
            {
                database.AddInParameter(dbCommand, "UserName", DbType.String, userInfo.UserName);
            }
            //if (userInfo.CouterID != int.MaxValue)
            //{
            //    database.AddInParameter(dbCommand, "CouterID", DbType.Int32, userInfo.CouterID);
            //}
            if (userInfo.UserID != int.MaxValue)
            {
                database.AddInParameter(dbCommand, "UserID", DbType.Int32, userInfo.UserID);
            }
            if (userInfo.RoleID != int.MaxValue)
            {
                database.AddInParameter(dbCommand, "RoleID", DbType.Int32, userInfo.RoleID);
            }
            if (userInfo.Name != null && !string.IsNullOrEmpty(userInfo.Name))  //��̨����
            {
                database.AddInParameter(dbCommand, "name", DbType.String, userInfo.Name);
            }
            return CommPager.QueryPager(database, dbCommand, SQL_SELECT_CUSTOMER, pageNo, pageSize, out rowCount, "TSM_CUSTOMER");

        }
        #endregion

        #region ����Ա��½��֤

        /// <summary>
        /// ��½��֤
        /// </summary>
        public ManagementCenter.Model.UM_UserInfo TranLogin(int UserID, string Password)
        {
            string SQL_SELECT_Login =
           @"select * FROM UM_UserInfo where UserID=@UserID AND Password=@Password AND RoleID=@RoleID ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_SELECT_Login);
            db.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
            db.AddInParameter(dbCommand, "Password", DbType.String, Password);
            db.AddInParameter(dbCommand, "RoleID", DbType.Int32, (int)Types.RoleTypeEnum.Transaction);
            ManagementCenter.Model.UM_UserInfo model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }

        #endregion

        //=================================================����Ա========================================== 

        #region ����Ա��ҳ��ѯ
        /// <summary>
        /// ��ҳ��ѯ�û�
        /// </summary>
        /// <param name="managerQueryEntity">��ѯ���� �����û�ʵ��</param>
        /// <param name="pageNo">ҳ��</param>
        /// <param name="pageSize">ÿҳ��¼��</param>
        /// <param name="rowCount">��ҳ��</param>
        /// <returns></returns>
        public DataSet GetPagingManager(ManagementCenter.Model.UserManage.ManagerQueryEntity managerQueryEntity, int pageNo, int pageSize, out int rowCount)
        {

            string SQL_SELECT_Manager = @"select UM_UserInfo.*,UM_ManagerBeloneToGroup.ManagerGroupID,UM_ManagerGroup.ManagerGroupName
                                                                        From UM_UserInfo,UM_ManagerBeloneToGroup,UM_ManagerGroup
                                                                        Where UM_UserInfo.UserID=UM_ManagerBeloneToGroup.UserID 
                                                                        AND UM_ManagerBeloneToGroup.ManagerGroupID=UM_ManagerGroup.ManagerGroupID  ";

            if (managerQueryEntity.LoginName != null && !string.IsNullOrEmpty(managerQueryEntity.LoginName))
            {
                SQL_SELECT_Manager += "AND LoginName LIKE  '%' + @LoginName + '%' ";
            }
            if (managerQueryEntity.UserName != null && !string.IsNullOrEmpty(managerQueryEntity.UserName))
            {
                SQL_SELECT_Manager += "AND UserName LIKE  '%' + @UserName + '%' ";
            }
            if (managerQueryEntity.UserID != int.MaxValue)
            {
                SQL_SELECT_Manager += "AND UM_UserInfo.UserID=@UserID ";
            }
            if (managerQueryEntity.RoleID != int.MaxValue)
            {
                SQL_SELECT_Manager += "AND RoleID=@RoleID ";
            }
            if (managerQueryEntity.ManagerGroupID != int.MaxValue)
            {
                SQL_SELECT_Manager += "AND ManagerGroupID=@ManagerGroupID ";
            }
            if (managerQueryEntity.ManagerGroupName != null && !string.IsNullOrEmpty(managerQueryEntity.ManagerGroupName))
            {
                SQL_SELECT_Manager += "AND ManagerGroupName LIKE  '%' + @ManagerGroupName + '%' ";
            }
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_Manager);


            if (managerQueryEntity.LoginName != null && !string.IsNullOrEmpty(managerQueryEntity.LoginName))
            {
                database.AddInParameter(dbCommand, "LoginName", DbType.String, managerQueryEntity.LoginName);
            }
            if (managerQueryEntity.UserName != null && !string.IsNullOrEmpty(managerQueryEntity.UserName))
            {
                database.AddInParameter(dbCommand, "UserName", DbType.String, managerQueryEntity.UserName);
            }
            if (managerQueryEntity.UserID != int.MaxValue)
            {
                database.AddInParameter(dbCommand, "UserID", DbType.Int32, managerQueryEntity.UserID);
            }
            if (managerQueryEntity.RoleID != int.MaxValue)
            {
                database.AddInParameter(dbCommand, "RoleID", DbType.Int32, managerQueryEntity.RoleID);
            }
            if (managerQueryEntity.ManagerGroupID != int.MaxValue)
            {
                database.AddInParameter(dbCommand, "ManagerGroupID", DbType.Int32, managerQueryEntity.ManagerGroupID);
            }
            if (managerQueryEntity.ManagerGroupName != null && !string.IsNullOrEmpty(managerQueryEntity.ManagerGroupName))
            {
                database.AddInParameter(dbCommand, "ManagerGroupName", DbType.String, managerQueryEntity.ManagerGroupName);
            }

            return CommPager.QueryPager(database, dbCommand, SQL_SELECT_Manager, pageNo, pageSize, out rowCount, "UM_UserInfo");

        }
        #endregion

        #region ����Ա��½��֤

        /// <summary>
        /// ����Ա��½��֤
        /// </summary>
        public ManagementCenter.Model.UM_UserInfo ManagerLoginConfirm(string LoginName, string Password, int AddType)
        {
            string SQL_SELECT_Login =
           @"SELECT [UserName],[LoginName],[Password],[UserID],[CertificateStyle],[Postalcode],[RoleID],
     [CertificateNo],[Telephone],[Address],[Email],[QuestionID],[Answer],[CouterID],[Remark],[AddType],[AddTime]
 FROM UM_UserInfo where LoginName=@LoginName AND Password=@Password AND (RoleID=@MRoleID OR RoleID=@ARoleID) AND AddType=@AddType  ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_SELECT_Login);
            db.AddInParameter(dbCommand, "LoginName", DbType.String, LoginName);
            db.AddInParameter(dbCommand, "Password", DbType.String, Password);
            db.AddInParameter(dbCommand, "MRoleID", DbType.Int32, (int)Types.RoleTypeEnum.Manager);
            db.AddInParameter(dbCommand, "ARoleID", DbType.Int32, (int)Types.RoleTypeEnum.Admin);
            db.AddInParameter(dbCommand, "AddType", DbType.Int32, AddType);
            ManagementCenter.Model.UM_UserInfo model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }
        #endregion

        #region ����Ա�һ�����

        /// <summary>
        /// ����Ա�һ�����
        /// </summary>
        public ManagementCenter.Model.UM_UserInfo SeekForPassword(string LoginName, string Answer, int QuestionID)
        {
            string SQL_SELECT_Login =
           @"SELECT [UserName],[LoginName],[Password],[UserID],[CertificateStyle],[Postalcode],[RoleID],
[CertificateNo],[Telephone],[Address],[Email],[QuestionID],[Answer],[CouterID],[Remark],[AddType],[AddTime]
 FROM UM_UserInfo where LoginName=@LoginName AND Answer=@Answer AND QuestionID=@QuestionID  ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_SELECT_Login);
            db.AddInParameter(dbCommand, "LoginName", DbType.String, LoginName);
            db.AddInParameter(dbCommand, "Answer", DbType.String, Answer);
            db.AddInParameter(dbCommand, "QuestionID", DbType.Int32, QuestionID);
            ManagementCenter.Model.UM_UserInfo model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }
        #endregion

        #region �Թ���Ա��½�������������֤
        /// <summary>
        /// �Թ���Ա�û��������������֤
        /// </summary>
        /// <param name="LoginName">����Ա�û���</param>
        /// <param name="PassWord">����</param>
        /// <returns>��ѯ��������</returns>
        public ManagementCenter.Model.UM_UserInfo AdminLogin(string LoginName, string PassWord)
        {
            string SQL_SELECT_Login =
           @"SELECT [UserName],[LoginName],[Password],[UserID],[CertificateStyle],[Postalcode],[RoleID],
[CertificateNo],[Telephone],[Address],[Email],[QuestionID],[Answer],[CouterID],[Remark],[AddType],[AddTime]
 FROM UM_UserInfo where LoginName=@LoginName AND Password=@Password AND (RoleID=1 or RoleID=2)";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_SELECT_Login);
            db.AddInParameter(dbCommand, "LoginName", DbType.String, LoginName);
            db.AddInParameter(dbCommand, "Password", DbType.String, PassWord);
            ManagementCenter.Model.UM_UserInfo model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }
        #endregion �Թ���Ա��½�������������֤

        #region ��ѯ������Ա
        /// <summary>
        /// ��ȡ���й���Ա
        /// </summary>
        /// <returns>��ȡ���н���Ա</returns>
        public List<ManagementCenter.Model.UM_UserInfo> GetAllUser()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,b.name ");
            strSql.Append(" from UM_UserInfo a,CT_Counter b where b.CouterID=a.CouterID ");
            List<ManagementCenter.Model.UM_UserInfo> list = new List<UM_UserInfo>();
            Database db = DatabaseFactory.CreateDatabase();
            using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, strSql.ToString()))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderBind(dataReader));
                }
            }
            return list;
        }
        #endregion ��ѯ������Ա
    }
}

