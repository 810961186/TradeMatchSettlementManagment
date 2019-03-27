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
    ///����������������_�ǽ������� ���ݷ�����CM_NotTradeDate��
    ///���ߣ�����ΰ
    ///����: 2008-11-26
    ///�޸ģ�Ҷ��
    ///ʱ�䣺2010-04-07
    ///��������Ӹ��ݽ��������ͺͷǽ�����ʱ�������Ƿ���ڼ�¼����
    /// </summary>
    public class CM_NotTradeDateDAL
    {
        public CM_NotTradeDateDAL()
        { }
        #region SQL
        /// <summary>
        /// ���ݲ�ѯ�������ؽ���������_�ǽ�����������
        /// </summary>
        private string SQL_SELECT_CMNOTTRADEDATE = @"SELECT B.BOURSETYPENAME,A.* FROM CM_NOTTRADEDATE AS A,CM_BOURSETYPE AS B 
                                                    WHERE A.BOURSETYPEID=B.BOURSETYPEID ";

        /// <summary>
        ///  ���ݽ���������_�ǽ������ڱ��еĽ���������ID��ȡ��������������
        /// </summary>
        private string SQL_SELECTBOURSETYPENAME_CMNOTTRADEDATE = @"SELECT DISTINCT A.BOURSETYPEID,A.BOURSETYPENAME 
                                                                FROM CM_BOURSETYPE A,CM_NOTTRADEDATE B 
                                                                WHERE A.BOURSETYPEID=B.BOURSETYPEID ";
        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(NotTradeDateID)+1 from CM_NotTradeDate";
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
        public bool Exists(int NotTradeDateID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CM_NotTradeDate where NotTradeDateID=@NotTradeDateID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "NotTradeDateID", DbType.Int32, NotTradeDateID);
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
        public int Add(ManagementCenter.Model.CM_NotTradeDate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CM_NotTradeDate(");
            strSql.Append("NotTradeDay,BourseTypeID)");

            strSql.Append(" values (");
            strSql.Append("@NotTradeDay,@BourseTypeID)");
            strSql.Append(";select @@IDENTITY");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            //db.AddInParameter(dbCommand, "NotTradeDateID", DbType.Int32, model.NotTradeDateID);
            db.AddInParameter(dbCommand, "NotTradeDay", DbType.DateTime, model.NotTradeDay);
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, model.BourseTypeID);
            //db.ExecuteNonQuery(dbCommand);
            int result;
            object obj = db.ExecuteScalar(dbCommand);
            if (!int.TryParse(obj.ToString(), out result))
            {
                return AppGlobalVariable.INIT_INT;
            }
            return result;
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(ManagementCenter.Model.CM_NotTradeDate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CM_NotTradeDate set ");
            //strSql.Append("NotTradeDateMonth=@NotTradeDateMonth,");
            strSql.Append("NotTradeDay=@NotTradeDay,");
            strSql.Append("BourseTypeID=@BourseTypeID");
            strSql.Append(" where NotTradeDateID=@NotTradeDateID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "NotTradeDateID", DbType.Int32, model.NotTradeDateID);
            //db.AddInParameter(dbCommand, "NotTradeDateMonth", DbType.Int32, model.NotTradeDateMonth);
            db.AddInParameter(dbCommand, "NotTradeDay", DbType.DateTime, model.NotTradeDay);
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, model.BourseTypeID);
            db.ExecuteNonQuery(dbCommand);
            return true;
        }

        #region ���ݷǽ�����ID��ɾ���ǽ�����
        /// <summary>
        /// ���ݷǽ�����ID��ɾ���ǽ�����
        /// </summary>
        /// <param name="NotTradeDateID">�ǽ�����ID</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        public bool Delete(int NotTradeDateID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete CM_NotTradeDate ");
            strSql.Append(" where NotTradeDateID=@NotTradeDateID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "NotTradeDateID", DbType.Int32, NotTradeDateID);
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

        #region ���ݷǽ�����ID��ɾ���ǽ�����
        /// <summary>
        /// ���ݷǽ�����ID��ɾ���ǽ�����
        /// </summary>
        /// <param name="NotTradeDateID">�ǽ�����ID</param>
        /// <returns></returns>
        public bool Delete(int NotTradeDateID)
        {
            return Delete(NotTradeDateID, null, null);
        }
        #endregion

        #region ���ݽ���������ID��ɾ���ǽ�����
        /// <summary>
        /// ���ݽ���������ID��ɾ���ǽ�����
        /// </summary>
        /// <param name="BourseTypeID">����������ID</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        public bool DeleteByBourseTypeID(int BourseTypeID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete CM_NotTradeDate ");
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

        #region ���ݽ���������ID��ɾ���ǽ�����
        /// <summary>
        /// ���ݽ���������ID��ɾ���ǽ�����
        /// </summary>
        /// <param name="BourseTypeID">����������ID</param>
        /// <returns></returns>
        public bool DeleteByBourseTypeID(int BourseTypeID)
        {
            return DeleteByBourseTypeID(BourseTypeID, null, null);
        }
        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_NotTradeDate GetModel(int NotTradeDateID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select NotTradeDateID,NotTradeDay,BourseTypeID from CM_NotTradeDate ");
            strSql.Append(" where NotTradeDateID=@NotTradeDateID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "NotTradeDateID", DbType.Int32, NotTradeDateID);
            ManagementCenter.Model.CM_NotTradeDate model = null;
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
        ///  ���ݽ��������ͺͷǽ�����ʱ�������Ƿ���ڼ�¼
        /// </summary>
        /// <param name="BourseTypeID">����������</param>
        /// <param name="NotTradeDay">�ǽ�����ʱ��</param>
        /// <returns></returns>
        public ManagementCenter.Model.CM_NotTradeDate GetNotTradeDate(int BourseTypeID, DateTime NotTradeDay)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select NotTradeDateID,NotTradeDay,BourseTypeID from CM_NotTradeDate ");
            strSql.Append(" where BourseTypeID=@BourseTypeID and NotTradeDay=@NotTradeDay");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, BourseTypeID);
            db.AddInParameter(dbCommand, "NotTradeDay", DbType.DateTime, NotTradeDay);
            ManagementCenter.Model.CM_NotTradeDate model = null;
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
            strSql.Append("select NotTradeDateID,NotTradeDay,BourseTypeID ");
            strSql.Append(" FROM CM_NotTradeDate ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        #region ��ý���������_�ǽ������������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// <summary>
        /// ��ý���������_�ǽ������������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        /// <param name="strWhere">����</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_NotTradeDate> GetListArray(string strWhere, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select NotTradeDateID,NotTradeDay,BourseTypeID ");
            strSql.Append(" FROM CM_NotTradeDate ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.CM_NotTradeDate> list = new List<ManagementCenter.Model.CM_NotTradeDate>();
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
        #endregion

        #region ��ý���������_�ǽ������������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// <summary>
        /// ��ý���������_�ǽ������������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        /// <param name="strWhere">����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_NotTradeDate> GetListArray(string strWhere)
        {
            return GetListArray(strWhere, null, null);
        }
        #endregion

        /// <summary>
        /// ��������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        public List<ManagementCenter.Model.CM_NotTradeDate> GetListArrayByBreedClassID(int BreedClassID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.NotTradeDateID,A.NotTradeDay,A.BourseTypeID ");
            strSql.Append(" FROM CM_NotTradeDate as A,CM_BreedClass as B ");
            strSql.Append(" WHERE A.BourseTypeID=B.BourseTypeID ");
            strSql.Append(" AND B.BreedClassID=@BreedClassID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
            List<ManagementCenter.Model.CM_NotTradeDate> list = new List<ManagementCenter.Model.CM_NotTradeDate>();
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderBind(dataReader));
                }
            }
            return list;
        }

        #region ��ȡ���н���������_�ǽ�������
        /// <summary>
        /// ��ȡ���н���������_�ǽ�������
        /// </summary>
        /// <param name="BourseTypeName">��������������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMNotTradeDate(string BourseTypeName, int pageNo, int pageSize,
                                            out int rowCount)
        {
            //������ѯ

            if (BourseTypeName != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(BourseTypeName))
            {
                SQL_SELECT_CMNOTTRADEDATE += "AND (BourseTypeName LIKE  '%' + @BourseTypeName + '%') ";
            }

            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_CMNOTTRADEDATE);

            if (BourseTypeName != AppGlobalVariable.INIT_STRING && BourseTypeName != string.Empty)
            {
                database.AddInParameter(dbCommand, "BourseTypeName", DbType.String, BourseTypeName);
            }

            return CommPager.QueryPager(database, dbCommand, SQL_SELECT_CMNOTTRADEDATE, pageNo, pageSize,
                                        out rowCount, "CM_NotTradeDate");
        }

        #endregion

        #region ���ݽ���������_�ǽ������ڱ��еĽ���������ID��ȡ��������������
        /// <summary>
        ///���ݽ���������_�ǽ������ڱ��еĽ���������ID��ȡ��������������
        /// </summary>
        /// <returns></returns>
        public DataSet GetCMNotTradeDateBourseTypeName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBOURSETYPENAME_CMNOTTRADEDATE);
            return database.ExecuteDataSet(dbCommand);
        }
        #endregion


        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.CM_NotTradeDate ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.CM_NotTradeDate model = new ManagementCenter.Model.CM_NotTradeDate();
            object ojb;
            ojb = dataReader["NotTradeDateID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.NotTradeDateID = (int)ojb;
            }
            ojb = dataReader["NotTradeDay"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.NotTradeDay = (DateTime)ojb;
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

