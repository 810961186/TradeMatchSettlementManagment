using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///�������ǵ�ͣ��������� ҵ���߼���QH_HighLowStopScopeTypeBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    public class QH_HighLowStopScopeTypeBLL
    {
        private readonly ManagementCenter.DAL.QH_HighLowStopScopeTypeDAL qH_HighLowStopScopeTypeDAL =
            new ManagementCenter.DAL.QH_HighLowStopScopeTypeDAL();

        public QH_HighLowStopScopeTypeBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_HighLowStopScopeTypeDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int HighLowStopScopeID)
        {
            return qH_HighLowStopScopeTypeDAL.Exists(HighLowStopScopeID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(ManagementCenter.Model.QH_HighLowStopScopeType model)
        {
            qH_HighLowStopScopeTypeDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.QH_HighLowStopScopeType model)
        {
            qH_HighLowStopScopeTypeDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int HighLowStopScopeID)
        {
            qH_HighLowStopScopeTypeDAL.Delete(HighLowStopScopeID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_HighLowStopScopeType GetModel(int HighLowStopScopeID)
        {
            return qH_HighLowStopScopeTypeDAL.GetModel(HighLowStopScopeID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_HighLowStopScopeType GetModelByCache(int HighLowStopScopeID)
        {
            string CacheKey = "QH_HighLowStopScopeTypeModel-" + HighLowStopScopeID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_HighLowStopScopeTypeDAL.GetModel(HighLowStopScopeID);
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
            return (ManagementCenter.Model.QH_HighLowStopScopeType) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_HighLowStopScopeTypeDAL.GetList(strWhere);
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
        /// ���ݲ�ѯ������ȡ���е��ǵ�ͣ��������ͣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_HighLowStopScopeType> GetListArray(string strWhere)
        {
            try
            {
                return qH_HighLowStopScopeTypeDAL.GetListArray(strWhere);
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