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
    /// ���ݷ�����BD_FreezeTypeDal��
    /// </summary>
    public class BD_FreezeTypeDal
    {
        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(FreezeTypeLogo)+1 from BD_FreezeType";
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
        public bool Exists(int FreezeTypeLogo)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BD_FreezeType where FreezeTypeLogo=@FreezeTypeLogo ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FreezeTypeLogo", DbType.Int32, FreezeTypeLogo);
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
        public void Add(BD_FreezeTypeInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into BD_FreezeType(");
            strSql.Append("FreezeTypeLogo,FreezeDescribe)");

            strSql.Append(" values (");
            strSql.Append("@FreezeTypeLogo,@FreezeDescribe)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FreezeTypeLogo", DbType.Int32, model.FreezeTypeLogo);
            db.AddInParameter(dbCommand, "FreezeDescribe", DbType.AnsiString, model.FreezeDescribe);
            db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(BD_FreezeTypeInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("update BD_FreezeType set ");
            strSql.Append("FreezeDescribe=@FreezeDescribe");
            strSql.Append(" where FreezeTypeLogo=@FreezeTypeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FreezeTypeLogo", DbType.Int32, model.FreezeTypeLogo);
            db.AddInParameter(dbCommand, "FreezeDescribe", DbType.AnsiString, model.FreezeDescribe);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int FreezeTypeLogo)
        {
			
            StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from BD_FreezeType ");
            strSql.Append(" where FreezeTypeLogo=@FreezeTypeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FreezeTypeLogo", DbType.Int32,FreezeTypeLogo);
            db.ExecuteNonQuery(dbCommand);

        }*/

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public BD_FreezeTypeInfo GetModel(int FreezeTypeLogo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FreezeTypeLogo,FreezeDescribe from BD_FreezeType ");
            strSql.Append(" where FreezeTypeLogo=@FreezeTypeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FreezeTypeLogo", DbType.Int32, FreezeTypeLogo);
            BD_FreezeTypeInfo model = null;
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
        public List<BD_FreezeTypeInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FreezeTypeLogo,FreezeDescribe ");
            strSql.Append(" FROM BD_FreezeType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<BD_FreezeTypeInfo> list = new List<BD_FreezeTypeInfo>();
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
        public BD_FreezeTypeInfo ReaderBind(IDataReader dataReader)
        {
            BD_FreezeTypeInfo model = new BD_FreezeTypeInfo();
            object ojb;
            ojb = dataReader["FreezeTypeLogo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FreezeTypeLogo = (int) ojb;
            }
            model.FreezeDescribe = dataReader["FreezeDescribe"].ToString();
            return model;
        }

        #endregion  ��Ա����
    }
}