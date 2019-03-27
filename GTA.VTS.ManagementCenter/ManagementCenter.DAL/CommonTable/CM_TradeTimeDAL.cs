using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using ManagementCenter.Model.CommonClass;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Maticsoft.DBUtility;//�����������
namespace ManagementCenter.DAL
{
    /// <summary>
    ///����������������_����ʱ�� ���ݷ�����CM_TradeTime��
    ///���ߣ�����ΰ
    ///����:2008-11-20
    /// </summary>
    public class CM_TradeTimeDAL
    {
        public CM_TradeTimeDAL()
        { }

        #region SQL

        /// <summary>
        /// ���ݲ�ѯ�������ؽ���������_����ʱ��
        /// </summary>
        private string SQL_SELECT_CMTRADETIME = @"SELECT B.BOURSETYPENAME,A.* 
                                                FROM CM_TRADETIME AS A,CM_BOURSETYPE AS B 
                                                WHERE A.BOURSETYPEID=B.BOURSETYPEID ";

        /// <summary>
        ///  ���ݽ���������_����ʱ����еĽ���������ID��ȡ��������������
        /// </summary>
        private string SQL_SELECTBOURSETYPENAME_CMTRADETIME = @"SELECT A.BOURSETYPEID,A.BOURSETYPENAME 
                                                            FROM CM_BOURSETYPE A,CM_TRADETIME B 
                                                            WHERE A.BOURSETYPEID=B.BOURSETYPEID ";


        /// <summary>
        /// ���ݽ���������ID���ؽ���ʱ��
        /// </summary>
        private string SQL_SELECTCMTRADETIME_BYBOURSETYPEID =
            @"SELECT A.TRADETIMEID,A.STARTTIME,A.ENDTIME,A.BOURSETYPEID,B.BOURSETYPENAME 
              FROM CM_TRADETIME AS A,CM_BOURSETYPE AS B
              WHERE B.BOURSETYPEID=A.BOURSETYPEID AND B.BOURSETYPEID=@BOURSETYPEID";

        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(TradeTimeID)+1 from CM_TradeTime";
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
        public bool Exists(int TradeTimeID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CM_TradeTime where TradeTimeID=@TradeTimeID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeTimeID", DbType.Int32, TradeTimeID);
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
        /// ��ӽ���ʱ��
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.CM_TradeTime model)
        {
            return Add(model, null, null);
        }

        /// <summary>
        /// ��ӽ���ʱ��
        /// </summary>
        /// <param name="model">����ʱ��ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.CM_TradeTime model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CM_TradeTime(");
            strSql.Append("StartTime,EndTime,BourseTypeID)");

