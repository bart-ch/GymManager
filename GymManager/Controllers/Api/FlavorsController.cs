using AutoMapper;
using GymManager.Attributes;
using GymManager.Core;
using GymManager.Core.Domain;
using GymManager.Dtos;
using System;
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

        public IHttpActionResult GetFlavors()
        {
            var flavorsDtos = unitOfWork.Flavors
                .GetAll()
                .Select(Mapper.Map<Flavor, FlavorDto>);

            return Ok(flavorsDtos);
        }

        public IHttpActionResult GetFlavor(int id)
        {
            var flavor = unitOfWork.Flavors.SingleOrDefault(f => f.Id == id);

            if (flavor == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Flavor, FlavorDto>(flavor));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IHttpActionResult CreateFlavor(FlavorDto flavorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var flavorInDb = unitOfWork.Flavors.SingleOrDefault(f => f.Name == flavorDto.Name);

            if (flavorInDb != null)
            {
                return BadRequest();
            }

            var flavor = Mapper.Map<FlavorDto, Flavor>(flavorDto);

            unitOfWork.Flavors.Add(flavor);
            unitOfWork.Complete();

            flavorDto.Id = flavor.Id;

            return Created(new Uri(Request.RequestUri + "/" + flavor.Id), flavorDto);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public IHttpActionResult UpdateFlavor(int id, FlavorDto flavorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var otherFlavor = unitOfWork.Flavors.SingleOrDefault(f => f.Name == "Other");
            if (otherFlavor.Id == id)
            {
                return BadRequest();
            }

            var flavorInDbWithGivenName = unitOfWork.Flavors.SingleOrDefault(e => e.Name == flavorDto.Name);
            if (flavorInDbWithGivenName != null)
            {
                return BadRequest();
            }

            var flavorInDb = unitOfWork.Flavors.SingleOrDefault(e => e.Id == id);

            Mapper.Map(flavorDto, flavorInDb);

            unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteFlavor(int id)
        {
            var flavorInDb = unitOfWork.Flavors.SingleOrDefault(f => f.Id == id);

            if (flavorInDb == null)
            {
                return NotFound();
            }
            else if (flavorInDb.Name == "Other")
            {
                return BadRequest();
            }

            var supplementsOfTheFlavor = unitOfWork.Supplements.Find(s => s.FlavorId == id);

            if (supplementsOfTheFlavor.Count() > 0)
            {
                var otherFlavor = unitOfWork.Flavors.SingleOrDefault(f => f.Name == "Other");
                foreach (var supplement in supplementsOfTheFlavor)
                {
                    supplement.FlavorId = otherFlavor.Id;
                }
            }

            unitOfWork.Flavors.Remove(flavorInDb);
            unitOfWork.Complete();

            return Ok();
        }
    }
}