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
    ///�������ֲֺͱ�֤��������� ���ݷ�����QH_PositionBailTypeDAL��
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
	public class QH_PositionBailTypeDAL
	{
		public QH_PositionBailTypeDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(PositionBailTypeID)+1 from QH_PositionBailType";
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
		public bool Exists(int PositionBailTypeID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from QH_PositionBailType where PositionBailTypeID=@PositionBailTypeID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PositionBailTypeID", DbType.Int32,PositionBailTypeID);
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
		public void Add(ManagementCenter.Model.QH_PositionBailType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into QH_PositionBailType(");
			strSql.Append("PositionBailTypeID,PositionBailTypeName)");

			strSql.Append(" values (");
			strSql.Append("@PositionBailTypeID,@PositionBailTypeName)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PositionBailTypeID", DbType.Int32, model.PositionBailTypeID);
			db.AddInParameter(dbCommand, "PositionBailTypeName", DbType.String, model.PositionBailTypeName);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.QH_PositionBailType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update QH_PositionBailType set ");
			strSql.Append("PositionBailTypeName=@PositionBailTypeName");
			strSql.Append(" where PositionBailTypeID=@PositionBailTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PositionBailTypeID", DbType.Int32, model.PositionBailTypeID);
			db.AddInParameter(dbCommand, "PositionBailTypeName", DbType.String, model.PositionBailTypeName);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int PositionBailTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete QH_PositionBailType ");
			strSql.Append(" where PositionBailTypeID=@PositionBailTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PositionBailTypeID", DbType.Int32,PositionBailTypeID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.QH_PositionBailType GetModel(int PositionBailTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select PositionBailTypeID,PositionBailTypeName from QH_PositionBailType ");
			strSql.Append(" where PositionBailTypeID=@PositionBailTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PositionBailTypeID", DbType.Int32,PositionBailTypeID);
			ManagementCenter.Model.QH_PositionBailType model=null;
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
			strSql.Append("select PositionBailTypeID,PositionBailTypeName ");
			strSql.Append(" FROM QH_PositionBailType ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "QH_PositionBailType");
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
		public List<ManagementCenter.Model.QH_PositionBailType> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select PositionBailTypeID,PositionBailTypeName ");
			strSql.Append(" FROM QH_PositionBailType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.QH_PositionBailType> list = new List<ManagementCenter.Model.QH_PositionBailType>();
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
		public ManagementCenter.Model.QH_PositionBailType ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.QH_PositionBailType model=new ManagementCenter.Model.QH_PositionBailType();
			object ojb; 
			ojb = dataReader["PositionBailTypeID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.PositionBailTypeID=(int)ojb;
			}
			model.PositionBailTypeName=dataReader["PositionBailTypeName"].ToString();
			return model;
		}

		#endregion  ��Ա����
	}
}

