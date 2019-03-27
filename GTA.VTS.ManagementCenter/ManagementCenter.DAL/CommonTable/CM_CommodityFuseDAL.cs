using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using ManagementCenter.Model.CommonClass;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Maticsoft.DBUtility; //�����������

namespace ManagementCenter.DAL
{
    /// <summary>
    ///�������ɽ�����Ʒ_�۶ϱ�  ���ݷ�����CM_CommodityFuseDAL��
    ///���ߣ�����ΰ
    ///����:2008-11-20
    /// </summary>
    public class CM_CommodityFuseDAL
    {
        public CM_CommodityFuseDAL()
        {
        }

        #region SQL

        /// <summary>
        /// ���ݲ�ѯ�������ؿɽ�����Ʒ_�۶�
        /// </summary>
        private string SQL_SELECTBREEDCLASSNAME_CMCOMMODITYFUSES =
            @"SELECT A.* FROM CM_COMMODITYFUSE AS A,CM_COMMODITY AS B 
                                                            WHERE B.COMMODITYCODE=A.COMMODITYCODE ";

        #endregion

        #region  ��Ա����

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string CommodityCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CM_CommodityFuse where CommodityCode=@CommodityCode ");
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
        public bool Add(ManagementCenter.Model.CM_CommodityFuse model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CM_CommodityFuse(");
            //strSql.Append("NoAllowStartTime,TriggeringScale,TriggeringDuration,FuseDurationLimit,FuseTimeOfDay,CommodityCode)");
            strSql.Append("TriggeringScale,TriggeringDuration,FuseDurationLimit,FuseTimeOfDay,CommodityCode)");

            strSql.Append(" values (");
            strSql.Append("@TriggeringScale,@TriggeringDuration,@FuseDurationLimit,@FuseTimeOfDay,@CommodityCode)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());

            db.AddInParameter(dbCommand, "TriggeringScale", DbType.Decimal, model.TriggeringScale);
            db.AddInParameter(dbCommand, "TriggeringDuration", DbType.Int32, model.TriggeringDuration);
            db.AddInParameter(dbCommand, "FuseDurationLimit", DbType.Int32, model.FuseDurationLimit);
            db.AddInParameter(dbCommand, "FuseTimeOfDay", DbType.Int32, model.FuseTimeOfDay);
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, model.CommodityCode);
            //db.ExecuteNonQuery(dbCommand);
            //return true;
            return (db.ExecuteNonQuery(dbCommand) == 1);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(ManagementCenter.Model.CM_CommodityFuse model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CM_CommodityFuse set ");
            //strSql.Append("NoAllowStartTime=@NoAllowStartTime,");
            strSql.Append("TriggeringScale=@TriggeringScale,");
            strSql.Append("TriggeringDuration=@TriggeringDuration,");
            strSql.Append("FuseDurationLimit=@FuseDurationLimit,");
            strSql.Append("FuseTimeOfDay=@FuseTimeOfDay");
            strSql.Append(" where CommodityCode=@CommodityCode ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            //db.AddInParameter(dbCommand, "NoAllowStartTime", DbType.Int32, model.NoAllowStartTime);
            db.AddInParameter(dbCommand, "TriggeringScale", DbType.Decimal, model.TriggeringScale);
            db.AddInParameter(dbCommand, "TriggeringDuration", DbType.Int32, model.TriggeringDuration);
            db.AddInParameter(dbCommand, "FuseDurationLimit", DbType.Int32, model.FuseDurationLimit);
            db.AddInParameter(dbCommand, "FuseTimeOfDay", DbType.Int32, model.FuseTimeOfDay);
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, model.CommodityCode);
            db.ExecuteNonQuery(dbCommand);
            //return (db.ExecuteNonQuery(dbCommand) == 1);
            return true;
        }

        /// <summary>
        /// ������Ʒ����ɾ���ɽ�����Ʒ_�۶�
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Delete(string CommodityCode, DbTransaction tran, Database db)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete CM_CommodityFuse ");
            strSql.Append(" where CommodityCode=@CommodityCode ");
            if (db == null)
            {
                db = DatabaseFactory.CreateDatabase();
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
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
        /// ������Ʒ����ɾ���ɽ�����Ʒ_�۶�
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <returns></returns>
        public bool Delete(string CommodityCode)
        {
            return Delete(CommodityCode, null, null);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_CommodityFuse GetModel(string CommodityCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select TriggeringScale,TriggeringDuration,FuseDurationLimit,FuseTimeOfDay,CommodityCode from CM_CommodityFuse ");
            strSql.Append(" where CommodityCode=@CommodityCode ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
            ManagementCenter.Model.CM_CommodityFuse model = null;
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
            strSql.Append("select TriggeringScale,TriggeringDuration,FuseDurationLimit,FuseTimeOfDay,CommodityCode ");
            strSql.Append(" FROM CM_CommodityFuse ");
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
        public List<ManagementCenter.Model.CM_CommodityFuse> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TriggeringScale,TriggeringDuration,FuseDurationLimit,FuseTimeOfDay,CommodityCode ");
            strSql.Append(" FROM CM_CommodityFuse ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<ManagementCenter.Model.CM_CommodityFuse> list = new List<ManagementCenter.Model.CM_CommodityFuse>();
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

        #region ��ȡ���пɽ�����Ʒ_�۶�

        /// <summary>
        /// ��ȡ���пɽ�����Ʒ_�۶�
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMCommodityFuse(string CommodityCode, int pageNo, int pageSize,
                                             out int rowCount)
        {
            //������ѯ
            if (CommodityCode != AppGlobalVariable.INIT_STRING && !string.IsNullOrEmpty(CommodityCode))
            {
                SQL_SELECTBREEDCLASSNAME_CMCOMMODITYFUSES += "AND (A.CommodityCode LIKE  '%' + @CommodityCode + '%') ";
            }
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand(SQL_SELECTBREEDCLASSNAME_CMCOMMODITYFUSES);
            if (CommodityCode != AppGlobalVariable.INIT_STRING && CommodityCode != string.Empty)
            {
                database.AddInParameter(dbCommand, "CommodityCode", DbType.String, CommodityCode);
            }

            return CommPager.QueryPager(database, dbCommand, SQL_SELECTBREEDCLASSNAME_CMCOMMODITYFUSES, pageNo, pageSize,
                                        out rowCount, "CM_CommodityFuse");
        }

        #endregion

        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public ManagementCenter.Model.CM_CommodityFuse ReaderBind(IDataReader dataReader)
        {
            ManagementCenter.Model.CM_CommodityFuse model = new ManagementCenter.Model.CM_CommodityFuse();
            object ojb;
            //ojb = dataReader["NoAllowStartTime"];
            //if(ojb != null && ojb != DBNull.Value)
            //{
            //    model.NoAllowStartTime=(int)ojb;
            //}
            ojb = dataReader["TriggeringScale"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TriggeringScale = (decimal) ojb;
            }
            ojb = dataReader["TriggeringDuration"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TriggeringDuration = (int) ojb;
            }
            ojb = dataReader["FuseDurationLimit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FuseDurationLimit = (int) ojb;
            }
            ojb = dataReader["FuseTimeOfDay"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FuseTimeOfDay = (int) ojb;
            }
            model.CommodityCode = dataReader["CommodityCode"].ToString();
            return model;
        }

        #endregion  ��Ա����
    }
}