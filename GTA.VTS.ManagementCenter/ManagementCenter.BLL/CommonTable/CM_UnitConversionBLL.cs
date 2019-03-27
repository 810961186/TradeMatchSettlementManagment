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
    ///�������ֻ�_Ʒ��_���׵�λ���� ҵ���߼���CM_UnitConversionBLL ��ժҪ˵����
    /// ������뷶Χ:5750-5769
    ///���ߣ�����ΰ
    ///����:2008-11-27
    /// </summary>
    public class CM_UnitConversionBLL
    {
        private readonly ManagementCenter.DAL.CM_UnitConversionDAL cM_UnitConversionDAL =
            new ManagementCenter.DAL.CM_UnitConversionDAL();

        public CM_UnitConversionBLL()
        {
        }

        #region  ��Ա����

        #region ����ֻ�_Ʒ��_���׵�λ����
        /// <summary>
        /// ����ֻ�_Ʒ��_���׵�λ����
        /// </summary>
        /// <param name="model">�ֻ�_Ʒ��_���׵�λ����ʵ��</param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.CM_UnitConversion model)
        {
            try
            {
                return cM_UnitConversionDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5750";
                string errMsg = "����ֻ�_Ʒ��_���׵�λ����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return AppGlobalVariable.INIT_INT;
            }
        }
        #endregion

        #region �����ֻ�_Ʒ��_���׵�λ����
        /// <summary>
        /// �����ֻ�_Ʒ��_���׵�λ����
        /// </summary>
        /// <param name="model">�ֻ�_Ʒ��_���׵�λ����ʵ��</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.CM_UnitConversion model)
        {
            try
            {
                return cM_UnitConversionDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5752";
                string errMsg = "�����ֻ�_Ʒ��_���׵�λ����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }
        #endregion

        #region �����ֻ�_Ʒ��_���׵�λ����IDɾ���ֻ�_Ʒ��_���׵�λ����
        /// <summary>
        /// �����ֻ�_Ʒ��_���׵�λ����IDɾ���ֻ�_Ʒ��_���׵�λ����
        /// </summary>
        /// <param name="UnitConversionID">�ֻ�_Ʒ��_���׵�λ����ID</param>
        /// <returns></returns>
        public bool Delete(int UnitConversionID)
        {
            try
            {
                return cM_UnitConversionDAL.Delete(UnitConversionID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5751";
                string errMsg = "�����ֻ�_Ʒ��_���׵�λ����IDɾ���ֻ�_Ʒ��_���׵�λ����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }
        #endregion

        #region �ݲ���Ҫ�Ĺ�������
        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return cM_UnitConversionDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int UnitConversionID)
        {
            return cM_UnitConversionDAL.Exists(UnitConversionID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_UnitConversion GetModel(int UnitConversionID)
        {
            return cM_UnitConversionDAL.GetModel(UnitConversionID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_UnitConversion GetModelByCache(int UnitConversionID)
        {
            string CacheKey = "CM_UnitConversionModel-" + UnitConversionID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_UnitConversionDAL.GetModel(UnitConversionID);
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
            return (ManagementCenter.Model.CM_UnitConversion) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_UnitConversionDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion

        #region ���ݲ�ѯ������ȡ���е��ֻ�_Ʒ��_���׵�λ���� ����ѯ������Ϊ�գ�
        /// <summary>
        /// ���ݲ�ѯ������ȡ���е��ֻ�_Ʒ��_���׵�λ���� ����ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_UnitConversion> GetListArray(string strWhere)
        {
            try
            {
                return cM_UnitConversionDAL.GetListArray(strWhere);

            }
            catch (Exception ex)
            {
                string errCode = "GL-5754";
                string errMsg = "���ݲ�ѯ������ȡ���е��ֻ�_Ʒ��_���׵�λ���� ����ѯ������Ϊ�գ�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
                }
        }
        #endregion

        #region ��ȡ�����ֻ�_Ʒ��_���׵�λ����

        /// <summary>
        /// ��ȡ�����ֻ�_Ʒ��_���׵�λ����
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMUnitConversion(string BreedClassName, int pageNo, int pageSize,
                                              out int rowCount)
        {
            try
            {
                CM_UnitConversionDAL cMUnitConversionDAL = new CM_UnitConversionDAL();
                return cMUnitConversionDAL.GetAllCMUnitConversion(BreedClassName, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-5753";
                string errMsg = "��ȡ�����ֻ�_Ʒ��_���׵�λ����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region �����ֻ�_Ʒ��_���׵�λ������е�Ʒ��ID��ȡƷ������

        /// <summary>
        /// �����ֻ�_Ʒ��_���׵�λ������е�Ʒ��ID��ȡƷ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetCMUnitConversionBreedClassName()
        {
            try
            {
                CM_UnitConversionDAL cMUnitConversionDAL = new CM_UnitConversionDAL();
                return cMUnitConversionDAL.GetCMUnitConversionBreedClassName();
            }
            catch (Exception ex)
            {
                string errCode = "GL-5755";
                string errMsg = "�����ֻ�_Ʒ��_���׵�λ������е�Ʒ��ID��ȡƷ������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #endregion  ��Ա����
    }
}