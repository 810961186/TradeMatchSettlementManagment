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
    ///��������Ч�걨���� ���ݷ�����XH_ValidDeclareTypeDAL��
    ///���ߣ�����ΰ
    ///����:2008-12-2
    /// </summary>
    public class XH_ValidDeclareTypeDAL
    {
        public XH_ValidDeclareTypeDAL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(BreedClassValidID)+1 from XH_ValidDeclareType";
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
        public bool Exists(int ValidDeclareTypeID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XH_ValidDeclareType where BreedClassValidID=@BreedClassValidID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassValidID", DbType.Int32, ValidDeclareTypeID);
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
        public void Add(ManagementCenter.Model.XH_ValidDeclareType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_ValidDeclareType(");
            strSql.Append("ValidDeclareTypeID)");

            strSql.Append(" values (");
            strSql.Append("@ValidDeclareTypeID)");
            strSql.Append(";select @@IDENTITY");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            //db.AddInParameter(dbCommand, "ValidDeclareTypeID", DbType.Int32, model.ValidDeclareTypeID);
            db.AddInParameter(dbCommand, "ValidDeclareTypeID", DbType.Int32, model.ValidDeclareTypeID);
            db.ExecuteNonQuery(dbCommand);
        }

        #region ����Ʒ����Ч�걨��ʶ������Ч�걨����

        /// <summary>
        /// ����Ʒ����Ч�걨��ʶ������Ч�걨����
        /// </summary>
        /// <param name="model">��Ч�걨����ʵ��</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.XH_ValidDeclareType model, DbTransaction tran,
                           Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_ValidDeclareType set ");
            strSql.Append("ValidDeclareTypeID=@ValidDeclareTypeID");
            strSql.Append(" where BreedClassValidID=@BreedClassValidID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassValidID", DbType.Int32, model.BreedClassValidID);
            db.AddInParameter(dbCommand, "ValidDeclareTypeID", DbType.Int32, model.ValidDeclareTypeID);
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

        #region ����Ʒ����Ч�걨��ʶ������Ч�걨����

        /// <summary>
        /// ����Ʒ����Ч�걨��ʶ������Ч�걨����
        /// </summary>
        /// <param name="model">��Ч�걨����ʵ��</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.XH_ValidDeclareType model)
        {
            return Update(model, null, null);
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_ValidDeclareType GetModel(int BreedClassValidID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BreedClassValidID,ValidDeclareTypeID from XH_ValidDeclareType ");
            strSql.Append(" where BreedClassValidID=@BreedClassValidID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassValidID", DbType.Int32, BreedClassValidID);
            ManagementCenter.Model.XH_ValidDeclareType model = null;
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
            strSql.Append("select BreedClassValidID,ValidDeclareTypeID ");
            strSql.Append(" FROM XH_ValidDeclareType ");
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
        public List<ManagementCenter.Model.XH_ValidDeclareType> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BreedClassValidID,ValidDeclareTypeID ");
            strSql.Append(" FROM XH_ValidDeclareType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.XH_ValidDeclareType> list =
                new List<ManagementCenter.Model.XH_ValidDeclareType>();
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
        public ManagementCenter.Model.XH_ValidDeclareType ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.XH_ValidDeclareType model = new ManagementCenter.Model.XH_ValidDeclareType();
            object ojb;
            ojb = dataReader["BreedClassValidID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassValidID = (int) ojb;
            }

            ojb = dataReader["ValidDeclareTypeID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ValidDeclareTypeID = (int) ojb;
            }
            return model;
        }

        #endregion  ��Ա����

        #region ����һ����Ч�걨����

        /// <summary>
        /// ����һ����Ч�걨����
        /// </summary>
        public int AddValidDeclareType(ManagementCenter.Model.XH_ValidDeclareType model, DbTransaction tran,
                                       Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_ValidDeclareType(");
            strSql.Append("ValidDeclareTypeID)");

            strSql.Append(" values (");
            strSql.Append("@ValidDeclareTypeID)");
            strSql.Append(";select @@IDENTITY");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ValidDeclareTypeID", DbType.Int32, model.ValidDeclareTypeID);
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

            // db.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region ����һ����Ч�걨����(����,û������)

        /// <summary>
        /// ����һ����Ч�걨����(����,û������)
        /// </summary>
        public int AddValidDeclareType(ManagementCenter.Model.XH_ValidDeclareType model)
        {
            return AddValidDeclareType(model, null, null);
        }

        #endregion

        #region ����Ʒ����Ч�걨��ʶɾ����Ч�걨����

        /// <summary>
        /// ����Ʒ����Ч�걨��ʶɾ����Ч�걨����
        /// </summary>
        /// <param name="BreedClassValidID">Ʒ����Ч�걨��ʶ</param>
        /// <param name="tran">����</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool DeleteValidDeclareType(int BreedClassValidID, DbTransaction tran,
                                           Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete XH_ValidDeclareType ");
            strSql.Append(" where BreedClassValidID=@BreedClassValidID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassValidID", DbType.Int32, BreedClassValidID);
            // db.ExecuteNonQuery(dbCommand);
            //int result;
            //object obj;
            if (tran == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
            //if (!int.TryParse(obj.ToString(), out result))
            //{
            //    return 0;
            //}
            return true;
        }

        #endregion

        #region ����Ʒ����Ч�걨��ʶɾ����Ч�걨����(������)

        /// <summary>
        /// ����Ʒ����Ч�걨��ʶɾ����Ч�걨����
        /// </summary>
        /// <param name="BreedClassValidID">Ʒ����Ч�걨��ʶ</param>
        /// <returns></returns>
        public bool DeleteValidDeclareType(int BreedClassValidID)
        {
            return DeleteValidDeclareType(BreedClassValidID, null, null);
        }

        #endregion
    }
}