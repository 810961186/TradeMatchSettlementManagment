﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using ReckoningCounter.Entity;

namespace ReckoningCounter.DAL.Data.HK
{
    /// <summary>
    /// 港股改单请求数据访问类HK_ModifyOrderRequestDal。
    /// Create By:李健华
    /// Create Date:2009-10-28
    /// </summary>
    public class HK_ModifyOrderRequestDal
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HK_ModifyOrderRequestDal()
        { }
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from HK_ModifyOrderRequest where ID=@ID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.AnsiString, ID);
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
        /// 增加一条数据
        /// 注：本方法没有插入创建时间，创建时间由数据创建
        /// </summary>
        public void Add(HKModifyOrderRequest model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into HK_ModifyOrderRequest(");
            strSql.Append("ID,ChannelID,TraderId,FundAccountId,TraderPassword,Code,EntrustNubmer,OrderPrice,OrderAmount,Message)");

            strSql.Append(" values (");
            strSql.Append("@ID,@ChannelID,@TraderId,@FundAccountId,@TraderPassword,@Code,@EntrustNubmer,@OrderPrice,@OrderAmount,@Message)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.AnsiString, model.ID);
            db.AddInParameter(dbCommand, "ChannelID", DbType.AnsiString, model.ChannelID);
            db.AddInParameter(dbCommand, "TraderId", DbType.AnsiString, model.TraderId);
            db.AddInParameter(dbCommand, "FundAccountId", DbType.AnsiString, model.FundAccountId);
            db.AddInParameter(dbCommand, "TraderPassword", DbType.AnsiString, model.TraderPassword);
            db.AddInParameter(dbCommand, "Code", DbType.AnsiString, model.Code);
            db.AddInParameter(dbCommand, "EntrustNubmer", DbType.AnsiString, model.EntrustNubmer);
            db.AddInParameter(dbCommand, "OrderPrice", DbType.Decimal, model.OrderPrice);
            db.AddInParameter(dbCommand, "OrderAmount", DbType.Int32, model.OrderAmount);
            db.AddInParameter(dbCommand, "Message", DbType.AnsiString, model.Message);
           // db.AddInParameter(dbCommand, "ModifyOrderDateTime", DbType.DateTime, model.ModifyOrderDateTime);
            db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(HKModifyOrderRequest model)
        {
            if (model.ModifyOrderDateTime == DateTime.MaxValue || model.ModifyOrderDateTime == DateTime.MinValue)
            {
                model.ModifyOrderDateTime = DateTime.Now;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HK_ModifyOrderRequest set ");
            strSql.Append("ChannelID=@ChannelID,");
            strSql.Append("TraderId=@TraderId,");
            strSql.Append("FundAccountId=@FundAccountId,");
            strSql.Append("TraderPassword=@TraderPassword,");
            strSql.Append("Code=@Code,");
            strSql.Append("EntrustNubmer=@EntrustNubmer,");
            strSql.Append("OrderPrice=@OrderPrice,");
            strSql.Append("OrderAmount=@OrderAmount,");
            strSql.Append("Message=@Message,");
            strSql.Append("ModifyOrderDateTime=@ModifyOrderDateTime");
            strSql.Append(" where ID=@ID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.AnsiString, model.ID);
            db.AddInParameter(dbCommand, "ChannelID", DbType.AnsiString, model.ChannelID);
            db.AddInParameter(dbCommand, "TraderId", DbType.AnsiString, model.TraderId);
            db.AddInParameter(dbCommand, "FundAccountId", DbType.AnsiString, model.FundAccountId);
            db.AddInParameter(dbCommand, "TraderPassword", DbType.AnsiString, model.TraderPassword);
            db.AddInParameter(dbCommand, "Code", DbType.AnsiString, model.Code);
            db.AddInParameter(dbCommand, "EntrustNubmer", DbType.AnsiString, model.EntrustNubmer);
            db.AddInParameter(dbCommand, "OrderPrice", DbType.Decimal, model.OrderPrice);
            db.AddInParameter(dbCommand, "OrderAmount", DbType.Int32, model.OrderAmount);
            db.AddInParameter(dbCommand, "Message", DbType.AnsiString, model.Message);
            db.AddInParameter(dbCommand, "ModifyOrderDateTime", DbType.DateTime, model.ModifyOrderDateTime);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from HK_ModifyOrderRequest ");
            strSql.Append(" where ID=@ID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.AnsiString, ID);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HKModifyOrderRequest GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ChannelID,TraderId,FundAccountId,TraderPassword,Code,EntrustNubmer,OrderPrice,OrderAmount,Message,ModifyOrderDateTime from HK_ModifyOrderRequest ");
            strSql.Append(" where ID=@ID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.AnsiString, ID);
            HKModifyOrderRequest model = null;
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ChannelID,TraderId,FundAccountId,TraderPassword,Code,EntrustNubmer,OrderPrice,OrderAmount,Message,ModifyOrderDateTime ");
            strSql.Append(" FROM HK_ModifyOrderRequest ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

   

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<HKModifyOrderRequest> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ChannelID,TraderId,FundAccountId,TraderPassword,Code,EntrustNubmer,OrderPrice,OrderAmount,Message,ModifyOrderDateTime ");
            strSql.Append(" FROM HK_ModifyOrderRequest ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<HKModifyOrderRequest> list = new List<HKModifyOrderRequest>();
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
        /// 对象实体绑定数据
        /// </summary>
        public HKModifyOrderRequest ReaderBind(IDataReader dataReader)
        {
            HKModifyOrderRequest model = new HKModifyOrderRequest();
            object ojb;
            model.ID = dataReader["ID"].ToString();
            model.ChannelID = dataReader["ChannelID"].ToString();
            model.TraderId = dataReader["TraderId"].ToString();
            model.FundAccountId = dataReader["FundAccountId"].ToString();
            model.TraderPassword = dataReader["TraderPassword"].ToString();
            model.Code = dataReader["Code"].ToString();
            model.EntrustNubmer = dataReader["EntrustNubmer"].ToString();
            ojb = dataReader["OrderPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OrderPrice = float.Parse(ojb.ToString());
            }
            ojb = dataReader["OrderAmount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OrderAmount = float.Parse(ojb.ToString());
            }
            model.Message = dataReader["Message"].ToString();
            ojb = dataReader["ModifyOrderDateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ModifyOrderDateTime = (DateTime)ojb;
            }
            return model;
        }

        #endregion  成员方法
       
    }
}
