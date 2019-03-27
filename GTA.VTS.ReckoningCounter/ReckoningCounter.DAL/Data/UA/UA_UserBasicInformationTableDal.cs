#region Using Namespace

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReckoningCounter.Model;

#endregion

namespace ReckoningCounter.DAL.Data
{
    /// <summary>
    /// ���ݷ�����UA_UserBasicInformationTableDal��
    /// </summary>
    public class UA_UserBasicInformationTableDal
    {
        #region  ��Ա����

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string UserID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UA_UserBasicInformationTable where UserID=@UserID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.AnsiString, UserID);
            int cmdresult;
            object obj = db.ExecuteScalar(dbCommand);
            if ((Equals(obj, null)) || (Equals(obj, DBNull.Value)))
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
        /// <summary>
        /// �����û�ID��������֤�Ƿ���ڼ�¼
        /// </summary>
        public bool Exists(string UserID, string pwd)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UA_UserBasicInformationTable where UserID=@UserID And Password=@pwd ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            //DbCommand dbCommand = db.GetStoredProcCommand("Proc_SelectUA_UserBasicInformationTableByUserIDAndPwd");
            db.AddInParameter(dbCommand, "UserID", DbType.String, UserID);
            db.AddInParameter(dbCommand, "pwd", DbType.String, pwd);
            int cmdresult;
            object obj = db.ExecuteScalar(dbCommand);
            if ((Equals(obj, null)) || (Equals(obj, DBNull.Value)))
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
        /// <summary>
        /// �����û�ID��������֤�ͽ�ɫ�ж��Ƿ���ڼ�¼
        /// </summary>
        public bool Exists(string UserID, string pwd, GTA.VTS.Common.CommonObject.Types.UserId roleNumber)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UA_UserBasicInformationTable where UserID=@UserID And Password=@pwd And RoleNumber=@RoleNumber");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.String, UserID);
            db.AddInParameter(dbCommand, "pwd", DbType.String, pwd);
            db.AddInParameter(dbCommand, "RoleNumber", DbType.Int32, (int)roleNumber);
            int cmdresult;
            object obj = db.ExecuteScalar(dbCommand);
            if ((Equals(obj, null)) || (Equals(obj, DBNull.Value)))
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
        /// <summary>
        /// �������������ݿ�����һ������
        /// </summary>
        public void Add(UA_UserBasicInformationTableInfo model, Database db, DbTransaction trm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UA_UserBasicInformationTable(");
            strSql.Append("UserID,Password,RoleNumber)");

            strSql.Append(" values (");
            strSql.Append("@UserID,@Password,@RoleNumber)");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.AnsiString, model.UserID);
            db.AddInParameter(dbCommand, "Password", DbType.AnsiString, model.Password);
            db.AddInParameter(dbCommand, "RoleNumber", DbType.Int32, model.RoleNumber);
            if (trm != null)
            {
                db.ExecuteNonQuery(dbCommand, trm);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand);
            }

        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(UA_UserBasicInformationTableInfo model)
        {
            Database db = DatabaseFactory.CreateDatabase();
            Add(model, db, null);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(UA_UserBasicInformationTableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UA_UserBasicInformationTable set ");
            strSql.Append("Password=@Password,");
            strSql.Append("RoleNumber=@RoleNumber");
            strSql.Append(" where UserID=@UserID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.AnsiString, model.UserID);
            db.AddInParameter(dbCommand, "Password", DbType.AnsiString, model.Password);
            db.AddInParameter(dbCommand, "RoleNumber", DbType.Int32, model.RoleNumber);
            db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// �����û�ID�����û�����
        /// Create BY:���
        /// Crate date:2009-07-16
        /// </summary>
        /// <param name="Pwd">������</param>
        /// <param name="userID">�û�ID</param>
        /// <returns></returns>
        public bool UpdatePwdByUserID(string Pwd, string userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UA_UserBasicInformationTable set ");
            strSql.Append("Password=@Password ");
            strSql.Append(" where UserID=@UserID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.AnsiString, userID);
            db.AddInParameter(dbCommand, "Password", DbType.AnsiString, Pwd);
            try
            {
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UA_UserBasicInformationTable ");
            strSql.Append(" where UserID=@UserID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.AnsiString, UserID);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public UA_UserBasicInformationTableInfo GetModel(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID,Password,RoleNumber from UA_UserBasicInformationTable ");
            strSql.Append(" where UserID=@UserID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.AnsiString, UserID);
            UA_UserBasicInformationTableInfo model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }


        /// <summary>
        /// ��������б�
        /// </summary>
        public List<UA_UserBasicInformationTableInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID,Password,RoleNumber ");
            strSql.Append(" FROM UA_UserBasicInformationTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<UA_UserBasicInformationTableInfo> list = new List<UA_UserBasicInformationTableInfo>();
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

        /// <summary>
        /// ��������б�
        /// </summary>
        public List<UA_UserBasicInformationTableInfo> GetAll()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID,Password,RoleNumber ");
            strSql.Append(" FROM UA_UserBasicInformationTable ");

            List<UA_UserBasicInformationTableInfo> list = new List<UA_UserBasicInformationTableInfo>();
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


        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public UA_UserBasicInformationTableInfo ReaderBind(IDataReader dataReader)
        {
            UA_UserBasicInformationTableInfo model = new UA_UserBasicInformationTableInfo();
            object ojb;
            model.UserID = dataReader["UserID"].ToString();
            model.Password = dataReader["Password"].ToString();
            ojb = dataReader["RoleNumber"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RoleNumber = (int)ojb;
            }
            return model;
        }

        #endregion  ��Ա����
    }
}