using AutoMapper;
using GymManager.Core;
using GymManager.Core.Domain;
using GymManager.Dtos;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace GymManager.Controllers.Api
{
    public class FlavorsController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public FlavorsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        public IHttpActionResult  GetFlavors()
        {
            var flavorsDtos = unitOfWork.Flavors
                .GetAll()
                .Select(Mapper.Map<Flavor, FlavorDto>);

            return Ok(flavorsDtos);
        }
    }
}