using System;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;
namespace ManagementCenter.BLL
{
	/// <summary>
    /// �������ⶳԭ��� ҵ���߼���UM_ThawReasonBLL ��ժҪ˵����
    /// ���ߣ�������
    /// ���ڣ�2008-11-20   
    /// </summary>
	public class UM_ThawReasonBLL
	{
        private readonly ManagementCenter.DAL.UM_ThawReasonDAL dal = new ManagementCenter.DAL.UM_ThawReasonDAL();
		public UM_ThawReasonBLL()
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
		public bool Exists(int ThawReasonID)
		{
			return dal.Exists(ThawReasonID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(ManagementCenter.Model.UM_ThawReason model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.UM_ThawReason model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int ThawReasonID)
		{
			
			dal.Delete(ThawReasonID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.UM_ThawReason GetModel(int ThawReasonID)
		{
			
			return dal.GetModel(ThawReasonID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public ManagementCenter.Model.UM_ThawReason GetModelByCache(int ThawReasonID)
		{
			
			string CacheKey = "UM_ThawReasonModel-" + ThawReasonID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ThawReasonID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ManagementCenter.Model.UM_ThawReason)objModel;
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

		#endregion  ��Ա����
	}
}

