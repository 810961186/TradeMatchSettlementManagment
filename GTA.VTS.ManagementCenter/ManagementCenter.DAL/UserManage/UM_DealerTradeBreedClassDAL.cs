using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Maticsoft.DBUtility;//�����������
namespace ManagementCenter.DAL
{
	/// <summary>
    /// ������Ʒ������Ȩ�ޱ� ���ݷ�����UM_DealerTradeBreedClassDAL��
    /// ���ߣ�������  �޸ģ�����ΰ
    /// ���ڣ�2008-11-18  �޸����ڣ�2009-11-04
    /// </summary>
	public class UM_DealerTradeBreedClassDAL
	{
		public UM_DealerTradeBreedClassDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(DealerTradeBreedClassID)+1 from UM_DealerTradeBreedClass";
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
		public bool Exists(int DealerTradeBreedClassID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from UM_DealerTradeBreedClass where DealerTradeBreedClassID=@DealerTradeBreedClassID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "DealerTradeBreedClassID", DbType.Int32,DealerTradeBreedClassID);
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
		public int Add(ManagementCenter.Model.UM_DealerTradeBreedClass model,DbTransaction tran, Database db)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into UM_DealerTradeBreedClass(");
			strSql.Append("BreedClassID,UserID)");

			strSql.Append(" values (");
			strSql.Append("@BreedClassID,@UserID)");
			strSql.Append(";select @@IDENTITY");
			if(db==null) db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
			db.AddInParameter(dbCommand, "UserID", DbType.Int32, model.UserID);
			int result;
		    object obj;
            if(tran==null)
            {
                obj = db.ExecuteScalar(dbCommand);
            }
            else
            {
                obj = db.ExecuteScalar(dbCommand,tran);
            }
			if(!int.TryParse(obj.ToString(),out result))
			{
				return 0;
			}
			return result;
		}
        /// <summary>
        /// ���Ʒ������Ȩ��
        /// </summary>
        /// <param name="model">Ʒ������Ȩ��ʵ��</param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.UM_DealerTradeBreedClass model)
        {
            return Add(model, null, null);
        }

	    /// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.UM_DealerTradeBreedClass model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update UM_DealerTradeBreedClass set ");
			strSql.Append("BreedClassID=@BreedClassID,");
			strSql.Append("UserID=@UserID");
			strSql.Append(" where DealerTradeBreedClassID=@DealerTradeBreedClassID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
			db.AddInParameter(dbCommand, "UserID", DbType.Int32, model.UserID);
			db.AddInParameter(dbCommand, "DealerTradeBreedClassID", DbType.Int32, model.DealerTradeBreedClassID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int DealerTradeBreedClassID,DbTransaction tran, Database db)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete UM_DealerTradeBreedClass ");
			strSql.Append(" where DealerTradeBreedClassID=@DealerTradeBreedClassID ");
            if (db==null) db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "DealerTradeBreedClassID", DbType.Int32,DealerTradeBreedClassID);
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
        /// ����Ʒ��Ȩ��IDɾ��Ʒ��Ȩ����Ϣ
        /// </summary>
        /// <param name="DealerTradeBreedClassID">Ʒ��Ȩ��ID</param>
        public void Delete(int DealerTradeBreedClassID)
        {
            Delete(DealerTradeBreedClassID,null,null);
        }

        /// <summary>
        /// ����Ʒ��ID��ɾ��Ʒ��Ȩ����Ϣ
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool DeleteDealerTradeByBreedClassID(int BreedClassID, DbTransaction tran, Database db)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete UM_DealerTradeBreedClass ");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
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
        /// ����Ʒ��ID��ɾ��Ʒ��Ȩ����Ϣ
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool DeleteDealerTradeByBreedClassID(int BreedClassID)
        {
            return DeleteDealerTradeByBreedClassID(BreedClassID, null, null);
        }

	    /// <summary>
        /// ɾ�����ݸ����û�ID
        /// </summary>
        public void DeleteByUserID(int UserID,DbTransaction tran, Database db)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete UM_DealerTradeBreedClass ");
            strSql.Append(" where UserID=@UserID ");
            if(db==null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
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
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.UM_DealerTradeBreedClass GetModel(int DealerTradeBreedClassID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BreedClassID,UserID,DealerTradeBreedClassID from UM_DealerTradeBreedClass ");
			strSql.Append(" where DealerTradeBreedClassID=@DealerTradeBreedClassID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "DealerTradeBreedClassID", DbType.Int32,DealerTradeBreedClassID);
			ManagementCenter.Model.UM_DealerTradeBreedClass model=null;
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
			strSql.Append("select BreedClassID,UserID,DealerTradeBreedClassID ");
			strSql.Append(" FROM UM_DealerTradeBreedClass ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "UM_DealerTradeBreedClass");
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
		public List<ManagementCenter.Model.UM_DealerTradeBreedClass> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BreedClassID,UserID,DealerTradeBreedClassID ");
			strSql.Append(" FROM UM_DealerTradeBreedClass ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.UM_DealerTradeBreedClass> list = new List<ManagementCenter.Model.UM_DealerTradeBreedClass>();
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
		public ManagementCenter.Model.UM_DealerTradeBreedClass ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.UM_DealerTradeBreedClass model=new ManagementCenter.Model.UM_DealerTradeBreedClass();
			object ojb; 
			ojb = dataReader["BreedClassID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.BreedClassID=(int)ojb;
			}
			ojb = dataReader["UserID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.UserID=(int)ojb;
			}
			ojb = dataReader["DealerTradeBreedClassID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.DealerTradeBreedClassID=(int)ojb;
			}
			return model;
		}

        /// <summary>
        /// ��ȡ�û���Ʒ�ֽ���Ȩ��
        /// </summary>
        /// <param name="UserID">����ԱID</param>
        /// <returns></returns>
        public DataSet GetUserBreedClassRight(int UserID)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("Select D.UserID,D.DealerTradeBreedClassID,B.BreedClassID,B.BreedClassName,C.BourseTypeName,c.BourseTypeID From CM_BreedClass as B ");
            strSql.Append("right Join CM_BourseType as C On C.BourseTypeID=B.BourseTypeID ");
            strSql.Append("Left Join ");
            //strSql.Append("(Select * From UM_DealerTradeBreedClass  where UserID=@UserID) AS D On D.BreedClassID=B.BreedClassID order by c.BourseTypeID");
            strSql.Append("(Select * From UM_DealerTradeBreedClass  where UserID=@UserID) AS D On D.BreedClassID=B.BreedClassID where  B.DELETESTATE<>1 order by c.BourseTypeID");

			Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
            return db.ExecuteDataSet(dbCommand);
            
        }

	    #endregion  ��Ա����
	}
}

