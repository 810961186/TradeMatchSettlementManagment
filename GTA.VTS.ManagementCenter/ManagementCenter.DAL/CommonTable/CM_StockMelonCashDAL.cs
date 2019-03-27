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
    ///��������Ʊ�ֺ��¼_�ֽ� ���ݷ�����CM_StockMelonCashDAL��
    ///���ߣ�������
    ///����:2008-11-20
	/// </summary>
	public class CM_StockMelonCashDAL
	{
		public CM_StockMelonCashDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(StockMelonCuttingCashID)+1 from StockMelonCash";
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
		public bool Exists(int StockMelonCuttingCashID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from StockMelonCash where StockMelonCuttingCashID=@StockMelonCuttingCashID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "StockMelonCuttingCashID", DbType.Int32,StockMelonCuttingCashID);
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
		public int Add(ManagementCenter.Model.CM_StockMelonCash model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into StockMelonCash(");
            strSql.Append("StockRightRegisterDate,StockRightLogoutDatumDate,StockCode,RatioOfCashDividend)");

			strSql.Append(" values (");
            strSql.Append("@StockRightRegisterDate,@StockRightLogoutDatumDate,@StockCode,@RatioOfCashDividend)");
			strSql.Append(";select @@IDENTITY");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "StockRightRegisterDate", DbType.DateTime, model.StockRightRegisterDate);
			db.AddInParameter(dbCommand, "StockRightLogoutDatumDate", DbType.DateTime, model.StockRightLogoutDatumDate);
            db.AddInParameter(dbCommand, "StockCode", DbType.String, model.CommodityCode);
			db.AddInParameter(dbCommand, "RatioOfCashDividend", DbType.Double, model.RatioOfCashDividend);
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
		public void Update(ManagementCenter.Model.CM_StockMelonCash model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update StockMelonCash set ");
			strSql.Append("StockRightRegisterDate=@StockRightRegisterDate,");
			strSql.Append("StockRightLogoutDatumDate=@StockRightLogoutDatumDate,");
            strSql.Append("StockCode=@StockCode,");
			strSql.Append("RatioOfCashDividend=@RatioOfCashDividend");
			strSql.Append(" where StockMelonCuttingCashID=@StockMelonCuttingCashID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "StockMelonCuttingCashID", DbType.Int32, model.StockMelonCuttingCashID);
			db.AddInParameter(dbCommand, "StockRightRegisterDate", DbType.DateTime, model.StockRightRegisterDate);
			db.AddInParameter(dbCommand, "StockRightLogoutDatumDate", DbType.DateTime, model.StockRightLogoutDatumDate);
            db.AddInParameter(dbCommand, "StockCode", DbType.String, model.CommodityCode);
			db.AddInParameter(dbCommand, "RatioOfCashDividend", DbType.Double, model.RatioOfCashDividend);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int StockMelonCuttingCashID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete StockMelonCash ");
			strSql.Append(" where StockMelonCuttingCashID=@StockMelonCuttingCashID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "StockMelonCuttingCashID", DbType.Int32,StockMelonCuttingCashID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.CM_StockMelonCash GetModel(int StockMelonCuttingCashID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select StockMelonCuttingCashID,StockRightRegisterDate,StockRightLogoutDatumDate,StockCode,RatioOfCashDividend from StockMelonCash ");
			strSql.Append(" where StockMelonCuttingCashID=@StockMelonCuttingCashID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "StockMelonCuttingCashID", DbType.Int32,StockMelonCuttingCashID);
			ManagementCenter.Model.CM_StockMelonCash model=null;
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
            strSql.Append("select StockMelonCuttingCashID,StockRightRegisterDate,StockRightLogoutDatumDate,StockCode,RatioOfCashDividend ");
			strSql.Append(" FROM StockMelonCash ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "CM_StockMelonCash");
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
		public List<ManagementCenter.Model.CM_StockMelonCash> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select StockMelonCuttingCashID,StockRightRegisterDate,StockRightLogoutDatumDate,StockCode,RatioOfCashDividend ");
			strSql.Append(" FROM StockMelonCash ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.CM_StockMelonCash> list = new List<ManagementCenter.Model.CM_StockMelonCash>();
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
		public ManagementCenter.Model.CM_StockMelonCash ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.CM_StockMelonCash model=new ManagementCenter.Model.CM_StockMelonCash();
			object ojb; 
			ojb = dataReader["StockMelonCuttingCashID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.StockMelonCuttingCashID=(int)ojb;
			}
			ojb = dataReader["StockRightRegisterDate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.StockRightRegisterDate=(DateTime)ojb;
			}
			ojb = dataReader["StockRightLogoutDatumDate"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.StockRightLogoutDatumDate=(DateTime)ojb;
			}
            model.CommodityCode = dataReader["StockCode"].ToString();
			ojb = dataReader["RatioOfCashDividend"];
			if(ojb != null && ojb != DBNull.Value)
			{
                model.RatioOfCashDividend = (double)ojb;
			}
			return model;
		}

		#endregion  ��Ա����
	}
}

