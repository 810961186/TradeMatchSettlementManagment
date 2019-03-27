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
    ///��������Ʒ�ڻ�_��֤����� ҵ���߼���QH_CFBailScaleValueBLL ��ժҪ˵����������뷶Χ:6600-6619
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
    public class QH_CFBailScaleValueBLL
    {
        private readonly ManagementCenter.DAL.QH_CFBailScaleValueDAL qH_CFBailScaleValueDAL =
            new ManagementCenter.DAL.QH_CFBailScaleValueDAL();

        public QH_CFBailScaleValueBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_CFBailScaleValueDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int CFBailScaleValueID)
        {
            return qH_CFBailScaleValueDAL.Exists(CFBailScaleValueID);
        }

        #region �����Ʒ�ڻ�_��֤�����

        /// <summary>
        /// �����Ʒ�ڻ�_��֤�����
        /// </summary>
        /// <param name="model">��Ʒ�ڻ�_��֤�����ʵ��</param>
        /// <returns></returns>
        public int AddQHCFBailScaleValue(ManagementCenter.Model.QH_CFBailScaleValue model)
        {
            try
            {
                return qH_CFBailScaleValueDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6600";
                string errMsg = "�����Ʒ�ڻ�_��֤�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return AppGlobalVariable.INIT_INT;
            }
        }

        /// <summary>
        /// �����Ʒ�ڻ�_��֤�����
        /// </summary>
        /// <param name="model">��Ʒ�ڻ�_��֤�����ʵ��</param>
        /// <param name="model2">��Ʒ�ڻ�_��֤�����ʵ��</param>
        /// <returns></returns>
        public int AddQHCFBailScaleValue(QH_CFBailScaleValue model, QH_CFBailScaleValue model2)
        {
            try
            {
                //����������¼
                int rlt = qH_CFBailScaleValueDAL.Add(model2);
                if (rlt != AppGlobalVariable.INIT_INT)
                {
                    model.RelationScaleID = rlt;
                }
                return qH_CFBailScaleValueDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6600";
                string errMsg = "�����Ʒ�ڻ�_��֤�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return AppGlobalVariable.INIT_INT;
            }
        }

        /// <summary>
        /// �����Ʒ�ڻ�_��ͱ�֤�����
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddQHCFMinScaleValue(QH_SIFBail model)
        {
            try
            {
                QH_SIFBailDAL bailDal = new QH_SIFBailDAL();
                if (bailDal.GetModel(model.BreedClassID) == null)
                {
                    return bailDal.Add(model);
                }
                else
                {
                    return bailDal.Update(model);
                }
            }
            catch (Exception ex)
            {
                string errCode = "GL-6600";
                string errMsg = "������Ʒ�ڻ�_��֤�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ������Ʒ�ڻ�_��֤�����

        /// <summary>
        /// ������Ʒ�ڻ�_��֤�����
        /// </summary>
        /// <param name="model">��Ʒ�ڻ�_��֤�����ʵ��</param>
        /// <returns></returns>
        public bool UpdateQHCFBailScaleValue(ManagementCenter.Model.QH_CFBailScaleValue model)
        {
            try
            {
                return qH_CFBailScaleValueDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6601";
                string errMsg = "������Ʒ�ڻ�_��֤�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        /// <summary>
        /// ������Ʒ�ڻ�_��֤�����
        /// </summary>
        /// <param name="model">��Ʒ�ڻ�_��֤�����ʵ��</param>
        /// <param name="model2">��Ʒ�ڻ�_��֤�����ʵ��</param>
        /// <returns></returns>
        public bool UpdateQHCFBailScaleValue(QH_CFBailScaleValue model, QH_CFBailScaleValue model2)
        {
            try
            {
                if (model.RelationScaleID != null && qH_CFBailScaleValueDAL.GetModel(model.RelationScaleID.Value) != null)
                {
                    //������¼���ڣ�����
                    qH_CFBailScaleValueDAL.Update(model2);
                }
                else
                {
                    //������¼�����ڣ�������¼
                    int rtn = qH_CFBailScaleValueDAL.Add(model2);
                    if (rtn != AppGlobalVariable.INIT_INT)
                    {
                        model.RelationScaleID = rtn;
                    }
                }
                return qH_CFBailScaleValueDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6601";
                string errMsg = "������Ʒ�ڻ�_��֤�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ɾ����Ʒ�ڻ�_��֤�����

        /// <summary>
        /// ɾ����Ʒ�ڻ�_��֤�����
        /// </summary>
        /// <param name="CFBailScaleValueID">��Ʒ�ڻ�_��֤�����ID</param>
        /// <returns></returns>
        public bool DeleteQHCFBailScaleValue(int CFBailScaleValueID)
        {
            try
            {
                return qH_CFBailScaleValueDAL.Delete(CFBailScaleValueID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6602";
                string errMsg = "ɾ����Ʒ�ڻ�_��֤�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        

        #region ������Ʒ�ڻ�_��֤�����ID��ȡ��Ʒ�ڻ�_��֤���������ʵ��

        /// <summary>
        /// ������Ʒ�ڻ�_��֤�����ID��ȡ��Ʒ�ڻ�_��֤���������ʵ��
        /// </summary>
        /// <param name="CFBailScaleValueID">��Ʒ�ڻ�_��֤�����ID</param>
        /// <returns></returns>
        public ManagementCenter.Model.QH_CFBailScaleValue GetQHCFBailScaleValueModel(int CFBailScaleValueID)
        {
            try
            {
                return qH_CFBailScaleValueDAL.GetModel(CFBailScaleValueID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6603";
                string errMsg = "������Ʒ�ڻ�_��֤�����ID��ȡ��Ʒ�ڻ�_��֤���������ʵ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_CFBailScaleValue GetModelByCache(int CFBailScaleValueID)
        {
            string CacheKey = "QH_CFBailScaleValueModel-" + CFBailScaleValueID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_CFBailScaleValueDAL.GetModel(CFBailScaleValueID);
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
            return (ManagementCenter.Model.QH_CFBailScaleValue) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_CFBailScaleValueDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ���ݲ�ѯ������ȡ���е���Ʒ�ڻ�_��֤���������ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_CFBailScaleValue> GetListArray(string strWhere)
        {
            try
            {
                return qH_CFBailScaleValueDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6604";
                string errMsg = "���ݲ�ѯ������ȡ���е���Ʒ�ڻ�_��֤���������ѯ������Ϊ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #region ��ȡ������Ʒ�ڻ�_��֤�����

        /// <summary>
        ///��ȡ������Ʒ�ڻ�_��֤�����
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="DeliveryMonthTypeID">�����·����ͱ�ʶ</param>
        /// <param name="PositionBailTypeID">�ֲֺͱ�֤��������ͱ�ʶ</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllQHCFBailScaleValue(string BreedClassName, int DeliveryMonthTypeID, int PositionBailTypeID,
                                                int pageNo, int pageSize,
                                                out int rowCount)
        {
            try
            {
                QH_CFBailScaleValueDAL qHCFBailScaleValueDAL = new QH_CFBailScaleValueDAL();
                return qHCFBailScaleValueDAL.GetAllQHCFBailScaleValue(BreedClassName, DeliveryMonthTypeID,
                                                                      PositionBailTypeID, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-6605";
                string errMsg = "��ȡ������Ʒ�ڻ�_��֤�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #endregion  ��Ա����


        
    }
}