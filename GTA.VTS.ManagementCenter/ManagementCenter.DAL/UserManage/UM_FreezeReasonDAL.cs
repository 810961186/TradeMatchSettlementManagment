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
    /// �������ʽ𶳽��  ���ݷ�����UM_FreezeReasonDAL��
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
	public class UM_FreezeReasonDAL
	{
		public UM_FreezeReasonDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(FreezeReasonID)+1 from UM_FreezeReason";
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
		public bool Exists(int FreezeReasonID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from UM_FreezeReason where FreezeReasonID=@FreezeReasonID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "FreezeReasonID", DbType.Int32,FreezeReasonID);
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
		public int Add(ManagementCenter.Model.UM_FreezeReason model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into UM_FreezeReason(");
			strSql.Append("FreezeReason,FreezeReasonTime,ThawReasonTime,DealerAccoutID,IsAuto)");

			strSql.Append(" values (");
			strSql.Append("@FreezeReason,@FreezeReasonTime,@ThawReasonTime,@DealerAccoutID,@IsAuto)");
			strSql.Append(";select @@IDENTITY");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "FreezeReason", DbType.String, model.FreezeReason);
			db.AddInParameter(dbCommand, "FreezeReasonTime", DbType.DateTime, model.FreezeReasonTime);
			db.AddInParameter(dbCommand, "ThawReasonTime", DbType.DateTime, model.ThawReasonTime);
			db.AddInParameter(dbCommand, "DealerAccoutID", DbType.String, model.DealerAccoutID);
			db.AddInParameter(dbCommand, "IsAuto", DbType.Byte, model.IsAuto);
			int result;
			object obj = db.ExecuteScalar(dbCommand);
			if(!int.TryParse(obj.ToString(),out result))
			{
				return 0;
			}
			return result;
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.UM_FreezeReason model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update UM_FreezeReason set ");
			strSql.Append("FreezeReason=@FreezeReason,");
			strSql.Append("FreezeReasonTime=@FreezeReasonTime,");
			strSql.Append("ThawReasonTime=@ThawReasonTime,");
			strSql.Append("DealerAccoutID=@DealerAccoutID,");
			strSql.Append("IsAuto=@IsAuto");
			strSql.Append(" where FreezeReasonID=@FreezeReasonID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "FreezeReasonID", DbType.Int32, model.FreezeReasonID);
			db.AddInParameter(dbCommand, "FreezeReason", DbType.String, model.FreezeReason);
			db.AddInParameter(dbCommand, "FreezeReasonTime", DbType.DateTime, model.FreezeReasonTime);
			db.AddInParameter(dbCommand, "ThawReasonTime", DbType.DateTime, model.ThawReasonTime);
			db.AddInParameter(dbCommand, "DealerAccoutID", DbType.String, model.DealerAccoutID);
			db.AddInParameter(dbCommand, "IsAuto", DbType.Byte, model.IsAuto);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int FreezeReasonID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete UM_FreezeReason ");
			strSql.Append(" where FreezeReasonID=@FreezeReasonID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "FreezeReasonID", DbType.Int32,FreezeReasonID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.UM_FreezeReason GetModel(int FreezeReasonID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FreezeReasonID,FreezeReason,FreezeReasonTime,ThawReasonTime,DealerAccoutID,IsAuto from UM_FreezeReason ");
			strSql.Append(" where FreezeReasonID=@FreezeReasonID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "FreezeReasonID", DbType.Int32,FreezeReasonID);
			ManagementCenter.Model.UM_FreezeReason model=null;
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
			strSql.Append("select FreezeReasonID,FreezeReason,FreezeReasonTime,ThawReasonTime,DealerAccoutID,IsAuto ");
			strSql.Append(" FROM UM_FreezeReason ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "UM_FreezeReason");
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
		public List<ManagementCenter.Model.UM_FreezeReason> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FreezeReasonID,FreezeReason,FreezeReasonTime,ThawReasonTime,DealerAccoutID,IsAuto ");
			strSql.Append(" FROM UM_FreezeReason ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.UM_FreezeReason> list = new List<ManagementCenter.Model.UM_FreezeReason>();
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
		public ManagementCenter.Model.UM_FreezeReason ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.UM_FreezeReason model=new ManagementCenter.Model.UM_FreezeReason();
			object ojb; 
			ojb = dataReader["FreezeReasonID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.FreezeReasonID=(int)ojb;
			}
			model.FreezeReason=dataReader["FreezeReason"].ToString();
			ojb = dataReader["FreezeReasonTime"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.FreezeReasonTime=(DateTime)ojb;
			}
			ojb = dataReader["ThawReasonTime"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ThawReasonTime=(DateTime)ojb;
			}
			model.DealerAccoutID=dataReader["DealerAccoutID"].ToString();
			ojb = dataReader["IsAuto"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.IsAuto=(int)ojb;
			}
			return model;
		}


        /// <summary>
        /// ɾ����¼���ݽ���ԱID
        /// </summary>
        public void DeleteByUserID(int UserID,DbTransaction tran,Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete  from UM_FreezeReason where DealerAccoutID in  ");
            strSql.Append(" (select DealerAccoutID from UM_DealerAccount where UserID=@UserID) ");
            if (db==null) db = DatabaseFactory.CreateDatabase();
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

		#endregion  ��Ա����
	}
}

