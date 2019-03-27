using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///���������׻������� ҵ���߼���CM_CurrencyTypeBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class CM_CurrencyTypeBLL
    {
        private readonly ManagementCenter.DAL.CM_CurrencyTypeDAL cM_CurrencyTypeDAL =
            new ManagementCenter.DAL.CM_CurrencyTypeDAL();

        public CM_CurrencyTypeBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return cM_CurrencyTypeDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int CurrencyTypeID)
        {
            return cM_CurrencyTypeDAL.Exists(CurrencyTypeID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(ManagementCenter.Model.CM_CurrencyType model)
        {
            cM_CurrencyTypeDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.CM_CurrencyType model)
        {
            cM_CurrencyTypeDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int CurrencyTypeID)
        {
            cM_CurrencyTypeDAL.Delete(CurrencyTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_CurrencyType GetModel(int CurrencyTypeID)
        {
            return cM_CurrencyTypeDAL.GetModel(CurrencyTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_CurrencyType GetModelByCache(int CurrencyTypeID)
        {
            string CacheKey = "CM_CurrencyTypeModel-" + CurrencyTypeID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_CurrencyTypeDAL.GetModel(CurrencyTypeID);
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
            return (ManagementCenter.Model.CM_CurrencyType) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_CurrencyTypeDAL.GetList(strWhere);
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
        /// ���ݲ�ѯ������ȡ���еĽ��׻������ͣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_CurrencyType> GetListArray(string strWhere)

        {
            return cM_CurrencyTypeDAL.GetListArray(strWhere);
        }

        /// <summary>
        /// ����Ʒ���ҵ�����
        /// </summary>
        /// <param name="BreedClassID"></param>
        /// <returns></returns>
        public ManagementCenter.Model.CM_CurrencyBreedClassType GetCurrencyByBreedClassID(int BreedClassID)
        {
            return cM_CurrencyTypeDAL.GetCurrencyByBreedClassID(BreedClassID);
        }

        /// <summary>
        /// ��ȡ����Ʒ�ֺͱ��ֵĶ�Ӧ��ϵ
        /// </summary>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_CurrencyBreedClassType> GetListCurrencyBreedClass()
        {
            return cM_CurrencyTypeDAL.GetListCurrencyBreedClass();
        }

        #endregion  ��Ա����
    }
}