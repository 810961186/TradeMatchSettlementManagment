using System;
using System.Collections.Generic;
using System.Data;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///��������Ч�걨ȡֵ ҵ���߼���XH_ValidDeclareValue ��ժҪ˵����������뷶Χ:5280-5299
    ///���ߣ�����ΰ
    ///����:2008-11-27
    /// </summary>
    public class XH_ValidDeclareValueBLL
    {
        private readonly ManagementCenter.DAL.XH_ValidDeclareValueDAL xH_ValidDeclareValueDAL =
            new ManagementCenter.DAL.XH_ValidDeclareValueDAL();

        public XH_ValidDeclareValueBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return xH_ValidDeclareValueDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int ValidDeclareValueID)
        {
            return xH_ValidDeclareValueDAL.Exists(ValidDeclareValueID);
        }

        #region ��û�õ��Ĺ�������
        /// <summary>
        /// ����һ������
        /// </summary>
        //public void Add(ManagementCenter.Model.XH_ValidDeclareValue model)
        //{
        //    xH_ValidDeclareValueDAL.Add(model);
        //}

        /// <summary>
        /// ����һ������
        /// </summary>
        //public void Update(ManagementCenter.Model.XH_ValidDeclareValue model)
        //{
        //    xH_ValidDeclareValueDAL.Update(model);
        //}

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        //public void Delete(int ValidDeclareValueID)
        //{
        //    xH_ValidDeclareValueDAL.Delete(ValidDeclareValueID);
        //}

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_ValidDeclareValue GetModel(int ValidDeclareValueID)
        {
            return xH_ValidDeclareValueDAL.GetModel(ValidDeclareValueID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.XH_ValidDeclareValue GetModelByCache(int ValidDeclareValueID)
        {
            string CacheKey = "XH_ValidDeclareValueModel-" + ValidDeclareValueID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = xH_ValidDeclareValueDAL.GetModel(ValidDeclareValueID);
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
            return (ManagementCenter.Model.XH_ValidDeclareValue) objModel;
        }
      

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return xH_ValidDeclareValueDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion

        #region ���ݲ�ѯ������ȡ���е���Ч�걨ȡֵ����ѯ������Ϊ�գ�
        /// <summary>
        /// ���ݲ�ѯ������ȡ���е���Ч�걨ȡֵ����ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.XH_ValidDeclareValue> GetListArray(string strWhere)
        {
            try
            {
                return xH_ValidDeclareValueDAL.GetListArray(strWhere);

            }
            catch (Exception ex)
            {
                string errCode = "GL-5280";
                string errMsg = "���ݲ�ѯ������ȡ���е���Ч�걨ȡֵʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion

        #endregion  ��Ա����
    }
}