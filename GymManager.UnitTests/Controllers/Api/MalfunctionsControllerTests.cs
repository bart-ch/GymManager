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
            var malfunctions = response.Content;

            Assert.That(malfunctions.Count, Is.EqualTo(2));
            Assert.That(malfunctions.ElementAt(0).Id, Is.EqualTo(equipmentId));
        }

        [Test]
        public void CreateMalfunction_ModelIsNotValid_ReturnBadRequest()
        {
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.CreateMalfunction(new MalfunctionDto());

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void CreateMalfunction_ModelIsValid_ReturnCreated()
        {
            var malfunctionDto = new MalfunctionDto { IsRepaired = true };
            var malfunction = Mapper.Map<MalfunctionDto, Malfunction>(malfunctionDto);
            unitOfWork.Setup(uow => uow.Malfunctions.Add(malfunction));
            unitOfWork.Setup(uow => uow.Complete());

            var result = controller.CreateMalfunction(malfunctionDto);

            Assert.That(result, Is.InstanceOf(typeof(CreatedNegotiatedContentResult<MalfunctionDto>)));
        }

        [Test]
        public void UpdateMalfunction_ModelIsNotValid_ReturnBadRequest()
        {
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.UpdateMalfunction(It.IsAny<int>(), new MalfunctionDto());

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void UpdateMalfunction_MalfunctionNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Malfunctions.SingleOrDefault(m => m.Id == It.IsAny<int>()))
                .Returns<Malfunction>(null);


            var result = controller.UpdateMalfunction(It.IsAny<int>(), new MalfunctionDto());

            Assert.That(result, Is.InstanceOf(typeof(NotFoundResult)));
        }


        [Test]
        public void UpdateMalfunction_MalfunctionFound_ReturnOk()
        {
            //given
            var id = 1;
            var malfunctionInDb = new Malfunction();
            unitOfWork.Setup(uow => uow.Malfunctions.SingleOrDefault(m => m.Id == id))
                .Returns(malfunctionInDb);         
            
            unitOfWork.Setup(uow => uow.Equipment
                .SingleOrDefault(e => e.Id == malfunctionInDb.EquipmentId))
                .Returns(new Equipment());
            //when
            var result = controller.UpdateMalfunction(id, new MalfunctionDto { IsRepaired = true });
            //then
            Assert.That(result, Is.InstanceOf(typeof(OkResult)));
        }

        [Test]
        public void UpdateMalfunction_MalfunctionIsRepaired_SetEquipmentAsOperational()
        {
            //given
            var id = 1;
            var malfunctionInDb = new Malfunction();
            var equipmentWhoseMalfunctionIsBeingEdited = new Equipment();
            unitOfWork.Setup(uow => uow.Malfunctions.SingleOrDefault(m => m.Id == id))
                .Returns(malfunctionInDb);

            unitOfWork.Setup(uow => uow.Equipment
                .SingleOrDefault(e => e.Id == malfunctionInDb.EquipmentId))
                .Returns(equipmentWhoseMalfunctionIsBeingEdited);
            //when
            controller.UpdateMalfunction(id, new MalfunctionDto { IsRepaired = true });
            //then
            Assert.IsTrue(equipmentWhoseMalfunctionIsBeingEdited.IsOperational);
        }

        [Test]
        public void UpdateMalfunction_MalfunctionIsNotRepaired_SetEquipmentAsNonOperational()
        {
            //given
            var id = 1;
            var malfunctionInDb = new Malfunction();
            var equipmentWhoseMalfunctionIsBeingEdited = new Equipment();
            unitOfWork.Setup(uow => uow.Malfunctions.SingleOrDefault(m => m.Id == id))
                .Returns(malfunctionInDb);

            unitOfWork.Setup(uow => uow.Equipment
                .SingleOrDefault(e => e.Id == malfunctionInDb.EquipmentId))
                .Returns(equipmentWhoseMalfunctionIsBeingEdited);
            //when
            controller.UpdateMalfunction(id, new MalfunctionDto { IsRepaired = false });
            //then
            Assert.IsFalse(equipmentWhoseMalfunctionIsBeingEdited.IsOperational);
        }

        [Test]
        public void DeleteMalfunction_MalfunctionNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Malfunctions.SingleOrDefault(m => m.Id == 1))
                .Returns<Malfunction>(null);

            var result = controller.DeleteMalfunction(1);

            Assert.That(result, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void DeleteMalfunction_MalfunctionFound_ReturnOk()
        {
            //given
            var id = 1;
            var malfunctionInDb = new Malfunction();
            unitOfWork.Setup(uow => uow.Malfunctions.SingleOrDefault(m => m.Id == id))
                .Returns(new Malfunction());
            unitOfWork.Setup(uow => uow.Equipment
                .SingleOrDefault(e => e.Id == malfunctionInDb.EquipmentId)).Returns(new Equipment());
            //when
            var result = controller.DeleteMalfunction(id);
            //then
            Assert.That(result, Is.InstanceOf(typeof(OkResult)));
        }

        [Test]
        public void DeleteMalfunction_MalfunctionFoundAndUnrepairedMalfunctionsLessOrEqualToZero_SetEquipmentIsOperationalToTrue()
        {
            //given
            var id = 1;
            var malfunctionInDb = new Malfunction();
            var equipmentWhoseMalfunctionIsBeingDeleted = new Equipment();

            unitOfWork.Setup(uow => uow.Malfunctions.SingleOrDefault(m => m.Id == id))
                .Returns(new Malfunction());

            unitOfWork.Setup(uow => uow.Equipment
                .SingleOrDefault(e => e.Id == malfunctionInDb.EquipmentId)).Returns(equipmentWhoseMalfunctionIsBeingDeleted);            
            
            unitOfWork.Setup(uow => uow.Malfunctions
                .Find(m => m.EquipmentId == malfunctionInDb.EquipmentId
                && m.Id != malfunctionInDb.Id
                && !m.IsRepaired)).Returns(new List<Malfunction>());

            //when
            controller.DeleteMalfunction(id);
            //then
            Assert.IsTrue(equipmentWhoseMalfunctionIsBeingDeleted.IsOperational);
        }

        [Test]
        public void DeleteMalfunction_MalfunctionFoundAndUnrepairedMalfunctionsGreaterThanZero_SetEquipmentIsOperationalToFalse()
        {
            //given
            var id = 1;
            var malfunctionInDb = new Malfunction();
            var equipmentWhoseMalfunctionIsBeingDeleted = new Equipment();

            unitOfWork.Setup(uow => uow.Malfunctions.SingleOrDefault(m => m.Id == id))
                .Returns(new Malfunction());

            unitOfWork.Setup(uow => uow.Equipment
                .SingleOrDefault(e => e.Id == malfunctionInDb.EquipmentId)).Returns(equipmentWhoseMalfunctionIsBeingDeleted);

            unitOfWork.Setup(uow => uow.Malfunctions
                .Find(m => m.EquipmentId == malfunctionInDb.EquipmentId
                && m.Id != malfunctionInDb.Id
                && !m.IsRepaired)).Returns(GetMalfunctionsList());

            //when
            controller.DeleteMalfunction(id);
            //then
            Assert.IsFalse(equipmentWhoseMalfunctionIsBeingDeleted.IsOperational);
        }

        private IEnumerable<Malfunction> GetMalfunctionsList()
        {
            return new List<Malfunction>
            {
                new Malfunction() {  Id = 1, Title="Test" },
                new Malfunction() {  Id = 2, Title="Example" }
            };
        }
    }
}
