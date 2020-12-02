using GymManager.Controllers;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace GymManager.UnitTests.Controllers
{
    [TestFixture]
    public class MalfunctionsControllerTests
    {
        private MalfunctionsController controller;

        [SetUp]
        public void SetUp()
        {
            controller = new MalfunctionsController();
        }

        [Test]
        public void Index_WhenCalled_ReturnView()
        {
            var result = controller.Index();

            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));
        }        

        [Test]
        public void New_WhenCalled_ReturnView()
        {
            var result = controller.New();

            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));
        }

        [Test]
        public void Edit_WhenCalled_ReturnView()
        {
            var result = controller.Edit();

            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));
        }

        [Test]
        public void History_WhenCalled_ReturnView()
        {
            var result = controller.History();

            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));
        }

        [Test]
        public void HistoryOverloaded_WhenCalled_ReturnView()
        {
            var viewName = "SingleEquipmentHistory";

            var result = controller.History(It.IsAny<int>());

            Assert.That(result.ViewName, Is.EqualTo(viewName));
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));
        }

        [Test]
        public void Details_WhenCalled_ReturnView()
        {
            var result = controller.Details();

            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));
        }
    }
}
