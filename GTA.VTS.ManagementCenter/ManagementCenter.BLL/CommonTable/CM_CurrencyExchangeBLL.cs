using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///����:����֮��һ����� ҵ���߼���CM_CurrencyExchangeBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class CM_CurrencyExchangeBLL
    {
        private readonly ManagementCenter.DAL.CM_CurrencyExchangeDAL cM_CurrencyExchangeDAL =
            new ManagementCenter.DAL.CM_CurrencyExchangeDAL();

        public CM_CurrencyExchangeBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return cM_CurrencyExchangeDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int CurrencyExchangeID)
        {
            return cM_CurrencyExchangeDAL.Exists(CurrencyExchangeID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.CM_CurrencyExchange model)
        {
            return cM_CurrencyExchangeDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.CM_CurrencyExchange model)
        {
            cM_CurrencyExchangeDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int CurrencyExchangeID)
        {
            cM_CurrencyExchangeDAL.Delete(CurrencyExchangeID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_CurrencyExchange GetModel(int CurrencyExchangeID)
        {
            return cM_CurrencyExchangeDAL.GetModel(CurrencyExchangeID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_CurrencyExchange GetModelByCache(int CurrencyExchangeID)
        {
            string CacheKey = "CM_CurrencyExchangeModel-" + CurrencyExchangeID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_CurrencyExchangeDAL.GetModel(CurrencyExchangeID);
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
            return (ManagementCenter.Model.CM_CurrencyExchange) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_CurrencyExchangeDAL.GetList(strWhere);
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
        /// ���ݲ�ѯ������ȡ���еı���֮��һ����ͣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_CurrencyExchange> GetListArray(string strWhere)
        {
            try
            {
                return cM_CurrencyExchangeDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                GTA.VTS.Common.CommonUtility.LogHelper.WriteError(ex.Message, ex);
                return null;
                //throw;
            }
        }

        #endregion  ��Ա����
    }
}