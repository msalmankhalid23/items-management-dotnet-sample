using ItemsManagementWebApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ItemsManagementWebApp.Tests.Controllers
{
    [TestClass]
    public class WebAppItemControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            ItemController controller = new ItemController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            ItemController controller = new ItemController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);

            // Assert
            Assert.AreEqual("Press create button to Add Items.", result.ViewBag.Message);
        }
    }
}
