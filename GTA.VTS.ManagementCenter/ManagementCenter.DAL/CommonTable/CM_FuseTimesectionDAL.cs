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
    ///�������۶�_ʱ��α�ʶ ���ݷ�����CM_FuseTimesection��
    ///���ߣ�����ΰ
    ///����:2008-11-20
    /// </summary>
    public class CM_FuseTimesectionDAL
    {
        public CM_FuseTimesectionDAL()
        {
        }

        #region SQL

        /// <summary>
        /// ������Ʒ�����ȡ�۶�_ʱ��α�ʶ
        /// </summary>
        private string SQL_SELECTBREEDCLASSNAME_CMFUSETIMESECTION =
            @"SELECT A.* FROM CM_FUSETIMESECTION AS A,CM_COMMODITYFUSE AS B 
                                                                  WHERE B.COMMODITYCODE=A.COMMODITYCODE ";

        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(TimesectionID)+1 from CM_FuseTimesection";
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
        public bool Exists(int TimesectionID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CM_FuseTimesection where TimesectionID=@TimesectionID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TimesectionID", DbType.Int32, TimesectionID);
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
        public int Add(ManagementCenter.Model.CM_FuseTimesection model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CM_FuseTimesection(");
            strSql.Append("CommodityCode,StartTime,EndTime)");

            strSql.Append(" values (");
            strSql.Append("@CommodityCode,@StartTime,@EndTime)");
            strSql.Append(";select @@IDENTITY");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, model.CommodityCode);
            db.AddInParameter(dbCommand, "StartTime", DbType.DateTime, model.StartTime);
            db.AddInParameter(dbCommand, "EndTime", DbType.DateTime, model.EndTime);
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
        public bool Update(ManagementCenter.Model.CM_FuseTimesection model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CM_FuseTimesection set ");
            strSql.Append("CommodityCode=@CommodityCode,");
            strSql.Append("StartTime=@StartTime,");
            strSql.Append("EndTime=@EndTime");
            strSql.Append(" where TimesectionID=@TimesectionID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TimesectionID", DbType.Int32, model.TimesectionID);
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, model.CommodityCode);
            db.AddInParameter(dbCommand, "StartTime", DbType.DateTime, model.StartTime);
            db.AddInParameter(dbCommand, "EndTime", DbType.DateTime, model.EndTime);
            db.ExecuteNonQuery(dbCommand);
            return true;
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int TimesectionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete CM_FuseTimesection ");
            strSql.Append(" where TimesectionID=@TimesectionID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TimesectionID", DbType.Int32, TimesectionID);
            db.ExecuteNonQuery(dbCommand);
            return true;
        }

        /// <summary>
        /// ������Ʒ����ɾ���۶�_ʱ��α�ʶ
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool DeleteByCommodityCode(string CommodityCode, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete CM_FuseTimesection ");
            strSql.Append(" where CommodityCode=@CommodityCode ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
            // db.ExecuteNonQuery(dbCommand);
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
        /// ������Ʒ����ɾ���۶�_ʱ��α�ʶ
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <returns></returns>
        public bool DeleteByCommodityCode(string CommodityCode)
        {
            return DeleteByCommodityCode(CommodityCode, null, null);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_FuseTimesection GetModel(int TimesectionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TimesectionID,CommodityCode,StartTime,EndTime from CM_FuseTimesection ");
            strSql.Append(" where TimesectionID=@TimesectionID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TimesectionID", DbType.Int32, TimesectionID);
            ManagementCenter.Model.CM_FuseTimesection model = null;
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
            strSql.Append("select TimesectionID,CommodityCode,StartTime,EndTime ");
            strSql.Append(" FROM CM_FuseTimesection ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        #region ������Ʒ�����ȡ�����۶�_ʱ��α�ʶ

        /// <summary>
        /// ������Ʒ�����ȡ�۶�_ʱ��α�ʶ
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <returns></returns>
        public DataSet GetCMFuseTimesectionByCommodityCode(string CommodityCode)
        {
            if (CommodityCode != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(CommodityCode))
            {
                SQL_SELECTBREEDCLASSNAME_CMFUSETIMESECTION += "AND (A.CommodityCode=@CommodityCode ) ";
            }
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBREEDCLASSNAME_CMFUSETIMESECTION);
            if (CommodityCode != AppGlobalVariable.INIT_STRING && CommodityCode != string.Empty)
            {
                database.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
            }
            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

        #region ��ȡ�����۶�_ʱ��α�ʶ

        ///// <summary>
        ///// ��ȡ�����۶�_ʱ��α�ʶ
        ///// </summary>
        ///// <param name="CommodityCode">��Ʒ����</param>
        ///// <param name="pageNo">��ǰҳ</param>
        ///// <param name="pageSize">��ʾ��¼��</param>
        ///// <param name="rowCount">������</param>
        ///// <returns></returns>
        //public DataSet GetAllCMFuseTimesection(string CommodityCode, int pageNo, int pageSize,
        //                                    out int rowCount)
        //{
        //    //������ѯ
        //    if (CommodityCode != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(CommodityCode))
        //    {
        //        SQL_SELECTBREEDCLASSNAME_CMFUSETIMESECTION += "AND (CommodityCode LIKE  '%' + @CommodityCode + '%') ";
        //    }
        //    Database database = DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBREEDCLASSNAME_CMFUSETIMESECTION);
        //    if (CommodityCode != AppGlobalVariable.INIT_STRING && CommodityCode != string.Empty)
        //    {
        //        database.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
        //    }

        //    return CommPager.QueryPager(database, dbCommand, SQL_SELECTBREEDCLASSNAME_CMFUSETIMESECTION, pageNo, pageSize,
        //                                out rowCount, "CM_FuseTimesection");
        //}

        #endregion

        /// <summary>
        /// ��������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        public List<ManagementCenter.Model.CM_FuseTimesection> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TimesectionID,CommodityCode,StartTime,EndTime ");
            strSql.Append(" FROM CM_FuseTimesection ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.CM_FuseTimesection> list = new List<ManagementCenter.Model.CM_FuseTimesection>();
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
        public ManagementCenter.Model.CM_FuseTimesection ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.CM_FuseTimesection model = new ManagementCenter.Model.CM_FuseTimesection();
            object ojb;
            ojb = dataReader["TimesectionID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TimesectionID = (int) ojb;
            }
            model.CommodityCode = dataReader["CommodityCode"].ToString();
            ojb = dataReader["StartTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.StartTime = (DateTime) ojb;
            }
            ojb = dataReader["EndTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.EndTime = (DateTime) ojb;
            }
            return model;
        }

        #endregion  ��Ա����
    }
}