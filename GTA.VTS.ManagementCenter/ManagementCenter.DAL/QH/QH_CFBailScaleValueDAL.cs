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
    ///��������Ʒ�ڻ�_��֤�����  ���ݷ�����QH_CFBailScaleValueDAL��
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    public class QH_CFBailScaleValueDAL
    {
        public QH_CFBailScaleValueDAL()
        { }
        #region SQL
        /// <summary>
        /// ���ݲ�ѯ����������Ʒ�ڻ�_��֤�����
        /// </summary>
        private string SQL_SELECT_QHCFBAILSCALEVALUE = @"SELECT B.BREEDCLASSNAME,A.*,C.BailScale as MinScale
                                                FROM QH_CFBAILSCALEVALUE AS A,CM_BREEDCLASS AS B,QH_SIFBail AS C
                                                WHERE A.BREEDCLASSID=B.BREEDCLASSID AND B.BREEDCLASSTYPEID=2 and A.BREEDCLASSID=C.BREEDCLASSID ";


        #endregion


        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(CFBailScaleValueID)+1 from QH_CFBailScaleValue";
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
        public bool Exists(int CFBailScaleValueID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from QH_CFBailScaleValue where CFBailScaleValueID=@CFBailScaleValueID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CFBailScaleValueID", DbType.Int32, CFBailScaleValueID);
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
        public int Add(ManagementCenter.Model.QH_CFBailScaleValue model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QH_CFBailScaleValue(");
            strSql.Append("Start,BailScale,Ends,BreedClassID,DeliveryMonthType,UpperLimitIfEquation,LowerLimitIfEquation,PositionBailTypeID,RelationScaleID)");

            strSql.Append(" values (");
            strSql.Append("@Start,@BailScale,@Ends,@BreedClassID,@DeliveryMonthType,@UpperLimitIfEquation,@LowerLimitIfEquation,@PositionBailTypeID,@RelationScaleID)");
            strSql.Append(";select @@IDENTITY");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Start", DbType.Int32, model.Start);
            db.AddInParameter(dbCommand, "BailScale", DbType.Decimal, model.BailScale);
            db.AddInParameter(dbCommand, "Ends", DbType.Int32, model.Ends);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
            db.AddInParameter(dbCommand, "DeliveryMonthType", DbType.Int32, model.DeliveryMonthType);
            db.AddInParameter(dbCommand, "UpperLimitIfEquation", DbType.Int32, model.UpperLimitIfEquation);
            db.AddInParameter(dbCommand, "LowerLimitIfEquation", DbType.Int32, model.LowerLimitIfEquation);
            db.AddInParameter(dbCommand, "PositionBailTypeID", DbType.Int32, model.PositionBailTypeID);
            db.AddInParameter(dbCommand, "RelationScaleID", DbType.Int32, model.RelationScaleID);
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
        public bool Update(ManagementCenter.Model.QH_CFBailScaleValue model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QH_CFBailScaleValue set ");
            strSql.Append("Start=@Start,");
            strSql.Append("BailScale=@BailScale,");
            strSql.Append("Ends=@Ends,");
            strSql.Append("BreedClassID=@BreedClassID,");
            strSql.Append("DeliveryMonthType=@DeliveryMonthType,");
            strSql.Append("UpperLimitIfEquation=@UpperLimitIfEquation,");
            strSql.Append("LowerLimitIfEquation=@LowerLimitIfEquation,");
            strSql.Append("PositionBailTypeID=@PositionBailTypeID,");
            strSql.Append("RelationScaleID=@RelationScaleID");
            strSql.Append(" where CFBailScaleValueID=@CFBailScaleValueID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CFBailScaleValueID", DbType.Int32, model.CFBailScaleValueID);
            db.AddInParameter(dbCommand, "Start", DbType.Int32, model.Start);
            db.AddInParameter(dbCommand, "BailScale", DbType.Decimal, model.BailScale);
            db.AddInParameter(dbCommand, "Ends", DbType.Int32, model.Ends);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
            db.AddInParameter(dbCommand, "DeliveryMonthType", DbType.Int32, model.DeliveryMonthType);
            db.AddInParameter(dbCommand, "UpperLimitIfEquation", DbType.Int32, model.UpperLimitIfEquation);
            db.AddInParameter(dbCommand, "LowerLimitIfEquation", DbType.Int32, model.LowerLimitIfEquation);
            db.AddInParameter(dbCommand, "PositionBailTypeID", DbType.Int32, model.PositionBailTypeID);
            db.AddInParameter(dbCommand, "RelationScaleID", DbType.Int32, model.RelationScaleID);
            db.ExecuteNonQuery(dbCommand);
            return true;

        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int CFBailScaleValueID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete QH_CFBailScaleValue ");
            strSql.Append(" where CFBailScaleValueID=@CFBailScaleValueID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CFBailScaleValueID", DbType.Int32, CFBailScaleValueID);
            db.ExecuteNonQuery(dbCommand);
            return true;
        }

        /// <summary>
        /// ����Ʒ��ID��ɾ����Ʒ�ڻ�_��֤�����
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool DeleteCFBailScaleVByBreedClassID(int BreedClassID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete QH_CFBailScaleValue ");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
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

        /// <summary>
        /// ����Ʒ��ID��ɾ����Ʒ�ڻ�_��֤�����
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool DeleteCFBailScaleVByBreedClassID(int BreedClassID)
        {
            return DeleteCFBailScaleVByBreedClassID(BreedClassID, null, null);
        }
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_CFBailScaleValue GetModel(int CFBailScaleValueID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CFBailScaleValueID,Start,BailScale,Ends,a.BreedClassID,DeliveryMonthType,UpperLimitIfEquation,LowerLimitIfEquation,PositionBailTypeID,RelationScaleID from QH_CFBailScaleValue a");
            strSql.Append(" where CFBailScaleValueID=@CFBailScaleValueID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CFBailScaleValueID", DbType.Int32, CFBailScaleValueID);
            ManagementCenter.Model.QH_CFBailScaleValue model = null;
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
            strSql.Append("select CFBailScaleValueID,Start,BailScale,Ends,a.BreedClassID,DeliveryMonthType,UpperLimitIfEquation,LowerLimitIfEquation,PositionBailTypeID,RelationScaleID ");
            strSql.Append(" FROM QH_CFBailScaleValue a ");
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
        public List<ManagementCenter.Model.QH_CFBailScaleValue> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CFBailScaleValueID,Start,BailScale,Ends,a.BreedClassID,DeliveryMonthType,UpperLimitIfEquation,LowerLimitIfEquation,PositionBailTypeID,RelationScaleID ");
            strSql.Append(" FROM QH_CFBailScaleValue a ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.QH_CFBailScaleValue> list = new List<ManagementCenter.Model.QH_CFBailScaleValue>();
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

        #region ��ȡ������Ʒ�ڻ�_��֤�����

        /// <summary>
        ///��ȡ������Ʒ�ڻ�_��֤�����
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="DeliveryMonthType">�����·����ͱ�ʶ</param>
        /// <param name="PositionBailTypeID">�ֲֺͱ�֤��������ͱ�ʶ</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllQHCFBailScaleValue(string BreedClassName, int DeliveryMonthType, int PositionBailTypeID,
                                                  int pageNo, int pageSize,
                                                  out int rowCount)
        {
            //������ѯ
            if (BreedClassName != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(BreedClassName))
            {
                SQL_SELECT_QHCFBAILSCALEVALUE += "AND (BreedClassName LIKE  '%' + @BreedClassName + '%') ";
            }

            if (DeliveryMonthType != AppGlobalVariable.INIT_INT)
            {
                SQL_SELECT_QHCFBAILSCALEVALUE += "AND (DeliveryMonthType=@DeliveryMonthType) ";
            }
            if (PositionBailTypeID != AppGlobalVariable.INIT_INT)
            {
                SQL_SELECT_QHCFBAILSCALEVALUE += "AND (PositionBailTypeID=@PositionBailTypeID) ";
            }

            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_QHCFBAILSCALEVALUE);

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
            return CommPager.QueryPager(database, dbCommand, SQL_SELECT_QHCFBAILSCALEVALUE, pageNo, pageSize,
                                        out rowCount, "QH_CFBailScaleValue");
        }

        #endregion

        #region ����ʵ�������
        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.QH_CFBailScaleValue ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.QH_CFBailScaleValue model = new ManagementCenter.Model.QH_CFBailScaleValue();
            object ojb;
            ojb = dataReader["CFBailScaleValueID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CFBailScaleValueID = (int)ojb;
            }
            ojb = dataReader["Start"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Start = (int)ojb;
            }
            ojb = dataReader["BailScale"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BailScale = (decimal)ojb;
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
            ojb = dataReader["DeliveryMonthType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DeliveryMonthType = (int)ojb;
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
            ojb = dataReader["RelationScaleID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RelationScaleID = (int)ojb;
            }
            return model;
        }
        #endregion       

        #endregion  ��Ա����

       
    }
}

