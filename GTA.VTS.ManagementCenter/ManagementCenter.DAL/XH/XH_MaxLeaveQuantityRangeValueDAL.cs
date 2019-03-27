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
    ///���������׹���_���ί����_��Χ_ֵ ���ݷ�����XH_MaxLeaveQuantityRangeValueDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
	public class XH_MaxLeaveQuantityRangeValueDAL
	{
		public XH_MaxLeaveQuantityRangeValueDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(BreedClassID)+1 from XH_MaxLeaveQuantityRangeValue";
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
		public bool Exists(int BreedClassID,int FieldRangeID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from XH_MaxLeaveQuantityRangeValue where BreedClassID=@BreedClassID and FieldRangeID=@FieldRangeID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32,BreedClassID);
			db.AddInParameter(dbCommand, "FieldRangeID", DbType.Int32,FieldRangeID);
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
		public void Add(ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into XH_MaxLeaveQuantityRangeValue(");
			strSql.Append("Value,BreedClassID,FieldRangeID)");

			strSql.Append(" values (");
			strSql.Append("@Value,@BreedClassID,@FieldRangeID)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "Value", DbType.Decimal, model.Value);
			db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
			db.AddInParameter(dbCommand, "FieldRangeID", DbType.Int32, model.FieldRangeID);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update XH_MaxLeaveQuantityRangeValue set ");
			strSql.Append("Value=@Value");
			strSql.Append(" where BreedClassID=@BreedClassID and FieldRangeID=@FieldRangeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "Value", DbType.Decimal, model.Value);
			db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
			db.AddInParameter(dbCommand, "FieldRangeID", DbType.Int32, model.FieldRangeID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int BreedClassID,int FieldRangeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete XH_MaxLeaveQuantityRangeValue ");
			strSql.Append(" where BreedClassID=@BreedClassID and FieldRangeID=@FieldRangeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32,BreedClassID);
			db.AddInParameter(dbCommand, "FieldRangeID", DbType.Int32,FieldRangeID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue GetModel(int BreedClassID,int FieldRangeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Value,BreedClassID,FieldRangeID from XH_MaxLeaveQuantityRangeValue ");
			strSql.Append(" where BreedClassID=@BreedClassID and FieldRangeID=@FieldRangeID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32,BreedClassID);
			db.AddInParameter(dbCommand, "FieldRangeID", DbType.Int32,FieldRangeID);
			ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue model=null;
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
			strSql.Append("select Value,BreedClassID,FieldRangeID ");
			strSql.Append(" FROM XH_MaxLeaveQuantityRangeValue ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "XH_MaxLeaveQuantityRangeValue");
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
		public List<ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Value,BreedClassID,FieldRangeID ");
			strSql.Append(" FROM XH_MaxLeaveQuantityRangeValue ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue> list = new List<ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue>();
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
		public ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue model=new ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue();
			object ojb; 
			ojb = dataReader["Value"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Value=(decimal)ojb;
			}
			ojb = dataReader["BreedClassID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.BreedClassID=(int)ojb;
			}
			ojb = dataReader["FieldRangeID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.FieldRangeID=(int)ojb;
			}
			return model;
		}

		#endregion  ��Ա����
	}
}

