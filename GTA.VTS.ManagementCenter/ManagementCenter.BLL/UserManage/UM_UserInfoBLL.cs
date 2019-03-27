using System;
using System.Data;
using System.Data.Common;
using GTA.VTS.Common.CommonObject;
using GTA.VTS.Common.CommonUtility;
using LTP.Common;
using ManagementCenter.DAL;
using ManagementCenter.Model;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Types=ManagementCenter.Model.CommonClass.Types;

namespace ManagementCenter.BLL
{
    /// <summary>
    /// �������û�������Ϣ�� ҵ���߼���UM_UserInfoBLL ��ժҪ˵����
    /// ���ߣ�����Ա��������
    /// ���ڣ�2008-11-20
    /// </summary>
    public class UM_UserInfoBLL
    {
        private readonly ManagementCenter.DAL.UM_UserInfoDAL dal = new ManagementCenter.DAL.UM_UserInfoDAL();

        public UM_UserInfoBLL()
        {
        }

        //===========================================��������===============================================

        #region �õ����ID

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        #endregion

        #region �Ƿ���ڸü�¼

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int UserID)
        {
            return dal.Exists(UserID);
        }

        #endregion

        #region ����һ������

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ManagementCenter.Model.UM_UserInfo model)
        {
            return dal.Add(model);
        }

        #endregion

        #region ����һ������

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ManagementCenter.Model.UM_UserInfo model)
        {
            dal.Update(model);
        }

        #endregion

        #region ɾ��һ������

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(int UserID)
        {
            dal.Delete(UserID);
        }

        #endregion

        #region �õ�һ������ʵ��

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ManagementCenter.Model.UM_UserInfo GetModel(int UserID)
        {
            return dal.GetModel(UserID);
        }

        #endregion

        #region �õ�һ������ʵ�壬�ӻ�����

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public ManagementCenter.Model.UM_UserInfo GetModelByCache(int UserID)
        {
            string CacheKey = "UM_UserInfoModel-" + UserID;
            object objModel = LTP.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(UserID);
                    if (objModel != null)
                    {
                        int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
                        LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache),
                                                      TimeSpan.Zero);
                    }
                }
                catch
                {}
            }
            return (ManagementCenter.Model.UM_UserInfo) objModel;
        }

        #endregion

        #region ��������б�

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ManagementCenter.Model.UM_UserInfo> GetListArray(string strWhere)
        {
            return dal.GetListArray(strWhere);
        }

        #endregion

        #region

        /// <summary>
        /// ��������б�
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion

        //===========================================����Ա===============================================

        #region ��ӹ���Ա

        /// <summary>
        /// ��ӹ���Ա
        /// </summary>
        /// <param name="model"></param>
        /// <param name="RightGroupID"></param>
        /// <returns></returns>
        public bool ManagerAdd(ManagementCenter.Model.UM_UserInfo model, int RightGroupID)
        {
            ManagementCenter.DAL.UM_UserInfoDAL UserInfoDAL = new UM_UserInfoDAL();
            ManagementCenter.DAL.UM_ManagerBeloneToGroupDAL ManagerBeloneToGroupDAL = new UM_ManagerBeloneToGroupDAL();

            Database db = DatabaseFactory.CreateDatabase();
            DbConnection Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open) Conn.Open();
            DbTransaction Tran = Conn.BeginTransaction();
            try
            {
                int UserID = UserInfoDAL.Add(model, Tran, db);
                if (UserID != 0)
                {
                    UM_ManagerBeloneToGroup ManagerBeloneToGroup = new UM_ManagerBeloneToGroup();
                    ManagerBeloneToGroup.UserID = UserID;
                    ManagerBeloneToGroup.ManagerGroupID = RightGroupID;
                    ManagerBeloneToGroupDAL.Add(ManagerBeloneToGroup, Tran, db);
                    Tran.Commit();
                }
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-1100";
                string errMsg = "��ӹ���Աʧ�ܣ�";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open) Conn.Close();
            }
            return true;
        }

        #endregion

        #region ���¹���Ա

        /// <summary>
        /// ���¹���Ա
        /// </summary>
        /// <param name="model">�û���Ϣʵ��</param>
        /// <param name="RightGroupID">Ȩ����id</param>
        /// <returns></returns>
        public bool ManagerUpdate(ManagementCenter.Model.UM_UserInfo model, int RightGroupID)
        {
            ManagementCenter.DAL.UM_UserInfoDAL UserInfoDAL = new UM_UserInfoDAL();
            ManagementCenter.DAL.UM_ManagerBeloneToGroupDAL ManagerBeloneToGroupDAL = new UM_ManagerBeloneToGroupDAL();

            Database db = DatabaseFactory.CreateDatabase();
            DbConnection Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open) Conn.Open();
            DbTransaction Tran = Conn.BeginTransaction();
            try
            {
                UserInfoDAL.Update(model, Tran, db);

                UM_ManagerBeloneToGroup ManagerBeloneToGroup = new UM_ManagerBeloneToGroup();
                ManagerBeloneToGroup.UserID = model.UserID;
                ManagerBeloneToGroup.ManagerGroupID = RightGroupID;
                ManagerBeloneToGroupDAL.Update(ManagerBeloneToGroup, Tran, db);
                Tran.Commit();
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-1101";
                string errMsg = "���¹���Աʧ�ܣ�";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open) Conn.Close();
            }
            return true;
        }

        #endregion

        #region ɾ������Ա

        /// <summary>
        /// ɾ������Ա
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool ManagerDelete(int UserID)
        {
            ManagementCenter.DAL.UM_UserInfoDAL UserInfoDAL = new UM_UserInfoDAL();
            ManagementCenter.DAL.UM_ManagerBeloneToGroupDAL ManagerBeloneToGroupDAL = new UM_ManagerBeloneToGroupDAL();

            Database db = DatabaseFactory.CreateDatabase();
            DbConnection Conn = db.CreateConnection();
            if (Conn.State != ConnectionState.Open) Conn.Open();
            DbTransaction Tran = Conn.BeginTransaction();
            try
            {
                ManagerBeloneToGroupDAL.Delete(UserID, Tran, db);
                UserInfoDAL.Delete(UserID, Tran, db);
                Tran.Commit();
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                string errCode = "GL-1102";
                string errMsg = "ɾ������Աʧ�ܣ�";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open) Conn.Close();
            }
            return true;
        }

        #endregion

        #region ����Ա��ҳ��ѯ

        /// <summary>
        /// ����Ա��ҳ��ѯ
        /// </summary>
        /// <param name="managerQueryEntity">��ѯʵ��</param>
        /// <param name="pageNo">�ڼ�ҳ</param>
        /// <param name="pageSize">ÿҳ��С</param>
        /// <param name="rowCount">�ܼ�¼����</param>
        /// <returns></returns>
        public DataSet GetPagingManager(Model.UserManage.ManagerQueryEntity managerQueryEntity, int pageNo, int pageSize,
                                        out int rowCount)
        {
            try
            {
                return dal.GetPagingManager(managerQueryEntity, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                string errCode = "GL-1103";
                string errMsg = "����Ա��ҳ��ѯʧ�ܣ�";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                rowCount = 0;
                return null;
            }
        }

        #endregion

        #region �жϹ���Ա��¼���Ƿ����
        /// <summary>
        /// �жϹ���Ա��¼���Ƿ����
        /// </summary>
        /// <param name="loginname">��½����</param>
        /// <returns></returns>
        public bool CheckLoginName(string loginname)
        {
            try
            {
                string Where = string.Format(" LoginName='{0}' AND (RoleID={1} OR RoleID={2})", loginname,
                                             (int) Types.RoleTypeEnum.Manager, (int) Types.RoleTypeEnum.Admin);
                DataSet ds = dal.GetList(Where);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count == 0) return true;
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errCode = "GL-1104";
                string errMsg = "�жϹ���Ա��¼���Ƿ����ʧ�ܣ�";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return false;
            }
        }

        #endregion

        #region ����Ա��֤
        /// <summary>
        /// ����Ա��֤
        /// </summary>
        /// <param name="LoginName">��½��</param>
        /// <param name="Password">����</param>
        /// <param name="AddType">����Ա���������</param>
        /// <returns></returns>
        public UM_UserInfo ManagerLoginConfirm(string LoginName, string Password, int AddType)
        {
            try
            {
                UM_UserInfo UserInfo = dal.ManagerLoginConfirm(LoginName, Password, AddType);
                return UserInfo;
            }
            catch (Exception ex)
            {
                string errCode = "GL-1105";
                string errMsg = "����Ա��֤ʧ�ܣ�";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return null;
            }
        }

        #endregion

        #region �һ����뷽��
        /// <summary>
        /// �һ�����
        /// </summary>
        /// <param name="LoginName">��½��</param>
        /// <param name="Answer">�����</param>
        /// <param name="QuestionID">��������</param>
        /// <returns></returns>
        public ManagementCenter.Model.UM_UserInfo SeekForPassword(string LoginName, string Answer, int QuestionID)
        {
            try
            {
                return dal.SeekForPassword(LoginName, Answer, QuestionID);
            }
            catch (Exception ex)
            {
                string errCode = "GL-1106";
                string errMsg = "����Ա��֤ʧ�ܣ�";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                return null;
            }
        }
        #endregion

        //===========================================����Ա===============================================

        #region ����Ա��ҳ��ѯ

        /// <summary>
        /// ����Ա��ҳ��ѯ
        /// </summary>
        /// <param name="userInfo">�û���ѯʵ��</param>
        /// <param name="pageNo">�ڼ�ҳ</param>
        /// <param name="pageSize">ÿҳ����</param>
        /// <param name="rowCount">�ܼ�¼��</param>
        /// <returns></returns>
        public DataSet GetPagingUser(Model.UM_UserInfo userInfo, int pageNo, int pageSize, out int rowCount)
        {
            try
            {
                return dal.GetPagingUser(userInfo, pageNo, pageSize, out rowCount);
            }
            catch (Exception ex)
            {
                string errCode = "GL-0260";
                string errMsg = "����Ա��ҳ��ѯ��";
                VTException vte = new VTException(errCode, errMsg, ex);
                LogHelper.WriteError(vte.ToString(), vte.InnerException);
                rowCount = 0;
                return null;
            }
        }

        #endregion
    }
}