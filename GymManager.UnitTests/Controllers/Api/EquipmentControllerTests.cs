using AutoMapper;
using GymManager.App_Start;
using GymManager.Controllers.Api;
using GymManager.Core;
using GymManager.Core.Domain;
using GymManager.Dtos;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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
            controller = new EquipmentController(unitOfWork.Object);
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
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
        public void GetSingleEquipment_EquipmentNotFound_ReturnNotFoundResult()
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
