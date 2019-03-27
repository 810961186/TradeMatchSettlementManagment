using System;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;
namespace ManagementCenter.BLL
{
	/// <summary>
    /// ����: �ʽ𶳽�� ҵ���߼���UM_FreezeReasonBLL ��ժҪ˵����
    /// ���ߣ�������
    /// ���ڣ�2008-11-20   
    /// </summary>
	public class UM_FreezeReasonBLL
	{
        private readonly ManagementCenter.DAL.UM_FreezeReasonDAL dal = new ManagementCenter.DAL.UM_FreezeReasonDAL();
		public UM_FreezeReasonBLL()
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
		public bool Exists(int FreezeReasonID)
		{
			return dal.Exists(FreezeReasonID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(ManagementCenter.Model.UM_FreezeReason model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.UM_FreezeReason model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int FreezeReasonID)
		{
			
			dal.Delete(FreezeReasonID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.UM_FreezeReason GetModel(int FreezeReasonID)
		{
			
			return dal.GetModel(FreezeReasonID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public ManagementCenter.Model.UM_FreezeReason GetModelByCache(int FreezeReasonID)
		{
			
			string CacheKey = "UM_FreezeReasonModel-" + FreezeReasonID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(FreezeReasonID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ManagementCenter.Model.UM_FreezeReason)objModel;
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

