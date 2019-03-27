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
    ///�������ڻ�_�ֲ����� ���ݷ�����QH_PositionLimitValueDAL��
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// Desc: �������ֶ�PositionLimitType��ش���
    /// Update By: ����
    /// Update Date: 2010-01-21
    /// </summary>
    public class QH_PositionLimitValueDAL
    {
        public QH_PositionLimitValueDAL()
        {
        }

        #region SQL

        /// <summary>
        /// ���ݲ�ѯ��������(��Ʒ)�ڻ�_�ֲ�����
        /// </summary>
        private string SQL_SELECT_QHPOSITIONLIMITVALUE =
            @"SELECT B.BREEDCLASSNAME,A.* 
                                                FROM QH_POSITIONLIMITVALUE AS A,CM_BREEDCLASS AS B
                                                WHERE A.BREEDCLASSID=B.BREEDCLASSID AND B.BREEDCLASSTYPEID=2 ";

        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(PositionLimitValueID)+1 from QH_PositionLimitValue";
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
        public bool Exists(int PositionLimitValueID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from QH_PositionLimitValue where PositionLimitValueID=@PositionLimitValueID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "PositionLimitValueID", DbType.Int32, PositionLimitValueID);
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
        public int Add(ManagementCenter.Model.QH_PositionLimitValue model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QH_PositionLimitValue(");
            strSql.Append(
                "Start,PositionValue,DeliveryMonthType,Ends,BreedClassID,UpperLimitIfEquation,LowerLimitIfEquation,PositionBailTypeID,PositionValueTypeID,MinUnitLimit)");

            strSql.Append(" values (");
            strSql.Append(
                "@Start,@PositionValue,@DeliveryMonthType,@Ends,@BreedClassID,@UpperLimitIfEquation,@LowerLimitIfEquation,@PositionBailTypeID,@PositionValueTypeID,@MinUnitLimit)");
            strSql.Append(";select @@IDENTITY");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Start", DbType.Int32, model.Start);
            db.AddInParameter(dbCommand, "PositionValue", DbType.Decimal, model.PositionValue);
            db.AddInParameter(dbCommand, "DeliveryMonthType", DbType.Int32, model.DeliveryMonthType);
            db.AddInParameter(dbCommand, "Ends", DbType.Int32, model.Ends);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
            db.AddInParameter(dbCommand, "UpperLimitIfEquation", DbType.Int32, model.UpperLimitIfEquation);
            db.AddInParameter(dbCommand, "LowerLimitIfEquation", DbType.Int32, model.LowerLimitIfEquation);
            db.AddInParameter(dbCommand, "PositionBailTypeID", DbType.Int32, model.PositionBailTypeID);
            db.AddInParameter(dbCommand, "PositionValueTypeID", DbType.Int32, model.PositionValueTypeID);
            db.AddInParameter(dbCommand, "MinUnitLimit", DbType.Int32, model.MinUnitLimit);
            int result;
            object obj = db.ExecuteScalar(dbCommand);
            if (!int.TryParse(obj.ToString(), out result))
            {
                //return 0;//��Ҫ����
                return AppGlobalVariable.INIT_INT;
            }
            return result;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(ManagementCenter.Model.QH_PositionLimitValue model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QH_PositionLimitValue set ");
            strSql.Append("Start=@Start,");
            strSql.Append("PositionValue=@PositionValue,");
            strSql.Append("DeliveryMonthType=@DeliveryMonthType,");
            strSql.Append("Ends=@Ends,");
            strSql.Append("BreedClassID=@BreedClassID,");
            strSql.Append("UpperLimitIfEquation=@UpperLimitIfEquation,");
            strSql.Append("LowerLimitIfEquation=@LowerLimitIfEquation,");
            strSql.Append("PositionBailTypeID=@PositionBailTypeID,");
            strSql.Append("PositionValueTypeID=@PositionValueTypeID,");
            strSql.Append("MinUnitLimit=@MinUnitLimit");
            strSql.Append(" where PositionLimitValueID=@PositionLimitValueID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Start", DbType.Int32, model.Start);
            db.AddInParameter(dbCommand, "PositionValue", DbType.Decimal, model.PositionValue);
            db.AddInParameter(dbCommand, "DeliveryMonthType", DbType.Int32, model.DeliveryMonthType);
            db.AddInParameter(dbCommand, "PositionLimitValueID", DbType.Int32, model.PositionLimitValueID);
            db.AddInParameter(dbCommand, "Ends", DbType.Int32, model.Ends);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
            db.AddInParameter(dbCommand, "UpperLimitIfEquation", DbType.Int32, model.UpperLimitIfEquation);
            db.AddInParameter(dbCommand, "LowerLimitIfEquation", DbType.Int32, model.LowerLimitIfEquation);
            db.AddInParameter(dbCommand, "PositionBailTypeID", DbType.Int32, model.PositionBailTypeID);
            db.AddInParameter(dbCommand, "PositionValueTypeID", DbType.Int32, model.PositionValueTypeID);
            db.AddInParameter(dbCommand, "MinUnitLimit", DbType.Int32, model.MinUnitLimit);
            db.ExecuteNonQuery(dbCommand);
            return true;
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int PositionLimitValueID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete QH_PositionLimitValue ");
            strSql.Append(" where PositionLimitValueID=@PositionLimitValueID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "PositionLimitValueID", DbType.Int32, PositionLimitValueID);
            db.ExecuteNonQuery(dbCommand);
            return true;
        }

        /// <summary>
        /// ����Ʒ��ID��ɾ���ڻ�_�ֲ�����
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool DeletePositionLimitVByBreedClassID(int BreedClassID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete QH_PositionLimitValue ");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            if(db==null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
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
        /// ����Ʒ��ID��ɾ���ڻ�_�ֲ�����
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool DeletePositionLimitVByBreedClassID(int BreedClassID)
        {
            return DeletePositionLimitVByBreedClassID(BreedClassID, null, null);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_PositionLimitValue GetModel(int PositionLimitValueID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select Start,PositionValue,DeliveryMonthType,PositionLimitValueID,Ends,BreedClassID,UpperLimitIfEquation,LowerLimitIfEquation,PositionBailTypeID,PositionValueTypeID,MinUnitLimit from QH_PositionLimitValue ");
            strSql.Append(" where PositionLimitValueID=@PositionLimitValueID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "PositionLimitValueID", DbType.Int32, PositionLimitValueID);
            ManagementCenter.Model.QH_PositionLimitValue model = null;
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
                "select Start,PositionValue,DeliveryMonthType,PositionLimitValueID,Ends,BreedClassID,UpperLimitIfEquation,LowerLimitIfEquation,PositionBailTypeID,PositionValueTypeID,MinUnitLimit ");
            strSql.Append(" FROM QH_PositionLimitValue ");
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
        public List<ManagementCenter.Model.QH_PositionLimitValue> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select Start,PositionValue,DeliveryMonthType,PositionLimitValueID,Ends,BreedClassID,UpperLimitIfEquation,LowerLimitIfEquation,PositionBailTypeID,PositionValueTypeID,MinUnitLimit ");
            strSql.Append(" FROM QH_PositionLimitValue ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.QH_PositionLimitValue> list =
                new List<ManagementCenter.Model.QH_PositionLimitValue>();
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

        #region ��ȡ����(��Ʒ)�ڻ�_�ֲ�����

        /// <summary>
        ///��ȡ����(��Ʒ)�ڻ�_�ֲ����� 
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="DeliveryMonthType">�����·����ͱ�ʶ</param>
        /// <param name="PositionBailTypeID">�ֲֺͱ�֤��������ͱ�ʶ</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllQHPositionLimitValue(string BreedClassName, int DeliveryMonthType, int PositionBailTypeID,
                                                  int pageNo, int pageSize,
                                                  out int rowCount)
        {
            //������ѯ
            if (BreedClassName != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(BreedClassName))
            {
                SQL_SELECT_QHPOSITIONLIMITVALUE += "AND (BreedClassName LIKE  '%' + @BreedClassName + '%') ";
            }

            if (DeliveryMonthType != AppGlobalVariable.INIT_INT)
            {
                SQL_SELECT_QHPOSITIONLIMITVALUE += "AND (DeliveryMonthType=@DeliveryMonthType) ";
            }
            if (PositionBailTypeID != AppGlobalVariable.INIT_INT)
            {
                SQL_SELECT_QHPOSITIONLIMITVALUE += "AND (PositionBailTypeID=@PositionBailTypeID) ";
            }

            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_QHPOSITIONLIMITVALUE);

            if (BreedClassName != AppGlobalVariable.INIT_STRING && BreedClassName != string.Empty)
            {
                database.AddInParameter(dbCommand, "BreedClassName", DbType.String, BreedClassName);
            }

            if (DeliveryMonthType != AppGlobalVariable.INIT_INT)
            {
                database.AddInParameter(dbCommand, "DeliveryMonthType", DbType.Int32, DeliveryMonthType);
            }

            if (PositionBailTypeID != AppGlobalVariable.INIT_INT)
            {
                database.AddInParameter(dbCommand, "PositionBailTypeID", DbType.Int32, PositionBailTypeID);
            }
            return CommPager.QueryPager(database, dbCommand, SQL_SELECT_QHPOSITIONLIMITVALUE, pageNo, pageSize,
                                        out rowCount, "QH_PositionLimitValue");
        }

        #endregion

        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.QH_PositionLimitValue ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.QH_PositionLimitValue model = new ManagementCenter.Model.QH_PositionLimitValue();
            object ojb;
            ojb = dataReader["Start"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Start = (int)ojb;
            }
            ojb = dataReader["PositionValue"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PositionValue = (decimal)ojb;
            }
            ojb = dataReader["DeliveryMonthType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DeliveryMonthType = (int)ojb;
            }
            ojb = dataReader["PositionLimitValueID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PositionLimitValueID = (int)ojb;
            }
            ojb = dataReader["Ends"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ends = (int)ojb;
            }
            ojb = dataReader["BreedClassID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassID = (int)ojb;
            }
            ojb = dataReader["UpperLimitIfEquation"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UpperLimitIfEquation = (int)ojb;
            }
            ojb = dataReader["LowerLimitIfEquation"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LowerLimitIfEquation = (int)ojb;
            }
            ojb = dataReader["PositionBailTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PositionBailTypeID = (int)ojb;
            }
            ojb = dataReader["PositionValueTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PositionValueTypeID = (int)ojb;
            }
            ojb = dataReader["MinUnitLimit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MinUnitLimit = (int)ojb;
            }
            return model;
        }

        #endregion  ��Ա����
    }
}