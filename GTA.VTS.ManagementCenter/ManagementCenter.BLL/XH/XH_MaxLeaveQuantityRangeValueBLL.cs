using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    /// ���������׹���_���ί����_��Χ_ֵ  ҵ���߼���XH_MaxLeaveQuantityRangeValueBLL ��ժҪ˵����
    /// ˵������û�н��׹���_���ί����_��Χ_ֵ ����
    ///���ߣ�����ΰ
    ///���ڣ�2008-11-27
    /// </summary>
    public class XH_MaxLeaveQuantityRangeValueBLL
    {
        private readonly ManagementCenter.DAL.XH_MaxLeaveQuantityRangeValueDAL xH_MaxLeaveQuantityRangeValueDAL =
            new ManagementCenter.DAL.XH_MaxLeaveQuantityRangeValueDAL();

        public XH_MaxLeaveQuantityRangeValueBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return xH_MaxLeaveQuantityRangeValueDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int BreedClassID, int FieldRangeID)
        {
            return xH_MaxLeaveQuantityRangeValueDAL.Exists(BreedClassID, FieldRangeID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue model)
        {
            xH_MaxLeaveQuantityRangeValueDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue model)
        {
            xH_MaxLeaveQuantityRangeValueDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int BreedClassID, int FieldRangeID)
        {
            xH_MaxLeaveQuantityRangeValueDAL.Delete(BreedClassID, FieldRangeID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue GetModel(int BreedClassID, int FieldRangeID)
        {
            return xH_MaxLeaveQuantityRangeValueDAL.GetModel(BreedClassID, FieldRangeID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue GetModelByCache(int BreedClassID, int FieldRangeID)
        {
            string CacheKey = "XH_MaxLeaveQuantityRangeValueModel-" + BreedClassID + FieldRangeID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = xH_MaxLeaveQuantityRangeValueDAL.GetModel(BreedClassID, FieldRangeID);
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
            return (ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return xH_MaxLeaveQuantityRangeValueDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}
        /// <summary>
        /// ���ݲ�ѯ������ȡ���еĽ��׹���_���ί����_��Χ_ֵ����ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.XH_MaxLeaveQuantityRangeValue> GetListArray(string strWhere)
        {
            try
            {
                return xH_MaxLeaveQuantityRangeValueDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                GTA.VTS.Common.CommonUtility.LogHelper.WriteError(ex.Message, ex);
                return null;
                //throw;
            }
        }

        #endregion  ��Ա����
    }
}