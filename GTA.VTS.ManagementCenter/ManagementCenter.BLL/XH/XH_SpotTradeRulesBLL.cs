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
    ///�������ֻ�_Ʒ��_���׹��� ҵ���߼���XH_SpotTradeRulesBLL ��ժҪ˵����������뷶Χ:5200-5219
    ///���ߣ�����ΰ
    ///���ڣ�2008-11-27
    /// </summary>
    public class XH_SpotTradeRulesBLL
    {
        private readonly ManagementCenter.DAL.XH_SpotTradeRulesDAL xH_SpotTradeRulesDAL =
            new ManagementCenter.DAL.XH_SpotTradeRulesDAL();

        public XH_SpotTradeRulesBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return xH_SpotTradeRulesDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int BreedClassID)
        {
            return xH_SpotTradeRulesDAL.Exists(BreedClassID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Add(ManagementCenter.Model.XH_SpotTradeRules model)
        {
            try
            {
                return xH_SpotTradeRulesDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.Message, ex);
                return false;
                //throw;
            }
        }

        #region �����ֻ����׹���

        /// <summary>
        /// �����ֻ����׹���
        /// </summary>
        /// <param name="model">�ֻ�_Ʒ��_���׹���ʵ��</param>
        /// <returns></returns>
        public bool UpdateSpotTradeRules(ManagementCenter.Model.XH_SpotTradeRules model)
        {
            try
            {
                return xH_SpotTradeRulesDAL.UpdateSpotTradeRules(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5203";
                string errMsg = "�����ֻ����׹���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int BreedClassID)
        {
            xH_SpotTradeRulesDAL.Delete(BreedClassID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_SpotTradeRules GetModel(int BreedClassID)
        {
            return xH_SpotTradeRulesDAL.GetModel(BreedClassID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.XH_SpotTradeRules GetModelByCache(int BreedClassID)
        {
            string CacheKey = "XH_SpotTradeRulesModel-" + BreedClassID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = xH_SpotTradeRulesDAL.GetModel(BreedClassID);
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
            return (ManagementCenter.Model.XH_SpotTradeRules) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return xH_SpotTradeRulesDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region ���ݲ�ѯ������ȡ���е��ֻ�_Ʒ��_���׹��򣨲�ѯ������Ϊ�գ�

        /// <summary>
        /// ���ݲ�ѯ������ȡ���е��ֻ�_Ʒ��_���׹��򣨲�ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.XH_SpotTradeRules> GetListArray(string strWhere)
        {
            try
            {
                return xH_SpotTradeRulesDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5205";
                string errMsg = "���ݲ�ѯ������ȡ���е��ֻ�_Ʒ��_���׹���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// �����ֻ�������е�Ʒ��ID��ȡƷ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetBreedClassNameByBreedClassID()
        {
            try
            {
                return xH_SpotTradeRulesDAL.GetBreedClassNameByBreedClassID();
            }
            catch (Exception ex)
            {
                string errCode = "GL-5206";
                string errMsg = "�����ֻ�������е�Ʒ��ID��ȡƷ������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion  ��Ա����

        #region ����ֻ����׹���

        /// <summary>
        ///  ����ֻ����׹���
        /// </summary>
        /// <param name="xHSpotTradeRules">�ֻ����׹���ʵ����</param>
        /// <returns></returns>
        public bool AddXHSpotTradeRules(ManagementCenter.Model.XH_SpotTradeRules xHSpotTradeRules)
        {
            try
            {
                XH_SpotTradeRulesDAL xHSpotTradeRulesDAL = new XH_SpotTradeRulesDAL();
                return xHSpotTradeRulesDAL.Add(xHSpotTradeRules);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5200";
                string errMsg = "����ֻ����׹���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
            //return true;
        }

        #endregion

        #region ����Ʒ�ֱ�ʶ,Ʒ���ǵ�����ʶ,Ʒ����Ч�걨��ʶ,ɾ���ֻ�Ʒ�ֽ��׹���

        /// <summary>
        ///����Ʒ�ֱ�ʶ,Ʒ���ǵ�����ʶ,Ʒ����Ч�걨��ʶ,ɾ���ֻ�Ʒ�ֽ��׹��� 
        /// </summary>
        /// <param name="BreedClassID">Ʒ�ֱ�ʶ</param>
        /// <param name="BreedClassHighLowID">Ʒ���ǵ�����ʶ</param>
        /// <param name="BreedClassValidID">Ʒ����Ч�걨��ʶ</param>
        /// <returns></returns>
        public bool DeleteSpotTradeRules(int BreedClassID, int BreedClassHighLowID, int BreedClassValidID)
        {
            XH_SpotTradeRulesDAL xHSpotTradeRulesDAL = new XH_SpotTradeRulesDAL();
            XH_SpotHighLowControlTypeBLL xHSpotHighLowControlTypeBLL = new XH_SpotHighLowControlTypeBLL();
            XH_ValidDeclareTypeBLL xHValidDeclareTypeBLL = new XH_ValidDeclareTypeBLL();

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
                //?��ɾ�����׹���_���׷���_���׵�λ_������(��С���׵�λ)
                xHSpotTradeRulesDAL.Delete(BreedClassID, Tran, db);
                if (BreedClassHighLowID != AppGlobalVariable.INIT_INT)
                {
                    if (xHSpotHighLowControlTypeBLL.DeleteSpotHighLowValue(BreedClassHighLowID, Tran, db, true))
                    {
                        if (BreedClassValidID != AppGlobalVariable.INIT_INT)
                        {
                            if (xHValidDeclareTypeBLL.DeleteValidDeclareValue(BreedClassValidID, Tran, db, true))
                            {
                                Tran.Commit();
                                return true;
                            }
                        }
                    }
                }
                Tran.Rollback();
                return false;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-5201";
                string errMsg = "����Ʒ�ֱ�ʶ,Ʒ���ǵ�����ʶ,Ʒ����Ч�걨��ʶ,ɾ���ֻ�Ʒ�ֽ��׹���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);

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

        #region ����Ʒ�ֱ�ʶ,Ʒ���ǵ�����ʶ,Ʒ����Ч�걨��ʶ,ɾ���ֻ�Ʒ�ֽ��׹���(������ر�ȫ��ɾ��)

        /// <summary>
        ///����Ʒ�ֱ�ʶ,Ʒ���ǵ�����ʶ,Ʒ����Ч�걨��ʶ,ɾ���ֻ�Ʒ�ֽ��׹���(������ر�ȫ��ɾ��) 
        /// </summary>
        /// <param name="BreedClassID">Ʒ�ֱ�ʶ</param>
        ///// <param name="BreedClassHighLowID">Ʒ���ǵ�����ʶ</param>
        ///// <param name="BreedClassValidID">Ʒ����Ч�걨��ʶ</param>
        /// <returns></returns>
        public bool DeleteSpotTradeRulesAboutAll(int BreedClassID)
        {
            XH_SpotTradeRulesDAL xHSpotTradeRulesDAL = new XH_SpotTradeRulesDAL();
            XH_SpotPositionDAL xHSpotPositionDAL = new XH_SpotPositionDAL();
            XH_SpotHighLowValueDAL xHSpotHighLowValueDAL = new XH_SpotHighLowValueDAL();
            XH_ValidDeclareValueDAL xHValidDeclareValueDAL = new XH_ValidDeclareValueDAL();
            //XH_MinChangePriceValueDAL xHMinChangePriceValueDAL = new XH_MinChangePriceValueDAL();
            CM_FieldRangeDAL cMFieldRangeDAL = new CM_FieldRangeDAL();
            XH_ValidDeclareTypeDAL xHValidDeclareTypeDAL = new XH_ValidDeclareTypeDAL();
            XH_MinVolumeOfBusinessDAL xHMinVolumeOfBusinessDAL = new XH_MinVolumeOfBusinessDAL();

            XH_SpotHighLowControlTypeDAL xHSpotHighLowControlTypeDAL = new XH_SpotHighLowControlTypeDAL();
            CM_UnitConversionDAL cM_UnitConversionDAL=new CM_UnitConversionDAL();


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
                int BreedClassHighLowID = AppGlobalVariable.INIT_INT;
                int BreedClassValidID = AppGlobalVariable.INIT_INT;
                XH_SpotTradeRules xHSpotTradeRules = new XH_SpotTradeRules();
                xHSpotTradeRules = xHSpotTradeRulesDAL.GetModel(BreedClassID);
                if (xHSpotTradeRules != null)
                {
                    if (!string.IsNullOrEmpty(xHSpotTradeRules.BreedClassHighLowID.ToString()))
                {
                    BreedClassHighLowID = Convert.ToInt32(xHSpotTradeRules.BreedClassHighLowID);
                }
                if (!string.IsNullOrEmpty(xHSpotTradeRules.BreedClassValidID.ToString()))
                {
                    BreedClassValidID = Convert.ToInt32(xHSpotTradeRules.BreedClassValidID);
                }
                if (BreedClassHighLowID != AppGlobalVariable.INIT_INT)
                {
                    if (!xHSpotHighLowValueDAL.DeleteSpotHighLowValue(BreedClassHighLowID, Tran, db))
                    {
                        Tran.Rollback();
                        return false;
                    }
                }
                if (BreedClassValidID != AppGlobalVariable.INIT_INT)
                {
                    if (!xHValidDeclareValueDAL.DeleteVDeclareValue(BreedClassValidID, Tran, db))
                    {
                        Tran.Rollback();
                        return false;
                    }
                }
                //if (!xHMinChangePriceValueDAL.Delete(BreedClassID, Tran, db))
                //{
                //    Tran.Rollback();
                //    return false;
                //}

                //List<Model.XH_MinChangePriceValue> xhMinCHangePriceV =
                //    xHMinChangePriceValueDAL.GetListArray(string.Format("BreedClassID={0}", BreedClassID), Tran, db);
                //foreach (Model.XH_MinChangePriceValue FieldRangeID in xhMinCHangePriceV)
                //{
                //    if (!cMFieldRangeDAL.Delete(FieldRangeID.FieldRangeID))
                //    {
                //        Tran.Rollback();
                //        return false;
                //    }
                //}
                if (!xHMinVolumeOfBusinessDAL.DeleteXHMinVolumeOfBusByBreedClassID(BreedClassID, Tran, db))
                {
                    Tran.Rollback();
                    return false;
                }
                //���ɾ���ֻ���λ����
                List<Model.CM_UnitConversion> cMUnitC =
                    cM_UnitConversionDAL.GetListArray(string.Format("BreedClassID={0}", BreedClassID));
                foreach (Model.CM_UnitConversion unitConversion in cMUnitC)
                {
                    if (!cM_UnitConversionDAL.DeleteUnitConversionByBreedClassID(Convert.ToInt32(unitConversion.BreedClassID),Tran,db))
                    {
                        Tran.Rollback();
                        return false;
                    }
                }

                if (!xHSpotTradeRulesDAL.Delete(BreedClassID, Tran, db))
                {
                    Tran.Rollback();
                    return false;
                }
                if (!xHValidDeclareTypeDAL.DeleteValidDeclareType(BreedClassValidID, Tran, db))
                {
                    Tran.Rollback();
                    return false;
                }
                if (!xHSpotHighLowControlTypeDAL.Delete(BreedClassHighLowID, Tran, db))
                {
                    Tran.Rollback();
                    return false;
                }
                }
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-5202";
                string errMsg = "����Ʒ�ֱ�ʶ,Ʒ���ǵ�����ʶ,Ʒ����Ч�걨��ʶ,ɾ���ֻ�Ʒ�ֽ��׹���(������ر�ȫ��ɾ��)ʧ��!";
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

        #region ��ȡ�����ֻ����׹���

        /// <summary>
        /// ��ȡ�����ֻ����׹���
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllSpotTradeRules(string BreedClassName, int pageNo, int pageSize,
                                            out int rowCount)
        {
            try
            {
                XH_SpotTradeRulesDAL xHSpotTradeRulesDAL = new XH_SpotTradeRulesDAL();
                return xHSpotTradeRulesDAL.GetAllSpotTradeRules(BreedClassName, pageNo, pageSize,
                                                                out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-5204";
                string errMsg = "��ȡ�����ֻ����׹���ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);

                return null;
            }
        }

        #endregion

        #region ����Ʒ��ID�����ֻ�������ϸ(��Ʒ�ֵ��ǵ�������Ч�걨)����

        /// <summary>
        /// ����Ʒ��ID�����ֻ�������ϸ(��Ʒ�ֵ��ǵ�������Ч�걨)����
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public DataSet GetSpotTradeRulesDetail(int BreedClassID)
        {
            try
            {
                XH_SpotTradeRulesDAL xHSpotTradeRulesDAL = new XH_SpotTradeRulesDAL();
                return xHSpotTradeRulesDAL.GetSpotTradeRulesDetail(BreedClassID);
            }
            catch (Exception ex)
            {
                GTA.VTS.Common.CommonUtility.LogHelper.WriteError(ex.Message, ex);
                return null;
            }
        }

        #endregion
    }
}