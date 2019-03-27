using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;
using ManagementCenter.Model.CommonClass;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///���������������� ҵ���߼���CM_BourseTypeBLL ��ժҪ˵���� ������뷶Χ:4700-4719
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class CM_BourseTypeBLL
    {
        private readonly CM_BourseTypeDAL cM_BourseTypeDAL =
            new CM_BourseTypeDAL();

        #region  ��Ա����

        #region ��ȡ���������͵����ID
        /// <summary>
        /// ��ȡ���������͵����ID
        /// </summary>
        public int GetCMBourseTypeMaxId()
        {
            try
            {
                return cM_BourseTypeDAL.GetMaxId();

            }
            catch (Exception ex)
            {
                string errCode = "GL-4709";
                string errMsg = "��ȡ���������͵����IDʧ��!";
                var exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return AppGlobalVariable.INIT_INT;
            }
        }
        #endregion

        #region �Ƿ���ڸü�¼
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int BourseTypeID)
        {
            return cM_BourseTypeDAL.Exists(BourseTypeID);
        }
        #endregion

        #region ��ӽ��������ͺͽ���ʱ��
        /// <summary>
        /// ��ӽ��������ͺͽ���ʱ��
        /// </summary>
        /// <param name="cmBourseType">����������ʵ��</param>
        /// <param name="cmTradeTimeList">����ʱ��ʵ�弯��</param>
        /// <returns></returns>
        public int AddCMBourseTypeAndTradeTime(CM_BourseType cmBourseType, List<CM_TradeTime> cmTradeTimeList)
        {
            var cmBourseTypeDal = new CM_BourseTypeDAL();
            var cmTradeTimeDal = new CM_TradeTimeDAL();
            DbConnection Conn = null;
            Database db = DatabaseFactory.CreateDatabase();
            Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            DbTransaction tran = Conn.BeginTransaction();
            //����������ID
            int BourseTypeID = AppGlobalVariable.INIT_INT;
            //����ʱ��ID
            int TradeTimeID = AppGlobalVariable.INIT_INT;
            try
            {
                if (cmBourseType != null && cmTradeTimeList.Count > 0)
                {
                    BourseTypeID = cmBourseTypeDal.Add(cmBourseType, tran, db);
                    if (BourseTypeID == AppGlobalVariable.INIT_INT)
                    {
                        tran.Rollback();
                    }
                    foreach (CM_TradeTime _cmTradeTime in cmTradeTimeList)
                    {
                        _cmTradeTime.BourseTypeID = BourseTypeID;
                        TradeTimeID = cmTradeTimeDal.Add(_cmTradeTime, tran, db);
                        if (TradeTimeID == AppGlobalVariable.INIT_INT)
                        {
                            tran.Rollback();
                            break;
                        }
                    }
                }
                //���ݽ���������ID��ȡ����������Ľ��׿�ʼʱ�������Ľ��׽���ʱ��
                List<CM_TradeTime> cmTTimeList =
                    cmTradeTimeDal.GetListArray(string.Format("BourseTypeID={0}", BourseTypeID), tran, db);
                if (cmTTimeList.Count > 0)
                {
                    DateTime _tempStartTime = AppGlobalVariable.INIT_DATETIME;
                    DateTime _tempEndTime = AppGlobalVariable.INIT_DATETIME;
                    bool isStartTime = true;//��־��ʼֵ�Ƿ�ı�
                    bool isEndTime = true;//��־����ֵ�Ƿ�ı�
                    for (int i = 0; i < cmTTimeList.Count; i++)
                    {
                        if (isStartTime)
                        {
                            _tempStartTime = (DateTime)cmTTimeList[0].StartTime;
                        }
                        if (isEndTime)
                        {
                            _tempEndTime = (DateTime)cmTTimeList[0].EndTime;
                        }
                        if (_tempStartTime > (DateTime)cmTTimeList[i].StartTime)
                        {
                            _tempStartTime = (DateTime)cmTTimeList[i].StartTime;
                            isStartTime = false;
                        }
                        if (_tempEndTime < (DateTime)cmTTimeList[i].EndTime)
                        {
                            _tempEndTime = (DateTime)cmTTimeList[i].EndTime;
                            isEndTime = false;
                        }
                    }
                    bool result = cmBourseTypeDal.Update(BourseTypeID, _tempStartTime, _tempEndTime, tran, db);
                    if (!result)
                    {
                        tran.Rollback();
                    }
                    tran.Commit();
                }
                return BourseTypeID;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                string errCode = "GL-4708";
                string errMsg = "��ӽ��������ͺͽ���ʱ��ʧ��!";
                var exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return AppGlobalVariable.INIT_INT;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        #endregion

        #region ���½��������͵Ĵ�Ͻ���ί�п�ʼʱ��ʹ�Ͻ���ί�н���ʱ�䣨���ݽ��׿�ʼ�ͽ���ʱ����£�
        /// <summary>
        /// ���½��������͵Ĵ�Ͻ���ί�п�ʼʱ��ʹ�Ͻ���ί�н���ʱ�䣨���ݽ��׿�ʼ�ͽ���ʱ����£�
        /// </summary>
        /// <param name="bourseTypeID">����������ID</param>
        /// <param name="receivingConsignStartTime">��Ͻ���ί�п�ʼʱ��</param>
        /// <param name="receivingConsignEndTime">��Ͻ���ί�н���ʱ��</param>
        /// <returns></returns>
        public bool UpdateBourseTypeConsignTime(int bourseTypeID, DateTime receivingConsignStartTime, DateTime receivingConsignEndTime)
        {
            try
            {
                var cmBourseTypeDal = new CM_BourseTypeDAL();
                cmBourseTypeDal.Update(bourseTypeID, receivingConsignStartTime, receivingConsignEndTime);
                return true;
            }
            catch (Exception ex)
            {
                GTA.VTS.Common.CommonUtility.LogHelper.WriteError(ex.Message, ex);
                return false;
            }
        }
        #endregion

        #region �õ�һ������ʵ�壬�ӻ����С�
        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public CM_BourseType GetModelByCache(int BourseTypeID)
        {
            string CacheKey = "CM_BourseTypeModel-" + BourseTypeID;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_BourseTypeDAL.GetModel(BourseTypeID);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache),
                                           TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (CM_BourseType)objModel;
        }
        #endregion

        #region ��������б�
        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_BourseTypeDAL.GetList(strWhere);
        }
        #endregion

        #region ��������б�
        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion

        #region ���ݲ�ѯ������ȡ���еĽ��������ͣ���ѯ������Ϊ�գ�

        /// <summary>
        /// ���ݲ�ѯ������ȡ���еĽ��������ͣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">������ѯ</param>
        /// <returns></returns>
        public List<CM_BourseType> GetListArray(string strWhere)
        {
            try
            {
                return cM_BourseTypeDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4705";
                string errMsg = "���ݲ�ѯ������ȡ���еĽ��������ͣ���ѯ������Ϊ�գ�ʧ��!";
                var exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ��ȡ���н���������

        /// <summary>
        /// ��ȡ���н���������
        /// </summary>
        /// <param name="BourseTypeName">��������������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMBourseType(string BourseTypeName, int pageNo, int pageSize,
                                          out int rowCount)
        {
            try
            {
                var cMBourseTypeDAL = new CM_BourseTypeDAL();
                return cMBourseTypeDAL.GetAllCMBourseType(BourseTypeName, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-4703";
                string errMsg = "��ȡ���н���������ʧ��!";
                var exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ��ȡ��������������

        /// <summary>
        /// ��ȡ��������������
        /// </summary>
        /// <returns></returns>
        public DataSet GetBourseTypeName()
        {
            try
            {
                var cMBourseTypeDAL = new CM_BourseTypeDAL();
                return cMBourseTypeDAL.GetBourseTypeName();
            }
            catch (Exception ex)
            {
                string errCode = "GL-4704";
                string errMsg = "��ȡ��������������ʧ��!";
                var exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region �жϽ����������Ƿ��Ѿ�����

        /// <summary>
        /// �жϽ����������Ƿ��Ѿ�����
        /// </summary>
        /// <param name="BourseTypeName">����������</param>
        /// <returns></returns>
        public bool IsExistBourseTypeName(string BourseTypeName)
        {
            try
            {
                var cMBourseTypeDAL = new CM_BourseTypeDAL();
                string strWhere = string.Format("BourseTypeName='{0}'", BourseTypeName);
                DataSet _ds = cMBourseTypeDAL.GetList(strWhere);
                if (_ds != null)
                {
                    if (_ds.Tables[0].Rows.Count == 0)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errCode = "GL-4706";
                string errMsg = "�жϽ����������Ƿ��Ѿ�����ʧ��!";
                var exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ɾ�����������ͼ�������ı�

        /// <summary>
        /// ɾ�����������ͼ�������ı�
        /// </summary>
        /// <param name="BourseTypeID">����������ID</param>
        public bool DeleteCMBourseTypeAbout(int BourseTypeID)
        {
            CM_NotTradeDateDAL cM_NotTradeDateDAL = new CM_NotTradeDateDAL();
            CM_TradeTimeDAL cM_TradeTimeDAL = new CM_TradeTimeDAL();
            CM_BreedClassDAL cM_BreedClassDAL = new CM_BreedClassDAL();
            RC_MatchMachineDAL rC_MatchMachineDAL = new RC_MatchMachineDAL();
            RC_TradeCommodityAssignDAL rC_TradeCommodityAssignDAL = new RC_TradeCommodityAssignDAL();

            DbConnection Conn = null;
            Database db = DatabaseFactory.CreateDatabase();
            Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            DbTransaction Tran = Conn.BeginTransaction();
            try
            {
                int NewBourseTypeID = 5;//δ���佻������ID=5���̶�ֵ
                //��ô�ϻ������б�
                List<RC_MatchMachine> rCMatchMachineList =
                    rC_MatchMachineDAL.GetListArray(string.Format("BourseTypeID={0}", BourseTypeID), Tran, db);
                if (rCMatchMachineList.Count > 0)
                {
                    foreach (RC_MatchMachine rCMatchMode in rCMatchMachineList)
                    {
                        // List<RC_TradeCommodityAssign> rCTradeCommodityAssignList =
                        //rC_TradeCommodityAssignDAL.GetListArray(string.Format("MatchMachineID={0}", rCMatchMode.MatchMachineID), Tran, db);
                        //ɾ����ϻ�����������ͬһ����ϻ�ID�Ĵ���
                        if (!rC_TradeCommodityAssignDAL.DeleteRCTradeCommodityAByMachineID(rCMatchMode.MatchMachineID))
                        {
                            Tran.Rollback();
                            return false;
                        }

                    }
                    //���ݽ���������ID��ɾ����ϻ�
                    if (!rC_MatchMachineDAL.DeleteByBourseTypeID(BourseTypeID, Tran, db))
                    {
                        Tran.Rollback();
                        return false;
                    }

                }
                //���ݽ���������ɾ��������
                if (!cM_TradeTimeDAL.DeleteCMTradeTimeByBourseTypeID(BourseTypeID, Tran, db))
                {
                    Tran.Rollback();
                    return false;
                }
                //���ݽ���������ɾ���ǽ�����
                if (!cM_NotTradeDateDAL.DeleteByBourseTypeID(BourseTypeID, Tran, db))
                {
                    Tran.Rollback();
                    return false;
                }

                //���ݽ���������ID�����½�����ƷƷ���еĽ���������ID
                if (!cM_BreedClassDAL.UpdateBourseTypeID(BourseTypeID, NewBourseTypeID, Tran, db))
                {
                    Tran.Rollback();
                    return false;
                }
                if (!cM_BourseTypeDAL.Delete(BourseTypeID, Tran, db))
                {
                    Tran.Rollback();
                    return false;
                }
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-4701";
                string errMsg = "ɾ������������ʧ��!";
                var exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        #endregion

        #region ���ݽ���������ID��ȡ����������ʵ��

        /// <summary>
        /// ���ݽ���������ID��ȡ����������ʵ��
        /// </summary>
        /// <param name="BourseTypeID"></param>
        /// <returns></returns>
        public CM_BourseType GetModel(int BourseTypeID)
        {
            try
            {
                return cM_BourseTypeDAL.GetModel(BourseTypeID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4707";
                string errMsg = "ɾ������������ʧ��!";
                var exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region  ���½���������

        /// <summary>
        /// ���½���������
        /// </summary>
        /// <param name="model">����������ʵ��</param>
        /// <returns></returns>
        public bool UpdateCMBourseType(CM_BourseType model)
        {
            try
            {
                return cM_BourseTypeDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4702";
                string errMsg = "���½���������ʧ��!";
                var exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ��ӽ���������

        /// <summary>
        /// ��ӽ���������
        /// </summary>
        /// <param name="model">����������ʵ��</param>
        /// <returns></returns>
        public int AddCMBourseType(CM_BourseType model)
        {
            try
            {
                return cM_BourseTypeDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4700";
                string errMsg = "��ӽ���������ʧ��!";
                var exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return AppGlobalVariable.INIT_INT;
            }
        }

        #endregion

        #endregion  ��Ա����
    }
}