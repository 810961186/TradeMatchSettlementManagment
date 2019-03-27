using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;
using GTA.VTS.Common.CommonObject;
namespace ManagementCenter.BLL
{
	/// <summary>
    /// ��������ʼ�ʽ�� ҵ���߼���UM_OriginationFundBLL ��ժҪ˵����
    /// ���ߣ�������
    /// ���ڣ�2008-11-20   
    /// </summary>
	public class UM_OriginationFundBLL
	{
        private readonly ManagementCenter.DAL.UM_OriginationFundDAL dal = new ManagementCenter.DAL.UM_OriginationFundDAL();
		public UM_OriginationFundBLL()
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
		public bool Exists(int OriginationFundID)
		{
			return dal.Exists(OriginationFundID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(ManagementCenter.Model.UM_OriginationFund model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.UM_OriginationFund model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int OriginationFundID)
		{
			
			dal.Delete(OriginationFundID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.UM_OriginationFund GetModel(int OriginationFundID)
		{
			
			return dal.GetModel(OriginationFundID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public ManagementCenter.Model.UM_OriginationFund GetModelByCache(int OriginationFundID)
		{
			
			string CacheKey = "UM_OriginationFundModel-" + OriginationFundID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(OriginationFundID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ManagementCenter.Model.UM_OriginationFund)objModel;
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


        #region
        /// <summary>
        /// �����û�ID��ȡ��ʼ�ʽ��б�
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.UM_OriginationFund> GetListArrayByUserID(int UserID)
        {
            try
            {
                //ManagementCenter.DAL.UM_DealerAccountDAL DealerAccountDAL = new UM_DealerAccountDAL();
                ////DealerAccountDAL.DeleteByUserID(

                //ManagementCenter.DAL.UM_AccountTypeDAL AccountTypeDAL = new UM_AccountTypeDAL();
                //List<UM_AccountType> L_UM_AccountType =
                //    AccountTypeDAL.GetListArray(string.Format("AccountAttributionType={0}",
                //                                              (int) Types.AccountAttributionType.BankAccount));

                //if (L_UM_AccountType != null && L_UM_AccountType.Count == 1)
                //{
                //    List<UM_DealerAccount> L_UM_DealerAccount =
                //        DealerAccountDAL.GetListArray(string.Format("AccountTypeID={0} AND UserID={1}",
                //                                                    L_UM_AccountType[0].AccountTypeID, UserID));
                //    if (L_UM_DealerAccount != null && L_UM_DealerAccount.Count == 1)
                //    {
                //        List<ManagementCenter.Model.UM_OriginationFund> L_UM_OriginationFund =
                //            dal.GetListArray(string.Format("DealerAccoutID='{0}'", L_UM_DealerAccount[0].DealerAccoutID));
                //        return L_UM_OriginationFund;
                //    }
                //}
                //return null;
                return dal.GetListByUserID(UserID);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
  
        }

	    #endregion


        #endregion  ��Ա����
    }
}

