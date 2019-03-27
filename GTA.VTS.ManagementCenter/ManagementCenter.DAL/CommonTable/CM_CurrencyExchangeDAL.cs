using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Maticsoft.DBUtility; //�����������

namespace ManagementCenter.DAL
{
    /// <summary>
    ///����:����֮��һ����� ���ݷ�����CM_CurrencyExchangeDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-20
    /// </summary>
    public class CM_CurrencyExchangeDAL
    {
        public CM_CurrencyExchangeDAL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(CurrencyExchangeID)+1 from CM_CurrencyExchange";
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
        public bool Exists(int CurrencyExchangeID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CM_CurrencyExchange where CurrencyExchangeID=@CurrencyExchangeID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CurrencyExchangeID", DbType.Int32, CurrencyExchangeID);
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


        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.CM_CurrencyExchange model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CM_CurrencyExchange(");
            strSql.Append("Describe)");

            strSql.Append(" values (");
            strSql.Append("@Describe)");
            strSql.Append(";select @@IDENTITY");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Describe", DbType.String, model.Describe);
            int result;
            object obj = db.ExecuteScalar(dbCommand);
            if (!int.TryParse(obj.ToString(), out result)) 
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.CM_CurrencyExchange model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CM_CurrencyExchange set ");
            strSql.Append("Describe=@Describe");
            strSql.Append(" where CurrencyExchangeID=@CurrencyExchangeID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CurrencyExchangeID", DbType.Int32, model.CurrencyExchangeID);
            db.AddInParameter(dbCommand, "Describe", DbType.String, model.Describe);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int CurrencyExchangeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete CM_CurrencyExchange ");
            strSql.Append(" where CurrencyExchangeID=@CurrencyExchangeID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CurrencyExchangeID", DbType.Int32, CurrencyExchangeID);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_CurrencyExchange GetModel(int CurrencyExchangeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CurrencyExchangeID,Describe from CM_CurrencyExchange ");
            strSql.Append(" where CurrencyExchangeID=@CurrencyExchangeID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CurrencyExchangeID", DbType.Int32, CurrencyExchangeID);
            ManagementCenter.Model.CM_CurrencyExchange model = null;
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
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CurrencyExchangeID,Describe ");
            strSql.Append(" FROM CM_CurrencyExchange ");
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
            db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "CM_CurrencyExchange");
            db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "ID");
            db.AddInParameter(dbCommand, "PageSize", DbType.Int32, PageSize);
            db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
            db.AddInParameter(dbCommand, "IsReCount", DbType.Boolean, 0);
            db.AddInParameter(dbCommand, "OrderType", DbType.Boolean, 0);
            db.AddInParameter(dbCommand, "strWhere", DbType.AnsiString, strWhere);
            return db.ExecuteDataSet(dbCommand);
        }*/

        /// <summary>
        /// ��������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        public List<ManagementCenter.Model.CM_CurrencyExchange> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CurrencyExchangeID,Describe ");
            strSql.Append(" FROM CM_CurrencyExchange ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.CM_CurrencyExchange> list =
                new List<ManagementCenter.Model.CM_CurrencyExchange>();
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
        public ManagementCenter.Model.CM_CurrencyExchange ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.CM_CurrencyExchange model = new ManagementCenter.Model.CM_CurrencyExchange();
            object ojb;
            ojb = dataReader["CurrencyExchangeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CurrencyExchangeID = (int) ojb;
            }
            model.Describe = dataReader["Describe"].ToString();
            return model;
        }

        #endregion  ��Ա����
    }
}