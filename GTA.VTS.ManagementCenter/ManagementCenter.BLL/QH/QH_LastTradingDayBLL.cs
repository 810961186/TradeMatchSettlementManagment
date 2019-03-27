using System;
using System.Collections.Generic;
using System.Data;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;
using ManagementCenter.Model.CommonClass;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///������������� ҵ���߼���QH_LastTradingDayBLL ��ժҪ˵����������뷶Χ:6050-6059
    ///���ߣ�����ΰ
    ///���ڣ�2008-11-27
    /// </summary>
    public class QH_LastTradingDayBLL
    {
        private readonly ManagementCenter.DAL.QH_LastTradingDayDAL qH_LastTradingDayDAL =
            new ManagementCenter.DAL.QH_LastTradingDayDAL();

        public QH_LastTradingDayBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_LastTradingDayDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int LastTradingDayID)
        {
            return qH_LastTradingDayDAL.Exists(LastTradingDayID);
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="model">�������ʵ��</param>
        /// <returns></returns>
        public int AddQHLastTradingDay(ManagementCenter.Model.QH_LastTradingDay model)
        {
            try
            {
                QH_LastTradingDayDAL qHLastTradingDayDAL = new QH_LastTradingDayDAL();
                return qHLastTradingDayDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6050";
                string errMsg = "����������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return AppGlobalVariable.INIT_INT;
            }
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="model">�������ʵ��</param>
        /// <returns></returns>
        public bool UpdateQHLastTradingDay(ManagementCenter.Model.QH_LastTradingDay model)
        {
            try
            {
                QH_LastTradingDayDAL qHLastTradingDayDAL = new QH_LastTradingDayDAL();
                return qHLastTradingDayDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6051";
                string errMsg = "�����������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        /// <summary>
        /// ɾ���������
        /// </summary>
        /// <param name="LastTradingDayID">�������ID</param>
        /// <returns></returns>
        public bool DeleteQHLastTradingDay(int LastTradingDayID)
        {
            try
            {
                QH_LastTradingDayDAL qHLastTradingDayDAL = new QH_LastTradingDayDAL();
                return qHLastTradingDayDAL.Delete(LastTradingDayID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6052";
                string errMsg = "ɾ���������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #region �����������ID����ȡ�������ʵ��
        /// <summary>
       /// �����������ID����ȡ�������ʵ��
       /// </summary>
       /// <param name="LastTradingDayID">�������ID</param>
       /// <returns></returns>
        public ManagementCenter.Model.QH_LastTradingDay GetQHLastTradingDayModel(int LastTradingDayID)
        {
            try
            {
                QH_LastTradingDayDAL qHLastTradingDayDAL = new QH_LastTradingDayDAL();
                return qHLastTradingDayDAL.GetModel(LastTradingDayID);

            }
            catch (Exception ex)
            {
                string errCode = "GL-6053";
                string errMsg = "�����������ID����ȡ�������ʵ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_LastTradingDay GetModelByCache(int LastTradingDayID)
        {
            string CacheKey = "QH_LastTradingDayModel-" + LastTradingDayID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_LastTradingDayDAL.GetModel(LastTradingDayID);
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
            return (ManagementCenter.Model.QH_LastTradingDay) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_LastTradingDayDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ���ݲ�ѯ������ȡ���е�������գ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_LastTradingDay> GetListArray(string strWhere)
        {
            try
            {
                return qH_LastTradingDayDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6054";
                string errMsg = "���ݲ�ѯ������ȡ���е�������գ���ѯ������Ϊ�գ�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion  ��Ա����
    }
}