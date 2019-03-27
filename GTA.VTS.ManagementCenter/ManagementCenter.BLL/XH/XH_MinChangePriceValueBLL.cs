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
    ///���������׹���_��С�䶯��λ_��Χ_ֵ ҵ���߼���XH_MinChangePriceValueBLL ��ժҪ˵����
    /// ������뷶Χ:5320-5339
    ///���ߣ�����ΰ
    ///���ڣ�2008-12-22
    /// </summary>
    public class XH_MinChangePriceValueBLL
    {
        private readonly ManagementCenter.DAL.XH_MinChangePriceValueDAL xH_MinChangePriceValueDAL =
            new ManagementCenter.DAL.XH_MinChangePriceValueDAL();

        public XH_MinChangePriceValueBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return xH_MinChangePriceValueDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int BreedClassID, int FieldRangeID)
        {
            return xH_MinChangePriceValueDAL.Exists(BreedClassID, FieldRangeID);
        }
        #region �ݲ��õĹ�������
        /// <summary>
        /// ����һ������
        /// </summary>
        //public void Add(ManagementCenter.Model.XH_MinChangePriceValue model)
        //{
        //    xH_MinChangePriceValueDAL.Add(model);
        //}

        /// <summary>
        /// ����һ������
        /// </summary>
        //public bool Update(ManagementCenter.Model.XH_MinChangePriceValue model)
        //{
        //    return xH_MinChangePriceValueDAL.Update(model);
        //}

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_MinChangePriceValue GetModel(int BreedClassID, int FieldRangeID)
        {
            return xH_MinChangePriceValueDAL.GetModel(BreedClassID, FieldRangeID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.XH_MinChangePriceValue GetModelByCache(int BreedClassID, int FieldRangeID)
        {
            string CacheKey = "XH_MinChangePriceValueModel-" + BreedClassID + FieldRangeID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = xH_MinChangePriceValueDAL.GetModel(BreedClassID, FieldRangeID);
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
            return (ManagementCenter.Model.XH_MinChangePriceValue) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return xH_MinChangePriceValueDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
#endregion

        #region ���ݲ�ѯ������ȡ���еĽ��׹���_��С�䶯��λ_��Χ_ֵ����ѯ������Ϊ�գ�
        /// <summary>
        /// ���ݲ�ѯ������ȡ���еĽ��׹���_��С�䶯��λ_��Χ_ֵ����ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.XH_MinChangePriceValue> GetListArray(string strWhere)
        {
            try
            {
                return xH_MinChangePriceValueDAL.GetListArray(strWhere);

            }
            catch (Exception ex)
            {
                string errCode = "GL-5324";
                string errMsg = "���ݲ�ѯ������ȡ���еĽ��׹���_��С�䶯��λ_��Χ_ֵ����ѯ������Ϊ�գ�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion

        #region ����ֻ���С�䶯��λ��Χֵ

        /// <summary>
        /// ����ֻ���С�䶯��λ��Χֵ
        /// </summary>
        /// <param name="xH_MinChangePriceValue">��С�䶯��λʵ��</param>
        /// <param name="cM_FieldRange">�ֶη�Χʵ��</param>
        /// <returns></returns>
        public bool AddXHMinChangePriceValue(XH_MinChangePriceValue xH_MinChangePriceValue, CM_FieldRange cM_FieldRange)
        {
            CM_FieldRangeDAL cMFieldRangeDAL = new CM_FieldRangeDAL();
            XH_MinChangePriceValueDAL xHMinChangePriceValueDAL = new XH_MinChangePriceValueDAL();
            DbConnection Conn = null;
            Database db = DatabaseFactory.CreateDatabase();
            Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            DbTransaction Tran = Conn.BeginTransaction();

            int fieldRangeID = AppGlobalVariable.INIT_INT;
            try
            {
                fieldRangeID = cMFieldRangeDAL.Add(cM_FieldRange, Tran, db);

                if (fieldRangeID != AppGlobalVariable.INIT_INT)
                {
                    xH_MinChangePriceValue.FieldRangeID = fieldRangeID;

                    xHMinChangePriceValueDAL.Add(xH_MinChangePriceValue, Tran, db);

                    Tran.Commit();
                }

                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-5320";
                string errMsg = "����ֻ���С�䶯��λ��Χֵʧ��!";
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

        #region ���½��׹���_��С�䶯��λ_��Χ_ֵ

        /// <summary>
        /// ���½��׹���_��С�䶯��λ_��Χ_ֵ
        /// </summary>
        /// <param name="xHMinChangePriceValue">���׹���_��С�䶯��λ_��Χ_ֵʵ��</param>
        /// <param name="cMFieldRange">�ֶη�Χֵʵ��</param>
        /// <returns></returns>
        public bool UpdateMinChangePriceValue(CM_FieldRange cMFieldRange, XH_MinChangePriceValue xHMinChangePriceValue)
        {
            CM_FieldRangeDAL cMFieldRangeDAL = new CM_FieldRangeDAL();
            XH_MinChangePriceValueDAL xHMinChangePriceValueDAL = new XH_MinChangePriceValueDAL();
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
                cMFieldRangeDAL.Update(cMFieldRange, Tran, db);
                xHMinChangePriceValueDAL.Update(xHMinChangePriceValue, Tran, db);
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-5322";
                string errMsg = "���½��׹���_��С�䶯��λ_��Χ_ֵʧ��!";
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

        #region ����Ʒ��ID���ֶη�ΧID��ɾ����С�䶯��λ_��Χ_ֵ

        /// <summary>
        /// ����Ʒ��ID���ֶη�ΧID��ɾ����С�䶯��λ_��Χ_ֵ
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="FieldRangeID">�ֶη�ΧID</param>
        /// <returns></returns>
        public bool DeleteMinChangePriceValue(int BreedClassID, int FieldRangeID)
        {
            CM_FieldRangeDAL cMFieldRangeDAL = new CM_FieldRangeDAL();
            XH_MinChangePriceValueDAL xHMinChangePriceValueDAL = new XH_MinChangePriceValueDAL();
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
                xHMinChangePriceValueDAL.Delete(BreedClassID, FieldRangeID, Tran, db);
                cMFieldRangeDAL.Delete(FieldRangeID, Tran, db);
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-5321";
                string errMsg = "����Ʒ��ID���ֶη�ΧID��ɾ����С�䶯��λ_��Χ_ֵʧ��!";
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

        #region ����Ʒ��ID��ȡ���׹���_��С�䶯��λ_��Χ_ֵ

        /// <summary>
        /// ����Ʒ��ID��ȡ���׹���_��С�䶯��λ_��Χ_ֵ
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public DataSet GetMinChangePriceFieldRangeByBreedClassID(int BreedClassID)
        {
            try
            {
                XH_MinChangePriceValueDAL xHMinChangePriceValueDAL = new XH_MinChangePriceValueDAL();
                return xHMinChangePriceValueDAL.GetMinChangePriceFieldRangeByBreedClassID(BreedClassID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5323";
                string errMsg = "����Ʒ��ID��ȡ���׹���_��С�䶯��λ_��Χ_ֵʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #endregion  ��Ա����
    }
}