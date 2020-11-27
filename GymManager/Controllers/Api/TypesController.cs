using AutoMapper;
using GymManager.Core;
using GymManager.Dtos;
using System.Linq;
using System.Web.Http;

namespace GymManager.Controllers.Api
{
    [Authorize]
    public class TypesController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public TypesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetTypes()
        {
            var typeDtos = unitOfWork.Types
                .GetAll()
                .Select(Mapper.Map<Core.Domain.Type,TypeDto>);

            return Ok(typeDtos);
        }
    }
}
