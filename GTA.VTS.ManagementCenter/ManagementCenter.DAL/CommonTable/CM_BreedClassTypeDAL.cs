using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Maticsoft.DBUtility;//������������
namespace ManagementCenter.DAL
{
	/// <summary>
    ///������������ƷƷ������ ���ݷ�����CM_BreedClassTypeDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-20
	/// </summary>
	public class CM_BreedClassTypeDAL
	{
		public CM_BreedClassTypeDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(BreedClassTypeID)+1 from CM_BreedClassType";
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
		public bool Exists(int BreedClassTypeID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from CM_BreedClassType where BreedClassTypeID=@BreedClassTypeID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "BreedClassTypeID", DbType.Int32,BreedClassTypeID);
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
		public void Add(ManagementCenter.Model.CM_BreedClassType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CM_BreedClassType(");
			strSql.Append("BreedClassTypeID,BreedClassTypeName)");

			strSql.Append(" values (");
			strSql.Append("@BreedClassTypeID,@BreedClassTypeName)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "BreedClassTypeID", DbType.Int32, model.BreedClassTypeID);
			db.AddInParameter(dbCommand, "BreedClassTypeName", DbType.String, model.BreedClassTypeName);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.CM_BreedClassType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CM_BreedClassType set ");
			strSql.Append("BreedClassTypeName=@BreedClassTypeName");
			strSql.Append(" where BreedClassTypeID=@BreedClassTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "BreedClassTypeID", DbType.Int32, model.BreedClassTypeID);
			db.AddInParameter(dbCommand, "BreedClassTypeName", DbType.String, model.BreedClassTypeName);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int BreedClassTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete CM_BreedClassType ");
			strSql.Append(" where BreedClassTypeID=@BreedClassTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "BreedClassTypeID", DbType.Int32,BreedClassTypeID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.CM_BreedClassType GetModel(int BreedClassTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BreedClassTypeID,BreedClassTypeName from CM_BreedClassType ");
			strSql.Append(" where BreedClassTypeID=@BreedClassTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "BreedClassTypeID", DbType.Int32,BreedClassTypeID);
			ManagementCenter.Model.CM_BreedClassType model=null;
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
			strSql.Append("select BreedClassTypeID,BreedClassTypeName ");
			strSql.Append(" FROM CM_BreedClassType ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "CM_BreedClassType");
			db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "ID");
			db.AddInParameter(dbCommand, "PageSize", DbType.Int32, PageSize);
			db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
			db.AddInParameter(dbCommand, "IsReCount", DbType.Boolean, 0);
			db.AddInParameter(dbCommand, "OrderType", DbType.Boolean, 0);
			db.AddInParameter(dbCommand, "strWhere", DbType.AnsiString, strWhere);
			return db.ExecuteDataSet(dbCommand);
		}*/

		/// <summary>
		/// ��������б�����DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
		/// </summary>
		public List<ManagementCenter.Model.CM_BreedClassType> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BreedClassTypeID,BreedClassTypeName ");
			strSql.Append(" FROM CM_BreedClassType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.CM_BreedClassType> list = new List<ManagementCenter.Model.CM_BreedClassType>();
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
		public ManagementCenter.Model.CM_BreedClassType ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.CM_BreedClassType model=new ManagementCenter.Model.CM_BreedClassType();
			object ojb; 
			ojb = dataReader["BreedClassTypeID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.BreedClassTypeID=(int)ojb;
			}
			model.BreedClassTypeName=dataReader["BreedClassTypeName"].ToString();
			return model;
		}

		#endregion  ��Ա����
	}
}
