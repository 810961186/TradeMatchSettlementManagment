using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    /// ������������Ʊ���� ҵ���߼���ZFInfo ��ժҪ˵����
    /// ���ߣ�������
    /// ���ڣ�2008-11-20
    /// </summary>
    public class ZFInfoBLL
    {
        private readonly ManagementCenter.DAL.ZFInfoDAL dal = new ZFInfoDAL();

        public ZFInfoBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string stkcd)
        {
            return dal.Exists(stkcd);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(ManagementCenter.Model.ZFInfo model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.ZFInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(string stkcd)
        {
            dal.Delete(stkcd);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.ZFInfo GetModel(string stkcd)
        {
            return dal.GetModel(stkcd);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.ZFInfo GetModelByCache(string stkcd)
        {
            string CacheKey = "ZFInfoModel-" + stkcd;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(stkcd);
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
            return (ManagementCenter.Model.ZFInfo) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
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
        /// ��ȡʵ���б�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.ZFInfo> GetListArray(string strWhere)
        {
            return dal.GetListArray(strWhere);
        }

        #endregion  ��Ա����
    }
}