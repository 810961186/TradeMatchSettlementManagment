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
    ///���������׹���ί���� ҵ���߼���QH_ConsignQuantumBLL ��ժҪ˵����������뷶Χ:6020-6039
    ///���ߣ�����ΰ
    ///���ڣ�2008-11-27
    /// </summary>
    public class QH_ConsignQuantumBLL
    {
        private readonly ManagementCenter.DAL.QH_ConsignQuantumDAL qH_ConsignQuantumDAL =
            new ManagementCenter.DAL.QH_ConsignQuantumDAL();

        public QH_ConsignQuantumBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_ConsignQuantumDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int ConsignQuantumID)
        {
            return qH_ConsignQuantumDAL.Exists(ConsignQuantumID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        //public int Add(ManagementCenter.Model.QH_ConsignQuantum model)
        //{
        //    return qH_ConsignQuantumDAL.Add(model);
        //}
        ///// <summary>
        ///// ����һ������
        ///// </summary>
        //public void Update(ManagementCenter.Model.QH_ConsignQuantum model)
        //{
        //    qH_ConsignQuantumDAL.Update(model);
        //}
        ///// <summary>
        ///// ɾ��һ������
        ///// </summary>
        //public void Delete(int ConsignQuantumID)
        //{
        //    qH_ConsignQuantumDAL.Delete(ConsignQuantumID);
        //}
        /// <summary>
        /// ��ӽ��׹���͵������ί����
        /// </summary>
        /// <param name="qHConsignQuantum">���׹���ʵ��</param>
        /// <param name="qHSingleRequestQuantityl">�������ί����ʵ��</param>
        /// /// <param name="qHSingleRequestQuantity2">�������ί����ʵ��</param>
        /// <returns></returns>
        public int AddQHConsignQuantumAndSingle(QH_ConsignQuantum qHConsignQuantum,
                                                QH_SingleRequestQuantity qHSingleRequestQuantityl,
                                                QH_SingleRequestQuantity qHSingleRequestQuantity2)
        {
            QH_ConsignQuantumDAL qHConsignQuantumDAL = new QH_ConsignQuantumDAL();
            QH_SingleRequestQuantityDAL qHSingleRequestQuantityDAL = new QH_SingleRequestQuantityDAL();
            DbConnection Conn = null;
            Database db = DatabaseFactory.CreateDatabase();
            Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            DbTransaction Tran = Conn.BeginTransaction();

            int consignQuantumID = AppGlobalVariable.INIT_INT;
            try
            {
                consignQuantumID = qHConsignQuantumDAL.Add(qHConsignQuantum, Tran, db);

                if (consignQuantumID != AppGlobalVariable.INIT_INT)
                {
                    if (qHSingleRequestQuantityl != null)
                    {
                        qHSingleRequestQuantityl.ConsignQuantumID = consignQuantumID;
                        qHSingleRequestQuantityDAL.Add(qHSingleRequestQuantityl, Tran, db);
                    }

                    if (qHSingleRequestQuantity2 != null)
                    {
                        qHSingleRequestQuantity2.ConsignQuantumID = consignQuantumID;
                        qHSingleRequestQuantityDAL.Add(qHSingleRequestQuantity2, Tran, db);
                    }

                    Tran.Commit();
                }

                return consignQuantumID;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-6020";
                string errMsg = "��ӽ��׹���͵������ί����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return consignQuantumID;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        #region  ���½��׹���͵������ί����

        /// <summary>
        /// ���½��׹���͵������ί����
        /// </summary>
        /// <param name="qHConsignQuantum">���׹���ʵ��</param>
        /// <param name="qHSingleRequestQuantity1">�������ί����ʵ��</param>
        /// <param name="qHSingleRequestQuantity2">�������ί����ʵ��</param>
        /// <returns></returns>
        public bool UpdateQHConsignQuantumAndSingle(QH_ConsignQuantum qHConsignQuantum,
                                                    QH_SingleRequestQuantity qHSingleRequestQuantity1,
                                                    QH_SingleRequestQuantity qHSingleRequestQuantity2)
        {
            QH_ConsignQuantumDAL qHConsignQuantumDAL = new QH_ConsignQuantumDAL();
            QH_SingleRequestQuantityDAL qHSingleRequestQuantityDAL = new QH_SingleRequestQuantityDAL();
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
                qHConsignQuantumDAL.Update(qHConsignQuantum);
                if (qHSingleRequestQuantity1 != null)
                {
                    qHSingleRequestQuantityDAL.Update(qHSingleRequestQuantity1);
                }
                if (qHSingleRequestQuantity2 != null)
                {
                    qHSingleRequestQuantityDAL.Update(qHSingleRequestQuantity2);
                }
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-6021";
                string errMsg = "���½��׹���͵������ί����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        #endregion

        #region ���ݽ��׹���ί������ʶɾ�����׹���͵������ί����

        /// <summary>
        /// ���ݽ��׹���ί������ʶɾ�����׹���͵������ί����
        /// </summary>
        /// <param name="ConsignQuantumID">���׹���ί����ID</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <param name="falg">����Ƿ�ʹ�����ⲿ����</param>
        /// <returns></returns>
        public bool DeleteQHConsignQuantumAndSingle(int ConsignQuantumID, DbTransaction tran, Database db, bool falg)
        {
            QH_ConsignQuantumDAL qHConsignQuantumDAL = new QH_ConsignQuantumDAL();
            QH_SingleRequestQuantityDAL qHSingleRequestQuantityDAL = new QH_SingleRequestQuantityDAL();
            DbConnection Conn = null;
            try
            {
                //������������
                if (db == null && tran == null)
                {
                    db = DatabaseFactory.CreateDatabase();
                    Conn = db.CreateConnection();
                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }
                    tran = Conn.BeginTransaction();
                }
                qHSingleRequestQuantityDAL.DeleteSingleRQByConsignQuantumID(ConsignQuantumID, tran, db);
                qHConsignQuantumDAL.Delete(ConsignQuantumID, tran, db);
                if (!falg)
                {
                    tran.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (!falg)
                {
                    tran.Rollback();
                }
                string errCode = "GL-6022";
                string errMsg = "���ݽ��׹���ί������ʶɾ�����׹���͵������ί����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    if (!falg) Conn.Close();
                }
            }
        }

        #endregion

        #region ���ݽ��׹���ί������ʶɾ�����׹���͵������ί����

        /// <summary>
        /// ���ݽ��׹���ί������ʶɾ�����׹���͵������ί����
        /// </summary>
        /// <param name="ConsignQuantumID">���׹���ί����ID</param>
        /// <returns></returns>
        public bool DeleteQHConsignQuantumAndSingle(int ConsignQuantumID)
        {
            try
            {
                return DeleteQHConsignQuantumAndSingle(ConsignQuantumID, null, null, false);

            }
            catch (Exception ex)
            {
                string errCode = "GL-6025";
                string errMsg = "���ݽ��׹���ί������ʶɾ�����׹���͵������ί����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ���ݽ��׹���ί����ID����ȡί����ʵ��

        /// <summary>
        /// ���ݽ��׹���ί����ID����ȡί����ʵ��
        /// </summary>
        /// <param name="ConsignQuantumID">���׹���ί����ID</param>
        /// <returns></returns>
        public ManagementCenter.Model.QH_ConsignQuantum GetQHConsignQuantumModel(int ConsignQuantumID)
        {
            try
            {
                QH_ConsignQuantumDAL qHConsignQuantumDAL = new QH_ConsignQuantumDAL();
                return qHConsignQuantumDAL.GetModel(ConsignQuantumID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6023";
                string errMsg = "���ݽ��׹���ί����ID����ȡί����ʵ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_ConsignQuantum GetModelByCache(int ConsignQuantumID)
        {
            string CacheKey = "QH_ConsignQuantumModel-" + ConsignQuantumID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_ConsignQuantumDAL.GetModel(ConsignQuantumID);
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
            return (ManagementCenter.Model.QH_ConsignQuantum)objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_ConsignQuantumDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ���ݲ�ѯ������ȡ���еĽ��׹���ί��������ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_ConsignQuantum> GetListArray(string strWhere)
        {
            try
            {
                return qH_ConsignQuantumDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6024";
                string errMsg = "���ݲ�ѯ������ȡ���еĽ��׹���ί��������ѯ������Ϊ�գ�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion  ��Ա����
    }
}