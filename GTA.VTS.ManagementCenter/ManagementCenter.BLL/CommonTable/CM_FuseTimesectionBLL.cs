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
    ///�������۶�_ʱ��α�ʶ ҵ���߼���CM_FuseTimesection ��ժҪ˵����������뷶Χ:6720-6739
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class CM_FuseTimesectionBLL
    {
        private readonly ManagementCenter.DAL.CM_FuseTimesectionDAL cM_FuseTimesectionDAL = new CM_FuseTimesectionDAL();

        public CM_FuseTimesectionBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return cM_FuseTimesectionDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int TimesectionID)
        {
            return cM_FuseTimesectionDAL.Exists(TimesectionID);
        }

        #region ����۶�_ʱ��α�ʶ

        /// <summary>
        /// ����۶�_ʱ��α�ʶ
        /// </summary>
        /// <param name="model">�۶�_ʱ��α�ʶʵ��</param>
        /// <returns></returns>
        public int AddCMFuseTimesection(ManagementCenter.Model.CM_FuseTimesection model)
        {
            try
            {
                return cM_FuseTimesectionDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6720";
                string errMsg = "����۶�_ʱ��α�ʶʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return AppGlobalVariable.INIT_INT;
            }
        }

        #endregion

        #region ����۶�_ʱ��α�ʶ ����

        /// <summary>
        /// ����۶�_ʱ��α�ʶ
        /// </summary>
        /// <param name="model">�۶�_ʱ��α�ʶʵ��</param>
        /// <param name="msg">���ش�������ʾ��Ϣ</param>
        /// <returns></returns>
        public int AddCMFuseTimesection(ManagementCenter.Model.CM_FuseTimesection model, ref string msg)
        {
            try
            {
                string m_msg = string.Empty;
                if (model.EndTime < model.StartTime)
                {
                    msg += "��ʼʱ�䲻�ܴ��ڽ�ֹʱ��!" + "\t\n";
                }
                else if (model.EndTime < model.StartTime)
                {
                    msg += "��ֹʱ�䲻��С����ʼʱ��!" + "\t\n";
                }
                if (model.StartTime == model.EndTime)
                {
                    msg += "��ʼ�ͽ�ֹʱ�䲻�����!" + "\t\n";
                }
                m_msg = msg;
                if (!string.IsNullOrEmpty(m_msg))
                {
                    return AppGlobalVariable.INIT_INT;
                }
                return cM_FuseTimesectionDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6721";
                string errMsg = "����۶�_ʱ��α�ʶʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return AppGlobalVariable.INIT_INT;
            }
        }

        #endregion

        #region �޸��۶�_ʱ��α�ʶ

        /// <summary>
        /// �޸��۶�_ʱ��α�ʶ
        /// </summary>
        /// <param name="model">�۶�_ʱ��α�ʶʵ��</param>
        /// <returns></returns>
        public bool UpdateCMFuseTimesection(ManagementCenter.Model.CM_FuseTimesection model)
        {
            try
            {
                return cM_FuseTimesectionDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6722";
                string errMsg = "�޸��۶�_ʱ��α�ʶʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region �޸��۶�_ʱ��α�ʶ ����

        /// <summary>
        /// �޸��۶�_ʱ��α�ʶ ����
        /// </summary>
        /// <param name="model">�۶�_ʱ��α�ʶʵ��</param>
        /// <param name="msg">���ش�������ʾ��Ϣ</param>
        /// <returns></returns>
        public bool UpdateCMFuseTimesection(ManagementCenter.Model.CM_FuseTimesection model, ref string msg)
        {
            try
            {
                string m_msg = string.Empty;
                if (model.StartTime > model.EndTime)
                {
                    msg += "��ʼʱ�䲻�ܴ��ڽ�ֹʱ��!" + "\t\n";
                }
                else if (model.EndTime < model.StartTime)
                {
                    msg += "��ֹʱ�䲻��С����ʼʱ��!" + "\t\n";
                }
                if (model.StartTime == model.EndTime)
                {
                    msg += "��ʼ�ͽ�ֹʱ�䲻�����!" + "\t\n";
                }
                m_msg = msg;
                if (!string.IsNullOrEmpty(m_msg))
                {
                    return false;
                }
                return cM_FuseTimesectionDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6723";
                string errMsg = "�޸��۶�_ʱ��α�ʶʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ɾ���۶�_ʱ��α�ʶ

        /// <summary>
        /// ɾ���۶�_ʱ��α�ʶ
        /// </summary>
        /// <param name="TimesectionID">�۶�_ʱ��α�ʶ</param>
        /// <returns></returns>
        public bool DeleteCMFuseTimesection(int TimesectionID)
        {
            try
            {
                return cM_FuseTimesectionDAL.Delete(TimesectionID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6724";
                string errMsg = "ɾ���۶�_ʱ��α�ʶʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ������Ʒ�����ȡ�����۶�_ʱ��α�ʶ

        /// <summary>
        /// ������Ʒ�����ȡ�۶�_ʱ��α�ʶ
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <returns></returns>
        public DataSet GetCMFuseTimesectionByCommodityCode(string CommodityCode)
        {
            try
            {
                CM_FuseTimesectionDAL cMFuseTimesectionDAL = new CM_FuseTimesectionDAL();
                return cMFuseTimesectionDAL.GetCMFuseTimesectionByCommodityCode(CommodityCode);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6725";
                string errMsg = "������Ʒ�����ȡ�����۶�_ʱ��α�ʶʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_FuseTimesection GetModel(int TimesectionID)
        {
            return cM_FuseTimesectionDAL.GetModel(TimesectionID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_FuseTimesection GetModelByCache(int TimesectionID)
        {
            string CacheKey = "CM_FuseTimesectionModel-" + TimesectionID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_FuseTimesectionDAL.GetModel(TimesectionID);
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
            return (ManagementCenter.Model.CM_FuseTimesection) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_FuseTimesectionDAL.GetList(strWhere);
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
        /// ����������ȡ���е��۶�_ʱ��α�ʶ(������Ϊ��)
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_FuseTimesection> GetListArray(string strWhere)
        {
            return cM_FuseTimesectionDAL.GetListArray(strWhere);
        }

        #endregion  ��Ա����
    }
}