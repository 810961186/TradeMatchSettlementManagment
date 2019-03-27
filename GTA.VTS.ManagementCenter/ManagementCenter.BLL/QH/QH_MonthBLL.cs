using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///�������·� ҵ���߼���QH_MonthBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    public class QH_MonthBLL
    {
        private readonly ManagementCenter.DAL.QH_MonthDAL qH_MonthDAL = new ManagementCenter.DAL.QH_MonthDAL();

        public QH_MonthBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_MonthDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int MonthID)
        {
            return qH_MonthDAL.Exists(MonthID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(ManagementCenter.Model.QH_Month model)
        {
            qH_MonthDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.QH_Month model)
        {
            qH_MonthDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int MonthID)
        {
            qH_MonthDAL.Delete(MonthID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_Month GetModel(int MonthID)
        {
            return qH_MonthDAL.GetModel(MonthID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_Month GetModelByCache(int MonthID)
        {
            string CacheKey = "QH_MonthModel-" + MonthID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_MonthDAL.GetModel(MonthID);
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
            return (ManagementCenter.Model.QH_Month) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_MonthDAL.GetList(strWhere);
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
        /// ���ݲ�ѯ������ȡ���е��·� ����ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_Month> GetListArray(string strWhere)
        {
            try
            {
                return qH_MonthDAL.GetListArray(strWhere);
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