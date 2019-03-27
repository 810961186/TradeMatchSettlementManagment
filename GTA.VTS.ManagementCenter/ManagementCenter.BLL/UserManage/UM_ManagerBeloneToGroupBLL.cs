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
    /// ����������Ա_������ ҵ���߼���UM_ManagerBeloneToGroupBLL ��ժҪ˵����
    /// ���ߣ�������
    /// ���ڣ�2008-11-20   
	/// </summary>
	public class UM_ManagerBeloneToGroupBLL
	{
        private readonly ManagementCenter.DAL.UM_ManagerBeloneToGroupDAL dal = new ManagementCenter.DAL.UM_ManagerBeloneToGroupDAL();
		public UM_ManagerBeloneToGroupBLL()
		{}
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
		public bool Exists(int UserID)
		{
			return dal.Exists(UserID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void  Add(ManagementCenter.Model.UM_ManagerBeloneToGroup model)
		{
			 dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.UM_ManagerBeloneToGroup model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int UserID)
		{
			
			dal.Delete(UserID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.UM_ManagerBeloneToGroup GetModel(int UserID)
		{
			
			return dal.GetModel(UserID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public ManagementCenter.Model.UM_ManagerBeloneToGroup GetModelByCache(int UserID)
		{
			
			string CacheKey = "UM_ManagerBeloneToGroupModel-" + UserID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(UserID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ManagementCenter.Model.UM_ManagerBeloneToGroup)objModel;
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
        /// ��ȡ����Ա����Ȩ�����б�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.UM_ManagerBeloneToGroup> GetListArray(string strWhere)
        {
            try
            {
                return dal.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-1121";
                string errMsg = "��ȡ����Ա����Ȩ�����б�";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return null;
            }
        }
		/// <summary>
		/// ��������б�
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  ��Ա����
	}
}

