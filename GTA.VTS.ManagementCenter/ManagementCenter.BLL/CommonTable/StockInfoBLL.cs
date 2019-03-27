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
    /// ����:��Ʊ��Ȩ֤������� ҵ���߼���StockInfo ��ժҪ˵����������뷶Χ:4320-4323
    /// ���ߣ�������     �޸ģ�����ΰ
    /// ���ڣ�2008-11-20  2009-12-01
	/// </summary>
	public class StockInfoBLL
	{
		private readonly ManagementCenter.DAL.StockInfoDAL dal=new StockInfoDAL();
		public StockInfoBLL()
		{}
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string StockCode)
		{
			return dal.Exists(StockCode);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(ManagementCenter.Model.StockInfo model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.StockInfo model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string StockCode)
		{
			
			dal.Delete(StockCode);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.StockInfo GetModel(string StockCode)
		{
			
			return dal.GetModel(StockCode);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public ManagementCenter.Model.StockInfo GetModelByCache(string StockCode)
		{
			
			string CacheKey = "StockInfoModel-" + StockCode;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(StockCode);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ManagementCenter.Model.StockInfo)objModel;
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
        /// ���ݲ�ѯ������ȡ��ͨ������Ϣ��(��ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ��������Ϊ�գ�</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.StockInfo> GetStockInfoList(string strWhere)
        {
            try
            {
                StockInfoDAL stockInfoDAL=new StockInfoDAL();
                return stockInfoDAL.GetStockInfoList(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4320";
                string errMsg = "���ݲ�ѯ������ȡ��ͨ������Ϣ��(��ѯ������Ϊ�գ�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

		#endregion  ��Ա����
	}
}

