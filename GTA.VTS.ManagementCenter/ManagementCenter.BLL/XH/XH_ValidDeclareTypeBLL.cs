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
    ///��������Ч�걨���ͱ� ҵ���߼���XH_ValidDeclareTypeBLL ��ժҪ˵����������뷶Χ:5260-5279
    ///���ߣ�����ΰ
    ///����:2008-12-2
    /// </summary>
    public class XH_ValidDeclareTypeBLL
    {
        /// <summary>
        /// ��Ч�걨���ͱ� DAL
        /// </summary>
        private readonly ManagementCenter.DAL.XH_ValidDeclareTypeDAL xH_ValidDeclareTypeDAL =
            new ManagementCenter.DAL.XH_ValidDeclareTypeDAL();

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public XH_ValidDeclareTypeBLL()
        {
        }
        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return xH_ValidDeclareTypeDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int BreedClassValidID)
        {
            return xH_ValidDeclareTypeDAL.Exists(BreedClassValidID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        //public void Add(ManagementCenter.Model.XH_ValidDeclareType model)
        //{
        //    xH_ValidDeclareTypeDAL.Add(model);
        //}

        /// <summary>
        /// ����һ������
        /// </summary>
        //public void Update(ManagementCenter.Model.XH_ValidDeclareType model)
        //{
        //    xH_ValidDeclareTypeDAL.Update(model);
        //}

        #region ����Ʒ����Ч�걨��ʶ��ȡ��Ч�걨����ʵ��
        /// <summary>
        /// ����Ʒ����Ч�걨��ʶ��ȡ��Ч�걨����ʵ��
        /// </summary>
        /// <param name="BreedClassValidID">Ʒ����Ч�걨��ʶ</param>
        /// <returns></returns>
        public ManagementCenter.Model.XH_ValidDeclareType GetModelValidDeclareType(int BreedClassValidID)
        {
            try
            {
                return xH_ValidDeclareTypeDAL.GetModel(BreedClassValidID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5266";
                string errMsg = "���ݲ�ѯ������ȡ���е���Ч�걨����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.XH_ValidDeclareType GetModelByCache(int BreedClassValidID)
        {
            string CacheKey = "XH_ValidDeclareTypeModel-" + BreedClassValidID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = xH_ValidDeclareTypeDAL.GetModel(BreedClassValidID);
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
            return (ManagementCenter.Model.XH_ValidDeclareType)objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return xH_ValidDeclareTypeDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region ���ݲ�ѯ������ȡ���е���Ч�걨���ͣ���ѯ������Ϊ�գ�
        /// <summary>
        /// ���ݲ�ѯ������ȡ���е���Ч�걨���ͣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.XH_ValidDeclareType> GetListArray(string strWhere)
        {
            try
            {
                return xH_ValidDeclareTypeDAL.GetListArray(strWhere);

            }
            catch (Exception ex)
            {
                string errCode = "GL-5265";
                string errMsg = "���ݲ�ѯ������ȡ���е���Ч�걨����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }
        #endregion

        #endregion  ��Ա����

        #region ����Ʒ����Ч�걨��ʶɾ����Ч�걨ȡֵ����Ч�걨����

        /// <summary>
        ///  ����Ʒ����Ч�걨��ʶɾ����Ч�걨ȡֵ����Ч�걨����
        /// </summary>
        /// <param name="BreedClassValidID">Ʒ����Ч�걨��ʶ</param>
        /// <param name="tran">����</param>
        /// <param name="db">db</param>
        /// <param name="flag">״̬��ʶ</param>
        /// <returns></returns>
        public bool DeleteValidDeclareValue(int BreedClassValidID, DbTransaction tran, Database db, bool flag)
        {
            XH_ValidDeclareTypeDAL xHValidDeclareTypeDAL = new XH_ValidDeclareTypeDAL();
            XH_ValidDeclareValueDAL xHValidDeclareValueDAL = new XH_ValidDeclareValueDAL();
            DbConnection Conn = null;
            try
            {
                //������������
                if (db == null && tran == null)
                {
                    db = DatabaseFactory.CreateDatabase();
                    Conn = db.CreateConnection();
                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }
                    tran = Conn.BeginTransaction();
                }

                xHValidDeclareValueDAL.DeleteVDeclareValue(BreedClassValidID, tran, db);
                xHValidDeclareTypeDAL.DeleteValidDeclareType(BreedClassValidID, tran, db);
                if (!flag)
                {
                    tran.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (!flag) tran.Rollback();
                string errCode = "GL-5261";
                string errMsg = "����Ʒ����Ч�걨��ʶɾ����Ч�걨ȡֵ����Ч�걨����ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);

                return false;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    if (!flag) Conn.Close();
                }
            }
        }

        #endregion

        #region ����Ʒ����Ч�걨��ʶɾ����Ч�걨ȡֵ����Ч�걨����(����)

        /// <summary>
        ///  ����Ʒ����Ч�걨��ʶɾ����Ч�걨ȡֵ����Ч�걨����
        /// </summary>
        /// <param name="BreedClassValidID">Ʒ����Ч�걨��ʶ</param>
        /// <returns></returns>
        public bool DeleteValidDeclareValue(int BreedClassValidID)
        {
            try
            {
                return DeleteValidDeclareValue(BreedClassValidID, null, null, false);

            }
            catch (Exception ex)
            {
                string errCode = "GL-5262";
                string errMsg = "����Ʒ����Ч�걨��ʶɾ����Ч�걨ȡֵ����Ч�걨����(����)ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region ����Ʒ����Ч�걨��ʶ��ȡ��Ч�걨ȡֵʵ��

        /// <summary>
        /// ����Ʒ����Ч�걨��ʶ��ȡ��Ч�걨ȡֵʵ��
        /// </summary>
        /// <param name="BreedClassValidID">Ʒ����Ч�걨��ʶ</param>
        /// <returns></returns>
        public ManagementCenter.Model.XH_ValidDeclareValue GetModelValidDeclareValue(int BreedClassValidID)
        {
            try
            {
                XH_ValidDeclareValueDAL xHValidDeclareValueDAL = new XH_ValidDeclareValueDAL();
                return xHValidDeclareValueDAL.GetModelValidDeclareValue(BreedClassValidID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-5264";
                string errMsg = "����Ʒ����Ч�걨��ʶ��ȡ��Ч�걨ȡֵʵ��ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion
    }
}