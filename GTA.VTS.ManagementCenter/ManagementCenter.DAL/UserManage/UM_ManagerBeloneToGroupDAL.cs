using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Maticsoft.DBUtility;//�����������
namespace ManagementCenter.DAL
{
	/// <summary>
    /// ����������Ա_������� ���ݷ�����UM_ManagerBeloneToGroupDAL��
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
	public class UM_ManagerBeloneToGroupDAL
	{
		public UM_ManagerBeloneToGroupDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(UserID)+1 from UM_ManagerBeloneToGroup";
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
		public bool Exists(int UserID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from UM_ManagerBeloneToGroup where UserID=@UserID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "UserID", DbType.Int32,UserID);
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
        public void Add(ManagementCenter.Model.UM_ManagerBeloneToGroup model,DbTransaction tran,Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UM_ManagerBeloneToGroup(");
            strSql.Append("UserID,ManagerGroupID)");

            strSql.Append(" values (");
            strSql.Append("@UserID,@ManagerGroupID)");

            if(db==null) db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.Int32, model.UserID);
            db.AddInParameter(dbCommand, "ManagerGroupID", DbType.Int32, model.ManagerGroupID);
            if(tran==null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand,tran);
            }  
        }
        /// <summary>
        /// ��ӹ���Ա��������Ϣ
        /// </summary>
        /// <param name="model">����Ա������ʵ��</param>
        public void Add(ManagementCenter.Model.UM_ManagerBeloneToGroup model)
        {
            Add(model, null, null);
        }

	    /// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.UM_ManagerBeloneToGroup model,DbTransaction tran,Database db)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update UM_ManagerBeloneToGroup set ");
			strSql.Append("ManagerGroupID=@ManagerGroupID");
			strSql.Append(" where UserID=@UserID ");
			if(db==null) db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "UserID", DbType.Int32, model.UserID);
			db.AddInParameter(dbCommand, "ManagerGroupID", DbType.Int32, model.ManagerGroupID);
            if (tran==null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand,tran);
            }
		}

        /// <summary>
        /// ���¹���Ա������
        /// </summary>
        /// <param name="model">����Ա������ʵ��</param>
        public void Update(ManagementCenter.Model.UM_ManagerBeloneToGroup model)
        {
            Update(model,null,null);
        }

	    /// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int UserID,DbTransaction tran,Database db)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete UM_ManagerBeloneToGroup ");
			strSql.Append(" where UserID=@UserID ");
			if(db==null) db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "UserID", DbType.Int32,UserID);

            if(tran==null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand,tran);
            }	       
		}
        /// <summary>
        /// �����û�IDɾ������Ա������
        /// </summary>
        /// <param name="UserID"></param>
        public void  Delete(int UserID)
        {
            Delete(UserID, null, null);
        }

	    /// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.UM_ManagerBeloneToGroup GetModel(int UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserID,ManagerGroupID from UM_ManagerBeloneToGroup ");
			strSql.Append(" where UserID=@UserID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "UserID", DbType.Int32,UserID);
			ManagementCenter.Model.UM_ManagerBeloneToGroup model=null;
			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				if(dataReader.Read())
				{
					model=ReaderBind(dataReader);
				}
			}
			return model;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserID,ManagerGroupID ");
			strSql.Append(" FROM UM_ManagerBeloneToGroup ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			Database db = DatabaseFactory.CreateDatabase();
			return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
		}

		/*
		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("UP_GetRecordByPage");
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "UM_ManagerBeloneToGroup");
			db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "ID");
			db.AddInParameter(dbCommand, "PageSize", DbType.Int32, PageSize);
			db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
			db.AddInParameter(dbCommand, "IsReCount", DbType.Boolean, 0);
			db.AddInParameter(dbCommand, "OrderType", DbType.Boolean, 0);
			db.AddInParameter(dbCommand, "strWhere", DbType.AnsiString, strWhere);
			return db.ExecuteDataSet(dbCommand);
		}*/

		/// <summary>
		/// ��������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
		/// </summary>
		public List<ManagementCenter.Model.UM_ManagerBeloneToGroup> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserID,ManagerGroupID ");
			strSql.Append(" FROM UM_ManagerBeloneToGroup ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.UM_ManagerBeloneToGroup> list = new List<ManagementCenter.Model.UM_ManagerBeloneToGroup>();
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
		public ManagementCenter.Model.UM_ManagerBeloneToGroup ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.UM_ManagerBeloneToGroup model=new ManagementCenter.Model.UM_ManagerBeloneToGroup();
			object ojb; 
			ojb = dataReader["UserID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.UserID=(int)ojb;
			}
			ojb = dataReader["ManagerGroupID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ManagerGroupID=(int)ojb;
			}
			return model;
		}

		#endregion  ��Ա����
	}
}

