using System;
using System.Collections.Generic;
using System.Data;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    /// ����: �ʺ����� ҵ���߼���UM_AccountTypeBLL ��ժҪ˵����
    /// ���ߣ�������  �޸�:����ΰ
    /// ���ڣ�2008-11-20  �޸�����:2009-11-03
    /// </summary>
    public class UM_AccountTypeBLL
    {
        private readonly ManagementCenter.DAL.UM_AccountTypeDAL dal = new ManagementCenter.DAL.UM_AccountTypeDAL();

        public UM_AccountTypeBLL()
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
        public bool Exists(int AccountTypeID)
        {
            return dal.Exists(AccountTypeID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(ManagementCenter.Model.UM_AccountType model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.UM_AccountType model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int AccountTypeID)
        {
            dal.Delete(AccountTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.UM_AccountType GetModel(int AccountTypeID)
        {
            return dal.GetModel(AccountTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.UM_AccountType GetModelByCache(int AccountTypeID)
        {
            string CacheKey = "UM_AccountTypeModel-" + AccountTypeID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AccountTypeID);
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
            return (ManagementCenter.Model.UM_AccountType) objModel;
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
        /// ��ȡ�����ʻ�����
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.UM_AccountType> GetListArray(string strWhere)
        {
            //return dal.GetListArray(string.Empty);
            try
            {
                return dal.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-";
                string errMsg = "���ݲ�ѯ������ȡ�˻�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }

        }
        #endregion  ��Ա����
    }
}