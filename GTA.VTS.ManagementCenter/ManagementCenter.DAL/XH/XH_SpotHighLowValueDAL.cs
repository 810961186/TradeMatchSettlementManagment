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
    ///�������ֻ�_Ʒ��_�ǵ��� ���ݷ�����XH_SpotHighLowValue��
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class XH_SpotHighLowValueDAL
    {
        public XH_SpotHighLowValueDAL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(HightLowValueID)+1 from XH_SpotHighLowValue";
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
        public bool Exists(int HightLowValueID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XH_SpotHighLowValue where HightLowValueID=@HightLowValueID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "HightLowValueID", DbType.Int32, HightLowValueID);
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
        public void Add(ManagementCenter.Model.XH_SpotHighLowValue model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_SpotHighLowValue(");
            strSql.Append(
                "FundYestClosePriceScale,RightHighLowScale,NormalValue,StValue,BreedClassHighLowID)");

            strSql.Append(" values (");
            strSql.Append(
                "@FundYestClosePriceScale,@RightHighLowScale,@NormalValue,@StValue,@BreedClassHighLowID)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            //  db.AddInParameter(dbCommand, "HightLowValueID", DbType.Int32, model.HightLowValueID);
            db.AddInParameter(dbCommand, "FundYestClosePriceScale", DbType.Decimal, model.FundYestClosePriceScale);
            db.AddInParameter(dbCommand, "RightHighLowScale", DbType.Decimal, model.RightHighLowScale);
            db.AddInParameter(dbCommand, "NormalValue", DbType.Decimal, model.NormalValue);
            db.AddInParameter(dbCommand, "StValue", DbType.Decimal, model.StValue);
            db.AddInParameter(dbCommand, "BreedClassHighLowID", DbType.Int32, model.BreedClassHighLowID);
            db.ExecuteNonQuery(dbCommand);
        }


        #region �õ�һ������ʵ��
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_SpotHighLowValue GetModel(int HightLowValueID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select HightLowValueID,FundYestClosePriceScale,RightHighLowScale,NormalValue,StValue,BreedClassHighLowID from XH_SpotHighLowValue ");
            strSql.Append(" where HightLowValueID=@HightLowValueID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "HightLowValueID", DbType.Int32, HightLowValueID);
            ManagementCenter.Model.XH_SpotHighLowValue model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }
        #endregion
        #region ��������б�

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select HightLowValueID,FundYestClosePriceScale,RightHighLowScale,NormalValue,StValue,BreedClassHighLowID ");
            strSql.Append(" FROM XH_SpotHighLowValue ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        #endregion

        #region ��������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�

        /// <summary>
        /// ��������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        public List<ManagementCenter.Model.XH_SpotHighLowValue> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select HightLowValueID,FundYestClosePriceScale,RightHighLowScale,NormalValue,StValue,BreedClassHighLowID ");
            strSql.Append(" FROM XH_SpotHighLowValue ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.XH_SpotHighLowValue> list =
                new List<ManagementCenter.Model.XH_SpotHighLowValue>();
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

        #endregion

        #region ����ʵ�������

        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.XH_SpotHighLowValue ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.XH_SpotHighLowValue model = new ManagementCenter.Model.XH_SpotHighLowValue();
            object ojb;
            ojb = dataReader["HightLowValueID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HightLowValueID = (int) ojb;
            }
            ojb = dataReader["FundYestClosePriceScale"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FundYestClosePriceScale = (decimal) ojb;
            }
            ojb = dataReader["RightHighLowScale"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RightHighLowScale = (decimal) ojb;
            }
            ojb = dataReader["NormalValue"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.NormalValue = (decimal) ojb;
            }
            ojb = dataReader["StValue"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.StValue = (decimal) ojb;
            }
            ojb = dataReader["BreedClassHighLowID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassHighLowID = (int) ojb;
            }
            return model;
        }

        #endregion

        #endregion  ��Ա����

        //================================  �޸ĺ��  ���� ================================

        #region ����ֻ��ǵ���

        /// <summary>
        /// ����ֻ��ǵ���
        /// </summary>
        /// <param name="model">�ֻ��ǵ���ʵ��</param>
        /// <param name="tran">����</param>
        /// <param name="db">��������</param>
        /// <returns></returns>
        public int AddXHSpotHighLowValue(ManagementCenter.Model.XH_SpotHighLowValue model, DbTransaction tran,
                                         Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_SpotHighLowValue(");
            strSql.Append(
                "FundYestClosePriceScale,RightHighLowScale,NormalValue,StValue,BreedClassHighLowID)");

            strSql.Append(" values (");
            strSql.Append(
                "@FundYestClosePriceScale,@RightHighLowScale,@NormalValue,@StValue,@BreedClassHighLowID)");
            strSql.Append(";select @@IDENTITY");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            // db.AddInParameter(dbCommand, "HightLowValueID", DbType.Int32, model.HightLowValueID);
            if (model.FundYestClosePriceScale == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "FundYestClosePriceScale", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "FundYestClosePriceScale", DbType.Decimal, model.FundYestClosePriceScale);
            }
            if (model.RightHighLowScale == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "RightHighLowScale", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "RightHighLowScale", DbType.Decimal, model.RightHighLowScale);
            }
            if (model.NormalValue == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "NormalValue", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "NormalValue", DbType.Decimal, model.NormalValue);
            }
            if (model.StValue == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "StValue", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "StValue", DbType.Decimal, model.StValue);
            }
            db.AddInParameter(dbCommand, "BreedClassHighLowID", DbType.Int32, model.BreedClassHighLowID);
            //db.ExecuteNonQuery(dbCommand);
            //int result;
            object obj;
            if (tran == null)
            {
                obj = db.ExecuteScalar(dbCommand);
            }
            else
            {
                obj = db.ExecuteScalar(dbCommand, tran);
            }

            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
            {
                return AppGlobalVariable.INIT_INT;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        #endregion

        #region  ����ֻ��ǵ���(����,������)

        /// <summary>
        /// ����ֻ��ǵ���(����,������)
        /// </summary>
        /// <param name="model">�ֻ��ǵ���ʵ��</param>
        /// <returns></returns>
        public int AddXHSpotHighLowValue(ManagementCenter.Model.XH_SpotHighLowValue model)
        {
            return AddXHSpotHighLowValue(model, null, null);
        }

        #endregion

        #region ����Ʒ���ǵ�����ʶɾ��Ʒ���ǵ���ȡֵ

        /// <summary>
        /// ����Ʒ���ǵ�����ʶɾ��Ʒ���ǵ���ȡֵ
        /// </summary>
        /// <param name="BreedClassHighLowID">Ʒ���ǵ�����ʶ</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool DeleteSpotHighLowValue(int BreedClassHighLowID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete XH_SpotHighLowValue ");
            strSql.Append(" where BreedClassHighLowID=@BreedClassHighLowID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassHighLowID", DbType.Int32, BreedClassHighLowID);
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

        #region ����Ʒ���ǵ�����ʶɾ��Ʒ���ǵ���ȡֵ

        /// <summary>
        /// ����Ʒ���ǵ�����ʶɾ��Ʒ���ǵ���ȡֵ
        /// </summary>
        /// <param name="BreedClassHighLowID">Ʒ���ǵ�����ʶ</param>
        /// <returns></returns>
        public bool DeleteSpotHighLowValueByBCHighLowID(int BreedClassHighLowID)
        {
            return DeleteSpotHighLowValue(BreedClassHighLowID, null, null);
        }

        #endregion

        #region �����ǵ���ȡֵ

        /// <summary>
        /// �����ǵ���ȡֵ
        /// </summary>
        /// <param name="model">�ǵ���ȡֵʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.XH_SpotHighLowValue model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_SpotHighLowValue set ");
            strSql.Append("FundYestClosePriceScale=@FundYestClosePriceScale,");
            strSql.Append("RightHighLowScale=@RightHighLowScale,");
            strSql.Append("NormalValue=@NormalValue,");
            strSql.Append("StValue=@StValue");
            //strSql.Append("BreedClassHighLowID=@BreedClassHighLowID");
            //strSql.Append(" where HightLowValueID=@HightLowValueID ");
            strSql.Append(" where BreedClassHighLowID=@BreedClassHighLowID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            // db.AddInParameter(dbCommand, "HightLowValueID", DbType.Int32, model.HightLowValueID);
            if (model.FundYestClosePriceScale == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "FundYestClosePriceScale", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "FundYestClosePriceScale", DbType.Decimal, model.FundYestClosePriceScale);
            }
            if (model.RightHighLowScale == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "RightHighLowScale", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "RightHighLowScale", DbType.Decimal, model.RightHighLowScale);
            }
            if (model.NormalValue == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "NormalValue", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "NormalValue", DbType.Decimal, model.NormalValue);
            }
            if (model.StValue == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "StValue", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "StValue", DbType.Decimal, model.StValue);
            }
            //db.AddInParameter(dbCommand, "FundYestClosePriceScale", DbType.Decimal, model.FundYestClosePriceScale);
            //db.AddInParameter(dbCommand, "RightHighLowScale", DbType.Decimal, model.RightHighLowScale);
            //db.AddInParameter(dbCommand, "NormalValue", DbType.Decimal, model.NormalValue);
            //db.AddInParameter(dbCommand, "StValue", DbType.Decimal, model.StValue);
            db.AddInParameter(dbCommand, "BreedClassHighLowID", DbType.Int32, model.BreedClassHighLowID);
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

        #region �����ǵ���ȡֵ

        /// <summary>
        /// �����ǵ���ȡֵ
        /// </summary>
        /// <param name="model">�ǵ���ȡֵʵ��</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.XH_SpotHighLowValue model)
        {
            return Update(model, null, null);
        }

        #endregion

        #region  ����Ʒ���ǵ�����ʶ�õ�һ������ʵ��

        /// <summary>
        /// ����Ʒ���ǵ�����ʶ�õ�һ������ʵ��
        /// </summary>
        /// <param name="BreedClassHighLowID">Ʒ���ǵ�����ʶ</param>
        /// <returns></returns>
        public ManagementCenter.Model.XH_SpotHighLowValue GetModelByBCHighLowID(int BreedClassHighLowID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select HightLowValueID,FundYestClosePriceScale,RightHighLowScale,NormalValue,StValue,BreedClassHighLowID from XH_SpotHighLowValue ");
            strSql.Append(" where BreedClassHighLowID=@BreedClassHighLowID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassHighLowID", DbType.Int32, BreedClassHighLowID);
            ManagementCenter.Model.XH_SpotHighLowValue model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }

        #endregion
    }
}