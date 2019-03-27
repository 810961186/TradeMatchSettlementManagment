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
    ///��������Ч�걨ȡֵ ���ݷ�����XH_ValidDeclareValue��
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class XH_ValidDeclareValueDAL
    {
        public XH_ValidDeclareValueDAL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(ValidDeclareValueID)+1 from XH_ValidDeclareValue";
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
        public bool Exists(int ValidDeclareValueID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XH_ValidDeclareValue where ValidDeclareValueID=@ValidDeclareValueID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ValidDeclareValueID", DbType.Int32, ValidDeclareValueID);
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
        public bool Add(ManagementCenter.Model.XH_ValidDeclareValue model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_ValidDeclareValue(");
            strSql.Append("UpperLimit,LowerLimit,NewDayUpperLimit,NewDayLowerLimit,BreedClassValidID)");

            strSql.Append(" values (");
            strSql.Append("@UpperLimit,@LowerLimit,@NewDayUpperLimit,@NewDayLowerLimit,@BreedClassValidID)");
            strSql.Append(";select @@IDENTITY");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            if (model.UpperLimit == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "UpperLimit", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "UpperLimit", DbType.Decimal, model.UpperLimit);
            }
            if(model.UpperLimit==AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "LowerLimit", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "LowerLimit", DbType.Decimal, model.LowerLimit);
            }
            //db.AddInParameter(dbCommand, "IsMarketNewDay", DbType.Int32, model.IsMarketNewDay);
            if (model.NewDayUpperLimit == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "NewDayUpperLimit", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "NewDayUpperLimit", DbType.Decimal, model.NewDayUpperLimit);
            }
            if (model.NewDayLowerLimit == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "NewDayLowerLimit", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "NewDayLowerLimit", DbType.Decimal, model.NewDayLowerLimit);
            }
            db.AddInParameter(dbCommand, "BreedClassValidID", DbType.Int32, model.BreedClassValidID);
            if (tran == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
            return true;
            //  return obj;
        }

        /// <summary>
        /// �����Ч�걨(����,������)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(ManagementCenter.Model.XH_ValidDeclareValue model)
        {
            return Add(model, null, null);
        }

        #region ������Ч�걨ȡֵ

        /// <summary>
        /// ������Ч�걨ȡֵ
        /// </summary>
        /// <param name="model">��Ч�걨ȡֵʵ����</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.XH_ValidDeclareValue model, DbTransaction tran,
                           Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_ValidDeclareValue set ");
            strSql.Append("UpperLimit=@UpperLimit,");
            strSql.Append("LowerLimit=@LowerLimit,");
            strSql.Append("NewDayUpperLimit=@NewDayUpperLimit,");
            strSql.Append("NewDayLowerLimit=@NewDayLowerLimit");
            strSql.Append(" where BreedClassValidID=@BreedClassValidID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            if (model.UpperLimit == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "UpperLimit", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "UpperLimit", DbType.Decimal, model.UpperLimit);
            }
            if (model.UpperLimit == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "LowerLimit", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "LowerLimit", DbType.Decimal, model.LowerLimit);
            }
            if (model.NewDayUpperLimit == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "NewDayUpperLimit", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "NewDayUpperLimit", DbType.Decimal, model.NewDayUpperLimit);
            }
            if (model.NewDayLowerLimit == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "NewDayLowerLimit", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "NewDayLowerLimit", DbType.Decimal, model.NewDayLowerLimit);
            }
            db.AddInParameter(dbCommand, "BreedClassValidID", DbType.Int32, model.BreedClassValidID);
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

        #region ������Ч�걨ȡֵ

        /// <summary>
        /// ������Ч�걨ȡֵ
        /// </summary>
        /// <param name="model">��Ч�걨ȡֵʵ����</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.XH_ValidDeclareValue model)
        {
            return Update(model, null, null);
        }

        #endregion

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int ValidDeclareValueID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete XH_ValidDeclareValue ");
            strSql.Append(" where ValidDeclareValueID=@ValidDeclareValueID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ValidDeclareValueID", DbType.Int32, ValidDeclareValueID);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_ValidDeclareValue GetModel(int ValidDeclareValueID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select ValidDeclareValueID,UpperLimit,LowerLimit,NewDayUpperLimit,NewDayLowerLimit,BreedClassValidID from XH_ValidDeclareValue ");
            strSql.Append(" where ValidDeclareValueID=@ValidDeclareValueID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ValidDeclareValueID", DbType.Int32, ValidDeclareValueID);
            ManagementCenter.Model.XH_ValidDeclareValue model = null;
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
            strSql.Append("select ValidDeclareValueID,UpperLimit,LowerLimit,NewDayUpperLimit,NewDayLowerLimit,BreedClassValidID ");
            strSql.Append(" FROM XH_ValidDeclareValue ");
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
        public List<ManagementCenter.Model.XH_ValidDeclareValue> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ValidDeclareValueID,UpperLimit,LowerLimit,NewDayUpperLimit,NewDayLowerLimit,BreedClassValidID ");
            strSql.Append(" FROM XH_ValidDeclareValue ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.XH_ValidDeclareValue> list =
                new List<ManagementCenter.Model.XH_ValidDeclareValue>();
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
        public ManagementCenter.Model.XH_ValidDeclareValue ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.XH_ValidDeclareValue model = new ManagementCenter.Model.XH_ValidDeclareValue();
            object ojb;
            ojb = dataReader["ValidDeclareValueID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ValidDeclareValueID = (int) ojb;
            }
            ojb = dataReader["UpperLimit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UpperLimit = (decimal) ojb;
            }
            ojb = dataReader["LowerLimit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LowerLimit = (decimal) ojb;
            }
            ojb = dataReader["NewDayUpperLimit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.NewDayUpperLimit = (decimal)ojb;
            }
            ojb = dataReader["NewDayLowerLimit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.NewDayLowerLimit = (decimal)ojb;
            }
            ojb = dataReader["BreedClassValidID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassValidID = (int) ojb;
            }
            return model;
        }

        #endregion  ��Ա����

        #region  ����Ʒ����Ч�걨��ʶɾ����Ч�걨ȡֵ

        /// <summary>
        /// ����Ʒ����Ч�걨��ʶɾ����Ч�걨ȡֵ
        /// </summary>
        /// <param name="BreedClassValidID">Ʒ����Ч�걨��ʶ</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool DeleteVDeclareValue(int BreedClassValidID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete XH_ValidDeclareValue ");
            strSql.Append(" where BreedClassValidID=@BreedClassValidID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassValidID", DbType.Int32, BreedClassValidID);
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

        #region ����Ʒ����Ч�걨��ʶɾ����Ч�걨ȡֵ(������)

        /// <summary>
        /// ����Ʒ����Ч�걨��ʶɾ����Ч�걨ȡֵ
        /// </summary>
        /// <param name="BreedClassValidID">Ʒ����Ч�걨��ʶ</param>
        /// <returns></returns>
        public bool DeleteVDeclareValueByBCValidID(int BreedClassValidID)
        {
            return DeleteVDeclareValue(BreedClassValidID, null, null);
        }

        #endregion

        #region ����Ʒ����Ч�걨��ʶ��ȡ��Ч�걨ȡֵʵ��
        /// <summary>
       /// ����Ʒ����Ч�걨��ʶ��ȡ��Ч�걨ȡֵʵ��
       /// </summary>
       /// <param name="BreedClassValidID">Ʒ����Ч�걨��ʶ</param>
       /// <returns></returns>
        public ManagementCenter.Model.XH_ValidDeclareValue GetModelValidDeclareValue(int BreedClassValidID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select ValidDeclareValueID,UpperLimit,LowerLimit,NewDayUpperLimit,NewDayLowerLimit,BreedClassValidID from XH_ValidDeclareValue ");
            strSql.Append(" where BreedClassValidID=@BreedClassValidID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassValidID", DbType.Int32, BreedClassValidID);
            ManagementCenter.Model.XH_ValidDeclareValue model = null;
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