using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///������������ƷƷ������ ҵ���߼���CM_BreedClassTypeBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class CM_BreedClassTypeBLL
    {
        private readonly ManagementCenter.DAL.CM_BreedClassTypeDAL cM_BreedClassTypeDAL =
            new ManagementCenter.DAL.CM_BreedClassTypeDAL();

        public CM_BreedClassTypeBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return cM_BreedClassTypeDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int BreedClassTypeID)
        {
            return cM_BreedClassTypeDAL.Exists(BreedClassTypeID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(ManagementCenter.Model.CM_BreedClassType model)
        {
            cM_BreedClassTypeDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.CM_BreedClassType model)
        {
            cM_BreedClassTypeDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int BreedClassTypeID)
        {
            cM_BreedClassTypeDAL.Delete(BreedClassTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_BreedClassType GetModel(int BreedClassTypeID)
        {
            return cM_BreedClassTypeDAL.GetModel(BreedClassTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_BreedClassType GetModelByCache(int BreedClassTypeID)
        {
            string CacheKey = "CM_BreedClassTypeModel-" + BreedClassTypeID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_BreedClassTypeDAL.GetModel(BreedClassTypeID);
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
            return (ManagementCenter.Model.CM_BreedClassType) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_BreedClassTypeDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ���ݲ�ѯ������ȡ���еĽ�����ƷƷ�����ͣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_BreedClassType> GetListArray(string strWhere)
        {
            return cM_BreedClassTypeDAL.GetListArray(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  ��Ա����
    }
}