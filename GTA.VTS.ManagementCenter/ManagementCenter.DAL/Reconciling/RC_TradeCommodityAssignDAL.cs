using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Maticsoft.DBUtility; //�����������

namespace ManagementCenter.DAL
{
    /// <summary>
    /// ��������ϻ��������� ���ݷ�����RC_TradeCommodityAssignDAL��
    /// ���ߣ�������    �޸ģ�����ΰ
    /// ���ڣ�2008-11-18  2009-10-28
    /// </summary>
    public class RC_TradeCommodityAssignDAL
    {
        public RC_TradeCommodityAssignDAL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(MatchMachineID)+1 from RC_TradeCommodityAssign";
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
        public bool Exists(string CommodityCode, int MatchMachineID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select count(1) from RC_TradeCommodityAssign where CommodityCode=@CommodityCode and MatchMachineID=@MatchMachineID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
            db.AddInParameter(dbCommand, "MatchMachineID", DbType.Int32, MatchMachineID);
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
        public void Add(ManagementCenter.Model.RC_TradeCommodityAssign model, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into RC_TradeCommodityAssign(");
            strSql.Append("CommodityCode,MatchMachineID,CodeFormSource)");

            strSql.Append(" values (");
            strSql.Append("@CommodityCode,@MatchMachineID,@CodeFormSource)");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, model.CommodityCode);
            db.AddInParameter(dbCommand, "MatchMachineID", DbType.Int32, model.MatchMachineID);
            db.AddInParameter(dbCommand, "CodeFormSource", DbType.Int32, model.CodeFormSource);
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
        /// ��Ӵ�ϻ����������Ϣ
        /// </summary>
        /// <param name="model">��ϻ��������ʵ��</param>
        public void Add(ManagementCenter.Model.RC_TradeCommodityAssign model)
        {
            Add(model, null, null);
        }


        /// <summary>
        /// ����һ������  
        /// </summary>
        public void Update(string OldCommodityCode, string NewCommodityCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update RC_TradeCommodityAssign set CommodityCode=@NewCommodityCode ");

            strSql.Append(" where CommodityCode=@OldCommodityCode ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OldCommodityCode", DbType.String, OldCommodityCode);
            db.AddInParameter(dbCommand, "NewCommodityCode", DbType.String, NewCommodityCode);//DbType.Single
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(string CommodityCode, int MatchMachineID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete RC_TradeCommodityAssign ");
            strSql.Append(" where CommodityCode=@CommodityCode and MatchMachineID=@MatchMachineID ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
            db.AddInParameter(dbCommand, "MatchMachineID", DbType.Int32, MatchMachineID);
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
        /// ɾ�����ݸ�����Ʒ����
        /// </summary>
        public void DeleteByCommodityCode(string CommodityCode, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete RC_TradeCommodityAssign ");
            strSql.Append(" where CommodityCode=@CommodityCode ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
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
        /// ���ݴ���ɾ����ϻ����������еĴ���
        /// </summary>
        /// <param name="CommodityCode">����</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        public bool DeleteRCByCommodityCode(string CommodityCode, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete RC_TradeCommodityAssign ");
            strSql.Append(" where CommodityCode=@CommodityCode ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
            if (tran == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
            return true;
        }


        /// <summary>
        /// ɾ�����ݸ��ݴ�ϻ�
        /// </summary>
        public void DeleteByMachineID(int MatchMachineID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete RC_TradeCommodityAssign ");
            strSql.Append(" where MatchMachineID=@MatchMachineID ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "MatchMachineID", DbType.Int32, MatchMachineID);
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
        /// ���ݴ�ϻ�IDɾ�����ݸ��ݴ�ϻ�����
        /// </summary>
        /// <param name="MatchMachineID">��ϻ�ID</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        public bool DeleteRCTradeCommodityAByMachineID(int MatchMachineID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete RC_TradeCommodityAssign ");
            strSql.Append(" where MatchMachineID=@MatchMachineID ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "MatchMachineID", DbType.Int32, MatchMachineID);
            if (tran == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, tran);
            }
            return true;
        }

        /// <summary>
        /// ���ݴ�ϻ�IDɾ�����ݸ��ݴ�ϻ�����
        /// </summary>
        /// <param name="MatchMachineID">��ϻ�ID</param>
        /// <returns></returns>
        public bool DeleteRCTradeCommodityAByMachineID(int MatchMachineID)
        {
            return DeleteRCTradeCommodityAByMachineID(MatchMachineID, null, null);
        }

        /// <summary>
        /// ɾ����������
        /// </summary>
        public void DeleteAll(DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete RC_TradeCommodityAssign ");
            if (db == null) db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
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
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.RC_TradeCommodityAssign GetModel(string CommodityCode, int MatchMachineID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommodityCode,MatchMachineID,CodeFormSource from RC_TradeCommodityAssign ");
            strSql.Append(" where CommodityCode=@CommodityCode and MatchMachineID=@MatchMachineID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
            db.AddInParameter(dbCommand, "MatchMachineID", DbType.Int32, MatchMachineID);
            ManagementCenter.Model.RC_TradeCommodityAssign model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommodityCode,MatchMachineID,CodeFormSource ");
            strSql.Append(" FROM RC_TradeCommodityAssign ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "RC_TradeCommodityAssign");
			db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "ID");
			db.AddInParameter(dbCommand, "PageSize", DbType.Int32, PageSize);
			db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
			db.AddInParameter(dbCommand, "IsReCount", DbType.Boolean, 0);
			db.AddInParameter(dbCommand, "OrderType", DbType.Boolean, 0);
			db.AddInParameter(dbCommand, "strWhere", DbType.AnsiString, strWhere);
			return db.ExecuteDataSet(dbCommand);
		}*/

        #region ��ô�ϻ��������������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// <summary>
        /// ��ô�ϻ��������������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.RC_TradeCommodityAssign> GetListArray(string strWhere, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommodityCode,MatchMachineID,CodeFormSource ");
            strSql.Append(" FROM RC_TradeCommodityAssign ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.RC_TradeCommodityAssign> list =
                new List<ManagementCenter.Model.RC_TradeCommodityAssign>();
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            if (tran == null)
            {
                using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, strSql.ToString()))
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind(dataReader));
                    }
                }
            }
            else
            {
                using (IDataReader dataReader = db.ExecuteReader(tran, CommandType.Text, strSql.ToString()))
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind(dataReader));
                    }
                }
            }
            return list;
        }
        #endregion

        #region ��ô�ϻ��������������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// <summary>
        /// ��ô�ϻ��������������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.RC_TradeCommodityAssign> GetListArray(string strWhere)
        {
            return GetListArray(strWhere, null, null);
        }
        #endregion

        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.RC_TradeCommodityAssign ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.RC_TradeCommodityAssign model = new ManagementCenter.Model.RC_TradeCommodityAssign();
            object ojb;
            model.CommodityCode = dataReader["CommodityCode"].ToString();
            ojb = dataReader["MatchMachineID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MatchMachineID = (int)ojb;
            }
            ojb = dataReader["CodeFormSource"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CodeFormSource = (int)ojb;
            }
            return model;
        }

        /// <summary>
        /// �õ���Ʒ�����б���ݽ�����ID
        /// </summary>
        /// <param name="BourseTypeID"></param>
        /// <returns></returns>
        public DataSet GetCodeListByBourseTypeID(int BourseTypeID)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append(
            //    "select A.CommodityCode,A.BreedClassID,C.BourseTypeID from CM_Commodity as A ,CM_BreedClass AS B,CM_BourseType AS C ");
            //strSql.Append(
            //    " WHERE A.BreedClassID=B.BreedClassID AND B.BourseTypeID=C.BourseTypeID AND C.BourseTypeID=@BourseTypeID ");

            string strSql = @"select A.CommodityCode,A.BreedClassID,C.BourseTypeID,[CodeFormSource]=1 
                            from CM_Commodity as A ,CM_BreedClass AS B,CM_BourseType AS C
                            WHERE A.BreedClassID=B.BreedClassID 
                            AND B.BourseTypeID=C.BourseTypeID 
                            AND C.BourseTypeID=@BourseTypeID
                            union
                            select A.HKCommodityCode as CommodityCode,A.BreedClassID,C.BourseTypeID,[CodeFormSource]=2 
                            from HK_Commodity as A ,CM_BreedClass AS B,CM_BourseType AS C
                            WHERE A.BreedClassID=B.BreedClassID 
                            AND B.BourseTypeID=C.BourseTypeID 
                            AND C.BourseTypeID=@BourseTypeID ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, BourseTypeID);
            return db.ExecuteDataSet(dbCommand);
        }


        /// <summary>
        /// ���ݴ�ϻ��õ���ϻ���ϵĴ���
        /// </summary>
        /// <param name="MatchMachineID">��ϻ����</param>
        /// <returns></returns>
        public DataSet GetCodeListByMatchMachineID(int MatchMachineID)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select a.CommodityCode, b.CommodityName from RC_TradeCommodityAssign as a,CM_commodity as b ");
            //strSql.Append(" where a.CommodityCode=b.CommodityCode and MatchMachineID=@MatchMachineID ");

            strSql.Append("select a.CommodityCode, b.CommodityName from RC_TradeCommodityAssign as a ");
            strSql.Append(" inner join CM_commodity as b on a.CommodityCode=b.CommodityCode and MatchMachineID=@MatchMachineID ");
            strSql.Append(" union  ");
            strSql.Append("select a.CommodityCode, c.HKCommodityName from RC_TradeCommodityAssign as a ");
            strSql.Append(" inner join HK_Commodity as c on a.CommodityCode=c.HKCommodityCode and MatchMachineID=@MatchMachineID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "MatchMachineID", DbType.Int32, MatchMachineID);
            return db.ExecuteDataSet(dbCommand);
        }

        /// <summary>
        /// �õ�������û�з������Ʒ����
        /// </summary>
        /// <param name="BourseTypeID"></param>
        /// <returns></returns>
        public DataSet GetNotAssignCodeByBourseTypeID(int BourseTypeID)
        {
            //            string strSql =
            //                @"select CM_commodity.CommodityCode,CM_commodity.CommodityName from CM_commodity,CM_BreedClass 
            //                                    WHERE CM_commodity.BreedClassID=CM_BreedClass.BreedClassID
            //                                    AND CM_BreedClass.BourseTypeID=@BourseTypeID 
            //                                    AND CM_commodity.CommodityCode NOT IN 
            //                                    (select RC_TradeCommodityAssign.CommodityCode from RC_TradeCommodityAssign,RC_MatchMachine
            //                                     where RC_TradeCommodityAssign.MatchMachineID=RC_MatchMachine.MatchMachineID
            //                                        and RC_MatchMachine.BourseTypeID=@BourseTypeID)";
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select CommodityCode from RC_TradeCommodityAssign where MatchMachineID=@MatchMachineID ");

            string strSql = @"select c.CommodityCode,c.CommodityName,[CodeFormSource]=1
                            from CM_commodity as c,CM_BreedClass as b
                            WHERE c.BreedClassID=b.BreedClassID
                            AND b.BourseTypeID=@BourseTypeID 
                            AND c.IsExpired<>1
                            AND c.CommodityCode NOT IN 
                            (select RC_TradeCommodityAssign.CommodityCode from RC_TradeCommodityAssign,RC_MatchMachine
                             where RC_TradeCommodityAssign.MatchMachineID=RC_MatchMachine.MatchMachineID
	                            and RC_MatchMachine.BourseTypeID=@BourseTypeID)   
                            union
                             select c.HKCommodityCode as CommodityCode,c.HKCommodityName as CommodityName ,[CodeFormSource]=2
                            from HK_Commodity as c,CM_BreedClass as b
                            WHERE c.BreedClassID=b.BreedClassID
                            AND b.BourseTypeID=@BourseTypeID 
                            AND c.HKCommodityCode NOT IN 
                            (select RC_TradeCommodityAssign.CommodityCode from RC_TradeCommodityAssign,RC_MatchMachine
                             where RC_TradeCommodityAssign.MatchMachineID=RC_MatchMachine.MatchMachineID
	                            and RC_MatchMachine.BourseTypeID=@BourseTypeID) ";


            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BourseTypeID", DbType.Int32, BourseTypeID);
            return db.ExecuteDataSet(dbCommand);
        }

        #endregion  ��Ա����
    }
}