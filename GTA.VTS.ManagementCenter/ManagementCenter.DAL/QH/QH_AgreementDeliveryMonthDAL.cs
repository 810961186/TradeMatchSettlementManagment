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
    ///��������Լ�����·� ���ݷ�����QH_AgreementDeliveryMonthDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-21
    ///�޸ģ�Ҷ��
    ///���ڣ�2010-01-20
    ///���������ͨ�������·ݱ�ʶ��Ʒ�ֱ�ʶ��ѯ�˽����·��Ƿ����
    /// </summary>
    public class QH_AgreementDeliveryMonthDAL
    {
        public QH_AgreementDeliveryMonthDAL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(AgreementDeliveryMonthID)+1 from QH_AgreementDeliveryMonth";
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
        public bool Exists(int AgreementDeliveryMonthID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select count(1) from QH_AgreementDeliveryMonth where AgreementDeliveryMonthID=@AgreementDeliveryMonthID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AgreementDeliveryMonthID", DbType.Int32, AgreementDeliveryMonthID);
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

        #region ��Ӻ�Լ�����·�

        /// <summary>
        /// ��Ӻ�Լ�����·�
        /// </summary>
        /// <param name="model">��Լ�����·�ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.QH_AgreementDeliveryMonth model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QH_AgreementDeliveryMonth(");
            strSql.Append("MonthID,BreedClassID)");

            strSql.Append(" values (");
            strSql.Append("@MonthID,@BreedClassID)");
            strSql.Append(";select @@IDENTITY");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "MonthID", DbType.Int32, model.MonthID);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
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

        #region ��Ӻ�Լ�����·�

        /// <summary>
        /// ��Ӻ�Լ�����·�
        /// </summary>
        /// <param name="model">��Լ�����·�ʵ��</param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.QH_AgreementDeliveryMonth model)
        {
            return Add(model, null, null);
        }

        #endregion

        #region ���º�Լ�����·�

        /// <summary>
        /// ���º�Լ�����·�
        /// </summary>
        /// <param name="model">��Լ�����·�ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.QH_AgreementDeliveryMonth model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QH_AgreementDeliveryMonth set ");
            strSql.Append("MonthID=@MonthID,");
            strSql.Append("BreedClassID=@BreedClassID");
            strSql.Append(" where AgreementDeliveryMonthID=@AgreementDeliveryMonthID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AgreementDeliveryMonthID", DbType.Int32, model.AgreementDeliveryMonthID);
            db.AddInParameter(dbCommand, "MonthID", DbType.Int32, model.MonthID);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
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

        #region ���º�Լ�����·�

        /// <summary>
        /// ���º�Լ�����·�
        /// </summary>
        /// <param name="model">��Լ�����·�ʵ��</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.QH_AgreementDeliveryMonth model)
        {
            return Update(model, null, null);
        }

        #endregion

        #region ���ݺ�Լ�����·�ID��ɾ����Լ�����·�

        /// <summary>
        /// ���ݺ�Լ�����·�ID��ɾ����Լ�����·�
        /// </summary>
        /// <param name="AgreementDeliveryMonthID">��Լ�����·�ID</param>
        /// <returns></returns>
        public void Delete(int AgreementDeliveryMonthID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete QH_AgreementDeliveryMonth ");
            strSql.Append(" where AgreementDeliveryMonthID=@AgreementDeliveryMonthID ");

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AgreementDeliveryMonthID", DbType.Int32, AgreementDeliveryMonthID);
            db.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region  ���ݺ�Լ�·�ID��Ʒ��ID��ɾ����Լ�����·�

        /// <summary>
        ///  ���ݺ�Լ�·�ID��Ʒ��ID��ɾ����Լ�����·�
        /// </summary>
        /// <param name="MonthID">�·�ID</param>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Delete(int MonthID, int BreedClassID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete QH_AgreementDeliveryMonth ");
            strSql.Append(" where MonthID=@MonthID and BreedClassID=@BreedClassID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "MonthID", DbType.Int32, MonthID);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);

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

        #region ���ݺ�Լ�·�ID��Ʒ��ID��ɾ����Լ�����·�

        /// <summary>
        ///  ���ݺ�Լ�·�ID��Ʒ��ID��ɾ����Լ�����·�
        /// </summary>
        /// <param name="MonthID">�·�ID</param>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool Delete(int MonthID, int BreedClassID)
        {
            return Delete(MonthID, BreedClassID, null, null);
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_AgreementDeliveryMonth GetModel(int AgreementDeliveryMonthID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AgreementDeliveryMonthID,MonthID,BreedClassID from QH_AgreementDeliveryMonth ");
            strSql.Append(" where AgreementDeliveryMonthID=@AgreementDeliveryMonthID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AgreementDeliveryMonthID", DbType.Int32, AgreementDeliveryMonthID);
            ManagementCenter.Model.QH_AgreementDeliveryMonth model = null;
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
            strSql.Append("select AgreementDeliveryMonthID,MonthID,BreedClassID ");
            strSql.Append(" FROM QH_AgreementDeliveryMonth ");
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
        public List<ManagementCenter.Model.QH_AgreementDeliveryMonth> GetListArray(string strWhere, DbTransaction tran,
                                                                                   Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AgreementDeliveryMonthID,MonthID,BreedClassID ");
            strSql.Append(" FROM QH_AgreementDeliveryMonth ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.QH_AgreementDeliveryMonth> list =
                new List<ManagementCenter.Model.QH_AgreementDeliveryMonth>();
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
        public List<ManagementCenter.Model.QH_AgreementDeliveryMonth> GetListArray(string strWhere)
        {
            return GetListArray(strWhere, null, null);
        }

        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.QH_AgreementDeliveryMonth ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.QH_AgreementDeliveryMonth model =
                new ManagementCenter.Model.QH_AgreementDeliveryMonth();
            object ojb;
            ojb = dataReader["AgreementDeliveryMonthID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AgreementDeliveryMonthID = (int) ojb;
            }
            ojb = dataReader["MonthID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MonthID = (int) ojb;
            }
            ojb = dataReader["BreedClassID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassID = (int) ojb;
            }
            return model;
        }
        /// <summary>
        /// ����Ʒ�ֱ�ʶ���·ݱ�ʶ��ѯ�Ƿ���ڴ˽����·�
        /// </summary>
        /// <param name="breedClassID">Ʒ�ֱ�ʶ</param>
        /// <param name="monthid">�����·ݱ�ʶ</param>
        /// <returns>�Ƿ���ڴ˽����·�</returns>
        public ManagementCenter.Model.QH_AgreementDeliveryMonth GetQHAgreementDeliveryBreedClassID(int breedClassID, int monthid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AgreementDeliveryMonthID,MonthID,BreedClassID from QH_AgreementDeliveryMonth ");
            strSql.Append(" where MonthID=@monthid and  BreedClassID=@breedClassID");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "monthid", DbType.Int32, monthid);
            db.AddInParameter(dbCommand, "breedClassID", DbType.Int32, breedClassID);
            ManagementCenter.Model.QH_AgreementDeliveryMonth model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }

        #endregion  ��Ա����

      
    }
}