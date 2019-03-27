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
    /// ���ݷ�����XH_AccountHoldTableDal��
    /// </summary>
    public class XH_AccountHoldTableDal
    {
        #region  ��Ա����

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int AccountHoldLogoId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XH_AccountHoldTable where AccountHoldLogoId=@AccountHoldLogoId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AccountHoldLogoId", DbType.Int32, AccountHoldLogoId);
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
        public  XH_AccountHoldTableInfo GetXhAccountHoldTableWithNoLock(string strHoldAccount, string strCode, int iCurrType)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append(
            //    "select AccountHoldLogoId,AvailableAmount,FreezeAmount,UserAccountDistributeLogo,CostPrice,Code,BreakevenPrice,CurrencyTypeId from XH_AccountHoldTable WITH (NOLOCK)");
            strSql.Append("select AccountHoldLogoId,UserAccountDistributeLogo,CurrencyTypeId,Code,AvailableAmount,FreezeAmount,CostPrice,BreakevenPrice,HoldAveragePrice from XH_AccountHoldTable WITH (NOLOCK)");

            strSql.Append(
                " where CurrencyTypeId=@CurrencyTypeId AND UserAccountDistributeLogo=@UserAccountDistributeLogo AND Code=@Code");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, iCurrType);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.String, strHoldAccount);
            db.AddInParameter(dbCommand, "Code", DbType.String, strCode);
            XH_AccountHoldTableInfo model = null;
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
        /// ����һ������
        /// </summary>
        public int Add(XH_AccountHoldTableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_AccountHoldTable(");
            strSql.Append("UserAccountDistributeLogo,CurrencyTypeId,Code,AvailableAmount,FreezeAmount,CostPrice,BreakevenPrice,HoldAveragePrice)");

            strSql.Append(" values (");
            strSql.Append("@UserAccountDistributeLogo,@CurrencyTypeId,@Code,@AvailableAmount,@FreezeAmount,@CostPrice,@BreakevenPrice,@HoldAveragePrice)");
            strSql.Append(";select @@IDENTITY");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, model.CurrencyTypeId);
            db.AddInParameter(dbCommand, "Code", DbType.AnsiString, model.Code);
            db.AddInParameter(dbCommand, "AvailableAmount", DbType.Decimal, model.AvailableAmount);
            db.AddInParameter(dbCommand, "FreezeAmount", DbType.Decimal, model.FreezeAmount);
            db.AddInParameter(dbCommand, "CostPrice", DbType.Decimal, model.CostPrice);
            db.AddInParameter(dbCommand, "BreakevenPrice", DbType.Decimal, model.BreakevenPrice);
            db.AddInParameter(dbCommand, "HoldAveragePrice", DbType.Decimal, model.HoldAveragePrice);
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
        public int Add(XH_AccountHoldTableInfo model, Database db ,DbTransaction transaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_AccountHoldTable(");
            strSql.Append("UserAccountDistributeLogo,CurrencyTypeId,Code,AvailableAmount,FreezeAmount,CostPrice,BreakevenPrice,HoldAveragePrice)");

            strSql.Append(" values (");
            strSql.Append("@UserAccountDistributeLogo,@CurrencyTypeId,@Code,@AvailableAmount,@FreezeAmount,@CostPrice,@BreakevenPrice,@HoldAveragePrice)");
            strSql.Append(";select @@IDENTITY");
            //Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, model.CurrencyTypeId);
            db.AddInParameter(dbCommand, "Code", DbType.AnsiString, model.Code);
            db.AddInParameter(dbCommand, "AvailableAmount", DbType.Decimal, model.AvailableAmount);
            db.AddInParameter(dbCommand, "FreezeAmount", DbType.Decimal, model.FreezeAmount);
            db.AddInParameter(dbCommand, "CostPrice", DbType.Decimal, model.CostPrice);
            db.AddInParameter(dbCommand, "BreakevenPrice", DbType.Decimal, model.BreakevenPrice);
            db.AddInParameter(dbCommand, "HoldAveragePrice", DbType.Decimal, model.HoldAveragePrice);
            int result;
            object obj = db.ExecuteScalar(dbCommand, transaction);
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public XH_AccountHoldTableInfo GetXhAccountHoldTable(string strHoldAccount, string strCode, int iCurrType)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append(
            //    "select AccountHoldLogoId,AvailableAmount,FreezeAmount,UserAccountDistributeLogo,CostPrice,Code,BreakevenPrice,CurrencyTypeId from XH_AccountHoldTable ");
            strSql.Append("select AccountHoldLogoId,UserAccountDistributeLogo,CurrencyTypeId,Code,AvailableAmount,FreezeAmount,CostPrice,BreakevenPrice,HoldAveragePrice from XH_AccountHoldTable ");

            strSql.Append(
                " where CurrencyTypeId=@CurrencyTypeId AND UserAccountDistributeLogo=@UserAccountDistributeLogo AND Code=@Code");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, iCurrType);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.String, strHoldAccount);
            db.AddInParameter(dbCommand, "Code", DbType.String, strCode);
            XH_AccountHoldTableInfo model = null;
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
        /// ����һ������
        /// </summary>
        public bool AddRecord(XH_AccountHoldTableInfo model,ReckoningTransaction tm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XH_AccountHoldTable(");
            strSql.Append(
                "AvailableAmount,FreezeAmount,UserAccountDistributeLogo,CostPrice,Code,BreakevenPrice,CurrencyTypeId,HoldAveragePrice)");

            strSql.Append(" values (");
            strSql.Append(
                "@AvailableAmount,@FreezeAmount,@UserAccountDistributeLogo,@CostPrice,@Code,@BreakevenPrice,@CurrencyTypeId,@HoldAveragePrice)");
            strSql.Append(";select @@IDENTITY");
            Database db = tm.Database;
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AvailableAmount", DbType.Decimal, model.AvailableAmount);
            db.AddInParameter(dbCommand, "FreezeAmount", DbType.Decimal, model.FreezeAmount);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "CostPrice", DbType.Decimal, model.CostPrice);
            db.AddInParameter(dbCommand, "Code", DbType.AnsiString, model.Code);
            db.AddInParameter(dbCommand, "BreakevenPrice", DbType.Decimal, model.BreakevenPrice);
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, model.CurrencyTypeId);
            db.AddInParameter(dbCommand, "HoldAveragePrice", DbType.Decimal, model.HoldAveragePrice);
            int result;
            object obj = db.ExecuteScalar(dbCommand,tm.Transaction);
            if (!int.TryParse(obj.ToString(), out result))
            {
                return false;
            }

            model.AccountHoldLogoId = result;
            return true;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(XH_AccountHoldTableInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_AccountHoldTable set ");
            strSql.Append("UserAccountDistributeLogo=@UserAccountDistributeLogo,");
            strSql.Append("CurrencyTypeId=@CurrencyTypeId,");
            strSql.Append("Code=@Code,");
            strSql.Append("AvailableAmount=@AvailableAmount,");
            strSql.Append("FreezeAmount=@FreezeAmount,");
            strSql.Append("CostPrice=@CostPrice,");
            strSql.Append("BreakevenPrice=@BreakevenPrice,");
            strSql.Append("HoldAveragePrice=@HoldAveragePrice");
            strSql.Append(" where AccountHoldLogoId=@AccountHoldLogoId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AccountHoldLogoId", DbType.Int32, model.AccountHoldLogoId);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, model.CurrencyTypeId);
            db.AddInParameter(dbCommand, "Code", DbType.AnsiString, model.Code);
            db.AddInParameter(dbCommand, "AvailableAmount", DbType.Decimal, model.AvailableAmount);
            db.AddInParameter(dbCommand, "FreezeAmount", DbType.Decimal, model.FreezeAmount);
            db.AddInParameter(dbCommand, "CostPrice", DbType.Decimal, model.CostPrice);
            db.AddInParameter(dbCommand, "BreakevenPrice", DbType.Decimal, model.BreakevenPrice);
            db.AddInParameter(dbCommand, "HoldAveragePrice", DbType.Decimal, model.HoldAveragePrice);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void AddUpdate(XH_AccountHoldTableInfo_Delta model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_AccountHoldTable set ");
            strSql.Append("AvailableAmount=AvailableAmount+@AvailableAmount,");
            strSql.Append("FreezeAmount=FreezeAmount+@FreezeAmount");
            strSql.Append(" where AccountHoldLogoId=@AccountHoldLogoId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AccountHoldLogoId", DbType.Int32, model.AccountHoldLogoId);
            db.AddInParameter(dbCommand, "AvailableAmount", DbType.Decimal, model.AvailableAmountDelta);
            db.AddInParameter(dbCommand, "FreezeAmount", DbType.Decimal, model.FreezeAmountDelta);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// �������������
        /// </summary>
        /// <param name="model">ʵ��</param>
        /// <param name="db">Database</param>
        /// <param name="transaction">DbTransaction</param>
        public void Update(XH_AccountHoldTableInfo model, Database db, DbTransaction transaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_AccountHoldTable set ");
            strSql.Append("UserAccountDistributeLogo=@UserAccountDistributeLogo,");
            strSql.Append("CurrencyTypeId=@CurrencyTypeId,");
            strSql.Append("Code=@Code,");
            strSql.Append("AvailableAmount=@AvailableAmount,");
            strSql.Append("FreezeAmount=@FreezeAmount,");
            strSql.Append("CostPrice=@CostPrice,");
            strSql.Append("BreakevenPrice=@BreakevenPrice,");
            strSql.Append("HoldAveragePrice=@HoldAveragePrice");
            strSql.Append(" where AccountHoldLogoId=@AccountHoldLogoId ");
            //Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AccountHoldLogoId", DbType.Int32, model.AccountHoldLogoId);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, model.CurrencyTypeId);
            db.AddInParameter(dbCommand, "Code", DbType.AnsiString, model.Code);
            db.AddInParameter(dbCommand, "AvailableAmount", DbType.Decimal, model.AvailableAmount);
            db.AddInParameter(dbCommand, "FreezeAmount", DbType.Decimal, model.FreezeAmount);
            db.AddInParameter(dbCommand, "CostPrice", DbType.Decimal, model.CostPrice);
            db.AddInParameter(dbCommand, "BreakevenPrice", DbType.Decimal, model.BreakevenPrice);
            db.AddInParameter(dbCommand, "HoldAveragePrice", DbType.Decimal, model.HoldAveragePrice);
            db.ExecuteNonQuery(dbCommand, transaction);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void AddUpdate(XH_AccountHoldTableInfo_Delta model, Database db, DbTransaction transaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_AccountHoldTable set ");
            strSql.Append("AvailableAmount=AvailableAmount+@AvailableAmount,");
            strSql.Append("FreezeAmount=FreezeAmount+@FreezeAmount");
            strSql.Append(" where AccountHoldLogoId=@AccountHoldLogoId ");
            //Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AccountHoldLogoId", DbType.Int32, model.AccountHoldLogoId);
            db.AddInParameter(dbCommand, "AvailableAmount", DbType.Decimal, model.AvailableAmountDelta);
            db.AddInParameter(dbCommand, "FreezeAmount", DbType.Decimal, model.FreezeAmountDelta);
            db.ExecuteNonQuery(dbCommand, transaction);
        }

        /// <summary>
        /// �������������
        /// </summary>
        /// <param name="model">ʵ��</param>
        /// <param name="db">Database</param>
        /// <param name="transaction">DbTransaction</param>
        public bool UpdateRecord(XH_AccountHoldTableInfo model, Database db, DbTransaction transaction)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XH_AccountHoldTable set ");
            strSql.Append("AvailableAmount=@AvailableAmount,");
            strSql.Append("FreezeAmount=@FreezeAmount,");
            strSql.Append("UserAccountDistributeLogo=@UserAccountDistributeLogo,");
            strSql.Append("CostPrice=@CostPrice,");
            strSql.Append("Code=@Code,");
            strSql.Append("BreakevenPrice=@BreakevenPrice,");
            strSql.Append("CurrencyTypeId=@CurrencyTypeId,");
            strSql.Append("HoldAveragePrice=@HoldAveragePrice");
            strSql.Append(" where AccountHoldLogoId=@AccountHoldLogoId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AccountHoldLogoId", DbType.Int32, model.AccountHoldLogoId);
            db.AddInParameter(dbCommand, "AvailableAmount", DbType.Decimal, model.AvailableAmount);
            db.AddInParameter(dbCommand, "FreezeAmount", DbType.Decimal, model.FreezeAmount);
            db.AddInParameter(dbCommand, "UserAccountDistributeLogo", DbType.AnsiString, model.UserAccountDistributeLogo);
            db.AddInParameter(dbCommand, "CostPrice", DbType.Decimal, model.CostPrice);
            db.AddInParameter(dbCommand, "Code", DbType.AnsiString, model.Code);
            db.AddInParameter(dbCommand, "BreakevenPrice", DbType.Decimal, model.BreakevenPrice);
            db.AddInParameter(dbCommand, "CurrencyTypeId", DbType.Int32, model.CurrencyTypeId);
            db.AddInParameter(dbCommand, "HoldAveragePrice", DbType.Decimal, model.HoldAveragePrice);
            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
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
        public void Delete(int AccountHoldLogoId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XH_AccountHoldTable ");
            strSql.Append(" where AccountHoldLogoId=@AccountHoldLogoId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AccountHoldLogoId", DbType.Int32, AccountHoldLogoId);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public XH_AccountHoldTableInfo GetModel(int AccountHoldLogoId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AccountHoldLogoId,UserAccountDistributeLogo,CurrencyTypeId,Code,AvailableAmount,FreezeAmount,CostPrice,BreakevenPrice,HoldAveragePrice from XH_AccountHoldTable ");
            strSql.Append(" where AccountHoldLogoId=@AccountHoldLogoId ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "AccountHoldLogoId", DbType.Int32, AccountHoldLogoId);
            XH_AccountHoldTableInfo model = null;
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
        public List<XH_AccountHoldTableInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AccountHoldLogoId,UserAccountDistributeLogo,CurrencyTypeId,Code,AvailableAmount,FreezeAmount,CostPrice,BreakevenPrice,HoldAveragePrice ");
            strSql.Append(" FROM XH_AccountHoldTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<XH_AccountHoldTableInfo> list = new List<XH_AccountHoldTableInfo>();
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
        /// ���ȫ�������б�
        /// </summary>
        public List<XH_AccountHoldTableInfo> GetAllListArray()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AccountHoldLogoId,UserAccountDistributeLogo,CurrencyTypeId,Code,AvailableAmount,FreezeAmount,CostPrice,BreakevenPrice,HoldAveragePrice ");
            strSql.Append(" FROM XH_AccountHoldTable ");
            
            List<XH_AccountHoldTableInfo> list = new List<XH_AccountHoldTableInfo>();
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
        public XH_AccountHoldTableInfo ReaderBind(IDataReader dataReader)
        {
            XH_AccountHoldTableInfo model = new XH_AccountHoldTableInfo();
            object ojb;
            ojb = dataReader["AccountHoldLogoId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AccountHoldLogoId = (int)ojb;
            }
            model.UserAccountDistributeLogo = dataReader["UserAccountDistributeLogo"].ToString();
            ojb = dataReader["CurrencyTypeId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CurrencyTypeId = (int)ojb;
            }
            model.Code = dataReader["Code"].ToString();
            ojb = dataReader["AvailableAmount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AvailableAmount = (decimal)ojb;
            }
            ojb = dataReader["FreezeAmount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FreezeAmount = (decimal)ojb;
            }
            ojb = dataReader["CostPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CostPrice = (decimal)ojb;
            }
            ojb = dataReader["BreakevenPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreakevenPrice = (decimal)ojb;
            }
            ojb = dataReader["HoldAveragePrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HoldAveragePrice = (decimal)ojb;
            }
            return model;
        }

        #region �����û�ID�������ѯ�û���ӵ�е��ֻ��ֲ��˺���ϸ
        /// <summary>
        /// �����û�ID�������ѯ�û���ӵ�е��ֻ��ֲ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="pwd">����</param>
        /// <param name="type">Ҫ��ѯ�Ļ�������</param>
        /// <returns></returns>
        public List<XH_AccountHoldTableInfo> GetListByUserIDAndPwd(string userID, string pwd, QueryType.QueryCurrencyType type)
        {
            return GetListArray(string.Format(BuildQueryWhere(type, QueryType.QueryWhereType.ByUserAndPwd), userID, pwd));
        }
        #endregion

        #region �����û�ID��ѯ�û���ӵ�е��ֻ��ֲ��˺���ϸ
        /// <summary>
        /// �����û�ID��ѯ�û���ӵ�е��ֻ��ֲ��˺���ϸ
        /// </summary>
        /// <param name="userID">�û�������Ա��ID</param>
        /// <param name="type">Ҫ��ѯ�Ļ�������</param>
        /// <returns></returns>
        public List<XH_AccountHoldTableInfo> GetListByUserID(string userID, QueryType.QueryCurrencyType type)
        {
            return GetListArray(string.Format(BuildQueryWhere(type, QueryType.QueryWhereType.ByUserID), userID));
        }
        #endregion

        #region  �����ֻ��ֲ��˺Ų�ѯ�ֻ��ֲ��˺���ϸ
        /// <summary>
        /// �����ֻ��ֲ��˺Ų�ѯ�ֻ��ֲ��˺���ϸ
        /// </summary>
        ///<param name="account">�ֻ��ֲ��˺�</param>
        /// <param name="type">����</param>
        /// <returns></returns>
        public List<XH_AccountHoldTableInfo> GetListByAccount(string account, QueryType.QueryCurrencyType type)
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
                    strByWhere = "  UserAccountDistributeLogo in( select useraccountdistributelogo from UA_UserAccountAllocationTable  where accounttypelogo in (select accounttypelogo from BD_AccountType where atcid='" + (int)GTA.VTS.Common.CommonObject.Types.AccountAttributionType.SpotHold + "')  and userid='{0}' )";
                    break;
                case QueryType.QueryWhereType.ByUserAndPwd:
                    strByWhere = "   UserAccountDistributeLogo in( select useraccountdistributelogo from dbo.UA_UserAccountAllocationTable  where accounttypelogo in (select accounttypelogo from BD_AccountType where atcid='" + (int)GTA.VTS.Common.CommonObject.Types.AccountAttributionType.SpotHold + "') and userid in (select userid from dbo.UA_UserBasicInformationTable where  userid='{1}' And  Password ='{0}' ))";
                    break;
                case QueryType.QueryWhereType.ByAccount:
                    strByWhere = " UserAccountDistributeLogo='{0}'  ";
                    break;
                default:
                    strByWhere = " AccountHoldLogoId ='{0}'";
                    break;
            }

            if (QueryType.QueryCurrencyType.ALL != type)
            {
                strByWhere += "  And  CurrencyTypeId='" + (int)type + "'";
            }
            return strByWhere;

        }
        #endregion
 
        #endregion  ��Ա����
    }
}