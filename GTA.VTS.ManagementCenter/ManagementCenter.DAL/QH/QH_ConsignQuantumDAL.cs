using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using ManagementCenter.Model.CommonClass;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Maticsoft.DBUtility; //�����������

namespace ManagementCenter.DAL
{
    /// <summary>
    ///���������׹���ί���� ���ݷ�����QH_ConsignQuantumDAL��
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    public class QH_ConsignQuantumDAL
    {
        public QH_ConsignQuantumDAL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(ConsignQuantumID)+1 from QH_ConsignQuantum";
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
        public bool Exists(int ConsignQuantumID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from QH_ConsignQuantum where ConsignQuantumID=@ConsignQuantumID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ConsignQuantumID", DbType.Int32, ConsignQuantumID);
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

        #region ��ӽ��׹���ί����

        /// <summary>
        /// ��ӽ��׹���ί����
        /// </summary>
        /// <param name="model">���׹���ί����ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.QH_ConsignQuantum model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QH_ConsignQuantum(");
            strSql.Append("MinConsignQuantum)");

            strSql.Append(" values (");
            strSql.Append("@MinConsignQuantum)");
            strSql.Append(";select @@IDENTITY");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "MinConsignQuantum", DbType.Int32, model.MinConsignQuantum);
            // int result;
            object obj = null;
            //object obj = db.ExecuteScalar(dbCommand);
            //if(!int.TryParse(obj.ToString(),out result))
            //{
            //    return 0;
            //}
            if (tran == null)
            {
                obj = db.ExecuteScalar(dbCommand);
            }
            else
            {
                obj = db.ExecuteScalar(dbCommand, tran);
            }
            if (string.IsNullOrEmpty(obj.ToString()))
            {
                return AppGlobalVariable.INIT_INT;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        #endregion

        #region ��ӽ��׹���ί����

        /// <summary>
        /// ��ӽ��׹���ί����
        /// </summary>
        /// <param name="model">���׹���ί����ʵ��</param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.QH_ConsignQuantum model)
        {
            return Add(model, null, null);
        }

        #endregion

        #region �޸Ľ��׹���ί����

        /// <summary>
        /// �޸Ľ��׹���ί����
        /// </summary>
        /// <param name="model">���׹���ί����ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.QH_ConsignQuantum model, DbTransaction tran,
                           Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QH_ConsignQuantum set ");
            strSql.Append("MinConsignQuantum=@MinConsignQuantum");
            strSql.Append(" where ConsignQuantumID=@ConsignQuantumID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ConsignQuantumID", DbType.Int32, model.ConsignQuantumID);
            db.AddInParameter(dbCommand, "MinConsignQuantum", DbType.Int32, model.MinConsignQuantum);
            // db.ExecuteNonQuery(dbCommand);
            if (tran == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
            return true;
        }

        #endregion

        #region �޸Ľ��׹���ί����

        /// <summary>
        /// �޸Ľ��׹���ί����
        /// </summary>
        /// <param name="model">���׹���ί����ʵ��</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.QH_ConsignQuantum model)
        {
            return Update(model, null, null);
        }

        #endregion

        #region  ���ݽ��׹���ί����ID��ɾ�����׹���ί����

        /// <summary>
        /// ���ݽ��׹���ί����ID��ɾ�����׹���ί����
        /// </summary>
        /// <param name="ConsignQuantumID">���׹���ί����ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Delete(int ConsignQuantumID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete QH_ConsignQuantum ");
            strSql.Append(" where ConsignQuantumID=@ConsignQuantumID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ConsignQuantumID", DbType.Int32, ConsignQuantumID);
            //db.ExecuteNonQuery(dbCommand);
            object obj;
            if (tran == null)
            {
                obj = db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                obj = db.ExecuteNonQuery(dbCommand, tran);
            }
            return true;
        }

        #endregion

        #region  ���ݽ��׹���ί����ID��ɾ�����׹���ί����

        /// <summary>
        /// ���ݽ��׹���ί����ID��ɾ�����׹���ί����
        /// </summary>
        /// <param name="ConsignQuantumID">���׹���ί����ʵ��</param>
        /// <returns></returns>
        public bool Delete(int ConsignQuantumID)
        {
            return Delete(ConsignQuantumID, null, null);
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_ConsignQuantum GetModel(int ConsignQuantumID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ConsignQuantumID,MinConsignQuantum from QH_ConsignQuantum ");
            strSql.Append(" where ConsignQuantumID=@ConsignQuantumID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ConsignQuantumID", DbType.Int32, ConsignQuantumID);
            ManagementCenter.Model.QH_ConsignQuantum model = null;
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
            strSql.Append("select ConsignQuantumID,MinConsignQuantum ");
            strSql.Append(" FROM QH_ConsignQuantum ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }


        /// <summary>
        /// ��������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        public List<ManagementCenter.Model.QH_ConsignQuantum> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ConsignQuantumID,MinConsignQuantum ");
            strSql.Append(" FROM QH_ConsignQuantum ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.QH_ConsignQuantum> list = new List<ManagementCenter.Model.QH_ConsignQuantum>();
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
        public ManagementCenter.Model.QH_ConsignQuantum ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.QH_ConsignQuantum model = new ManagementCenter.Model.QH_ConsignQuantum();
            object ojb;
            ojb = dataReader["ConsignQuantumID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ConsignQuantumID = (int) ojb;
            }
            ojb = dataReader["MinConsignQuantum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MinConsignQuantum = (int) ojb;
            }
            return model;
        }

        #endregion  ��Ա����
    }
}