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
    ///�������ֻ�_Ʒ��_�ǵ���_�������� ���ݷ�����XH_SpotHighLowControlTypeDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class XH_SpotHighLowControlTypeDAL
    {
        public XH_SpotHighLowControlTypeDAL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(BreedClassHighLowID)+1 from XH_SpotHighLowControlType";
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
        public bool Exists(int BreedClassHighLowID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select count(1) from XH_SpotHighLowControlType where BreedClassHighLowID=@BreedClassHighLowID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassHighLowID", DbType.Int32, BreedClassHighLowID);
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
        /// �����ǵ�����������
        /// </summary>
        public int Add(ManagementCenter.Model.XH_SpotHighLowControlType model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_SpotHighLowControlType(");
            strSql.Append("HighLowTypeID)");

            strSql.Append(" values (");
            strSql.Append("@HighLowTypeID)");
            strSql.Append(";select @@IDENTITY");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "HighLowTypeID", DbType.Int32, model.HighLowTypeID);
            //int result;
            //object obj = db.ExecuteScalar(dbCommand);
            //if (!int.TryParse(obj.ToString(), out result))
            //{
            //    return 0;
            //}

            int result;
            object obj;
            if (tran == null)
            {
                obj = db.ExecuteScalar(dbCommand);
            }
            else
            {
                obj = db.ExecuteScalar(dbCommand, tran);
            }
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// �����ǵ�����������(����,������)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.XH_SpotHighLowControlType model)
        {
            return Add(model, null, null);
        }

        #region  �����ǵ�����������

        /// <summary>
        /// �����ǵ�����������
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.XH_SpotHighLowControlType model, DbTransaction tran,
                           Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_SpotHighLowControlType set ");
            strSql.Append("HighLowTypeID=@HighLowTypeID");
            strSql.Append(" where BreedClassHighLowID=@BreedClassHighLowID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "HighLowTypeID", DbType.Int32, model.HighLowTypeID);
            db.AddInParameter(dbCommand, "BreedClassHighLowID", DbType.Int32, model.BreedClassHighLowID);
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

        #region  �����ǵ�����������

        /// <summary>
        /// �����ǵ�����������
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.XH_SpotHighLowControlType model)
        {
            return Update(model, null, null);
        }

        #endregion

        #region ����Ʒ���ǵ�����ʶɾ���ǵ�����������

        /// <summary>
        /// ����Ʒ���ǵ�����ʶɾ���ǵ�����������
        /// </summary>
        /// <param name="BreedClassHighLowID">Ʒ���ǵ�����ʶ</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Delete(int BreedClassHighLowID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete XH_SpotHighLowControlType ");
            strSql.Append(" where BreedClassHighLowID=@BreedClassHighLowID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassHighLowID", DbType.Int32, BreedClassHighLowID);
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

        #region ����Ʒ���ǵ�����ʶɾ���ǵ�����������(������)

        /// <summary>
        /// ����Ʒ���ǵ�����ʶɾ���ǵ�����������
        /// </summary>
        /// <param name="BreedClassHighLowID">Ʒ���ǵ�����ʶ</param>
        /// <returns></returns>
        public bool Delete(int BreedClassHighLowID)
        {
            return Delete(BreedClassHighLowID, null, null);
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_SpotHighLowControlType GetModel(int BreedClassHighLowID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select HighLowTypeID,BreedClassHighLowID from XH_SpotHighLowControlType ");
            strSql.Append(" where BreedClassHighLowID=@BreedClassHighLowID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassHighLowID", DbType.Int32, BreedClassHighLowID);
            ManagementCenter.Model.XH_SpotHighLowControlType model = null;
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
            strSql.Append("select HighLowTypeID,BreedClassHighLowID ");
            strSql.Append(" FROM XH_SpotHighLowControlType ");
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
            db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "XH_SpotHighLowControlType");
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
        public List<ManagementCenter.Model.XH_SpotHighLowControlType> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select HighLowTypeID,BreedClassHighLowID ");
            strSql.Append(" FROM XH_SpotHighLowControlType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.XH_SpotHighLowControlType> list =
                new List<ManagementCenter.Model.XH_SpotHighLowControlType>();
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
        public ManagementCenter.Model.XH_SpotHighLowControlType ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.XH_SpotHighLowControlType model =
                new ManagementCenter.Model.XH_SpotHighLowControlType();
            object ojb;
            ojb = dataReader["HighLowTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HighLowTypeID = (int) ojb;
            }
            ojb = dataReader["BreedClassHighLowID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassHighLowID = (int) ojb;
            }
            return model;
        }

        #endregion  ��Ա����
    }
}