using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using ManagementCenter.Model;
using ManagementCenter.Model.CommonClass;
using Microsoft.Practices.EnterpriseLibrary.Data;

//�����������

namespace ManagementCenter.DAL
{
    /// <summary>
    ///������������ƷƷ�� ���ݷ�����CM_BreedClassDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-20
    /// </summary>
    public class CM_BreedClassDAL
    {
        #region SQL

        /// <summary>
        /// ��ȡ�ֻ�Ʒ������
        /// </summary>
        private string SQL_SELECT_CMBREEDCLASS =
            @"SELECT BREEDCLASSID,BREEDCLASSNAME 
                                FROM CM_BREEDCLASS 
                                WHERE BREEDCLASSTYPEID=1 AND DELETESTATE IS NOT NULL AND DELETESTATE<>1 ";


        /// <summary>
        /// ���ݲ�ѯ������ȡ������ƷƷ�� 
        /// </summary>
        private string SQL_SELECTALL_CMBREEDCLASS = @"SELECT * FROM CM_BREEDCLASS WHERE DELETESTATE IS NOT NULL AND DELETESTATE<>1 ";//1=1

        /// <summary>
        ///  ���ݽ�����ƷƷ�ֱ��еĽ���������ID��ȡ��������������
        /// </summary>
        private string SQL_SELECTBOURSETYPENAME_CMBREEDCLASS =
            @"SELECT A.BOURSETYPEID,A.BOURSETYPENAME 
                                                            FROM CM_BOURSETYPE A,CM_BREEDCLASS B 
                                                            WHERE A.BOURSETYPEID=B.BOURSETYPEID ";

        /// <summary>
        /// ��ȡ(���۹�Ʒ�����)������ƷƷ�ֱ��е�Ʒ������
        /// </summary>
        private string SQL_SELECTBREEDCLASSNAME_CM_BREEDCLASS = @"SELECT BREEDCLASSID,BREEDCLASSNAME FROM CM_BREEDCLASS WHERE BREEDCLASSTYPEID<>4 ";

        /// <summary>
        /// ��ȡƷ����������Ʒ�ڻ����ָ�ڻ���Ʒ������
        /// </summary>
        private string SQL_SELECTBREEDCLASSNAMEQHFUTURECOSTS_CMBREEDCLASS =
            @"SELECT BREEDCLASSID,BREEDCLASSNAME 
                                                    FROM CM_BREEDCLASS 
                                                    WHERE BREEDCLASSTYPEID<>1 AND BREEDCLASSTYPEID<>4 ";

        /// <summary>
        /// ��ȡƷ����������Ʒ�ڻ���Ʒ������
        /// </summary>
        private string SQL_SELECTBREEDCLASSNAMESPQH_CMBREEDCLASS =
            @"SELECT BREEDCLASSID,BREEDCLASSNAME 
                                                    FROM CM_BREEDCLASS 
                                                    WHERE BREEDCLASSTYPEID=2 ";

        /// <summary>
        ///��ȡƷ�������ǹ�ָ�ڻ���Ʒ������
        /// </summary>
        private string SQL_SELECTQHSIF_CMBREEDCLASS =
            @"SELECT BREEDCLASSID,BREEDCLASSNAME 
                                                        FROM CM_BREEDCLASS 
                                                        WHERE BREEDCLASSTYPEID=3 ";

