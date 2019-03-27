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
    /// ������ҵ���߼���RC_MatchCenterBLL ��ժҪ˵����
    /// ���ߣ�������    �޸ģ�����ΰ
    /// ���ڣ�2008-12-15  2009-10-30 �쳣���뷶Χ 2200-2220
    /// </summary>
    public class RC_MatchCenterBLL
    {
        private readonly ManagementCenter.DAL.RC_MatchCenterDAL dal = new ManagementCenter.DAL.RC_MatchCenterDAL();

        public RC_MatchCenterBLL()
        {
        }

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
        public bool Exists(int MatchCenterID)
        {
            return dal.Exists(MatchCenterID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.RC_MatchCenter model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.RC_MatchCenter model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int MatchCenterID)
        {
            dal.Delete(MatchCenterID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.RC_MatchCenter GetModel(int MatchCenterID)
        {
            return dal.GetModel(MatchCenterID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.RC_MatchCenter GetModelByCache(int MatchCenterID)
        {
            string CacheKey = "RC_MatchCenterModel-" + MatchCenterID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(MatchCenterID);
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
            return (ManagementCenter.Model.RC_MatchCenter) objModel;
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
        /// ��ȡ���еĴ�������б�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.RC_MatchCenter> GetListArray(string strWhere)
        {
            return dal.GetListArray(strWhere);
        }

        /// <summary>
        /// �����ݱ���
        /// </summary>
        /// <param name="MatchCenter">�������ʵ��</param>
        /// <param name="dt">��������ϻ����������</param>
        /// <returns></returns>
        public bool GuideSave(RC_MatchCenter MatchCenter, DataTable dt)
        {
            try
            {
                RC_MatchCenterDAL MatchCenterDAL = new RC_MatchCenterDAL();
                RC_MatchMachineDAL MatchMachineDAL = new RC_MatchMachineDAL();
                RC_TradeCommodityAssignDAL TradeCommodityAssignDAL = new RC_TradeCommodityAssignDAL();

                Database db = DatabaseFactory.CreateDatabase();
                DbConnection Conn = db.CreateConnection();
                if (Conn.State != ConnectionState.Open) Conn.Open();
                DbTransaction Tran = Conn.BeginTransaction();
                try
                {
                    TradeCommodityAssignDAL.DeleteAll(Tran, db);
                    MatchMachineDAL.DeleteAll(Tran, db);
                    MatchCenterDAL.DeleteAll(Tran, db);

                    int MatchCenterID = MatchCenterDAL.Add(MatchCenter, Tran, db);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int BourseTypeID = int.Parse(dt.Rows[i]["BourseTypeID"].ToString());
                        int MachineNO = int.Parse(dt.Rows[i]["MachineNo"].ToString());
                        if (!AssignCode(MachineNO, BourseTypeID, MatchCenterID, Tran, db))
                            return false;
                    }
                    Tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Tran.Rollback();
                    string errCode = "GL-2200";
                    string errMsg = "�����ݱ���ʧ��";
                    VTException vte = new VTException(errCode, errMsg, ex);
                    LogHelper.WriteError(vte.ToString(), vte.InnerException);
                    return false;
                }
                finally
                {
                    if (Conn.State == ConnectionState.Open) Conn.Close();
                }
            }
            catch (Exception ex)
            {
                string errCode = "GL-2201";
                string errMsg = "���ݿ�����ʧ��";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return false;
            }
        }


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="MachineNO">��ϻ�����</param>
        /// <param name="BourseTypeID">��������ʶID</param>
        /// <param name="MatchCenterID">�������ID</param>
        /// <param name="Tran">����</param>
        /// <param name="db">���ݿ�</param>
        /// <returns></returns>
        private bool AssignCode(int MachineNO, int BourseTypeID, int MatchCenterID, DbTransaction Tran, Database db)
        {
            RC_MatchMachineDAL MatchMachineDAL = new RC_MatchMachineDAL();
            RC_TradeCommodityAssignDAL TradeCommodityAssignDAL = new RC_TradeCommodityAssignDAL();
            try
            {
                RC_MatchMachine MatchMachine;
                RC_TradeCommodityAssign TradeCommodityAssign;

                DataSet ds = TradeCommodityAssignDAL.GetCodeListByBourseTypeID(BourseTypeID);
                int CodeNum = ds.Tables[0].Rows.Count;
                int M_C_Num = (int) CodeNum/MachineNO;

                for (int i = 1; i <= MachineNO; i++)
                {
                    MatchMachine = new RC_MatchMachine();
                    MatchMachine.BourseTypeID = BourseTypeID;
                    MatchMachine.MatchCenterID = MatchCenterID;
                    int MachineID = MatchMachineDAL.Add(MatchMachine, Tran, db);

                    int start, end;
                    if (i < MachineNO)
                    {
                        start = (i - 1)*M_C_Num;
                        end = i*M_C_Num;
                    }
                    else
                    {
                        start = (i - 1)*M_C_Num;
                        end = CodeNum;
                    }
                    for (int j = start; j < end; j++)
                    {
                        TradeCommodityAssign = new RC_TradeCommodityAssign();
                        TradeCommodityAssign.CommodityCode = ds.Tables[0].Rows[j]["CommodityCode"].ToString();
                        TradeCommodityAssign.MatchMachineID = MachineID;
                        //������Դ�Ǹ���:1��CM_Commodity��;2����HK_Commodity��
                        TradeCommodityAssign.CodeFormSource = Convert.ToInt32(ds.Tables[0].Rows[j]["CodeFormSource"].ToString());
                        TradeCommodityAssignDAL.Add(TradeCommodityAssign, Tran, db);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                string errCode = "GL-2202";
                string errMsg = "�������ʧ��";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return false;
            }
        }

        /// <summary>
        /// �������������
        /// </summary>
        public void CenterTestConnection()
        {
            try
            {
                List<ManagementCenter.Model.RC_MatchCenter> l_MatchCenter = dal.GetListArray(string.Empty);
                foreach (RC_MatchCenter center in l_MatchCenter)
                {
                    bool flag = false;
                    try
                    {
                        flag = ServiceIn.DoOrderServiceProxy.GetInstance().TestConnection(center.IP, (int) center.Port,
                                                                                          center.XiaDanService);
                    }
                    catch (VTException ex)
                    {
                        //д�쳣��־
                        flag = false;
                        GTA.VTS.Common.CommonUtility.LogHelper.WriteError(ex.Message, ex);
                    }
                    if (center.State == null)
                    {
                        if (flag)
                        {
                            center.State = (int) ManagementCenter.Model.CommonClass.Types.StateEnum.ConnSuccess;
                        }
                        else
                        {
                            center.State = (int) ManagementCenter.Model.CommonClass.Types.StateEnum.ConnDefeat;
                        }
                        dal.Update(center);
                    }
                    else if (center.State == (int) ManagementCenter.Model.CommonClass.Types.StateEnum.ConnDefeat)
                    {
                        if (flag)
                        {
                            center.State = (int) ManagementCenter.Model.CommonClass.Types.StateEnum.ConnSuccess;
                            dal.Update(center);
                        }
                    }
                    else
                    {
                        if (!flag)
                        {
                            center.State = (int) ManagementCenter.Model.CommonClass.Types.StateEnum.ConnDefeat;
                            dal.Update(center);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errCode = "GL-2203";
                string errMsg = "�������������ʧ��";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
            }
        }

        #endregion  ��Ա����
    }
}