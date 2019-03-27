#region Using Namespace

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReckoningCounter.Entity;
using ReckoningCounter.Model;
using ReckoningCounter.Entity.Model.QueryFilter;

#endregion

namespace ReckoningCounter.DAL.Data
{
    /// <summary>
    /// ���ݷ�����XH_CapitalAccountFreezeTableDal��
    /// </summary>
    public class XH_CapitalAccountFreezeTableDal
    {
        #region  ��Ա����

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int CapitalFreezeLogoId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select count(1) from XH_CapitalAccountFreezeTable where CapitalFreezeLogoId=@CapitalFreezeLogoId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalFreezeLogoId", DbType.Int32, CapitalFreezeLogoId);
            int cmdresult;
            object obj = db.ExecuteScalar(dbCommand);
            if ((Equals(obj, null)) || (Equals(obj, DBNull.Value)))
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
        /// ��������б�
        /// </summary>
        public List<XH_CapitalAccountFreezeTableInfo> GetListArrayWithNoLock(string enTrustNumber, int freezeType)
        {
            string format = "EntrustNumber='{0}' AND FreezeTypeLogo={1}";
            string where = string.Format(format, enTrustNumber, freezeType);

            return GetListArrayWithNoLock(where);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public  List<XH_CapitalAccountFreezeTableInfo> GetListArray(string enTrustNumber, int freezeType)
        {
            string format = "EntrustNumber='{0}' AND FreezeTypeLogo={1}";
            string where = string.Format(format, enTrustNumber, freezeType);

            return GetListArray(where);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public  List<XH_CapitalAccountFreezeTableInfo> GetListArrayWithNoLock(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select FreezeTime,FreezeCapitalAmount,CapitalFreezeLogoId,ThawTime,FreezeTypeLogo,EntrustNumber,CapitalAccountLogo,OweCosting,FreezeCost");
            strSql.Append(" FROM XH_CapitalAccountFreezeTable  WITH (NOLOCK)");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<XH_CapitalAccountFreezeTableInfo> list = new List<XH_CapitalAccountFreezeTableInfo>();
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
        /// ����һ������
        /// </summary>
        public int Add(XH_CapitalAccountFreezeTableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_CapitalAccountFreezeTable(");
            strSql.Append(
                "FreezeTime,FreezeCapitalAmount,ThawTime,FreezeTypeLogo,EntrustNumber,CapitalAccountLogo,OweCosting,FreezeCost)");

            strSql.Append(" values (");
            strSql.Append(
                "@FreezeTime,@FreezeCapitalAmount,@ThawTime,@FreezeTypeLogo,@EntrustNumber,@CapitalAccountLogo,@OweCosting,@FreezeCost)");
            strSql.Append(";select @@IDENTITY");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FreezeTime", DbType.DateTime, model.FreezeTime);
            db.AddInParameter(dbCommand, "FreezeCapitalAmount", DbType.Decimal, model.FreezeCapitalAmount);
            db.AddInParameter(dbCommand, "ThawTime", DbType.DateTime, model.ThawTime);
            db.AddInParameter(dbCommand, "FreezeTypeLogo", DbType.Int32, model.FreezeTypeLogo);
         
            db.AddInParameter(dbCommand, "EntrustNumber", DbType.AnsiString, model.EntrustNumber);
            db.AddInParameter(dbCommand, "CapitalAccountLogo", DbType.Int32, model.CapitalAccountLogo);
            db.AddInParameter(dbCommand, "OweCosting", DbType.Decimal, model.OweCosting);
            db.AddInParameter(dbCommand, "FreezeCost", DbType.Decimal, model.FreezeCost);
            int result;
            object obj = db.ExecuteScalar(dbCommand);
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(XH_CapitalAccountFreezeTableInfo model, Database db, DbTransaction transaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_CapitalAccountFreezeTable(");
            strSql.Append(
                "FreezeTime,FreezeCapitalAmount,ThawTime,FreezeTypeLogo,EntrustNumber,CapitalAccountLogo,OweCosting,FreezeCost)");

            strSql.Append(" values (");
            strSql.Append(
                "@FreezeTime,@FreezeCapitalAmount,@ThawTime,@FreezeTypeLogo,@EntrustNumber,@CapitalAccountLogo,@OweCosting,@FreezeCost)");
            strSql.Append(";select @@IDENTITY");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FreezeTime", DbType.DateTime, model.FreezeTime);
            db.AddInParameter(dbCommand, "FreezeCapitalAmount", DbType.Decimal, model.FreezeCapitalAmount);
            db.AddInParameter(dbCommand, "ThawTime", DbType.DateTime, model.ThawTime);
            db.AddInParameter(dbCommand, "FreezeTypeLogo", DbType.Int32, model.FreezeTypeLogo);
            db.AddInParameter(dbCommand, "EntrustNumber", DbType.AnsiString, model.EntrustNumber);
            db.AddInParameter(dbCommand, "CapitalAccountLogo", DbType.Int32, model.CapitalAccountLogo);
            db.AddInParameter(dbCommand, "OweCosting", DbType.Decimal, model.OweCosting);
            db.AddInParameter(dbCommand, "FreezeCost", DbType.Decimal, model.FreezeCost);
            int result;
            object obj = db.ExecuteScalar(dbCommand, transaction);
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public bool AddRecord(XH_CapitalAccountFreezeTableInfo model, Database db, DbTransaction transaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_CapitalAccountFreezeTable(");
            strSql.Append(
                "FreezeTime,FreezeCapitalAmount,ThawTime,FreezeTypeLogo,EntrustNumber,CapitalAccountLogo,OweCosting,FreezeCost)");

            strSql.Append(" values (");
            strSql.Append(
                "@FreezeTime,@FreezeCapitalAmount,@ThawTime,@FreezeTypeLogo,@EntrustNumber,@CapitalAccountLogo,@OweCosting,@FreezeCost)");
            strSql.Append(";select @@IDENTITY");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FreezeTime", DbType.DateTime, model.FreezeTime);
            db.AddInParameter(dbCommand, "FreezeCapitalAmount", DbType.Decimal, model.FreezeCapitalAmount);
            db.AddInParameter(dbCommand, "ThawTime", DbType.DateTime, model.ThawTime);
            db.AddInParameter(dbCommand, "FreezeTypeLogo", DbType.Int32, model.FreezeTypeLogo);
            db.AddInParameter(dbCommand, "EntrustNumber", DbType.AnsiString, model.EntrustNumber);
            db.AddInParameter(dbCommand, "CapitalAccountLogo", DbType.Int32, model.CapitalAccountLogo);
            db.AddInParameter(dbCommand, "OweCosting", DbType.Decimal, model.OweCosting);
            db.AddInParameter(dbCommand, "FreezeCost", DbType.Decimal, model.FreezeCost);
            int result;
            object obj = db.ExecuteScalar(dbCommand, transaction);
            if (!int.TryParse(obj.ToString(), out result))
            {
                return false;
            }

            model.CapitalFreezeLogoId = result;
            return true;
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(XH_CapitalAccountFreezeTableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_CapitalAccountFreezeTable set ");
            strSql.Append("FreezeTime=@FreezeTime,");
            strSql.Append("FreezeCapitalAmount=@FreezeCapitalAmount,");
            strSql.Append("ThawTime=@ThawTime,");
            strSql.Append("FreezeTypeLogo=@FreezeTypeLogo,");
            strSql.Append("EntrustNumber=@EntrustNumber,");
            strSql.Append("CapitalAccountLogo=@CapitalAccountLogo,");
            strSql.Append("OweCosting=@OweCosting,");
            strSql.Append("FreezeCost=@FreezeCost");
            strSql.Append(" where CapitalFreezeLogoId=@CapitalFreezeLogoId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FreezeTime", DbType.DateTime, model.FreezeTime);
            db.AddInParameter(dbCommand, "FreezeCapitalAmount", DbType.Decimal, model.FreezeCapitalAmount);
            db.AddInParameter(dbCommand, "CapitalFreezeLogoId", DbType.Int32, model.CapitalFreezeLogoId);
            db.AddInParameter(dbCommand, "ThawTime", DbType.DateTime, model.ThawTime);
            db.AddInParameter(dbCommand, "FreezeTypeLogo", DbType.Int32, model.FreezeTypeLogo);
            db.AddInParameter(dbCommand, "EntrustNumber", DbType.AnsiString, model.EntrustNumber);
            db.AddInParameter(dbCommand, "CapitalAccountLogo", DbType.Int32, model.CapitalAccountLogo);
            db.AddInParameter(dbCommand, "OweCosting", DbType.Decimal, model.OweCosting);
            db.AddInParameter(dbCommand, "FreezeCost", DbType.Decimal, model.FreezeCost);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// �������Ľ��ͷ���
        /// </summary>
        public void Clear(int CapitalFreezeLogoId, Database db, DbTransaction transaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_CapitalAccountFreezeTable set ");
            strSql.Append("FreezeCapitalAmount=@FreezeCapitalAmount,");
            strSql.Append("FreezeCost=@FreezeCost");
            strSql.Append(" where CapitalFreezeLogoId=@CapitalFreezeLogoId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FreezeCapitalAmount", DbType.Decimal, 0);
            db.AddInParameter(dbCommand, "FreezeCost", DbType.Decimal, 0);
            db.AddInParameter(dbCommand, "CapitalFreezeLogoId", DbType.Int32, CapitalFreezeLogoId);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(XH_CapitalAccountFreezeTableInfo model,ReckoningTransaction tm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_CapitalAccountFreezeTable set ");
            strSql.Append("FreezeTime=@FreezeTime,");
            strSql.Append("FreezeCapitalAmount=@FreezeCapitalAmount,");
            strSql.Append("ThawTime=@ThawTime,");
            strSql.Append("FreezeTypeLogo=@FreezeTypeLogo,");
            strSql.Append("EntrustNumber=@EntrustNumber,");
            strSql.Append("CapitalAccountLogo=@CapitalAccountLogo,");
            strSql.Append("OweCosting=@OweCosting,");
            strSql.Append("FreezeCost=@FreezeCost");
            strSql.Append(" where CapitalFreezeLogoId=@CapitalFreezeLogoId ");
            Database db = tm.Database;
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FreezeTime", DbType.DateTime, model.FreezeTime);
            db.AddInParameter(dbCommand, "FreezeCapitalAmount", DbType.Decimal, model.FreezeCapitalAmount);
            db.AddInParameter(dbCommand, "CapitalFreezeLogoId", DbType.Int32, model.CapitalFreezeLogoId);
            db.AddInParameter(dbCommand, "ThawTime", DbType.DateTime, model.ThawTime);
            db.AddInParameter(dbCommand, "FreezeTypeLogo", DbType.Int32, model.FreezeTypeLogo);
            db.AddInParameter(dbCommand, "EntrustNumber", DbType.AnsiString, model.EntrustNumber);
            db.AddInParameter(dbCommand, "CapitalAccountLogo", DbType.Int32, model.CapitalAccountLogo);
            db.AddInParameter(dbCommand, "OweCosting", DbType.Decimal, model.OweCosting);
            db.AddInParameter(dbCommand, "FreezeCost", DbType.Decimal, model.FreezeCost);
            db.ExecuteNonQuery(dbCommand,tm.Transaction);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool UpdateRecord(XH_CapitalAccountFreezeTableInfo model, ReckoningTransaction tm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_CapitalAccountFreezeTable set ");
            strSql.Append("FreezeTime=@FreezeTime,");
            strSql.Append("FreezeCapitalAmount=@FreezeCapitalAmount,");
            strSql.Append("ThawTime=@ThawTime,");
            strSql.Append("FreezeTypeLogo=@FreezeTypeLogo,");
            strSql.Append("EntrustNumber=@EntrustNumber,");
            strSql.Append("CapitalAccountLogo=@CapitalAccountLogo,");
            strSql.Append("OweCosting=@OweCosting,");
            strSql.Append("FreezeCost=@FreezeCost");
            strSql.Append(" where CapitalFreezeLogoId=@CapitalFreezeLogoId ");
            Database db = tm.Database;
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FreezeTime", DbType.DateTime, model.FreezeTime);
            db.AddInParameter(dbCommand, "FreezeCapitalAmount", DbType.Decimal, model.FreezeCapitalAmount);
            db.AddInParameter(dbCommand, "CapitalFreezeLogoId", DbType.Int32, model.CapitalFreezeLogoId);
            db.AddInParameter(dbCommand, "ThawTime", DbType.DateTime, model.ThawTime);
            db.AddInParameter(dbCommand, "FreezeTypeLogo", DbType.Int32, model.FreezeTypeLogo);
            db.AddInParameter(dbCommand, "EntrustNumber", DbType.AnsiString, model.EntrustNumber);
            db.AddInParameter(dbCommand, "CapitalAccountLogo", DbType.Int32, model.CapitalAccountLogo);
            db.AddInParameter(dbCommand, "OweCosting", DbType.Decimal, model.OweCosting);
            db.AddInParameter(dbCommand, "FreezeCost", DbType.Decimal, model.FreezeCost);
            try
            {
                db.ExecuteNonQuery(dbCommand, tm.Transaction);
                return true;
            }
            catch
            {

                return false;
            }
            
        }


        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int CapitalFreezeLogoId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XH_CapitalAccountFreezeTable ");
            strSql.Append(" where CapitalFreezeLogoId=@CapitalFreezeLogoId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalFreezeLogoId", DbType.Int32, CapitalFreezeLogoId);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="CapitalFreezeLogoId">�ʽ𶳽�Id</param>
        /// <param name="reckoningTransaction">�������</param>
        public  void Delete(int CapitalFreezeLogoId, ReckoningTransaction reckoningTransaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XH_CapitalAccountFreezeTable ");
            strSql.Append(" where CapitalFreezeLogoId=@CapitalFreezeLogoId ");
            Database db = reckoningTransaction.Database;
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalFreezeLogoId", DbType.Int32, CapitalFreezeLogoId);
            db.ExecuteNonQuery(dbCommand, reckoningTransaction.Transaction);
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="CapitalFreezeLogoId">�ʽ𶳽�Id</param>
        /// <param name="reckoningTransaction">�������</param>
        public bool DeleteRecord(int CapitalFreezeLogoId, ReckoningTransaction reckoningTransaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XH_CapitalAccountFreezeTable ");
            strSql.Append(" where CapitalFreezeLogoId=@CapitalFreezeLogoId ");
            Database db = reckoningTransaction.Database;
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalFreezeLogoId", DbType.Int32, CapitalFreezeLogoId);
            try
            {
                db.ExecuteNonQuery(dbCommand, reckoningTransaction.Transaction);
                return true;
            }
            catch
            {

                return false;
            }
            
        }


        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public XH_CapitalAccountFreezeTableInfo GetModel(int CapitalFreezeLogoId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select FreezeTime,FreezeCapitalAmount,CapitalFreezeLogoId,ThawTime,FreezeTypeLogo,EntrustNumber,CapitalAccountLogo,OweCosting,FreezeCost from XH_CapitalAccountFreezeTable ");
            strSql.Append(" where CapitalFreezeLogoId=@CapitalFreezeLogoId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalFreezeLogoId", DbType.Int32, CapitalFreezeLogoId);
            XH_CapitalAccountFreezeTableInfo model = null;
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
        public List<XH_CapitalAccountFreezeTableInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select FreezeTime,FreezeCapitalAmount,CapitalFreezeLogoId,ThawTime,FreezeTypeLogo,EntrustNumber,CapitalAccountLogo,OweCosting,FreezeCost ");
            strSql.Append(" FROM XH_CapitalAccountFreezeTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<XH_CapitalAccountFreezeTableInfo> list = new List<XH_CapitalAccountFreezeTableInfo>();
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
        /// ��������б�
        /// </summary>
        public List<XH_CapitalAccountFreezeTableInfo> GetListArray(string strWhere,ReckoningTransaction tm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select FreezeTime,FreezeCapitalAmount,CapitalFreezeLogoId,ThawTime,FreezeTypeLogo,EntrustNumber,CapitalAccountLogo,OweCosting,FreezeCost ");
            strSql.Append(" FROM XH_CapitalAccountFreezeTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<XH_CapitalAccountFreezeTableInfo> list = new List<XH_CapitalAccountFreezeTableInfo>();
            Database db = tm.Database;
            using (IDataReader dataReader = db.ExecuteReader(tm.Transaction,CommandType.Text, strSql.ToString()))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderBind(dataReader));
                }
            }
            return list;
        }

        /// <summary>
        /// ��ȡ�����ʽ����
        /// </summary>
        /// <returns></returns>
        public List<XH_CapitalAccountFreezeSum> GetAllFreezeMoney()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CapitalAccountLogo,sum(freezecapitalamount+freezecost) FreezeCapitalSum ");
            strSql.Append("from xh_capitalaccountfreezetable ");
            //strSql.Append("where freezetypelogo=1 ");
            strSql.Append("group by CapitalAccountLogo");

            List<XH_CapitalAccountFreezeSum> list = new List<XH_CapitalAccountFreezeSum>();
            Database db = DatabaseFactory.CreateDatabase();
            
            using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, strSql.ToString()))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderBind2(dataReader));
                }
            }
            return list;
        }

        private XH_CapitalAccountFreezeSum ReaderBind2(IDataReader dataReader)
        {
            XH_CapitalAccountFreezeSum model = new XH_CapitalAccountFreezeSum();
            object ojb;
            ojb = dataReader["CapitalAccountLogo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CapitalAccountLogo = (int)ojb;
            }
            ojb = dataReader["FreezeCapitalSum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FreezeCapitalSum = (decimal)ojb;
            }
            return model;
        }

        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public XH_CapitalAccountFreezeTableInfo ReaderBind(IDataReader dataReader)
        {
            XH_CapitalAccountFreezeTableInfo model = new XH_CapitalAccountFreezeTableInfo();
            object ojb;
            ojb = dataReader["FreezeTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FreezeTime = (DateTime) ojb;
            }
            ojb = dataReader["FreezeCapitalAmount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FreezeCapitalAmount = (decimal) ojb;
            }
            ojb = dataReader["CapitalFreezeLogoId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CapitalFreezeLogoId = (int) ojb;
            }
            ojb = dataReader["ThawTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ThawTime = (DateTime) ojb;
            }
            ojb = dataReader["FreezeTypeLogo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FreezeTypeLogo = (int) ojb;
            }
            model.EntrustNumber = dataReader["EntrustNumber"].ToString();
            ojb = dataReader["CapitalAccountLogo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CapitalAccountLogo = (int) ojb;
            }
            ojb = dataReader["OweCosting"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OweCosting = (decimal) ojb;
            }
            ojb = dataReader["FreezeCost"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FreezeCost = (decimal) ojb;
            }
            return model;
        }


        ///// <summary>
        ///// ����������ҳ��ѯ
        ///// </summary>
        ///// <param name="pageProcInfo">��ҳ�洢���̹�������</param>
        ///// <param name="total">��ҳ��</param>
        ///// <returns></returns>
        //public List<XH_CapitalAccountFreezeTableInfo> PagingXH_CapitalAccountFreezeByFilter(PagingProceduresInfo pageProcInfo, out int total)
        //{
        //    List<XH_CapitalAccountFreezeTableInfo> list = new List<XH_CapitalAccountFreezeTableInfo>();
        //    Database db = DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = CommonDALOperate.PagingProceduresDbCommand(db, pageProcInfo);
        //    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
        //    {
        //        while (dataReader.Read())
        //        {
        //            list.Add(ReaderBind(dataReader));
        //        }

        //    }
        //    total = db.GetParameterValue(dbCommand, "@Total") != null ? (int)db.GetParameterValue(dbCommand, "@Total") : 0;
        //    return list;
        //}
        #endregion  ��Ա����
    }
}