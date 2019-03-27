using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///��������Ʊ�ֺ��¼_��Ʊ ҵ���߼���CM_StockMelonStockBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class CM_StockMelonStockBLL
    {
        private readonly ManagementCenter.DAL.CM_StockMelonStockDAL cM_StockMelonStockDAL =
            new ManagementCenter.DAL.CM_StockMelonStockDAL();

        public CM_StockMelonStockBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return cM_StockMelonStockDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int StockMelonStockID)
        {
            return cM_StockMelonStockDAL.Exists(StockMelonStockID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.CM_StockMelonStock model)
        {
            return cM_StockMelonStockDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.CM_StockMelonStock model)
        {
            cM_StockMelonStockDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int StockMelonStockID)
        {
            cM_StockMelonStockDAL.Delete(StockMelonStockID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_StockMelonStock GetModel(int StockMelonStockID)
        {
            return cM_StockMelonStockDAL.GetModel(StockMelonStockID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_StockMelonStock GetModelByCache(int StockMelonStockID)
        {
            string CacheKey = "CM_StockMelonStockModel-" + StockMelonStockID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_StockMelonStockDAL.GetModel(StockMelonStockID);
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
            return (ManagementCenter.Model.CM_StockMelonStock) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_StockMelonStockDAL.GetList(strWhere);
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
        /// ���ݲ�ѯ������ȡ���еĹ�Ʊ�ֺ��¼_��Ʊ����ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_StockMelonStock> GetListArray(string strWhere)
        {
            return cM_StockMelonStockDAL.GetListArray(strWhere);
        }

        #endregion  ��Ա����
    }
}