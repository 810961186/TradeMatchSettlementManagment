#region Using Namespace

using GTA.VTS.Common.CommonUtility;
using ReckoningCounter.DAL;
using ReckoningCounter.DAL.Data;
using ReckoningCounter.Entity;
using ReckoningCounter.Entity.Contants; 
using ReckoningCounter.Model;

#endregion

namespace ReckoningCounter.BLL.DelegateValidate.Local
{
    /// <summary>
    /// ֤ȯ�����ڲ�У�飬�����뷶Χ1500-1599
    /// ���ߣ�����
    /// ���ڣ�2008-11-24
    /// </summary>
    public class LocalStockCacelValidater
    {
        private string orderNo;
        private XH_TodayEntrustTableInfo todayEntrustTable;

        public LocalStockCacelValidater(string orderNo)
        {
            this.orderNo = orderNo;
        }

        /// <summary>
        /// ���г���У��
        /// </summary>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns>У����</returns>
        public bool Check(ref string errMsg)
        {
            bool result = CheckDelegateExist(ref errMsg);

            if (result)
            {
                int state =todayEntrustTable.OrderStatusId;
                Types.OrderStateType orderState = (Types.OrderStateType) state;
                result = CheckCanCancelDelegate(orderState, ref errMsg);
            }

            return result;
        }

        /// <summary>
        /// ����Ƿ���ڶ�Ӧ��ί�е���
        /// </summary>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns>У����</returns>
        private bool CheckDelegateExist(ref string errMsg)
        {
            bool result = false;
            errMsg = "";

            try
            {
                XH_TodayEntrustTableDal dal = new XH_TodayEntrustTableDal();

                //todayEntrustTable = DataRepository.XhTodayEntrustTableProvider.GetByEntrustNumber(this.orderNo);
                todayEntrustTable = dal.GetModel(this.orderNo);
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteError(ex.Message,ex);
            }

            if (todayEntrustTable != null)
            {
                result = true;
            }
            else
            {
                string errCode = "GT-1500";
                errMsg = "ί�в����ڡ�";
                errMsg = errCode + ":" + errMsg;
                LogHelper.WriteInfo(errMsg);
            }

            return result;
        }

        /// <summary>
        /// ����Ƿ�ɳ���
        /// </summary>
        /// <param name="orderState">ί��״̬</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns>У����</returns>
        private bool CheckCanCancelDelegate(Types.OrderStateType orderState, ref string errMsg)
        {
            bool result = false;
            errMsg = "";

            if (orderState == Types.OrderStateType.DOSUnRequired || orderState == Types.OrderStateType.DOSRequiredSoon ||
                orderState == Types.OrderStateType.DOSIsRequired || orderState == Types.OrderStateType.DOSPartDealed)
            {
                result = true;
            }
            else
            {
                string errCode = "GT-1501";
                errMsg = "ί�в��ɳ���";
                errMsg = errCode + ":" + errMsg;
                LogHelper.WriteInfo(errMsg);
            }

            return result;
        }
    }
}