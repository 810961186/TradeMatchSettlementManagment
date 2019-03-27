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
    /// ����������Ա�� ���ݷ�����UM_ManagerGroupDAL��
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
    public class UM_ManagerGroupDAL
    {
        public UM_ManagerGroupDAL()
        { }
        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(ManagerGroupID)+1 from UM_ManagerGroup";
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
        public bool Exists(int ManagerGroupID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UM_ManagerGroup where ManagerGroupID=@ManagerGroupID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ManagerGroupID", DbType.Int32, ManagerGroupID);
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
        public int Add(ManagementCenter.Model.UM_ManagerGroup model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UM_ManagerGroup(");
            strSql.Append("ManagerGroupName)");

            strSql.Append(" values (");
            strSql.Append("@ManagerGroupName)");
            strSql.Append(";select @@IDENTITY");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ManagerGroupName", DbType.String, model.ManagerGroupName);
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
        }
        /// <summary>
        /// ��ӹ���Ա��
        /// </summary>
        /// <param name="model">����Ա��ʵ��</param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.UM_ManagerGroup model)
        {
            return Add(model, null, null);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.UM_ManagerGroup model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UM_ManagerGroup set ");
            strSql.Append("ManagerGroupName=@ManagerGroupName");
            strSql.Append(" where ManagerGroupID=@ManagerGroupID ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ManagerGroupID", DbType.Int32, model.ManagerGroupID);
            db.AddInParameter(dbCommand, "ManagerGroupName", DbType.String, model.ManagerGroupName);
            if (tran == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
        }
        /// <summary>
        /// ���¹���Ա��
        /// </summary>
        /// <param name="model">����Ա��ʵ��</param>
        public void Update(ManagementCenter.Model.UM_ManagerGroup model)
        {
            Update(model, null, null);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int ManagerGroupID, DbTransaction tran, Database db)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete UM_ManagerGroup ");
            strSql.Append(" where ManagerGroupID=@ManagerGroupID ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ManagerGroupID", DbType.Int32, ManagerGroupID);
            if (tran == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
        }
        /// <summary>
        /// ���ݹ���Ա����IDɾ��
        /// </summary>
        /// <param name="ManagerGroupID">����Ա��ID</param>
        public void Delete(int ManagerGroupID)
        {
            Delete(ManagerGroupID, null, null);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.UM_ManagerGroup GetModel(int ManagerGroupID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ManagerGroupID,ManagerGroupName from UM_ManagerGroup ");
            strSql.Append(" where ManagerGroupID=@ManagerGroupID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ManagerGroupID", DbType.Int32, ManagerGroupID);
            ManagementCenter.Model.UM_ManagerGroup model = null;
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
            strSql.Append("select ManagerGroupID,ManagerGroupName ");
            strSql.Append(" FROM UM_ManagerGroup ");
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
        public List<ManagementCenter.Model.UM_ManagerGroup> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ManagerGroupID,ManagerGroupName ");
            strSql.Append(" FROM UM_ManagerGroup ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.UM_ManagerGroup> list = new List<ManagementCenter.Model.UM_ManagerGroup>();
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
        public ManagementCenter.Model.UM_ManagerGroup ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.UM_ManagerGroup model = new ManagementCenter.Model.UM_ManagerGroup();
            object ojb;
            ojb = dataReader["ManagerGroupID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ManagerGroupID = (int)ojb;
            }
            model.ManagerGroupName = dataReader["ManagerGroupName"].ToString();
            return model;
        }

        #endregion  ��Ա����


        #region ��ҳ��ѯȨ����

        /// <summary>
        /// ��ҳ��ѯȨ����
        /// </summary>
        /// <param name="strwhere">����</param>
        /// <param name="pageNo">�ڼ�ҳ</param>
        /// <param name="pageSize">ÿҳ��С</param>
        /// <param name="rowCount">�ܼ�¼��</param>
        /// <returns>����DataSet</returns>
        public DataSet GetPagingManagerGroup(string strwhere, int pageNo, int pageSize, out int rowCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ManagerGroupID,ManagerGroupName ");
            strSql.Append(" FROM UM_ManagerGroup ");
            if (strwhere.Trim() != "")
            {
                strSql.Append(" where " + strwhere);
            }
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(strSql.ToString());

            return CommPager.QueryPager(database, dbCommand, strSql.ToString(), pageNo, pageSize, out rowCount, "UM_ManagerGroup");

        }
        #endregion

    }
}

