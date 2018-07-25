using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Web.Mvc;
using System.Net;

using OrderManagement.Controllers;
using OrderProductDBModel;

namespace OrderManagement.Tests
{
    [TestClass]
    public class OrdersContorllerTests
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        private string GettestValueFromColumn(string columnHeading)
        {
            var val = TestContext.DataRow[columnHeading].ToString();
            return val;
        }


        [TestMethod]
        public void IndexAction_Returns_IndexView()
        {
            //Arrange
            OrdersController oc = new OrdersController();

            //Act
            ViewResult actualResult = oc.Index() as ViewResult;
            string actualViewName = actualResult.ViewName;

            //Assert
            string expectedViewName = "Index";
            Assert.AreEqual(expectedViewName, actualViewName);
        }

        public void DetailAction_Returns_OrderModel()
        {
            //Arrange
            OrdersController oc = new OrdersController();

            //Act
            ViewResult actualResult = oc.Details(1) as ViewResult;
            Type actualModelType = actualResult.Model.GetType();

            //Assert
            Type expectedModelType = typeof(OrderProductDBModel.Order);
            Assert.AreEqual(expectedModelType, actualModelType);
        }

        [TestMethod]

        public void DetailsAction_NullID_BADHTTP()
        {
            //Arrange
            OrdersController oc = new OrdersController();

            //Act
            HttpStatusCodeResult actualResult = oc.Details(null) as HttpStatusCodeResult;
            int actualStatusCode = actualResult.StatusCode;

            //Assert
            int expectedStatusCode = new HttpStatusCodeResult(HttpStatusCode.BadRequest).StatusCode;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }
        [TestMethod]
        public void DetailsAction_InvalidID_NotFoundHTTP()
        {
            //Arrange
            OrdersController oc = new OrdersController();

            //Act
            HttpStatusCodeResult actualResult = oc.Details(-1) as HttpStatusCodeResult;
            int actualStatusCode = actualResult.StatusCode;

            //Assert
            int expectedStatusCode = new HttpStatusCodeResult(HttpStatusCode.NotFound).StatusCode;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }


        [DataSource("System.Data.Odbc", "Dsn=Excel Files;Driver={Microsoft Excel Driver (*.xls)};dbq=D:\\sheridan\\Semester6\\PROG35142 Advanced .NET Server Development\\Practice\\DataDrivenTestingPractice\\OrderManagement.Tests\\OrderTest.xlsx;defaultdir=.;driverid=790;maxbuffersize=2048;pagetimeout=5;readonly=true", "Sheet1$", DataAccessMethod.Sequential), TestMethod]
        public void DetailsAction_Returns_ValidOrder()
        {
            //Arrange
            OrdersController oc = new OrdersController();

            //Act
            int id = int.Parse(GettestValueFromColumn("passedId"));
            ViewResult actualResult = oc.Details(id) as ViewResult;
            Order actualOrder = actualResult.Model as Order;
            string actualDepartment = actualOrder.Department;

            //Assert

            string ExpectedDepartment = GettestValueFromColumn("ExpectedDepartment");
            Assert.AreEqual(ExpectedDepartment, actualDepartment);

        }


    }
}
