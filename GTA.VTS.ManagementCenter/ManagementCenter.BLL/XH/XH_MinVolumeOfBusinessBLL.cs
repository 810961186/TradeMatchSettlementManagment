using System;
using System.Collections.Generic;
using System.Data;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.Model;
using ManagementCenter.Model.CommonClass;

namespace ManagementCenter.BLL
{
    /// <summary>
    /// ���������׹���_���׷���_���׵�λ_������(��С���׵�λ) ҵ���߼���XH_MinVolumeOfBusinessBLL ��ժҪ˵����
    ///������뷶Χ:5300-5319
    ///���ߣ�����ΰ
    ///���ڣ�2008-12-15
    /// </summary>
    public class XH_MinVolumeOfBusinessBLL
    {
        private readonly ManagementCenter.DAL.XH_MinVolumeOfBusinessDAL xH_MinVolumeOfBusinessDAL =
            new ManagementCenter.DAL.XH_MinVolumeOfBusinessDAL();

        public XH_MinVolumeOfBusinessBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return xH_MinVolumeOfBusinessDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int MinVolumeOfBusinessID)
        {
            return xH_MinVolumeOfBusinessDAL.Exists(MinVolumeOfBusinessID);
        }

        #region ��ӽ��׹���_���׷���_���׵�λ_������(��С���׵�λ)
        /// <summary>
        /// ��ӽ��׹���_���׷���_���׵�λ_������(��С���׵�λ)
        /// </summary>
        /// <param name="model">���׹���_���׷���_���׵�λ_������(��С���׵�λ)ʵ��</param>
        /// <returns></returns>
        public int Add(ManagementCenter.Model.XH_MinVolumeOfBusiness model)
        {
            try
            {
                return xH_MinVolumeOfBusinessDAL.Add(model);

            }
            catch (Exception ex)
            {
                string errCode = "GL-5300";
                string errMsg = "��ӽ��׹���_���׷���_���׵�λ_������(��С���׵�λ)ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);

                return AppGlobalVariable.INIT_INT;
            }
        }
        #endregion

        #region �ݲ��õĹ�������
        /// <summary>
        /// ����һ������
        /// </summary>
        //public void Update(ManagementCenter.Model.XH_MinVolumeOfBusiness model)
        //{
        //    xH_MinVolumeOfBusinessDAL.Update(model);
        //}

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        //public void Delete(int MinVolumeOfBusinessID)
        //{
        //    xH_MinVolumeOfBusinessDAL.Delete(MinVolumeOfBusinessID);
        //}

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_MinVolumeOfBusiness GetModel(int MinVolumeOfBusinessID)
        {
            return xH_MinVolumeOfBusinessDAL.GetModel(MinVolumeOfBusinessID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.XH_MinVolumeOfBusiness GetModelByCache(int MinVolumeOfBusinessID)
        {
            string CacheKey = "XH_MinVolumeOfBusinessModel-" + MinVolumeOfBusinessID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = xH_MinVolumeOfBusinessDAL.GetModel(MinVolumeOfBusinessID);
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
            return (ManagementCenter.Model.XH_MinVolumeOfBusiness)objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return xH_MinVolumeOfBusinessDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion

        #region ���ݲ�ѯ������ȡ���еĽ��׹���_���׷���_���׵�λ_������(��С���׵�λ����ѯ������Ϊ�գ�
        /// <summary>
        /// ���ݲ�ѯ������ȡ���еĽ��׹���_���׷���_���׵�λ_������(��С���׵�λ����ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.XH_MinVolumeOfBusiness> GetListArray(string strWhere)
        {
            try
            {
                return xH_MinVolumeOfBusinessDAL.GetListArray(strWhere);

            }
            catch (Exception ex)
            {
                string errCode = "GL-5304";
                string errMsg = "���ݲ�ѯ������ȡ���еĽ��׹���_���׷���_���׵�λ_������(��С���׵�λ����ѯ������Ϊ��)ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);

                return null;
            }
        }
        #endregion

        #endregion  ��Ա����

        #region ��ȡ���н��׹���_���׷���_���׵�λ_������(��С���׵�λ)

        /// <summary>
        /// ��ȡ���н��׹���_���׷���_���׵�λ_������(��С���׵�λ)
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllXHMinVolumeOfBusiness(string BreedClassName, int pageNo, int pageSize, out int rowCount)
        {
            try
            {
                return xH_MinVolumeOfBusinessDAL.GetAllXHMinVolumeOfBusiness(BreedClassName, pageNo, pageSize,
                                                                             out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-5303";
                string errMsg = "��ȡ���н��׹���_���׷���_���׵�λ_������(��С���׵�λ)ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);

                return null;

            }
        }

        #endregion

        #region ���ݽ��׹���_���׷���_���׵�λ_������(��С���׵�λ)ID��ɾ��������

        /// <summary>
        /// ���ݽ��׹���_���׷���_���׵�λ_������(��С���׵�λ)ID��ɾ��������
        /// </summary>
        /// <param name="minVolumeOfBusinessID">���ݽ��׹���_���׷���_���׵�λ_������(��С���׵�λ)ID</param>
        /// <returns></returns>
        public bool DeleteXHMinVolumeOfBusByID(int minVolumeOfBusinessID)
        {
            try
            {
                return xH_MinVolumeOfBusinessDAL.DeleteXHMinVolumeOfBusByID(minVolumeOfBusinessID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5301";
                string errMsg = "���ݽ��׹���_���׷���_���׵�λ_������(��С���׵�λ)ID��ɾ��������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;

            }
        }

        #endregion

        #region ���½��׹���_���׷���_���׵�λ_������(��С���׵�λ)����

        /// <summary>
        /// ���½��׹���_���׷���_���׵�λ_������(��С���׵�λ)����
        /// </summary>
        /// <param name="model">���׹���_���׷���_���׵�λ_������(��С���׵�λ)ʵ��</param>
        public bool UpdateXHMinVolumeOfBus(ManagementCenter.Model.XH_MinVolumeOfBusiness model)
        {
            try
            {
                return xH_MinVolumeOfBusinessDAL.UpdateXHMinVolumeOfBus(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5302";
                string errMsg = "���½��׹���_���׷���_���׵�λ_������(��С���׵�λ)����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion


        #region �����ֻ������͸۹ɹ�����е�Ʒ�ֱ�ʶ��ȡƷ������
        /// <summary>
        /// �����ֻ������͸۹ɹ�����е�Ʒ�ֱ�ʶ��ȡƷ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetXHAndHKBreedClassNameByBreedClassID()
        {
            try
            {
                return xH_MinVolumeOfBusinessDAL.GetXHAndHKBreedClassNameByBreedClassID();
            }
            catch (Exception ex)
            {
                string errCode = "GL-";
                string errMsg = "�����ֻ������͸۹ɹ�����е�Ʒ�ֱ�ʶ��ȡƷ����������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion

    }
}