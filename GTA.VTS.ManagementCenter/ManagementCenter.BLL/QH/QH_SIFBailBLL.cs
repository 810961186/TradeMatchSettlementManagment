using System;
using System.Collections.Generic;
using System.Data;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///������Ʒ��_��ָ�ڻ�_��֤�� ҵ���߼���QH_SIFBailBLL ��ժҪ˵����������뷶Χ:6640-6659
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    public class QH_SIFBailBLL
    {
        private readonly ManagementCenter.DAL.QH_SIFBailDAL qH_SIFBailDAL = new ManagementCenter.DAL.QH_SIFBailDAL();

        public QH_SIFBailBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_SIFBailDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int BreedClassID)
        {
            return qH_SIFBailDAL.Exists(BreedClassID);
        }

       /// <summary>
       /// ���Ʒ��_��ָ�ڻ�_��֤��
       /// </summary>
        /// <param name="model">Ʒ��_��ָ�ڻ�_��֤��ʵ��</param>
        public void Add(ManagementCenter.Model.QH_SIFBail model)
        {
            try
            {
                qH_SIFBailDAL.Add(model);

            }
            catch (Exception ex)
            {
                string errCode = "GL-6640";
                string errMsg = "���Ʒ��_��ָ�ڻ�_��֤��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return;
            }
        }

       /// <summary>
       /// ����Ʒ��_��ָ�ڻ�_��֤��
       /// </summary>
       /// <param name="model">Ʒ��_��ָ�ڻ�_��֤��ʵ��</param>
        public void Update(ManagementCenter.Model.QH_SIFBail model)
        {
            try
            {
                qH_SIFBailDAL.Update(model);

            }
            catch (Exception ex)
            {
                string errCode = "GL-6641";
                string errMsg = "����Ʒ��_��ָ�ڻ�_��֤��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return;
            }
        }

        /// <summary>
        /// ����Ʒ��IDɾ��Ʒ��_��ָ�ڻ�_��֤��
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        public void Delete(int BreedClassID)
        {
            try
            {
                qH_SIFBailDAL.Delete(BreedClassID);

            }
            catch (Exception ex)
            {
                string errCode = "GL-6642";
                string errMsg = "����Ʒ��IDɾ��Ʒ��_��ָ�ڻ�_��֤��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return;
            }
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_SIFBail GetModel(int BreedClassID)
        {
            return qH_SIFBailDAL.GetModel(BreedClassID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_SIFBail GetModelByCache(int BreedClassID)
        {
            string CacheKey = "QH_SIFBailModel-" + BreedClassID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_SIFBailDAL.GetModel(BreedClassID);
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
            return (ManagementCenter.Model.QH_SIFBail) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_SIFBailDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        /// <summary>
        /// ���ݲ�ѯ������ȡ���е�Ʒ��_��ָ�ڻ�_��֤�𣨲�ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_SIFBail> GetListArray(string strWhere)
        {
            try
            {
                return qH_SIFBailDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6643";
                string errMsg = "���ݲ�ѯ������ȡ���е�Ʒ��_��ָ�ڻ�_��֤�𣨲�ѯ������Ϊ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion  ��Ա����
    }
}