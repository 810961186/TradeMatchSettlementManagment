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
    ///������Ʒ��_�ֻ�_���׷��� ҵ���߼���XH_SpotCostsBLL ��ժҪ˵����
    /// ������뷶Χ:5500-5519
    ///���ߣ�����ΰ
    ///���ڣ�2008-11-27
    /// </summary>
    public class XH_SpotCostsBLL
    {
        private readonly ManagementCenter.DAL.XH_SpotCostsDAL xH_SpotCostsDAL =
            new ManagementCenter.DAL.XH_SpotCostsDAL();

        public XH_SpotCostsBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return xH_SpotCostsDAL.GetMaxId();
        }

        /// <summary>
        /// �ж�Ʒ��_�ֻ�_���׷�����Ʒ�������Ƿ��Ѿ�����
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool Exists(int BreedClassID)
        {
            try
            {
                XH_SpotCostsDAL xHSpotCostsDAL = new XH_SpotCostsDAL();
                return xHSpotCostsDAL.Exists(BreedClassID);

            }
            catch (Exception ex)
            {
                string errCode = "GL-5507";
                string errMsg = "�ж�Ʒ��_�ֻ�_���׷�����Ʒ�������Ƿ��Ѿ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #region ����ֻ����׷���
        /// <summary>
        /// ����ֻ����׷���
        /// </summary>
        /// <param name="model">�ֻ����׷���ʵ��</param>
        /// <returns></returns>
        public bool Add(ManagementCenter.Model.XH_SpotCosts model)
        {
            try
            {
                return xH_SpotCostsDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5500";
                string errMsg = " ����ֻ����׷���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }
        #endregion

        #region �����ֻ����׷���
        /// <summary>
        /// �����ֻ����׷���
        /// </summary>
        /// <param name="model">�ֻ����׷���ʵ��</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.XH_SpotCosts model)
        {
            try
            {
                return xH_SpotCostsDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5502";
                string errMsg = " �����ֻ����׷���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }
        #endregion

        #region ����Ʒ��ID��ɾ���ֻ����׷���

        /// <summary>
        ///����Ʒ��ID��ɾ���ֻ����׷���
        /// </summary>
        public bool DeleteSpotCosts(int BreedClassID)
        {
            try
            {
                return xH_SpotCostsDAL.Delete(BreedClassID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5501";
                string errMsg = " ����Ʒ��ID��ɾ���ֻ����׷���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ����Ʒ��ID�õ��ֻ����׷��ö���ʵ��

        /// <summary>
        /// ����Ʒ��ID�õ��ֻ����׷��ö���ʵ��
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public ManagementCenter.Model.XH_SpotCosts GetModel(int BreedClassID)
        {
            try
            {
                return xH_SpotCostsDAL.GetModel(BreedClassID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5505";
                string errMsg = " ����Ʒ��ID�õ��ֻ����׷��ö���ʵ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region �ݲ��õĹ�������

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.XH_SpotCosts GetModelByCache(int BreedClassID)
        {
            string CacheKey = "XH_SpotCostsModel-" + BreedClassID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = xH_SpotCostsDAL.GetModel(BreedClassID);
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
            return (ManagementCenter.Model.XH_SpotCosts) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return xH_SpotCostsDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        #endregion

        #region ���ݲ�ѯ������ȡ���е�Ʒ��_�ֻ�_���׷��ã���ѯ������Ϊ�գ�

        /// <summary>
        /// ���ݲ�ѯ������ȡ���е�Ʒ��_�ֻ�_���׷��ã���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.XH_SpotCosts> GetListArray(string strWhere)
        {
            try
            {
                return xH_SpotCostsDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5504";
                string errMsg = " ���ݲ�ѯ������ȡ���е�Ʒ��_�ֻ�_���׷��ã���ѯ������Ϊ�գ�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ��ȡ�����ֻ����׷���

        /// <summary>
        /// ��ȡ�����ֻ����׷���
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllSpotCosts(int BreedClassID, string BreedClassName, int pageNo, int pageSize,
                                       out int rowCount)
        {
            try
            {
                XH_SpotCostsDAL xHSpotCostsDAL = new XH_SpotCostsDAL();
                return xHSpotCostsDAL.GetAllSpotCosts(BreedClassID, BreedClassName, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-5503";
                string errMsg = " ��ȡ�����ֻ����׷���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region �����ֻ����׷��ñ��е�Ʒ��ID��ȡƷ������

        /// <summary>
        /// �����ֻ����׷��ñ��е�Ʒ��ID��ȡƷ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpotCostsBreedClassName()
        {
            try
            {
                XH_SpotCostsDAL xHSpotCostsDAL = new XH_SpotCostsDAL();
                return xHSpotCostsDAL.GetSpotCostsBreedClassName();
            }
            catch (Exception ex)
            {
                string errCode = "GL-5506";
                string errMsg = "�����ֻ����׷��ñ��е�Ʒ��ID��ȡƷ������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #endregion  ��Ա����
    }
}