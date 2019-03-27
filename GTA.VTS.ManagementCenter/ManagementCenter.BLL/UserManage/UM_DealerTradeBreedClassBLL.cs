using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    /// ����: Ʒ������Ȩ�ޱ� ҵ���߼���UM_DealerTradeBreedClassBLL ��ժҪ˵����
    /// ���ߣ�������
    /// ���ڣ�2008-11-20   
    /// </summary>
    public class UM_DealerTradeBreedClassBLL
    {
        /// <summary>
        /// Ʒ������Ȩ��DAL
        /// </summary>
        private readonly ManagementCenter.DAL.UM_DealerTradeBreedClassDAL dal =
            new ManagementCenter.DAL.UM_DealerTradeBreedClassDAL();

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public UM_DealerTradeBreedClassBLL()
        {
        }
        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int DealerTradeBreedClassID)
        {
            return dal.Exists(DealerTradeBreedClassID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.UM_DealerTradeBreedClass model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.UM_DealerTradeBreedClass model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int DealerTradeBreedClassID)
        {
            dal.Delete(DealerTradeBreedClassID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.UM_DealerTradeBreedClass GetModel(int DealerTradeBreedClassID)
        {
            return dal.GetModel(DealerTradeBreedClassID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.UM_DealerTradeBreedClass GetModelByCache(int DealerTradeBreedClassID)
        {
            string CacheKey = "UM_DealerTradeBreedClassModel-" + DealerTradeBreedClassID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(DealerTradeBreedClassID);
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
            return (ManagementCenter.Model.UM_DealerTradeBreedClass)objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ��ȡ�û���Ʒ�ֽ���Ȩ��
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataSet GetUserBreedClassRight(int UserID)
        {
            return dal.GetUserBreedClassRight(UserID);
        }

        /// <summary>
        /// �����û�ID��ȡƷ��Ȩ���б�
        /// </summary>
        /// <param name="UserID">�û�ID</param>
        /// <returns>����Ʒ��Ȩ���б�</returns>
        public List<ManagementCenter.Model.UM_DealerTradeBreedClass> GetBreedClassRightList(int UserID)
        {
            List<ManagementCenter.Model.UM_DealerTradeBreedClass> l =
                dal.GetListArray(string.Format("UserID={0}", UserID));
            return l;
        }

        #endregion  ��Ա����
    }
}