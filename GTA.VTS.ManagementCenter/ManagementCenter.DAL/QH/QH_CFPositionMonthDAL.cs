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
    ///�������ڻ�_Ʒ��_�����·� ���ݷ�����QH_CFPositionMonthDAL��
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
	public class QH_CFPositionMonthDAL
	{
		public QH_CFPositionMonthDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(DeliveryMonthTypeID)+1 from QH_CFPositionMonth";
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
		public bool Exists(int DeliveryMonthTypeID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from QH_CFPositionMonth where DeliveryMonthTypeID=@DeliveryMonthTypeID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "DeliveryMonthTypeID", DbType.Int32,DeliveryMonthTypeID);
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
		public void Add(ManagementCenter.Model.QH_CFPositionMonth model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into QH_CFPositionMonth(");
			strSql.Append("DeliveryMonthTypeID,DeliveryMonthName)");

			strSql.Append(" values (");
			strSql.Append("@DeliveryMonthTypeID,@DeliveryMonthName)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "DeliveryMonthTypeID", DbType.Int32, model.DeliveryMonthTypeID);
			db.AddInParameter(dbCommand, "DeliveryMonthName", DbType.String, model.DeliveryMonthName);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.QH_CFPositionMonth model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update QH_CFPositionMonth set ");
			strSql.Append("DeliveryMonthName=@DeliveryMonthName");
			strSql.Append(" where DeliveryMonthTypeID=@DeliveryMonthTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "DeliveryMonthTypeID", DbType.Int32, model.DeliveryMonthTypeID);
			db.AddInParameter(dbCommand, "DeliveryMonthName", DbType.String, model.DeliveryMonthName);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int DeliveryMonthTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete QH_CFPositionMonth ");
			strSql.Append(" where DeliveryMonthTypeID=@DeliveryMonthTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "DeliveryMonthTypeID", DbType.Int32,DeliveryMonthTypeID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.QH_CFPositionMonth GetModel(int DeliveryMonthTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select DeliveryMonthTypeID,DeliveryMonthName from QH_CFPositionMonth ");
			strSql.Append(" where DeliveryMonthTypeID=@DeliveryMonthTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "DeliveryMonthTypeID", DbType.Int32,DeliveryMonthTypeID);
			ManagementCenter.Model.QH_CFPositionMonth model=null;
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
			strSql.Append("select DeliveryMonthTypeID,DeliveryMonthName ");
			strSql.Append(" FROM QH_CFPositionMonth ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "QH_CFPositionMonth");
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
		public List<ManagementCenter.Model.QH_CFPositionMonth> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select DeliveryMonthTypeID,DeliveryMonthName ");
			strSql.Append(" FROM QH_CFPositionMonth ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.QH_CFPositionMonth> list = new List<ManagementCenter.Model.QH_CFPositionMonth>();
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
		public ManagementCenter.Model.QH_CFPositionMonth ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.QH_CFPositionMonth model=new ManagementCenter.Model.QH_CFPositionMonth();
			object ojb; 
			ojb = dataReader["DeliveryMonthTypeID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.DeliveryMonthTypeID=(int)ojb;
			}
			model.DeliveryMonthName=dataReader["DeliveryMonthName"].ToString();
			return model;
		}

		#endregion  ��Ա����
	}
}

