using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///���������׷��� ҵ���߼���CM_TradeWayBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class CM_TradeWayBLL
    {
        private readonly ManagementCenter.DAL.CM_TradeWayDAL cM_TradeWayDAL = new ManagementCenter.DAL.CM_TradeWayDAL();

        public CM_TradeWayBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return cM_TradeWayDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int TradeWayID)
        {
            return cM_TradeWayDAL.Exists(TradeWayID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.CM_TradeWay model)
        {
            return cM_TradeWayDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.CM_TradeWay model)
        {
            cM_TradeWayDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int TradeWayID)
        {
            cM_TradeWayDAL.Delete(TradeWayID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_TradeWay GetModel(int TradeWayID)
        {
            return cM_TradeWayDAL.GetModel(TradeWayID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_TradeWay GetModelByCache(int TradeWayID)
        {
            string CacheKey = "CM_TradeWayModel-" + TradeWayID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_TradeWayDAL.GetModel(TradeWayID);
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
            return (ManagementCenter.Model.CM_TradeWay) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_TradeWayDAL.GetList(strWhere);
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
        /// ���ݲ�ѯ������ȡ���еĽ��׷��򣨲�ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_TradeWay> GetListArray(string strWhere)
        {
            return cM_TradeWayDAL.GetListArray(strWhere);
        }

        #endregion  ��Ա����
    }
}