        /// <summary>
        /// ��ȡ�ֻ���ͨ�͸۹�Ʒ������
        /// </summary>
        private string SQL_SELECT_XHANDHKCMBREEDCLASS =
            @"SELECT BREEDCLASSID,BREEDCLASSNAME 
                                FROM CM_BREEDCLASS 
                                WHERE BREEDCLASSTYPEID=1 OR BREEDCLASSTYPEID=4 AND DELETESTATE IS NOT NULL AND DELETESTATE<>1 ";


        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(BreedClassID)+1 from CM_BreedClass";
            Database db = DatabaseFactory.CreateDatabase();
            object obj = db.ExecuteScalar(CommandType.Text, strsql);
            if (obj != null && obj != DBNull.Value)
            {
                return int.Parse(obj.ToString());
            }
            return AppGlobalVariable.INIT_INT;//1;
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int BreedClassID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            var strSql = new StringBuilder();
            strSql.Append("select count(1) from CM_BreedClass where BreedClassID=@BreedClassID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
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


        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Add(CM_BreedClass model)
        {
            var strSql = new StringBuilder();
            strSql.Append("SET IDENTITY_INSERT [CM_BreedClass] ON; ");//�ر��Զ�������(��ΪϵͳĬ��ǰ1500ID����ϵͳID)
            strSql.Append("insert into CM_BreedClass(");
            //strSql.Append("BreedClassName,AccountTypeIDFund,BreedClassTypeID,AccountTypeIDHold,BourseTypeID,ISSysDefaultBreed,ISHKBreedClassType,DeleteState)");
            strSql.Append("BreedClassID,BreedClassName,AccountTypeIDFund,BreedClassTypeID,AccountTypeIDHold,BourseTypeID,ISSysDefaultBreed,ISHKBreedClassType,DeleteState)");
            strSql.Append(" values (");
            strSql.Append("@BreedClassID,@BreedClassName,@AccountTypeIDFund,@BreedClassTypeID,@AccountTypeIDHold,@BourseTypeID,@ISSysDefaultBreed,@ISHKBreedClassType,@DeleteState)");
            //strSql.Append(";select @@IDENTITY");
            strSql.Append(";SET IDENTITY_INSERT [CM_BreedClass] OFF");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
            db.AddInParameter(dbCommand, "BreedClassName", DbType.String, model.BreedClassName);
            db.AddInParameter(dbCommand, "AccountTypeIDFund", DbType.Int32, model.AccountTypeIDFund);
            db.AddInParameter(dbCommand, "BreedClassTypeID", DbType.Int32, model.BreedClassTypeID);
            db.AddInParameter(dbCommand, "AccountTypeIDHold", DbType.Int32, model.AccountTypeIDHold);
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, model.BourseTypeID);
            db.AddInParameter(dbCommand, "ISSysDefaultBreed", DbType.Int32, model.ISSysDefaultBreed);
            db.AddInParameter(dbCommand, "ISHKBreedClassType", DbType.Int32, model.ISHKBreedClassType);
            model.DeleteState = (int)Types.IsYesOrNo.No;//���ʱĬ��״̬Ϊ��ɾ��
            db.AddInParameter(dbCommand, "DeleteState", DbType.Int32, model.DeleteState);
            //int result;
            //object obj = db.ExecuteScalar(dbCommand);
            //if (!int.TryParse(obj.ToString(), out result))
            //{
            //    return AppGlobalVariable.INIT_INT;
            //}
            //return result;
            db.ExecuteScalar(dbCommand);
            return true;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(CM_BreedClass model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update CM_BreedClass set ");
            strSql.Append("BreedClassName=@BreedClassName,");
            strSql.Append("AccountTypeIDFund=@AccountTypeIDFund,");
            strSql.Append("BreedClassTypeID=@BreedClassTypeID,");
            strSql.Append("AccountTypeIDHold=@AccountTypeIDHold,");
            strSql.Append("BourseTypeID=@BourseTypeID");
            // strSql.Append("UnitID=@UnitID");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
            db.AddInParameter(dbCommand, "BreedClassName", DbType.String, model.BreedClassName);
            db.AddInParameter(dbCommand, "AccountTypeIDFund", DbType.Int32, model.AccountTypeIDFund);
            db.AddInParameter(dbCommand, "BreedClassTypeID", DbType.Int32, model.BreedClassTypeID);
            db.AddInParameter(dbCommand, "AccountTypeIDHold", DbType.Int32, model.AccountTypeIDHold);
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, model.BourseTypeID);
            // db.AddInParameter(dbCommand,"UnitID",DbType.Int32,model.UnitID);
            db.ExecuteNonQuery(dbCommand);
            return true;
        }

