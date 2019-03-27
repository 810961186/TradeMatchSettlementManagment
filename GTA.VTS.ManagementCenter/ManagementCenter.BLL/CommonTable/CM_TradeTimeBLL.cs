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
    ///����������������_����ʱ�� ҵ���߼���CM_TradeTimeBLL ��ժҪ˵����������뷶Χ:4740-4759
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class CM_TradeTimeBLL
    {
        /// <summary>
        /// ����������_����ʱ�� DAL
        /// </summary>
        private readonly ManagementCenter.DAL.CM_TradeTimeDAL cM_TradeTimeDAL =
            new ManagementCenter.DAL.CM_TradeTimeDAL();

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public CM_TradeTimeBLL()
        {
        }
        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return cM_TradeTimeDAL.GetMaxId();
        }

        /// <summary>
        /// ���ݽ���ʱ��ID�ж��Ƿ��Ѵ��ڽ���ʱ���¼
        /// </summary>
        /// <param name="TradeTimeID">����ʱ��ID</param>
        /// <returns></returns>
        public bool ExistsCMTradeTime(int TradeTimeID)
        {
            try
            {
                return cM_TradeTimeDAL.Exists(TradeTimeID);

            }
            catch (Exception ex)
            {
                string errCode = "GL-4747";
                string errMsg = " ���ݽ���ʱ��ID�ж��Ƿ��Ѵ��ڽ���ʱ���¼ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        /// <summary>
        ///�ȽϽ���ʱ�� 
        /// </summary>
        /// <param name="cMTradeTimeList">����ʱ�伯��</param>
        /// <returns></returns>
        public CM_TradeTime CompareTradeTime(List<CM_TradeTime> cMTradeTimeList)
        {
            try
            {
                CM_TradeTime cM_TradeTime = new CM_TradeTime();
                DateTime _tempStartTime = AppGlobalVariable.INIT_DATETIME;
                DateTime _tempEndTime = AppGlobalVariable.INIT_DATETIME;
                DateTime _changeStartTime = AppGlobalVariable.INIT_DATETIME;//ĳ����¼�Ŀ�ʼʱ��
                DateTime _changeEndTime = AppGlobalVariable.INIT_DATETIME;//ĳ����¼�Ľ���ʱ��
                string timeFormat = string.Empty;//��ʱ���ʽ
                bool isStartTime = true; //��־��ʼֵ�Ƿ�ı�
                bool isEndTime = true; //��־����ֵ�Ƿ�ı�
                for (int i = 0; i < cMTradeTimeList.Count; i++)
                {
                    timeFormat = ((DateTime)cMTradeTimeList[i].StartTime).ToString("HH:mm");
                    _changeStartTime = Convert.ToDateTime(timeFormat);
                    timeFormat = ((DateTime)cMTradeTimeList[i].EndTime).ToString("HH:mm");
                    _changeEndTime = Convert.ToDateTime(timeFormat);
                    if (isStartTime)
                    {
                        timeFormat = ((DateTime)cMTradeTimeList[0].StartTime).ToString("HH:mm");
                        _tempStartTime = Convert.ToDateTime(timeFormat); //ת���ɵ�ǰ���ڵ�ʱ��
                        cM_TradeTime.StartTime = _tempStartTime;
                    }
                    if (isEndTime)
                    {
                        timeFormat = ((DateTime)cMTradeTimeList[0].EndTime).ToString("HH:mm");
                        _tempEndTime = Convert.ToDateTime(timeFormat); //ת���ɵ�ǰ���ڵ�ʱ��
                        cM_TradeTime.EndTime = _tempEndTime;
                    }
                    if (_tempStartTime > _changeStartTime)
                    {
                        _tempStartTime = _changeStartTime;
                        cM_TradeTime.StartTime = _tempStartTime;
                        isStartTime = false;
                    }
                    if (_tempEndTime < _changeEndTime)
                    {
                        _tempEndTime = _changeEndTime;
                        cM_TradeTime.EndTime = _tempEndTime;
                        isEndTime = false;
                    }
                }
                return cM_TradeTime;
            }
            catch (Exception ex)
            {
                string errCode = "GL-4749";
                string errMsg = "�ȽϽ���ʱ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #region ��ӽ���ʱ��

        /// <summary>
        /// ��ӽ���ʱ��
        /// </summary>
        /// <param name="model">����������_����ʱ��ʵ��</param>
        /// <returns></returns>
        public int AddCMTradeTime(ManagementCenter.Model.CM_TradeTime model)
        {
            DbConnection Conn = null;
            Database db = DatabaseFactory.CreateDatabase();
            Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            DbTransaction tran = Conn.BeginTransaction();
            try
            {
                CM_TradeTimeDAL cM_TradeTimeDAL = new CM_TradeTimeDAL();
                CM_BourseTypeDAL cM_BourseTypeDAL = new CM_BourseTypeDAL();
                //int tradeTimeID = (int)model.TradeTimeID;
                int addTTimeResult = AppGlobalVariable.INIT_INT;
                //����������ID
                int bourseTypeID = AppGlobalVariable.INIT_INT;
                bourseTypeID = (int)model.BourseTypeID;
                addTTimeResult = cM_TradeTimeDAL.Add(model, tran, db);
                if (addTTimeResult == AppGlobalVariable.INIT_INT)
                {
                    tran.Rollback();
                    return AppGlobalVariable.INIT_INT;
                }
                if (bourseTypeID != AppGlobalVariable.INIT_INT)
                {
                    //���ݽ���������ID��ȡ����������Ľ��׿�ʼʱ�������Ľ��׽���ʱ��
                    List<CM_TradeTime> cmTTimeList =
                        cM_TradeTimeDAL.GetListArray(string.Format("BourseTypeID={0}", bourseTypeID), tran, db);
                    if (cmTTimeList.Count > 0)
                    {
                        CM_TradeTime cmTradeTime = CompareTradeTime(cmTTimeList);
                        if (cmTradeTime != null)
                        {
                            bool result = cM_BourseTypeDAL.Update(bourseTypeID, (DateTime)cmTradeTime.StartTime,
                                                                  (DateTime)cmTradeTime.EndTime, tran,
                                                                  db);
                            if (!result)
                            {
                                tran.Rollback();
                                return AppGlobalVariable.INIT_INT;
                            }
                        }
                        tran.Commit();
                    }
                }
                return addTTimeResult;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                string errCode = "GL-4740";
                string errMsg = "��ӽ���ʱ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
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

        #region ��ӽ���ʱ�� ����
        /// <summary>
        /// ��ӽ���ʱ��
        /// </summary>
        /// <param name="model">����������_����ʱ��ʵ��</param>
        /// <param name="msg">���ش�������ʾ��Ϣ</param>
        /// <returns></returns>
        //public int AddCMTradeTime(ManagementCenter.Model.CM_TradeTime model, ref string msg)
        //{
        //    try
        //    {
        //        string m_msg = string.Empty;
        //        if (model.StartTime > model.EndTime)
        //        {
        //            m_msg += "���׿�ʼʱ�䲻�ܴ��ڽ��׽���ʱ��!" + "\t\n";
        //        }
        //        if (model.EndTime < model.StartTime)
        //        {
        //            m_msg += "���׽���ʱ�䲻��С�ڽ��׿�ʼʱ��!" + "\t\n";
        //        }
        //        if (model.StartTime == model.EndTime)
        //        {
        //            m_msg += "���׿�ʼʱ��ͽ��׽���ʱ�䲻�����!" + "\t\n";
        //        }
        //        msg = m_msg;
        //        if (!string.IsNullOrEmpty(msg))
        //        {
        //            return AppGlobalVariable.INIT_INT;
        //        }
        //        return cM_TradeTimeDAL.Add(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        string errCode = "GL-4740";
        //        string errMsg = "��ӽ���ʱ��ʧ��!";
        //        VTException exception = new VTException(errCode, errMsg, ex);
        //        LogHelper.WriteError(exception.ToString(), exception.InnerException);
        //        return AppGlobalVariable.INIT_INT;
        //    }
        //}
        #endregion

        #region ���½���ʱ��

        /// <summary>
        /// ���½���ʱ��
        /// </summary>
        /// <param name="model">����������_����ʱ��ʵ��</param>
        /// <returns></returns>
        public bool UpdateCMTradeTime(ManagementCenter.Model.CM_TradeTime model)
        {
            DbConnection Conn = null;
            Database db = DatabaseFactory.CreateDatabase();
            Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            DbTransaction tran = Conn.BeginTransaction();
            try
            {
                // return cM_TradeTimeDAL.Update(model);
                CM_TradeTimeDAL cM_TradeTimeDAL = new CM_TradeTimeDAL();
                CM_BourseTypeDAL cM_BourseTypeDAL = new CM_BourseTypeDAL();
                //����������ID
                int bourseTypeID = AppGlobalVariable.INIT_INT;
                bourseTypeID = (int)model.BourseTypeID;
                bool updateTTimeResult = cM_TradeTimeDAL.Update(model, tran, db);
                if (!updateTTimeResult)
                {
                    tran.Rollback();
                    return false;
                }
                if (bourseTypeID != AppGlobalVariable.INIT_INT)
                {
                    //���ݽ���������ID��ȡ����������Ľ��׿�ʼʱ�������Ľ��׽���ʱ��
                    List<CM_TradeTime> cmTTimeList =
                        cM_TradeTimeDAL.GetListArray(string.Format("BourseTypeID={0}", bourseTypeID), tran, db);
                    if (cmTTimeList.Count > 0)
                    {
                       CM_TradeTime cmTradeTime = CompareTradeTime(cmTTimeList);
                       if (cmTradeTime != null)
                       {
                           bool result = cM_BourseTypeDAL.Update(bourseTypeID, (DateTime) cmTradeTime.StartTime,
                                                                 (DateTime) cmTradeTime.EndTime, tran,
                                                                 db);
                           if (!result)
                           {
                               tran.Rollback();
                               return false;
                           }
                       }
                        tran.Commit();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                string errCode = "GL-4742";
                string errMsg = "���½���ʱ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
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

        #region ���ݽ���ʱ��ID��ɾ������ʱ��

        /// <summary>
        /// ���ݽ���ʱ��ID��ɾ������ʱ��
        /// </summary>
        /// <param name="TradeTimeID">����ʱ��ID</param>
        /// <returns></returns>
        public bool DeleteCMTradeTime(int TradeTimeID)
        {
            DbConnection Conn = null;
            Database db = DatabaseFactory.CreateDatabase();
            Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            DbTransaction tran = Conn.BeginTransaction();
            try
            {
                //return cM_TradeTimeDAL.Delete(TradeTimeID);
                CM_TradeTimeDAL cM_TradeTimeDAL = new CM_TradeTimeDAL();
                CM_BourseTypeDAL cM_BourseTypeDAL = new CM_BourseTypeDAL();
                CM_TradeTime cMTradeTime = cM_TradeTimeDAL.GetModel(TradeTimeID);
                if (cMTradeTime != null)
                {
                    //����������ID
                    int bourseTypeID = AppGlobalVariable.INIT_INT;
                    bourseTypeID = (int)cMTradeTime.BourseTypeID;
                    bool deleTTimeResult = cM_TradeTimeDAL.Delete(TradeTimeID);
                    if (!deleTTimeResult)
                    {
                        tran.Rollback();
                        return false;
                    }
                    if (bourseTypeID != AppGlobalVariable.INIT_INT)
                    {
                        //���ݽ���������ID��ȡ����������Ľ��׿�ʼʱ�������Ľ��׽���ʱ��
                        List<CM_TradeTime> cmTTimeList =
                            cM_TradeTimeDAL.GetListArray(string.Format("BourseTypeID={0}", bourseTypeID), tran, db);
                        if (cmTTimeList.Count > 0)
                        {
                           CM_TradeTime cmTradeTime = CompareTradeTime(cmTTimeList);
                           if (cmTradeTime != null)
                           {
                               bool result = cM_BourseTypeDAL.Update(bourseTypeID, (DateTime) cmTradeTime.StartTime,
                                                                     (DateTime) cmTradeTime.EndTime, tran,
                                                                     db);
                               if (!result)
                               {
                                   tran.Rollback();
                                   return false;
                               }
                           }
                            tran.Commit();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                string errCode = "GL-4741";
                string errMsg = " ���ݽ���ʱ��ID��ɾ������ʱ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
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

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.CM_TradeTime GetModel(int TradeTimeID)
        {
            return cM_TradeTimeDAL.GetModel(TradeTimeID);
        }

        #region ��ȡ���н���������_����ʱ��

        /// <summary>
        /// ��ȡ���н���������_����ʱ��
        /// </summary>
        /// <param name="BourseTypeName">��������������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMTradeTime(string BourseTypeName, int pageNo, int pageSize,
                                         out int rowCount)
        {
            try
            {
                CM_TradeTimeDAL cMTradeTimeDAL = new CM_TradeTimeDAL();
                return cMTradeTimeDAL.GetAllCMTradeTime(BourseTypeName, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-4743";
                string errMsg = " ��ȡ���н���������_����ʱ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region  ���ݽ���������_����ʱ����еĽ���������ID��ȡ��������������

        /// <summary>
        /// ���ݽ���������_����ʱ����еĽ���������ID��ȡ��������������
        /// </summary>
        /// <returns></returns>
        public DataSet GetBourseTypeNameByBourseTypeID()
        {
            try
            {
                CM_TradeTimeDAL cMTradeTimeDAL = new CM_TradeTimeDAL();
                return cMTradeTimeDAL.GetBourseTypeNameByBourseTypeID();
            }
            catch (Exception ex)
            {
                string errCode = "GL-4744";
                string errMsg = " ���ݽ���������_����ʱ����еĽ���������ID��ȡ��������������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_TradeTime GetModelByCache(int TradeTimeID)
        {
            string CacheKey = "CM_TradeTimeModel-" + TradeTimeID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_TradeTimeDAL.GetModel(TradeTimeID);
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
            return (ManagementCenter.Model.CM_TradeTime)objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return cM_TradeTimeDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region ���ݲ�ѯ������ȡ���еĽ���������_����ʱ�䣨��ѯ������Ϊ�գ�

        /// <summary>
        /// ���ݲ�ѯ������ȡ���еĽ���������_����ʱ�䣨��ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_TradeTime> GetListArray(string strWhere)
        {
            try
            {
                return cM_TradeTimeDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4745";
                string errMsg = " ���ݲ�ѯ������ȡ���еĽ���������_����ʱ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ���ݽ��������ͷ��ؽ�����ʱ��

        /// <summary>
        /// ���ݽ��������ͷ��ؽ�����ʱ��
        /// </summary>
        /// <param name="BourseTypeID">����������</param>
        /// <returns></returns>
        public DataSet GetTradeTimeByBourseTypeID(int BourseTypeID)
        {
            try
            {
                return cM_TradeTimeDAL.GetList(string.Format("BourseTypeID={0}", BourseTypeID));
            }
            catch (Exception ex)
            {
                string errCode = "GL-4746";
                string errMsg = " ���ݽ��������ͷ��ؽ�����ʱ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ���ݽ���������ID���ؽ���ʱ��
        /// <summary>
        /// ���ݽ���������ID���ؽ���ʱ��
        /// </summary>
        /// <param name="bourseTypeID">����������ID</param>
        /// <returns></returns>
        public DataSet GetTradeTimeAndBourseTypeList(int bourseTypeID)
        {
            try
            {
                CM_TradeTimeDAL cmTradeTimeDAL = new CM_TradeTimeDAL();
                return cmTradeTimeDAL.GetTradeTimeAndBourseTypeList(bourseTypeID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4748";
                string errMsg = " ���ݽ���������ID���ؽ���ʱ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion

        #endregion  ��Ա����
    }
}