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
    ///����:�ֻ�_Ʒ��_�ǵ��� ҵ���߼���XH_SpotHighLow ��ժҪ˵����
    /// ������뷶Χ:5240-5259
    ///���ߣ�����ΰ
    ///����:2008-12-27
    /// </summary>
    public class XH_SpotHighLowValueBLL
    {
        private readonly ManagementCenter.DAL.XH_SpotHighLowValueDAL xH_SpotHighLowValueDAL =
            new XH_SpotHighLowValueDAL();

        public XH_SpotHighLowValueBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return xH_SpotHighLowValueDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int HightLowValueID)
        {
            return xH_SpotHighLowValueDAL.Exists(HightLowValueID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        //public void Add(ManagementCenter.Model.XH_SpotHighLowValue model)
        //{
        //    xH_SpotHighLowValueDAL.Add(model);
        //}

        /// <summary>
        /// ����һ������
        /// </summary>
        //public void Update(ManagementCenter.Model.XH_SpotHighLowValue model)
        //{
        //    xH_SpotHighLowValueDAL.Update(model);
        //}

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_SpotHighLowValue GetModel(int HightLowValueID)
        {
            return xH_SpotHighLowValueDAL.GetModel(HightLowValueID);
        }

        #region  ����Ʒ���ǵ�����ʶ�õ�һ������ʵ��

        /// <summary>
        /// ����Ʒ���ǵ�����ʶ�õ�һ������ʵ��
        /// </summary>
        /// <param name="BreedClassHighLowID">Ʒ���ǵ�����ʶ</param>
        /// <returns></returns>
        public ManagementCenter.Model.XH_SpotHighLowValue GetModelByBCHighLowID(int BreedClassHighLowID)
        {
            try
            {
                XH_SpotHighLowValueDAL xHSpotHighLowValueDAL = new XH_SpotHighLowValueDAL();
                return xHSpotHighLowValueDAL.GetModelByBCHighLowID(BreedClassHighLowID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5240";
                string errMsg = "����Ʒ���ǵ�����ʶ�õ�һ������ʵ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.XH_SpotHighLowValue GetModelByCache(int HightLowValueID)
        {
            string CacheKey = "XH_SpotHighLowValueModel-" + HightLowValueID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = xH_SpotHighLowValueDAL.GetModel(HightLowValueID);
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
            return (ManagementCenter.Model.XH_SpotHighLowValue) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return xH_SpotHighLowValueDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region  ���ݲ�ѯ������ȡ���е��ֻ�_Ʒ��_�ǵ�������ѯ������Ϊ�գ�
        /// <summary>
        /// ���ݲ�ѯ������ȡ���е��ֻ�_Ʒ��_�ǵ�������ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.XH_SpotHighLowValue> GetListArray(string strWhere)
        {
            try
            {
                return xH_SpotHighLowValueDAL.GetListArray(strWhere);

            }
            catch (Exception ex)
            {
                string errCode = "GL-5241";
                string errMsg = "���ݲ�ѯ������ȡ���е��ֻ�_Ʒ��_�ǵ���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
                
            }
        }
        #endregion

        #endregion  ��Ա����
    }
}