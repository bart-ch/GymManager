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
    public class SupplementsControllerTests
    {
        private SupplementsController controller;
        private Mock<IUnitOfWork> unitOfWork;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://localhost:5000/Supplement/New", UriKind.Absolute)
            };
            var controllerContext = new HttpControllerContext
            {
                Request = request
            };
            controller = new SupplementsController(unitOfWork.Object)
            {
                ControllerContext = controllerContext
            };
        }

        [Test]
        public void GetSupplements_DatabaseReturnSupplements_ReturnCorrectSupplements()
        {
            unitOfWork.Setup(uow => uow.Supplements.GetSupplementsWithFlavorsAndTypes())
                .Returns(GetSupplementsList());

            var response = controller.GetSupplements() as OkNegotiatedContentResult<IEnumerable<SupplementDto>>;
            var supplements = response.Content;

            Assert.IsNotNull(response);
            Assert.That(supplements.Count, Is.EqualTo(2));
            Assert.That(supplements.ElementAt(0).Id, Is.EqualTo(1));
            Assert.That(supplements.ElementAt(0).Brand, Is.EqualTo("Test"));
        }

        [Test]
        public void GetSupplement_SupplementNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Supplements
                .GetSingleOrDefaultSupplementWithFlavorAndType(s => s.Id == It.IsAny<int>()))
                .Returns<Supplement>(null);

            var response = controller.GetSupplement(1);

            Assert.That(response, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void GetSupplement_SupplementFound_ReturnCorrectSupplement()
        {
            var supplement = new Supplement();
            var id = 1;

            unitOfWork.Setup(uow => uow.Supplements
                .GetSingleOrDefaultSupplementWithFlavorAndType(s => s.Id == id))
                .Returns(supplement);

            var response = controller.GetSupplement(id) as OkNegotiatedContentResult<SupplementDto>;
            var result = response.Content;

            Assert.IsNotNull(response);
            Assert.AreEqual(result, Mapper.Map<Supplement, SupplementDto>(supplement));
        }

        [Test]
        public void CreateSupplement_ModelIsNotValid_ReturnBadRequest()
        {
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.CreateSupplement(new SupplementDto());

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void CreateSupplement_ModelIsValid_ReturnCreated()
        {
            var supplementDto = new SupplementDto();
            var supplement = Mapper.Map<SupplementDto, Supplement>(supplementDto);
            unitOfWork.Setup(uow => uow.Supplements.Add(supplement));
            unitOfWork.Setup(uow => uow.Complete());

            var result = controller.CreateSupplement(supplementDto);

            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf(typeof(CreatedNegotiatedContentResult<SupplementDto>)));
        }

        [Test]
        public void UpdateSupplement_ModelIsNotValid_ReturnBadRequest()
        {
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.UpdateSupplement(It.IsAny<int>(), new SupplementDto());

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void UpdateSupplement_SupplementNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Supplements.SingleOrDefault(s => s.Id == It.IsAny<int>()))
                .Returns<Equipment>(null);


            var result = controller.UpdateSupplement(It.IsAny<int>(), new SupplementDto());

            Assert.That(result, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void UpdateSupplement_SupplementFound_ReturnOk()
        {
            var id = 1;
            unitOfWork.Setup(uow => uow.Supplements.SingleOrDefault(s => s.Id == id))
                .Returns(new Supplement());


            var result = controller.UpdateSupplement(id, new SupplementDto());

            Assert.That(result, Is.InstanceOf(typeof(OkResult)));
        }

        [Test]
        public void DeleteSupplement_SupplementNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Supplements.SingleOrDefault(s => s.Id == 1))
                .Returns<Supplement>(null);

            var result = controller.DeleteSuplement(1);

            Assert.That(result, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void DeleteEquipment_SupplementFound_ReturnOk()
        {
            var id = 1;
            unitOfWork.Setup(uow => uow.Supplements.SingleOrDefault(s => s.Id == id))
                .Returns(new Supplement());

            var result = controller.DeleteSuplement(id);

            Assert.That(result, Is.InstanceOf(typeof(OkResult)));
        }

        private IEnumerable<Supplement> GetSupplementsList()
        {
            return new List<Supplement>
            {
                new Supplement() {  Id = 1, Brand="Test"},
                new Supplement() {  Id = 2, Brand="Example"}
            };
        }
    }
}
