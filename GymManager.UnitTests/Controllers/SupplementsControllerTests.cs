using GymManager.Controllers;
using NUnit.Framework;
using System.Web.Mvc;

namespace GymManager.UnitTests.Controllers
{
    [TestFixture]
    public class SupplementsControllerTests
    {
        private SupplementsController controller;

        [SetUp]
        public void SetUp()
        {
            controller = new SupplementsController();
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
    }
}
