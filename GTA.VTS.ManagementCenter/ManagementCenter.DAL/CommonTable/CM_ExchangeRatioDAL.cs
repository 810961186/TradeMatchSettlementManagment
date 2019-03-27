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
    ///���������ʱ� ���ݷ�����CM_ExchangeRatioDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-20
	/// </summary>
	public class CM_ExchangeRatioDAL
	{
		public CM_ExchangeRatioDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(CurrencyExchangeID)+1 from CM_ExchangeRatio";
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
		public bool Exists(int CurrencyExchangeID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from CM_ExchangeRatio where CurrencyExchangeID=@CurrencyExchangeID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "CurrencyExchangeID", DbType.Int32,CurrencyExchangeID);
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
		public void Add(ManagementCenter.Model.CM_ExchangeRatio model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CM_ExchangeRatio(");
			strSql.Append("Ratio,Remarks,CurrencyExchangeID)");

			strSql.Append(" values (");
			strSql.Append("@Ratio,@Remarks,@CurrencyExchangeID)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "Ratio", DbType.Decimal, model.Ratio);
			db.AddInParameter(dbCommand, "Remarks", DbType.String, model.Remarks);
			db.AddInParameter(dbCommand, "CurrencyExchangeID", DbType.Int32, model.CurrencyExchangeID);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.CM_ExchangeRatio model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CM_ExchangeRatio set ");
			strSql.Append("Ratio=@Ratio,");
			strSql.Append("Remarks=@Remarks");
			strSql.Append(" where CurrencyExchangeID=@CurrencyExchangeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "Ratio", DbType.Decimal, model.Ratio);
			db.AddInParameter(dbCommand, "Remarks", DbType.String, model.Remarks);
			db.AddInParameter(dbCommand, "CurrencyExchangeID", DbType.Int32, model.CurrencyExchangeID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int CurrencyExchangeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete CM_ExchangeRatio ");
			strSql.Append(" where CurrencyExchangeID=@CurrencyExchangeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "CurrencyExchangeID", DbType.Int32,CurrencyExchangeID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.CM_ExchangeRatio GetModel(int CurrencyExchangeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Ratio,Remarks,CurrencyExchangeID from CM_ExchangeRatio ");
			strSql.Append(" where CurrencyExchangeID=@CurrencyExchangeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "CurrencyExchangeID", DbType.Int32,CurrencyExchangeID);
			ManagementCenter.Model.CM_ExchangeRatio model=null;
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
			strSql.Append("select Ratio,Remarks,CurrencyExchangeID ");
			strSql.Append(" FROM CM_ExchangeRatio ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "CM_ExchangeRatio");
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
		public List<ManagementCenter.Model.CM_ExchangeRatio> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Ratio,Remarks,CurrencyExchangeID ");
			strSql.Append(" FROM CM_ExchangeRatio ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.CM_ExchangeRatio> list = new List<ManagementCenter.Model.CM_ExchangeRatio>();
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
		public ManagementCenter.Model.CM_ExchangeRatio ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.CM_ExchangeRatio model=new ManagementCenter.Model.CM_ExchangeRatio();
			object ojb; 
			ojb = dataReader["Ratio"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Ratio=(decimal)ojb;
			}
			model.Remarks=dataReader["Remarks"].ToString();
			ojb = dataReader["CurrencyExchangeID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.CurrencyExchangeID=(int)ojb;
			}
			return model;
		}

		#endregion  ��Ա����
	}
}

