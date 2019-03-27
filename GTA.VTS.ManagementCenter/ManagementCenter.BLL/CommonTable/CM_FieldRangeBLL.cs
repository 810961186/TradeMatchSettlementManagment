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
    /// �������ֶ�_��Χ ҵ���߼���CM_FieldRangeBLL ��ժҪ˵����
    /// ������뷶Χ:5340-5359
    ///���ߣ�����ΰ
    ///����:2008-11-27
    /// </summary>
    public class CM_FieldRangeBLL
    {
        private readonly ManagementCenter.DAL.CM_FieldRangeDAL cM_FieldRangeDAL =
            new ManagementCenter.DAL.CM_FieldRangeDAL();

        public CM_FieldRangeBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return cM_FieldRangeDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int FieldRangeID)
        {
            return cM_FieldRangeDAL.Exists(FieldRangeID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.CM_FieldRange model)
        {
            return cM_FieldRangeDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.CM_FieldRange model)
        {
            cM_FieldRangeDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int FieldRangeID)
        {
            cM_FieldRangeDAL.Delete(FieldRangeID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_FieldRange GetModel(int FieldRangeID)
        {
            return cM_FieldRangeDAL.GetModel(FieldRangeID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_FieldRange GetModelByCache(int FieldRangeID)
        {
            string CacheKey = "CM_FieldRangeModel-" + FieldRangeID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_FieldRangeDAL.GetModel(FieldRangeID);
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
            return (ManagementCenter.Model.CM_FieldRange) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_FieldRangeDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region ���ݲ�ѯ������ȡ���е��ֶ�_��Χ����ѯ������Ϊ�գ�
        /// <summary>
        /// ���ݲ�ѯ������ȡ���е��ֶ�_��Χ����ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_FieldRange> GetListArray(string strWhere)

        {
            try
            {
                return cM_FieldRangeDAL.GetListArray(strWhere);

            }
            catch (Exception ex)
            {
                string errCode = "GL-5340";
                string errMsg = "���ݲ�ѯ������ȡ���е��ֶ�_��Χ(��ѯ������Ϊ��)ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion

        #endregion  ��Ա����
    }
}