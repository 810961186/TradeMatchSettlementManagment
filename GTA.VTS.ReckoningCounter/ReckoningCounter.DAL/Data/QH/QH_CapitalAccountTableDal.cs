#region Using Namespace

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GTA.VTS.Common.CommonUtility;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReckoningCounter.Model;
using ReckoningCounter.Entity.Model.QueryFilter;

#endregion

namespace ReckoningCounter.DAL.Data
{
    /// <summary>
    /// ���ݷ�����QH_CapitalAccountTableDal��
    /// Create BY�����
    /// Create Date��2009-10-15
    /// Update by:����
    /// Update date:2009-12-25
    /// Desc.: ��Ӹ��Ի��ʽ����õķ���
    /// </summary>
    public class QH_CapitalAccountTableDal
    {
        #region  ��Ա����

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int CapitalAccountLogoId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select count(1) from QH_CapitalAccountTable where CapitalAccountLogoId=@CapitalAccountLogoId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalAccountLogoId", DbType.Int32, CapitalAccountLogoId);
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
        /// ��ȡ�����ʽ�
        /// </summary>
        /// <param name="strCapitalAccount"></param>
        /// <param name="iCurrType"></param>
        /// <returns></returns>
        public decimal GetAvailableCapitalWithNoLock(string strCapitalAccount, int iCurrType)
        {
            decimal result = -1;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select AvailableCapital from QH_CapitalAccountTable WITH (NOLOCK)");
            strSql.Append(
                " where TradeCurrencyType=@TradeCurrencyType AND UserAccountDistributeLogo=@UserAccountDistributeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeCurrencyType", DbType.Int32, iCurrType);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.String, strCapitalAccount);

            try
            {
                object obj = db.ExecuteScalar(dbCommand);
                if (obj != null)
                {
                    decimal.TryParse(obj.ToString(), out result);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.Message, ex);
            }

