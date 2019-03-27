#region Using Namespace

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReckoningCounter.Model;

#endregion

namespace ReckoningCounter.DAL.Data
{
    /// <summary>
    /// ���ݷ�����UA_BankAccountTableDal��
    /// Update by:����
    /// Update date:2009-12-24
    /// Desc.: ��Ӹ��Ի��ʽ����õķ���
    /// </summary>
    public class UA_BankAccountTableDal
    {
        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            string strsql = "select max(TradeCurrencyTypeLogo)+1 from UA_BankAccountTable";
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
        public bool Exists(int TradeCurrencyTypeLogo, string UserAccountDistributeLogo)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select count(1) from UA_BankAccountTable where TradeCurrencyTypeLogo=@TradeCurrencyTypeLogo and UserAccountDistributeLogo=@UserAccountDistributeLogo ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeCurrencyTypeLogo", DbType.Int32, TradeCurrencyTypeLogo);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, UserAccountDistributeLogo);
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
        /// ����һ������
        /// </summary>
        public void Add(UA_BankAccountTableInfo model)
        {
            Database db = DatabaseFactory.CreateDatabase();
            Add(model, db, null);

        }
        /// <summary>
        /// ���Ƿ�����������һ������
        /// <param name="model">Ҫ������û��˺Ŷ���</param>
        /// <param name="db">�������ݶ���</param>
        /// <param name="trm">��������������Ϊnull������</param>
        /// </summary>
        public void Add(UA_BankAccountTableInfo model, Database db, DbTransaction trm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UA_BankAccountTable(");
            strSql.Append(
                "CapitalRemainAmount,TradeCurrencyTypeLogo,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapital,AvailableCapital)");

            strSql.Append(" values (");
            strSql.Append(
                "@CapitalRemainAmount,@TradeCurrencyTypeLogo,@UserAccountDistributeLogo,@BalanceOfTheDay,@TodayOutInCapital,@FreezeCapital,@AvailableCapital)");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalRemainAmount", DbType.Decimal, model.CapitalRemainAmount);
            db.AddInParameter(dbCommand, "TradeCurrencyTypeLogo", DbType.Int32, model.TradeCurrencyTypeLogo);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "BalanceOfTheDay", DbType.Decimal, model.BalanceOfTheDay);
            db.AddInParameter(dbCommand, "TodayOutInCapital", DbType.Decimal, model.TodayOutInCapital);
            db.AddInParameter(dbCommand, "FreezeCapital", DbType.Decimal, model.FreezeCapital);
            db.AddInParameter(dbCommand, "AvailableCapital", DbType.Decimal, model.AvailableCapital);
            if (trm == null)
            {
                db.ExecuteNonQuery(dbCommand);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand, trm);
            }
        }

