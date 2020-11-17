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
    public class FlavorsControllerTests
    {
        private FlavorsController controller;
        private Mock<IUnitOfWork> unitOfWork;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://localhost:5000/Flavors/Index", UriKind.Absolute)
            };
            var controllerContext = new HttpControllerContext
            {
                Request = request
            };
            controller = new FlavorsController(unitOfWork.Object)
            {
                ControllerContext = controllerContext
            };
        }

        [Test]
        public void GetFlavors_DatabaseReturnFlavors_ReturnCorrectFlavors()
        {
            unitOfWork.Setup(uow => uow.Flavors.GetAll())
                .Returns(GetFlavorsList());

            var response = controller.GetFlavors() as OkNegotiatedContentResult<IEnumerable<FlavorDto>>;
            var flavors = response.Content;

            Assert.IsNotNull(response);
            Assert.That(flavors.Count, Is.EqualTo(2));
            Assert.That(flavors.ElementAt(0).Id, Is.EqualTo(1));
            Assert.That(flavors.ElementAt(0).Name, Is.EqualTo("Test"));
        }

        [Test]
        public void GetFlavor_FlavorNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Flavors
                .SingleOrDefault(f => f.Id == 1))
                .Returns<Flavor>(null);

            var response = controller.GetFlavor(1);

            Assert.That(response, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void GetFlavor_FlavorFound_ReturnCorrectFlavor()
        {
            var flavor = new Flavor();
            var id = 1;

            unitOfWork.Setup(uow => uow.Flavors
                .SingleOrDefault(f => f.Id == id))
                .Returns(flavor);

            var response = controller.GetFlavor(id) as OkNegotiatedContentResult<FlavorDto>;
            var result = response.Content;

            Assert.IsNotNull(response);
            Assert.AreEqual(result, Mapper.Map<Flavor, FlavorDto>(flavor));
        }

        [Test]
        public void CreateFlavor_ModelIsNotValid_ReturnBadRequest()
        {
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.CreateFlavor(new FlavorDto());

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void CreateFlavor_NameAlreadyExists_ReturnBadRequest()
        {
            var flavorDto = new FlavorDto();
            unitOfWork.Setup(uof => uof.Flavors.SingleOrDefault(f => f.Name == flavorDto.Name))
                .Returns(new Flavor());

            var result = controller.CreateFlavor(new FlavorDto());

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void CreateFlavor_ModelIsValid_ReturnCreated()
        {
            var flavorDto = new FlavorDto();
            var flavor = Mapper.Map<FlavorDto, Flavor>(flavorDto);
            unitOfWork.Setup(uow => uow.Flavors.Add(flavor));
            unitOfWork.Setup(uow => uow.Complete());

            var result = controller.CreateFlavor(flavorDto);

            Assert.That(result, Is.InstanceOf(typeof(CreatedNegotiatedContentResult<FlavorDto>)));
        }

        [Test]
        public void UpdateFlavor_ModelIsNotValid_ReturnBadRequest()
        {
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.UpdateFlavor(It.IsAny<int>(), new FlavorDto());

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void UpdateFlavor_FlavorIsOther_ReturnBadRequest()
        {
            unitOfWork.Setup(uow => uow.Flavors.SingleOrDefault(f => f.Name == "Other"))
                .Returns(new Flavor { Id = 1 });

            var result = controller.UpdateFlavor(1, new FlavorDto());

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void UpdateFlavor_FlavorWithGivenNameExists_ReturnBadRequest()
        {
            var flavorDto = new FlavorDto { Name = "Example" };
            unitOfWork.Setup(uow => uow.Flavors.SingleOrDefault(f => f.Name == "Other"))
                .Returns(new Flavor());
            unitOfWork.Setup(uow => uow.Flavors.SingleOrDefault(e => e.Name == flavorDto.Name))
                .Returns(new Flavor { Name = "Example" });

            var result = controller.UpdateFlavor(It.IsAny<int>(), flavorDto);

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void UpdateFlavor_SupplementFoundAndModelIsValidAndIsNotOther_ReturnOk()
        {
            var id = 1;
            unitOfWork.Setup(uow => uow.Flavors.SingleOrDefault(f => f.Name == "Other"))
                .Returns(new Flavor());
            unitOfWork.Setup(uow => uow.Flavors.SingleOrDefault(f => f.Id == id))
                .Returns(new Flavor());

            var result = controller.UpdateFlavor(id, new FlavorDto());

            unitOfWork.Verify(uow => uow.Complete());
            Assert.That(result, Is.InstanceOf(typeof(OkResult)));
        }

        [Test]
        public void DeleteFlavor_FlavorNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Flavors
                .SingleOrDefault(f => f.Id == It.IsAny<int>()))
                .Returns<FlavorDto>(null);

            var result = controller.DeleteFlavor(It.IsAny<int>());

            Assert.That(result, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void DeleteFlavor_FlavorIsOther_ReturnBadRequest()
        {
            var id = 1;
            unitOfWork.Setup(uow => uow.Flavors
                .SingleOrDefault(f => f.Id == id))
                .Returns(new Flavor { Name = "Other" });

            var result = controller.DeleteFlavor(id);

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void DeleteFlavor_FlavorFoundAndIsNotOther_ReturnOk()
        {
            var id = 1;
            unitOfWork.Setup(uow => uow.Flavors.SingleOrDefault(f => f.Id == id))
                .Returns(new Flavor());            
            
            unitOfWork.Setup(uow => uow.Supplements.Find(s => s.FlavorId == id))
                .Returns(new List<Supplement>());

            var result = controller.DeleteFlavor(id);

            Assert.That(result, Is.InstanceOf(typeof(OkResult)));
        }

        [Test]
        public void DeleteFlavor_DeletingFlavorIsAssignedToExistingSupplements_ChangeSupplementsFlavorToOther()
        {
            //given
            var id = 1;
            var otherFlavorId = 2;
            var supplementWhoseFlavorIsBeingDeleted = new Supplement { FlavorId = (byte)id };
            var supplementList = new List<Supplement> { supplementWhoseFlavorIsBeingDeleted };
            unitOfWork.Setup(uow => uow.Flavors.SingleOrDefault(f => f.Id == id)).Returns(new Flavor());
            unitOfWork.Setup(uow => uow.Supplements.Find(s => s.FlavorId == id)).Returns(supplementList);
            unitOfWork.Setup(uow => uow.Flavors.SingleOrDefault(f => f.Name == "Other"))
                .Returns(new Flavor { Id = (byte) otherFlavorId });
            //when
            controller.DeleteFlavor(id);
            //then
            Assert.That(supplementWhoseFlavorIsBeingDeleted.FlavorId, Is.EqualTo(otherFlavorId));
        }

        private IEnumerable<Flavor> GetFlavorsList()
        {
            return new List<Flavor>
            {
                new Flavor() {  Id = 1, Name="Test"     },
                new Flavor() {  Id = 2, Name="Example"  }
            };
        }
    }
}
