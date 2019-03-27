using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Maticsoft.DBUtility; //�����������

namespace ManagementCenter.DAL
{
    /// <summary>
    ///������Ʒ��_�ֻ�_���׷���_�ɽ���_����������  ���ݷ�����XH_SpotRangeCostDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class XH_SpotRangeCostDAL
    {
        public XH_SpotRangeCostDAL()
        {
        }

        #region SQL

        /// <summary>
        /// ����Ʒ��ID��ȡ�ֻ����׷��ý���������_��Χ_ֵ
        /// </summary>
        private string SQL_SELECT_XHSPOTRANGECOST =
            @"SELECT A.*,B.[VALUE],B.BREEDCLASSID FROM 
CM_FIELDRANGE A,XH_SpotRangeCost B
WHERE A.FIELDRANGEID=B.FIELDRANGEID 
AND B.BREEDCLASSID=@BREEDCLASSID";

        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(FieldRangeID)+1 from XH_SpotRangeCost";
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
        public bool Exists(int FieldRangeID, int BreedClassID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select count(1) from XH_SpotRangeCost where FieldRangeID=@FieldRangeID and BreedClassID=@BreedClassID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FieldRangeID", DbType.Int32, FieldRangeID);
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
        public bool Add(ManagementCenter.Model.XH_SpotRangeCost model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_SpotRangeCost(");
            strSql.Append("Value,FieldRangeID,BreedClassID)");

            strSql.Append(" values (");
            strSql.Append("@Value,@FieldRangeID,@BreedClassID)");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Value", DbType.Decimal, model.Value);
            //db.AddInParameter(dbCommand, "FieldRangeID", DbType.Int32, model.FieldRangeID);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
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

        #region ����ֻ����׷��ý���������

        /// <summary>
        /// ����ֻ����׷��ý���������
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(ManagementCenter.Model.XH_SpotRangeCost model)
        {
            return Add(model, null, null);
        }

        #endregion

        #region  �����ֻ�����������

        /// <summary>
        /// �����ֻ�����������
        /// </summary>
        /// <param name="model">�ֻ�����������ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.XH_SpotRangeCost model, DbTransaction tran,
                           Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_SpotRangeCost set ");
            strSql.Append("Value=@Value");
            strSql.Append(" where FieldRangeID=@FieldRangeID and BreedClassID=@BreedClassID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Value", DbType.Decimal, model.Value);
            //db.AddInParameter(dbCommand, "FieldRangeID", DbType.Int32, model.FieldRangeID);
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

        #region �����ֻ�����������

        /// <summary>
        /// �����ֻ�����������
        /// </summary>
        /// <param name="model">�ֻ�����������ʵ��</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.XH_SpotRangeCost model)
        {
            return Update(model, null, null);
        }

        #endregion

        #region �����ֶη�ΧID��Ʒ��ID��ɾ���ֻ����׷���������

        /// <summary>
        /// �����ֶη�ΧID��Ʒ��ID��ɾ���ֻ����׷���������
        /// </summary>
        /// <param name="FieldRangeID">�ֶη�ΧID</param>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Delete(int FieldRangeID, int BreedClassID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete XH_SpotRangeCost ");
            strSql.Append(" where FieldRangeID=@FieldRangeID and BreedClassID=@BreedClassID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FieldRangeID", DbType.Int32, FieldRangeID);
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

        #endregion

        #region �����ֶη�ΧID��Ʒ��ID��ɾ���ֻ����׷���������

        /// <summary>
        /// �����ֶη�ΧID��Ʒ��ID��ɾ���ֻ����׷���������
        /// </summary>
        /// <param name="FieldRangeID">�ֶη�ΧID</param>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool Delete(int FieldRangeID, int BreedClassID)
        {
            return Delete(FieldRangeID, BreedClassID);
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_SpotRangeCost GetModel(int FieldRangeID, int BreedClassID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Value,FieldRangeID,BreedClassID from XH_SpotRangeCost ");
            strSql.Append(" where FieldRangeID=@FieldRangeID and BreedClassID=@BreedClassID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FieldRangeID", DbType.Int32, FieldRangeID);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
            ManagementCenter.Model.XH_SpotRangeCost model = null;
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
            strSql.Append("select Value,FieldRangeID,BreedClassID ");
            strSql.Append(" FROM XH_SpotRangeCost ");
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
        public List<ManagementCenter.Model.XH_SpotRangeCost> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select Value,FieldRangeID,BreedClassID ");
            strSql.Append("select Value,BreedClassID ");
            strSql.Append(" FROM XH_SpotRangeCost ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.XH_SpotRangeCost> list = new List<ManagementCenter.Model.XH_SpotRangeCost>();
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
        public ManagementCenter.Model.XH_SpotRangeCost ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.XH_SpotRangeCost model = new ManagementCenter.Model.XH_SpotRangeCost();
            object ojb;
            ojb = dataReader["Value"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Value = (decimal) ojb;
            }
            //ojb = dataReader["FieldRangeID"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.FieldRangeID = (int) ojb;
            //}
            ojb = dataReader["BreedClassID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassID = (int) ojb;
            }
            return model;
        }

        #region ����Ʒ��ID��ȡ�ֻ����׷��ý���������_��Χ_ֵ

        /// <summary>
        /// ����Ʒ��ID��ȡ�ֻ����׷��ý���������_��Χ_ֵ
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public DataSet GetXHSpotRangeCostByBreedClassID(int BreedClassID)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_XHSPOTRANGECOST);
            database.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);

            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

        #endregion  ��Ա����
    }
}