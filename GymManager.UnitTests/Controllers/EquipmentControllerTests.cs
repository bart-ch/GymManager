using GymManager.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GymManager.UnitTests.Controllers
{
    [TestFixture]
    public class EquipmentControllerTests
    {
        private EquipmentController controller;

        [SetUp]
        public void SetUp()
        {
            controller = new EquipmentController();
        }

        [Test]
        public void Index_WhenCalled_ReturnView()
        {
            var result = controller.Index();

            Assert.That(string.Empty, Is.EqualTo(result.ViewName));
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));
        }        
        
        [Test]
        public void New_WhenCalled_ReturnView()
        {
            var result = controller.New();

            Assert.That(string.Empty, Is.EqualTo(result.ViewName));
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));
        }        
        
        [Test]
        public void Edit_WhenCalled_ReturnView()
        {
            var result = controller.Edit();

            Assert.That(string.Empty, Is.EqualTo(result.ViewName));
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));
        }
    }
}
