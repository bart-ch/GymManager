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
            unitOfWork.Setup(uow => uow.EquipmentOrders.GetEquipmentOrdersWithUsersAndTypesAndOrderStatuses())
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

        [Test]
        public void UpdateEquipmentOrder_ModelIsNotValid_ReturnBadRequest()
        {
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.UpdateEquipmentOrder(It.IsAny<int>(), new EquipmentOrderDto());

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void UpdateEquipmentOrder_EquipmentOrderNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.EquipmentOrders.SingleOrDefault(eo => eo.Id == It.IsAny<int>()))
                .Returns<EquipmentOrder>(null);


            var result = controller.UpdateEquipmentOrder(It.IsAny<int>(), new EquipmentOrderDto());

            Assert.That(result, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void UpdateEquipmentOrder_EquipmentOrderFound_ReturnOk()
        {
            var id = 1;
            unitOfWork.Setup(uow => uow.EquipmentOrders.SingleOrDefault(eo => eo.Id == id))
                .Returns(new EquipmentOrder());


            var result = controller.UpdateEquipmentOrder(id, new EquipmentOrderDto());

            Assert.That(result, Is.InstanceOf(typeof(OkResult)));
        }

        [Test]
        public void UpdateOrderStatusOfEquipment_GivenOrderStatusDoesntExist_ReturnBadRequest()
        {
            byte orderStatusId = 1;
            unitOfWork.Setup(uow => uow.OrderStatuses.SingleOrDefault(os => os.Id == orderStatusId))
                .Returns<OrderStatus>(null);

            var result = controller.UpdateOrderStatusOfEquipment(It.IsAny<int>(), orderStatusId);

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void UpdateOrderStatusOfEquipment_GivenOrderStatusExistAndEquipmentOrderNotFound_ReturnNotFound()
        {
            //given
            byte orderStatusId = 1;
            var id = 1;
            unitOfWork.Setup(uow => uow.OrderStatuses.SingleOrDefault(os => os.Id == orderStatusId))
                .Returns(new OrderStatus());

            unitOfWork.Setup(uow => uow.EquipmentOrders.SingleOrDefault(eo => eo.Id == id))
                .Returns<OrderStatus>(null);
            //when
            var result = controller.UpdateOrderStatusOfEquipment(id, orderStatusId);
            //then
            Assert.That(result, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void UpdateOrderStatusOfEquipment_GivenOrderStatusExistAndEquipmentOrderFound_ReturnOk()
        {
            //given
            byte orderStatusId = 1;
            var id = 1;
            unitOfWork.Setup(uow => uow.OrderStatuses.SingleOrDefault(os => os.Id == orderStatusId))
                .Returns(new OrderStatus());

            unitOfWork.Setup(uow => uow.EquipmentOrders.SingleOrDefault(eo => eo.Id == id))
                .Returns(new EquipmentOrder());
            //when
            var result = controller.UpdateOrderStatusOfEquipment(id, orderStatusId);
            //then
            Assert.That(result, Is.InstanceOf(typeof(OkResult)));
        }

        [Test]
        public void DeleteEquipmentOrder_EquipmentOrderNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.EquipmentOrders.SingleOrDefault(eo => eo.Id == It.IsAny<int>()))
                .Returns<EquipmentOrder>(null);

            var result = controller.DeleteEquipmentOrder(It.IsAny<int>());

            Assert.That(result, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void DeleteEquipmentOrder_EquipmentOrderFound_ReturnOk()
        {
            var id = 1;
            unitOfWork.Setup(uow => uow.EquipmentOrders.SingleOrDefault(eo => eo.Id == id))
                .Returns(new EquipmentOrder());

            var result = controller.DeleteEquipmentOrder(id);

            Assert.That(result, Is.InstanceOf(typeof(OkResult)));
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
