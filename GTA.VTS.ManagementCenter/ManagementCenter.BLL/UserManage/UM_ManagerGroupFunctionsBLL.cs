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
    /// ����������Ա����ù��ܱ� ҵ���߼���UM_ManagerGroupFunctionsBLL ��ժҪ˵����
    /// ���ߣ�������
    /// ���ڣ�2008-11-20   
    /// </summary>
    public class UM_ManagerGroupFunctionsBLL
    {
        private readonly ManagementCenter.DAL.UM_ManagerGroupFunctionsDAL dal =
            new ManagementCenter.DAL.UM_ManagerGroupFunctionsDAL();

        public UM_ManagerGroupFunctionsBLL()
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
        public bool Exists(int ManageGroupFuntctiosID)
        {
            return dal.Exists(ManageGroupFuntctiosID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.UM_ManagerGroupFunctions model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.UM_ManagerGroupFunctions model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int ManageGroupFuntctiosID)
        {
            dal.Delete(ManageGroupFuntctiosID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.UM_ManagerGroupFunctions GetModel(int ManageGroupFuntctiosID)
        {
            return dal.GetModel(ManageGroupFuntctiosID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.UM_ManagerGroupFunctions GetModelByCache(int ManageGroupFuntctiosID)
        {
            string CacheKey = "UM_ManagerGroupFunctionsModel-" + ManageGroupFuntctiosID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ManageGroupFuntctiosID);
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
            return (ManagementCenter.Model.UM_ManagerGroupFunctions) objModel;
        }

        /// <summary>
        /// ����������ȡʵ���б�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.UM_ManagerGroupFunctions> GetListArray(string strWhere)
        {
            try
            {
                return dal.GetListArray(strWhere);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
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
        /// ������ԱȨ��
        /// </summary>
        /// <param name="ManagerID"></param>
        /// <param name="Function"></param>
        /// <returns></returns>
        public bool CheckRight(int ManagerID, int Function)
        {
            try
            {
                List<UM_ManagerGroupFunctions> l =
                    dal.GetRightListByManagerID(ManagerID);
                if (l == null) return false;
                foreach (var functions in l)
                {
                    if (functions.FunctionID == Function)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                string errCode = "GL-1305";
                string errMsg = "������ԱȨ��ʧ�ܣ�";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return false;
            }
        }

        /// <summary>
        ///���ݹ���ԱID��ȡȨ���б�
        /// </summary>
        public List<ManagementCenter.Model.UM_ManagerGroupFunctions> GetRightListByManagerID(int ManagerID)
        {
            try
            {
                return dal.GetRightListByManagerID(ManagerID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-1306";
                string errMsg = "���ݹ���ԱID��ȡȨ���б�ʧ�ܣ�";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return null;
            }
        }

        #endregion  ��Ա����
    }
}