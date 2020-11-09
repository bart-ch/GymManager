using AutoMapper;
using GymManager.App_Start;
using GymManager.Controllers.Api;
using GymManager.Core;
using GymManager.Core.Domain;
using GymManager.Dtos;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace GymManager.UnitTests.Controllers.Api
{
    [TestFixture]
    public class AreasControllerTests
    {
        private AreasController controller;
        private Mock<IUnitOfWork> unitOfWork;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            controller = new AreasController(unitOfWork.Object);
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }

        [Test]
        public void GetAreas_DatabaseReturnAreas_ReturnCorrectAreas()
        {
            unitOfWork.Setup(uow => uow.Areas.GetAll())
                .Returns(GetAreasList());

            var response = controller.GetAreas() as OkNegotiatedContentResult<IEnumerable<AreaDto>>;
            var areas = response.Content;

            Assert.IsNotNull(response);
            Assert.That(areas.Count, Is.EqualTo(2));
            Assert.That(areas.ElementAt(0).Id, Is.EqualTo(1));
            Assert.That(areas.ElementAt(0).Name, Is.EqualTo("Example"));
        }

        private IEnumerable<Area> GetAreasList()
        {

            return new List<Area>()
            {
                new Area() {  Id = 1, Name = "Example"},
                new Area() {  Id = 2, Name = "Test"}
            };

        }
    }
}
