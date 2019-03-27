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
    ///���������������� ���ݷ�����CM_BourseTypeDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-20
    ///Desc: �����˴�������ֶ� CodeRulesType
    ///Update By: ����
    ///Update Date: 2010-03-10
    /// </summary>
    public class CM_BourseTypeDAL
    {
        public CM_BourseTypeDAL()
        {
        }

        #region SQL
        /// <summary>
        /// ���ؽ�������Ϣ
        /// </summary>
        private string SQL_SELECT_CMBOURSETYPE = @"SELECT * FROM CM_BOURSETYPE WHERE DELETESTATE IS NOT NULL AND DELETESTATE<>1 ";// 1=1

        /// <summary>
        /// ��ȡ��������������
        /// </summary>
        private string SQL_SELECTBOURSETYPENAME_CMBOURSETYPE = @"SELECT BOURSETYPEID,BOURSETYPENAME FROM CM_BOURSETYPE 
                                                                 WHERE DELETESTATE IS NOT NULL AND DELETESTATE<>1 ";

        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(BourseTypeID)+1 from CM_BourseType";
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
        public bool Exists(int BourseTypeID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CM_BourseType where BourseTypeID=@BourseTypeID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, BourseTypeID);
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
        /// ��ӽ���������
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.CM_BourseType model)
        {
            return Add(model, null, null);
        }

        /// <summary>
        /// ��ӽ���������
        /// </summary>
        /// <param name="model">����������ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.CM_BourseType model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SET IDENTITY_INSERT [CM_BourseType] ON; ");//�ر��Զ�������(��ΪϵͳĬ��ǰ100ID����ϵͳID)
            strSql.Append("insert into CM_BourseType(");
            strSql.Append("BourseTypeID,BourseTypeName,ReceivingConsignStartTime,ReceivingConsignEndTime,CounterFromSubmitStartTime,CounterFromSubmitEndTime,ISSysDefaultBourseType,DeleteState,CodeRulesType)");
            strSql.Append(" values (");
            strSql.Append("@BourseTypeID,@BourseTypeName,@ReceivingConsignStartTime,@ReceivingConsignEndTime,@CounterFromSubmitStartTime,@CounterFromSubmitEndTime,@ISSysDefaultBourseType,@DeleteState,@CodeRulesType)");
            //strSql.Append(";select @@IDENTITY");
            strSql.Append(";SET IDENTITY_INSERT [CM_BourseType] OFF");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, model.BourseTypeID);
            db.AddInParameter(dbCommand, "BourseTypeName", DbType.String, model.BourseTypeName);
            db.AddInParameter(dbCommand, "ReceivingConsignStartTime", DbType.DateTime, model.ReceivingConsignStartTime);
            db.AddInParameter(dbCommand, "ReceivingConsignEndTime", DbType.DateTime, model.ReceivingConsignEndTime);
            db.AddInParameter(dbCommand, "CounterFromSubmitStartTime", DbType.DateTime, model.CounterFromSubmitStartTime);
            db.AddInParameter(dbCommand, "CounterFromSubmitEndTime", DbType.DateTime, model.CounterFromSubmitEndTime);
            db.AddInParameter(dbCommand, "ISSysDefaultBourseType", DbType.Int32, model.ISSysDefaultBourseType);
            model.DeleteState = (int)Types.IsYesOrNo.No;//���ʱĬ��״̬Ϊ��ɾ��
            db.AddInParameter(dbCommand, "DeleteState", DbType.Int32, model.DeleteState);
            db.AddInParameter(dbCommand, "CodeRulesType", DbType.Int32, model.CodeRulesType);
            
            object obj;
            if (tran == null)
            {
                obj = db.ExecuteScalar(dbCommand);
            }
            else
            {
                obj = db.ExecuteScalar(dbCommand, tran);
            }
            //if (!int.TryParse(obj.ToString(), out result))
            //{
            //    return AppGlobalVariable.INIT_INT;
            //}
           // return result;
            return  model.BourseTypeID;
        }

        /// <summary>
        /// ���½���������
        /// </summary>
        /// <param name="model">����������ʵ��</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.CM_BourseType model)
        {
            return Update(model, null, null);
        }

        /// <summary>
        /// ���½���������
        /// </summary>
        /// <param name="model">����������ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.CM_BourseType model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CM_BourseType set ");
            strSql.Append("BourseTypeName=@BourseTypeName,");
            //strSql.Append("ReceivingConsignStartTime=@ReceivingConsignStartTime,");
            //strSql.Append("ReceivingConsignEndTime=@ReceivingConsignEndTime,");
            strSql.Append("CounterFromSubmitStartTime=@CounterFromSubmitStartTime,");
            strSql.Append("CounterFromSubmitEndTime=@CounterFromSubmitEndTime");
            strSql.Append(" where BourseTypeID=@BourseTypeID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, model.BourseTypeID);
            db.AddInParameter(dbCommand, "BourseTypeName", DbType.String, model.BourseTypeName);
            //db.AddInParameter(dbCommand, "ReceivingConsignStartTime", DbType.DateTime, model.ReceivingConsignStartTime);
            //db.AddInParameter(dbCommand, "ReceivingConsignEndTime", DbType.DateTime, model.ReceivingConsignEndTime);
            db.AddInParameter(dbCommand, "CounterFromSubmitStartTime", DbType.DateTime, model.CounterFromSubmitStartTime);
            db.AddInParameter(dbCommand, "CounterFromSubmitEndTime", DbType.DateTime, model.CounterFromSubmitEndTime);
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
        /// ���½��������͵Ĵ�Ͻ���ί�п�ʼʱ��ʹ�Ͻ���ί�н���ʱ�䣨���ݽ��׿�ʼ�ͽ���ʱ����£�
        /// </summary>
        /// <param name="bourseTypeID">����������ID</param>
        /// <param name="receivingConsignStartTime">��Ͻ���ί�п�ʼʱ��</param>
        /// <param name="receivingConsignEndTime">��Ͻ���ί�н���ʱ��</param>
        /// <param name="db"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public bool Update(int bourseTypeID, DateTime receivingConsignStartTime, DateTime receivingConsignEndTime, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CM_BourseType set ");
            strSql.Append("ReceivingConsignStartTime=@receivingConsignStartTime,");
            strSql.Append("ReceivingConsignEndTime=@receivingConsignEndTime");
            strSql.Append(" where BourseTypeID=@bourseTypeID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, bourseTypeID);
            db.AddInParameter(dbCommand, "ReceivingConsignStartTime", DbType.DateTime, receivingConsignStartTime);
            db.AddInParameter(dbCommand, "ReceivingConsignEndTime", DbType.DateTime, receivingConsignEndTime);
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

        /// <summary>
        /// ���½��������͵Ĵ�Ͻ���ί�п�ʼʱ��ʹ�Ͻ���ί�н���ʱ�䣨���ݽ��׿�ʼ�ͽ���ʱ����£�
        /// </summary>
        /// <param name="bourseTypeID">����������ID</param>
        /// <param name="receivingConsignStartTime">��Ͻ���ί�п�ʼʱ��</param>
        /// <param name="receivingConsignEndTime">��Ͻ���ί�н���ʱ��</param>
        /// <returns></returns>
        public bool Update(int bourseTypeID, DateTime receivingConsignStartTime, DateTime receivingConsignEndTime)
        {
            return Update(bourseTypeID, receivingConsignStartTime, receivingConsignEndTime, null, null);
        }

        #region ���ݽ���������ID��ɾ��������(������)
        /// <summary>
        /// ���ݽ���������ID��ɾ��������(������)
        /// </summary>
        /// <param name="BourseTypeID">����������ID</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        public bool Delete(int BourseTypeID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete CM_BourseType ");
            strSql.Append(" where BourseTypeID=@BourseTypeID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, BourseTypeID);
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


        #region ���ݽ���������ID��ɾ��������(������)
        /// <summary>
        /// ���ݽ���������ID��ɾ��������(������)
        /// </summary>
        /// <param name="BourseTypeID">����������ID</param>
        /// <returns></returns>
        public bool Delete(int BourseTypeID)
        {
            return Delete(BourseTypeID, null, null);
        }
        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_BourseType GetModel(int BourseTypeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BourseTypeID,BourseTypeName,ReceivingConsignStartTime,ReceivingConsignEndTime,CounterFromSubmitStartTime,CounterFromSubmitEndTime,ISSysDefaultBourseType,DeleteState,CodeRulesType from CM_BourseType ");
            strSql.Append(" where BourseTypeID=@BourseTypeID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, BourseTypeID);
            ManagementCenter.Model.CM_BourseType model = null;
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
            strSql.Append("select BourseTypeID,BourseTypeName,ReceivingConsignStartTime,ReceivingConsignEndTime,CounterFromSubmitStartTime,CounterFromSubmitEndTime,ISSysDefaultBourseType,DeleteState,CodeRulesType ");
            strSql.Append(" FROM CM_BourseType ");
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
        public List<ManagementCenter.Model.CM_BourseType> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BourseTypeID,BourseTypeName,ReceivingConsignStartTime,ReceivingConsignEndTime,CounterFromSubmitStartTime,CounterFromSubmitEndTime,ISSysDefaultBourseType,DeleteState,CodeRulesType ");
            strSql.Append(" FROM CM_BourseType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.CM_BourseType> list = new List<ManagementCenter.Model.CM_BourseType>();
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

        #region ��ȡ���н���������

        /// <summary>
        /// ��ȡ���н���������
        /// </summary>
        /// <param name="BourseTypeName">��������������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMBourseType(string BourseTypeName, int pageNo, int pageSize,
                                            out int rowCount)
        {
            //������ѯ
            if (BourseTypeName != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(BourseTypeName))
            {
                SQL_SELECT_CMBOURSETYPE += "AND (BourseTypeName LIKE  '%' + @BourseTypeName + '%') ";
            }

            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_CMBOURSETYPE);

            if (BourseTypeName != AppGlobalVariable.INIT_STRING && BourseTypeName != string.Empty)
            {
                database.AddInParameter(dbCommand, "BourseTypeName", DbType.String, BourseTypeName);
            }

            return CommPager.QueryPager(database, dbCommand, SQL_SELECT_CMBOURSETYPE, pageNo, pageSize,
                                        out rowCount, "CM_BourseType");
        }

        #endregion

        #region ��ȡ��������������
        /// <summary>
        /// ��ȡ��������������
        /// </summary>
        /// <returns></returns>
        public DataSet GetBourseTypeName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBOURSETYPENAME_CMBOURSETYPE);
            return database.ExecuteDataSet(dbCommand);
        }
        #endregion


        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.CM_BourseType ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.CM_BourseType model = new ManagementCenter.Model.CM_BourseType();
            object ojb;
            ojb = dataReader["BourseTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BourseTypeID = (int)ojb;
            }
            model.BourseTypeName = dataReader["BourseTypeName"].ToString();
            ojb = dataReader["ReceivingConsignStartTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ReceivingConsignStartTime = (DateTime)ojb;
            }
            ojb = dataReader["ReceivingConsignEndTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ReceivingConsignEndTime = (DateTime)ojb;
            }
            ojb = dataReader["CounterFromSubmitStartTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CounterFromSubmitStartTime = (DateTime)ojb;
            }
            ojb = dataReader["CounterFromSubmitEndTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CounterFromSubmitEndTime = (DateTime)ojb;
            }
            ojb = dataReader["ISSysDefaultBourseType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ISSysDefaultBourseType = (int)ojb;
            }
            ojb = dataReader["DeleteState"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DeleteState = (int)ojb;
            }
            ojb = dataReader["CodeRulesType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CodeRulesType = (int)ojb;
            }
            return model;
        }

        #endregion  ��Ա����
    }
}