            strSql.Append(" values (");
            strSql.Append("@StartTime,@EndTime,@BourseTypeID)");
            strSql.Append(";select @@IDENTITY");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "StartTime", DbType.DateTime, model.StartTime);
            db.AddInParameter(dbCommand, "EndTime", DbType.DateTime, model.EndTime);
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, model.BourseTypeID);
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
                return AppGlobalVariable.INIT_INT;
            }
            return result;
        }


        /// <summary>
        /// ���½���ʱ��
        /// </summary>
        /// <param name="model">����ʱ��ʵ��</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.CM_TradeTime model)
        {
            return Update(model, null, null);
        }

        /// <summary>
        /// ���½���ʱ��
        /// </summary>
        /// <param name="model">����ʱ��ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.CM_TradeTime model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CM_TradeTime set ");
            strSql.Append("StartTime=@StartTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("BourseTypeID=@BourseTypeID");
            strSql.Append(" where TradeTimeID=@TradeTimeID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeTimeID", DbType.Int32, model.TradeTimeID);
            db.AddInParameter(dbCommand, "StartTime", DbType.DateTime, model.StartTime);
            db.AddInParameter(dbCommand, "EndTime", DbType.DateTime, model.EndTime);
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, model.BourseTypeID);
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

        /// <summary>
        /// ���ݽ���ʱ��ID��ɾ������ʱ��
        /// </summary>
        /// <param name="TradeTimeID">����ʱ��ID</param>
        /// <returns></returns>
        public bool Delete(int TradeTimeID)
        {
            return Delete(TradeTimeID, null, null);
        }

        /// <summary>
        ///  ���ݽ���ʱ��ID��ɾ������ʱ��
        /// </summary>
        /// <param name="TradeTimeID">����ʱ��ID</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Delete(int TradeTimeID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete CM_TradeTime ");
            strSql.Append(" where TradeTimeID=@TradeTimeID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeTimeID", DbType.Int32, TradeTimeID);
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

        #region ���ݽ���������ID��ɾ������ʱ��
        /// <summary>
        ///  ���ݽ���������ID��ɾ������ʱ��
        /// </summary>
        /// <param name="BourseTypeID">����������ID</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        public bool DeleteCMTradeTimeByBourseTypeID(int BourseTypeID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete CM_TradeTime ");
            strSql.Append(" where BourseTypeID=@BourseTypeID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, BourseTypeID);
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

        #region ���ݽ���������ID��ɾ������ʱ��(������)
        /// <summary>
        ///  ���ݽ���������ID��ɾ������ʱ��(������)
        /// </summary>
        /// <param name="BourseTypeID">����������ID</param>
        /// <returns></returns>
        public bool DeleteCMTradeTimeByBourseTypeID(int BourseTypeID)
        {
            return DeleteCMTradeTimeByBourseTypeID(BourseTypeID, null, null);
        }
        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_TradeTime GetModel(int TradeTimeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TradeTimeID,StartTime,EndTime,BourseTypeID from CM_TradeTime ");
            strSql.Append(" where TradeTimeID=@TradeTimeID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeTimeID", DbType.Int32, TradeTimeID);
            ManagementCenter.Model.CM_TradeTime model = null;
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
            strSql.Append("select TradeTimeID,StartTime,EndTime,BourseTypeID ");
            strSql.Append(" FROM CM_TradeTime ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// ���ݽ���������ID���ؽ���ʱ��
        /// </summary>
        /// <param name="bourseTypeID">����������ID</param>
        /// <returns></returns>
        public DataSet GetTradeTimeAndBourseTypeList(int bourseTypeID)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTCMTRADETIME_BYBOURSETYPEID);
            database.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, bourseTypeID);
            return database.ExecuteDataSet(dbCommand);
        }

        /// <summary>
        /// ��������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        public List<ManagementCenter.Model.CM_TradeTime> GetListArray(string strWhere, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TradeTimeID,StartTime,EndTime,BourseTypeID ");
            strSql.Append(" FROM CM_TradeTime ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.CM_TradeTime> list = new List<ManagementCenter.Model.CM_TradeTime>();
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            if (tran == null)
            {
                using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, strSql.ToString()))
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind(dataReader));
                    }
                }
            }
            else
            {
                using (IDataReader dataReader = db.ExecuteReader(tran, CommandType.Text, strSql.ToString()))
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind(dataReader));
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// ��������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        public List<ManagementCenter.Model.CM_TradeTime> GetListArray(string strWhere)
        {
            return GetListArray(strWhere, null, null);
        }


        #region ��ȡ���н���������_����ʱ��

        /// <summary>
        /// ��ȡ���н���������_����ʱ��
        /// </summary>
        /// <param name="BourseTypeName">��������������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMTradeTime(string BourseTypeName, int pageNo, int pageSize,
                                            out int rowCount)
        {
            //������ѯ
            if (BourseTypeName != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(BourseTypeName))
            {
                SQL_SELECT_CMTRADETIME += "AND (BourseTypeName LIKE  '%' + @BourseTypeName + '%') ";
            }

            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_CMTRADETIME);

            if (BourseTypeName != AppGlobalVariable.INIT_STRING && BourseTypeName != string.Empty)
            {
                database.AddInParameter(dbCommand, "BourseTypeName", DbType.String, BourseTypeName);
            }
            return CommPager.QueryPager(database, dbCommand, SQL_SELECT_CMTRADETIME, pageNo, pageSize,
                                        out rowCount, "CM_TradeTime");
        }

        #endregion

        #region  ���ݽ���������_����ʱ����еĽ���������ID��ȡ��������������
        /// <summary>
        /// ���ݽ���������_����ʱ����еĽ���������ID��ȡ��������������
        /// </summary>
        /// <returns></returns>
        public DataSet GetBourseTypeNameByBourseTypeID()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBOURSETYPENAME_CMTRADETIME);
            return database.ExecuteDataSet(dbCommand);
        }
        #endregion

        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.CM_TradeTime ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.CM_TradeTime model = new ManagementCenter.Model.CM_TradeTime();
            object ojb;
            ojb = dataReader["TradeTimeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradeTimeID = (int)ojb;
            }
            ojb = dataReader["StartTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.StartTime = (DateTime)ojb;
            }
            ojb = dataReader["EndTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.EndTime = (DateTime)ojb;
            }
            ojb = dataReader["BourseTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BourseTypeID = (int)ojb;
            }
            return model;
        }

        #endregion  ��Ա����
    }
}

