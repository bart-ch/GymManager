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
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace GymManager.UnitTests.Controllers.Api
{
    [TestFixture]
    public class EquipmentOrdersControllerTests
    {
        private EquipmentOrdersController controller;
        private Mock<IUnitOfWork> unitOfWork;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://localhost:5000/EquipmentOrders/New", UriKind.Absolute)
            };
            var controllerContext = new HttpControllerContext
            {
                Request = request
            };

            var claim = new Claim("Test", "Id");
            var mockIdentity =
                Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == claim);

            controller = new EquipmentOrdersController(unitOfWork.Object)
            {
                ControllerContext = controllerContext,
                User = Mock.Of<IPrincipal>(ip => ip.Identity == mockIdentity)
            };
        }

        [Test]
        public void GetEquipmentOrders_DatabaseReturnEquipmentOrders_ReturnCorrectEquipmentOrders()
        {
            unitOfWork.Setup(uow => uow.EquipmentOrders.GetEquipmentOrdersWithTypesAndOrderStatuses())
                .Returns(GetEquipmentOrdersList());

            var response = controller.GetEquipmentOrders() as OkNegotiatedContentResult<IEnumerable<EquipmentOrderDto>>;
            var equipmentOrder = response.Content;

            Assert.IsNotNull(response);
            Assert.That(equipmentOrder.Count, Is.EqualTo(2));
            Assert.That(equipmentOrder.ElementAt(0).Id, Is.EqualTo(1));
            Assert.That(equipmentOrder.ElementAt(0).Brand, Is.EqualTo("Test"));
        }

        [Test]
        public void GetEquipmentOrder_EquipmentOrderNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.EquipmentOrders
                .GetSingleEquipmentOrderWithTypeAndOrderStatus(eo => eo.Id == It.IsAny<int>()))
                .Returns<EquipmentOrder>(null);

            var response = controller.GetEquipmentOrder(It.IsAny<int>());

            Assert.That(response, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void GetEquipmentOrder_EquipmentOrderFound_ReturnCorrectEquipmentOrder()
        {
            var equipmentOrder = new EquipmentOrder();
            var id = 1;

            unitOfWork.Setup(uow => uow.EquipmentOrders
                .GetSingleEquipmentOrderWithTypeAndOrderStatus(eo => eo.Id == id))
                .Returns(equipmentOrder);

            var response = controller.GetEquipmentOrder(id) as OkNegotiatedContentResult<EquipmentOrderDto>;
            var result = response.Content;

            Assert.IsNotNull(response);
            Assert.AreEqual(result, Mapper.Map<EquipmentOrder, EquipmentOrderDto>(equipmentOrder));
        }

        [Test]
        public void CreateEquipmentOrder_ModelIsNotValid_ReturnBadRequest()
        {
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.CreateEquipmentOrder(new EquipmentOrderDto());

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void CreateEquipmentOrder_ModelIsValid_ReturnCreated()
        {
            var equipmentOrderDto = new EquipmentOrderDto();
            var equipmentOrder = Mapper.Map<EquipmentOrderDto, EquipmentOrder>(equipmentOrderDto);
            unitOfWork.Setup(uow => uow.EquipmentOrders.Add(equipmentOrder));
            unitOfWork.Setup(uow => uow.Complete());

            var result = controller.CreateEquipmentOrder(equipmentOrderDto);

            Assert.That(result, Is.InstanceOf(typeof(CreatedNegotiatedContentResult<EquipmentOrderDto>)));
        }

        private IEnumerable<EquipmentOrder> GetEquipmentOrdersList()
        {
            return new List<EquipmentOrder>
            {
                new EquipmentOrder() {  Id = 1, Brand="Test", Model = "Test"},
                new EquipmentOrder() {  Id = 2, Brand="Example", Model = "Example"}
            };
        }
    }
}
