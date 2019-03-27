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
    /// ��������ʼ�ʽ�� ���ݷ�����UM_OriginationFundDAL��
    /// ���ߣ�������
    /// ���ڣ�2008-11-18
    /// </summary>
	public class UM_OriginationFundDAL
	{
		public UM_OriginationFundDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			string strsql = "select max(OriginationFundID)+1 from UM_OriginationFund";
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
		public bool Exists(int OriginationFundID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from UM_OriginationFund where OriginationFundID=@OriginationFundID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "OriginationFundID", DbType.Int32,OriginationFundID);
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
        public int Add(ManagementCenter.Model.UM_OriginationFund model, DbTransaction tran, Database db)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into UM_OriginationFund(");
			strSql.Append("FundMoney,Remark,TransactionCurrencyTypeID,DealerAccoutID)");

			strSql.Append(" values (");
			strSql.Append("@FundMoney,@Remark,@TransactionCurrencyTypeID,@DealerAccoutID)");
			strSql.Append(";select @@IDENTITY");

			if(db==null) db = DatabaseFactory.CreateDatabase();

			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "FundMoney", DbType.Currency, model.FundMoney);
			db.AddInParameter(dbCommand, "Remark", DbType.String, model.Remark);
			db.AddInParameter(dbCommand, "TransactionCurrencyTypeID", DbType.Int32, model.TransactionCurrencyTypeID);
			db.AddInParameter(dbCommand, "DealerAccoutID", DbType.String, model.DealerAccoutID);
			int result;
            object obj;
            if (tran == null)
            {
                obj = db.ExecuteScalar(dbCommand);
            }
            else
            {
                obj = db.ExecuteScalar(dbCommand, tran);
            }
			if(!int.TryParse(obj.ToString(),out result))
			{
				return 0;
			}
			return result;
		}
        /// <summary>
        /// ��ӳ�ʼ���ʽ�
        /// </summary>
        /// <param name="model">��ʼ���ʽ�ʵ��</param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.UM_OriginationFund model)
        {
            return Add(model, null,null);
        }

	    /// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.UM_OriginationFund model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update UM_OriginationFund set ");
			strSql.Append("FundMoney=@FundMoney,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("TransactionCurrencyTypeID=@TransactionCurrencyTypeID,");
			strSql.Append("DealerAccoutID=@DealerAccoutID");
			strSql.Append(" where OriginationFundID=@OriginationFundID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "FundMoney", DbType.Currency, model.FundMoney);
			db.AddInParameter(dbCommand, "Remark", DbType.String, model.Remark);
			db.AddInParameter(dbCommand, "OriginationFundID", DbType.Int32, model.OriginationFundID);
			db.AddInParameter(dbCommand, "TransactionCurrencyTypeID", DbType.Int32, model.TransactionCurrencyTypeID);
			db.AddInParameter(dbCommand, "DealerAccoutID", DbType.String, model.DealerAccoutID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int OriginationFundID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete UM_OriginationFund ");
			strSql.Append(" where OriginationFundID=@OriginationFundID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "OriginationFundID", DbType.Int32,OriginationFundID);
			db.ExecuteNonQuery(dbCommand);

		}

        /// <summary>
		/// ɾ�����ݸ����ʺ�
		/// </summary>
        public void DeleteByDealerAccoutID(string DealerAccoutID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete UM_OriginationFund ");
            strSql.Append(" where DealerAccoutID=@DealerAccoutID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "DealerAccoutID", DbType.String, DealerAccoutID);
			db.ExecuteNonQuery(dbCommand);

		}
        
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.UM_OriginationFund GetModel(int OriginationFundID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FundMoney,Remark,OriginationFundID,TransactionCurrencyTypeID,DealerAccoutID from UM_OriginationFund ");
			strSql.Append(" where OriginationFundID=@OriginationFundID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "OriginationFundID", DbType.Int32,OriginationFundID);
			ManagementCenter.Model.UM_OriginationFund model=null;
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
			strSql.Append("select FundMoney,Remark,OriginationFundID,TransactionCurrencyTypeID,DealerAccoutID ");
			strSql.Append(" FROM UM_OriginationFund ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "UM_OriginationFund");
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
		public List<ManagementCenter.Model.UM_OriginationFund> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FundMoney,Remark,OriginationFundID,TransactionCurrencyTypeID,DealerAccoutID ");
			strSql.Append(" FROM UM_OriginationFund ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.UM_OriginationFund> list = new List<ManagementCenter.Model.UM_OriginationFund>();
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
		public ManagementCenter.Model.UM_OriginationFund ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.UM_OriginationFund model=new ManagementCenter.Model.UM_OriginationFund();
			object ojb; 
			ojb = dataReader["FundMoney"];
			if(ojb != null && ojb != DBNull.Value)
			{ 
                model.FundMoney = (decimal)ojb;
			}
			model.Remark=dataReader["Remark"].ToString();
			ojb = dataReader["OriginationFundID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.OriginationFundID=(int)ojb;
			}
			ojb = dataReader["TransactionCurrencyTypeID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.TransactionCurrencyTypeID=(int)ojb;
			}
			model.DealerAccoutID=dataReader["DealerAccoutID"].ToString();
			return model;
		}

        /// <summary>
        /// ɾ����¼���ݽ���ԱID
        /// </summary>
        public void DeleteByUserID(int UserID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UM_OriginationFund  where DealerAccoutID in  ");
            strSql.Append(" (select DealerAccoutID from UM_DealerAccount where UserID=@UserID) ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
            if (tran == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
        }

        /// <summary>
        /// ���ݽ���ԱID��ȡ��ʼ�ʽ��б�
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.UM_OriginationFund> GetListByUserID(int UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select * from UM_OriginationFund  where DealerAccoutID in  ");
            strSql.Append(" (select DealerAccoutID from UM_DealerAccount where UserID=@UserID) ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
            List<ManagementCenter.Model.UM_OriginationFund> list = new List<ManagementCenter.Model.UM_OriginationFund>();
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderBind(dataReader));
                }
            }
            return list;
        }
		#endregion  ��Ա����
	}
}

