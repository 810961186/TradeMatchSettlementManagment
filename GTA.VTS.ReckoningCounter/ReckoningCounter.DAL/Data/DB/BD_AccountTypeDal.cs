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
    /// Title: ���ݷ�����BD_AccountTypeDal��
    /// Create BY�����
    /// Create Date��2009-10-15
    /// </summary>
    public class BD_AccountTypeDal
    {
        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(AccountTypeLogo)+1 from BD_AccountType";
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
        /// <param name="AccountTypeLogo">�˻���������ID</param>
        /// <returns>�Ƿ����</returns>
        public bool Exists(int AccountTypeLogo)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BD_AccountType where AccountTypeLogo=@AccountTypeLogo ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AccountTypeLogo", DbType.Int32, AccountTypeLogo);
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
        public void Add(BD_AccountTypeInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into BD_AccountType(");
            strSql.Append("AccountTypeLogo,AccountTypeName,Remarks,ATCId,RelationAccountId)");

            strSql.Append(" values (");
            strSql.Append("@AccountTypeLogo,@AccountTypeName,@Remarks,@ATCId,@RelationAccountId)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AccountTypeLogo", DbType.Int32, model.AccountTypeLogo);
            db.AddInParameter(dbCommand, "AccountTypeName", DbType.AnsiString, model.AccountTypeName);
            db.AddInParameter(dbCommand, "Remarks", DbType.AnsiString, model.Remarks);
            db.AddInParameter(dbCommand, "ATCId", DbType.Int32, model.ATCId);
            db.AddInParameter(dbCommand, "RelationAccountId", DbType.Int32, model.RelationAccountId);
            db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(BD_AccountTypeInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("update BD_AccountType set ");
            strSql.Append("AccountTypeName=@AccountTypeName,");
            strSql.Append("Remarks=@Remarks,");
            strSql.Append("ATCId=@ATCId,");
            strSql.Append("RelationAccountId=@RelationAccountId");
            strSql.Append(" where AccountTypeLogo=@AccountTypeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AccountTypeLogo", DbType.Int32, model.AccountTypeLogo);
            db.AddInParameter(dbCommand, "AccountTypeName", DbType.AnsiString, model.AccountTypeName);
            db.AddInParameter(dbCommand, "Remarks", DbType.AnsiString, model.Remarks);
            db.AddInParameter(dbCommand, "ATCId", DbType.Int32, model.ATCId);
            db.AddInParameter(dbCommand, "RelationAccountId", DbType.Int32, model.RelationAccountId);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int AccountTypeLogo)
        {
			
            StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from BD_AccountType ");
            strSql.Append(" where AccountTypeLogo=@AccountTypeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AccountTypeLogo", DbType.Int32,AccountTypeLogo);
            db.ExecuteNonQuery(dbCommand);

        }*/

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        /// <param name="AccountTypeLogo">�˻���������ID</param>
        /// <returns>�˻�����ʵ��</returns>
        public BD_AccountTypeInfo GetModel(int AccountTypeLogo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AccountTypeLogo,AccountTypeName,Remarks,ATCId,RelationAccountId from BD_AccountType ");
            strSql.Append(" where AccountTypeLogo=@AccountTypeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AccountTypeLogo", DbType.Int32, AccountTypeLogo);
            BD_AccountTypeInfo model = null;
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
        /// <param name="strWhere">Where����</param>
        /// <returns>�˻�����ʵ���б�</returns>
        public List<BD_AccountTypeInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AccountTypeLogo,AccountTypeName,Remarks,ATCId,RelationAccountId ");
            strSql.Append(" FROM BD_AccountType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<BD_AccountTypeInfo> list = new List<BD_AccountTypeInfo>();
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
        /// <returns>�˻�����ʵ���б�</returns>
        public List<BD_AccountTypeInfo> GetAll()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AccountTypeLogo,AccountTypeName,Remarks,ATCId,RelationAccountId ");
            strSql.Append(" FROM BD_AccountType ");
            
            List<BD_AccountTypeInfo> list = new List<BD_AccountTypeInfo>();
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
        /// <param name="dataReader">IDataReaderʵ��</param>
        /// <returns>�˻�����ʵ��</returns>
        public BD_AccountTypeInfo ReaderBind(IDataReader dataReader)
        {
            BD_AccountTypeInfo model = new BD_AccountTypeInfo();
            object ojb;
            ojb = dataReader["AccountTypeLogo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AccountTypeLogo = (int) ojb;
            }
            model.AccountTypeName = dataReader["AccountTypeName"].ToString();
            model.Remarks = dataReader["Remarks"].ToString();
            ojb = dataReader["ATCId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ATCId = (int) ojb;
            }
            ojb = dataReader["RelationAccountId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RelationAccountId = (int) ojb;
            }
            return model;
        }

        #endregion  ��Ա����
    }
}