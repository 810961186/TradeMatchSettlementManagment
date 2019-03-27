using System.Net.Mime;
using GTA.VTS.Common.CommonObject;
using CommonRealtimeMarket;
using CommonRealtimeMarket.factory;
using GTA.VTS.Common.CommonUtility;
using GTAMarketSocket;
using ReckoningCounter.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReckoningCounter.Entity;
//using RealTime.Server.Handler;
using RealTime.Common.CommonClass;

namespace ReckoningCounterBizTest
{
    
    
    /// <summary>
    ///This is a test class for OrderAccepterTest and is intended
    ///to contain all OrderAccepterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OrderAccepterTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for OrderAccepter Constructor
        ///</summary>
        [TestMethod()]
        public void OrderAccepterConstructorTest()
        {
            OrderAccepter target = new OrderAccepter();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CancelSPQHOrder
        ///</summary>
        [TestMethod()]
        public void CancelMercantileFuturesOrderTest()
        {
            OrderAccepter target = new OrderAccepter(); // TODO: Initialize to an appropriate value
            string OrderId = string.Empty; // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            string messageExpected = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            int errCode = 0;
            actual = target.CancelMercantileFuturesOrder(OrderId, ref message, out errCode);
            Assert.AreEqual(messageExpected, message);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CancelGZQHOrder
        ///</summary>
        [TestMethod()]
        public void CancelStockIndexFuturesOrderTest()
        {
            OrderAccepter target = new OrderAccepter(); // TODO: Initialize to an appropriate value
            string OrderId = string.Empty; // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            string messageExpected = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            int errCode = 0;
            actual = target.CancelStockIndexFuturesOrder(OrderId, ref message,out errCode);
            Assert.AreEqual(messageExpected, message);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CancelStockOrder
        ///</summary>
        [TestMethod()]
        public void CancelStockOrderTest()
        {
            OrderAccepter target = new OrderAccepter(); // TODO: Initialize to an appropriate value
            string OrderId = string.Empty; // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            string messageExpected = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            //actual = target.CancelStockOrder(OrderId, ref message);
            //Assert.AreEqual(messageExpected, message);
            //Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DoMercantileFuturesOrder
        ///</summary>
        [TestMethod()]
        public void DoMercantileFuturesOrderTest()
        {
            OrderAccepter target = new OrderAccepter(); // TODO: Initialize to an appropriate value
            MercantileFuturesOrderRequest futuresorder = null; // TODO: Initialize to an appropriate value
            OrderResponse expected = null; // TODO: Initialize to an appropriate value
            OrderResponse actual;
            actual = target.DoMercantileFuturesOrder(futuresorder);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DoStockIndexFuturesOrder
        ///</summary>
        [TestMethod()]
        public void DoStockIndexFuturesOrderTest()
        {
            OrderAccepter target = new OrderAccepter(); // TODO: Initialize to an appropriate value
            StockIndexFuturesOrderRequest futuresorder = null; // TODO: Initialize to an appropriate value
            OrderResponse expected = null; // TODO: Initialize to an appropriate value
            OrderResponse actual;
            actual = target.DoStockIndexFuturesOrder(futuresorder);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DoStockOrder
        ///</summary>
        [TestMethod()]
        public void DoStockOrderTest()
        {
            InitRealtimeMarketComponent();
            OrderAccepter target = new OrderAccepter(); // TODO: Initialize to an appropriate value
            StockOrderRequest stockorder = BuildStockOrderRequest(); // TODO: Initialize to an appropriate value
            OrderResponse expected = null; // TODO: Initialize to an appropriate value
            OrderResponse actual;
            actual = target.DoStockOrder(stockorder);
            Assert.IsNotNull(actual.OrderId);
        }

        StockOrderRequest BuildStockOrderRequest()
        {
            var result = new StockOrderRequest();
            //ͨ������ͨ��ID
            result.ChannelID = "A";
            //��������
            result.BuySell = GTA.VTS.Common.CommonObject.Types.TransactionDirection.Buying;
            //����
            result.Code = "000001";
            //�ʽ��ʻ�
            result.FundAccountId ="XH0001";
            //����Ա����
            result.TraderPassword = "000";
            //ί����
            result.OrderAmount = 100;
            //ί�м�
            result.OrderPrice = 44.58F;
            //���׵�λ��
            result.OrderUnitType = Types.UnitType.Thigh;
            //�޼�ί��/�м�ί��
            result.OrderWay = ReckoningCounter.Entity.Contants.Types.OrderPriceType.OPTLimited;
            //Ͷ���ϵ
            result.PortfoliosId = "1";

            return result;
        }


        #region == �����ʼ�� ==


        /// <summary>
        /// ��������µ�����
        /// </summary>
        private GTASocket SocketService = null;
        /// <summary>
        /// ��ʼ���������
        /// </summary>
        void InitRealtimeMarketComponent()
        {
            //��½����
            GTASocketSingletonForRealTime.StartLogin("rtuser", "11", ShowConnectMessage);
            //���ݽ���ע���¼�
            this.FEvent += Event;
            SocketService = GTASocketSingletonForRealTime.SocketService;
            GTASocketSingletonForRealTime.SocketStateChanged += GTASocketSingleton_SocketStateChanged;
          //  SocketService.AddEventDelegate(this.FEvent);
            GTASocketSingletonForRealTime.SocketStatusHandle(SocketStatusChange);
            IRealtimeMarketService rms = RealtimeMarketServiceFactory.GetService();

        }

        /// <summary>
        /// Socket״̬�ı䴥���¼�
        /// </summary>
        /// <param name="_e"></param>
        private delegate void DelegateSocketStatusChange(SocketServiceStatusEventArg _e);

        /// <summary>
        /// ״̬�ı䴥���¼�
        /// </summary>
        /// <param name="e"></param>
        private void SocketStatusChange(SocketServiceStatusEventArg e)
        {
           


        }

        /// <summary>
        /// ״̬�����ݲ�ʹ��)
        /// </summary>
        /// <param name="state"></param>
        void GTASocketSingleton_SocketStateChanged(EnumSocketResetState state)
        {

        }

        /// <summary>
        /// ��ʾ��½��Ϣ
        /// </summary>
        /// <param name="e"></param>
        private void ShowConnectMessage(SocketServiceStatusEventArg e)
        {
           
        }


        /// <summary>
        /// ��Ϣ��ӡ�¼�
        /// </summary>
        public GTAMarketSocket.OnEventDelegate FEvent;


        /// <summary>
        /// �����Ϣ
        /// </summary>
        public void ClearEchoInfo()
        {
         
        }

        /// <summary>
        /// ��Ϣ����
        /// </summary>
        /// <param name="eventMessage"></param>
        public void Event(string eventMessage)
        {
           // MessageDisplayHelper.Event(eventMessage, lstMessages);
        }

        #endregion

        ///2009-04-09�¼ӵĲ��Է���
        /// <summary>
        ///DoStockIndexFuturesOrder �Ĳ���
        ///</summary>
        [TestMethod()]
        public void DoStockIndexFuturesOrderTest1()
        {
            OrderAccepter target = new OrderAccepter(); // TODO: ��ʼ��Ϊ�ʵ���ֵ
            StockIndexFuturesOrderRequest futuresorder = null; // TODO: ��ʼ��Ϊ�ʵ���ֵ
            futuresorder = new StockIndexFuturesOrderRequest();
            //��ֵ
            futuresorder.BuySell = Types.TransactionDirection.Buying;
            futuresorder.Code = "IF0904";
            futuresorder.ChannelID = "0";
            futuresorder.FundAccountId = "010000002406";
            futuresorder.OpenCloseType = ReckoningCounter.Entity.Contants.Types.FutureOpenCloseType.OpenPosition;
            futuresorder.OrderAmount = 1;
            futuresorder.OrderPrice = 2505;
            futuresorder.OrderUnitType = GTA.VTS.Common.CommonObject.Types.UnitType.Hand;
            futuresorder.OrderWay = ReckoningCounter.Entity.Contants.Types.OrderPriceType.OPTLimited;
            futuresorder.TraderId = "24";
            futuresorder.TraderPassword = "888888";
            futuresorder.PortfoliosId = "0";

            OrderResponse expected = new OrderResponse();  //null; // TODO: ��ʼ��Ϊ�ʵ���ֵ
            expected.OrderId = "123456789";//������
            OrderResponse actual;
            actual = target.DoStockIndexFuturesOrder(futuresorder);
            Assert.AreNotEqual(expected, actual);
            // Assert.Inconclusive("��֤�˲��Է�������ȷ�ԡ�");
        }
    }
}
