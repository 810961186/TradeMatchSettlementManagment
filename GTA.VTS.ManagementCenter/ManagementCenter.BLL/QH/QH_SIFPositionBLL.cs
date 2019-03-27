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
    /// ��������ָ�ڻ��ֲ����� ҵ���߼���QH_SIFPositionBLL ��ժҪ˵����������뷶Χ:6660-6679
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    public class QH_SIFPositionBLL
    {
        private readonly ManagementCenter.DAL.QH_SIFPositionDAL qH_SIFPositionDAL =
            new ManagementCenter.DAL.QH_SIFPositionDAL();

        public QH_SIFPositionBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_SIFPositionDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int BreedClassID)
        {
            return qH_SIFPositionDAL.Exists(BreedClassID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        //public void Add(ManagementCenter.Model.QH_SIFPosition model)
        //{
        //    qH_SIFPositionDAL.Add(model);
        //}

        #region ��ӹ�ָ�ڻ��ֲ����ƺ�Ʒ��_��ָ�ڻ�_��֤������

        /// <summary>
        /// ��ӹ�ָ�ڻ��ֲ����ƺ�Ʒ��_��ָ�ڻ�_��֤������
        /// </summary>
        /// <param name="qHSIFPosition">��ָ�ڻ��ֲ�����ʵ��</param>
        /// <param name="qHSIFBail">Ʒ��_��ָ�ڻ�_��֤��ʵ��</param>
        /// <returns></returns>
        public bool AddQHSIFPositionAndQHSIFBail(ManagementCenter.Model.QH_SIFPosition qHSIFPosition,
                                                 ManagementCenter.Model.QH_SIFBail qHSIFBail)
        {
            QH_SIFBailDAL qHSIFBailDAL = new QH_SIFBailDAL();
            QH_SIFPositionDAL qHSIFPositionDAL = new QH_SIFPositionDAL();
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
                qHSIFPositionDAL.Add(qHSIFPosition, Tran, db);
                qHSIFBailDAL.Add(qHSIFBail, Tran, db);
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-6660";
                string errMsg = "��ӹ�ָ�ڻ��ֲ����ƺ�Ʒ��_��ָ�ڻ�_��֤������ʧ��!";
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
        /// ����һ������
        /// </summary>
        //public void Update(ManagementCenter.Model.QH_SIFPosition model)
        //{
        //    qH_SIFPositionDAL.Update(model);
        //}

        #region  �޸Ĺ�ָ�ڻ��ֲ����ƺ�Ʒ��_��ָ�ڻ�_��֤������

        /// <summary>
        /// �޸Ĺ�ָ�ڻ��ֲ����ƺ�Ʒ��_��ָ�ڻ�_��֤������
        /// </summary>
        /// <param name="qHSIFPosition">��ָ�ڻ��ֲ�����ʵ��</param>
        /// <param name="qHSIFBail">Ʒ��_��ָ�ڻ�_��֤��ʵ��</param>
        /// <returns></returns>
        public bool UpdateQHSIFPositionAndQHSIFBail(ManagementCenter.Model.QH_SIFPosition qHSIFPosition,
                                                    ManagementCenter.Model.QH_SIFBail qHSIFBail)
        {
            QH_SIFBailDAL qHSIFBailDAL = new QH_SIFBailDAL();
            QH_SIFPositionDAL qHSIFPositionDAL = new QH_SIFPositionDAL();
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
                qHSIFPositionDAL.Update(qHSIFPosition, Tran, db);
                qHSIFBailDAL.Update(qHSIFBail, Tran, db);
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-6661";
                string errMsg = "�޸Ĺ�ָ�ڻ��ֲ����ƺ�Ʒ��_��ָ�ڻ�_��֤������ʧ��!";
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
        /// ɾ��һ������
        /// </summary>
        //public void Delete(int BreedClassID)
        //{
        //    qH_SIFPositionDAL.Delete(BreedClassID);
        //}

        #region ɾ����ָ�ڻ��ֲ����ƺ�Ʒ��_��ָ�ڻ�_��֤������

        /// <summary>
        /// ɾ����ָ�ڻ��ֲ����ƺ�Ʒ��_��ָ�ڻ�_��֤������
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool DeleteQHSIFPositionAndQHSIFBail(int BreedClassID)
        {
            QH_SIFBailDAL qHSIFBailDAL = new QH_SIFBailDAL();
            QH_SIFPositionDAL qHSIFPositionDAL = new QH_SIFPositionDAL();
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
                qHSIFPositionDAL.Delete(BreedClassID, Tran, db);
                qHSIFBailDAL.Delete(BreedClassID, Tran, db);
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-6662";
                string errMsg = "ɾ����ָ�ڻ��ֲ����ƺ�Ʒ��_��ָ�ڻ�_��֤������ʧ��!";
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

        #region  ��ȡ���й�ָ�ڻ��ֲ����ƺ�Ʒ��_��ָ�ڻ�_��֤������

        /// <summary>
        ///  ��ȡ���й�ָ�ڻ��ֲ����ƺ�Ʒ��_��ָ�ڻ�_��֤������
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllQHSIFPositionAndQHSIFBail(string BreedClassName, int pageNo, int pageSize,
                                                       out int rowCount)
        {
            try
            {
                QH_SIFPositionDAL qHSIFPositionDAL = new QH_SIFPositionDAL();
                return qHSIFPositionDAL.GetAllQHSIFPositionAndQHSIFBail(BreedClassName, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-6663";
                string errMsg = "��ȡ���й�ָ�ڻ��ֲ����ƺ�Ʒ��_��ָ�ڻ�_��֤������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_SIFPosition GetModel(int BreedClassID)
        {
            return qH_SIFPositionDAL.GetModel(BreedClassID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_SIFPosition GetModelByCache(int BreedClassID)
        {
            string CacheKey = "QH_SIFPositionModel-" + BreedClassID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_SIFPositionDAL.GetModel(BreedClassID);
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
            return (ManagementCenter.Model.QH_SIFPosition) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_SIFPositionDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }


        /// <summary>
        /// ���ݲ�ѯ������ȡ���еĹ�ָ�ڻ��ֲ����ƣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_SIFPosition> GetListArray(string strWhere)
        {
            try
            {
                return qH_SIFPositionDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6664";
                string errMsg = "���ݲ�ѯ������ȡ���еĹ�ָ�ڻ��ֲ����ƣ���ѯ������Ϊ�գ�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion  ��Ա����
    }
}