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
    /// �������ֻ�_������ƷƷ��_�ֲ����� ҵ���߼���XH_SpotPositionBLL ��ժҪ˵����
    /// ������뷶Χ:5650-5669
    ///���ߣ�����ΰ
    ///���ڣ�2008-11-27
    /// </summary>
    public class XH_SpotPositionBLL
    {
        private readonly ManagementCenter.DAL.XH_SpotPositionDAL xH_SpotPositionDAL =
            new ManagementCenter.DAL.XH_SpotPositionDAL();

        public XH_SpotPositionBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return xH_SpotPositionDAL.GetMaxId();
        }

        #region ����Ʒ��ID���ж��ֻ�_������ƷƷ��_�ֲ����Ƽ�¼�Ƿ��Ѵ���

        /// <summary>
        /// ����Ʒ��ID���ж��ֻ�_������ƷƷ��_�ֲ����Ƽ�¼�Ƿ��Ѵ���
        /// </summary>
        /// <param name="BreedClassID"></param>
        /// <returns></returns>
        public bool ExistsXHSpotPosition(int BreedClassID)
        {
            try
            {
                XH_SpotPositionDAL xHSpotPositionDAL = new XH_SpotPositionDAL();
                return xHSpotPositionDAL.Exists(BreedClassID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5650";
                string errMsg = "����Ʒ��ID���ж��ֻ�_������ƷƷ��_�ֲ����Ƽ�¼�Ƿ��Ѵ���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ����ֻ�_������ƷƷ��_�ֲ�����
        /// <summary>
        /// ����ֻ�_������ƷƷ��_�ֲ�����
        /// </summary>
        /// <param name="model">�ֻ�_������ƷƷ��_�ֲ�����ʵ��</param>
        /// <returns></returns>
        public bool Add(ManagementCenter.Model.XH_SpotPosition model)
        {
            try
            {
                return xH_SpotPositionDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5651";
                string errMsg = "����ֻ�_������ƷƷ��_�ֲ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }
        #endregion

        #region �����ֻ�_������ƷƷ��_�ֲ�����
        /// <summary>
        /// �����ֻ�_������ƷƷ��_�ֲ�����
        /// </summary>
        /// <param name="model">�ֻ�_������ƷƷ��_�ֲ�����ʵ��</param>
        /// <returns></returns>
        public bool Update(ManagementCenter.Model.XH_SpotPosition model)
        {
            try
            {
                return xH_SpotPositionDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5653";
                string errMsg = "�����ֻ�_������ƷƷ��_�ֲ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }
        #endregion

        #region ɾ���ֻ�_������ƷƷ��_�ֲ�����
        /// <summary>
        /// ɾ���ֻ�_������ƷƷ��_�ֲ�����
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool Delete(int BreedClassID)
        {
            try
            {
                return xH_SpotPositionDAL.Delete(BreedClassID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5652";
                string errMsg = "ɾ���ֻ�_������ƷƷ��_�ֲ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }
        #endregion

        #region �ݲ��õĹ�������
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_SpotPosition GetModel(int BreedClassID)
        {
            return xH_SpotPositionDAL.GetModel(BreedClassID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.XH_SpotPosition GetModelByCache(int BreedClassID)
        {
            string CacheKey = "XH_SpotPositionModel-" + BreedClassID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = xH_SpotPositionDAL.GetModel(BreedClassID);
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
            return (ManagementCenter.Model.XH_SpotPosition) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return xH_SpotPositionDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion

        #region ���ݲ�ѯ������ȡ���е��ֻ�_������ƷƷ��_�ֲ����ƣ���ѯ������Ϊ�գ�
        /// <summary>
        /// ���ݲ�ѯ������ȡ���е��ֻ�_������ƷƷ��_�ֲ����ƣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.XH_SpotPosition> GetListArray(string strWhere)
        {
            try
            {
                return xH_SpotPositionDAL.GetListArray(strWhere);

            }
            catch (Exception ex)
            {
                string errCode = "GL-5655";
                string errMsg = "���ݲ�ѯ������ȡ���е��ֻ�_������ƷƷ��_�ֲ�����(��ѯ������Ϊ��)ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion

        #region ��ȡ�����ֻ�_������ƷƷ��_�ֲ�����

        /// <summary>
        /// ��ȡ�����ֻ�_������ƷƷ��_�ֲ�����
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllXHSpotPosition(string BreedClassName, int pageNo, int pageSize,
                                            out int rowCount)
        {
            try
            {
                XH_SpotPositionDAL xHSpotPositionDAL = new XH_SpotPositionDAL();
                return xHSpotPositionDAL.GetAllXHSpotPosition(BreedClassName, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-5654";
                string errMsg = "��ȡ�����ֻ�_������ƷƷ��_�ֲ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region �����ֻ�������ƷƷ��_�ֲ����Ʊ��е�Ʒ��ID��ȡƷ������

        /// <summary>
        /// �����ֻ�������ƷƷ��_�ֲ����Ʊ��е�Ʒ��ID��ȡƷ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpotPositionBreedClassName()
        {
            try
            {
                XH_SpotPositionDAL xHSpotPositionDAL = new XH_SpotPositionDAL();
                return xHSpotPositionDAL.GetSpotPositionBreedClassName();
            }
            catch (Exception ex)
            {
                string errCode = "GL-5656";
                string errMsg = "�����ֻ�������ƷƷ��_�ֲ����Ʊ��е�Ʒ��ID��ȡƷ������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #endregion  ��Ա����
    }
}