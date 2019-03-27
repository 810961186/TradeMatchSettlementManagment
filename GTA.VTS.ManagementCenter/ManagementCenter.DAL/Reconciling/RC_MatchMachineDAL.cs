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
    /// ��������ϻ��� ���ݷ�����RC_MatchMachineDAL��
    /// ���ߣ������� �޸ģ�����ΰ
    /// ���ڣ�2008-11-18 2009-10-28
    /// </summary>
    public class RC_MatchMachineDAL
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public RC_MatchMachineDAL()
        {
        }
        #endregion

        #region SQL

        /// <summary>
        ///  ���ݴ������_��ϻ����еĽ���������ID��ȡ��������������
        /// </summary>
        private string SQL_SELECTBOURSETYPENAME_RCMATCHMACHINE =
            @"SELECT A.BOURSETYPEID,A.BOURSETYPENAME 
                                                                FROM CM_BOURSETYPE A,RC_MatchMachine B 
                                                                WHERE A.BOURSETYPEID=B.BOURSETYPEID ";

        /// <summary>
        ///  ���ݴ������_��ϻ����еĴ������ID��ȡ�����������
        /// </summary>
        private string SQL_SELECTMATCHCENTERNAME_RCMATCHMACHINE =
            @"SELECT A.MATCHCENTERID,A.MATCHCENTERNAME 
                                                                    FROM RC_MATCHCENTER A,RC_MATCHMACHINE B 
                                                                    WHERE A.MATCHCENTERID=B.MATCHCENTERID ";

        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(MatchMachineID)+1 from RC_MatchMachine";
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
        public bool Exists(int MatchMachineID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from RC_MatchMachine where MatchMachineID=@MatchMachineID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "MatchMachineID", DbType.Int32, MatchMachineID);
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
        public int Add(ManagementCenter.Model.RC_MatchMachine model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into RC_MatchMachine(");
            strSql.Append("MatchMachineName,BourseTypeID,MatchCenterID)");

            strSql.Append(" values (");
            strSql.Append("@MatchMachineName,@BourseTypeID,@MatchCenterID)");
            strSql.Append(";select @@IDENTITY");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "MatchMachineName", DbType.String, model.MatchMachineName);
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, model.BourseTypeID);
            db.AddInParameter(dbCommand, "MatchCenterID", DbType.Int32, model.MatchCenterID);
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
        /// ��Ӵ�ϻ�
        /// </summary>
        /// <param name="model">��ϻ�ʵ��</param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.RC_MatchMachine model)
        {
            return Add(model, null, null);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.RC_MatchMachine model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update RC_MatchMachine set ");
            strSql.Append("MatchMachineName=@MatchMachineName,");
            strSql.Append("BourseTypeID=@BourseTypeID,");
            strSql.Append("MatchCenterID=@MatchCenterID");
            strSql.Append(" where MatchMachineID=@MatchMachineID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "MatchMachineID", DbType.Int32, model.MatchMachineID);
            db.AddInParameter(dbCommand, "MatchMachineName", DbType.String, model.MatchMachineName);
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, model.BourseTypeID);
            db.AddInParameter(dbCommand, "MatchCenterID", DbType.Int32, model.MatchCenterID);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int MatchMachineID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete RC_MatchMachine ");
            strSql.Append(" where MatchMachineID=@MatchMachineID ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "MatchMachineID", DbType.Int32, MatchMachineID);
            if (tran == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
        }

        /// <summary>
        /// ɾ����������
        /// </summary>
        public void DeleteAll(DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete RC_MatchMachine ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            if (tran == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
        }

        #region ���ݽ���������ID��ɾ����ϻ�
        /// <summary>
        /// ���ݽ���������ID��ɾ����ϻ�
        /// </summary>
        /// <param name="BourseTypeID">����������ID</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        public bool DeleteByBourseTypeID(int BourseTypeID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete RC_MatchMachine ");
            strSql.Append(" where BourseTypeID=@BourseTypeID ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
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

        #region ���ݽ���������ID��ɾ����ϻ�
        /// <summary>
        /// ���ݽ���������ID��ɾ����ϻ�
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
        public ManagementCenter.Model.RC_MatchMachine GetModel(int MatchMachineID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MatchMachineID,MatchMachineName,BourseTypeID,MatchCenterID from RC_MatchMachine ");
            strSql.Append(" where MatchMachineID=@MatchMachineID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "MatchMachineID", DbType.Int32, MatchMachineID);
            ManagementCenter.Model.RC_MatchMachine model = null;
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
            strSql.Append("select MatchMachineID,MatchMachineName,BourseTypeID,MatchCenterID ");
            strSql.Append(" FROM RC_MatchMachine ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        #region ��ô�ϻ������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// <summary>
        /// ��ô�ϻ������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.RC_MatchMachine> GetListArray(string strWhere, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MatchMachineID,MatchMachineName,BourseTypeID,MatchCenterID ");
            strSql.Append(" FROM RC_MatchMachine ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.RC_MatchMachine> list = new List<ManagementCenter.Model.RC_MatchMachine>();
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

        #region ��ô�ϻ������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// <summary>
        /// ��ô�ϻ������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.RC_MatchMachine> GetListArray(string strWhere)
        {
            return GetListArray(strWhere, null, null);
        }
        #endregion

        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.RC_MatchMachine ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.RC_MatchMachine model = new ManagementCenter.Model.RC_MatchMachine();
            object ojb;
            ojb = dataReader["MatchMachineID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MatchMachineID = (int)ojb;
            }
            model.MatchMachineName = dataReader["MatchMachineName"].ToString();
            ojb = dataReader["BourseTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BourseTypeID = (int)ojb;
            }
            ojb = dataReader["MatchCenterID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MatchCenterID = (int)ojb;
            }
            return model;
        }

        #region ��ϻ���ҳ��ѯ
        /// <summary>
        /// ��ϻ���ҳ��ѯ
        /// </summary>
        /// <param name="machineQueryEntity">��ϻ�ʵ��</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>

        public DataSet GetPagingMachine(ManagementCenter.Model.RC_MatchMachine machineQueryEntity, int pageNo,
                                        int pageSize,
                                        out int rowCount)
        {
            string SQL_SELECT_Machine =
                @" select MatchMachineID,MatchMachineName,BourseTypeID,MatchCenterID FROM RC_MatchMachine where 1=1 ";

            if (machineQueryEntity.BourseTypeID != int.MaxValue)
            {
                SQL_SELECT_Machine += "AND BourseTypeID=@BourseTypeID ";
            }
            if (machineQueryEntity.MatchCenterID != int.MaxValue)
            {
                SQL_SELECT_Machine += "AND MatchCenterID=@MatchCenterID ";
            }
            if (machineQueryEntity.MatchMachineID != int.MaxValue)
            {
                SQL_SELECT_Machine += "AND MatchMachineID=@MatchMachineID ";
            }
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_Machine);

            if (machineQueryEntity.BourseTypeID != int.MaxValue)
            {
                database.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, machineQueryEntity.BourseTypeID);
            }
            if (machineQueryEntity.MatchCenterID != int.MaxValue)
            {
                database.AddInParameter(dbCommand, "MatchCenterID", DbType.Int32, machineQueryEntity.MatchCenterID);
            }
            if (machineQueryEntity.MatchMachineID != int.MaxValue)
            {
                database.AddInParameter(dbCommand, "MatchMachineID", DbType.Int32, machineQueryEntity.MatchMachineID);
            }

            return CommPager.QueryPager(database, dbCommand, SQL_SELECT_Machine, pageNo, pageSize, out rowCount,
                                        "UM_Machine");
        }

        #endregion

        #region  ���ݴ������_��ϻ����еĽ���������ID��ȡ��������������

        /// <summary>
        /// ���ݴ������_��ϻ����еĽ���������ID��ȡ��������������
        /// </summary>
        /// <returns></returns>
        public DataSet GetRCMatchMachineBourseTypeName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBOURSETYPENAME_RCMATCHMACHINE);
            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

        #region  ���ݴ������_��ϻ����еĴ������ID��ȡ�����������

        /// <summary>
        /// ���ݴ������_��ϻ����еĴ������ID��ȡ�����������
        /// </summary>
        /// <returns></returns>
        public DataSet GetRCMatchMachineMatchCenterName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTMATCHCENTERNAME_RCMATCHMACHINE);
            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

        #endregion  ��Ա����
    }
}