            return result;
        }


        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public QH_CapitalAccountTableInfo GetQHCapitalAccount(string strCapitalAccount, int iCurrType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select CapitalAccountLogoId,AvailableCapital,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapitalTotal,CapitalBalance,MarginTotal,TradeCurrencyType from QH_CapitalAccountTable ");
            strSql.Append(
                " where TradeCurrencyType=@TradeCurrencyType AND UserAccountDistributeLogo=@UserAccountDistributeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeCurrencyType", DbType.Int32, iCurrType);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.String, strCapitalAccount);
            QH_CapitalAccountTableInfo model = null;
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
        /// ���Ƿ�����������һ������
        /// <param name="model">Ҫ������û��˺Ŷ���</param>
        /// <param name="db">�������ݶ���</param>
        /// <param name="trm">��������������Ϊnull������</param>
        /// </summary>
        public int Add(QH_CapitalAccountTableInfo model, Database db, DbTransaction trm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QH_CapitalAccountTable(");
            strSql.Append(
                "AvailableCapital,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapitalTotal,MarginTotal,TradeCurrencyType,CloseFloatProfitLossTotal,CloseMarketProfitLossTotal)");

            strSql.Append(" values (");
            strSql.Append(
                "@AvailableCapital,@UserAccountDistributeLogo,@BalanceOfTheDay,@TodayOutInCapital,@FreezeCapitalTotal,@MarginTotal,@TradeCurrencyType,@CloseFloatProfitLossTotal,@CloseMarketProfitLossTotal)");
            strSql.Append(";select @@IDENTITY");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AvailableCapital", DbType.Decimal, model.AvailableCapital);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "BalanceOfTheDay", DbType.Decimal, model.BalanceOfTheDay);
            db.AddInParameter(dbCommand, "TodayOutInCapital", DbType.Decimal, model.TodayOutInCapital);
            db.AddInParameter(dbCommand, "FreezeCapitalTotal", DbType.Decimal, model.FreezeCapitalTotal);
            // db.AddInParameter(dbCommand, "CapitalBalance", DbType.Decimal, model.CapitalBalance);//���ݿ�ͳ��Լ���ֶ�
            db.AddInParameter(dbCommand, "MarginTotal", DbType.Decimal, model.MarginTotal);
            db.AddInParameter(dbCommand, "TradeCurrencyType", DbType.Int32, model.TradeCurrencyType);
            db.AddInParameter(dbCommand, "CloseFloatProfitLossTotal", DbType.Decimal, model.CloseFloatProfitLossTotal);
            db.AddInParameter(dbCommand, "CloseMarketProfitLossTotal", DbType.Decimal, model.CloseMarketProfitLossTotal);
            int result;
            object obj = null;
            if (trm == null)
            {
                obj = db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                obj = db.ExecuteNonQuery(dbCommand, trm);
            }
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;

        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(QH_CapitalAccountTableInfo model)
        {
            Database db = DatabaseFactory.CreateDatabase();
            return Add(model, db, null);
        }

        /// <summary>
        /// ת��ʱ��������ID���ӻ��߼����ʽ�
        /// Create by:���
        /// Crate date:2009-07-19
        /// Desc.:���ӻ��߼�����isdd���жϣ����Ӽ�����ԭ��������Ҫ���ӵ�ֵ��������Ϊ10����Ϊ100���Ϊ110
        /// </summary>
        /// <param name="amount">Ҫ���ٵ��ʽ��ܶ�</param>
        /// <param name="isAdd">����/����</param>
        /// <param name="id">����</param>
        /// <param name="db">���ݿ����</param>
        /// <param name="trm">��������</param>
        public void AddOrSubCapital(decimal amount, bool isAdd, int id, Database db, DbTransaction trm)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QH_CapitalAccountTable set ");
            strSql.Append("AvailableCapital=AvailableCapital {0} @Amount,");
            strSql.Append("TodayOutInCapital=TodayOutInCapital {0} @Amount,");
            strSql.Append("BalanceOfTheDay=BalanceOfTheDay {0} @Amount,");
            strSql.Append(" where CapitalAccountLogoId=@CapitalAccountLogoId ");

            DbCommand dbCommand = db.GetSqlStringCommand(string.Format(strSql.ToString(), isAdd ? "+" : "-"));
            db.AddInParameter(dbCommand, "CapitalAccountLogoId", DbType.Int32, id);
            db.AddInParameter(dbCommand, "Amount", DbType.Decimal, amount);
            db.ExecuteNonQuery(dbCommand, trm);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(QH_CapitalAccountTableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QH_CapitalAccountTable set ");
            strSql.Append("AvailableCapital=@AvailableCapital,");
            strSql.Append("UserAccountDistributeLogo=@UserAccountDistributeLogo,");
            strSql.Append("BalanceOfTheDay=@BalanceOfTheDay,");
            strSql.Append("TodayOutInCapital=@TodayOutInCapital,");
            strSql.Append("FreezeCapitalTotal=@FreezeCapitalTotal,");
            // strSql.Append("CapitalBalance=@CapitalBalance,");
            strSql.Append("MarginTotal=@MarginTotal,");
            strSql.Append("TradeCurrencyType=@TradeCurrencyType,");
            strSql.Append("CloseFloatProfitLossTotal=@CloseFloatProfitLossTotal,");
            strSql.Append("CloseMarketProfitLossTotal=@CloseMarketProfitLossTotal");
            strSql.Append(" where CapitalAccountLogoId=@CapitalAccountLogoId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalAccountLogoId", DbType.Int32, model.CapitalAccountLogoId);
            db.AddInParameter(dbCommand, "AvailableCapital", DbType.Decimal, model.AvailableCapital);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "BalanceOfTheDay", DbType.Decimal, model.BalanceOfTheDay);
            db.AddInParameter(dbCommand, "TodayOutInCapital", DbType.Decimal, model.TodayOutInCapital);
            db.AddInParameter(dbCommand, "FreezeCapitalTotal", DbType.Decimal, model.FreezeCapitalTotal);
            // db.AddInParameter(dbCommand, "CapitalBalance", DbType.Decimal, model.CapitalBalance);
            db.AddInParameter(dbCommand, "MarginTotal", DbType.Decimal, model.MarginTotal);
            db.AddInParameter(dbCommand, "TradeCurrencyType", DbType.Int32, model.TradeCurrencyType);
            db.AddInParameter(dbCommand, "CloseFloatProfitLossTotal", DbType.Decimal, model.CloseFloatProfitLossTotal);
            db.AddInParameter(dbCommand, "CloseMarketProfitLossTotal", DbType.Decimal, model.CloseMarketProfitLossTotal);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// �����±仯�ֶ�
        /// </summary>
        public void Update(QH_CapitalAccountTableInfo model, Database db, DbTransaction transaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QH_CapitalAccountTable set ");
            strSql.Append("AvailableCapital=@AvailableCapital,");
            //strSql.Append("UserAccountDistributeLogo=@UserAccountDistributeLogo,");
            //strSql.Append("BalanceOfTheDay=@BalanceOfTheDay,");
            strSql.Append("TodayOutInCapital=@TodayOutInCapital,");
            strSql.Append("FreezeCapitalTotal=@FreezeCapitalTotal,");
            //strSql.Append("CapitalBalance=@CapitalBalance,");
            strSql.Append("MarginTotal=@MarginTotal,");
            //strSql.Append("TradeCurrencyType=@TradeCurrencyType,");
            strSql.Append("CloseFloatProfitLossTotal=@CloseFloatProfitLossTotal,");
            strSql.Append("CloseMarketProfitLossTotal=@CloseMarketProfitLossTotal");
            strSql.Append(" where CapitalAccountLogoId=@CapitalAccountLogoId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalAccountLogoId", DbType.Int32, model.CapitalAccountLogoId);
            db.AddInParameter(dbCommand, "AvailableCapital", DbType.Decimal, model.AvailableCapital);
            //db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            //db.AddInParameter(dbCommand, "BalanceOfTheDay", DbType.Decimal, model.BalanceOfTheDay);
            db.AddInParameter(dbCommand, "TodayOutInCapital", DbType.Decimal, model.TodayOutInCapital);
            db.AddInParameter(dbCommand, "FreezeCapitalTotal", DbType.Decimal, model.FreezeCapitalTotal);
            //db.AddInParameter(dbCommand, "CapitalBalance", DbType.Decimal, model.CapitalBalance);
            db.AddInParameter(dbCommand, "MarginTotal", DbType.Decimal, model.MarginTotal);
            //db.AddInParameter(dbCommand, "TradeCurrencyType", DbType.Int32, model.TradeCurrencyType);
            db.AddInParameter(dbCommand, "CloseFloatProfitLossTotal", DbType.Decimal, model.CloseFloatProfitLossTotal);
            db.AddInParameter(dbCommand, "CloseMarketProfitLossTotal", DbType.Decimal, model.CloseMarketProfitLossTotal);
            db.ExecuteNonQuery(dbCommand, transaction);
        }

        /// <summary>
        /// �����±仯�ֶ�
        /// </summary>
        public bool UpdateRecord(QH_CapitalAccountTableInfo model, Database db, DbTransaction transaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QH_CapitalAccountTable set ");
            strSql.Append("AvailableCapital=@AvailableCapital,");
            //strSql.Append("UserAccountDistributeLogo=@UserAccountDistributeLogo,");
            //strSql.Append("BalanceOfTheDay=@BalanceOfTheDay,");
            strSql.Append("TodayOutInCapital=@TodayOutInCapital,");
            strSql.Append("FreezeCapitalTotal=@FreezeCapitalTotal,");
            //strSql.Append("CapitalBalance=@CapitalBalance,");
            strSql.Append("MarginTotal=@MarginTotal,");
            //strSql.Append("TradeCurrencyType=@TradeCurrencyType,");
            strSql.Append("CloseFloatProfitLossTotal=@CloseFloatProfitLossTotal,");
            strSql.Append("CloseMarketProfitLossTotal=@CloseMarketProfitLossTotal");
            strSql.Append(" where CapitalAccountLogoId=@CapitalAccountLogoId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalAccountLogoId", DbType.Int32, model.CapitalAccountLogoId);
            db.AddInParameter(dbCommand, "AvailableCapital", DbType.Decimal, model.AvailableCapital);
            //db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            //db.AddInParameter(dbCommand, "BalanceOfTheDay", DbType.Decimal, model.BalanceOfTheDay);
            db.AddInParameter(dbCommand, "TodayOutInCapital", DbType.Decimal, model.TodayOutInCapital);
            db.AddInParameter(dbCommand, "FreezeCapitalTotal", DbType.Decimal, model.FreezeCapitalTotal);
            //db.AddInParameter(dbCommand, "CapitalBalance", DbType.Decimal, model.CapitalBalance);
            db.AddInParameter(dbCommand, "MarginTotal", DbType.Decimal, model.MarginTotal);
            //db.AddInParameter(dbCommand, "TradeCurrencyType", DbType.Int32, model.TradeCurrencyType);
            db.AddInParameter(dbCommand, "CloseFloatProfitLossTotal", DbType.Decimal, model.CloseFloatProfitLossTotal);
            db.AddInParameter(dbCommand, "CloseMarketProfitLossTotal", DbType.Decimal, model.CloseMarketProfitLossTotal);
            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }

        }

        /// <summary>
        /// ����ֵ�ۼӵ�ԭ�����ֶ���(+=)
        /// </summary>
        public void AddUpdate(QH_CapitalAccountTableInfo model, Database db, DbTransaction transaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QH_CapitalAccountTable set ");
            strSql.Append("AvailableCapital=AvailableCapital+(@AvailableCapital),");
            //strSql.Append("UserAccountDistributeLogo=@UserAccountDistributeLogo,");
            //strSql.Append("BalanceOfTheDay=@BalanceOfTheDay,");
            strSql.Append("TodayOutInCapital=TodayOutInCapital+(@TodayOutInCapital),");
            strSql.Append("FreezeCapitalTotal=FreezeCapitalTotal+(@FreezeCapitalTotal),");
            //strSql.Append("CapitalBalance=@CapitalBalance,");
            strSql.Append("MarginTotal=MarginTotal+(@MarginTotal),");
            //strSql.Append("TradeCurrencyType=@TradeCurrencyType,");
            strSql.Append("CloseFloatProfitLossTotal=CloseFloatProfitLossTotal+(@CloseFloatProfitLossTotal),");
            strSql.Append("CloseMarketProfitLossTotal=CloseMarketProfitLossTotal+(@CloseMarketProfitLossTotal)");
            strSql.Append(" where CapitalAccountLogoId=@CapitalAccountLogoId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalAccountLogoId", DbType.Int32, model.CapitalAccountLogoId);
            db.AddInParameter(dbCommand, "AvailableCapital", DbType.Decimal, model.AvailableCapital);
            //db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            //db.AddInParameter(dbCommand, "BalanceOfTheDay", DbType.Decimal, model.BalanceOfTheDay);
            db.AddInParameter(dbCommand, "TodayOutInCapital", DbType.Decimal, model.TodayOutInCapital);
            db.AddInParameter(dbCommand, "FreezeCapitalTotal", DbType.Decimal, model.FreezeCapitalTotal);
            //db.AddInParameter(dbCommand, "CapitalBalance", DbType.Decimal, model.CapitalBalance);
            db.AddInParameter(dbCommand, "MarginTotal", DbType.Decimal, model.MarginTotal);
            //db.AddInParameter(dbCommand, "TradeCurrencyType", DbType.Int32, model.TradeCurrencyType);
            db.AddInParameter(dbCommand, "CloseFloatProfitLossTotal", DbType.Decimal, model.CloseFloatProfitLossTotal);
            db.AddInParameter(dbCommand, "CloseMarketProfitLossTotal", DbType.Decimal, model.CloseMarketProfitLossTotal);
            db.ExecuteNonQuery(dbCommand, transaction);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int CapitalAccountLogoId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from QH_CapitalAccountTable ");
            strSql.Append(" where CapitalAccountLogoId=@CapitalAccountLogoId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalAccountLogoId", DbType.Int32, CapitalAccountLogoId);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public QH_CapitalAccountTableInfo GetModel(int CapitalAccountLogoId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select CapitalAccountLogoId,AvailableCapital,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapitalTotal,CapitalBalance,MarginTotal,TradeCurrencyType,CloseFloatProfitLossTotal,CloseMarketProfitLossTotal from QH_CapitalAccountTable ");
            strSql.Append(" where CapitalAccountLogoId=@CapitalAccountLogoId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalAccountLogoId", DbType.Int32, CapitalAccountLogoId);
            QH_CapitalAccountTableInfo model = null;
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
        public List<QH_CapitalAccountTableInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select CapitalAccountLogoId,AvailableCapital,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapitalTotal,CapitalBalance,MarginTotal,TradeCurrencyType,CloseFloatProfitLossTotal,CloseMarketProfitLossTotal ");
            strSql.Append(" FROM QH_CapitalAccountTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<QH_CapitalAccountTableInfo> list = new List<QH_CapitalAccountTableInfo>();
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
        public List<QH_CapitalAccountTableInfo> GetListArray(Database db, DbTransaction transaction, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select CapitalAccountLogoId,AvailableCapital,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapitalTotal,CapitalBalance,MarginTotal,TradeCurrencyType,CloseFloatProfitLossTotal,CloseMarketProfitLossTotal ");
            strSql.Append(" FROM QH_CapitalAccountTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<QH_CapitalAccountTableInfo> list = new List<QH_CapitalAccountTableInfo>();
            using (IDataReader dataReader = db.ExecuteReader(transaction, CommandType.Text, strSql.ToString()))
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
        public List<QH_CapitalAccountTableInfo> GetAllListArray()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select CapitalAccountLogoId,AvailableCapital,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapitalTotal,CapitalBalance,MarginTotal,TradeCurrencyType,CloseFloatProfitLossTotal,CloseMarketProfitLossTotal ");
            strSql.Append(" FROM QH_CapitalAccountTable ");

            List<QH_CapitalAccountTableInfo> list = new List<QH_CapitalAccountTableInfo>();
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
        public QH_CapitalAccountTableInfo ReaderBind(IDataReader dataReader)
        {
            QH_CapitalAccountTableInfo model = new QH_CapitalAccountTableInfo();
            object ojb;
            ojb = dataReader["CapitalAccountLogoId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CapitalAccountLogoId = (int)ojb;
            }
            ojb = dataReader["AvailableCapital"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AvailableCapital = (decimal)ojb;
            }
            model.UserAccountDistributeLogo = dataReader["UserAccountDistributeLogo"].ToString();
            ojb = dataReader["BalanceOfTheDay"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BalanceOfTheDay = (decimal)ojb;
            }
            ojb = dataReader["TodayOutInCapital"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TodayOutInCapital = (decimal)ojb;
            }
            ojb = dataReader["FreezeCapitalTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FreezeCapitalTotal = (decimal)ojb;
            }
            ojb = dataReader["CapitalBalance"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CapitalBalance = (decimal)ojb;
            }
            ojb = dataReader["MarginTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MarginTotal = (decimal)ojb;
            }
            ojb = dataReader["TradeCurrencyType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradeCurrencyType = (int)ojb;
            }
            ojb = dataReader["CloseFloatProfitLossTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CloseFloatProfitLossTotal = (decimal)ojb;
            }
            ojb = dataReader["CloseMarketProfitLossTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CloseMarketProfitLossTotal = (decimal)ojb;
            }
            return model;
        }



        /// <summary>
        /// �����û�ID�������ѯ�û���ӵ�е��ڻ��ʽ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="pwd">����</param>
        /// <param name="type">Ҫ��ѯ�Ļ�������</param>
        /// <returns></returns>
        public List<QH_CapitalAccountTableInfo> GetListByUserIDAndPwd(string userID, string pwd, QueryType.QueryCurrencyType type)
        {
            return GetListArray(string.Format(BuildQueryWhere(type, QueryType.QueryWhereType.ByUserAndPwd), userID, pwd));
        }

        /// <summary>
        /// �����û�ID��ѯ�û���ӵ�е��ڻ��ʽ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="type">Ҫ��ѯ�Ļ�������</param>
        /// <returns></returns>
        public List<QH_CapitalAccountTableInfo> GetListByUserID(string userID, QueryType.QueryCurrencyType type)
        {
            return GetListArray(string.Format(BuildQueryWhere(type, QueryType.QueryWhereType.ByUserID), userID));
        }
        /// <summary>
        /// �����ڻ��ʽ��˺Ų�ѯ�ڻ��ʽ��˺���ϸ
        /// </summary>
        ///<param name="account">�ڻ��ʽ��˺�</param>
        /// <param name="type">����</param>
        /// <returns></returns>
        public List<QH_CapitalAccountTableInfo> GetListByAccount(string account, QueryType.QueryCurrencyType type)
        {
            return GetListArray(string.Format(BuildQueryWhere(type, QueryType.QueryWhereType.ByAccount), account));
        }
        /// <summary>
        /// Title:���ݲ�ѯ�Ļ������ͺͲ�ѯ���������͹�����ѯ��SQLScript
        /// Desc:Ĭ�Ϸ��ظ���������ѯ
        /// </summary>
        /// <param name="type">Ҫ��ѯ�Ļ�������</param>
        /// <param name="byType">��ѯ��������</param>
        /// <returns>���ز�ѯ�������ű����</returns>
        string BuildQueryWhere(QueryType.QueryCurrencyType type, QueryType.QueryWhereType byType)
        {
            string strByWhere = "";
            switch (byType)
            {
                case QueryType.QueryWhereType.ByUserID:
                    strByWhere = "  UserAccountDistributeLogo in( select useraccountdistributelogo from UA_UserAccountAllocationTable  where accounttypelogo in (select accounttypelogo from BD_AccountType where atcid='" + (int)GTA.VTS.Common.CommonObject.Types.AccountAttributionType.FuturesCapital + "')  and userid='{0}' )";
                    break;
                case QueryType.QueryWhereType.ByUserAndPwd:
                    strByWhere = "   UserAccountDistributeLogo in( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo in (select accounttypelogo from BD_AccountType where atcid='" + (int)GTA.VTS.Common.CommonObject.Types.AccountAttributionType.FuturesCapital + "') and userid in (select userid from dbo.UA_UserBasicInformationTable where  userid='{1}' And  Password ='{0}' ))";
                    break;
                case QueryType.QueryWhereType.ByAccount:
                    strByWhere = " UserAccountDistributeLogo='{0}'  ";
                    break;
                default:
                    strByWhere = " CapitalAccountLogoId ='{0}'";
                    break;
            }

            if (QueryType.QueryCurrencyType.ALL != type)
            {
                strByWhere += "  And  TradeCurrencyType='" + (int)type + "'";
            }
            return strByWhere;

        }

        #endregion  ��Ա����

        /// <summary>
        /// ���Ի��ʽ�����
        /// </summary>
        /// <param name="amount">���ý��</param>
        /// <param name="account">�ʽ��˺�</param>
        /// <param name="currencyType">����</param>
        /// <param name="db">�������Ӷ���</param>
        /// <param name="trm">��������</param>
        public void PersonalizationCapital(decimal amount, string account, GTA.VTS.Common.CommonObject.Types.CurrencyType currencyType, Database db, DbTransaction trm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [QH_CapitalAccountTable]   SET ");
            strSql.Append(" AvailableCapital = @amount");
            strSql.Append(" WHERE UserAccountDistributeLogo=@UserAccountDistributeLogo");
            strSql.Append("    And  TradeCurrencyType=@TradeCurrencyType");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "amount", DbType.Decimal, amount);
            db.AddInParameter(dbCommand, "TradeCurrencyType", DbType.Int32, (int)currencyType);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, account);
            db.ExecuteNonQuery(dbCommand, trm);
        }
    }
}