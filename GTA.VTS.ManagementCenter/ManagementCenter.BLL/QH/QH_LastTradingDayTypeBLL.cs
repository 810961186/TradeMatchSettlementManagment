using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///����������������� ҵ���߼���QH_LastTradingDayTypeBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    public class QH_LastTradingDayTypeBLL
    {
        private readonly ManagementCenter.DAL.QH_LastTradingDayTypeDAL qH_LastTradingDayTypeDAL =
            new ManagementCenter.DAL.QH_LastTradingDayTypeDAL();

        public QH_LastTradingDayTypeBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_LastTradingDayTypeDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int LastTradingDayTypeID)
        {
            return qH_LastTradingDayTypeDAL.Exists(LastTradingDayTypeID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(ManagementCenter.Model.QH_LastTradingDayType model)
        {
            qH_LastTradingDayTypeDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.QH_LastTradingDayType model)
        {
            qH_LastTradingDayTypeDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int LastTradingDayTypeID)
        {
            qH_LastTradingDayTypeDAL.Delete(LastTradingDayTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_LastTradingDayType GetModel(int LastTradingDayTypeID)
        {
            return qH_LastTradingDayTypeDAL.GetModel(LastTradingDayTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_LastTradingDayType GetModelByCache(int LastTradingDayTypeID)
        {
            string CacheKey = "QH_LastTradingDayTypeModel-" + LastTradingDayTypeID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_LastTradingDayTypeDAL.GetModel(LastTradingDayTypeID);
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
            return (ManagementCenter.Model.QH_LastTradingDayType) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_LastTradingDayTypeDAL.GetList(strWhere);
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
        /// ���ݲ�ѯ������ȡ���е�����������ͣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_LastTradingDayType> GetListArray(string strWhere)
        {
            try
            {
                return qH_LastTradingDayTypeDAL.GetListArray(strWhere);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        #endregion  ��Ա����
    }
}