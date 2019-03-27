using System;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;
namespace ManagementCenter.BLL
{
	/// <summary>
    /// �������ʽ���ϸ�� ҵ���߼���UM_FundAddInfoBLL ��ժҪ˵����
    /// ���ߣ�������
    /// ���ڣ�2008-11-20   
    /// </summary>
	public class UM_FundAddInfoBLL
	{
        private readonly ManagementCenter.DAL.UM_FundAddInfoDAL dal = new ManagementCenter.DAL.UM_FundAddInfoDAL();
		public UM_FundAddInfoBLL()
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
		public bool Exists(int AddFundID)
		{
			return dal.Exists(AddFundID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(ManagementCenter.Model.UM_FundAddInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.UM_FundAddInfo model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int AddFundID)
		{
			
			dal.Delete(AddFundID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.UM_FundAddInfo GetModel(int AddFundID)
		{
			
			return dal.GetModel(AddFundID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public ManagementCenter.Model.UM_FundAddInfo GetModelByCache(int AddFundID)
		{
			
			string CacheKey = "UM_FundAddInfoModel-" + AddFundID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(AddFundID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ManagementCenter.Model.UM_FundAddInfo)objModel;
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

        #region ׷���ʽ���ʷ��ҳ��ѯ
        /// <summary>
        /// ׷���ʽ���ʷ��ҳ��ѯ
        /// </summary>
        /// <param name="fundAddQueryEntity"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public DataSet GetPagingFund(ManagementCenter.Model.UserManage.FundAddQueryEntity fundAddQueryEntity, int pageNo, int pageSize,
                                        out int rowCount)
		{
		    try
		    {
		        return  dal.GetPagingFund(fundAddQueryEntity, pageNo, pageSize, out rowCount);
		    }
		    catch (Exception)
		    {
		        rowCount = 0;
		        return null;
		    }
        }
        #endregion

        #endregion  ��Ա����
    }
}

