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
    public class EmployeesControllerTests
    {
        private EmployeesController controller;
        private Mock<IUnitOfWork> unitOfWork;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://localhost:5000/Employees/Edit", UriKind.Absolute)
            };
            var controllerContext = new HttpControllerContext
            {
                Request = request
            };
            controller = new EmployeesController(unitOfWork.Object)
            {
                ControllerContext = controllerContext
            };
        }

        [Test]
        public void GetEmployees_DatabaseReturnEmployees_ReturnCorrectEmployees()
        {
            unitOfWork.Setup(uow => uow.Employees.GetAll())
                .Returns(GetEmployeesList());

            var response = controller.GetEmployees() as OkNegotiatedContentResult<IEnumerable<ApplicationUserDto>>;
            var employees = response.Content;

            Assert.IsNotNull(response);
            Assert.That(employees.Count, Is.EqualTo(2));
            Assert.That(employees.ElementAt(0).Name, Is.EqualTo("Test"));
            Assert.That(employees.ElementAt(0).JobTitle, Is.EqualTo("Test"));
        }

        [Test]
        public void GetEmployee_EmployeeNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Employees.SingleOrDefault(e => e.Id == It.IsAny<string>()))
                .Returns<ApplicationUser>(null);

            var response = controller.GetEmployee(It.IsAny<string>());

            Assert.That(response, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void GetEmployee_EmployeeFound_ReturnCorrectEmployee()
        {
            var employee = new ApplicationUser();
            string id = "1";

            unitOfWork.Setup(uow => uow.Employees.SingleOrDefault(e => e.Id == id))
                .Returns(employee);

            var response = controller.GetEmployee(id) as OkNegotiatedContentResult<ApplicationUserDto>;
            var result = response.Content;

            Assert.IsNotNull(response);
            Assert.AreEqual(result, Mapper.Map<ApplicationUser, ApplicationUserDto>(employee));
        }

        [Test]
        public void UpdateEmployee_ModelIsNotValid_ReturnBadRequest()
        {
            controller.ModelState.AddModelError("key", "error message");

            var result = controller.UpdateEmployee(It.IsAny<string>(), new ApplicationUserDto());

            Assert.That(result, Is.InstanceOf(typeof(BadRequestResult)));
        }

        [Test]
        public void UpdateEmployee_EmployeeNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Employees.SingleOrDefault(e => e.Id == It.IsAny<string>()))
                .Returns<ApplicationUser>(null);

            var result = controller.UpdateEmployee(It.IsAny<string>(), new ApplicationUserDto());

            Assert.That(result, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void UpdateEmployee_ModelValidAndEmployeeFound_ReturnOk()
        {
            string id = "1";
            unitOfWork.Setup(uow => uow.Employees.SingleOrDefault(e => e.Id == id))
                .Returns(new ApplicationUser());


            var result = controller.UpdateEmployee(id, new ApplicationUserDto());

            Assert.That(result, Is.InstanceOf(typeof(OkResult)));
        }


        [Test]
        public void DeleteEmployee_EmployeeNotFound_ReturnNotFound()
        {
            unitOfWork.Setup(uow => uow.Employees.SingleOrDefault(e => e.Id == It.IsAny<string>()))
                .Returns<ApplicationUser>(null);

            var result = controller.DeleteEmployee(It.IsAny<string>());

            Assert.That(result, Is.InstanceOf(typeof(NotFoundResult)));
        }

        [Test]
        public void DeleteEmployee_EmployeeFound_ReturnOk()
        {
            string id = "1";
            unitOfWork.Setup(uow => uow.Employees.SingleOrDefault(e => e.Id == id))
                .Returns(new ApplicationUser());

            var result = controller.DeleteEmployee(id);

            Assert.That(result, Is.InstanceOf(typeof(OkResult)));
        }

        private IEnumerable<ApplicationUser> GetEmployeesList()
        {
            return new List<ApplicationUser>
            {
                new ApplicationUser() {  Name="Test", JobTitle = "Test"},
                new ApplicationUser() {  Name="Example", JobTitle = "Example"}
            };
        }
    }
}
