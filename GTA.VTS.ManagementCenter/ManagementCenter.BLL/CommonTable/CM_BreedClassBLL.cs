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
using Types = GTA.VTS.Common.CommonObject.Types;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///������������ƷƷ�� ҵ���߼���CM_BreedClassBLL ��ժҪ˵����������뷶Χ:4100-4130
    ///���ߣ�����ΰ
    ///����:2008-11-21
    /// </summary>
    public class CM_BreedClassBLL
    {
        private readonly ManagementCenter.DAL.CM_BreedClassDAL cM_BreedClassDAL =
            new ManagementCenter.DAL.CM_BreedClassDAL();

        public CM_BreedClassBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        ///��ȡ������ƷƷ�ֱ�����ID 
        /// </summary>
        /// <returns></returns>
        public int GetCMBreedClassMaxId()
        {
            try
            {
                return cM_BreedClassDAL.GetMaxId();

            }
            catch (Exception ex)
            {
                string errCode = "GL-4116";
                string errMsg = "��ȡ������ƷƷ�ֱ�����IDʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return AppGlobalVariable.INIT_INT;
            }
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int BreedClassID)
        {
            return cM_BreedClassDAL.Exists(BreedClassID);
        }

        /// <summary>
        /// ��ӽ�����ƷƷ��
        /// </summary>
        /// <param name="model">������ƷƷ��ʵ��</param>
        /// <returns></returns>
        //public int AddCMBreedClass(CM_BreedClass model)
        public bool AddCMBreedClass(CM_BreedClass model)
        {
            try
            {
                return cM_BreedClassDAL.Add(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4100";
                string errMsg = "��ӽ�����ƷƷ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                //return AppGlobalVariable.INIT_INT;
                return false;
            }
        }

        /// <summary>
        /// ���½�����ƷƷ��
        /// </summary>
        /// <param name="model">������ƷƷ��ʵ��</param>
        /// <returns></returns>
        public bool UpdateCMBreedClass(CM_BreedClass model)
        {
            try
            {
                return cM_BreedClassDAL.Update(model);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4102";
                string errMsg = "���½�����ƷƷ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        /// <summary>
        /// ɾ��������ƷƷ��
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool DeleteCMBreedClass(int BreedClassID)
        {
            try
            {
                return cM_BreedClassDAL.Delete(BreedClassID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4101";
                string errMsg = "ɾ��������ƷƷ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #region ɾ��Ʒ��ʱ�������Ʒ��ID��ɾ������������ı�
        /// <summary>
        /// ɾ��Ʒ��ʱ�������Ʒ��ID��ɾ������������ı�
        /// </summary>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool DeleteCMBreedClassALLAbout(int BreedClassID)
        {
            XH_SpotTradeRulesBLL xH_SpotTradeRulesBLL = new XH_SpotTradeRulesBLL();
            QH_FuturesTradeRulesBLL qH_FuturesTradeRulesBLL = new QH_FuturesTradeRulesBLL();
            QH_SIFPositionBLL qH_SIFPositionBLL = new QH_SIFPositionBLL();
            CM_CommodityDAL cM_CommodityDAL = new CM_CommodityDAL();
            CM_BreedClassDAL cMBreedClassDAL = new CM_BreedClassDAL();
            CM_BreedClass cM_BreedClass = new CM_BreedClass();
            XH_SpotPositionDAL xH_SpotPositionDAL = new XH_SpotPositionDAL();
            UM_DealerTradeBreedClassDAL uM_DealerTradeBreedClassDAL = new UM_DealerTradeBreedClassDAL();
            UM_DealerTradeBreedClass uM_DealerTradeBreedClass = new UM_DealerTradeBreedClass();
            //QH_SIFPositionDAL qH_SIFPositionDAL=new QH_SIFPositionDAL();//��ָ�ڻ��ֲ�����
            //QH_SIFPosition qH_SIFPosition=new QH_SIFPosition();
            QH_PositionLimitValueDAL qH_PositionLimitValueDAL = new QH_PositionLimitValueDAL();//�ڻ��ֲ�����
            XH_SpotCostsDAL xH_SpotCostsDAL = new XH_SpotCostsDAL();//�ֻ����׷���
            QH_FutureCostsDAL qH_FutureCostsDAL = new QH_FutureCostsDAL();//�ڻ����׷���
            QH_CFBailScaleValueDAL qH_CFBailScaleValueDAL = new QH_CFBailScaleValueDAL();//��Ʒ�ڻ�_��֤�����

            HK_SpotTradeRulesBLL hK_SpotTradeRulesBLL = new HK_SpotTradeRulesBLL();//�۹ɽ��׹���BLL
            HK_SpotCostsDAL hK_SpotCostsDAL = new HK_SpotCostsDAL();//�۹ɽ��׷���
            HK_CommodityDAL hK_CommodityDAL = new HK_CommodityDAL();//�۹ɽ�����Ʒ

            //�ڻ���֤�� add by ���� 2010-02-02
            QH_SIFBailDAL qh_SIFBailDal = new QH_SIFBailDAL();

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
                int NewBreedClassID = AppGlobalVariable.INIT_INT; //δ����Ʒ��ID
                //��ȡϵͳĬ��δ����Ʒ�ֵ�Ʒ��ID
                List<CM_BreedClass> cMBreedClassList =
                    cMBreedClassDAL.GetListArray(string.Format("DeleteState={0}", (int)Types.IsYesOrNo.Yes));
                if (cMBreedClassList.Count == 0)
                {
                    return false;
                }
                if (cMBreedClassList[0].ISSysDefaultBreed == (int)Types.IsYesOrNo.Yes)
                {
                    NewBreedClassID = cMBreedClassList[0].BreedClassID;
                }
                cM_BreedClass = cMBreedClassDAL.GetModel(BreedClassID);
                if (cM_BreedClass == null)
                {
                    return false;
                }

                //ɾ����ϻ���Ĵ���
                int ISHKBreedClassType = Convert.ToInt32(cM_BreedClass.ISHKBreedClassType);//�Ƿ�۹�����
                if (ISHKBreedClassType == (int)GTA.VTS.Common.CommonObject.Types.IsYesOrNo.Yes)//��Ʒ�������Ǹ۹�ʱ
                {
                    List<ManagementCenter.Model.HK_Commodity> hkCommodityList =
                        hK_CommodityDAL.GetListArray(string.Format("BreedClassID={0}", BreedClassID));
                    if (hkCommodityList.Count > 0)
                    {
                        List<ManagementCenter.Model.RC_TradeCommodityAssign> rcTradeCommodityAssignList =
                            rC_TradeCommodityAssignDAL.GetListArray(string.Format("CodeFormSource={0}",
                                                                                  (int)Types.IsCodeFormSource.No));
                        if (rcTradeCommodityAssignList.Count > 0)
                        {
                            for (int i = 0; i < hkCommodityList.Count; i++)
                            {
                                for (int j = i; j < rcTradeCommodityAssignList.Count; j++)
                                {
                                    if (hkCommodityList[i].HKCommodityCode == rcTradeCommodityAssignList[j].CommodityCode)
                                    {
                                        if (!rC_TradeCommodityAssignDAL.DeleteRCByCommodityCode(hkCommodityList[i].HKCommodityCode, Tran, db))
                                        {
                                            Tran.Rollback();
                                            return false;
                                        }
                                        break;
                                    }
                                }
                            }

                        }

                    }

                }
                else if (ISHKBreedClassType == (int)GTA.VTS.Common.CommonObject.Types.IsYesOrNo.No)//������Ʒ��������ʱ)
                {
                    List<ManagementCenter.Model.CM_Commodity> cmCommodityList =
                       cM_CommodityDAL.GetListArray(string.Format("BreedClassID={0}", BreedClassID));
                    if (cmCommodityList.Count > 0)
                    {
                        List<ManagementCenter.Model.RC_TradeCommodityAssign> rcTradeCommodityAssignList =
                            rC_TradeCommodityAssignDAL.GetListArray(string.Format("CodeFormSource={0}",
                                                                                  (int)Types.IsCodeFormSource.Yes));
                        if (rcTradeCommodityAssignList.Count > 0)
                        {
                            for (int i = 0; i < cmCommodityList.Count; i++)
                            {
                                for (int j = i; j < rcTradeCommodityAssignList.Count; j++)
                                {
                                    if (cmCommodityList[i].CommodityCode == rcTradeCommodityAssignList[j].CommodityCode)
                                    {
                                        if (!rC_TradeCommodityAssignDAL.DeleteRCByCommodityCode(cmCommodityList[i].CommodityCode, Tran, db))
                                        {
                                            Tran.Rollback();
                                            return false;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                //Ʒ������ID
                int breedClassType = Convert.ToInt32(cM_BreedClass.BreedClassTypeID);
                if (breedClassType == (int)GTA.VTS.Common.CommonObject.Types.BreedClassTypeEnum.Stock)
                {
                    if (!xH_SpotTradeRulesBLL.DeleteSpotTradeRulesAboutAll(BreedClassID))
                    {
                        Tran.Rollback();
                        return false;
                    }
                    if (!xH_SpotPositionDAL.Delete(BreedClassID, Tran, db)) //ɾ���ֲ�
                    {
                        Tran.Rollback();
                        return false;
                    }
                    if (!xH_SpotCostsDAL.Delete(BreedClassID, Tran, db)) //ɾ���ֻ����׷���
                    {
                        Tran.Rollback();
                        return false;
                    }

                }
                else if (breedClassType == (int)GTA.VTS.Common.CommonObject.Types.BreedClassTypeEnum.StockIndexFuture)
                {
                    if (!qH_FuturesTradeRulesBLL.DeleteFuturesTradeRulesAboutAll(BreedClassID))
                    {
                        Tran.Rollback();
                        return false;
                    }
                    if (!qH_SIFPositionBLL.DeleteQHSIFPositionAndQHSIFBail(BreedClassID))
                    {
                        Tran.Rollback();
                        return false;
                    }
                    if (!qH_PositionLimitValueDAL.DeletePositionLimitVByBreedClassID(BreedClassID, Tran, db))
                    {
                        Tran.Rollback();
                        return false;
                    }
                    if (!qH_FutureCostsDAL.Delete(BreedClassID, Tran, db)) //ɾ���ڻ����׷���
                    {
                        Tran.Rollback();
                        return false;
                    }
                    if (!qH_CFBailScaleValueDAL.DeleteCFBailScaleVByBreedClassID(BreedClassID, Tran, db))//ɾ����Ʒ�ڻ�_��֤�����
                    {
                        Tran.Rollback();
                        return false;
                    }
                }
                else if (breedClassType == (int)GTA.VTS.Common.CommonObject.Types.BreedClassTypeEnum.HKStock)
                {
                    if (!hK_SpotTradeRulesBLL.DeleteHKSpotTradeRulesAbout(BreedClassID))
                    {
                        Tran.Rollback();
                        return false;
                    }
                    if (!hK_SpotCostsDAL.Delete(BreedClassID, Tran, db)) //ɾ���۹ɽ��׷���
                    {
                        Tran.Rollback();
                        return false;
                    }
                    if (!xH_SpotPositionDAL.Delete(BreedClassID, Tran, db)) //ɾ���ֲ�
                    {
                        Tran.Rollback();
                        return false;
                    }
                    //����Ʒ��ID�����¸۹ɽ�����Ʒ���е�Ʒ��ID(
                    if (!hK_CommodityDAL.UpdateHKBreedClassID(BreedClassID, NewBreedClassID, Tran, db))
                    {
                        Tran.Rollback();
                        return false;
                    }
                }
                if (breedClassType != (int)GTA.VTS.Common.CommonObject.Types.BreedClassTypeEnum.HKStock)
                {
                    //����Ʒ��ID�����½�����Ʒ���е�Ʒ��ID(
                    if (!cM_CommodityDAL.UpdateBreedClassID(BreedClassID, NewBreedClassID, Tran, db))
                    {
                        Tran.Rollback();
                        return false;
                    }
                }

                //ɾ��Ʒ��Ȩ�ޱ��¼
                List<Model.UM_DealerTradeBreedClass> uMDealerTradeBreedClass =
                    uM_DealerTradeBreedClassDAL.GetListArray(string.Format("BreedClassID={0}", BreedClassID));
                foreach (Model.UM_DealerTradeBreedClass umDTradeBreedClass in uMDealerTradeBreedClass)
                {
                    if (!uM_DealerTradeBreedClassDAL.DeleteDealerTradeByBreedClassID(Convert.ToInt32(umDTradeBreedClass.BreedClassID), Tran, db))
                    {
                        Tran.Rollback();
                        return false;
                    }
                }
                //ɾ���ڻ���֤���¼ add by ���� 2010-02-02
                if (!qh_SIFBailDal.Delete(BreedClassID, Tran, db))
                {
                    Tran.Rollback();
                    return false;
                }
                if (!cMBreedClassDAL.Delete(BreedClassID, Tran, db))
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
                string errCode = "GL-4115";
                string errMsg = " ɾ��Ʒ��ʱ�������Ʒ��ID��ɾ������������ı�ʧ��!";
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
        public ManagementCenter.Model.CM_BreedClass GetModel(int BreedClassID)
        {
            return cM_BreedClassDAL.GetModel(BreedClassID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.CM_BreedClass GetModelByCache(int BreedClassID)
        {
            string CacheKey = "CM_BreedClassModel-" + BreedClassID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = cM_BreedClassDAL.GetModel(BreedClassID);
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
            return (ManagementCenter.Model.CM_BreedClass)objModel;
        }

        #region ���ݲ�ѯ������ȡƷ�������б�
        /// <summary>
        /// ���ݲ�ѯ������ȡƷ�������б�
        /// </summary>
        /// <param name="strWhere">����</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            try
            {
                return cM_BreedClassDAL.GetList(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4114";
                string errMsg = "���ݲ�ѯ������ȡƷ�������б�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ���ݲ�ѯ������ȡ���еĽ�����ƷƷ�֣���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.CM_BreedClass> GetListArray(string strWhere)
        {
            try
            {
                return cM_BreedClassDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-4103";
                string errMsg = "���ݲ�ѯ������ȡ���еĽ�����ƷƷ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #region ��ȡ�ֻ�Ʒ������

        /// <summary>
        /// ��ȡ�ֻ�Ʒ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetBreedClassName()
        {
            try
            {
                CM_BreedClassDAL cMBreedClassDAL = new CM_BreedClassDAL();
                return cMBreedClassDAL.GetBreedClassName();
            }
            catch (Exception ex)
            {
                string errCode = "GL-4104";
                string errMsg = "��ȡ�ֻ�Ʒ������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ��ȡ���н�����ƷƷ��

        /// <summary>
        /// ��ȡ���н�����ƷƷ��
        /// </summary>
        /// <param name="BreedClassTypeID">Ʒ������ID</param>
        ///  <param name="BourseTypeID">����������ID</param>
        /// <param name="pageNo">��ǰҳ</param>
        /// <param name="pageSize">��ʾ��¼��</param>
        /// <param name="rowCount">������</param>
        /// <returns></returns>
        public DataSet GetAllCMBreedClass(int BreedClassTypeID, int BourseTypeID, int pageNo, int pageSize,
                                          out int rowCount)
        {
            try
            {
                CM_BreedClassDAL cMBreedClassDAL = new CM_BreedClassDAL();
                return cMBreedClassDAL.GetAllCMBreedClass(BreedClassTypeID, BourseTypeID, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                rowCount = AppGlobalVariable.INIT_INT;
                string errCode = "GL-4105";
                string errMsg = "��ȡ���н�����ƷƷ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region  ���ݽ�����ƷƷ�ֱ��еĽ���������ID��ȡ��������������

        /// <summary>
        /// ���ݽ�����ƷƷ�ֱ��еĽ���������ID��ȡ��������������
        /// </summary>
        /// <returns></returns>
        public DataSet GetCMBreedClassBourseTypeName()
        {
            try
            {
                CM_BreedClassDAL cMBreedClassDAL = new CM_BreedClassDAL();
                return cMBreedClassDAL.GetCMBreedClassBourseTypeName();
            }
            catch (Exception ex)
            {
                string errCode = "GL-4106";
                string errMsg = "���ݽ�����ƷƷ�ֱ��еĽ���������ID��ȡ��������������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ��ȡ����Ʒ������

        /// <summary>
        /// ��ȡ����Ʒ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllBreedClassName()
        {
            try
            {
                CM_BreedClassDAL cMBreedClassDAL = new CM_BreedClassDAL();
                return cMBreedClassDAL.GetAllBreedClassName();
            }
            catch (Exception ex)
            {
                string errCode = "GL-4107";
                string errMsg = "��ȡ����Ʒ������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ��ȡƷ����������Ʒ�ڻ���Ʒ������

        /// <summary>
        /// ��ȡƷ����������Ʒ�ڻ���Ʒ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpQhTypeBreedClassName()
        {
            try
            {
                CM_BreedClassDAL cMBreedClassDAL = new CM_BreedClassDAL();
                return cMBreedClassDAL.GetSpQhTypeBreedClassName();
            }
            catch (Exception ex)
            {
                string errCode = "GL-4108";
                string errMsg = "��ȡƷ����������Ʒ�ڻ���Ʒ������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ��ȡƷ����������Ʒ�ڻ����ָ�ڻ���Ʒ������

        /// <summary>
        /// ��ȡƷ����������Ʒ�ڻ����ָ�ڻ���Ʒ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetQHFutureCostsBreedClassName()
        {
            try
            {
                CM_BreedClassDAL cMBreedClassDAL = new CM_BreedClassDAL();
                return cMBreedClassDAL.GetQHFutureCostsBreedClassName();
            }
            catch (Exception ex)
            {
                string errCode = "GL-4109";
                string errMsg = "��ȡƷ����������Ʒ�ڻ����ָ�ڻ���Ʒ������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region ��ȡƷ�������ǹ�ָ�ڻ���Ʒ������

        /// <summary>
        /// ��ȡƷ�������ǹ�ָ�ڻ���Ʒ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetQHSIFPositionAndBailBreedClassName()
        {
            try
            {
                CM_BreedClassDAL cMBreedClassDAL = new CM_BreedClassDAL();
                return cMBreedClassDAL.GetQHSIFPositionAndBailBreedClassName();
            }
            catch (Exception ex)
            {
                string errCode = "GL-4110";
                string errMsg = "��ȡƷ�������ǹ�ָ�ڻ���Ʒ������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion

        #region  �ж�Ʒ�������Ƿ��Ѿ�����
        /// <summary>
        /// �ж�Ʒ�������Ƿ��Ѿ�����
        /// </summary>
        /// <param name="BreedClassName">Ʒ������</param>
        /// <returns></returns>
        public bool IsExistBreedClassName(string BreedClassName)
        {
            try
            {
                CM_BreedClassDAL cMBreedClassDAL = new CM_BreedClassDAL();
                string strWhere = string.Format("BreedClassName='{0}'", BreedClassName);
                DataSet _ds = cMBreedClassDAL.GetList(strWhere);
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
                string errCode = "GL-4111";
                string errMsg = "�жϽ�����ƷƷ�������Ƿ��Ѿ�����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;

            }
        }
        #endregion

        #region ��ȡ�ֻ���ͨ�͸۹�Ʒ������

        /// <summary>
        /// ��ȡ�ֻ���ͨ�͸۹�Ʒ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetXHAndHKBreedClassName()
        {
            try
            {
                CM_BreedClassDAL cMBreedClassDAL = new CM_BreedClassDAL();
                return cMBreedClassDAL.GetXHAndHKBreedClassName();
            }
            catch (Exception ex)
            {
                string errCode = "GL-4113";
                string errMsg = "��ȡ�ֻ���ͨ�͸۹�Ʒ������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion

        #region ����Ʒ�ֱ�ʶ���ؽ�����ƷƷ��ʵ��
        /// <summary>
        /// ����Ʒ�ֱ�ʶ���ؽ�����ƷƷ��ʵ��
        /// </summary>
        /// <param name="breedClassID">Ʒ�ֱ�ʶ</param>
        /// <returns></returns>
        public CM_BreedClass GetBreedClassByBClassID(int breedClassID)
        {
            try
            {
                CM_BreedClassBLL cM_BreedClassBLL = new CM_BreedClassBLL();
                List<CM_BreedClass> cM_BreedClassList =
                    cM_BreedClassBLL.GetListArray(string.Format("BreedClassID={0}", breedClassID));
                if (cM_BreedClassList.Count > 0)
                {
                    CM_BreedClass cM_BreedClass = cM_BreedClassList[0];
                    if (cM_BreedClass != null)
                    {
                        return cM_BreedClass;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                string errCode = "GL-4114";
                string errMsg = "����Ʒ�ֱ�ʶ���ؽ�����ƷƷ��ʵ��ʧ��";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return null;
            }
        }

        #endregion

        #endregion  ��Ա����
    }
}