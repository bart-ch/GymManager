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
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace GymManager.UnitTests.Controllers.Api
{
    [TestFixture]
    public class EquipmentControllerTests
    {
        private EquipmentController controller;
        private Mock<IUnitOfWork> unitOfWork;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://localhost:5000/Equipment/New", UriKind.Absolute)
            };
            var controllerContext = new HttpControllerContext
            {
                Request = request
            };
            controller = new EquipmentController(unitOfWork.Object)
            {
                ControllerContext = controllerContext
            };
        }


        [Test]
        public void GetEquipment_DatabaseReturnEquipment_ReturnCorrectEquipment()
        {
            unitOfWork.Setup(uow => uow.Equipment.GetEquipmentWithAreasAndTypes())
                .Returns(GetEquipmentList());

            var response = controller.GetEquipment() as OkNegotiatedContentResult<IEnumerable<EquipmentDto>>;
            var equipment = response.Content;

            Assert.IsNotNull(response);
            Assert.That(equipment.Count, Is.EqualTo(2));
            Assert.That(equipment.ElementAt(0).Id, Is.EqualTo(1));
            Assert.That(equipment.ElementAt(0).Brand, Is.EqualTo("Test"));
        }

        [Test]
        public void GetSingleEquipment_EquipmentNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Equipment
                .GetSingleOrDefaultEquipmentWithAreaAndType(e => e.Id == 1))
                .Returns<Equipment>(null);

            var response = controller.GetSingleEquipment(1);

            Assert.That(response, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void GetSingleEquipment_EquipmentFound_ReturnCorrectEquipment()
        {
            var equipment = new Equipment();
            var id = 1;

            unitOfWork.Setup(uow => uow.Equipment
                .GetSingleOrDefaultEquipmentWithAreaAndType(e => e.Id == id))
                .Returns(equipment);

            var response = controller.GetSingleEquipment(id) as OkNegotiatedContentResult<EquipmentDto>;
            var result = response.Content;

            Assert.IsNotNull(response);
            Assert.AreEqual(result, Mapper.Map<Equipment, EquipmentDto>(equipment));
        }


        [Test]
        public void CreateEquipment_ModelIsNotValid_ReturnBadRequest()
        {
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.CreateEquipment(new EquipmentDto());

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void CreateEquipment_ModelIsValid_ReturnCreated()
        {
            var equipmentDto = new EquipmentDto();
            var equipment = Mapper.Map<EquipmentDto, Equipment>(equipmentDto);
            unitOfWork.Setup(uow => uow.Equipment.Add(equipment));
            unitOfWork.Setup(uow => uow.Complete());

            var result = controller.CreateEquipment(equipmentDto);

            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf(typeof(CreatedNegotiatedContentResult<EquipmentDto>)));
        }

        [Test]
        public void UpdateEquipment_ModelIsNotValid_ReturnBadRequest()
        {
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.UpdateEquipment(1, new EquipmentDto());

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void UpdateEquipment_EquipmentNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Equipment
                .SingleOrDefault(e => e.Id == 1))
                .Returns<Equipment>(null);


            var result = controller.UpdateEquipment(1, new EquipmentDto());

            Assert.That(result, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void UpdateEquipment_EquipmentFound_ReturnOk()
        {
            var id = 1;
            unitOfWork.Setup(uow => uow.Equipment
                .SingleOrDefault(e => e.Id == id))
                .Returns(new Equipment());


            var result = controller.UpdateEquipment(id, new EquipmentDto());

            unitOfWork.Verify(uow => uow.Complete());
            Assert.That(result, Is.InstanceOf(typeof(OkResult)));
        }

        [Test]
        public void DeleteEquipment_EquipmentNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Equipment
                .SingleOrDefault(e => e.Id == 1))
                .Returns<Equipment>(null);

            var result = controller.DeleteEquipment(1);

            Assert.That(result, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void DeleteEquipment_EquipmentFound_ReturnOk()
        {
            var id = 1;
            unitOfWork.Setup(uow => uow.Equipment
                .SingleOrDefault(e => e.Id == id))
                .Returns(new Equipment());

            var result = controller.DeleteEquipment(id);

            unitOfWork.Verify(uow => uow.Complete());
            Assert.That(result, Is.InstanceOf(typeof(OkResult)));
        }

        private IEnumerable<Equipment> GetEquipmentList()
        {
            return new List<Equipment>()
            {
                new Equipment() {  Id = 1, Brand="Test", Model = "Test"},
                new Equipment() {  Id = 2, Brand="Example", Model = "Example"}
            };
        }
    }
}
