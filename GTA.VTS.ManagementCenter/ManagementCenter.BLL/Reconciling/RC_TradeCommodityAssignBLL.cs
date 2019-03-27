using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ManagementCenter.BLL
{
    /// <summary>
    /// �������������ҵ���߼��ࡣ
    /// ���ߣ�������
    /// ���ڣ�2008-12-15  �쳣���뷶Χ 2241-2260
    /// </summary>
    public class RC_TradeCommodityAssignBLL
    {
        /// <summary>
        /// �������DAL
        /// </summary>
        private readonly ManagementCenter.DAL.RC_TradeCommodityAssignDAL dal =
            new ManagementCenter.DAL.RC_TradeCommodityAssignDAL();

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public RC_TradeCommodityAssignBLL()
        {
        }
        #endregion
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
        public bool Exists(string CommodityCode, int MatchMachineID)
        {
            return dal.Exists(CommodityCode, MatchMachineID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(ManagementCenter.Model.RC_TradeCommodityAssign model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// ��������
        /// </summary>
        public bool Update(int MatchMachineID, List<ManagementCenter.Model.RC_TradeCommodityAssign> ladd,
                           List<ManagementCenter.Model.RC_TradeCommodityAssign> ldel)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open) Conn.Open();
            DbTransaction Tran = Conn.BeginTransaction();
            try
            {
                foreach (var assign in ldel)
                {
                    dal.Delete(assign.CommodityCode, MatchMachineID, Tran, db);
                }
                foreach (var assign in ladd)
                {
                    assign.MatchMachineID = MatchMachineID;
                    dal.Add(assign, Tran, db);
                }
                Tran.Commit();
                return true;
            }
            catch (Exception)
            {
                Tran.Rollback();
                return false;
                throw;
            }
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(string CommodityCode, int MatchMachineID)
        {
            //dal.Delete(CommodityCode,MatchMachineID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.RC_TradeCommodityAssign GetModel(string CommodityCode, int MatchMachineID)
        {
            return dal.GetModel(CommodityCode, MatchMachineID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.RC_TradeCommodityAssign GetModelByCache(string CommodityCode, int MatchMachineID)
        {
            string CacheKey = "RC_TradeCommodityAssignModel-" + CommodityCode + MatchMachineID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(CommodityCode, MatchMachineID);
                    if (objModel != null)
                    {
                        int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache),
                                                      TimeSpan.Zero);
                    }
                }
                catch
                {}
            }
            return (ManagementCenter.Model.RC_TradeCommodityAssign) objModel;
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
        /// ��ȡ��Ʒ���䵽��ϻ����б�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.RC_TradeCommodityAssign> GetListArray(string strWhere)
        {
            return dal.GetListArray(strWhere);
        }


        /// <summary>
        /// ���ݴ�ϻ��õ���ϻ���ϵĴ���
        /// </summary>
        /// <param name="MatchMachineID"></param>
        /// <returns></returns>
        public DataSet GetCodeListByMatchMachineID(int MatchMachineID)
        {
            try
            {
                return dal.GetCodeListByMatchMachineID(MatchMachineID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-2241";
                string errMsg = "���ݴ�ϻ��õ���ϻ���ϵĴ���ʧ��";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return null;
            }
        }

        /// <summary>
        /// �õ�������û�з������Ʒ����
        /// </summary>
        /// <param name="BourseTypeID"></param>
        /// <returns></returns>
        public DataSet GetNotAssignCodeByBourseTypeID(int BourseTypeID)
        {
            try
            {
                return dal.GetNotAssignCodeByBourseTypeID(BourseTypeID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-2242";
                string errMsg = "�õ�������û�з������Ʒ����ʧ��";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return null;
            }
        }

        #endregion  ��Ա����
    }
}