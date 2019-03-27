using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    /// ������Ȩ֤�ǵ����۸� ҵ���߼���XH_RightHightLowPrices ��ժҪ˵����
    /// ���ߣ�����ΰ
    /// ���ڣ�2008-11-26
    /// </summary>
    public class XH_RightHightLowPricesBLL
    {
        private readonly ManagementCenter.DAL.XH_RightHightLowPricesDAL dal = new XH_RightHightLowPricesDAL();

        public XH_RightHightLowPricesBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int RightHightLowPriceID)
        {
            return dal.Exists(RightHightLowPriceID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(ManagementCenter.Model.XH_RightHightLowPrices model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.XH_RightHightLowPrices model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int RightHightLowPriceID)
        {
            dal.Delete(RightHightLowPriceID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_RightHightLowPrices GetModel(int RightHightLowPriceID)
        {
            return dal.GetModel(RightHightLowPriceID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.XH_RightHightLowPrices GetModelByCache(int RightHightLowPriceID)
        {
            string CacheKey = "XH_RightHightLowPricesModel-" + RightHightLowPriceID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(RightHightLowPriceID);
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
            return (ManagementCenter.Model.XH_RightHightLowPrices) objModel;
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
        /// ��ȡ���е�Ȩ֤�ǵ����۸񣨲�ѯ������Ϊ�գ�
        /// <param name="strWhere">��ѯ����</param>
        /// </summary>
        /// <returns></returns>
        public List<ManagementCenter.Model.XH_RightHightLowPrices> GetListArray(string strWhere)
        {
            XH_RightHightLowPricesDAL xH_RightHightLowPricesDAL = new XH_RightHightLowPricesDAL();
            return xH_RightHightLowPricesDAL.GetListArray(strWhere);
        }

        #endregion  ��Ա����
    }
}