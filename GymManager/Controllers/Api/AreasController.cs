using AutoMapper;
using GymManager.Core;
using GymManager.Core.Domain;
using GymManager.Dtos;
using System.Linq;
using System.Web.Http;

namespace GymManager.Controllers.Api
{
    public class AreasController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public AreasController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetAreas()
        {
            var areaDtos = unitOfWork.Areas
                .GetAll()
                .Select(Mapper.Map<Area, AreaDto>);

            return Ok(areaDtos);
        }
    }
}
