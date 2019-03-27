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
    ///�������ֻ�_Ʒ��_���׵�λ���� ���ݷ�����CM_UnitConversionDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-26
	/// </summary>
	public class CM_UnitConversionDAL
	{
		public CM_UnitConversionDAL()
		{}

        #region SQL

        /// <summary>
        /// ���ݲ�ѯ���������ֻ�_Ʒ��_���׵�λ����
        /// </summary>
        private string SQL_SELECT_CMUNITCONVERSION =
            @"SELECT B.BREEDCLASSNAME,A.* FROM CM_UNITCONVERSION AS A,CM_BREEDCLASS AS B 
                                                            WHERE A.BREEDCLASSID=B.BREEDCLASSID ";

        /// <summary>
        /// �����ֻ�_Ʒ��_���׵�λ������е�Ʒ�ֱ�ʶ��ȡƷ������
        /// </summary>
        private string SQL_SELECTBREEDCLASSNAME_CMUNITCONVERSION =
                                                            @"SELECT A.BREEDCLASSID,A.BREEDCLASSNAME 
                                                FROM CM_BREEDCLASS A,CM_UNITCONVERSION B 
                                                WHERE A.BREEDCLASSID=B.BREEDCLASSID";

        #endregion

		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
            string strsql = "select max(UnitConversionID)+1 from CM_UnitConversion";
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
        public bool Exists(int UnitConversionID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CM_UnitConversion where UnitConversionID=@UnitConversionID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
		    db.AddInParameter(dbCommand, "UnitConversionID", DbType.Int32, UnitConversionID); 
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
		public int Add(ManagementCenter.Model.CM_UnitConversion model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CM_UnitConversion(");
            strSql.Append("Value,UnitIDTo,UnitIDFrom,BreedClassID)");

			strSql.Append(" values (");
            strSql.Append("@Value,@UnitIDTo,@UnitIDFrom,@BreedClassID)");
			strSql.Append(";select @@IDENTITY");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Value", DbType.Int32, model.Value);
			db.AddInParameter(dbCommand, "UnitIDTo", DbType.Int32, model.UnitIDTo);
			db.AddInParameter(dbCommand, "UnitIDFrom", DbType.Int32, model.UnitIDFrom);
            db.AddInParameter(dbCommand, "BreedClassID",DbType.Int32,model.BreedClassID);
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
		public bool Update(ManagementCenter.Model.CM_UnitConversion model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CM_UnitConversion set ");
			strSql.Append("Value=@Value,");
			strSql.Append("UnitIDTo=@UnitIDTo,");
			strSql.Append("UnitIDFrom=@UnitIDFrom,");
		    strSql.Append("BreedClassID=@BreedClassID");
            strSql.Append(" where UnitConversionID=@UnitConversionID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UnitConversionID", DbType.Int32, model.UnitConversionID);
            db.AddInParameter(dbCommand, "Value", DbType.Int32, model.Value);
			db.AddInParameter(dbCommand, "UnitIDTo", DbType.Int32, model.UnitIDTo);
			db.AddInParameter(dbCommand, "UnitIDFrom", DbType.Int32, model.UnitIDFrom);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);

			db.ExecuteNonQuery(dbCommand);
		    return true;

		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
        public bool Delete(int UnitConversionID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete CM_UnitConversion ");
            strSql.Append(" where UnitConversionID=@UnitConversionID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UnitConversionID", DbType.Int32,UnitConversionID);
			db.ExecuteNonQuery(dbCommand);
		    return true;
		}

        /// <summary>
        /// ����Ʒ��IDɾ���ֻ�_Ʒ��_���׵�λ����
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="db">���ݿ�</param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public bool DeleteUnitConversionByBreedClassID(int BreedClassID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete CM_UnitConversion ");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            if ((db==null))
            {
                 db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
            //db.ExecuteNonQuery(dbCommand);
            object obj;
            if (tran == null)
            {
                obj = db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                obj = db.ExecuteNonQuery(dbCommand, tran);
            }
            return true;
        }

        /// <summary>
        /// ����Ʒ��IDɾ���ֻ�_Ʒ��_���׵�λ����
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool DeleteUnitConversionByBreedClassID(int BreedClassID)
        {
            return DeleteUnitConversionByBreedClassID(BreedClassID, null, null);
           
        }

	    /// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
        public ManagementCenter.Model.CM_UnitConversion GetModel(int UnitConversionID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select UnitConversionID,Value,UnitIDTo,UnitIDFrom,BreedClassID from CM_UnitConversion ");
            strSql.Append(" where UnitConversionID=@UnitConversionID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UnitConversionID", DbType.Int32, UnitConversionID);
			ManagementCenter.Model.CM_UnitConversion model=null;
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
            strSql.Append("select UnitConversionID,Value,UnitIDTo,UnitIDFrom,BreedClassID ");
			strSql.Append(" FROM CM_UnitConversion ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			Database db = DatabaseFactory.CreateDatabase();
			return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
		}

		
		/// <summary>
		/// ��������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
		/// </summary>
		public List<ManagementCenter.Model.CM_UnitConversion> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select UnitConversionID,Value,UnitIDTo,UnitIDFrom,BreedClassID ");
			strSql.Append(" FROM CM_UnitConversion ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<ManagementCenter.Model.CM_UnitConversion> list = new List<ManagementCenter.Model.CM_UnitConversion>();
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

        #region ��ȡ�����ֻ�_Ʒ��_���׵�λ����

        /// <summary>
        /// ��ȡ�����ֻ�_Ʒ��_���׵�λ����
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMUnitConversion(string BreedClassName, int pageNo, int pageSize,
                                            out int rowCount)
        {
            //������ѯ
            if (BreedClassName != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(BreedClassName))
            {
                SQL_SELECT_CMUNITCONVERSION += "AND (BreedClassName LIKE  '%' + @BreedClassName + '%') ";
            }

            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_CMUNITCONVERSION);

            if (BreedClassName != AppGlobalVariable.INIT_STRING && BreedClassName != string.Empty)
            {
                database.AddInParameter(dbCommand, "BreedClassName", DbType.String, BreedClassName);
            }

            return CommPager.QueryPager(database, dbCommand, SQL_SELECT_CMUNITCONVERSION, pageNo, pageSize,
                                        out rowCount, "CM_UnitConversion");
        }

        #endregion

        #region �����ֻ�_Ʒ��_���׵�λ������е�Ʒ��ID��ȡƷ������

        /// <summary>
        /// �����ֻ�_Ʒ��_���׵�λ������е�Ʒ��ID��ȡƷ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetCMUnitConversionBreedClassName()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBREEDCLASSNAME_CMUNITCONVERSION);
            return database.ExecuteDataSet(dbCommand);
        }

        #endregion

		/// <summary>
		/// ����ʵ�������
		/// </summary>
		public ManagementCenter.Model.CM_UnitConversion ReaderBind(IDataReader dataReader)
		{
			ManagementCenter.Model.CM_UnitConversion model=new ManagementCenter.Model.CM_UnitConversion();
			object ojb;
            ojb = dataReader["UnitConversionID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UnitConversionID = (int)ojb;
            }
			ojb = dataReader["Value"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Value=(int)ojb;
			}
			ojb = dataReader["UnitIDTo"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.UnitIDTo=(int)ojb;
			}
			ojb = dataReader["UnitIDFrom"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.UnitIDFrom=(int)ojb;
			}
            ojb = dataReader["BreedClassID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassID = (int)ojb;
            }
			return model;
		}

		#endregion  ��Ա����
	}
}

