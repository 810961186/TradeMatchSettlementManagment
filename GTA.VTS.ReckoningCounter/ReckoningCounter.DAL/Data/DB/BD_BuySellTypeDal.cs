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
    /// ���ݷ�����BD_BuySellTypeDal��
    /// </summary>
    public class BD_BuySellTypeDal
    {
        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(BuysellId)+1 from BD_BuySellType";
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
        public bool Exists(int BuysellId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BD_BuySellType where BuysellId=@BuysellId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BuysellId", DbType.Int32, BuysellId);
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
        public void Add(BD_BuySellTypeInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into BD_BuySellType(");
            strSql.Append("BuysellId,BuysellName)");

            strSql.Append(" values (");
            strSql.Append("@BuysellId,@BuysellName)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BuysellId", DbType.Int32, model.BuysellId);
            db.AddInParameter(dbCommand, "BuysellName", DbType.AnsiString, model.BuysellName);
            db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(BD_BuySellTypeInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("update BD_BuySellType set ");
            strSql.Append("BuysellName=@BuysellName");
            strSql.Append(" where BuysellId=@BuysellId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BuysellId", DbType.Int32, model.BuysellId);
            db.AddInParameter(dbCommand, "BuysellName", DbType.AnsiString, model.BuysellName);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int BuysellId)
        {
			
            StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from BD_BuySellType ");
            strSql.Append(" where BuysellId=@BuysellId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BuysellId", DbType.Int32,BuysellId);
            db.ExecuteNonQuery(dbCommand);

        }*/

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public BD_BuySellTypeInfo GetModel(int BuysellId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BuysellId,BuysellName from BD_BuySellType ");
            strSql.Append(" where BuysellId=@BuysellId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BuysellId", DbType.Int32, BuysellId);
            BD_BuySellTypeInfo model = null;
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
        public List<BD_BuySellTypeInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BuysellId,BuysellName ");
            strSql.Append(" FROM BD_BuySellType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<BD_BuySellTypeInfo> list = new List<BD_BuySellTypeInfo>();
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
        public BD_BuySellTypeInfo ReaderBind(IDataReader dataReader)
        {
            BD_BuySellTypeInfo model = new BD_BuySellTypeInfo();
            object ojb;
            ojb = dataReader["BuysellId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BuysellId = (int) ojb;
            }
            model.BuysellName = dataReader["BuysellName"].ToString();
            return model;
        }

        #endregion  ��Ա����
    }
}