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
    ///�������ֻ�_������ƷƷ��_�ֲ����� ���ݷ�����XH_SpotPositionDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class XH_SpotPositionDAL
    {
        public XH_SpotPositionDAL()
        {
        }

        #region SQL

        /// <summary>
        /// ���ݲ�ѯ���������ֻ��ֲ����� 
        /// </summary>
        private string SQL_SELECT_XHSPOTPOSITION =
            @"SELECT B.BREEDCLASSNAME,A.* FROM XH_SPOTPOSITION AS A,CM_BREEDCLASS AS B 
                                                            WHERE A.BREEDCLASSID=B.BREEDCLASSID ";

        /// <summary>
        /// �����ֻ�������ƷƷ��_�ֲ����Ʊ��е�Ʒ�ֱ�ʶ��ȡƷ������
        /// </summary>
        private string SQL_SELECTBREEDCLASSNAME_XHSPOTPOSITION =
                                                            @"SELECT A.BREEDCLASSID,A.BREEDCLASSNAME 
                                                FROM CM_BREEDCLASS A,XH_SPOTPOSITION B 
                                                WHERE A.BREEDCLASSID=B.BREEDCLASSID";

        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(BreedClassID)+1 from XH_SpotPosition";
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
            strSql.Append("select count(1) from XH_SpotPosition where BreedClassID=@BreedClassID ");
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
        public bool Add(ManagementCenter.Model.XH_SpotPosition model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_SpotPosition(");
            strSql.Append("Rate,BreedClassID)");

            strSql.Append(" values (");
            strSql.Append("@Rate,@BreedClassID)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Rate", DbType.Decimal, model.Rate);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
            db.ExecuteNonQuery(dbCommand);
            return true;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(ManagementCenter.Model.XH_SpotPosition model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_SpotPosition set ");
            strSql.Append("Rate=@Rate");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Rate", DbType.Decimal, model.Rate);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
            db.ExecuteNonQuery(dbCommand);
            return true;
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int BreedClassID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete XH_SpotPosition ");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            if(db==null)
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
        /// ����Ʒ��ID��ɾ���ֲ�����
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
        public ManagementCenter.Model.XH_SpotPosition GetModel(int BreedClassID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Rate,BreedClassID from XH_SpotPosition ");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
            ManagementCenter.Model.XH_SpotPosition model = null;
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
            strSql.Append("select Rate,BreedClassID ");
            strSql.Append(" FROM XH_SpotPosition ");
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
        public List<ManagementCenter.Model.XH_SpotPosition> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Rate,BreedClassID ");
            strSql.Append(" FROM XH_SpotPosition ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.XH_SpotPosition> list = new List<ManagementCenter.Model.XH_SpotPosition>();
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

        #region ��ȡ�����ֻ�_������ƷƷ��_�ֲ�����

        /// <summary>
        /// ��ȡ�����ֻ�_������ƷƷ��_�ֲ�����
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllXHSpotPosition(string BreedClassName, int pageNo, int pageSize,
                                            out int rowCount)
        {
            //������ѯ
            if (BreedClassName != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(BreedClassName))
            {
                SQL_SELECT_XHSPOTPOSITION += "AND (BreedClassName LIKE  '%' + @BreedClassName + '%') ";
            }

            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_XHSPOTPOSITION);

            if (BreedClassName != AppGlobalVariable.INIT_STRING && BreedClassName != string.Empty)
            {
                database.AddInParameter(dbCommand, "BreedClassName", DbType.String, BreedClassName);
            }

            return CommPager.QueryPager(database, dbCommand, SQL_SELECT_XHSPOTPOSITION, pageNo, pageSize,
                                        out rowCount, "XH_SpotPosition");
        }

        #endregion


        #region �����ֻ�������ƷƷ��_�ֲ����Ʊ��е�Ʒ��ID��ȡƷ������

        /// <summary>
        /// �����ֻ�������ƷƷ��_�ֲ����Ʊ��е�Ʒ��ID��ȡƷ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpotPositionBreedClassName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBREEDCLASSNAME_XHSPOTPOSITION);
            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.XH_SpotPosition ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.XH_SpotPosition model = new ManagementCenter.Model.XH_SpotPosition();
            object ojb;
            ojb = dataReader["Rate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Rate = (decimal) ojb;
            }
            ojb = dataReader["BreedClassID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassID = (int) ojb;
            }
            return model;
        }

        #endregion  ��Ա����
    }
}