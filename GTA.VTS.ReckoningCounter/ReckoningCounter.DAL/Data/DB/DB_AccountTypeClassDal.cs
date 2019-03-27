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
    /// ���ݷ�����DB_AccountTypeClassDal��
    /// </summary>
    public class DB_AccountTypeClassDal
    {
        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(ATCId)+1 from DB_AccountTypeClass";
            Database db = DatabaseFactory.CreateDatabase();
            object obj = db.ExecuteScalar(CommandType.Text, strsql);
            if (obj != null && obj != DBNull.Value)
            {
                return int.Parse(obj.ToString());
            }
            return 1;
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int ATCId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DB_AccountTypeClass where ATCId=@ATCId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ATCId", DbType.Int32, ATCId);
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

        /*
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(DB_AccountTypeClassInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into DB_AccountTypeClass(");
            strSql.Append("ATCId,ATCName)");

            strSql.Append(" values (");
            strSql.Append("@ATCId,@ATCName)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ATCId", DbType.Int32, model.ATCId);
            db.AddInParameter(dbCommand, "ATCName", DbType.AnsiString, model.ATCName);
            db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(DB_AccountTypeClassInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("update DB_AccountTypeClass set ");
            strSql.Append("ATCName=@ATCName");
            strSql.Append(" where ATCId=@ATCId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ATCId", DbType.Int32, model.ATCId);
            db.AddInParameter(dbCommand, "ATCName", DbType.AnsiString, model.ATCName);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int ATCId)
        {
			
            StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from DB_AccountTypeClass ");
            strSql.Append(" where ATCId=@ATCId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ATCId", DbType.Int32,ATCId);
            db.ExecuteNonQuery(dbCommand);

        }*/

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public DB_AccountTypeClassInfo GetModel(int ATCId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ATCId,ATCName from DB_AccountTypeClass ");
            strSql.Append(" where ATCId=@ATCId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ATCId", DbType.Int32, ATCId);
            DB_AccountTypeClassInfo model = null;
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
        public List<DB_AccountTypeClassInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ATCId,ATCName ");
            strSql.Append(" FROM DB_AccountTypeClass ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<DB_AccountTypeClassInfo> list = new List<DB_AccountTypeClassInfo>();
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
        public DB_AccountTypeClassInfo ReaderBind(IDataReader dataReader)
        {
            DB_AccountTypeClassInfo model = new DB_AccountTypeClassInfo();
            object ojb;
            ojb = dataReader["ATCId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ATCId = (int) ojb;
            }
            model.ATCName = dataReader["ATCName"].ToString();
            return model;
        }

        #endregion  ��Ա����
    }
}