        #region  ���ݽ���������ID�����½�����ƷƷ���еĽ���������ID
        /// <summary>
        /// ���ݽ���������ID�����½�����ƷƷ���еĽ���������ID
        /// </summary>
        /// <param name="OldBourseTypeID">�޸�ǰ�Ľ���������ID</param>
        /// <param name="NewBourseTypeID">�޸ĺ�Ľ���������ID</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        public bool UpdateBourseTypeID(int OldBourseTypeID, int NewBourseTypeID, DbTransaction tran, Database db)
        {
            var strSql = new StringBuilder();
            strSql.Append("update CM_BreedClass set ");
            strSql.Append("BourseTypeID=@NewBourseTypeID");
            strSql.Append(" where BourseTypeID=@OldBourseTypeID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OldBourseTypeID", DbType.Int32, OldBourseTypeID);
            db.AddInParameter(dbCommand, "NewBourseTypeID", DbType.Int32, NewBourseTypeID);
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

        #region  ���ݽ���������ID�����½�����ƷƷ���еĽ���������ID
        /// <summary>
        /// ���ݽ���������ID�����½�����ƷƷ���еĽ���������ID
        /// </summary>
        /// <param name="OldBourseTypeID">�޸�ǰ�Ľ���������ID</param>
        /// <param name="NewBourseTypeID">�޸ĺ�Ľ���������ID</param>
        /// <returns></returns>
        public bool UpdateBourseTypeID(int OldBourseTypeID, int NewBourseTypeID)
        {
            return UpdateBourseTypeID(OldBourseTypeID, NewBourseTypeID, null, null);
        }
        #endregion

        /// <summary>
        /// ����Ʒ��ID��ɾ��Ʒ��
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Delete(int BreedClassID, DbTransaction tran, Database db)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete CM_BreedClass ");
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
        ///  ����Ʒ��ID��ɾ��Ʒ��
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool Delete(int BreedClassID)
        {
            return Delete(BreedClassID, null, null);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public CM_BreedClass GetModel(int BreedClassID)
        {
            var strSql = new StringBuilder();
            strSql.Append(
                "select BreedClassID,BreedClassName,AccountTypeIDFund,BreedClassTypeID,AccountTypeIDHold,BourseTypeID,ISSysDefaultBreed,ISHKBreedClassType,DeleteState from CM_BreedClass ");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
            CM_BreedClass model = null;
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
            var strSql = new StringBuilder();
            strSql.Append(
                "select BreedClassID,BreedClassName,AccountTypeIDFund,BreedClassTypeID,AccountTypeIDHold,BourseTypeID,ISSysDefaultBreed,ISHKBreedClassType,DeleteState");
            strSql.Append(" FROM CM_BreedClass ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        #region  ��ý�����ƷƷ�������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// <summary>
        ///  ��ý�����ƷƷ�������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        public List<CM_BreedClass> GetListArray(string strWhere, DbTransaction tran, Database db)
        {
            var strSql = new StringBuilder();
            strSql.Append(
                "select BreedClassID,BreedClassName,AccountTypeIDFund,BreedClassTypeID,AccountTypeIDHold,BourseTypeID,ISSysDefaultBreed,ISHKBreedClassType,DeleteState");
            strSql.Append(" FROM CM_BreedClass ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var list = new List<CM_BreedClass>();
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

        #region  ��ý�����ƷƷ�������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// <summary>
        ///  ��ý�����ƷƷ�������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<CM_BreedClass> GetListArray(string strWhere)
        {
            return GetListArray(strWhere, null, null);
        }
        #endregion

        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public CM_BreedClass ReaderBind(IDataReader dataReader)
        {
            var model = new CM_BreedClass();
            object ojb;
            ojb = dataReader["BreedClassID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassID = (int)ojb;
            }
            model.BreedClassName = dataReader["BreedClassName"].ToString();
            ojb = dataReader["AccountTypeIDFund"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AccountTypeIDFund = (int)ojb;
            }
            ojb = dataReader["BreedClassTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassTypeID = (int)ojb;
            }
            ojb = dataReader["AccountTypeIDHold"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AccountTypeIDHold = (int)ojb;
            }
            ojb = dataReader["BourseTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BourseTypeID = (int)ojb;
            }
            ojb = dataReader["ISSysDefaultBreed"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ISSysDefaultBreed = (int)ojb;
            }
            ojb = dataReader["ISHKBreedClassType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ISHKBreedClassType = (int)ojb;
            }
            ojb = dataReader["DeleteState"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DeleteState = (int)ojb;
            }

            return model;
        }

        #endregion  ��Ա����

        #region ��ȡ�ֻ�Ʒ������

        /// <summary>
        /// ��ȡ�ֻ�Ʒ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetBreedClassName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_CMBREEDCLASS);
            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

        #region ��ȡ�ֻ���ͨ�͸۹�Ʒ������

        /// <summary>
        /// ��ȡ�ֻ���ͨ�͸۹�Ʒ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetXHAndHKBreedClassName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_XHANDHKCMBREEDCLASS);
            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

        #region ��ȡ���н�����ƷƷ��

        /// <summary>
        /// ��ȡ���н�����ƷƷ��
        /// </summary>
        /// <param name="BreedClassTypeID">Ʒ������ID</param>
        ///  <param name="BourseTypeID">����������ID</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMBreedClass(int BreedClassTypeID, int BourseTypeID, int pageNo, int pageSize,
                                          out int rowCount)
        {
            //������ѯ
            if (BreedClassTypeID != AppGlobalVariable.INIT_INT)
            {
                SQL_SELECTALL_CMBREEDCLASS += "AND (BreedClassTypeID=@BreedClassTypeID) ";
            }
            if (BourseTypeID != AppGlobalVariable.INIT_INT)
            {
                SQL_SELECTALL_CMBREEDCLASS += "AND (BourseTypeID=@BourseTypeID) ";
            }

            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTALL_CMBREEDCLASS);
            if (BreedClassTypeID != AppGlobalVariable.INIT_INT)
            {
                database.AddInParameter(dbCommand, "BreedClassTypeID", DbType.Int32, BreedClassTypeID);
            }
            if (BourseTypeID != AppGlobalVariable.INIT_INT)
            {
                database.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, BourseTypeID);
            }

            return CommPager.QueryPager(database, dbCommand, SQL_SELECTALL_CMBREEDCLASS, pageNo, pageSize,
                                        out rowCount, "CM_BreedClass");
        }

        #endregion

        #region  ���ݽ�����ƷƷ�ֱ��еĽ���������ID��ȡ��������������

        /// <summary>
        /// ���ݽ�����ƷƷ�ֱ��еĽ���������ID��ȡ��������������
        /// </summary>
        /// <returns></returns>
        public DataSet GetCMBreedClassBourseTypeName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBOURSETYPENAME_CMBREEDCLASS);
            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

        #region ��ȡ����Ʒ������

        /// <summary>
        /// ��ȡ����Ʒ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllBreedClassName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBREEDCLASSNAME_CM_BREEDCLASS);
            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

        #region ��ȡƷ����������Ʒ�ڻ���Ʒ������

        /// <summary>
        /// ��ȡƷ����������Ʒ�ڻ���Ʒ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpQhTypeBreedClassName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBREEDCLASSNAMESPQH_CMBREEDCLASS);
            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

        #region ��ȡƷ����������Ʒ�ڻ����ָ�ڻ���Ʒ������

        /// <summary>
        /// ��ȡƷ����������Ʒ�ڻ����ָ�ڻ���Ʒ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetQHFutureCostsBreedClassName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBREEDCLASSNAMEQHFUTURECOSTS_CMBREEDCLASS);
            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

        #region ��ȡƷ�������ǹ�ָ�ڻ���Ʒ������

        /// <summary>
        /// ��ȡƷ�������ǹ�ָ�ڻ���Ʒ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetQHSIFPositionAndBailBreedClassName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTQHSIF_CMBREEDCLASS);
            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

    }
}