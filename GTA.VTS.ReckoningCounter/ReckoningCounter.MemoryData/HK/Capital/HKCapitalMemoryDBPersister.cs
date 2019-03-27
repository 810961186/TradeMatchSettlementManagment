#region Using Namespace

using System;
using System.Collections.Generic;
using System.Data.Common;
using GTA.VTS.Common.CommonUtility;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReckoningCounter.DAL.Data.HK;
using ReckoningCounter.Entity.Model.HK;
using ReckoningCounter.MemoryData.Interface;

#endregion

namespace ReckoningCounter.MemoryData.HK.Capital
{
    /// <summary>
    /// �۹��ڴ��ʽ�����ݿ�־û���
    /// ���ߣ�����
    /// </summary>
    public class HKCapitalMemoryDBPersister :
        IMemoryPersister<int, HK_CapitalAccountInfo, HK_CapitalAccount_DeltaInfo>
    {
        #region Implementation of IMemoryPersister<HKCapitalMemoryTable,HK_CapitalAccount_DeltaInfo>

        /// <summary>
        /// ͬ���������ݿ�
        /// </summary>
        public void SyncChangeToBase()
        {
            HK_CapitalAccount_DeltaDal deltaInfoDal = new HK_CapitalAccount_DeltaDal();
            var deltaList = deltaInfoDal.GetAllSum();

            if (deltaList == null)
                return;

            if (deltaList.Count == 0)
                return;

            try
            {
                bool isSuccess = false;
                Database database = DatabaseFactory.CreateDatabase();
                HK_CapitalAccountDal dal = new HK_CapitalAccountDal();

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
        /// ��ȡ�����ʽ�ʵ��
        /// </summary>
        /// <returns></returns>
        public List<HK_CapitalAccountInfo> GetAllBaseTable()
        {
            HK_CapitalAccountDal dal = new HK_CapitalAccountDal();
            List<HK_CapitalAccountInfo> list = dal.GetAllListArray();

            return list;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="baseTable"></param>
        /// <returns></returns>
        public bool InsertBaseTable(HK_CapitalAccountInfo baseTable)
        {
            //��ʵ��
            return true;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="baseTable"></param>
        public void DeleteBaseTable(HK_CapitalAccountInfo baseTable)
        {
            //��ʵ��
        }

        /// <summary>
        /// �־û�
        /// </summary>
        /// <param name="baseTable"></param>
        public void PersistBase(HK_CapitalAccountInfo baseTable)
        {
            //�ʽ��ʵ��
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseTable"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void PersistBase(HK_CapitalAccountInfo baseTable, Database db, DbTransaction transaction)
        {
            //�ʽ��ʵ��
        }

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="baseTable"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void InsertBaseTableWithTransaction(HK_CapitalAccountInfo baseTable, Database db,
                                                   DbTransaction transaction)
        {
            //�ʽ��ʵ��
        }

        /// <summary>
        /// ���������ʽ��˻�����
        /// </summary>
        /// <param name="baseList">�۹��ʽ��˻�ʵ��</param>
        public void Commit(List<HK_CapitalAccountInfo> baseList)
        {
            try
            {
                bool isSuccess = false;
                HK_CapitalAccountDal dal = new HK_CapitalAccountDal();

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
                    HK_CapitalAccount_DeltaDal deltaInfoDal = new HK_CapitalAccount_DeltaDal();
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
        /// ��ȡָ��id�ĸ۹��ʽ��˻�
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public HK_CapitalAccountInfo GetBaseTable(int k)
        {
            HK_CapitalAccountDal dal = new HK_CapitalAccountDal();
            var data = dal.GetModel(k);

            return data;
        }

        /// <summary>
        /// �־û��ʽ�仯
        /// </summary>
        /// <param name="delta">�ʽ�仯ʵ��</param>
        public void PersistChange(HK_CapitalAccount_DeltaInfo delta)
        {
            HK_CapitalAccount_DeltaDal deltaInfoDal = new HK_CapitalAccount_DeltaDal();
            deltaInfoDal.Add(delta);
        }

        /// <summary>
        /// ������־û��ʽ�仯
        /// </summary>
        /// <param name="delta">�ʽ�仯ʵ��</param>
        /// <param name="db">���ݿ����</param>
        /// <param name="transaction">�������</param>
        public void PersistChangeWithTransaction(HK_CapitalAccount_DeltaInfo delta, Database db,
                                                 DbTransaction transaction)
        {
            HK_CapitalAccount_DeltaDal deltaInfoDal = new HK_CapitalAccount_DeltaDal();
            deltaInfoDal.Add(delta, db, transaction);
        }
        /// <summary>
        /// ����ʽ�仯��¼
        /// </summary>
        /// <param name="database">���ݿ����</param>
        /// <param name="transaction">�������</param>
        private void CleanDeltaTable(Database database, DbTransaction transaction)
        {
            HK_CapitalAccount_DeltaDal deltaInfoDal = new HK_CapitalAccount_DeltaDal();
            deltaInfoDal.Delete(database, transaction);
        }
        /// <summary>
        /// �������������ݿ��л�ȡ��Ӧ�Ļ���������
        /// </summary>
        /// <param name="where">����</param>
        /// <returns></returns>
        public HK_CapitalAccountInfo GetBaseFromDBByWhere(string where)
        {
            try
            {
                HK_CapitalAccountDal dal = new HK_CapitalAccountDal();
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
                LogHelper.WriteError("�������������ݿ��л�ȡ��Ӧ�ĸ۹��ʽ�����������쳣:" + where + ex.Message, ex);
                return null;
            }
        }
        #endregion



    }
}