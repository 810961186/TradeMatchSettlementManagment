using System;
using System.Collections.Generic;
using System.Data;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///����������ί���� ҵ���߼���QH_SingleRequestQuantityBLL ��ժҪ˵����������뷶Χ:6040-6049
    ///���ߣ�����ΰ
    ///���ڣ�2008-11-27
    /// </summary>
    public class QH_SingleRequestQuantityBLL
    {
        private readonly ManagementCenter.DAL.QH_SingleRequestQuantityDAL qH_SingleRequestQuantityDAL =
            new ManagementCenter.DAL.QH_SingleRequestQuantityDAL();

        public QH_SingleRequestQuantityBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_SingleRequestQuantityDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int SingleRequestQuantityID)
        {
            return qH_SingleRequestQuantityDAL.Exists(SingleRequestQuantityID);
        }

        ///// <summary>
        ///// ����һ������
        ///// </summary>
        //public int Add(ManagementCenter.Model.QH_SingleRequestQuantity model)
        //{
        //    return qH_SingleRequestQuantityDAL.Add(model);
        //}

        ///// <summary>
        ///// ����һ������
        ///// </summary>
        //public void Update(ManagementCenter.Model.QH_SingleRequestQuantity model)
        //{
        //    qH_SingleRequestQuantityDAL.Update(model);
        //}

        ///// <summary>
        ///// ɾ��һ������
        ///// </summary>
        //public void Delete(int SingleRequestQuantityID)
        //{
        //    qH_SingleRequestQuantityDAL.Delete(SingleRequestQuantityID);
        //}

        #region ���ݵ���ί������ʶ��ȡ����ί����ʵ��
        /// <summary>
        /// ���ݵ���ί������ʶ��ȡ����ί����ʵ��
       /// </summary>
        /// <param name="SingleRequestQuantityID">����ί������ʶ</param>
       /// <returns></returns>
        public ManagementCenter.Model.QH_SingleRequestQuantity GetQHSingleRequestQuantityModel(int SingleRequestQuantityID)
        {
            try
            {
                QH_SingleRequestQuantityDAL qHSingleRequestQuantityDAL=new QH_SingleRequestQuantityDAL();
                return qH_SingleRequestQuantityDAL.GetModel(SingleRequestQuantityID);

            }
            catch (Exception ex)
            {
                string errCode = "GL-6040";
                string errMsg = "���ݵ���ί������ʶ��ȡ����ί����ʵ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion

         #region ���ݽ��׹���ί����ID��ȡ����ί����
        /// <summary>
        /// ���ݽ��׹���ί����ID��ȡ����ί����
        /// </summary>
        /// <param name="ConsignQuantumID">���׹���ί����ID</param>
        /// <returns></returns>
        public ManagementCenter.Model.QH_SingleRequestQuantity GetQHSingleRequestQuantityModelByConsignQuantumID(int ConsignQuantumID)
        {
            try
            {
                QH_SingleRequestQuantityDAL qHSingleRequestQuantityDAL = new QH_SingleRequestQuantityDAL();
                return qH_SingleRequestQuantityDAL.GetQHSingleRequestQuantityModelByConsignQuantumID(ConsignQuantumID);

            }
            catch (Exception ex)
            {
                string errCode = "GL-6041";
                string errMsg = "���ݽ��׹���ί����ID��ȡ����ί����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
         #endregion

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_SingleRequestQuantity GetModelByCache(int SingleRequestQuantityID)
        {
            string CacheKey = "QH_SingleRequestQuantityModel-" + SingleRequestQuantityID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_SingleRequestQuantityDAL.GetModel(SingleRequestQuantityID);
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
            return (ManagementCenter.Model.QH_SingleRequestQuantity) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_SingleRequestQuantityDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ���ݲ�ѯ������ȡ���еĵ���ί��������ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_SingleRequestQuantity> GetListArray(string strWhere)
        {
            try
            {
                return qH_SingleRequestQuantityDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6042";
                string errMsg = "���ݲ�ѯ������ȡ���еĵ���ί��������ѯ������Ϊ�գ�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion  ��Ա����
    }
}