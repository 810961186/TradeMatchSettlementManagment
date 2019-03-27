using System;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;
namespace ManagementCenter.BLL
{
	/// <summary>
    /// ����: ҵ���߼���UM_DealerAccount ��ժҪ˵����
    /// ���ߣ�������
    /// ���ڣ�2008-11-20
	/// </summary>
	public class UM_DealerAccountBLL
	{
        private readonly ManagementCenter.DAL.UM_DealerAccountDAL dal = new ManagementCenter.DAL.UM_DealerAccountDAL();
		public UM_DealerAccountBLL()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string DealerAccoutID)
		{
			return dal.Exists(DealerAccoutID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(ManagementCenter.Model.UM_DealerAccount model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.UM_DealerAccount model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string DealerAccoutID)
		{
			
			dal.Delete(DealerAccoutID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.UM_DealerAccount GetModel(string DealerAccoutID)
		{
			
			return dal.GetModel(DealerAccoutID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public ManagementCenter.Model.UM_DealerAccount GetModelByCache(string DealerAccoutID)
		{
			
			string CacheKey = "UM_DealerAccountModel-" + DealerAccoutID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DealerAccoutID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ManagementCenter.Model.UM_DealerAccount)objModel;
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
		/// ��������б�
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

        public ManagementCenter.Model.UM_DealerAccount GetModelByUserIDAndType(int UserID, int AccountAttributionType)
		{
		    try
		    {
                return dal.GetModelByUserIDAndType(UserID, AccountAttributionType);
		    }
		    catch (Exception)
		    {
		        return null;
		    }
		}

	    #endregion  ��Ա����
	}
}

