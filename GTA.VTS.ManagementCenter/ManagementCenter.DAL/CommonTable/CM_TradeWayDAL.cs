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
    ///���������׷��� ���ݷ�����CM_TradeWay��
    ///���ߣ�����ΰ
    ///����:2008-11-26
	/// </summary>
	public class CM_TradeWayDAL
	{
		public CM_TradeWayDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(TradeWayID)+1 from CM_TradeWay";
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
		public bool Exists(int TradeWayID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from CM_TradeWay where TradeWayID=@TradeWayID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "TradeWayID", DbType.Int32,TradeWayID);
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
		public int Add(ManagementCenter.Model.CM_TradeWay model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CM_TradeWay(");
			strSql.Append("TradeWayName)");

			strSql.Append(" values (");
			strSql.Append("@TradeWayName)");
			strSql.Append(";select @@IDENTITY");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "TradeWayName", DbType.String, model.TradeWayName);
			int result;
			object obj = db.ExecuteScalar(dbCommand);
			if(!int.TryParse(obj.ToString(),out result))
			{
                return AppGlobalVariable.INIT_INT;
			}
			return result;
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.CM_TradeWay model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CM_TradeWay set ");
			strSql.Append("TradeWayName=@TradeWayName");
			strSql.Append(" where TradeWayID=@TradeWayID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "TradeWayID", DbType.Int32, model.TradeWayID);
			db.AddInParameter(dbCommand, "TradeWayName", DbType.String, model.TradeWayName);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int TradeWayID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete CM_TradeWay ");
			strSql.Append(" where TradeWayID=@TradeWayID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "TradeWayID", DbType.Int32,TradeWayID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.CM_TradeWay GetModel(int TradeWayID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select TradeWayID,TradeWayName from CM_TradeWay ");
			strSql.Append(" where TradeWayID=@TradeWayID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "TradeWayID", DbType.Int32,TradeWayID);
			ManagementCenter.Model.CM_TradeWay model=null;
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
			strSql.Append("select TradeWayID,TradeWayName ");
			strSql.Append(" FROM CM_TradeWay ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "CM_TradeWay");
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
		public List<ManagementCenter.Model.CM_TradeWay> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select TradeWayID,TradeWayName ");
			strSql.Append(" FROM CM_TradeWay ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.CM_TradeWay> list = new List<ManagementCenter.Model.CM_TradeWay>();
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
		public ManagementCenter.Model.CM_TradeWay ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.CM_TradeWay model=new ManagementCenter.Model.CM_TradeWay();
			object ojb; 
			ojb = dataReader["TradeWayID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.TradeWayID=(int)ojb;
			}
			model.TradeWayName=dataReader["TradeWayName"].ToString();
			return model;
		}

		#endregion  ��Ա����
	}
}