        /// <summary>
        /// �����Ƿ�ʹ������������
        /// </summary>
        /// <param name="model"></param>
        /// <param name="db"></param>
        /// <param name="trm"></param>
        public void Update(UA_BankAccountTableInfo model, Database db, DbTransaction trm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UA_BankAccountTable set ");
            strSql.Append("CapitalRemainAmount=@CapitalRemainAmount,");
            strSql.Append("BalanceOfTheDay=@BalanceOfTheDay,");
            strSql.Append("TodayOutInCapital=@TodayOutInCapital,");
            strSql.Append("FreezeCapital=@FreezeCapital,");
            strSql.Append("AvailableCapital=@AvailableCapital");
            strSql.Append(
                " where TradeCurrencyTypeLogo=@TradeCurrencyTypeLogo and UserAccountDistributeLogo=@UserAccountDistributeLogo ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalRemainAmount", DbType.Decimal, model.CapitalRemainAmount);
            db.AddInParameter(dbCommand, "TradeCurrencyTypeLogo", DbType.Int32, model.TradeCurrencyTypeLogo);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "BalanceOfTheDay", DbType.Decimal, model.BalanceOfTheDay);
            db.AddInParameter(dbCommand, "TodayOutInCapital", DbType.Decimal, model.TodayOutInCapital);
            db.AddInParameter(dbCommand, "FreezeCapital", DbType.Decimal, model.FreezeCapital);
            db.AddInParameter(dbCommand, "AvailableCapital", DbType.Decimal, model.AvailableCapital);
            if (trm != null)
            {
                db.ExecuteNonQuery(dbCommand, trm);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand);
            }
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(UA_BankAccountTableInfo model)
        {
            Database db = DatabaseFactory.CreateDatabase();
            Update(model, db, null);

        }

        /// <summary>
        /// ����ʱ���������˺źͽ��׻������������ʽ�
        /// Create BY:���
        /// Crate Date:2009-07-12
        /// </summary>
        /// <param name="addAmount">��������</param>
        /// <param name="bankAccount">�����˺�</param>
        /// <param name="currencyType">���׻�������</param>
        /// <param name="db"></param>
        /// <param name="trm">��������</param>
        public void AddCapital(decimal addAmount, string bankAccount, GTA.VTS.Common.CommonObject.Types.CurrencyType currencyType, Database db, DbTransaction trm)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [UA_BankAccountTable]   SET ");
            strSql.Append(" CapitalRemainAmount = CapitalRemainAmount + @AddAmount");
            strSql.Append("  ,TodayOutInCapital = TodayOutInCapital + @AddAmount");
            strSql.Append("  ,AvailableCapital =AvailableCapital + @AddAmount");
            strSql.Append(" WHERE UserAccountDistributeLogo=@UserAccountDistributeLogo");
            strSql.Append("    And  TradeCurrencyTypeLogo=@TradeCurrencyTypeLogo");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AddAmount", DbType.Decimal, addAmount);
            db.AddInParameter(dbCommand, "TradeCurrencyTypeLogo", DbType.Int32, (int)currencyType);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, bankAccount);
            db.ExecuteNonQuery(dbCommand, trm);
        }

        /// <summary>
        /// ת��ʱ���������˺źͽ��׻����������ӻ��߼����ʽ�ת��ת��)
        /// Create BY:���
        /// Crate Date:2009-07-12
        /// </summary>
        /// <param name="addAmount">��������</param>
        /// <param name="isAdd">���ӻ��߼���</param>
        /// <param name="bankAccount">�����˺�</param>
        /// <param name="currencyType">���׻�������</param>
        /// <param name="db"></param>
        /// <param name="trm">��������</param>
        public void AddOrSubCapital(decimal addAmount, bool isAdd, string bankAccount, GTA.VTS.Common.CommonObject.Types.CurrencyType currencyType, Database db, DbTransaction trm)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE [UA_BankAccountTable]   SET ");
            strSql.Append(" CapitalRemainAmount = CapitalRemainAmount {0} @AddAmount");
            strSql.Append("  ,TodayOutInCapital = TodayOutInCapital {0} @AddAmount");
            strSql.Append("  ,AvailableCapital =AvailableCapital {0} @AddAmount");
            strSql.Append("  ,BalanceOfTheDay =BalanceOfTheDay {0} @AddAmount");
            strSql.Append(" WHERE UserAccountDistributeLogo=@UserAccountDistributeLogo");
            strSql.Append("    And  TradeCurrencyTypeLogo=@TradeCurrencyTypeLogo");
            DbCommand dbCommand = db.GetSqlStringCommand(string.Format(strSql.ToString(), isAdd ? "+" : "-"));
            db.AddInParameter(dbCommand, "AddAmount", DbType.Decimal, addAmount);
            db.AddInParameter(dbCommand, "TradeCurrencyTypeLogo", DbType.Int32, (int)currencyType);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, bankAccount);
            db.ExecuteNonQuery(dbCommand, trm);
        }



        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int TradeCurrencyTypeLogo, string UserAccountDistributeLogo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UA_BankAccountTable ");
            strSql.Append(
                " where TradeCurrencyTypeLogo=@TradeCurrencyTypeLogo and UserAccountDistributeLogo=@UserAccountDistributeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeCurrencyTypeLogo", DbType.Int32, TradeCurrencyTypeLogo);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, UserAccountDistributeLogo);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public UA_BankAccountTableInfo GetModel(int TradeCurrencyTypeLogo, string UserAccountDistributeLogo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select CapitalRemainAmount,TradeCurrencyTypeLogo,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapital,AvailableCapital from UA_BankAccountTable ");
            strSql.Append(
                " where TradeCurrencyTypeLogo=@TradeCurrencyTypeLogo and UserAccountDistributeLogo=@UserAccountDistributeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeCurrencyTypeLogo", DbType.Int32, TradeCurrencyTypeLogo);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, UserAccountDistributeLogo);
            UA_BankAccountTableInfo model = null;
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
        public List<UA_BankAccountTableInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select CapitalRemainAmount,TradeCurrencyTypeLogo,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapital,AvailableCapital ");
            strSql.Append(" FROM UA_BankAccountTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<UA_BankAccountTableInfo> list = new List<UA_BankAccountTableInfo>();
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
        public UA_BankAccountTableInfo ReaderBind(IDataReader dataReader)
        {
            UA_BankAccountTableInfo model = new UA_BankAccountTableInfo();
            object ojb;
            ojb = dataReader["CapitalRemainAmount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CapitalRemainAmount = (decimal)ojb;
            }
            ojb = dataReader["TradeCurrencyTypeLogo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradeCurrencyTypeLogo = (int)ojb;
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
            ojb = dataReader["FreezeCapital"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FreezeCapital = (decimal)ojb;
            }
            ojb = dataReader["AvailableCapital"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AvailableCapital = (decimal)ojb;
            }
            return model;
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
            strSql.Append("UPDATE [UA_BankAccountTable]   SET ");
            strSql.Append(" AvailableCapital = @amount");
            strSql.Append(" WHERE UserAccountDistributeLogo=@UserAccountDistributeLogo");
            strSql.Append("    And  TradeCurrencyTypeLogo=@TradeCurrencyTypeLogo");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "amount", DbType.Decimal, amount);
            db.AddInParameter(dbCommand, "TradeCurrencyTypeLogo", DbType.Int32, (int)currencyType);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, account);
            db.ExecuteNonQuery(dbCommand, trm);
        }
    }
}