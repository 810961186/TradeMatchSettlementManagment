using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    /// ��������Ʊ�ֺ��¼_�ֽ� ҵ���߼���CM_StockMelonCashBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class CM_StockMelonCashBLL
    {
        private readonly ManagementCenter.DAL.CM_StockMelonCashDAL cM_StockMelonCashDAL =
            new ManagementCenter.DAL.CM_StockMelonCashDAL();

        public CM_StockMelonCashBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return cM_StockMelonCashDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int StockMelonCuttingCashID)
        {
            return cM_StockMelonCashDAL.Exists(StockMelonCuttingCashID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.CM_StockMelonCash model)
        {
            return cM_StockMelonCashDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.CM_StockMelonCash model)
        {
            cM_StockMelonCashDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int StockMelonCuttingCashID)
        {
            cM_StockMelonCashDAL.Delete(StockMelonCuttingCashID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_StockMelonCash GetModel(int StockMelonCuttingCashID)
        {
            return cM_StockMelonCashDAL.GetModel(StockMelonCuttingCashID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_StockMelonCash GetModelByCache(int StockMelonCuttingCashID)
        {
            string CacheKey = "CM_StockMelonCashModel-" + StockMelonCuttingCashID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_StockMelonCashDAL.GetModel(StockMelonCuttingCashID);
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
            return (ManagementCenter.Model.CM_StockMelonCash) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_StockMelonCashDAL.GetList(strWhere);
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
        /// ���ݲ�ѯ������ȡ���еĹ�Ʊ�ֺ��¼_�ֽ𣨲�ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_StockMelonCash> GetListArray(string strWhere)
        {
            return cM_StockMelonCashDAL.GetListArray(strWhere);
        }

        #endregion  ��Ա����
    }
}