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
    ///������Ʒ��_�ֻ�_���׷��� ���ݷ�����XH_SpotCostsDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class XH_SpotCostsDAL
    {
        public XH_SpotCostsDAL()
        {
        }

        #region SQL

        /// <summary>
        /// ���ݲ�ѯ���������ֻ����׷�������
        /// </summary>
        private string SQL_SELECT_XHSPOTCOSTS =
            @"SELECT B.BREEDCLASSNAME,A.* FROM 
                                                 XH_SPOTCOSTS AS A,CM_BREEDCLASS AS B 
                                                 WHERE A.BREEDCLASSID=B.BREEDCLASSID ";

        /// <summary>
        /// �����ֻ����׷��ñ��е�Ʒ�ֱ�ʶ��ȡƷ������
        /// </summary>
        private string SQL_SELECTBREEDCLASSNAME_XHSPOTCOSTS =
            @"SELECT A.BREEDCLASSID,A.BREEDCLASSNAME 
			                                                    FROM CM_BREEDCLASS A,XH_SPOTCOSTS B 
			                                                    WHERE A.BREEDCLASSID=B.BREEDCLASSID";

        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(BreedClassID)+1 from XH_SpotCosts";
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
            strSql.Append("select count(1) from XH_SpotCosts where BreedClassID=@BreedClassID ");
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
        /// ����һ������
        /// </summary>
        public bool Add(ManagementCenter.Model.XH_SpotCosts model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_SpotCosts(");
            strSql.Append(
                "GetValueTypeID,StampDuty,StampDutyStartingpoint,Commision,TransferToll,MonitoringFee,TransferTollStartingpoint,TransferTollTypeID,ClearingFees,CommisionStartingpoint,PoundageSingleValue,SystemToll,BreedClassID,CurrencyTypeID,StampDutyTypeID)");

            strSql.Append(" values (");
            strSql.Append(
                "@GetValueTypeID,@StampDuty,@StampDutyStartingpoint,@Commision,@TransferToll,@MonitoringFee,@TransferTollStartingpoint,@TransferTollTypeID,@ClearingFees,@CommisionStartingpoint,@PoundageSingleValue,@SystemToll,@BreedClassID,@CurrencyTypeID,@StampDutyTypeID)");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "GetValueTypeID", DbType.Int32, model.GetValueTypeID);

            db.AddInParameter(dbCommand, "StampDuty", DbType.Decimal, model.StampDuty);

            db.AddInParameter(dbCommand, "StampDutyStartingpoint", DbType.Decimal, model.StampDutyStartingpoint);
            if (model.Commision == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "Commision", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "Commision", DbType.Decimal, model.Commision);
            }
            db.AddInParameter(dbCommand, "TransferToll", DbType.Decimal, model.TransferToll);

            db.AddInParameter(dbCommand, "MonitoringFee", DbType.Decimal, model.MonitoringFee);

            db.AddInParameter(dbCommand, "TransferTollStartingpoint", DbType.Decimal,
                              model.TransferTollStartingpoint);

            db.AddInParameter(dbCommand, "TransferTollTypeID", DbType.Int32, model.TransferTollTypeID);

            db.AddInParameter(dbCommand, "ClearingFees", DbType.Decimal, model.ClearingFees);

            db.AddInParameter(dbCommand, "CommisionStartingpoint", DbType.Decimal, model.CommisionStartingpoint);

            if (model.PoundageSingleValue == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "PoundageSingleValue", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "PoundageSingleValue", DbType.Decimal, model.PoundageSingleValue);
            }
            if (model.SystemToll == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "SystemToll", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "SystemToll", DbType.Decimal, model.SystemToll);
            }

            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
            db.AddInParameter(dbCommand, "CurrencyTypeID", DbType.Int32, model.CurrencyTypeID);
            db.AddInParameter(dbCommand, "StampDutyTypeID", DbType.Int32, model.StampDutyTypeID);
            //db.ExecuteNonQuery(dbCommand);
            //object obj;
            if (tran == null)
            {
                //obj = db.ExecuteNonQuery(dbCommand);
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                //obj = db.ExecuteNonQuery(dbCommand, tran);
                db.ExecuteNonQuery(dbCommand, tran);
            }
            return true;
        }

        /// <summary>
        /// ����ֻ����׷���
        /// </summary>
        /// <param name="model">�ֻ����׷���ʵ��</param>
        /// <returns></returns>
        public bool Add(ManagementCenter.Model.XH_SpotCosts model)
        {
            return Add(model, null, null);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(ManagementCenter.Model.XH_SpotCosts model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_SpotCosts set ");
            strSql.Append("GetValueTypeID=@GetValueTypeID,");
            strSql.Append("StampDuty=@StampDuty,");
            strSql.Append("StampDutyStartingpoint=@StampDutyStartingpoint,");
            strSql.Append("Commision=@Commision,");
            strSql.Append("TransferToll=@TransferToll,");
            strSql.Append("MonitoringFee=@MonitoringFee,");
            strSql.Append("TransferTollStartingpoint=@TransferTollStartingpoint,");
            strSql.Append("TransferTollTypeID=@TransferTollTypeID,");
            strSql.Append("ClearingFees=@ClearingFees,");
            strSql.Append("CommisionStartingpoint=@CommisionStartingpoint,");
            strSql.Append("PoundageSingleValue=@PoundageSingleValue,");
            strSql.Append("SystemToll=@SystemToll,");
            strSql.Append("CurrencyTypeID=@CurrencyTypeID,");
            strSql.Append("StampDutyTypeID=@StampDutyTypeID");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "GetValueTypeID", DbType.Int32, model.GetValueTypeID);
            if (model.StampDuty == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "StampDuty", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "StampDuty", DbType.Decimal, model.StampDuty);
            }
            if (model.StampDutyStartingpoint == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "StampDutyStartingpoint", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "StampDutyStartingpoint", DbType.Decimal, model.StampDutyStartingpoint);
            }
            if (model.Commision == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "Commision", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "Commision", DbType.Decimal, model.Commision);
            }
            if (model.TransferToll == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "TransferToll", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "TransferToll", DbType.Decimal, model.TransferToll);
            }
            if (model.MonitoringFee == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "MonitoringFee", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "MonitoringFee", DbType.Decimal, model.MonitoringFee);
            }
            if (model.TransferTollStartingpoint == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "TransferTollStartingpoint", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "TransferTollStartingpoint", DbType.Decimal,
                                  model.TransferTollStartingpoint);
            }
            db.AddInParameter(dbCommand, "TransferTollTypeID", DbType.Int32, model.TransferTollTypeID);
            if (model.ClearingFees == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "ClearingFees", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "ClearingFees", DbType.Decimal, model.ClearingFees);
            }
            if (model.CommisionStartingpoint == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "CommisionStartingpoint", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "CommisionStartingpoint", DbType.Decimal, model.CommisionStartingpoint);
            }
            if (model.PoundageSingleValue == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "PoundageSingleValue", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "PoundageSingleValue", DbType.Decimal, model.PoundageSingleValue);
            }
            if (model.SystemToll == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "SystemToll", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "SystemToll", DbType.Decimal, model.SystemToll);
            }
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
            db.AddInParameter(dbCommand, "CurrencyTypeID", DbType.Int32, model.CurrencyTypeID);
            db.AddInParameter(dbCommand, "StampDutyTypeID", DbType.Int32, model.StampDutyTypeID);
            db.ExecuteNonQuery(dbCommand);
            return true;
        }

        #region  ɾ���ֻ����׷���

        /// <summary>
        ///  ɾ���ֻ����׷���
        /// </summary>
        public bool Delete(int BreedClassID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete XH_SpotCosts ");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
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

        #endregion

        #region ɾ���ֻ����׷���

        /// <summary>
        /// ɾ���ֻ����׷���
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool Delete(int BreedClassID)
        {
            return Delete(BreedClassID, null, null);
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_SpotCosts GetModel(int BreedClassID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select GetValueTypeID,StampDuty,StampDutyStartingpoint,Commision,TransferToll,MonitoringFee,TransferTollStartingpoint,TransferTollTypeID,ClearingFees,CommisionStartingpoint,PoundageSingleValue,SystemToll,BreedClassID,CurrencyTypeID,StampDutyTypeID from XH_SpotCosts ");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
            ManagementCenter.Model.XH_SpotCosts model = null;
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
            strSql.Append(
                "select GetValueTypeID,StampDuty,StampDutyStartingpoint,Commision,TransferToll,MonitoringFee,TransferTollStartingpoint,TransferTollTypeID,ClearingFees,CommisionStartingpoint,PoundageSingleValue,SystemToll,BreedClassID,CurrencyTypeID,StampDutyTypeID ");
            strSql.Append(" FROM XH_SpotCosts ");
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
        public List<ManagementCenter.Model.XH_SpotCosts> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select GetValueTypeID,StampDuty,StampDutyStartingpoint,Commision,TransferToll,MonitoringFee,TransferTollStartingpoint,TransferTollTypeID,ClearingFees,CommisionStartingpoint,PoundageSingleValue,SystemToll,BreedClassID,CurrencyTypeID,StampDutyTypeID ");
            strSql.Append(" FROM XH_SpotCosts ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.XH_SpotCosts> list = new List<ManagementCenter.Model.XH_SpotCosts>();
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

        #region ��ȡ�����ֻ����׷���

        /// <summary>
        /// ��ȡ�����ֻ����׷���
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllSpotCosts(int BreedClassID, string BreedClassName, int pageNo, int pageSize,
                                       out int rowCount)
        {
            //������ѯ(�ݲ���Ҫ����Ʒ��ID��ѯ)
            if (BreedClassID != AppGlobalVariable.INIT_INT)
            {
                SQL_SELECT_XHSPOTCOSTS += "AND (BreedClassID=@BreedClassID) ";
            }
            if (BreedClassName != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(BreedClassName))
            {
                SQL_SELECT_XHSPOTCOSTS += "AND (BreedClassName LIKE  '%' + @BreedClassName + '%') ";
            }

            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_XHSPOTCOSTS);
            if (BreedClassID != AppGlobalVariable.INIT_INT)
            {
                database.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
            }
            if (BreedClassName != AppGlobalVariable.INIT_STRING && BreedClassName != string.Empty)
            {
                database.AddInParameter(dbCommand, "BreedClassName", DbType.String, BreedClassName);
            }

            return CommPager.QueryPager(database, dbCommand, SQL_SELECT_XHSPOTCOSTS, pageNo, pageSize,
                                        out rowCount, "XH_SpotConsts");
        }

        #endregion

        #region �����ֻ����׷��ñ��е�Ʒ��ID��ȡƷ������

        /// <summary>
        /// �����ֻ����׷��ñ��е�Ʒ��ID��ȡƷ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpotCostsBreedClassName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBREEDCLASSNAME_XHSPOTCOSTS);
            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.XH_SpotCosts ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.XH_SpotCosts model = new ManagementCenter.Model.XH_SpotCosts();
            object ojb;
            ojb = dataReader["GetValueTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GetValueTypeID = (int) ojb;
            }
            ojb = dataReader["StampDuty"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.StampDuty = (decimal) ojb;
            }
            ojb = dataReader["StampDutyStartingpoint"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.StampDutyStartingpoint = (decimal) ojb;
            }
            ojb = dataReader["Commision"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Commision = (decimal) ojb;
            }
            ojb = dataReader["TransferToll"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TransferToll = (decimal) ojb;
            }
            ojb = dataReader["MonitoringFee"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MonitoringFee = (decimal?) ojb;
            }
            ojb = dataReader["TransferTollStartingpoint"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TransferTollStartingpoint = (decimal?) ojb;
            }
            ojb = dataReader["TransferTollTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TransferTollTypeID = (int) ojb;
            }
            ojb = dataReader["ClearingFees"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ClearingFees = (decimal?) ojb;
            }
            ojb = dataReader["CommisionStartingpoint"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CommisionStartingpoint = (decimal) ojb;
            }
            ojb = dataReader["PoundageSingleValue"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PoundageSingleValue = (decimal?) ojb;
            }
            ojb = dataReader["SystemToll"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SystemToll = (decimal?) ojb;
            }
            ojb = dataReader["BreedClassID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassID = (int) ojb;
            }
            ojb = dataReader["CurrencyTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CurrencyTypeID = (int) ojb;
            }
            ojb = dataReader["StampDutyTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.StampDutyTypeID = (int) ojb;
            }
            return model;
        }

        #endregion  ��Ա����
    }
}