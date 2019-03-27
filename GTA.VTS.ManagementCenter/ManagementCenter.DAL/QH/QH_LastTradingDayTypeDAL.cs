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
    ///����������������� ���ݷ�����QH_LastTradingDayTypeDAL��
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
	public class QH_LastTradingDayTypeDAL
	{
		public QH_LastTradingDayTypeDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(LastTradingDayTypeID)+1 from QH_LastTradingDayType";
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
		public bool Exists(int LastTradingDayTypeID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from QH_LastTradingDayType where LastTradingDayTypeID=@LastTradingDayTypeID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "LastTradingDayTypeID", DbType.Int32,LastTradingDayTypeID);
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
		public void Add(ManagementCenter.Model.QH_LastTradingDayType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into QH_LastTradingDayType(");
			strSql.Append("LastTradingDayTypeID,TypeName)");

			strSql.Append(" values (");
			strSql.Append("@LastTradingDayTypeID,@TypeName)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "LastTradingDayTypeID", DbType.Int32, model.LastTradingDayTypeID);
			db.AddInParameter(dbCommand, "TypeName", DbType.AnsiString, model.TypeName);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.QH_LastTradingDayType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update QH_LastTradingDayType set ");
			strSql.Append("TypeName=@TypeName");
			strSql.Append(" where LastTradingDayTypeID=@LastTradingDayTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "LastTradingDayTypeID", DbType.Int32, model.LastTradingDayTypeID);
			db.AddInParameter(dbCommand, "TypeName", DbType.AnsiString, model.TypeName);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int LastTradingDayTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete QH_LastTradingDayType ");
			strSql.Append(" where LastTradingDayTypeID=@LastTradingDayTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "LastTradingDayTypeID", DbType.Int32,LastTradingDayTypeID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.QH_LastTradingDayType GetModel(int LastTradingDayTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select LastTradingDayTypeID,TypeName from QH_LastTradingDayType ");
			strSql.Append(" where LastTradingDayTypeID=@LastTradingDayTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "LastTradingDayTypeID", DbType.Int32,LastTradingDayTypeID);
			ManagementCenter.Model.QH_LastTradingDayType model=null;
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
			strSql.Append("select LastTradingDayTypeID,TypeName ");
			strSql.Append(" FROM QH_LastTradingDayType ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "QH_LastTradingDayType");
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
		public List<ManagementCenter.Model.QH_LastTradingDayType> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select LastTradingDayTypeID,TypeName ");
			strSql.Append(" FROM QH_LastTradingDayType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.QH_LastTradingDayType> list = new List<ManagementCenter.Model.QH_LastTradingDayType>();
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
		public ManagementCenter.Model.QH_LastTradingDayType ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.QH_LastTradingDayType model=new ManagementCenter.Model.QH_LastTradingDayType();
			object ojb; 
			ojb = dataReader["LastTradingDayTypeID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.LastTradingDayTypeID=(int)ojb;
			}
			model.TypeName=dataReader["TypeName"].ToString();
			return model;
		}

		#endregion  ��Ա����
	}
}

