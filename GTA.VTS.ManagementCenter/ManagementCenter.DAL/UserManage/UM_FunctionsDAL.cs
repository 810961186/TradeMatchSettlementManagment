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
    /// ������Ȩ�޹��ܱ� ���ݷ�����UM_FunctionsDAL��
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
	public class UM_FunctionsDAL
	{
		public UM_FunctionsDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(FunctionID)+1 from UM_Functions";
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
		public bool Exists(int FunctionID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from UM_Functions where FunctionID=@FunctionID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "FunctionID", DbType.Int32,FunctionID);
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
		public int Add(ManagementCenter.Model.UM_Functions model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into UM_Functions(");
			strSql.Append("FunctionName)");

			strSql.Append(" values (");
			strSql.Append("@FunctionName)");
			strSql.Append(";select @@IDENTITY");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "FunctionName", DbType.String, model.FunctionName);
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
		public void Update(ManagementCenter.Model.UM_Functions model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update UM_Functions set ");
			strSql.Append("FunctionName=@FunctionName");
			strSql.Append(" where FunctionID=@FunctionID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "FunctionID", DbType.Int32, model.FunctionID);
			db.AddInParameter(dbCommand, "FunctionName", DbType.String, model.FunctionName);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int FunctionID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete UM_Functions ");
			strSql.Append(" where FunctionID=@FunctionID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "FunctionID", DbType.Int32,FunctionID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.UM_Functions GetModel(int FunctionID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FunctionID,FunctionName from UM_Functions ");
			strSql.Append(" where FunctionID=@FunctionID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "FunctionID", DbType.Int32,FunctionID);
			ManagementCenter.Model.UM_Functions model=null;
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
			strSql.Append("select FunctionID,FunctionName ");
			strSql.Append(" FROM UM_Functions ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "UM_Functions");
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
		public List<ManagementCenter.Model.UM_Functions> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FunctionID,FunctionName ");
			strSql.Append(" FROM UM_Functions ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.UM_Functions> list = new List<ManagementCenter.Model.UM_Functions>();
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
		public ManagementCenter.Model.UM_Functions ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.UM_Functions model=new ManagementCenter.Model.UM_Functions();
			object ojb; 
			ojb = dataReader["FunctionID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.FunctionID=(int)ojb;
			}
			model.FunctionName=dataReader["FunctionName"].ToString();
			return model;
		}

		#endregion  ��Ա����
	}
}

