using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ManagementCenter.BLL
{
    /// <summary>
    /// ��������ϻ�����ҵ���߼��ࡣ
    /// ���ߣ�������
    /// ���ڣ�2008-12-15  �쳣���뷶Χ 2221-2240
    /// </summary>
    public class RC_MatchMachineBLL
    {
        /// <summary>
        /// ��ϻ�DAL
        /// </summary>
        private readonly ManagementCenter.DAL.RC_MatchMachineDAL dal = new ManagementCenter.DAL.RC_MatchMachineDAL();

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public RC_MatchMachineBLL()
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
        public bool Exists(int MatchMachineID)
        {
            return dal.Exists(MatchMachineID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.RC_MatchMachine model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.RC_MatchMachine model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int MatchMachineID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open) Conn.Open();
            DbTransaction Tran = Conn.BeginTransaction();
            try
            {
                ManagementCenter.DAL.RC_TradeCommodityAssignDAL TradeCommodityAssignDAL =
                    new RC_TradeCommodityAssignDAL();

                TradeCommodityAssignDAL.DeleteByMachineID(MatchMachineID, Tran, db);
                dal.Delete(MatchMachineID, Tran, db);
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-2221";
                string errMsg = "ɾ����ϻ�ʧ�� ID=" + MatchMachineID.ToString();
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open) Conn.Close();
            }
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.RC_MatchMachine GetModel(int MatchMachineID)
        {
            return dal.GetModel(MatchMachineID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.RC_MatchMachine GetModelByCache(int MatchMachineID)
        {
            string CacheKey = "RC_MatchMachineModel-" + MatchMachineID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(MatchMachineID);
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
            return (ManagementCenter.Model.RC_MatchMachine) objModel;
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
        /// ����������ȡ���д�ϻ��б�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns>���ش�ϻ�ʵ��</returns>
        public List<ManagementCenter.Model.RC_MatchMachine> GetListArray(string strWhere)
        {
            return dal.GetListArray(strWhere);
        }

        /// <summary>
        /// ��ҳ��ѯ��ϻ�
        /// </summary>
        /// <param name="machineQueryEntity">��ѯʵ��</param>
        /// <param name="pageNo">��ѯҳ��</param>
        /// <param name="pageSize">ÿҳ��С</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetPagingMachine(ManagementCenter.Model.RC_MatchMachine machineQueryEntity, int pageNo,
                                        int pageSize,
                                        out int rowCount)
        {
            try
            {
                return dal.GetPagingMachine(machineQueryEntity, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = 0;
                string errCode = "GL-2222";
                string errMsg = "��ҳ��ѯ��ϻ�ʧ��";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return null;
            }
        }

        #region  ���ݴ������_��ϻ����еĽ���������ID��ȡ��������������

        /// <summary>
        /// ���ݴ������_��ϻ����еĽ���������ID��ȡ��������������
        /// </summary>
        /// <returns></returns>
        public DataSet GetRCMatchMachineBourseTypeName()
        {
            try
            {
                return dal.GetRCMatchMachineBourseTypeName();
            }
            catch (Exception ex)
            {
                GTA.VTS.Common.CommonUtility.LogHelper.WriteError(ex.Message, ex);
                return null;
            }
        }

        #endregion

        #region  ���ݴ������_��ϻ����еĴ������ID��ȡ�����������

        /// <summary>
        /// ���ݴ������_��ϻ����еĴ������ID��ȡ�����������
        /// </summary>
        /// <returns></returns>
        public DataSet GetRCMatchMachineMatchCenterName()
        {
            try
            {
                return dal.GetRCMatchMachineMatchCenterName();
            }
            catch (Exception ex)
            {
                GTA.VTS.Common.CommonUtility.LogHelper.WriteError(ex.Message, ex);
                return null;
            }
        }

        #endregion

        #endregion  ��Ա����
    }
}