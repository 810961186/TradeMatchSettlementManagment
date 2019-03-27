using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;
using ManagementCenter.Model.CommonClass;
using Microsoft.Practices.EnterpriseLibrary.Data;
using GTA.VTS.Common.CommonUtility;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///������Ʒ��_�ֻ�_���׷���_�ɽ���_���������� ҵ���߼���XH_SpotRangeCostBLL ��ժҪ˵����
    ///���ߣ�����ΰ
    ///���ڣ�2008-11-27
    /// </summary>
    public class XH_SpotRangeCostBLL
    {
        private readonly ManagementCenter.DAL.XH_SpotRangeCostDAL xH_SpotRangeCostDAL =
            new ManagementCenter.DAL.XH_SpotRangeCostDAL();

        public XH_SpotRangeCostBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return xH_SpotRangeCostDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int FieldRangeID, int BreedClassID)
        {
            return xH_SpotRangeCostDAL.Exists(FieldRangeID, BreedClassID);
        }

        #region ����ֻ����׷��ý���������

        /// <summary>
        /// ����ֻ����׷��ý���������
        /// </summary>
        /// <param name="xH_SpotRangeCost">�ֻ�����������ʵ��</param>
        /// <param name="cM_FieldRange">�ֶη�Χʵ��</param>
        /// <returns></returns>
        public bool AddXHSpotRangeCost(XH_SpotRangeCost xH_SpotRangeCost, CM_FieldRange cM_FieldRange)
        {
            CM_FieldRangeDAL cMFieldRangeDAL = new CM_FieldRangeDAL();
            XH_SpotRangeCostDAL xHSpotRangeCostDAL = new XH_SpotRangeCostDAL();
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
                    //xH_SpotRangeCost.FieldRangeID = fieldRangeID;

                    xHSpotRangeCostDAL.Add(xH_SpotRangeCost, Tran, db);

                    Tran.Commit();
                }

                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                LogHelper.WriteError(ex.Message, ex);
                return false;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        #endregion

        #region ���½��׷��ý���������_��Χ_ֵ

        /// <summary>
        /// ���½��׷��ý���������_��Χ_ֵ
        /// </summary>
        /// <param name="xH_SpotRangeCost">����������_��Χ_ֵʵ����</param>
        /// <param name="cMFieldRange">�ֶη�Χֵʵ��</param>
        /// <returns></returns>
        public bool UpdateSpotRangeCost(CM_FieldRange cMFieldRange, XH_SpotRangeCost xH_SpotRangeCost)
        {
            CM_FieldRangeDAL cMFieldRangeDAL = new CM_FieldRangeDAL();
            XH_SpotRangeCostDAL xHSpotRangeCostDAL = new XH_SpotRangeCostDAL();
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
                xHSpotRangeCostDAL.Update(xH_SpotRangeCost, Tran, db);
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                GTA.VTS.Common.CommonUtility.LogHelper.WriteError(ex.Message, ex);
                return false;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        #endregion

        #region ����Ʒ��ID���ֶη�ΧID��ɾ������������_��Χ_ֵ

        /// <summary>
        /// ����Ʒ��ID���ֶη�ΧID��ɾ������������_��Χ_ֵ
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <param name="FieldRangeID">�ֶη�ΧID</param>
        /// <returns></returns>
        public bool DeleteSpotRangeCost(int BreedClassID, int FieldRangeID)
        {
            CM_FieldRangeDAL cMFieldRangeDAL = new CM_FieldRangeDAL();
            XH_SpotRangeCostDAL xHSpotRangeCostDAL = new XH_SpotRangeCostDAL();
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
                xHSpotRangeCostDAL.Delete(BreedClassID, FieldRangeID, Tran, db);
                cMFieldRangeDAL.Delete(FieldRangeID, Tran, db);
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                GTA.VTS.Common.CommonUtility.LogHelper.WriteError(ex.Message, ex);
                return false;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        #endregion

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_SpotRangeCost GetModel(int FieldRangeID, int BreedClassID)
        {
            return xH_SpotRangeCostDAL.GetModel(FieldRangeID, BreedClassID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.XH_SpotRangeCost GetModelByCache(int FieldRangeID, int BreedClassID)
        {
            string CacheKey = "XH_SpotRangeCostModel-" + FieldRangeID + BreedClassID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = xH_SpotRangeCostDAL.GetModel(FieldRangeID, BreedClassID);
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
            return (ManagementCenter.Model.XH_SpotRangeCost) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return xH_SpotRangeCostDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ���ݲ�ѯ������ȡ���е�Ʒ��_�ֻ�_���׷���_�ɽ���_���������ѣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.XH_SpotRangeCost> GetListArray(string strWhere)
        {
            return xH_SpotRangeCostDAL.GetListArray(strWhere);
        }

        #region ����Ʒ��ID��ȡ�ֻ����׷��ý���������_��Χ_ֵ

        /// <summary>
        /// ����Ʒ��ID��ȡ�ֻ����׷��ý���������_��Χ_ֵ
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public DataSet GetXHSpotRangeCostByBreedClassID(int BreedClassID)
        {
            try
            {
                XH_SpotRangeCostDAL xHSpotRangeCostDAL = new XH_SpotRangeCostDAL();
                return xHSpotRangeCostDAL.GetXHSpotRangeCostByBreedClassID(BreedClassID);
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