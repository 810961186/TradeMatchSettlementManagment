using System;
using System.Collections.Generic;
using System.Data;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;
using ManagementCenter.Model.CommonClass;

namespace ManagementCenter.BLL
{
    /// <summary>
    /// ����������������_�ǽ������� ҵ���߼���CM_NotTradeDate ��ժҪ˵����������뷶Χ:4720-4739
    /// ���ߣ�����ΰ
    /// ����:2008-11-26
    /// �޸ģ�Ҷ��
    /// ʱ�䣺2010-04-07
    /// ��������Ӹ��ݽ��������ͺͷǽ�����ʱ�������Ƿ���ڼ�¼����
    /// </summary>
    public class CM_NotTradeDateBLL
    {
        private readonly ManagementCenter.DAL.CM_NotTradeDateDAL cM_NotTradeDateDAL = new CM_NotTradeDateDAL();

        public CM_NotTradeDateBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return cM_NotTradeDateDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int NotTradeDateID)
        {
            return cM_NotTradeDateDAL.Exists(NotTradeDateID);
        }

        /// <summary>
        /// ��ӷǽ�����
        /// </summary>
        /// <param name="model">�ǽ�����ʵ��</param>
        /// <returns></returns>
        public int AddCMNotTradeDate(ManagementCenter.Model.CM_NotTradeDate model)
        {
            try
            {
                return cM_NotTradeDateDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4720";
                string errMsg = "��ӷǽ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return AppGlobalVariable.INIT_INT;
            }
        }

        /// <summary>
        /// ���·ǽ�����
        /// </summary>
        /// <param name="model">�ǽ�����ʵ��</param>
        /// <returns></returns>
        public bool UpdateCMNotTradeDate(ManagementCenter.Model.CM_NotTradeDate model)
        {
            try
            {
                return cM_NotTradeDateDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4722";
                string errMsg = "�޸ķǽ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        /// <summary>
        /// ɾ���ǽ�����
        /// </summary>
        /// <param name="NotTradeDateID">�ǽ�����ID</param>
        /// <returns></returns>
        public bool DeleteCMNotTradeDate(int NotTradeDateID)
        {
            try
            {
                return cM_NotTradeDateDAL.Delete(NotTradeDateID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4721";
                string errMsg = "ɾ���ǽ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_NotTradeDate GetModel(int NotTradeDateID)
        {
            return cM_NotTradeDateDAL.GetModel(NotTradeDateID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_NotTradeDate GetModelByCache(int NotTradeDateID)
        {
            string CacheKey = "CM_NotTradeDateModel-" + NotTradeDateID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_NotTradeDateDAL.GetModel(NotTradeDateID);
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
            return (ManagementCenter.Model.CM_NotTradeDate) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_NotTradeDateDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region ��ȡ���н���������_�ǽ�������

        /// <summary>
        /// ��ȡ���н���������_�ǽ�������
        /// </summary>
        /// <param name="BourseTypeName">��������������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMNotTradeDate(string BourseTypeName, int pageNo, int pageSize,
                                            out int rowCount)
        {
            try
            {
                CM_NotTradeDateDAL cMNotTradeDateDAL = new CM_NotTradeDateDAL();
                return cMNotTradeDateDAL.GetAllCMNotTradeDate(BourseTypeName, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-4723";
                string errMsg = "��ȡ���н���������_�ǽ�������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ���ݽ���������_�ǽ������ڱ��еĽ���������ID��ȡ��������������

        /// <summary>
        ///���ݽ���������_�ǽ������ڱ��еĽ���������ID��ȡ��������������
        /// </summary>
        /// <returns></returns>
        public DataSet GetCMNotTradeDateBourseTypeName()
        {
            try
            {
                CM_NotTradeDateDAL cMNotTradeDateDAL = new CM_NotTradeDateDAL();
                return cMNotTradeDateDAL.GetCMNotTradeDateBourseTypeName();
            }
            catch (Exception ex)
            {
                string errCode = "GL-4724";
                string errMsg = "���ݽ���������_�ǽ������ڱ��еĽ���������ID��ȡ��������������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// ���ݲ�ѯ������ȡ���еķǽ������ڣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">������ѯ</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_NotTradeDate> GetListArray(string strWhere)
        {
            try
            {
                return cM_NotTradeDateDAL.GetListArray(strWhere);

            }
            catch (Exception ex)
            {
                string errCode = "GL-4725";
                string errMsg = "���ݲ�ѯ������ȡ���еķǽ�������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }

        }
        /// <summary>
        ///  ���ݽ��������ͺͷǽ�����ʱ�������Ƿ���ڼ�¼
        /// </summary>
        /// <param name="BourseTypeID">����������</param>
        /// <param name="NotTradeDay">�ǽ�����ʱ��</param>
        /// <returns></returns>
        public ManagementCenter.Model.CM_NotTradeDate GetNotTradeDate(int BourseTypeID, DateTime NotTradeDay)
        {
            try
            {
                return cM_NotTradeDateDAL.GetNotTradeDate(BourseTypeID,NotTradeDay);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4726";
                string errMsg = "���ݽ��������ͺͷǽ����ղ�ѯʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion  ��Ա����
    }
}