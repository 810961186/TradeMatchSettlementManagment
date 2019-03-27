using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using ManagementCenter.Model.CommonClass;
using ManagementCenter.Model.CommonTable;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Maticsoft.DBUtility;//�����������

namespace ManagementCenter.DAL
{
    /// <summary>
    ///����:������Ʒ ���ݷ�����CM_CommodityDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-20
    /// </summary>
    public class CM_CommodityDAL
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public CM_CommodityDAL()
        { }
        #endregion

        #region SQL

        /// <summary>
        /// ��ȡ������Ʒ
        /// </summary>
        private string SQL_SELECT_CMCOMMODITY =
            @"SELECT * FROM CM_COMMODITY WHERE 1=1 ";

        /// <summary>
        /// ��ȡƷ�����͹�ָ�ڻ�����Ʒ����
        /// </summary>
        private string SQL_SELECTCOMMODITYCODE_CMCOMMODITY = @"SELECT B.COMMODITYCODE,B.COMMODITYNAME 
                                                            FROM CM_BREEDCLASS AS A,CM_COMMODITY AS B
                                                            WHERE A.BREEDCLASSID=B.BREEDCLASSID 
                                                            AND BREEDCLASSTYPEID=3 AND B.ISEXPIRED=2";

        #endregion
        #region  ��Ա����

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string CommodityCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CM_Commodity where CommodityCode=@CommodityCode ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
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
        public bool Add(ManagementCenter.Model.CM_Commodity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CM_Commodity(");
            strSql.Append("CommodityCode,CommodityName,BreedClassID,LabelCommodityCode,GoerScale,MarketDate,StockPinYin,turnovervolume,IsExpired,ISSysDefaultCode)");

            strSql.Append(" values (");
            strSql.Append("@CommodityCode,@CommodityName,@BreedClassID,@LabelCommodityCode,@GoerScale,@MarketDate,@StockPinYin,@turnovervolume,@IsExpired,@ISSysDefaultCode)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, model.CommodityCode);
            db.AddInParameter(dbCommand, "CommodityName", DbType.String, model.CommodityName);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
            if (string.IsNullOrEmpty(model.LabelCommodityCode) || model.LabelCommodityCode == AppGlobalVariable.INIT_STRING)
            {
                db.AddInParameter(dbCommand, "LabelCommodityCode", DbType.String, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "LabelCommodityCode", DbType.String, model.LabelCommodityCode);
            }
            if (model.GoerScale == AppGlobalVariable.INIT_DECIMAL)
            {
                db.AddInParameter(dbCommand, "GoerScale", DbType.Decimal, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "GoerScale", DbType.Decimal, model.GoerScale);

            }
            if (model.MarketDate == AppGlobalVariable.INIT_DATETIME)
            {
                db.AddInParameter(dbCommand, "MarketDate", DbType.DateTime, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "MarketDate", DbType.DateTime, model.MarketDate);

            }
            db.AddInParameter(dbCommand, "StockPinYin", DbType.String, model.StockPinYin);
            db.AddInParameter(dbCommand, "turnovervolume", DbType.Double, model.turnovervolume);
            if (model.IsExpired == AppGlobalVariable.INIT_INT)
            {
                model.IsExpired = (int)Types.IsYesOrNo.No;
                db.AddInParameter(dbCommand, "IsExpired", DbType.Int32, model.IsExpired);
            }
            else
            {
                db.AddInParameter(dbCommand, "IsExpired", DbType.Int32, model.IsExpired);

            }
            if (model.ISSysDefaultCode == AppGlobalVariable.INIT_INT)
            {
                model.ISSysDefaultCode = (int)Types.IsYesOrNo.No;
                db.AddInParameter(dbCommand, "ISSysDefaultCode", DbType.Int32, model.ISSysDefaultCode);
            }
            else
            {
                db.AddInParameter(dbCommand, "ISSysDefaultCode", DbType.Int32, model.ISSysDefaultCode);

            }
            db.ExecuteNonQuery(dbCommand);
            return true;
        }

        /// <summary>
        /// ���½�����Ʒ(����2��0����ֻ�����޸�Ʒ�����ͣ������ֶβ������޸�)
        /// </summary>
        /// <param name="model">������Ʒʵ��</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.CM_Commodity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CM_Commodity set ");
            //strSql.Append("CommodityName=@CommodityName,");
            strSql.Append("BreedClassID=@BreedClassID,");
            // strSql.Append("LabelCommodityCode=@LabelCommodityCode,");
            //strSql.Append("GoerScale=@GoerScale,");
            strSql.Append("MarketDate=@MarketDate,");
            //strSql.Append("StockPinYin=@StockPinYin,");
            //strSql.Append("turnovervolume=@turnovervolume,");
            strSql.Append("IsExpired=@IsExpired,");
            strSql.Append("ISSysDefaultCode=@ISSysDefaultCode");
            strSql.Append(" where CommodityCode=@CommodityCode ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, model.CommodityCode);
            //db.AddInParameter(dbCommand, "CommodityName", DbType.String, model.CommodityName);
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, model.BreedClassID);
            
            if (model.MarketDate == AppGlobalVariable.INIT_DATETIME)
            {
                db.AddInParameter(dbCommand, "MarketDate", DbType.DateTime, DBNull.Value);
            }
            else
            {
                db.AddInParameter(dbCommand, "MarketDate", DbType.DateTime, model.MarketDate);

            }
            //db.AddInParameter(dbCommand, "StockPinYin", DbType.String, model.StockPinYin);
            //db.AddInParameter(dbCommand, "turnovervolume", DbType.Double, model.turnovervolume);
            if (model.IsExpired == AppGlobalVariable.INIT_INT)
            {
                model.IsExpired = (int)Types.IsYesOrNo.No;
                db.AddInParameter(dbCommand, "IsExpired", DbType.Int32, model.IsExpired);
            }
            else
            {
                db.AddInParameter(dbCommand, "IsExpired", DbType.Int32, model.IsExpired);

            }
            if (model.ISSysDefaultCode == AppGlobalVariable.INIT_INT)
            {
                model.ISSysDefaultCode = (int)Types.IsYesOrNo.No;
                db.AddInParameter(dbCommand, "ISSysDefaultCode", DbType.Int32, model.ISSysDefaultCode);
            }
            else
            {
                db.AddInParameter(dbCommand, "ISSysDefaultCode", DbType.Int32, model.ISSysDefaultCode);

            }
            db.ExecuteNonQuery(dbCommand);
            return true;
        }

        #region ������Ʒ���룬�����ڻ������Ƿ����״̬
        /// <summary>
        /// ������Ʒ���룬�����ڻ������Ƿ����״̬
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <param name="IsExpired">�ڻ������Ƿ����</param>
        /// <returns></returns>
        public bool Update(string CommodityCode, int IsExpired)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CM_Commodity set ");
            strSql.Append("IsExpired=@IsExpired");
            strSql.Append(" where CommodityCode=@CommodityCode ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
            db.AddInParameter(dbCommand, "IsExpired", DbType.Int32, IsExpired);
            db.ExecuteNonQuery(dbCommand);
            return true;
        }
        #endregion

        #region  ����Ʒ��ID�����½�����Ʒ���е�Ʒ��ID
        /// <summary>
        /// ����Ʒ��ID�����½�����Ʒ���е�Ʒ��ID
        /// </summary>
        /// <param name="OldBreedClassID">�޸�ǰ��Ʒ��ID</param>
        /// <param name="NewBreedClassID">�޸ĺ��Ʒ��ID</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <returns></returns>
        public bool UpdateBreedClassID(int OldBreedClassID, int NewBreedClassID, DbTransaction tran, Database db)
        {
            var strSql = new StringBuilder();
            strSql.Append("update CM_Commodity set ");
            strSql.Append("BreedClassID=@NewBreedClassID");
            strSql.Append(" where BreedClassID=@OldBreedClassID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OldBreedClassID", DbType.Int32, OldBreedClassID);
            db.AddInParameter(dbCommand, "NewBreedClassID", DbType.Int32, NewBreedClassID);
            //db.ExecuteNonQuery(dbCommand);
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
        #endregion

        #region  ����Ʒ��ID�����½�����Ʒ���е�Ʒ��ID(������)
        /// <summary>
        /// ����Ʒ��ID�����½�����Ʒ���е�Ʒ��ID(������)
        /// </summary>
        /// <param name="OldBreedClassID">�޸�ǰ��Ʒ��ID</param>
        /// <param name="NewBreedClassID">�޸ĺ��Ʒ��ID</param>
        /// <returns></returns>
        public bool UpdateBreedClassID(int OldBreedClassID, int NewBreedClassID)
        {
            return UpdateBreedClassID(OldBreedClassID, NewBreedClassID, null, null);
        }
        #endregion

        /// <summary>
        /// ���ݴ���ɾ�������¼
        /// </summary>
        /// <param name="CommodityCode"></param>
        /// <returns></returns>
        public bool Delete(string CommodityCode)
        {
            return Delete(CommodityCode, null, null);
        }

        /// <summary>
        /// ���ݴ���ɾ�������¼
        /// </summary>
        /// <param name="CommodityCode">����</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Delete(string CommodityCode, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete CM_Commodity ");
            strSql.Append(" where CommodityCode=@CommodityCode ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();

            }
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
        /// ����Ʒ��IDɾ������Ʒ��
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="tran"></param>
        /// <param name="db">���ݿ�</param>
        /// <returns></returns>
        public bool DeleteCommodityByBreedClassID(int BreedClassID, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete CM_Commodity ");
            strSql.Append(" where BreedClassID=@BreedClassID ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();

            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
            // db.ExecuteNonQuery(dbCommand);
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
        ///  ����Ʒ��IDɾ������Ʒ��
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool DeleteCommodityByBreedClassID(int BreedClassID)
        {
            return DeleteCommodityByBreedClassID(BreedClassID, null, null);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_Commodity GetModel(string CommodityCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommodityCode,CommodityName,BreedClassID,LabelCommodityCode,GoerScale,MarketDate,StockPinYin,turnovervolume,IsExpired,ISSysDefaultCode from CM_Commodity ");
            strSql.Append(" where CommodityCode=@CommodityCode ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
            ManagementCenter.Model.CM_Commodity model = null;
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
            strSql.Append("select CommodityCode,CommodityName,BreedClassID,LabelCommodityCode,GoerScale,MarketDate,StockPinYin,turnovervolume,IsExpired,ISSysDefaultCode ");
            strSql.Append(" FROM CM_Commodity ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// ��������б���DataSetЧ�ʸߣ��Ƽ�ʹ�ã�
        /// </summary>
        public List<ManagementCenter.Model.CM_Commodity> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommodityCode,CommodityName,BreedClassID,LabelCommodityCode,GoerScale,MarketDate,StockPinYin,turnovervolume,IsExpired,ISSysDefaultCode ");
            strSql.Append(" FROM CM_Commodity ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.CM_Commodity> list = new List<ManagementCenter.Model.CM_Commodity>();
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
        /// ��ȡ���д��뼰�������̼�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_Commodity> GetListCMCommodityAndClosePrice(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select CommodityCode,CommodityName,BreedClassID,LabelCommodityCode,GoerScale,MarketDate,StockPinYin,turnovervolume,IsExpired,ISSysDefaultCode ");
            //strSql.Append(" FROM CM_Commodity ");
            strSql.Append("select a.*,b.ClosePrice from CM_Commodity a left join ClosePriceInfo b ");
            strSql.Append(" on a.CommodityCode=b.StockCode and b.BreedClassTypeID<>4 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.CM_Commodity> list = new List<ManagementCenter.Model.CM_Commodity>();
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

        #region ��ȡ���н�����Ʒ

        /// <summary>
        /// ��ȡ���н�����Ʒ
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <param name="CommodityName">��Ʒ����</param>
        ///  <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMCommodity(string CommodityCode, string CommodityName, int BreedClassID, int pageNo, int pageSize,
                                            out int rowCount)
        {
            //������ѯ
            if ((CommodityCode != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(CommodityCode))
                || (CommodityName != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(CommodityName)))
            {
                SQL_SELECT_CMCOMMODITY += " AND (CommodityCode LIKE  '%' + @CommodityCode + '%' ";
                //}
                //if (CommodityName != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(CommodityName))
                //{
                SQL_SELECT_CMCOMMODITY += " OR CommodityName LIKE  '%' + @CommodityName + '%') ";
            }
            if (BreedClassID != AppGlobalVariable.INIT_INT)
            {
                SQL_SELECT_CMCOMMODITY += "AND (BreedClassID=@BreedClassID) ";
            }
            SQL_SELECT_CMCOMMODITY += " AND ISEXPIRED<>1  ";

            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECT_CMCOMMODITY);

            if (CommodityCode != AppGlobalVariable.INIT_STRING && CommodityCode != string.Empty)
            {
                database.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
            }
            if (CommodityName != AppGlobalVariable.INIT_STRING && CommodityName != string.Empty)
            {
                database.AddInParameter(dbCommand, "CommodityName", DbType.String, CommodityName);
            }
            if (BreedClassID != AppGlobalVariable.INIT_INT)
            {
                database.AddInParameter(dbCommand, "BreedClassID", DbType.Int32, BreedClassID);
            }
            return CommPager.QueryPager(database, dbCommand, SQL_SELECT_CMCOMMODITY, pageNo, pageSize,
                                        out rowCount, "CM_Commodity");
        }

        #endregion

        #region ��ȡƷ�����͹�ָ�ڻ�����Ʒ����
        /// <summary>
        /// ��ȡƷ�����͹�ָ�ڻ�����Ʒ����
        /// </summary>
        /// <returns></returns>
        public DataSet GetQHSIFCommodityCode()
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTCOMMODITYCODE_CMCOMMODITY);
            return database.ExecuteDataSet(dbCommand);
        }
        #endregion

        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.CM_Commodity ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.CM_Commodity model = new ManagementCenter.Model.CM_Commodity();
            object ojb;
            model.CommodityCode = dataReader["CommodityCode"].ToString();
            model.CommodityName = dataReader["CommodityName"].ToString();
            model.StockPinYin = dataReader["StockPinYin"].ToString();
            ojb = dataReader["BreedClassID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassID = (int)ojb;
            }
            model.LabelCommodityCode = dataReader["LabelCommodityCode"].ToString();
            ojb = dataReader["GoerScale"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoerScale = (decimal)ojb;
            }
            ojb = dataReader["MarketDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MarketDate = (DateTime)ojb;
            }
            ojb = dataReader["turnovervolume"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.turnovervolume = (double)ojb;
            }
            ojb = dataReader["IsExpired"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsExpired = (int)ojb;
            }
            ojb = dataReader["ISSysDefaultCode"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ISSysDefaultCode = (int)ojb;
            }
            return model;
        }

        #endregion  ��Ա����

        #region �ṩǰ̨���񷽷�
        /// <summary>
        /// ���ݲ�ѯ������ȡ��ҵ��ʶ����
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns>������ҵ��ʶ����</returns>
        public List<ManagementCenter.Model.CommonTable.OnstageCommodity> GetCommodityListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CM_Commodity.*, Nindcd from CM_Commodity left join StockInfo on CommodityCode=StockCode ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.CommonTable.OnstageCommodity> list = new List<ManagementCenter.Model.CommonTable.OnstageCommodity>();
            Database db = DatabaseFactory.CreateDatabase();
            using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, strSql.ToString()))
            {
                while (dataReader.Read())
                {
                    list.Add(_ReaderBind(dataReader));
                }
            }
            return list;
        }

        /// <summary>
        /// ��ҵ��ʶʵ��
        /// </summary>
        /// <param name="dataReader">������</param>
        /// <returns>������ҵ��ʶʵ��</returns>
        public ManagementCenter.Model.CommonTable.OnstageCommodity _ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.CommonTable.OnstageCommodity model = new OnstageCommodity();
            object ojb;
            model.CommodityCode = dataReader["CommodityCode"].ToString();
            model.CommodityName = dataReader["CommodityName"].ToString();
            model.StockPinYin = dataReader["StockPinYin"].ToString();
            model.Nindcd = dataReader["Nindcd"].ToString();
            ojb = dataReader["BreedClassID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreedClassID = (int)ojb;
            }
            model.LabelCommodityCode = dataReader["LabelCommodityCode"].ToString();
            ojb = dataReader["GoerScale"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoerScale = (decimal)ojb;
            }
            ojb = dataReader["MarketDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MarketDate = (DateTime)ojb;
            }
            ojb = dataReader["turnovervolume"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.turnovervolume = (double)ojb;
            }
            ojb = dataReader["IsExpired"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsExpired = (int)ojb;
            }
            return model;
        }
        #endregion
    }
}

