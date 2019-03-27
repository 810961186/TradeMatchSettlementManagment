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
    /// ��̨����ҵ���߼���CT_CounterBLL���������3100-3120
    /// ���ߣ�������
    /// ���ڣ�2008-12-20
    /// </summary>
    public class CT_CounterBLL
    {
        private readonly ManagementCenter.DAL.CT_CounterDAL dal = new ManagementCenter.DAL.CT_CounterDAL();

        public CT_CounterBLL()
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
        public bool Exists(int CouterID)
        {
            return dal.Exists(CouterID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.CT_Counter model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.CT_Counter model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int CouterID)
        {
            dal.Delete(CouterID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CT_Counter GetModel(int CouterID)
        {
            return dal.GetModel(CouterID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CT_Counter GetModelByCache(int CouterID)
        {
            string CacheKey = "CT_CounterModel-" + CouterID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(CouterID);
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
            return (ManagementCenter.Model.CT_Counter) objModel;
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
        /// ��ȡ���еĹ�̨�б�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CT_Counter> GetListArray(string strWhere)
        {
            return dal.GetListArray(strWhere);
        }

        /// <summary>
        /// ��̨��ҳ��ѯʧ��
        /// </summary>
        /// <param name="counterQueryEntity">��ѯʵ��</param>
        /// <param name="pageNo">��ѯҳ��</param>
        /// <param name="pageSize">ÿҳ��С</param>
        /// <param name="rowCount">�ܼ�¼��</param>
        /// <returns></returns>
        public DataSet GetPagingCounter(ManagementCenter.Model.CT_Counter counterQueryEntity, int pageNo, int pageSize,
                                        out int rowCount)
        {
            try
            {
                return dal.GetPagingCounter(counterQueryEntity, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = 0;
                string errCode = "GL-3100";
                string errMsg = "��̨��ҳ��ѯʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        /// <summary>
        /// ��������̨����
        /// </summary>
        public void CenterTestConnection()
        {
            try
            {
                List<ManagementCenter.Model.CT_Counter> l_Counter = dal.GetListArray(string.Empty);
                foreach (CT_Counter CT in l_Counter)
                {
                    bool flag = false;
                    try
                    {
                        flag = ServiceIn.AccountManageServiceProxy.GetInstance().TestConnection(CT.IP, (int)CT.AccountServicePort,
                                                                                                CT.AccountServiceName);
                    }
                    catch (VTException ex)
                    {
                        //д�쳣��־
                        flag = false;
                        GTA.VTS.Common.CommonUtility.LogHelper.WriteError(ex.Message, ex);
                    }
                    if (CT.State == null)
                    {
                        if (flag)
                        {
                            CT.State = (int) ManagementCenter.Model.CommonClass.Types.StateEnum.ConnSuccess;
                        }
                        else
                        {
                            CT.State = (int) ManagementCenter.Model.CommonClass.Types.StateEnum.ConnDefeat;
                        }
                        dal.Update(CT);
                    }
                    else if (CT.State == (int) ManagementCenter.Model.CommonClass.Types.StateEnum.ConnDefeat)
                    {
                        if (flag)
                        {
                            CT.State = (int) ManagementCenter.Model.CommonClass.Types.StateEnum.ConnSuccess;
                            dal.Update(CT);
                        }
                    }
                    else
                    {
                        if (!flag)
                        {
                            CT.State = (int) ManagementCenter.Model.CommonClass.Types.StateEnum.ConnDefeat;
                            dal.Update(CT);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errCode = "GL-3101";
                string errMsg = "��������̨����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
            }
        }


        /// <summary>
        /// ��������̨����
        /// </summary>
        public bool TestCenterConnection()
        {
            try
            {
                List<ManagementCenter.Model.CT_Counter> l_Counter = dal.GetListArray(string.Empty);
                foreach (CT_Counter CT in l_Counter)
                {
                    try
                    {
                        ServiceIn.AccountManageServiceProxy.GetInstance().TestConnection(CT.IP, (int)CT.AccountServicePort,
                                                                                                CT.AccountServiceName);
                    }
                    catch (VTException ee)
                    {
                        GTA.VTS.Common.CommonUtility.LogHelper.WriteError(ee.Message, ee);
                        return false;
                    }
                   
                }
                return true;
            }
            catch (Exception ex)
            {
                string errCode = "GL-3101";
                string errMsg = "��������̨����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }
        #endregion  ��Ա����
    }
}