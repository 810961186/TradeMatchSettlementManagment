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
using ManagementCenter.Model.XH;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ManagementCenter.BLL
{
    /// <summary>
    ///�������ֻ�_Ʒ��_�ǵ���_�������� ҵ���߼���XH_SpotHighLowControlTypeBLL ��ժҪ˵����
    /// ������뷶Χ:5220-5239
    ///���ߣ�����ΰ
    ///���ڣ�2008-11-27
    /// </summary>
    public class XH_SpotHighLowControlTypeBLL
    {
        private readonly ManagementCenter.DAL.XH_SpotHighLowControlTypeDAL xH_SpotHighLowControlTypeDAL =
            new ManagementCenter.DAL.XH_SpotHighLowControlTypeDAL();

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public XH_SpotHighLowControlTypeBLL()
        {
        }
        #endregion

        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return xH_SpotHighLowControlTypeDAL.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int BreedClassHighLowID)
        {
            return xH_SpotHighLowControlTypeDAL.Exists(BreedClassHighLowID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(ManagementCenter.Model.XH_SpotHighLowControlType model)
        {
            xH_SpotHighLowControlTypeDAL.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.XH_SpotHighLowControlType model)
        {
            xH_SpotHighLowControlTypeDAL.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int BreedClassHighLowID)
        {
            xH_SpotHighLowControlTypeDAL.Delete(BreedClassHighLowID);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.XH_SpotHighLowControlType GetModel(int BreedClassHighLowID)
        {
            try
            {
                return xH_SpotHighLowControlTypeDAL.GetModel(BreedClassHighLowID);
            }
            catch (Exception ex)
            {
                GTA.VTS.Common.CommonUtility.LogHelper.WriteError(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.XH_SpotHighLowControlType GetModelByCache(int BreedClassHighLowID)
        {
            string CacheKey = "XH_SpotHighLowControlTypeModel-" + BreedClassHighLowID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = xH_SpotHighLowControlTypeDAL.GetModel(BreedClassHighLowID);
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
            return (ManagementCenter.Model.XH_SpotHighLowControlType)objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return xH_SpotHighLowControlTypeDAL.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ���ݲ�ѯ������ȡ���е��ֻ�_Ʒ��_�ǵ���_�������ͣ���ѯ������Ϊ�գ�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns></returns>
        public List<ManagementCenter.Model.XH_SpotHighLowControlType> GetListArray(string strWhere)
        {
            try
            {
                return xH_SpotHighLowControlTypeDAL.GetListArray(strWhere);

            }
            catch (Exception ex)
            {
                string errCode = "GL-5224";
                string errMsg = "���ݲ�ѯ������ȡ���е��ֻ�_Ʒ��_�ǵ���_��������ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
            }
        }

        #endregion  ��Ա����

        #region ����ֻ��ǵ�������Ч�걨
        /// <summary>
        /// ����ֻ��ǵ�������Ч�걨
        /// </summary>
        /// <param name="xHSpotHighLowConType"></param>
        /// <param name="xHSpotHighLowValue"></param>
        /// <param name="xHValidDecType">��Ч�걨����ʵ����</param>
        /// <param name="xHValidDeclareValue">��Ч�걨ȡֵʵ��</param>
        /// <returns></returns>
        public XH_AboutSpotHighLowEntity AddXHSpotHighLowAndValidDecl(XH_SpotHighLowControlType xHSpotHighLowConType,
                                         XH_SpotHighLowValue xHSpotHighLowValue, XH_ValidDeclareType xHValidDecType,
                                          XH_ValidDeclareValue xHValidDeclareValue)
        {
            XH_SpotHighLowValueDAL xHSpotHighLowValueDAL = new XH_SpotHighLowValueDAL();
            XH_SpotHighLowControlTypeDAL xHSpotHighLowControlTypeDAL = new XH_SpotHighLowControlTypeDAL();
            XH_ValidDeclareTypeDAL xHValidDeclareTypeDAL = new XH_ValidDeclareTypeDAL();
            XH_ValidDeclareValueDAL xHValidDeclareValueDAL = new XH_ValidDeclareValueDAL();

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
                int breedClassHighLowID = AppGlobalVariable.INIT_INT;
                int breedClassValidID = AppGlobalVariable.INIT_INT;
                //List<XH_AboutSpotHighLowEntity> xhAboutSpotHighLowEList=new List<XH_AboutSpotHighLowEntity>();
                XH_AboutSpotHighLowEntity xhAboutSpotHighLowEntity = new XH_AboutSpotHighLowEntity();
                breedClassHighLowID = xHSpotHighLowControlTypeDAL.Add(xHSpotHighLowConType, Tran, db);
                if (breedClassHighLowID != AppGlobalVariable.INIT_INT)
                {
                    xHSpotHighLowValue.BreedClassHighLowID = breedClassHighLowID;

                    if (xHSpotHighLowValueDAL.AddXHSpotHighLowValue(xHSpotHighLowValue, Tran, db) == AppGlobalVariable.INIT_INT)
                    {
                        Tran.Rollback();
                    }
                }
                breedClassValidID = xHValidDeclareTypeDAL.AddValidDeclareType(xHValidDecType, Tran, db);

                if (breedClassValidID != AppGlobalVariable.INIT_INT)
                {
                    xHValidDeclareValue.BreedClassValidID = breedClassValidID;

                    if (!xHValidDeclareValueDAL.Add(xHValidDeclareValue, Tran, db))
                    {
                        Tran.Rollback();
                    }

                }
                Tran.Commit();
                xhAboutSpotHighLowEntity.BreedClassHighLowID = breedClassHighLowID;
                xhAboutSpotHighLowEntity.BreedClassValidID = breedClassValidID;
                return xhAboutSpotHighLowEntity;
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-5224";
                string errMsg = "����ֻ�����Ч�걨ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return null;
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

        #region ����Ʒ���ǵ�����ʶɾ���ǵ���ȡֵ���ǵ�������

        /// <summary>
        /// ����Ʒ���ǵ�����ʶɾ���ǵ���ȡֵ���ǵ�������
        /// </summary>
        /// <param name="BreedClassHighLowID">Ʒ���ǵ�����ʶ</param>
        /// <param name="tran"></param>
        /// <param name="db"></param>
        /// <param name="falg">����Ƿ�ʹ�����ⲿ����</param>
        /// <returns></returns>
        public bool DeleteSpotHighLowValue(int BreedClassHighLowID, DbTransaction tran, Database db, bool falg)
        {
            XH_SpotHighLowValueDAL xHSpotHighLowValueDAL = new XH_SpotHighLowValueDAL();
            XH_SpotHighLowControlTypeDAL xHSpotHighLowControlTypeDAL = new XH_SpotHighLowControlTypeDAL();
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

                xHSpotHighLowValueDAL.DeleteSpotHighLowValue(BreedClassHighLowID, tran, db);
                xHSpotHighLowControlTypeDAL.Delete(BreedClassHighLowID, tran, db);
                if (!falg)
                {
                    tran.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (!falg)
                {
                    tran.Rollback();
                    string errCode = "GL-5221";
                    string errMsg = "����Ʒ���ǵ�����ʶɾ���ǵ���ȡֵ���ǵ�������ʧ��!";
                    VTException exception = new VTException(errCode, errMsg, ex);
                    LogHelper.WriteError(exception.ToString(), exception.InnerException);

                }
                return false;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    if (!falg) Conn.Close();
                }
            }
        }

        #endregion

        #region ����Ʒ���ǵ�����ʶɾ���ǵ���ȡֵ���ǵ�������(����)

        /// <summary>
        /// ����Ʒ���ǵ�����ʶɾ���ǵ���ȡֵ���ǵ�������
        /// </summary>
        /// <param name="BreedClassHighLowID">Ʒ���ǵ�����ʶ</param>
        /// <returns></returns>
        public bool DeleteSpotHighLowValue(int BreedClassHighLowID)
        {
            try
            {
                return DeleteSpotHighLowValue(BreedClassHighLowID, null, null, false);

            }
            catch (Exception ex)
            {
                string errCode = "GL-5222";
                string errMsg = "����Ʒ���ǵ�����ʶɾ���ǵ���ȡֵ���ǵ�������(����)ʧ��!";
                VTException exception = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(exception.ToString(), exception.InnerException);
                return false;
            }
        }

        #endregion

        #region �����ֻ��ǵ���ȡֵ���ǵ������ͼ���Ч�걨����Ч�걨����
        /// <summary>
        /// �����ֻ��ǵ���ȡֵ���ǵ������ͼ���Ч�걨����Ч�걨����
        /// </summary>
        /// <param name="xHSpotHighLowConType"></param>
        /// <param name="xHSpotHighLowValue"></param>
        /// <param name="xHValidDecType"></param>
        /// <param name="xHValidDeclareValue"></param>
        /// <returns></returns>
        public bool UpdateXHSpotHighLowAndValidDecl(XH_SpotHighLowControlType xHSpotHighLowConType,
                                         XH_SpotHighLowValue xHSpotHighLowValue, XH_ValidDeclareType xHValidDecType,
                                          XH_ValidDeclareValue xHValidDeclareValue)
        {
            XH_SpotHighLowValueDAL xHSpotHighLowValueDAL = new XH_SpotHighLowValueDAL();
            XH_SpotHighLowControlTypeDAL xHSpotHighLowControlTypeDAL = new XH_SpotHighLowControlTypeDAL();
            XH_ValidDeclareTypeDAL xHValidDeclareTypeDAL = new XH_ValidDeclareTypeDAL();
            XH_ValidDeclareValueDAL xHValidDeclareValueDAL = new XH_ValidDeclareValueDAL();

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
                if (!xHSpotHighLowControlTypeDAL.Update(xHSpotHighLowConType))
                {
                    Tran.Rollback();
                    return false;
                }
                if (!xHSpotHighLowValueDAL.Update(xHSpotHighLowValue))
                {
                    Tran.Rollback();
                    return false;
                }
                if (!xHValidDeclareTypeDAL.Update(xHValidDecType))
                {
                    Tran.Rollback();
                    return false;
                }
                if (!xHValidDeclareValueDAL.Update(xHValidDeclareValue))
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
                string errCode = "GL-5223";
                string errMsg = "�����ǵ���ȡֵ���ǵ�������ʧ��!";
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
    }
}