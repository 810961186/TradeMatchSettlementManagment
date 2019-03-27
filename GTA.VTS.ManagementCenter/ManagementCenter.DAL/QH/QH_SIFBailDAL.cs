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
    ///������Ʒ��_��ָ�ڻ�_��֤�� ���ݷ�����QH_SIFBailDAL��
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    public class QH_SIFBailDAL
    {
        public QH_SIFBailDAL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(BreedClassID)+1 from QH_SIFBail";
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
        public bool Exists(int BreedClassID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from QH_SIFBail where BreedClassID=@BreedClassID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
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
        /// ���Ʒ��_��ָ�ڻ�_��֤��
        /// </summary>
        /// <param name="model">Ʒ��_��ָ�ڻ�_��֤��ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Add(ManagementCenter.Model.QH_SIFBail model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QH_SIFBail(");
            strSql.Append("BailScale,BreedClassID)");

            strSql.Append(" values (");
            strSql.Append("@BailScale,@BreedClassID)");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            if (model.BailScale != AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "BailScale", DbType.Decimal, model.BailScale);
            }
            else
            {
                db.AddInParameter(dbCommand, "BailScale", DbType.Decimal, DBNull.Value);
            }
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
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

        /// <summary>
        ///  ���Ʒ��_��ָ�ڻ�_��֤��
        /// </summary>
        /// <param name="model"> Ʒ��_��ָ�ڻ�_��֤��ʵ��</param>
        /// <returns></returns>
        public bool Add(ManagementCenter.Model.QH_SIFBail model)
        {
            return Add(model, null, null);
        }


        /// <summary>
        /// ����Ʒ��_��ָ�ڻ�_��֤��
        /// </summary>
        /// <param name="model">Ʒ��_��ָ�ڻ�_��֤��ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.QH_SIFBail model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QH_SIFBail set ");
            strSql.Append("BailScale=@BailScale");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            if (model.BailScale != AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "BailScale", DbType.Decimal, model.BailScale);
            }
            else
            {
                db.AddInParameter(dbCommand, "BailScale", DbType.Decimal, DBNull.Value);
            } db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
            //	db.ExecuteNonQuery(dbCommand);
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

        /// <summary>
        /// ����Ʒ��_��ָ�ڻ�_��֤��
        /// </summary>
        /// <param name="model">Ʒ��_��ָ�ڻ�_��֤��ʵ��</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.QH_SIFBail model)
        {
            return Update(model, null, null);
        }

        /// <summary>
        /// ɾ��Ʒ��_��ָ�ڻ�_��֤��
        /// </summary>
        /// <param name="BreedClassID">Ʒ�ֱ�ʶ(Ʒ��_��ָ�ڻ�_��֤��)</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Delete(int BreedClassID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete QH_SIFBail ");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
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

        /// <summary>
        /// ɾ��Ʒ��_��ָ�ڻ�_��֤��
        /// </summary>
        /// <param name="BreedClassID">Ʒ�ֱ�ʶ(Ʒ��_��ָ�ڻ�_��֤��)</param>
        /// <returns></returns>
        public bool Delete(int BreedClassID)
        {
            return Delete(BreedClassID, null, null);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_SIFBail GetModel(int BreedClassID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BailScale,BreedClassID from QH_SIFBail ");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
            ManagementCenter.Model.QH_SIFBail model = null;
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
            strSql.Append("select BailScale,BreedClassID ");
            strSql.Append(" FROM QH_SIFBail ");
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
        public List<ManagementCenter.Model.QH_SIFBail> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BailScale,BreedClassID ");
            strSql.Append(" FROM QH_SIFBail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.QH_SIFBail> list = new List<ManagementCenter.Model.QH_SIFBail>();
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
        public ManagementCenter.Model.QH_SIFBail ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.QH_SIFBail model = new ManagementCenter.Model.QH_SIFBail();
            object ojb;
            ojb = dataReader["BailScale"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BailScale = (decimal) ojb;
            }
            ojb = dataReader["BreedClassID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassID = (int) ojb;
            }
            return model;
        }

        #endregion  ��Ա����
    }
}