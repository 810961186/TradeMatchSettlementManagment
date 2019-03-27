using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;
namespace ManagementCenter.BLL
{
	/// <summary>
    ///�������ֲֺͱ�֤��������� ҵ���߼���QH_PositionBailTypeBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
	public class QH_PositionBailTypeBLL
	{
        private readonly ManagementCenter.DAL.QH_PositionBailTypeDAL qH_PositionBailTypeDAL = new ManagementCenter.DAL.QH_PositionBailTypeDAL();
        public QH_PositionBailTypeBLL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
            return qH_PositionBailTypeDAL.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int PositionBailTypeID)
		{
            return qH_PositionBailTypeDAL.Exists(PositionBailTypeID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(ManagementCenter.Model.QH_PositionBailType model)
		{
            qH_PositionBailTypeDAL.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.QH_PositionBailType model)
		{
            qH_PositionBailTypeDAL.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int PositionBailTypeID)
		{

            qH_PositionBailTypeDAL.Delete(PositionBailTypeID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.QH_PositionBailType GetModel(int PositionBailTypeID)
		{

            return qH_PositionBailTypeDAL.GetModel(PositionBailTypeID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public ManagementCenter.Model.QH_PositionBailType GetModelByCache(int PositionBailTypeID)
		{
			
			string CacheKey = "QH_PositionBailTypeModel-" + PositionBailTypeID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
                    objModel = qH_PositionBailTypeDAL.GetModel(PositionBailTypeID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ManagementCenter.Model.QH_PositionBailType)objModel;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
            return qH_PositionBailTypeDAL.GetList(strWhere);
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
        /// ���ݲ�ѯ������ȡ���еĳֲֺͱ�֤��������ͣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_PositionBailType> GetListArray(string strWhere)
        {
            try
            {
                return qH_PositionBailTypeDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                GTA.VTS.Common.CommonUtility.LogHelper.WriteError(ex.Message, ex);
                return null;
            }
        }

		#endregion  ��Ա����
	}
}

