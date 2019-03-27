using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;
using ManagementCenter.Model.CommonClass;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///�������ڻ�_Ʒ��_���׹��� ҵ���߼���QH_FuturesTradeRulesBLL ��ժҪ˵���� ������뷶Χ:6000-6019
    ///���ߣ�����ΰ
    ///���ڣ�2008-11-27
    /// </summary>
    public class QH_FuturesTradeRulesBLL
    {
        private readonly ManagementCenter.DAL.QH_FuturesTradeRulesDAL qH_FuturesTradeRulesDAL =
            new ManagementCenter.DAL.QH_FuturesTradeRulesDAL();

        public QH_FuturesTradeRulesBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_FuturesTradeRulesDAL.GetMaxId();
        }

        #region ����Ʒ��ID���ж��ڻ����׹����Ƿ��Ѵ���
        /// <summary>
        /// ����Ʒ��ID���ж��ڻ����׹����Ƿ��Ѵ���
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool ExistsFuturesTradeRules(int BreedClassID)
        {
            try
            {
                QH_FuturesTradeRulesDAL qHFuturesTradeRulesDAL = new QH_FuturesTradeRulesDAL();
                return qHFuturesTradeRulesDAL.Exists(BreedClassID);

            }
            catch (Exception ex)
            {
                string errCode = "GL-6000";
                string errMsg = "����Ʒ��ID���ж��ڻ����׹����Ƿ��Ѵ���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }
        #endregion

        /// <summary>
        /// ����ڻ����׹���
        /// </summary>
        /// <param name="model">�ڻ�_Ʒ��_���׹���ʵ��</param>
        /// <returns></returns>
        public bool AddFuturesTradeRules(ManagementCenter.Model.QH_FuturesTradeRules model)
        {
            try
            {
                QH_FuturesTradeRulesDAL qHFuturesTradeRulesDAL = new QH_FuturesTradeRulesDAL();
                return qH_FuturesTradeRulesDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6001";
                string errMsg = "����ڻ����׹���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        /// <summary>
        /// �����ڻ����׹���
        /// </summary>
        /// <param name="model">�ڻ�_Ʒ��_���׹���ʵ��</param>
        /// <returns></returns>
        public bool UpdateFuturesTradeRules(ManagementCenter.Model.QH_FuturesTradeRules model)
        {
            try
            {
                QH_FuturesTradeRulesDAL qHFuturesTradeRulesDAL = new QH_FuturesTradeRulesDAL();
                return qH_FuturesTradeRulesDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6002";
                string errMsg = "�����ڻ����׹���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        /// <summary>
        /// ɾ���ڻ�_Ʒ��_���׹���
        /// </summary>
        /// <param name="BreedClassID">Ʒ�ֱ�ʶ</param>
        public bool DeleteFuturesTradeRules(int BreedClassID)
        {
            try
            {
                QH_FuturesTradeRulesDAL qHFuturesTradeRulesDAL = new QH_FuturesTradeRulesDAL();
                return qH_FuturesTradeRulesDAL.Delete(BreedClassID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6003";
                string errMsg = "ɾ���ڻ�_Ʒ��_���׹���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #region ����Ʒ�ֱ�ʶ,ɾ���ڻ�Ʒ�ֽ��׹���(������ر�ȫ��ɾ��)

        /// <summary>
        ///����Ʒ�ֱ�ʶ,ɾ���ڻ�Ʒ�ֽ��׹���(������ر�ȫ��ɾ��) 
        /// </summary>
        /// <param name="BreedClassID">Ʒ�ֱ�ʶ</param>
        /// <returns></returns>
        public bool DeleteFuturesTradeRulesAboutAll(int BreedClassID)
        {
            QH_FuturesTradeRulesDAL qHFuturesTradeRulesDAL=new QH_FuturesTradeRulesDAL();
            QH_AgreementDeliveryMonthDAL qH_AgreementDeliveryMonthDAL=new QH_AgreementDeliveryMonthDAL();
            QH_ConsignQuantumDAL qH_ConsignQuantumDAL=new QH_ConsignQuantumDAL();
            QH_SingleRequestQuantityDAL qH_SingleRequestQuantityDAL=new QH_SingleRequestQuantityDAL();
            QH_LastTradingDayDAL qH_LastTradingDayDAL=new QH_LastTradingDayDAL();

            DbConnection Conn = null;
            Database db = DatabaseFactory.CreateDatabase();
            Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            DbTransaction Tran = Conn.BeginTransaction();
            try
            {
                int ConsignQuantumID = AppGlobalVariable.INIT_INT;
                int LastTradingDayID = AppGlobalVariable.INIT_INT;
                QH_FuturesTradeRules qHFuturesTradeRules = new QH_FuturesTradeRules();
                qHFuturesTradeRules = qHFuturesTradeRulesDAL.GetModel(BreedClassID);
                if (qHFuturesTradeRules != null)
                {
                    if (!string.IsNullOrEmpty(qHFuturesTradeRules.ConsignQuantumID.ToString()))
                    {
                        ConsignQuantumID = Convert.ToInt32(qHFuturesTradeRules.ConsignQuantumID);
                    }
                    if (!string.IsNullOrEmpty(qHFuturesTradeRules.LastTradingDayID.ToString()))
                    {
                        LastTradingDayID = Convert.ToInt32(qHFuturesTradeRules.LastTradingDayID);
                    }
                    if (ConsignQuantumID != AppGlobalVariable.INIT_INT)
                    {
                        if (!qH_SingleRequestQuantityDAL.DeleteSingleRQByConsignQuantumID(ConsignQuantumID, Tran, db))
                        {
                            Tran.Rollback();
                            return false;
                        }

                    }


                    List<Model.QH_AgreementDeliveryMonth> qHAgreementDeliveryM =
                        qH_AgreementDeliveryMonthDAL.GetListArray(string.Format("BreedClassID={0}", BreedClassID), Tran,
                                                                  db);
                    foreach (Model.QH_AgreementDeliveryMonth MonthID in qHAgreementDeliveryM)
                    {

                        if (!qH_AgreementDeliveryMonthDAL.Delete((Int32) MonthID.MonthID, BreedClassID)) //?����
                        {
                            Tran.Rollback();
                            return false;
                        }
                    }

                    if (!qHFuturesTradeRulesDAL.Delete(BreedClassID, Tran, db))
                    {
                        Tran.Rollback();
                        return false;
                    }

                    if (ConsignQuantumID != AppGlobalVariable.INIT_INT)
                    {
                        if (!qH_ConsignQuantumDAL.Delete(ConsignQuantumID, Tran, db))
                        {
                            Tran.Rollback();
                            return false;
                        }
                    }
                    if (LastTradingDayID != AppGlobalVariable.INIT_INT)
                    {
                        if (!qH_LastTradingDayDAL.Delete(LastTradingDayID, Tran, db))
                        {
                            Tran.Rollback();
                            return false;
                        }
                    }
                }
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-6004";
                string errMsg = " ����Ʒ�ֱ�ʶ,ɾ���ڻ�Ʒ�ֽ��׹���(������ر�ȫ��ɾ��)ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        #endregion

        /// <summary>
        /// ����Ʒ��ID����ȡ�ڻ����׹������ʵ��
        /// </summary>
        /// <param name="BreedClassID"></param>
        /// <returns></returns>
        public ManagementCenter.Model.QH_FuturesTradeRules GetFuturesTradeRulesModel(int BreedClassID)
        {
            try
            {
                QH_FuturesTradeRulesDAL qHFuturesTradeRulesDAL = new QH_FuturesTradeRulesDAL();
                return qHFuturesTradeRulesDAL.GetModel(BreedClassID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6005";
                string errMsg = " ����Ʒ��ID����ȡ�ڻ����׹������ʵ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_FuturesTradeRules GetModelByCache(int BreedClassID)
        {
            string CacheKey = "QH_FuturesTradeRulesModel-" + BreedClassID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_FuturesTradeRulesDAL.GetModel(BreedClassID);
                    if (objModel != null)
                    {
                        int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache),
                                                      TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (ManagementCenter.Model.QH_FuturesTradeRules) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_FuturesTradeRulesDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ���ݲ�ѯ������ȡ���е��ڻ�_Ʒ��_���׹��򣨲�ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_FuturesTradeRules> GetListArray(string strWhere)
        {
            try
            {
                return qH_FuturesTradeRulesDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6006";
                string errMsg = " ���ݲ�ѯ������ȡ���е��ڻ�_Ʒ��_���׹��򣨲�ѯ������Ϊ�գ�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #region ��ȡ�����ڻ�_Ʒ��_���׹���

        /// <summary>
        /// ��ȡ�����ڻ�_Ʒ��_���׹���
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllFuturesTradeRules(string BreedClassName, int pageNo, int pageSize,
                                               out int rowCount)
        {
            try
            {
                QH_FuturesTradeRulesDAL qHFuturesTradeRulesDAL = new QH_FuturesTradeRulesDAL();
                return qH_FuturesTradeRulesDAL.GetAllFuturesTradeRules(BreedClassName, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-6007";
                string errMsg = "��ȡ�����ڻ�_Ʒ��_���׹���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #endregion  ��Ա����

        #region �����ڻ�_Ʒ��_���׹�����е�Ʒ�ֱ�ʶ��ȡƷ������

        /// <summary>
        /// �����ڻ�_Ʒ��_���׹�����е�Ʒ�ֱ�ʶ��ȡƷ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetQHBreedClassNameByBreedClassID()
        {
            try
            {
                QH_FuturesTradeRulesDAL qHFuturesTradeRulesDAL = new QH_FuturesTradeRulesDAL();
                return qH_FuturesTradeRulesDAL.GetQHBreedClassNameByBreedClassID();
            }
            catch (Exception ex)
            {
                string errCode = "GL-6008";
                string errMsg = "�����ڻ�_Ʒ��_���׹�����е�Ʒ�ֱ�ʶ��ȡƷ������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion
    }
}