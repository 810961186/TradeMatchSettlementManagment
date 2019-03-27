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
    /// ���ݷ�����XH_CapitalAccountTableDal��
    /// Create BY�����
    /// Create Date��2009-10-15
    /// Update by:����
    /// Update date:2009-12-25
    /// Desc.: ��Ӹ��Ի��ʽ����õķ���
    /// </summary>
    public class XH_CapitalAccountTableDal
    {
        #region  ��Ա����
        #region �Ƿ���ڸü�¼
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int CapitalAccountLogo)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XH_CapitalAccountTable where CapitalAccountLogo=@CapitalAccountLogo ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalAccountLogo", DbType.Int32, CapitalAccountLogo);
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
        /// �õ�һ������ʵ��
        /// </summary>
        public XH_CapitalAccountTableInfo GetXHCapitalAccountWithNoLock(string strCapitalAccount, int iCurrType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select AvailableCapital,CapitalAccountLogo,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapitalTotal,CapitalBalance,TradeCurrencyType,HasDoneProfitLossTotal from XH_CapitalAccountTable WITH (NOLOCK)");
            strSql.Append(
                " where TradeCurrencyType=@TradeCurrencyType AND UserAccountDistributeLogo=@UserAccountDistributeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeCurrencyType", DbType.Int32, iCurrType);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.String, strCapitalAccount);
            XH_CapitalAccountTableInfo model = null;
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
        /// �õ�һ������ʵ��
        /// </summary>
        public XH_CapitalAccountTableInfo GetXHCapitalAccount(string strCapitalAccount, int iCurrType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select AvailableCapital,CapitalAccountLogo,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapitalTotal,CapitalBalance,TradeCurrencyType,HasDoneProfitLossTotal from XH_CapitalAccountTable ");
            strSql.Append(
                " where TradeCurrencyType=@TradeCurrencyType AND UserAccountDistributeLogo=@UserAccountDistributeLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "TradeCurrencyType", DbType.Int32, iCurrType);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.String, strCapitalAccount);
            XH_CapitalAccountTableInfo model = null;
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
        /// ��ȡ�����ʽ�
        /// </summary>
        /// <param name="strCapitalAccount">�ʽ��˺�</param>
        /// <param name="iCurrType">����</param>
        /// <returns></returns>
        public decimal GetAvailableCapitalWithNoLock(string strCapitalAccount, int iCurrType)
        {
            decimal result = -1;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select AvailableCapital from XH_CapitalAccountTable WITH (NOLOCK)");
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
        #endregion

        #region ����һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(XH_CapitalAccountTableInfo model)
        {
            Database db = DatabaseFactory.CreateDatabase();
            return Add(model, db, null);

        }

        /// <summary>
        /// ���Ƿ�����������һ������
        /// <param name="model">Ҫ������û��˺Ŷ���</param>
        /// <param name="db">�������ݶ���</param>
        /// <param name="trm">��������������Ϊnull������</param>
        /// </summary>
        public int Add(XH_CapitalAccountTableInfo model, Database db, DbTransaction trm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_CapitalAccountTable(");
            strSql.Append(
                "AvailableCapital,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapitalTotal,TradeCurrencyType,HasDoneProfitLossTotal)");

            strSql.Append(" values (");
            strSql.Append(
                "@AvailableCapital,@UserAccountDistributeLogo,@BalanceOfTheDay,@TodayOutInCapital,@FreezeCapitalTotal,@TradeCurrencyType,@HasDoneProfitLossTotal)");
            strSql.Append(";select @@IDENTITY");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AvailableCapital", DbType.Decimal, model.AvailableCapital);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "BalanceOfTheDay", DbType.Decimal, model.BalanceOfTheDay);
            db.AddInParameter(dbCommand, "TodayOutInCapital", DbType.Decimal, model.TodayOutInCapital);
            db.AddInParameter(dbCommand, "FreezeCapitalTotal", DbType.Decimal, model.FreezeCapitalTotal);
            //db.AddInParameter(dbCommand, "CapitalBalance", DbType.Decimal, model.CapitalBalance);//���ݿ�ͳ��ֵ���ø�ֵ
            db.AddInParameter(dbCommand, "TradeCurrencyType", DbType.Int32, model.TradeCurrencyType);
            db.AddInParameter(dbCommand, "HasDoneProfitLossTotal", DbType.Decimal, model.HasDoneProfitLossTotal);
            int result;
            object obj = null;
            if (trm == null)
            {
                obj = db.ExecuteScalar(dbCommand);
            }
            else
            {
                obj = db.ExecuteScalar(dbCommand, trm);
            }
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;

        }


        #endregion

        #region ����һ������
        /// <summary>
        /// ����һ������
        /// Update BY:���
        /// Update date:2009-07-16
        /// Desc.:���������Ƿ�����������������������Ƿ������񷽷�����ΪĬ�ϲ���������
        /// </summary>
        public void Update(XH_CapitalAccountTableInfo model)
        {
            Database db = DatabaseFactory.CreateDatabase();
            Update(model, db, null);
        }
        /// <summary>
        /// ���������������
        /// Create BY:���
        /// Create date:2009-07-16
       /// </summary>
        /// <param name="model">Ҫ���µ�ʵ��</param>
        /// <param name="db"></param>
        /// <param name="trm">���Ϊnull����������</param>
        public void Update(XH_CapitalAccountTableInfo model, Database db, DbTransaction trm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_CapitalAccountTable set ");
            strSql.Append("AvailableCapital=@AvailableCapital,");
            strSql.Append("UserAccountDistributeLogo=@UserAccountDistributeLogo,");
            strSql.Append("BalanceOfTheDay=@BalanceOfTheDay,");
            strSql.Append("TodayOutInCapital=@TodayOutInCapital,");
            strSql.Append("FreezeCapitalTotal=@FreezeCapitalTotal,");
            //strSql.Append("CapitalBalance=@CapitalBalance,");
            strSql.Append("TradeCurrencyType=@TradeCurrencyType,");
            strSql.Append("HasDoneProfitLossTotal=@HasDoneProfitLossTotal");
            strSql.Append(" where CapitalAccountLogo=@CapitalAccountLogo ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AvailableCapital", DbType.Decimal, model.AvailableCapital);
            db.AddInParameter(dbCommand, "CapitalAccountLogo", DbType.Int32, model.CapitalAccountLogo);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "BalanceOfTheDay", DbType.Decimal, model.BalanceOfTheDay);
            db.AddInParameter(dbCommand, "TodayOutInCapital", DbType.Decimal, model.TodayOutInCapital);
            db.AddInParameter(dbCommand, "FreezeCapitalTotal", DbType.Decimal, model.FreezeCapitalTotal);
            //db.AddInParameter(dbCommand, "CapitalBalance", DbType.Decimal, model.CapitalBalance);
            db.AddInParameter(dbCommand, "TradeCurrencyType", DbType.Int32, model.TradeCurrencyType);
            db.AddInParameter(dbCommand, "HasDoneProfitLossTotal", DbType.Decimal, model.HasDoneProfitLossTotal);
            if (trm != null)
            {
                db.ExecuteNonQuery(dbCommand, trm);
            }
            else
            {
                db.ExecuteNonQuery(dbCommand);
            }
        }
        #endregion

        #region �����±仯�ֶ�
        /// <summary>
        /// �����±仯�ֶ�
        /// </summary>
        /// <param name="capitalAccountLogo"></param>
        /// <param name="availableCapital"></param>
        /// <param name="freezeCapitalTotal"></param>
        /// <param name="todayOutInCapital"></param>
        /// <param name="hasDoneProfitLossTotal"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Update(int capitalAccountLogo, decimal availableCapital, decimal freezeCapitalTotal,
                           decimal todayOutInCapital, decimal hasDoneProfitLossTotal, Database db, DbTransaction transaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_CapitalAccountTable set ");
            strSql.Append("AvailableCapital=@AvailableCapital,");
            strSql.Append("FreezeCapitalTotal=@FreezeCapitalTotal,");
            strSql.Append("TodayOutInCapital=@TodayOutInCapital,");
            strSql.Append("HasDoneProfitLossTotal=@HasDoneProfitLossTotal");
            strSql.Append(" where CapitalAccountLogo=@CapitalAccountLogo ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AvailableCapital", DbType.Decimal, availableCapital);
            db.AddInParameter(dbCommand, "CapitalAccountLogo", DbType.Int32, capitalAccountLogo);
            db.AddInParameter(dbCommand, "FreezeCapitalTotal", DbType.Decimal, freezeCapitalTotal);
            db.AddInParameter(dbCommand, "TodayOutInCapital", DbType.Decimal, todayOutInCapital);
            db.AddInParameter(dbCommand, "HasDoneProfitLossTotal", DbType.Decimal, hasDoneProfitLossTotal);
            db.ExecuteNonQuery(dbCommand, transaction);
        }
        #endregion

        #region ����ֵ�ۼӵ�ԭ�����ֶ���(+=)
        /// <summary>
        /// ����ֵ�ۼӵ�ԭ�����ֶ���(+=)
        /// </summary>
        /// <param name="capitalAccountLogo"></param>
        /// <param name="availableCapital"></param>
        /// <param name="freezeCapitalTotal"></param>
        /// <param name="todayOutInCapital"></param>
        /// <param name="hasDoneProfitLossTotal"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void AddUpdate(int capitalAccountLogo, decimal availableCapital, decimal freezeCapitalTotal,
                           decimal todayOutInCapital, decimal hasDoneProfitLossTotal, Database db, DbTransaction transaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_CapitalAccountTable set ");
            strSql.Append("AvailableCapital=AvailableCapital+(@AvailableCapital),");
            strSql.Append("FreezeCapitalTotal=FreezeCapitalTotal+(@FreezeCapitalTotal),");
            strSql.Append("TodayOutInCapital=@TodayOutInCapital,");
            strSql.Append("HasDoneProfitLossTotal=HasDoneProfitLossTotal+(@HasDoneProfitLossTotal)");
            strSql.Append(" where CapitalAccountLogo=@CapitalAccountLogo ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AvailableCapital", DbType.Decimal, availableCapital);
            db.AddInParameter(dbCommand, "CapitalAccountLogo", DbType.Int32, capitalAccountLogo);
            db.AddInParameter(dbCommand, "FreezeCapitalTotal", DbType.Decimal, freezeCapitalTotal);
            db.AddInParameter(dbCommand, "TodayOutInCapital", DbType.Decimal, todayOutInCapital);
            db.AddInParameter(dbCommand, "HasDoneProfitLossTotal", DbType.Decimal, hasDoneProfitLossTotal);
            db.ExecuteNonQuery(dbCommand, transaction);
        }

        #endregion

        #region ɾ��һ������
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int CapitalAccountLogo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XH_CapitalAccountTable ");
            strSql.Append(" where CapitalAccountLogo=@CapitalAccountLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalAccountLogo", DbType.Int32, CapitalAccountLogo);
            db.ExecuteNonQuery(dbCommand);
        }
        #endregion

        #region �õ�һ������ʵ��
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public XH_CapitalAccountTableInfo GetModel(int CapitalAccountLogo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select AvailableCapital,CapitalAccountLogo,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapitalTotal,CapitalBalance,TradeCurrencyType,HasDoneProfitLossTotal from XH_CapitalAccountTable ");
            strSql.Append(" where CapitalAccountLogo=@CapitalAccountLogo ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CapitalAccountLogo", DbType.Int32, CapitalAccountLogo);
            XH_CapitalAccountTableInfo model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }
        #endregion

        #region ��������б�
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<XH_CapitalAccountTableInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select AvailableCapital,CapitalAccountLogo,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapitalTotal,CapitalBalance,TradeCurrencyType,HasDoneProfitLossTotal ");
            strSql.Append(" FROM XH_CapitalAccountTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<XH_CapitalAccountTableInfo> list = new List<XH_CapitalAccountTableInfo>();
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
        #endregion

        #region ������������б�
        /// <summary>
        /// ������������б�
        /// </summary>
        public List<XH_CapitalAccountTableInfo> GetAllListArray()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select AvailableCapital,CapitalAccountLogo,UserAccountDistributeLogo,BalanceOfTheDay,TodayOutInCapital,FreezeCapitalTotal,CapitalBalance,TradeCurrencyType,HasDoneProfitLossTotal ");
            strSql.Append(" FROM XH_CapitalAccountTable ");

            List<XH_CapitalAccountTableInfo> list = new List<XH_CapitalAccountTableInfo>();
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
        #endregion

        #region ����ʵ�������
        /// <summary>
        /// ����ʵ�������
        /// </summary>
        public XH_CapitalAccountTableInfo ReaderBind(IDataReader dataReader)
        {
            XH_CapitalAccountTableInfo model = new XH_CapitalAccountTableInfo();
            object ojb;
            ojb = dataReader["AvailableCapital"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AvailableCapital = (decimal)ojb;
            }
            ojb = dataReader["CapitalAccountLogo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CapitalAccountLogo = (int)ojb;
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
            ojb = dataReader["TradeCurrencyType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TradeCurrencyType = (int)ojb;
            }
            ojb = dataReader["HasDoneProfitLossTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HasDoneProfitLossTotal = (decimal)ojb;
            }
            return model;
        }
        #endregion

        #region �����û�ID�������ѯ�û���ӵ�е��ֻ��ʽ��˺���ϸ
        /// <summary>
        /// �����û�ID�������ѯ�û���ӵ�е��ֻ��ʽ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="pwd">����</param>
        /// <param name="type">Ҫ��ѯ�Ļ�������</param>
        /// <returns></returns>
        public List<XH_CapitalAccountTableInfo> GetListByUserIDAndPwd(string userID, string pwd, QueryType.QueryCurrencyType type)
        {
            return GetListArray(string.Format(BuildQueryWhere(type, QueryType.QueryWhereType.ByUserAndPwd), userID, pwd));
        }
        #endregion

        #region �����û�ID��ѯ�û���ӵ�е��ֻ��ʽ��˺���ϸ
        /// <summary>
        /// �����û�ID��ѯ�û���ӵ�е��ֻ��ʽ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="type">Ҫ��ѯ�Ļ�������</param>
        /// <returns></returns>
        public List<XH_CapitalAccountTableInfo> GetListByUserID(string userID, QueryType.QueryCurrencyType type)
        {
            return GetListArray(string.Format(BuildQueryWhere(type, QueryType.QueryWhereType.ByUserID), userID));
        }
        #endregion

        #region  �����ֻ��ʽ��˺Ų�ѯ�ֻ��ʽ��˺���ϸ
        /// <summary>
        /// �����ֻ��ʽ��˺Ų�ѯ�ֻ��ʽ��˺���ϸ
        /// </summary>
        ///<param name="account">�ֻ��ʽ��˺�</param>
        /// <param name="type">����</param>
        /// <returns></returns>
        public List<XH_CapitalAccountTableInfo> GetListByAccount(string account, QueryType.QueryCurrencyType type)
        {
            return GetListArray(string.Format(BuildQueryWhere(type, QueryType.QueryWhereType.ByAccount), account));
        }
        #endregion

        #region ���ݲ�ѯ�Ļ������ͺͲ�ѯ���������͹�����ѯ��SQLScript
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
                    strByWhere = "  UserAccountDistributeLogo in( select useraccountdistributelogo from UA_UserAccountAllocationTable  where accounttypelogo in (select accounttypelogo from BD_AccountType where atcid='" + (int)GTA.VTS.Common.CommonObject.Types.AccountAttributionType.SpotCapital + "')  and userid='{0}' )";
                    break;
                case QueryType.QueryWhereType.ByUserAndPwd:
                    strByWhere = "   UserAccountDistributeLogo in( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo in (select accounttypelogo from BD_AccountType where atcid='" + (int)GTA.VTS.Common.CommonObject.Types.AccountAttributionType.SpotCapital + "') and userid in (select userid from dbo.UA_UserBasicInformationTable where  userid='{1}' And  Password ='{0}' ))";
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
        #endregion

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
            strSql.Append("UPDATE [XH_CapitalAccountTable]   SET ");
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