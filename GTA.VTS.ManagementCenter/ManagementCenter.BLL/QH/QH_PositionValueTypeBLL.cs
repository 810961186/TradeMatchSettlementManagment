using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;
namespace ManagementCenter.BLL
{
	/// <summary>
    ///��������Ʒ�ڻ�_�ֲ�ȡֵ���� ҵ���߼���QH_PositionValueTypeBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///����:2008-12-13
    /// </summary>
	public class QH_PositionValueTypeBLL
	{
        private readonly ManagementCenter.DAL.QH_PositionValueTypeDAL qH_PositionValueTypeDAL = new ManagementCenter.DAL.QH_PositionValueTypeDAL();
        public QH_PositionValueTypeBLL()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
            return qH_PositionValueTypeDAL.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int PositionValueTypeID)
		{
            return qH_PositionValueTypeDAL.Exists(PositionValueTypeID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(ManagementCenter.Model.QH_PositionValueType model)
		{
            qH_PositionValueTypeDAL.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(ManagementCenter.Model.QH_PositionValueType model)
		{
            qH_PositionValueTypeDAL.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int PositionValueTypeID)
		{

            qH_PositionValueTypeDAL.Delete(PositionValueTypeID);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public ManagementCenter.Model.QH_PositionValueType GetModel(int PositionValueTypeID)
		{

            return qH_PositionValueTypeDAL.GetModel(PositionValueTypeID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public ManagementCenter.Model.QH_PositionValueType GetModelByCache(int PositionValueTypeID)
		{
			
			string CacheKey = "QH_PositionValueTypeModel-" + PositionValueTypeID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
                    objModel = qH_PositionValueTypeDAL.GetModel(PositionValueTypeID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ManagementCenter.Model.QH_PositionValueType)objModel;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
            return qH_PositionValueTypeDAL.GetList(strWhere);
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
        /// ���ݲ�ѯ������ȡ���е���Ʒ�ڻ�_�ֲ�ȡֵ���ͣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_PositionValueType> GetListArray(string strWhere)
        {
            try
            {
                return qH_PositionValueTypeDAL.GetListArray(strWhere);
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

