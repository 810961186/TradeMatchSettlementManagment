#region Using Namespace

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReckoningCounter.Model;
using ReckoningCounter.Entity.AccountManagementAndFindEntity;

#endregion

namespace ReckoningCounter.DAL.Data
{
    /// <summary>
    /// ���ݷ�����UA_UserAccountAllocationTableDal��
    /// </summary>
    public class UA_UserAccountAllocationTableDal
    {
        #region  ��Ա����

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string UserAccountDistributeLogo)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select count(1) from UA_UserAccountAllocationTable where UserAccountDistributeLogo=@UserAccountDistributeLogo ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, UserAccountDistributeLogo);
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
        /// ���Ƿ�����������һ������
        /// <param name="model">Ҫ������û��˺Ŷ���</param>
        /// <param name="db">�������ݶ���</param>
        /// <param name="trm">��������������Ϊnull������</param>
        /// </summary>
        public void Add(UA_UserAccountAllocationTableInfo model, Database db, DbTransaction trm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UA_UserAccountAllocationTable(");
            strSql.Append("UserAccountDistributeLogo,WhetherAvailable,UserID,AccountTypeLogo)");

            strSql.Append(" values (");
            strSql.Append("@UserAccountDistributeLogo,@WhetherAvailable,@UserID,@AccountTypeLogo)");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "WhetherAvailable", DbType.Boolean, model.WhetherAvailable);
            db.AddInParameter(dbCommand, "UserID", DbType.AnsiString, model.UserID);
            db.AddInParameter(dbCommand, "AccountTypeLogo", DbType.Int32, model.AccountTypeLogo);
            if (trm == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, trm);
            }
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(UA_UserAccountAllocationTableInfo model)
        {
            Database db = DatabaseFactory.CreateDatabase();
            Add(model, db, null);
        }

        /// <summary>
        /// �����û�ID���˺�����ID�����˺ŵ�ʹ��״̬�Ƿ���ã�����/�ⶳ)
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="accountTypeLogo">�˺�����ID</param>
        /// <param name="state">״̬�Ƿ����</param>
        public void Update(string userID, int accountTypeLogo, bool state)
        {
            Database db = DatabaseFactory.CreateDatabase();
            Update(userID, accountTypeLogo, state, db, null);
        }
        /// <summary>
        /// �����û�ID���˺�����ID�����˺ŵ�ʹ��״̬�Ƿ���ã�����/�ⶳ)
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="accountTypeLogo">�˺�����ID</param>
        /// <param name="state">״̬�Ƿ����</param>
        /// <param name="db"></param>
        public void Update(string userID, int accountTypeLogo, bool state, Database db)
        {
            Update(userID, accountTypeLogo, state, db, null);
        }
        /// <summary>
        /// �����û�ID���˺�����ID�����˺ŵ�ʹ��״̬�Ƿ���ã�����/�ⶳ)
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="accountTypeLogo">�˺�����ID</param>
        /// <param name="state">״̬�Ƿ����</param>
        /// <param name="db"></param>
        /// <param name="tran">�����������Ϊnullʱ������</param>
        public void Update(string userID, int accountTypeLogo, bool state, Database db, DbTransaction tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UA_UserAccountAllocationTable set ");
            strSql.Append("WhetherAvailable=@WhetherAvailable ");
            strSql.Append(" where UserID=@UserID And AccountTypeLogo=@AccountTypeLogo ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "WhetherAvailable", DbType.Boolean, state);
            db.AddInParameter(dbCommand, "UserID", DbType.AnsiString, userID);
            db.AddInParameter(dbCommand, "AccountTypeLogo", DbType.Int32, accountTypeLogo);
            if (tran != null)
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand);
            }
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(UA_UserAccountAllocationTableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UA_UserAccountAllocationTable set ");
            strSql.Append("WhetherAvailable=@WhetherAvailable,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("AccountTypeLogo=@AccountTypeLogo");
            strSql.Append(" where UserAccountDistributeLogo=@UserAccountDistributeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "WhetherAvailable", DbType.Boolean, model.WhetherAvailable);
            db.AddInParameter(dbCommand, "UserID", DbType.AnsiString, model.UserID);
            db.AddInParameter(dbCommand, "AccountTypeLogo", DbType.Int32, model.AccountTypeLogo);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(string UserAccountDistributeLogo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UA_UserAccountAllocationTable ");
            strSql.Append(" where UserAccountDistributeLogo=@UserAccountDistributeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, UserAccountDistributeLogo);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public UA_UserAccountAllocationTableInfo GetModel(string UserAccountDistributeLogo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select UserAccountDistributeLogo,WhetherAvailable,UserID,AccountTypeLogo from UA_UserAccountAllocationTable ");
            strSql.Append(" where UserAccountDistributeLogo=@UserAccountDistributeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, UserAccountDistributeLogo);
            UA_UserAccountAllocationTableInfo model = null;
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
        public List<UA_UserAccountAllocationTableInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserAccountDistributeLogo,WhetherAvailable,UserID,AccountTypeLogo ");
            strSql.Append(" FROM UA_UserAccountAllocationTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<UA_UserAccountAllocationTableInfo> list = new List<UA_UserAccountAllocationTableInfo>();
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
        public List<UA_UserAccountAllocationTableInfo> GetAll()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserAccountDistributeLogo,WhetherAvailable,UserID,AccountTypeLogo ");
            strSql.Append(" FROM UA_UserAccountAllocationTable ");
            
            List<UA_UserAccountAllocationTableInfo> list = new List<UA_UserAccountAllocationTableInfo>();
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
        public UA_UserAccountAllocationTableInfo ReaderBind(IDataReader dataReader)
        {
            UA_UserAccountAllocationTableInfo model = new UA_UserAccountAllocationTableInfo();
            object ojb;
            model.UserAccountDistributeLogo = dataReader["UserAccountDistributeLogo"].ToString();
            ojb = dataReader["WhetherAvailable"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.WhetherAvailable = (bool)ojb;
            }
            model.UserID = dataReader["UserID"].ToString();
            ojb = dataReader["AccountTypeLogo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AccountTypeLogo = (int)ojb;
            }
            return model;
        }

        /// <summary>
        /// �����û�ID���˺����Ͳ�ѯ�˺���Ϣ
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="accountType">�˺����������������ͣ�����Ŀǰ���ݿ��оŸ�������������������ֵҪ����</param>
        /// <returns></returns>
        public List<UA_UserAccountAllocationTableInfo> GetUserAccountByUserIDAndPwdAndAccountType(string userId, int accountType)
        {
            return GetUserAccountByUserIDAndPwdAndAccountType(userId, "", accountType);
        }

        /// <summary>
        /// �����û�ID�����룬�˺����Ͳ�ѯ�˺���Ϣ
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="pwd">�û�����</param>
        /// <param name="accountType">�˺������ͣ�����Ŀǰ���ݿ��оŸ���</param>
        /// <returns></returns>
        public List<UA_UserAccountAllocationTableInfo> GetUserAccountByUserIDAndPwdAndAccountType(string userId, string pwd, int  accountType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserAccountDistributeLogo,WhetherAvailable,UserID,AccountTypeLogo from dbo.UA_UserAccountAllocationTable ");
            strSql.Append("  where accounttypelogo=@accounttypelogo) ");
            if (string.IsNullOrEmpty(pwd))
            {
                strSql.Append(" and  userid=@userid ");
            }
            else
            {
                strSql.Append(" and  Password =@Password and userid=@userid) ");
            }

            List<UA_UserAccountAllocationTableInfo> list = new List<UA_UserAccountAllocationTableInfo>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "accounttypelogo", DbType.Int32, accountType);
            if (!string.IsNullOrEmpty(pwd))
            {
                db.AddInParameter(dbCommand, "Password", DbType.String, pwd);
            }
            db.AddInParameter(dbCommand, "userid", DbType.String, userId.Trim());

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderBind(dataReader));
                }
            }
            return list;
        }
       
        /// <summary>
        /// �����û�ID���˺�����ѯ�˺���Ϣ
        /// ��Ϊһ���û�����ӵ�ж���˺����Է���list���磺�ֻ��ʽ��ʻ�-->֤ȯ�ʽ��ʻ�,�۹��ʽ��ʻ�)
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="accountTypeClass">�˺����������������ͣ�����Ŀǰ���ݿ��оŸ�������������������ֵҪ����</param>
        /// <returns></returns>
        public List<UA_UserAccountAllocationTableInfo> GetUserAccountByUserIDAndPwdAndAccountTypeClass(string userId, GTA.VTS.Common.CommonObject.Types.AccountAttributionType accountTypeClass)
        {
            return GetUserAccountByUserIDAndPwdAndAccountTypeClass(userId, "", accountTypeClass);
        }

        /// <summary>
        /// �����û�ID�����룬�˺�����ѯ�˺���Ϣ
        /// ��Ϊһ���û�����ӵ�ж���˺����Է���list���磺�ֻ��ʽ��ʻ�-->֤ȯ�ʽ��ʻ�,�۹��ʽ��ʻ�)
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="pwd">�û�����</param>
        /// <param name="accountTypeClass">�˺����������������ͣ�����Ŀǰ���ݿ��оŸ�������������������ֵҪ����</param>
        /// <returns></returns>
        public List<UA_UserAccountAllocationTableInfo> GetUserAccountByUserIDAndPwdAndAccountTypeClass(string userId, string pwd, GTA.VTS.Common.CommonObject.Types.AccountAttributionType accountTypeClass)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserAccountDistributeLogo,WhetherAvailable,UserID,AccountTypeLogo from dbo.UA_UserAccountAllocationTable ");
            strSql.Append("  where accounttypelogo in (select accounttypelogo from BD_AccountType where atcid=@atcid) ");
            if (string.IsNullOrEmpty(pwd))
            {
                strSql.Append(" and  userid=@userid ");
            }
            else
            {
                strSql.Append(" and userid in (select userid from dbo.UA_UserBasicInformationTable where Password =@Password and userid=@userid) ");
            }

            List<UA_UserAccountAllocationTableInfo> list = new List<UA_UserAccountAllocationTableInfo>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "atcid", DbType.Int32, (int)accountTypeClass);
            if (!string.IsNullOrEmpty(pwd))
            {
                db.AddInParameter(dbCommand, "Password", DbType.String, pwd);
            }
            db.AddInParameter(dbCommand, "userid", DbType.String, userId.Trim());

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderBind(dataReader));
                }
            }
            return list;
        }
       
        /// <summary>
        /// �����û����ʽ��˻����ع������û��ֲ��˻���Ϣ(Ҳ����������˺Ų�ѯ������������˺�)
        /// </summary>
        /// <param name="capitalAccount">�ʽ��˻�</param>
        /// <returns></returns>
        public UA_UserAccountAllocationTableInfo GetUserHoldAccountByUserCapitalAccount(string capitalAccount)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" select u1.UserAccountDistributeLogo,u1.WhetherAvailable,u1.UserID,u1.AccountTypeLogo  ");
            sb.Append(" from UA_UserAccountAllocationTable  as u1 ");
            sb.Append(" inner join UA_UserAccountAllocationTable as u2 on u2.UserAccountDistributeLogo=@UserAccountDistributeLogo ");
            sb.Append(" inner join BD_AccountType as b on b.AccountTypeLogo=u2.AccountTypeLogo ");
            sb.Append(" where u1.AccountTypeLogo =b.RelationAccountId  and u1.userID=u2.userID   ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString());
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, capitalAccount);
            UA_UserAccountAllocationTableInfo model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }
        #endregion  ��Ա����

        #region Other Method Create by:��� Date:2009-07-16
        /// <summary>
        /// ���ݲ�ѯʵ���ѯ����˺���Ϣ�����������ֻ�����û�ID���˺�����˺���������
        /// </summary>
        /// <param name="filter">Ҫ��ѯ�Ĺ���ʵ�壬��������û�ID���˺�����˺���������Ϊ""|null����</param>
        /// <returns></returns>
        public List<AccountFindResultEntity> GetUserAccountByFindFilter(FindAccountEntity filter)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select u.UserAccountDistributeLogo,u.UserID,u.AccountTypeLogo,a.AccountTypeName ");
            sb.Append(",WhetherAvailable=case u.WhetherAvailable when 1 then '����' when 0 then '�Ѷ���'");
            sb.Append(" end,c.ATCName ");
            sb.Append("  FROM  UA_UserAccountAllocationTable as u ");
            string whereStr = " 1=1 ";

            sb.Append("  inner join BD_AccountType as a on a.AccountTypeLogo=u.AccountTypeLogo ");
            if (filter.AccountType != 0)
            {
                whereStr += " And a.AccountTypeLogo='" + filter.AccountType + "'";
            }
            sb.Append(" inner join DB_AccountTypeClass as c on c.ATCId=a.ATCId ");
            if (filter.AccountAttributionType != 0)
            {
                whereStr += " And c.ATCId='" + filter.AccountAttributionType + "'";
            }
            if (!string.IsNullOrEmpty(filter.UserID))
            {
                whereStr += " And u.UserID='" + filter.UserID.Trim() + "'";
            }
            sb.Append(" where " + whereStr);
            Database db = DatabaseFactory.CreateDatabase();

            List<AccountFindResultEntity> list = new List<AccountFindResultEntity>();
            using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderDataBind(dataReader));
                }
            }
            return list;
        }
        /// <summary>
        /// �󶨲�ѯ������
        /// </summary>
        /// <param name="dr">��¼��ȡ���</param>
        /// <returns></returns>
        AccountFindResultEntity ReaderDataBind(IDataReader dr)
        {
            AccountFindResultEntity model = new AccountFindResultEntity();
            model.AccountID = dr["UserAccountDistributeLogo"].ToString();
            model.TraderID = dr["UserID"].ToString();
            model.AccountName = dr["AccountTypeName"].ToString();
            model.AccountState = dr["WhetherAvailable"].ToString();
            return model;
        }
        #endregion
    }
}