using System;
using System.Collections.Generic;
using System.Data;
using LTP.Common;
using ManagementCenter.Model;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///描述：月份 业务逻辑类QH_MonthBLL 的摘要说明。
    ///作者：刘书伟
    ///日期:2008-12-13
    /// </summary>
    public class QH_MonthBLL
    {
        private readonly ManagementCenter.DAL.QH_MonthDAL qH_MonthDAL = new ManagementCenter.DAL.QH_MonthDAL();

        public QH_MonthBLL()
        {
        }

        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_MonthDAL.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int MonthID)
        {
            return qH_MonthDAL.Exists(MonthID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(ManagementCenter.Model.QH_Month model)
        {
            qH_MonthDAL.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ManagementCenter.Model.QH_Month model)
        {
            qH_MonthDAL.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int MonthID)
        {
            qH_MonthDAL.Delete(MonthID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ManagementCenter.Model.QH_Month GetModel(int MonthID)
        {
            return qH_MonthDAL.GetModel(MonthID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public ManagementCenter.Model.QH_Month GetModelByCache(int MonthID)
        {
            string CacheKey = "QH_MonthModel-" + MonthID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_MonthDAL.GetModel(MonthID);
                    if (objModel != null)
                    {
                        int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache),
                                                      TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (ManagementCenter.Model.QH_Month) objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_MonthDAL.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}
        /// <summary>
        /// 根据查询条件获取所有的月份 （查询条件可为空）
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_Month> GetListArray(string strWhere)
        {
            try
            {
                return qH_MonthDAL.GetListArray(strWhere);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        #endregion  成员方法
    }
}