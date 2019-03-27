using System;
using System.Collections.Generic;
using System.Data;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;
using ManagementCenter.Model.CommonClass;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///������Ʒ��_�ڻ�_���׷��� ҵ���߼���QH_FutureCostsBLL ��ժҪ˵����������뷶Χ:6300-6319
    ///���ߣ�����ΰ
    ///���ڣ�2008-11-22
    /// </summary>
    public class QH_FutureCostsBLL
    {
        private readonly ManagementCenter.DAL.QH_FutureCostsDAL qH_FutureCostsDAL =
            new ManagementCenter.DAL.QH_FutureCostsDAL();

        public QH_FutureCostsBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_FutureCostsDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int BreedClassID)
        {
            try
            {
                return qH_FutureCostsDAL.Exists(BreedClassID);

            }
            catch (Exception ex)
            {
                string errCode = "GL-6300";
                string errMsg = "�ж��Ƿ�����ڻ����ü�¼ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #region ���Ʒ��_�ڻ�_���׷���

        /// <summary>
        /// ���Ʒ��_�ڻ�_���׷���
        /// </summary>
        /// <param name="model">Ʒ��_�ڻ�_���׷���ʵ��</param>
        /// <returns></returns>
        public bool AddQHFutureCosts(ManagementCenter.Model.QH_FutureCosts model)
        {
            try
            {
                return qH_FutureCostsDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6301";
                string errMsg = "���Ʒ��_�ڻ�_���׷���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region �޸�Ʒ��_�ڻ�_���׷���

        /// <summary>
        /// �޸�Ʒ��_�ڻ�_���׷���
        /// </summary>
        /// <param name="model">Ʒ��_�ڻ�_���׷���ʵ��</param>
        /// <returns></returns>
        public bool UpdateQHFutureCosts(ManagementCenter.Model.QH_FutureCosts model)
        {
            try
            {
                return qH_FutureCostsDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6302";
                string errMsg = "�޸�Ʒ��_�ڻ�_���׷���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ɾ��Ʒ��_�ڻ�_���׷���

        /// <summary>
        /// ɾ��Ʒ��_�ڻ�_���׷���
        /// </summary>
        /// <param name="BreedClassID">Ʒ��_�ڻ�_���׷���ID</param>
        /// <returns></returns>
        public bool DeleteQHFutureCosts(int BreedClassID)
        {
            try
            {
                return qH_FutureCostsDAL.Delete(BreedClassID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6303";
                string errMsg = "ɾ��Ʒ��_�ڻ�_���׷���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_FutureCosts GetModel(int BreedClassID)
        {
            return qH_FutureCostsDAL.GetModel(BreedClassID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_FutureCosts GetModelByCache(int BreedClassID)
        {
            string CacheKey = "QH_FutureCostsModel-" + BreedClassID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_FutureCostsDAL.GetModel(BreedClassID);
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
            return (ManagementCenter.Model.QH_FutureCosts) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_FutureCostsDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ���ݲ�ѯ������ȡ���е�Ʒ��_�ڻ�_���׷��ã���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_FutureCosts> GetListArray(string strWhere)
        {
            try
            {
                return qH_FutureCostsDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6304";
                string errMsg = "���ݲ�ѯ������ȡ���е�Ʒ��_�ڻ�_���׷��ã���ѯ������Ϊ�գ�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #region ��ȡ����Ʒ��_�ڻ�_���׷���

        /// <summary>
        ///��ȡ����Ʒ��_�ڻ�_���׷���
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllQHFutureCosts(string BreedClassName,
                                           int pageNo, int pageSize,
                                           out int rowCount)
        {
            try
            {
                QH_FutureCostsDAL qHFutureCostsDAL = new QH_FutureCostsDAL();
                return qHFutureCostsDAL.GetAllQHFutureCosts(BreedClassName, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-6305";
                string errMsg = "��ȡ����Ʒ��_�ڻ�_���׷���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #endregion  ��Ա����
    }
}