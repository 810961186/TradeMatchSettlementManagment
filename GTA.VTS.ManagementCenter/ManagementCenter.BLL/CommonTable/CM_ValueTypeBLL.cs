using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///���������׹���_ȡֵ���� ҵ���߼���CM_ValueTypeBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class CM_ValueTypeBLL
    {
        private readonly ManagementCenter.DAL.CM_ValueTypeDAL cM_ValueTypeDAL =
            new ManagementCenter.DAL.CM_ValueTypeDAL();

        public CM_ValueTypeBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return cM_ValueTypeDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int ValueTypeID)
        {
            return cM_ValueTypeDAL.Exists(ValueTypeID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.CM_ValueType model)
        {
            return cM_ValueTypeDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.CM_ValueType model)
        {
            cM_ValueTypeDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int ValueTypeID)
        {
            cM_ValueTypeDAL.Delete(ValueTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_ValueType GetModel(int ValueTypeID)
        {
            return cM_ValueTypeDAL.GetModel(ValueTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_ValueType GetModelByCache(int ValueTypeID)
        {
            string CacheKey = "CM_ValueTypeModel-" + ValueTypeID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_ValueTypeDAL.GetModel(ValueTypeID);
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
            return (ManagementCenter.Model.CM_ValueType) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_ValueTypeDAL.GetList(strWhere);
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
        /// ���ݲ�ѯ������ȡ���еĽ��׹���_ȡֵ���ͣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_ValueType> GetListArray(string strWhere)
        {
            return cM_ValueTypeDAL.GetListArray(strWhere);
        }

        #endregion  ��Ա����
    }
}