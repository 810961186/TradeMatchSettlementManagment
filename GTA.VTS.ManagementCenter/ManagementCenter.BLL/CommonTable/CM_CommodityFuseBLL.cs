using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;
using ManagementCenter.Model.CommonClass;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///�������ɽ�����Ʒ_�۶ϱ� ҵ���߼���CM_CommodityFuseBLL ��ժҪ˵����������뷶Χ:6700-6719
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class CM_CommodityFuseBLL
    {
        private readonly CM_CommodityFuseDAL cM_CommodityFuseDAL =
            new CM_CommodityFuseDAL();

        #region  ��Ա����

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool ExistsCommodityCode(string CommodityCode)
        {
            try
            {
                return cM_CommodityFuseDAL.Exists(CommodityCode);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6700";
                string errMsg = "�жϿɽ�����Ʒ_�۶��еļ�¼�Ƿ����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #region ��ӿɽ�����Ʒ_�۶�

        /// <summary>
        /// ��ӿɽ�����Ʒ_�۶�
        /// </summary>
        /// <param name="model">�ɽ�����Ʒ_�۶�ʵ��</param>
        /// <returns></returns>
        public bool AddCMCommodityFuse(CM_CommodityFuse model)
        {
            try
            {
                return cM_CommodityFuseDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6701";
                string errMsg = " ��ӿɽ�����Ʒ_�۶�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region �޸Ŀɽ�����Ʒ_�۶�

        /// <summary>
        /// �޸Ŀɽ�����Ʒ_�۶�
        /// </summary>
        /// <param name="model">�ɽ�����Ʒ_�۶�ʵ��</param>
        /// <returns></returns>
        public bool UpdateCMCommodityFuse(CM_CommodityFuse model)
        {
            try
            {
                return cM_CommodityFuseDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6702";
                string errMsg = " �޸Ŀɽ�����Ʒ_�۶�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ɾ���ɽ�����Ʒ_�۶�

        /// <summary>
        /// ɾ���ɽ�����Ʒ_�۶�
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <returns></returns>
        public bool DeleteCMCommodityFuse(string CommodityCode)
        {
            try
            {
                return cM_CommodityFuseDAL.Delete(CommodityCode);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6703";
                string errMsg = "ɾ���ɽ�����Ʒ_�۶�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ɾ���ɽ�����Ʒ_�۶�(ͬʱɾ��ͬһ��Ʒ������۶�_ʱ��α�ʶ���еļ�¼)

        /// <summary>
        /// ɾ���ɽ�����Ʒ_�۶�(ͬʱɾ��ͬһ��Ʒ������۶�_ʱ��α�ʶ���еļ�¼)
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <returns></returns>
        public bool DeleteCMCommodityFuseAbout(string CommodityCode)
        {
            CM_CommodityFuseDAL cMCommodityFuseDAL = new CM_CommodityFuseDAL();
            CM_FuseTimesectionDAL cMFuseTimesectionDAL = new CM_FuseTimesectionDAL();
            DbConnection Conn = null;
            Database db = DatabaseFactory.CreateDatabase();
            Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            DbTransaction Tran = Conn.BeginTransaction();
            try
            {
                if (!string.IsNullOrEmpty(CommodityCode))
                {
                    if (cMFuseTimesectionDAL.DeleteByCommodityCode(CommodityCode, Tran, db))
                    {
                        if (cMCommodityFuseDAL.Delete(CommodityCode, Tran, db))
                        {
                            Tran.Commit();
                            return true;
                        }
                    }
                }
                Tran.Rollback();
                return false;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-6704";
                string errMsg = "ɾ���ɽ�����Ʒ_�۶�(ͬʱɾ��ͬһ��Ʒ������۶�_ʱ��α�ʶ���еļ�¼)ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public CM_CommodityFuse GetModel(string CommodityCode)
        {
            return cM_CommodityFuseDAL.GetModel(CommodityCode);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public CM_CommodityFuse GetModelByCache(string CommodityCode)
        {
            string CacheKey = "CM_CommodityFuseModel-" + CommodityCode;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_CommodityFuseDAL.GetModel(CommodityCode);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache),
                                           TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (CM_CommodityFuse) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_CommodityFuseDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ���ݲ�ѯ������ȡ���еĿɽ�����Ʒ_�۶ϱ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<CM_CommodityFuse> GetListArray(string strWhere)
        {
            return cM_CommodityFuseDAL.GetListArray(strWhere);
        }

        #region ��ȡ���пɽ�����Ʒ_�۶�

        /// <summary>
        /// ��ȡ���пɽ�����Ʒ_�۶�
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMCommodityFuse(string CommodityCode, int pageNo, int pageSize,
                                             out int rowCount)
        {
            try
            {
                CM_CommodityFuseDAL cMCommodityFuseDAL = new CM_CommodityFuseDAL();
                return cMCommodityFuseDAL.GetAllCMCommodityFuse(CommodityCode, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-6705";
                string errMsg = "��ȡ���пɽ�����Ʒ_�۶�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #endregion  ��Ա����
    }
}