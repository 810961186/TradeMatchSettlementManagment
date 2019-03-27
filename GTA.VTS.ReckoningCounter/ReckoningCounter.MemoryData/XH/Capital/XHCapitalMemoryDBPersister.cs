#region Using Namespace

using System;
using System.Collections.Generic;
using System.Data.Common;
using GTA.VTS.Common.CommonUtility;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReckoningCounter.DAL.Data;
using ReckoningCounter.MemoryData.Interface;
using ReckoningCounter.Model;

#endregion

namespace ReckoningCounter.MemoryData.XH.Capital
{
    /// <summary>
    /// �ֻ��ڴ��ʽ�����ݿ�־û���
    /// ���ߣ�����
    /// </summary>
    public class XHCapitalMemoryDBPersister :
        IMemoryPersister<int, XH_CapitalAccountTableInfo, XH_CapitalAccountTable_DeltaInfo>
    {
        #region Implementation of IMemoryPersister<XHCapitalMemoryTable,XH_CapitalAccountTable_DeltaInfo>

        /// <summary>
        /// ���캯��
        /// </summary>
        public void SyncChangeToBase()
        {
            XH_CapitalAccountTable_DeltaInfoDal deltaInfoDal = new XH_CapitalAccountTable_DeltaInfoDal();
            var deltaList = deltaInfoDal.GetAllSum();

            if (deltaList == null)
                return;

            if (deltaList.Count == 0)
                return;

            try
            {
                bool isSuccess = false;
                Database database = DatabaseFactory.CreateDatabase();
                XH_CapitalAccountTableDal dal = new XH_CapitalAccountTableDal();

                using (DbConnection connection = database.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        foreach (var deltaInfo in deltaList)
                        {
                            dal.AddUpdate(deltaInfo.CapitalAccountLogo,
                                          deltaInfo.AvailableCapitalDelta,
                                          deltaInfo.FreezeCapitalTotalDelta,
                                          deltaInfo.TodayOutInCapital,
                                          deltaInfo.HasDoneProfitLossTotalDelta,
                                          database, transaction);
                        }

                        transaction.Commit();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        LogHelper.WriteError(ex.Message, ex);
                    }
                }

                //ͬ�����������������
                if (isSuccess)
                {
                    deltaInfoDal.Truncate();
                }

                /*DataManager.ExecuteInTransaction((database, transaction) =>
                                                     {
                                                         foreach (var deltaInfo in deltaList)
                                                         {
                                                             dal.AddUpdate(deltaInfo.CapitalAccountLogo,
                                                                           deltaInfo.AvailableCapitalDelta,
                                                                           deltaInfo.FreezeCapitalTotalDelta,
                                                                           deltaInfo.TodayOutInCapital,
                                                                           deltaInfo.HasDoneProfitLossTotalDelta,
                                                                           database, transaction);
                                                         }

                                                         //ͬ�����������������
                                                         CleanDeltaTable(database, transaction);
                                                         deltaList = null;
                                                     });*/
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.Message, ex);
            }
        }

        /// <summary>
        /// ��ȡ�����ʽ��ڴ��
        /// </summary>
        /// <returns></returns>
        public List<XH_CapitalAccountTableInfo> GetAllBaseTable()
        {
            XH_CapitalAccountTableDal dal = new XH_CapitalAccountTableDal();
            List<XH_CapitalAccountTableInfo> list = dal.GetAllListArray();

            return list;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="baseTable"></param>
        /// <returns></returns>
        public bool InsertBaseTable(XH_CapitalAccountTableInfo baseTable)
        {
            //��ʵ��
            return true;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="baseTable"></param>
        public void DeleteBaseTable(XH_CapitalAccountTableInfo baseTable)
        {
            //��ʵ��
        }

        /// <summary>
        /// �־û��仯��
        /// </summary>
        /// <param name="delta"></param>
        public void PersistChange(XH_CapitalAccountTable_DeltaInfo delta)
        {
            XH_CapitalAccountTable_DeltaInfoDal deltaInfoDal = new XH_CapitalAccountTable_DeltaInfoDal();
            deltaInfoDal.Add(delta);
        }

        /// <summary>
        /// �־û��ʽ��
        /// </summary>
        /// <param name="baseTable"></param>
        public void PersistBase(XH_CapitalAccountTableInfo baseTable)
        {
            //�ʽ��ʵ��
        }

        /// <summary>
        /// ������־û��ʽ��
        /// </summary>
        /// <param name="baseTable"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void PersistBase(XH_CapitalAccountTableInfo baseTable, Database db, DbTransaction transaction)
        {
            //�ʽ��ʵ��
        }

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="baseTable"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void InsertBaseTableWithTransaction(XH_CapitalAccountTableInfo baseTable, Database db,
                                                   DbTransaction transaction)
        {
            //�ʽ��ʵ��
        }

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="delta"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void PersistChangeWithTransaction(XH_CapitalAccountTable_DeltaInfo delta, Database db,
                                                 DbTransaction transaction)
        {
            XH_CapitalAccountTable_DeltaInfoDal deltaInfoDal = new XH_CapitalAccountTable_DeltaInfoDal();
            deltaInfoDal.Add(delta, db, transaction);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="baseList"></param>
        public void Commit(List<XH_CapitalAccountTableInfo> baseList)
        {
            try
            {
                bool isSuccess = false;
                XH_CapitalAccountTableDal dal = new XH_CapitalAccountTableDal();

                Database database = DatabaseFactory.CreateDatabase();

                using (DbConnection connection = database.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        foreach (var data in baseList)
                        {
                            dal.Update(data.CapitalAccountLogo,
                                       data.AvailableCapital,
                                       data.FreezeCapitalTotal,
                                       data.TodayOutInCapital,
                                       data.HasDoneProfitLossTotal, database,
                                       transaction);
                        }

                        transaction.Commit();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        LogHelper.WriteError(ex.Message, ex);
                    }
                }

                //�ύ���������������
                if (isSuccess)
                {
                    XH_CapitalAccountTable_DeltaInfoDal deltaInfoDal = new XH_CapitalAccountTable_DeltaInfoDal();
                    deltaInfoDal.Truncate();
                }

                /*DataManager.ExecuteInTransaction((database, transaction) =>
                                                     {
                                                         foreach (var data in baseList)
                                                         {
                                                             dal.Update(data.CapitalAccountLogo,
                                                                        data.AvailableCapital,
                                                                        data.FreezeCapitalTotal,
                                                                        data.TodayOutInCapital,
                                                                        data.HasDoneProfitLossTotal, database,
                                                                        transaction);
                                                         }

                                                         //�ύ���������������
                                                         CleanDeltaTable(database, transaction);
                                                     });*/
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.Message, ex);
            }
        }

        /// <summary>
        /// ��ȡָ��Id���ʽ��
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public XH_CapitalAccountTableInfo GetBaseTable(int k)
        {
            XH_CapitalAccountTableDal dal = new XH_CapitalAccountTableDal();
            var data = dal.GetModel(k);

            return data;
        }

        /// <summary>
        /// ����ʽ�仯
        /// </summary>
        /// <param name="database"></param>
        /// <param name="transaction"></param>
        private void CleanDeltaTable(Database database, DbTransaction transaction)
        {
            XH_CapitalAccountTable_DeltaInfoDal deltaInfoDal = new XH_CapitalAccountTable_DeltaInfoDal();
            deltaInfoDal.Delete(database, transaction);
        }
        /// <summary>
        /// �������������ݿ��л�ȡ��Ӧ�Ļ���������
        /// </summary>
        /// <param name="where">����</param>
        /// <returns></returns>
        public XH_CapitalAccountTableInfo GetBaseFromDBByWhere(string where)
        {
            try
            {

                XH_CapitalAccountTableDal dal = new XH_CapitalAccountTableDal();
                string sqlWhere = "";

                if (where.Split('@').Length > 1)
                {
                    sqlWhere = string.Format("UserAccountDistributeLogo='{0}'  and  TradeCurrencyType='{1}' ", where.Split('@')[0], where.Split('@')[1]);
                }
                else
                {
                    sqlWhere = string.Format("UserAccountDistributeLogo='{0}' ", where.Split('@')[0]);
                }
                var data = dal.GetListArray(sqlWhere);
                if (data != null && data.Count > 0)
                {
                    return data[0];
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteError("�������������ݿ��л�ȡ��Ӧ���ֻ��ʽ�����������쳣:" + where + ex.Message, ex);
                return null;
            }
        }
        #endregion

    }
}