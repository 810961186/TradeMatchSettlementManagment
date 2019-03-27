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
    ///��������Ʒ�ڻ�_�ֲ�ȡֵ���� ���ݷ�����QH_PositionValueTypeDAL��
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
	public class QH_PositionValueTypeDAL
	{
		public QH_PositionValueTypeDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(PositionValueTypeID)+1 from QH_PositionValueType";
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
		public bool Exists(int PositionValueTypeID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from QH_PositionValueType where PositionValueTypeID=@PositionValueTypeID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PositionValueTypeID", DbType.Int32,PositionValueTypeID);
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
		public void Add(ManagementCenter.Model.QH_PositionValueType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into QH_PositionValueType(");
			strSql.Append("PositionValueTypeID,PositionValueName)");

			strSql.Append(" values (");
			strSql.Append("@PositionValueTypeID,@PositionValueName)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PositionValueTypeID", DbType.Int32, model.PositionValueTypeID);
			db.AddInParameter(dbCommand, "PositionValueName", DbType.String, model.PositionValueName);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.QH_PositionValueType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update QH_PositionValueType set ");
			strSql.Append("PositionValueName=@PositionValueName");
			strSql.Append(" where PositionValueTypeID=@PositionValueTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PositionValueTypeID", DbType.Int32, model.PositionValueTypeID);
			db.AddInParameter(dbCommand, "PositionValueName", DbType.String, model.PositionValueName);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int PositionValueTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete QH_PositionValueType ");
			strSql.Append(" where PositionValueTypeID=@PositionValueTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PositionValueTypeID", DbType.Int32,PositionValueTypeID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.QH_PositionValueType GetModel(int PositionValueTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select PositionValueTypeID,PositionValueName from QH_PositionValueType ");
			strSql.Append(" where PositionValueTypeID=@PositionValueTypeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PositionValueTypeID", DbType.Int32,PositionValueTypeID);
			ManagementCenter.Model.QH_PositionValueType model=null;
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
			strSql.Append("select PositionValueTypeID,PositionValueName ");
			strSql.Append(" FROM QH_PositionValueType ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "QH_PositionValueType");
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
		public List<ManagementCenter.Model.QH_PositionValueType> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select PositionValueTypeID,PositionValueName ");
			strSql.Append(" FROM QH_PositionValueType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.QH_PositionValueType> list = new List<ManagementCenter.Model.QH_PositionValueType>();
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
		public ManagementCenter.Model.QH_PositionValueType ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.QH_PositionValueType model=new ManagementCenter.Model.QH_PositionValueType();
			object ojb; 
			ojb = dataReader["PositionValueTypeID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.PositionValueTypeID=(int)ojb;
			}
			model.PositionValueName=dataReader["PositionValueName"].ToString();
			return model;
		}

		#endregion  ��Ա����
	}
}

