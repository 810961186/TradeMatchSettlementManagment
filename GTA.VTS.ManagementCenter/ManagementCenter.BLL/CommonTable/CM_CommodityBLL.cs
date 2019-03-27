using System;
using System.Data;
using System.Data.Common;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;
using System.Collections.Generic;
using ManagementCenter.Model.CommonClass;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ManagementCenter.BLL
{
    /// <summary>
    /// ����:������Ʒ ҵ���߼���CM_CommodityBLL ��ժҪ˵����  ������뷶Χ:4300-4319
    ///���ߣ�����ΰ
    ///����:2008-11-21 �޸�:2009-08-13
    /// </summary>
    public class CM_CommodityBLL
    {
        private readonly ManagementCenter.DAL.CM_CommodityDAL cM_CommodityDAL =
            new ManagementCenter.DAL.CM_CommodityDAL();

        public CM_CommodityBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string CommodityCode)
        {
            return cM_CommodityDAL.Exists(CommodityCode);
        }

        #region ��ӽ�����Ʒ

        /// <summary>
        /// ��ӽ�����Ʒ
        /// </summary>
        /// <param name="model">������Ʒʵ��</param>
        /// <returns></returns>
        public bool AddCMCommodity(ManagementCenter.Model.CM_Commodity model)
        {
            try
            {
                return cM_CommodityDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4300";
                string errMsg = "��ӽ�����Ʒʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ���½�����Ʒ

        /// <summary>
        /// ���½�����Ʒ
        /// </summary>
        /// <param name="model">������Ʒʵ��</param>
        /// <returns></returns>
        public bool UpdateCMCommodity(ManagementCenter.Model.CM_Commodity model)
        {
            try
            {
                return cM_CommodityDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4302";
                string errMsg = "���½�����Ʒʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ������Ʒ����ɾ��������Ʒ����ر�ļ�¼ͬʱɾ����

        /// <summary>
        /// ������Ʒ����ɾ��������Ʒ����ر�ļ�¼ͬʱɾ����
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool DeleteCMCommodity(string CommodityCode, int BreedClassID)
        {
            CM_CommodityDAL cMCommodityDAL = new CM_CommodityDAL();
            RC_TradeCommodityAssignDAL TradeCommodityAssignDAL = new RC_TradeCommodityAssignDAL();
            CM_CommodityFuseDAL cMCommodityFuseDAL = new CM_CommodityFuseDAL();
            CM_FuseTimesectionDAL cMFuseTimesectionDAL = new CM_FuseTimesectionDAL();
            CM_BreedClassBLL cM_BreedClassBLL = new CM_BreedClassBLL();
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
                int breedClassTypeID = AppGlobalVariable.INIT_INT;//Ʒ������ID
                List<CM_BreedClass> cM_BreedClassList =
                  cM_BreedClassBLL.GetListArray(string.Format("BreedClassID={0}", BreedClassID));
                if (cM_BreedClassList.Count > 0)
                {
                    CM_BreedClass cM_BreedClass = cM_BreedClassList[0];
                    if (cM_BreedClass != null)
                    {
                        breedClassTypeID = Convert.ToInt32(cM_BreedClass.BreedClassTypeID);
                    }
                }
                if (breedClassTypeID == (int)GTA.VTS.Common.CommonObject.Types.BreedClassTypeEnum.StockIndexFuture)
                {
                    if (!string.IsNullOrEmpty(CommodityCode))
                    {
                        if (!cMFuseTimesectionDAL.DeleteByCommodityCode(CommodityCode, Tran, db))
                        {
                            Tran.Rollback();
                            return false;
                        }
                        else
                        {
                            if (!cMCommodityFuseDAL.Delete(CommodityCode, Tran, db))
                            {
                                Tran.Rollback();
                                return false;
                            }
                        }
                    }
                    TradeCommodityAssignDAL.DeleteByCommodityCode(CommodityCode, Tran, db);
                    if (!cMCommodityDAL.Delete(CommodityCode, Tran, db))
                    {
                        Tran.Rollback();
                        return false;
                    }
                }
                else
                {
                    TradeCommodityAssignDAL.DeleteByCommodityCode(CommodityCode, Tran, db);
                    if (!cMCommodityDAL.Delete(CommodityCode, Tran, db))
                    {
                        Tran.Rollback();
                        return false;
                    }
                }
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-4301";
                string errMsg = "ɾ��������Ʒʧ��!";
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
        public ManagementCenter.Model.CM_Commodity GetModel(string CommodityCode)
        {
            return cM_CommodityDAL.GetModel(CommodityCode);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_Commodity GetModelByCache(string CommodityCode)
        {
            string CacheKey = "CM_CommodityModel-" + CommodityCode;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_CommodityDAL.GetModel(CommodityCode);
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
            return (ManagementCenter.Model.CM_Commodity)objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_CommodityDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region ��ȡ���н�����Ʒ

        /// <summary>
        /// ��ȡ���н�����Ʒ
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <param name="CommodityName">��Ʒ����</param>
        ///  <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMCommodity(string CommodityCode, string CommodityName, int BreedClassID, int pageNo,
                                         int pageSize,
                                         out int rowCount)
        {
            try
            {
                CM_CommodityDAL cMCommodityDAL = new CM_CommodityDAL();
                return cMCommodityDAL.GetAllCMCommodity(CommodityCode, CommodityName, BreedClassID, pageNo, pageSize,
                                                        out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-4303";
                string errMsg = "��ȡ���н�����Ʒʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ��ȡƷ�����͹�ָ�ڻ�����Ʒ����

        /// <summary>
        /// ��ȡƷ�����͹�ָ�ڻ�����Ʒ����
        /// </summary>
        /// <returns></returns>
        public DataSet GetQHSIFCommodityCode()
        {
            try
            {
                CM_CommodityDAL cMCommodityDAL = new CM_CommodityDAL();
                return cMCommodityDAL.GetQHSIFCommodityCode();
            }
            catch (Exception ex)
            {
                string errCode = "GL-4304";
                string errMsg = "��ȡƷ�����͹�ָ�ڻ�����Ʒ����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ���ݲ�ѯ������ȡ���еĽ�����Ʒ����ѯ������Ϊ�գ�

        /// <summary>
        /// ���ݲ�ѯ������ȡ���еĽ�����Ʒ����ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_Commodity> GetListArray(string strWhere)
        {
            try
            {
                return cM_CommodityDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4305";
                string errMsg = "���ݲ�ѯ������ȡ���еĽ�����Ʒʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ��ȡ���д��뼰�������̼�
        /// <summary>
        /// ��ȡ���д��뼰�������̼�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_Commodity> GetListCMCommodityAndClosePrice(string strWhere)
        {
            try
            {
                return cM_CommodityDAL.GetListCMCommodityAndClosePrice(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4309";
                string errMsg = "��ȡ���д��뼰�������̼�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion

        #region �ṩǰ̨��ȡ��Ʒ����ķ���

        /// <summary>
        /// �ṩǰ̨��ȡ��Ʒ����ķ���
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CommonTable.OnstageCommodity> GetCommodityListArray(string strWhere)
        {
            try
            {
                return cM_CommodityDAL.GetCommodityListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4306";
                string errMsg = "�ṩǰ̨��ȡ��Ʒ����ķ���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);

                return null;
            }
        }

        #endregion

        #region �жϽ�����Ʒ�����Ƿ��Ѿ�����

        /// <summary>
        /// �жϽ�����Ʒ�����Ƿ��Ѿ�����
        /// </summary>
        /// <param name="CommodityCode">��Ʒ����</param>
        /// <returns></returns>
        public bool IsExistCommodityCode(string CommodityCode)
        {
            try
            {
                CM_CommodityDAL cMCommodityDAL = new CM_CommodityDAL();
                string strWhere = string.Format("CommodityCode='{0}'", CommodityCode);
                DataSet _ds = cMCommodityDAL.GetList(strWhere);
                if (_ds != null)
                {
                    if (_ds.Tables[0].Rows.Count == 0)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errCode = "GL-4307";
                string errMsg = "�жϽ�����Ʒ�����Ƿ��Ѿ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region �жϽ�����Ʒ�����Ƿ��Ѿ�����

        /// <summary>
        /// �жϽ�����Ʒ�����Ƿ��Ѿ�����
        /// </summary>
        /// <param name="CommodityName">��Ʒ����</param>
        /// <returns></returns>
        public bool IsExistCommodityName(string CommodityName)
        {
            try
            {
                CM_CommodityDAL cMCommodityDAL = new CM_CommodityDAL();
                string strWhere = string.Format("CommodityName='{0}'", CommodityName);
                DataSet _ds = cMCommodityDAL.GetList(strWhere);
                if (_ds != null)
                {
                    if (_ds.Tables[0].Rows.Count == 0)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errCode = "GL-4308";
                string errMsg = "�жϽ�����Ʒ�����Ƿ��Ѿ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #endregion  ��Ա����
    }
}