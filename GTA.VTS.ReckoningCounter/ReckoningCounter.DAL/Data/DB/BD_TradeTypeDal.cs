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
    /// ���ݷ�����BD_TradeTypeDal��
    /// </summary>
    public class BD_TradeTypeDal
    {
        #region  ��Ա����

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int TradeTypeId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BD_TradeType where TradeTypeId=@TradeTypeId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeTypeId", DbType.Int32, TradeTypeId);
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
        public int Add(BD_TradeTypeInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into BD_TradeType(");
            strSql.Append("TradeTypeName)");

            strSql.Append(" values (");
            strSql.Append("@TradeTypeName)");
            strSql.Append(";select @@IDENTITY");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeTypeName", DbType.AnsiString, model.TradeTypeName);
            int result;
            object obj = db.ExecuteScalar(dbCommand);
            if(!int.TryParse(obj.ToString(),out result))
            {
                return 0;
            }
            return result;
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(BD_TradeTypeInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("update BD_TradeType set ");
            strSql.Append("TradeTypeName=@TradeTypeName");
            strSql.Append(" where TradeTypeId=@TradeTypeId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeTypeId", DbType.Int32, model.TradeTypeId);
            db.AddInParameter(dbCommand, "TradeTypeName", DbType.AnsiString, model.TradeTypeName);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int TradeTypeId)
        {
			
            StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from BD_TradeType ");
            strSql.Append(" where TradeTypeId=@TradeTypeId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeTypeId", DbType.Int32,TradeTypeId);
            db.ExecuteNonQuery(dbCommand);

        }*/

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public BD_TradeTypeInfo GetModel(int TradeTypeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TradeTypeId,TradeTypeName from BD_TradeType ");
            strSql.Append(" where TradeTypeId=@TradeTypeId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeTypeId", DbType.Int32, TradeTypeId);
            BD_TradeTypeInfo model = null;
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
        public List<BD_TradeTypeInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TradeTypeId,TradeTypeName ");
            strSql.Append(" FROM BD_TradeType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<BD_TradeTypeInfo> list = new List<BD_TradeTypeInfo>();
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
        public BD_TradeTypeInfo ReaderBind(IDataReader dataReader)
        {
            BD_TradeTypeInfo model = new BD_TradeTypeInfo();
            object ojb;
            ojb = dataReader["TradeTypeId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradeTypeId = (int) ojb;
            }
            model.TradeTypeName = dataReader["TradeTypeName"].ToString();
            return model;
        }

        #endregion  ��Ա����
    }
}