using AutoMapper;
using GymManager.Core;
using GymManager.Core.Domain;
using GymManager.Dtos;
using System.Linq;
using System.Web.Http;

namespace GymManager.Controllers.Api
{
    public class SupplementsController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public SupplementsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetSupplements()
        {
            var supplementsDto = unitOfWork.Supplements
                .GetSupplementsWithFlavorsAndTypes()
                .Select(Mapper.Map<Supplement, SupplementDto>);

            return Ok(supplementsDto);
        }

        public IHttpActionResult GetSupplement(int id)
        {
            var supplement = unitOfWork.Supplements
                .GetSingleOrDefaultSupplementWithFlavorAndType(e => e.Id == id);

            if (supplement == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Supplement, SupplementDto>(supplement));
        }

        [HttpDelete]
        public IHttpActionResult DeleteSuplement(int id)
        {
            var supplementInDb = unitOfWork.Supplements.SingleOrDefault(s => s.Id == id);

            if (supplementInDb == null)
            {
                return NotFound();
            }

            unitOfWork.Supplements.Remove(supplementInDb);
            unitOfWork.Complete();

            return Ok();
        }
    }
}
