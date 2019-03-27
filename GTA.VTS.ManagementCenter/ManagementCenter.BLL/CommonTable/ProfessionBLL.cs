using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;
namespace ManagementCenter.BLL
{
	/// <summary>
    /// ����:��ҵ�� ҵ���߼���Profession ��ժҪ˵����
    /// ���ߣ�����Ա��������
    /// ���ڣ�2008-11-20
	/// </summary>
	public class Profession
	{
		private readonly ManagementCenter.DAL.ProfessionDAL dal=new ProfessionDAL();
		public Profession()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string Nindcd)
		{
			return dal.Exists(Nindcd);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(ManagementCenter.Model.Profession model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.Profession model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string Nindcd)
		{
			
			dal.Delete(Nindcd);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.Profession GetModel(string Nindcd)
		{
			
			return dal.GetModel(Nindcd);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public ManagementCenter.Model.Profession GetModelByCache(string Nindcd)
		{
			
			string CacheKey = "ProfessionModel-" + Nindcd;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Nindcd);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ManagementCenter.Model.Profession)objModel;
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

        /// <summary>
        /// �������ʵ�弯
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.Profession> GetListArray(string strWhere)
		{
		    return dal.GetListArray(strWhere);
		}

	    #endregion  ��Ա����
	}
}

