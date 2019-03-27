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
    ///��������Լ�����·�  ҵ���߼���QH_AgreementDeliveryMonthBLL ��ժҪ˵����������뷶Χ:6060-6069
    ///���ߣ�����ΰ
    ///���ڣ�2008-11-22
    ///�޸ģ�Ҷ��
    ///���ڣ�2010-01-20
    ///���������ͨ�������·ݱ�ʶ��Ʒ�ֱ�ʶ��ѯ��Ʒ�ֵ�ĳ�������·��Ƿ����
    /// </summary>
    public class QH_AgreementDeliveryMonthBLL
    {
        private readonly ManagementCenter.DAL.QH_AgreementDeliveryMonthDAL qH_AgreementDeliveryMonthDAL =
            new ManagementCenter.DAL.QH_AgreementDeliveryMonthDAL();

        public QH_AgreementDeliveryMonthBLL()
        {
        }

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return qH_AgreementDeliveryMonthDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int AgreementDeliveryMonthID)
        {
            return qH_AgreementDeliveryMonthDAL.Exists(AgreementDeliveryMonthID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.QH_AgreementDeliveryMonth model)
        {
            return qH_AgreementDeliveryMonthDAL.Add(model);
        }

        #region ���½����·�(�������,ɾ��)

        /// <summary>
        /// ���½����·�(�������,ɾ��)
        /// </summary>
        /// <param name="addMonthID">��Ҫ��ӵ��·�ID</param>
        /// <param name="deleteMonthID">��Ҫɾ�����·�ID</param>
        /// <param name="BreedClassID">Ʒ��ID</param>
        /// <returns></returns>
        public bool UpdateQHAgreementDeliveryMonth(List<int> addMonthID, List<int> deleteMonthID, int BreedClassID)
        {
            QH_AgreementDeliveryMonthDAL qHAgreementDeliveryMonthDAL = new QH_AgreementDeliveryMonthDAL();
            QH_AgreementDeliveryMonth QH_AgreementDeliveryMonth = new QH_AgreementDeliveryMonth();
            QH_AgreementDeliveryMonth.BreedClassID = BreedClassID;
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
                foreach (int addM in addMonthID)
                {
                    QH_AgreementDeliveryMonth.MonthID = addM;
                 int a= qHAgreementDeliveryMonthDAL.Add(QH_AgreementDeliveryMonth, Tran, db);
                }

                foreach (int  deleM in deleteMonthID)
                {
                    QH_AgreementDeliveryMonth.MonthID = deleM;
                    qHAgreementDeliveryMonthDAL.Delete(deleM, BreedClassID, Tran, db);
                }
                Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-6060";
                string errMsg = "���½����·�(�������,ɾ��)ʧ��!";
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
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.QH_AgreementDeliveryMonth model)
        {
            qH_AgreementDeliveryMonthDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int AgreementDeliveryMonthID)
        {
            qH_AgreementDeliveryMonthDAL.Delete(AgreementDeliveryMonthID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.QH_AgreementDeliveryMonth GetModel(int AgreementDeliveryMonthID)
        {
            return qH_AgreementDeliveryMonthDAL.GetModel(AgreementDeliveryMonthID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.QH_AgreementDeliveryMonth GetModelByCache(int AgreementDeliveryMonthID)
        {
            string CacheKey = "QH_AgreementDeliveryMonthModel-" + AgreementDeliveryMonthID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = qH_AgreementDeliveryMonthDAL.GetModel(AgreementDeliveryMonthID);
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
            return (ManagementCenter.Model.QH_AgreementDeliveryMonth) objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return qH_AgreementDeliveryMonthDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ���ݲ�ѯ������ȡ���еĺ�Լ�����·ݣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_AgreementDeliveryMonth> GetListArray(string strWhere)
        {
            try
            {
                return qH_AgreementDeliveryMonthDAL.GetListArray(strWhere);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6061";
                string errMsg = "���ݲ�ѯ������ȡ���еĺ�Լ�����·ݣ���ѯ������Ϊ�գ�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        /// <summary>
        /// ����Ʒ�ֱ�ʶ���غ�Լ�����·�
        /// </summary>
        /// <param name="breedClassID">Ʒ�ֱ�ʶ</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.QH_AgreementDeliveryMonth> GetQHAgreementDeliveryMonth(int breedClassID)
        {
            try
            {
                QH_AgreementDeliveryMonthDAL qHAgreementDeliveryMonthDAL = new QH_AgreementDeliveryMonthDAL();
                return qHAgreementDeliveryMonthDAL.GetListArray(string.Format("BreedClassID={0}", breedClassID));
            }
            catch (Exception ex)
            {
                string errCode = "GL-6062";
                string errMsg = "����Ʒ�ֱ�ʶ���غ�Լ�����·�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        /// <summary>
        /// ͨ�������·ݱ�ʶ��Ʒ�ֱ�ʶ��ѯĳ��Ʒ�ֵ�ĳ�������·��Ƿ����
        /// </summary>
        /// <param name="BreedClassID">Ʒ�ֱ�ʶ</param>
        /// <param name="monthid">�����·ݱ�ʶ</param>
        /// <returns>ĳ��Ʒ�ֵ�ĳ�������·��Ƿ����</returns>
        public QH_AgreementDeliveryMonth GetQHAgreementDeliveryBreedClassID(int BreedClassID, int monthid)
        {
            try
            {
                QH_AgreementDeliveryMonthDAL qHAgreementDeliveryMonthDAL = new QH_AgreementDeliveryMonthDAL();
                return qHAgreementDeliveryMonthDAL.GetQHAgreementDeliveryBreedClassID (BreedClassID, monthid);
            }
            catch (Exception ex)
            {
                string errCode = "GL-6062";
                string errMsg = "����Ʒ�ֱ�ʶ���غ�Լ�����·�ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion  ��Ա����

        
    }
}