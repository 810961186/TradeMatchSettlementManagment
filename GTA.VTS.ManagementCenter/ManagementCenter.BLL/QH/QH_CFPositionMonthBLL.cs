using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///�������ڻ�_Ʒ��_�����·� ҵ���߼���QH_CFPositionMonthBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    public class QH_CFPositionMonthBLL
    {
        private readonly ManagementCenter.DAL.QH_CFPositionMonthDAL qH_CFPositionMonthDAL =
            new ManagementCenter.DAL.QH_CFPositionMonthDAL();

        public QH_CFPositionMonthBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_CFPositionMonthDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int DeliveryMonthTypeID)
        {
            return qH_CFPositionMonthDAL.Exists(DeliveryMonthTypeID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(ManagementCenter.Model.QH_CFPositionMonth model)
        {
            qH_CFPositionMonthDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.QH_CFPositionMonth model)
        {
            qH_CFPositionMonthDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int DeliveryMonthTypeID)
        {
            qH_CFPositionMonthDAL.Delete(DeliveryMonthTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_CFPositionMonth GetModel(int DeliveryMonthTypeID)
        {
            return qH_CFPositionMonthDAL.GetModel(DeliveryMonthTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_CFPositionMonth GetModelByCache(int DeliveryMonthTypeID)
        {
            string CacheKey = "QH_CFPositionMonthModel-" + DeliveryMonthTypeID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_CFPositionMonthDAL.GetModel(DeliveryMonthTypeID);
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
            return (ManagementCenter.Model.QH_CFPositionMonth) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_CFPositionMonthDAL.GetList(strWhere);
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
        /// ���ݲ�ѯ������ȡ���е��ڻ�_Ʒ��_�����·ݣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_CFPositionMonth> GetListArray(string strWhere)
        {
            try
            {
                return qH_CFPositionMonthDAL.GetListArray(strWhere);
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