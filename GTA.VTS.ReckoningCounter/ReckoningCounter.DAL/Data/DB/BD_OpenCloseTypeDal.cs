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
    /// ���ݷ�����BD_OpenCloseTypeDal��
    /// </summary>
    public class BD_OpenCloseTypeDal
    {
        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(OCTId)+1 from BD_OpenCloseType";
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
        public bool Exists(int OCTId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BD_OpenCloseType where OCTId=@OCTId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OCTId", DbType.Int32, OCTId);
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
        public void Add(BD_OpenCloseTypeInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into BD_OpenCloseType(");
            strSql.Append("OCTId,OCTName)");

            strSql.Append(" values (");
            strSql.Append("@OCTId,@OCTName)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OCTId", DbType.Int32, model.OCTId);
            db.AddInParameter(dbCommand, "OCTName", DbType.AnsiString, model.OCTName);
            db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(BD_OpenCloseTypeInfo model)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("update BD_OpenCloseType set ");
            strSql.Append("OCTName=@OCTName");
            strSql.Append(" where OCTId=@OCTId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OCTId", DbType.Int32, model.OCTId);
            db.AddInParameter(dbCommand, "OCTName", DbType.AnsiString, model.OCTName);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int OCTId)
        {
			
            StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from BD_OpenCloseType ");
            strSql.Append(" where OCTId=@OCTId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OCTId", DbType.Int32,OCTId);
            db.ExecuteNonQuery(dbCommand);

        }*/

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public BD_OpenCloseTypeInfo GetModel(int OCTId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OCTId,OCTName from BD_OpenCloseType ");
            strSql.Append(" where OCTId=@OCTId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OCTId", DbType.Int32, OCTId);
            BD_OpenCloseTypeInfo model = null;
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
        public List<BD_OpenCloseTypeInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OCTId,OCTName ");
            strSql.Append(" FROM BD_OpenCloseType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<BD_OpenCloseTypeInfo> list = new List<BD_OpenCloseTypeInfo>();
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
        public BD_OpenCloseTypeInfo ReaderBind(IDataReader dataReader)
        {
            BD_OpenCloseTypeInfo model = new BD_OpenCloseTypeInfo();
            object ojb;
            ojb = dataReader["OCTId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OCTId = (int) ojb;
            }
            model.OCTName = dataReader["OCTName"].ToString();
            return model;
        }

        #endregion  ��Ա����
    }
}