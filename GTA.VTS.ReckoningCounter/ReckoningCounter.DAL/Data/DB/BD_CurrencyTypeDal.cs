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
    /// ���ݷ�����BD_CurrencyTypeDal��
    /// </summary>
    public class BD_CurrencyTypeDal
    {
        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(CurrencyTypeId)+1 from BD_CurrencyType";
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
        public bool Exists(int CurrencyTypeId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BD_CurrencyType where CurrencyTypeId=@CurrencyTypeId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, CurrencyTypeId);
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
        public void Add(BD_CurrencyTypeInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into BD_CurrencyType(");
            strSql.Append("CurrencyTypeId,CurrencyName)");

            strSql.Append(" values (");
            strSql.Append("@CurrencyTypeId,@CurrencyName)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, model.CurrencyTypeId);
            db.AddInParameter(dbCommand, "CurrencyName", DbType.AnsiString, model.CurrencyName);
            db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(BD_CurrencyTypeInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("update BD_CurrencyType set ");
            strSql.Append("CurrencyName=@CurrencyName");
            strSql.Append(" where CurrencyTypeId=@CurrencyTypeId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, model.CurrencyTypeId);
            db.AddInParameter(dbCommand, "CurrencyName", DbType.AnsiString, model.CurrencyName);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int CurrencyTypeId)
        {
			
            StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from BD_CurrencyType ");
            strSql.Append(" where CurrencyTypeId=@CurrencyTypeId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32,CurrencyTypeId);
            db.ExecuteNonQuery(dbCommand);

        }*/

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public BD_CurrencyTypeInfo GetModel(int CurrencyTypeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CurrencyTypeId,CurrencyName from BD_CurrencyType ");
            strSql.Append(" where CurrencyTypeId=@CurrencyTypeId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, CurrencyTypeId);
            BD_CurrencyTypeInfo model = null;
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
        public List<BD_CurrencyTypeInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CurrencyTypeId,CurrencyName ");
            strSql.Append(" FROM BD_CurrencyType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<BD_CurrencyTypeInfo> list = new List<BD_CurrencyTypeInfo>();
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
        public BD_CurrencyTypeInfo ReaderBind(IDataReader dataReader)
        {
            BD_CurrencyTypeInfo model = new BD_CurrencyTypeInfo();
            object ojb;
            ojb = dataReader["CurrencyTypeId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CurrencyTypeId = (int) ojb;
            }
            model.CurrencyName = dataReader["CurrencyName"].ToString();
            return model;
        }

        #endregion  ��Ա����
    }
}