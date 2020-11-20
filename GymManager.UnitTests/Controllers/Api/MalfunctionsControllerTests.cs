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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace GymManager.UnitTests.Controllers.Api
{
    [TestFixture]
    public class MalfunctionsControllerTests
    {
        private MalfunctionsController controller;
        private Mock<IUnitOfWork> unitOfWork;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://localhost:5000/Malfunctions/New", UriKind.Absolute)
            };
            var controllerContext = new HttpControllerContext
            {
                Request = request
            };
            controller = new MalfunctionsController(unitOfWork.Object)
            {
                ControllerContext = controllerContext
            };
        }

        [Test]
        public void GetMalfunctions_DatabaseReturnsMalfunctions_ReturnCorrectMalfunctions()
        {
            unitOfWork.Setup(uow => uow.Malfunctions.GetMalfunctionsWithEquipment())
                .Returns(GetMalfunctionsList());

            var response = controller.GetMalfunctions() as OkNegotiatedContentResult<IEnumerable<MalfunctionDto>>;
            var supplements = response.Content;

            Assert.IsNotNull(response);
            Assert.That(supplements.Count, Is.EqualTo(2));
            Assert.That(supplements.ElementAt(0).Id, Is.EqualTo(1));
            Assert.That(supplements.ElementAt(0).Title, Is.EqualTo("Test"));
        }

        [Test]
        public void GetMalfunction_MalfunctionNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Malfunctions
                .SingleOrDefault(s => s.Id == It.IsAny<int>()))
                .Returns<Malfunction>(null);

            var response = controller.GetMalfunction(1);

            Assert.That(response, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void GetMalfunction_MalfunctionFound_ReturnCorrectMalfunction()
        {
            var malfunction = new Malfunction();
            var id = 1;

            unitOfWork.Setup(uow => uow.Malfunctions
                .SingleOrDefault(m => m.Id == id))
                .Returns(malfunction);

            var response = controller.GetMalfunction(id) as OkNegotiatedContentResult<MalfunctionDto>;
            var result = response.Content;

            Assert.IsNotNull(response);
            Assert.AreEqual(result, Mapper.Map<Malfunction, MalfunctionDto>(malfunction));
        }

        [Test]
        public void GetMalfunctionsOfGivenEquipment_DatabaseReturnsMalfunctions_ReturnCorrectMalfunctions()
        {
            var equipmentId = 1;
            unitOfWork.Setup(uow => uow.Malfunctions
                .Find(m => m.Equipment.Id == equipmentId))
                .Returns(GetMalfunctionsList());

            var response = controller.GetMalfunctionsOfGivenEquipment(equipmentId) as OkNegotiatedContentResult<IEnumerable<MalfunctionDto>>;
            var supplements = response.Content;

            Assert.IsNotNull(response);
            Assert.That(supplements.Count, Is.EqualTo(2));
            Assert.That(supplements.ElementAt(0).Id, Is.EqualTo(1));
            Assert.That(supplements.ElementAt(0).Title, Is.EqualTo("Test"));
        }

        private IEnumerable<Malfunction> GetMalfunctionsList()
        {
            return new List<Malfunction>
            {
                new Malfunction() {  Id = 1, Title="Test"},
                new Malfunction() {  Id = 2, Title="Example"}
            };
        }
    }
}
