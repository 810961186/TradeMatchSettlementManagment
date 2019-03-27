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
    ///�������г������ ���ݷ�����CM_MarketParticipationDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-20
	/// </summary>
	public class CM_MarketParticipationDAL
	{
		public CM_MarketParticipationDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(BreedClassID)+1 from CM_MarketParticipation";
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
		public bool Exists(int BreedClassID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from CM_MarketParticipation where BreedClassID=@BreedClassID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32,BreedClassID);
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
		public void Add(ManagementCenter.Model.CM_MarketParticipation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CM_MarketParticipation(");
			strSql.Append("Participation,BreedClassID)");

			strSql.Append(" values (");
			strSql.Append("@Participation,@BreedClassID)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "Participation", DbType.Double, model.Participation);
			db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.CM_MarketParticipation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CM_MarketParticipation set ");
			strSql.Append("Participation=@Participation");
			strSql.Append(" where BreedClassID=@BreedClassID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "Participation", DbType.Double, model.Participation);
			db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int BreedClassID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete CM_MarketParticipation ");
			strSql.Append(" where BreedClassID=@BreedClassID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32,BreedClassID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.CM_MarketParticipation GetModel(int BreedClassID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Participation,BreedClassID from CM_MarketParticipation ");
			strSql.Append(" where BreedClassID=@BreedClassID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32,BreedClassID);
			ManagementCenter.Model.CM_MarketParticipation model=null;
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
			strSql.Append("select Participation,BreedClassID ");
			strSql.Append(" FROM CM_MarketParticipation ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "CM_MarketParticipation");
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
		public List<ManagementCenter.Model.CM_MarketParticipation> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Participation,BreedClassID ");
			strSql.Append(" FROM CM_MarketParticipation ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.CM_MarketParticipation> list = new List<ManagementCenter.Model.CM_MarketParticipation>();
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
		public ManagementCenter.Model.CM_MarketParticipation ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.CM_MarketParticipation model=new ManagementCenter.Model.CM_MarketParticipation();
			object ojb; 
			ojb = dataReader["Participation"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Participation=(float)ojb;
			}
			ojb = dataReader["BreedClassID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.BreedClassID=(int)ojb;
			}
			return model;
		}

		#endregion  ��Ա����
	}
}

