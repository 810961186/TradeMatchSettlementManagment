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
    ///�������ڻ�_�ֲ����� ҵ���߼���QH_PositionLimitValueBLL ��ժҪ˵����������뷶Χ:6620-6639
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    public class QH_PositionLimitValueBLL
    {
        private readonly ManagementCenter.DAL.QH_PositionLimitValueDAL qH_PositionLimitValueDAL =
            new ManagementCenter.DAL.QH_PositionLimitValueDAL();

        public QH_PositionLimitValueBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_PositionLimitValueDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int PositionLimitValueID)
        {
            return qH_PositionLimitValueDAL.Exists(PositionLimitValueID);
        }

        #region ���(��Ʒ)�ڻ�_�ֲ�����

        /// <summary>
        /// ���(��Ʒ)�ڻ�_�ֲ�����
        /// </summary>
        /// <param name="model">(��Ʒ)�ڻ�_�ֲ�����ʵ��</param>
        /// <returns></returns>
        public int AddQHPositionLimitValue(ManagementCenter.Model.QH_PositionLimitValue model)
        {
            try
            {
                QH_PositionLimitValueDAL qHPositionLimitValueDAL = new QH_PositionLimitValueDAL();
                return qHPositionLimitValueDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6620";
                string errMsg = "���(��Ʒ)�ڻ�_�ֲ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return AppGlobalVariable.INIT_INT;
            }
        }

        #endregion

        #region �޸�(��Ʒ)�ڻ�_�ֲ�����

        /// <summary>
        /// �޸�(��Ʒ)�ڻ�_�ֲ�����
        /// </summary>
        /// <param name="model">(��Ʒ)�ڻ�_�ֲ�����ʵ��</param>
        /// <returns></returns>
        public bool UpdateQHPositionLimitValue(ManagementCenter.Model.QH_PositionLimitValue model)
        {
            try
            {
                QH_PositionLimitValueDAL qHPositionLimitValueDAL = new QH_PositionLimitValueDAL();
                return qHPositionLimitValueDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6621";
                string errMsg = "�޸�(��Ʒ)�ڻ�_�ֲ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region  ɾ��(��Ʒ)�ڻ�_�ֲ�����

        /// <summary>
        /// ɾ��(��Ʒ)�ڻ�_�ֲ�����
        /// </summary>
        /// <param name="PositionLimitValueID">(��Ʒ)�ڻ�_�ֲ�����ID</param>
        /// <returns></returns>
        public bool DeleteQHPositionLimitValue(int PositionLimitValueID)
        {
            try
            {
                QH_PositionLimitValueDAL qHPositionLimitValueDAL = new QH_PositionLimitValueDAL();
                return qHPositionLimitValueDAL.Delete(PositionLimitValueID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6622";
                string errMsg = "ɾ��(��Ʒ)�ڻ�_�ֲ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region �����ڻ�-�ֲ����Ʊ�ʶ����ʵ��

        /// <summary>
        /// �����ڻ�-�ֲ����Ʊ�ʶ����ʵ��
        /// </summary>
        /// <param name="PositionLimitValueID">�ڻ�-�ֲ����Ʊ�ʶ</param>
        /// <returns></returns>
        public ManagementCenter.Model.QH_PositionLimitValue GetQHPositionLimitValueModel(int PositionLimitValueID)
        {
            try
            {
                return qH_PositionLimitValueDAL.GetModel(PositionLimitValueID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6623";
                string errMsg = "�����ڻ�-�ֲ����Ʊ�ʶ����ʵ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_PositionLimitValue GetModelByCache(int PositionLimitValueID)
        {
            string CacheKey = "QH_PositionLimitValueModel-" + PositionLimitValueID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_PositionLimitValueDAL.GetModel(PositionLimitValueID);
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
            return (ManagementCenter.Model.QH_PositionLimitValue) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_PositionLimitValueDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region ��ȡ����(��Ʒ)�ڻ�_�ֲ�����

        /// <summary>
        ///��ȡ����(��Ʒ)�ڻ�_�ֲ����� 
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="DeliveryMonthTypeID">�����·����ͱ�ʶ</param>
        /// <param name="PositionBailTypeID">�ֲֺͱ�֤��������ͱ�ʶ</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllQHPositionLimitValue(string BreedClassName, int DeliveryMonthTypeID, int PositionBailTypeID,
                                                  int pageNo, int pageSize,
                                                  out int rowCount)
        {
            try
            {
                QH_PositionLimitValueDAL qHPositionLimitValueDAL = new QH_PositionLimitValueDAL();
                return qHPositionLimitValueDAL.GetAllQHPositionLimitValue(BreedClassName, DeliveryMonthTypeID,
                                                                          PositionBailTypeID, pageNo, pageSize,
                                                                          out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-6624";
                string errMsg = "��ȡ����(��Ʒ)�ڻ�_�ֲ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// ���ݲ�ѯ������ȡ���е��ڻ�_�ֲ����ƣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_PositionLimitValue> GetListArray(string strWhere)
        {
            try
            {
                return qH_PositionLimitValueDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6625";
                string errMsg = "���ݲ�ѯ������ȡ���е��ڻ�_�ֲ����ƣ���ѯ������Ϊ�գ�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion  ��Ա����
    }
}