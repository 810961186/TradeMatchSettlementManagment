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
    ///��������Ʊ��Ȩ֤������� ���ݷ�����StockInfo��
    ///���ߣ�������  �޸ģ�����ΰ
    ///����:2008-11-20 2009-12-01
	/// </summary>
	public class StockInfoDAL
	{
        public StockInfoDAL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string StockCode)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from StockInfo where StockCode=@StockCode ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "StockCode", DbType.String,StockCode);
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
		public void Add(ManagementCenter.Model.StockInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into StockInfo(");
            strSql.Append("StockCode,StockName,Paydt,LabelCommodityCode,Nindcd,StockPinYin,turnovervolume)");

			strSql.Append(" values (");
            strSql.Append("@StockCode,@StockName,@Paydt,@LabelCommodityCode,@Nindcd,@StockPinYin,@turnovervolume)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "StockCode", DbType.String, model.StockCode);
			db.AddInParameter(dbCommand, "StockName", DbType.String, model.StockName);
			db.AddInParameter(dbCommand, "Paydt", DbType.String, model.Paydt);
			db.AddInParameter(dbCommand, "LabelCommodityCode", DbType.String, model.LabelCommodityCode);
			db.AddInParameter(dbCommand, "Nindcd", DbType.String, model.Nindcd);
            db.AddInParameter(dbCommand, "StockPinYin", DbType.String, model.StockPinYin);
            db.AddInParameter(dbCommand, "Nindcd", DbType.Double, model.turnovervolume);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.StockInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update StockInfo set ");
			strSql.Append("StockName=@StockName,");
			strSql.Append("Paydt=@Paydt,");
			strSql.Append("LabelCommodityCode=@LabelCommodityCode,");
            strSql.Append("StockPinYin=@StockPinYin,");
            strSql.Append("turnovervolume=@turnovervolume,");
			strSql.Append("Nindcd=@Nindcd");
			strSql.Append(" where StockCode=@StockCode ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "StockCode", DbType.String, model.StockCode);
			db.AddInParameter(dbCommand, "StockName", DbType.String, model.StockName);
			db.AddInParameter(dbCommand, "Paydt", DbType.String, model.Paydt);
			db.AddInParameter(dbCommand, "LabelCommodityCode", DbType.String, model.LabelCommodityCode);
			db.AddInParameter(dbCommand, "Nindcd", DbType.String, model.Nindcd);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string StockCode)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete StockInfo ");
			strSql.Append(" where StockCode=@StockCode ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "StockCode", DbType.String,StockCode);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.StockInfo GetModel(string StockCode)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select StockCode,StockName,Paydt,LabelCommodityCode,Nindcd,StockPinYin,turnovervolume from StockInfo ");
			strSql.Append(" where StockCode=@StockCode ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "StockCode", DbType.String,StockCode);
			ManagementCenter.Model.StockInfo model=null;
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
            strSql.Append("select StockCode,StockName,Paydt,LabelCommodityCode,Nindcd,StockPinYin,turnovervolume ");
			strSql.Append(" FROM StockInfo ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "StockInfo");
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
		public List<ManagementCenter.Model.StockInfo> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select StockCode,StockName,Paydt,LabelCommodityCode,Nindcd,StockPinYin,turnovervolume ");
			strSql.Append(" FROM StockInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.StockInfo> list = new List<ManagementCenter.Model.StockInfo>();
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
        /// ���ݲ�ѯ������ȡ��������ͨ������Ϣ(��ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ��������Ϊ�գ�</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.StockInfo> GetStockInfoList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select StockCode,StockName,Paydt,LabelCommodityCode,Nindcd,StockPinYin,turnovervolume,GoerScale,BreedClassID,[CodeFromSource]=1 ");
            strSql.Append(" FROM StockInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.StockInfo> list = new List<ManagementCenter.Model.StockInfo>();
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
		public ManagementCenter.Model.StockInfo ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.StockInfo model=new ManagementCenter.Model.StockInfo();
            object ojb;
			model.StockCode=dataReader["StockCode"].ToString();
			model.StockName=dataReader["StockName"].ToString();
			model.Paydt=dataReader["Paydt"].ToString();
			model.LabelCommodityCode=dataReader["LabelCommodityCode"].ToString();
			model.Nindcd=dataReader["Nindcd"].ToString();
            model.StockPinYin = dataReader["StockPinYin"].ToString();
             ojb = dataReader["turnovervolume"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.turnovervolume = (double)ojb;
            }
            //2009-12-01����ʵ���ֶ�
            ojb = dataReader["GoerScale"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoerScale = (decimal)ojb;
            }
            ojb = dataReader["BreedClassID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassID = (int)ojb;
            }
            //ojb = dataReader["CodeFormSource"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.CodeFromSource = (int)ojb;
            //}
			return model;
		}

		#endregion  ��Ա����
	}
}

