#region Using Namespace

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReckoningCounter.Model;
using ReckoningCounter.Entity.Model.QueryFilter;

#endregion

namespace ReckoningCounter.DAL.Data
{
    /// <summary>
    /// ���ݷ�����XH_TodayTradeTable��
    /// </summary>
    public class XH_TodayTradeTableDal
    {
        #region  ��Ա����


        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string TradeNumber)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XH_TodayTradeTable where TradeNumber=@TradeNumber ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeNumber", DbType.AnsiString, TradeNumber);
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
        public void Add(XH_TodayTradeTableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_TodayTradeTable(");
            strSql.Append("TradeNumber,EntrustNumber,PortfolioLogo,TradePrice,TradeAmount,EntrustPrice,StampTax,Commission,TransferAccountFee,TradeProceduresFee,MonitoringFee,TradingSystemUseFee,TradeCapitalAmount,ClearingFee,StockAccount,CapitalAccount,SpotCode,TradeTypeId,TradeUnitId,BuySellTypeId,CurrencyTypeId,TradeTime)");

            strSql.Append(" values (");
            strSql.Append("@TradeNumber,@EntrustNumber,@PortfolioLogo,@TradePrice,@TradeAmount,@EntrustPrice,@StampTax,@Commission,@TransferAccountFee,@TradeProceduresFee,@MonitoringFee,@TradingSystemUseFee,@TradeCapitalAmount,@ClearingFee,@StockAccount,@CapitalAccount,@SpotCode,@TradeTypeId,@TradeUnitId,@BuySellTypeId,@CurrencyTypeId,@TradeTime)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeNumber", DbType.AnsiString, model.TradeNumber);
            db.AddInParameter(dbCommand, "EntrustNumber", DbType.AnsiString, model.EntrustNumber);
            db.AddInParameter(dbCommand, "PortfolioLogo", DbType.AnsiString, model.PortfolioLogo);
            db.AddInParameter(dbCommand, "TradePrice", DbType.Decimal, model.TradePrice);
            db.AddInParameter(dbCommand, "TradeAmount", DbType.Int32, model.TradeAmount);
            db.AddInParameter(dbCommand, "EntrustPrice", DbType.Decimal, model.EntrustPrice);
            db.AddInParameter(dbCommand, "StampTax", DbType.Decimal, model.StampTax);
            db.AddInParameter(dbCommand, "Commission", DbType.Decimal, model.Commission);
            db.AddInParameter(dbCommand, "TransferAccountFee", DbType.Decimal, model.TransferAccountFee);
            db.AddInParameter(dbCommand, "TradeProceduresFee", DbType.Decimal, model.TradeProceduresFee);
            db.AddInParameter(dbCommand, "MonitoringFee", DbType.Decimal, model.MonitoringFee);
            db.AddInParameter(dbCommand, "TradingSystemUseFee", DbType.Decimal, model.TradingSystemUseFee);
            db.AddInParameter(dbCommand, "TradeCapitalAmount", DbType.Decimal, model.TradeCapitalAmount);
            db.AddInParameter(dbCommand, "ClearingFee", DbType.Decimal, model.ClearingFee);
            db.AddInParameter(dbCommand, "StockAccount", DbType.AnsiString, model.StockAccount);
            db.AddInParameter(dbCommand, "CapitalAccount", DbType.AnsiString, model.CapitalAccount);
            db.AddInParameter(dbCommand, "SpotCode", DbType.AnsiString, model.SpotCode);
            db.AddInParameter(dbCommand, "TradeTypeId", DbType.Int32, model.TradeTypeId);
            db.AddInParameter(dbCommand, "TradeUnitId", DbType.Int32, model.TradeUnitId);
            db.AddInParameter(dbCommand, "BuySellTypeId", DbType.Int32, model.BuySellTypeId);
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, model.CurrencyTypeId);
            db.AddInParameter(dbCommand, "TradeTime", DbType.DateTime, model.TradeTime);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(XH_TodayTradeTableInfo model,ReckoningTransaction tm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_TodayTradeTable(");
            strSql.Append("TradeNumber,EntrustNumber,PortfolioLogo,TradePrice,TradeAmount,EntrustPrice,StampTax,Commission,TransferAccountFee,TradeProceduresFee,MonitoringFee,TradingSystemUseFee,TradeCapitalAmount,ClearingFee,StockAccount,CapitalAccount,SpotCode,TradeTypeId,TradeUnitId,BuySellTypeId,CurrencyTypeId,TradeTime)");

            strSql.Append(" values (");
            strSql.Append("@TradeNumber,@EntrustNumber,@PortfolioLogo,@TradePrice,@TradeAmount,@EntrustPrice,@StampTax,@Commission,@TransferAccountFee,@TradeProceduresFee,@MonitoringFee,@TradingSystemUseFee,@TradeCapitalAmount,@ClearingFee,@StockAccount,@CapitalAccount,@SpotCode,@TradeTypeId,@TradeUnitId,@BuySellTypeId,@CurrencyTypeId,@TradeTime)");
            Database db = tm.Database;
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeNumber", DbType.AnsiString, model.TradeNumber);
            db.AddInParameter(dbCommand, "EntrustNumber", DbType.AnsiString, model.EntrustNumber);
            db.AddInParameter(dbCommand, "PortfolioLogo", DbType.AnsiString, model.PortfolioLogo);
            db.AddInParameter(dbCommand, "TradePrice", DbType.Decimal, model.TradePrice);
            db.AddInParameter(dbCommand, "TradeAmount", DbType.Int32, model.TradeAmount);
            db.AddInParameter(dbCommand, "EntrustPrice", DbType.Decimal, model.EntrustPrice);
            db.AddInParameter(dbCommand, "StampTax", DbType.Decimal, model.StampTax);
            db.AddInParameter(dbCommand, "Commission", DbType.Decimal, model.Commission);
            db.AddInParameter(dbCommand, "TransferAccountFee", DbType.Decimal, model.TransferAccountFee);
            db.AddInParameter(dbCommand, "TradeProceduresFee", DbType.Decimal, model.TradeProceduresFee);
            db.AddInParameter(dbCommand, "MonitoringFee", DbType.Decimal, model.MonitoringFee);
            db.AddInParameter(dbCommand, "TradingSystemUseFee", DbType.Decimal, model.TradingSystemUseFee);
            db.AddInParameter(dbCommand, "TradeCapitalAmount", DbType.Decimal, model.TradeCapitalAmount);
            db.AddInParameter(dbCommand, "ClearingFee", DbType.Decimal, model.ClearingFee);
            db.AddInParameter(dbCommand, "StockAccount", DbType.AnsiString, model.StockAccount);
            db.AddInParameter(dbCommand, "CapitalAccount", DbType.AnsiString, model.CapitalAccount);
            db.AddInParameter(dbCommand, "SpotCode", DbType.AnsiString, model.SpotCode);
            db.AddInParameter(dbCommand, "TradeTypeId", DbType.Int32, model.TradeTypeId);
            db.AddInParameter(dbCommand, "TradeUnitId", DbType.Int32, model.TradeUnitId);
            db.AddInParameter(dbCommand, "BuySellTypeId", DbType.Int32, model.BuySellTypeId);
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, model.CurrencyTypeId);
            db.AddInParameter(dbCommand, "TradeTime", DbType.DateTime, model.TradeTime);
            db.ExecuteNonQuery(dbCommand,tm.Transaction);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(XH_TodayTradeTableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_TodayTradeTable set ");
            strSql.Append("EntrustNumber=@EntrustNumber,");
            strSql.Append("PortfolioLogo=@PortfolioLogo,");
            strSql.Append("TradePrice=@TradePrice,");
            strSql.Append("TradeAmount=@TradeAmount,");
            strSql.Append("EntrustPrice=@EntrustPrice,");
            strSql.Append("StampTax=@StampTax,");
            strSql.Append("Commission=@Commission,");
            strSql.Append("TransferAccountFee=@TransferAccountFee,");
            strSql.Append("TradeProceduresFee=@TradeProceduresFee,");
            strSql.Append("MonitoringFee=@MonitoringFee,");
            strSql.Append("TradingSystemUseFee=@TradingSystemUseFee,");
            strSql.Append("TradeCapitalAmount=@TradeCapitalAmount,");
            strSql.Append("ClearingFee=@ClearingFee,");
            strSql.Append("StockAccount=@StockAccount,");
            strSql.Append("CapitalAccount=@CapitalAccount,");
            strSql.Append("SpotCode=@SpotCode,");
            strSql.Append("TradeTypeId=@TradeTypeId,");
            strSql.Append("TradeUnitId=@TradeUnitId,");
            strSql.Append("BuySellTypeId=@BuySellTypeId,");
            strSql.Append("CurrencyTypeId=@CurrencyTypeId,");
            strSql.Append("TradeTime=@TradeTime");
            strSql.Append(" where TradeNumber=@TradeNumber ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeNumber", DbType.AnsiString, model.TradeNumber);
            db.AddInParameter(dbCommand, "EntrustNumber", DbType.AnsiString, model.EntrustNumber);
            db.AddInParameter(dbCommand, "PortfolioLogo", DbType.AnsiString, model.PortfolioLogo);
            db.AddInParameter(dbCommand, "TradePrice", DbType.Decimal, model.TradePrice);
            db.AddInParameter(dbCommand, "TradeAmount", DbType.Int32, model.TradeAmount);
            db.AddInParameter(dbCommand, "EntrustPrice", DbType.Decimal, model.EntrustPrice);
            db.AddInParameter(dbCommand, "StampTax", DbType.Decimal, model.StampTax);
            db.AddInParameter(dbCommand, "Commission", DbType.Decimal, model.Commission);
            db.AddInParameter(dbCommand, "TransferAccountFee", DbType.Decimal, model.TransferAccountFee);
            db.AddInParameter(dbCommand, "TradeProceduresFee", DbType.Decimal, model.TradeProceduresFee);
            db.AddInParameter(dbCommand, "MonitoringFee", DbType.Decimal, model.MonitoringFee);
            db.AddInParameter(dbCommand, "TradingSystemUseFee", DbType.Decimal, model.TradingSystemUseFee);
            db.AddInParameter(dbCommand, "TradeCapitalAmount", DbType.Decimal, model.TradeCapitalAmount);
            db.AddInParameter(dbCommand, "ClearingFee", DbType.Decimal, model.ClearingFee);
            db.AddInParameter(dbCommand, "StockAccount", DbType.AnsiString, model.StockAccount);
            db.AddInParameter(dbCommand, "CapitalAccount", DbType.AnsiString, model.CapitalAccount);
            db.AddInParameter(dbCommand, "SpotCode", DbType.AnsiString, model.SpotCode);
            db.AddInParameter(dbCommand, "TradeTypeId", DbType.Int32, model.TradeTypeId);
            db.AddInParameter(dbCommand, "TradeUnitId", DbType.Int32, model.TradeUnitId);
            db.AddInParameter(dbCommand, "BuySellTypeId", DbType.Int32, model.BuySellTypeId);
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, model.CurrencyTypeId);
            db.AddInParameter(dbCommand, "TradeTime", DbType.DateTime, model.TradeTime);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(string TradeNumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XH_TodayTradeTable ");
            strSql.Append(" where TradeNumber=@TradeNumber ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeNumber", DbType.AnsiString, TradeNumber);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public XH_TodayTradeTableInfo GetModel(string TradeNumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TradeNumber,EntrustNumber,PortfolioLogo,TradePrice,TradeAmount,EntrustPrice,StampTax,Commission,TransferAccountFee,TradeProceduresFee,MonitoringFee,TradingSystemUseFee,TradeCapitalAmount,ClearingFee,StockAccount,CapitalAccount,SpotCode,TradeTypeId,TradeUnitId,BuySellTypeId,CurrencyTypeId,TradeTime from XH_TodayTradeTable ");
            strSql.Append(" where TradeNumber=@TradeNumber ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeNumber", DbType.AnsiString, TradeNumber);
            XH_TodayTradeTableInfo model = null;
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
        public List<XH_TodayTradeTableInfo> GetPushList(string channelID)
        {
            string where = string.Format("SELECT X.* FROM XH_TodayTradeTable X ,XH_PushBackOrderTable Y WHERE  X.TradeNumber = Y.TradeNumber and y.ChannelID = '{0}'", channelID);
            List<XH_TodayTradeTableInfo> list = new List<XH_TodayTradeTableInfo>();
            Database db = DatabaseFactory.CreateDatabase();
            using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, where.ToString()))
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
        public List<XH_TodayTradeTableInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TradeNumber,EntrustNumber,PortfolioLogo,TradePrice,TradeAmount,EntrustPrice,StampTax,Commission,TransferAccountFee,TradeProceduresFee,MonitoringFee,TradingSystemUseFee,TradeCapitalAmount,ClearingFee,StockAccount,CapitalAccount,SpotCode,TradeTypeId,TradeUnitId,BuySellTypeId,CurrencyTypeId,TradeTime ");
            strSql.Append(" FROM XH_TodayTradeTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<XH_TodayTradeTableInfo> list = new List<XH_TodayTradeTableInfo>();
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
        public XH_TodayTradeTableInfo ReaderBind(IDataReader dataReader)
        {
            XH_TodayTradeTableInfo model = new XH_TodayTradeTableInfo();
            object ojb;
            model.TradeNumber = dataReader["TradeNumber"].ToString();
            model.EntrustNumber = dataReader["EntrustNumber"].ToString();
            model.PortfolioLogo = dataReader["PortfolioLogo"].ToString();
            ojb = dataReader["TradePrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradePrice = (decimal)ojb;
            }
            ojb = dataReader["TradeAmount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradeAmount = (int)ojb;
            }
            ojb = dataReader["EntrustPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.EntrustPrice = (decimal)ojb;
            }
            ojb = dataReader["StampTax"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.StampTax = (decimal)ojb;
            }
            ojb = dataReader["Commission"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Commission = (decimal)ojb;
            }
            ojb = dataReader["TransferAccountFee"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TransferAccountFee = (decimal)ojb;
            }
            ojb = dataReader["TradeProceduresFee"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradeProceduresFee = (decimal)ojb;
            }
            ojb = dataReader["MonitoringFee"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MonitoringFee = (decimal)ojb;
            }
            ojb = dataReader["TradingSystemUseFee"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradingSystemUseFee = (decimal)ojb;
            }
            ojb = dataReader["TradeCapitalAmount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradeCapitalAmount = (decimal)ojb;
            }
            ojb = dataReader["ClearingFee"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ClearingFee = (decimal)ojb;
            }
            model.StockAccount = dataReader["StockAccount"].ToString();
            model.CapitalAccount = dataReader["CapitalAccount"].ToString();
            model.SpotCode = dataReader["SpotCode"].ToString();
            ojb = dataReader["TradeTypeId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradeTypeId = (int)ojb;
            }
            ojb = dataReader["TradeUnitId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradeUnitId = (int)ojb;
            }
            ojb = dataReader["BuySellTypeId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BuySellTypeId = (int)ojb;
            }
            ojb = dataReader["CurrencyTypeId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CurrencyTypeId = (int)ojb;
            }
            ojb = dataReader["TradeTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradeTime = (DateTime)ojb;
            }
            return model;
        }

        ///// <summary>
        ///// ����������ҳ��ѯ
        ///// </summary>
        ///// <param name="pageProcInfo">��ҳ�洢���̹�������</param>
        ///// <param name="total">��ҳ��</param>
        ///// <returns></returns>
        //public List<XH_TodayTradeTableInfo> PagingXH_TodayTradeByFilter(PagingProceduresInfo pageProcInfo, out int total)
        //{
        //    List<XH_TodayTradeTableInfo> list = new List<XH_TodayTradeTableInfo>();
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