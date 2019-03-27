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
    ///����������ί���� ���ݷ�����QH_SingleRequestQuantityDAL��
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    public class QH_SingleRequestQuantityDAL
    {
        public QH_SingleRequestQuantityDAL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(SingleRequestQuantityID)+1 from QH_SingleRequestQuantity";
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
        public bool Exists(int SingleRequestQuantityID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select count(1) from QH_SingleRequestQuantity where SingleRequestQuantityID=@SingleRequestQuantityID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "SingleRequestQuantityID", DbType.Int32, SingleRequestQuantityID);
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

        #region ��ӵ���ί����

        /// <summary>
        /// ��ӵ���ί����
        /// </summary>
        /// <param name="model">����ί����ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.QH_SingleRequestQuantity model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QH_SingleRequestQuantity(");
            strSql.Append("MaxConsignQuanturm,ConsignQuantumID,ConsignInstructionTypeID)");

            strSql.Append(" values (");
            strSql.Append("@MaxConsignQuanturm,@ConsignQuantumID,@ConsignInstructionTypeID)");
            strSql.Append(";select @@IDENTITY");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "MaxConsignQuanturm", DbType.Int32, model.MaxConsignQuanturm);
            db.AddInParameter(dbCommand, "ConsignQuantumID", DbType.Int32, model.ConsignQuantumID);
            db.AddInParameter(dbCommand, "ConsignInstructionTypeID", DbType.Int32, model.ConsignInstructionTypeID);
            //int result;
            //object obj = db.ExecuteScalar(dbCommand);
            //if(!int.TryParse(obj.ToString(),out result))
            //{
            //    return 0;
            //}
            //return result;
            object obj = null;

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

        #region ��ӵ���ί����

        /// <summary>
        /// ��ӵ���ί����
        /// </summary>
        /// <param name="model">����ί����ʵ��</param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.QH_SingleRequestQuantity model)
        {
            return Add(model, null, null);
        }

        #endregion

        #region ���µ���ί����

        /// <summary>
        /// ���µ���ί����
        /// </summary>
        /// <param name="model">����ί����ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.QH_SingleRequestQuantity model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QH_SingleRequestQuantity set ");
            strSql.Append("MaxConsignQuanturm=@MaxConsignQuanturm,");
            strSql.Append("ConsignQuantumID=@ConsignQuantumID,");
            strSql.Append("ConsignInstructionTypeID=@ConsignInstructionTypeID");
            strSql.Append(" where SingleRequestQuantityID=@SingleRequestQuantityID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "SingleRequestQuantityID", DbType.Int32, model.SingleRequestQuantityID);
            db.AddInParameter(dbCommand, "MaxConsignQuanturm", DbType.Int32, model.MaxConsignQuanturm);
            db.AddInParameter(dbCommand, "ConsignQuantumID", DbType.Int32, model.ConsignQuantumID);
            db.AddInParameter(dbCommand, "ConsignInstructionTypeID", DbType.Int32, model.ConsignInstructionTypeID);
            //db.ExecuteNonQuery(dbCommand);
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

        #region ���µ���ί����

        /// <summary>
        /// ���µ���ί����
        /// </summary>
        /// <param name="model">����ί����ʵ��</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.QH_SingleRequestQuantity model)
        {
            return Update(model, null, null);
        }

        #endregion

        #region ɾ������ί����

        /// <summary>
        /// ɾ������ί����
        /// </summary>
        /// <param name="SingleRequestQuantityID">����ί����ID</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Delete(int SingleRequestQuantityID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete QH_SingleRequestQuantity ");
            strSql.Append(" where SingleRequestQuantityID=@SingleRequestQuantityID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "SingleRequestQuantityID", DbType.Int32, SingleRequestQuantityID);
            // db.ExecuteNonQuery(dbCommand);
            // object obj;
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

        #region ɾ������ί����

        /// <summary>
        /// ɾ������ί����
        /// </summary>
        /// <param name="SingleRequestQuantityID">����ί����ID</param>
        /// <returns></returns>
        public bool Delete(int SingleRequestQuantityID)
        {
            return Delete(SingleRequestQuantityID, null, null);
        }

        #endregion

        #region ���ݽ��׹���ί����IDɾ������ί����

        /// <summary>
        /// ���ݽ��׹���ί����IDɾ������ί����
        /// </summary>
        /// <param name="ConsignQuantumID">���׹���ί����ID</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool DeleteSingleRQByConsignQuantumID(int ConsignQuantumID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete QH_SingleRequestQuantity ");
            strSql.Append(" where ConsignQuantumID=@ConsignQuantumID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ConsignQuantumID", DbType.Int32, ConsignQuantumID);
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

        #region ���ݽ��׹���ί����IDɾ������ί����

        /// <summary>
        /// ���ݽ��׹���ί����IDɾ������ί����
        /// </summary>
        /// <param name="ConsignQuantumID">���׹���ί����ID</param>
        /// <returns></returns>
        public bool DeleteSingleRQByConsignQuantumID(int ConsignQuantumID)
        {
            return DeleteSingleRQByConsignQuantumID(ConsignQuantumID,null,null);
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_SingleRequestQuantity GetModel(int SingleRequestQuantityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select SingleRequestQuantityID,MaxConsignQuanturm,ConsignQuantumID,ConsignInstructionTypeID from QH_SingleRequestQuantity ");
            strSql.Append(" where SingleRequestQuantityID=@SingleRequestQuantityID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "SingleRequestQuantityID", DbType.Int32, SingleRequestQuantityID);
            ManagementCenter.Model.QH_SingleRequestQuantity model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }

        #region ���ݽ��׹���ί����ID��ȡ����ί����

        /// <summary>
        /// ���ݽ��׹���ί����ID��ȡ����ί����
        /// </summary>
        /// <param name="ConsignQuantumID">���׹���ί����ID</param>
        /// <returns></returns>
        public ManagementCenter.Model.QH_SingleRequestQuantity GetQHSingleRequestQuantityModelByConsignQuantumID(
            int ConsignQuantumID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select SingleRequestQuantityID,MaxConsignQuanturm,ConsignQuantumID,ConsignInstructionTypeID from QH_SingleRequestQuantity ");
            strSql.Append(" where ConsignQuantumID=@ConsignQuantumID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ConsignQuantumID", DbType.Int32, ConsignQuantumID);
            ManagementCenter.Model.QH_SingleRequestQuantity model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }

        #endregion

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SingleRequestQuantityID,MaxConsignQuanturm,ConsignQuantumID,ConsignInstructionTypeID ");
            strSql.Append(" FROM QH_SingleRequestQuantity ");
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
        public List<ManagementCenter.Model.QH_SingleRequestQuantity> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SingleRequestQuantityID,MaxConsignQuanturm,ConsignQuantumID,ConsignInstructionTypeID ");
            strSql.Append(" FROM QH_SingleRequestQuantity ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.QH_SingleRequestQuantity> list =
                new List<ManagementCenter.Model.QH_SingleRequestQuantity>();
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
        public ManagementCenter.Model.QH_SingleRequestQuantity ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.QH_SingleRequestQuantity model =
                new ManagementCenter.Model.QH_SingleRequestQuantity();
            object ojb;
            ojb = dataReader["SingleRequestQuantityID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SingleRequestQuantityID = (int) ojb;
            }
            ojb = dataReader["MaxConsignQuanturm"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MaxConsignQuanturm = (int) ojb;
            }
            ojb = dataReader["ConsignQuantumID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ConsignQuantumID = (int) ojb;
            }
            ojb = dataReader["ConsignInstructionTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ConsignInstructionTypeID = (int) ojb;
            }
            return model;
        }

        #endregion  ��Ա����
    }
}