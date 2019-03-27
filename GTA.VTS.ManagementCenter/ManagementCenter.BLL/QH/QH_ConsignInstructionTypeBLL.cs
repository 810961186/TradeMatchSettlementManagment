using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    /// ������ί��ָ������ ҵ���߼���QH_ConsignInstructionTypeBLL ��ժҪ˵����
    /// ���ߣ�����ΰ
    /// ����:2008-12-13
    /// </summary>
    public class QH_ConsignInstructionTypeBLL
    {
        private readonly ManagementCenter.DAL.QH_ConsignInstructionTypeDAL qH_ConsignInstructionTypeDAL =
            new ManagementCenter.DAL.QH_ConsignInstructionTypeDAL();

        public QH_ConsignInstructionTypeBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_ConsignInstructionTypeDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int ConsignInstructionTypeID)
        {
            return qH_ConsignInstructionTypeDAL.Exists(ConsignInstructionTypeID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(ManagementCenter.Model.QH_ConsignInstructionType model)
        {
            qH_ConsignInstructionTypeDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.QH_ConsignInstructionType model)
        {
            qH_ConsignInstructionTypeDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int ConsignInstructionTypeID)
        {
            qH_ConsignInstructionTypeDAL.Delete(ConsignInstructionTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_ConsignInstructionType GetModel(int ConsignInstructionTypeID)
        {
            return qH_ConsignInstructionTypeDAL.GetModel(ConsignInstructionTypeID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_ConsignInstructionType GetModelByCache(int ConsignInstructionTypeID)
        {
            string CacheKey = "QH_ConsignInstructionTypeModel-" + ConsignInstructionTypeID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_ConsignInstructionTypeDAL.GetModel(ConsignInstructionTypeID);
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
            return (ManagementCenter.Model.QH_ConsignInstructionType) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_ConsignInstructionTypeDAL.GetList(strWhere);
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
        /// ���ݲ�ѯ������ȡ���е�ί��ָ�����ͣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_ConsignInstructionType> GetListArray(string strWhere)
        {
            try
            {
                return qH_ConsignInstructionTypeDAL.GetListArray(strWhere);
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