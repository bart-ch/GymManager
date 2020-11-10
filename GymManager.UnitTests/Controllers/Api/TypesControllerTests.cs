using AutoMapper;
using GymManager.App_Start;
using GymManager.Controllers.Api;
using GymManager.Core;
using GymManager.Dtos;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

namespace GymManager.UnitTests.Controllers.Api
{
    [TestFixture]
    public class TypesControllerTests
    {
        private TypesController controller;
        private Mock<IUnitOfWork> unitOfWork;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            controller = new TypesController(unitOfWork.Object);
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }

        [Test]
        public void GetTypes_DatabaseReturnTypes_ReturnCorrectTypes()
        {
            unitOfWork.Setup(uow => uow.Types.GetAll())
                .Returns(GetTypesList());

            var response = controller.GetTypes() as OkNegotiatedContentResult<IEnumerable<TypeDto>>;
            var areas = response.Content;

            Assert.IsNotNull(response);
            Assert.That(areas.Count, Is.EqualTo(2));
            Assert.That(areas.ElementAt(0).Id, Is.EqualTo(1));
            Assert.That(areas.ElementAt(0).Name, Is.EqualTo("Example"));
        }

        private IEnumerable<Core.Domain.Type> GetTypesList()
        {
            return new List<Core.Domain.Type>()
            {
                new Core.Domain.Type() {  Id = 1, Name = "Example"},
                new Core.Domain.Type() {  Id = 2, Name = "Test"}
            };

        }
    }
}
