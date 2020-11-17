using AutoMapper;
using GymManager.Attributes;
using GymManager.Core;
using GymManager.Core.Domain;
using GymManager.Dtos;
using System;
using System.Linq;
using System.Web.Http;

namespace GymManager.Controllers.Api
{
    public class MalfunctionsController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public MalfunctionsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("api/malfunctions/{equipmentId}")]
        public IHttpActionResult GetMalfunctionsOfGivenEquipment(int equipmentId)
        {
            var malfunctionDtos = unitOfWork.Malfunctions
                .Find(m => m.Equipment.Id == equipmentId)
                .Select(Mapper.Map<Malfunction, MalfunctionDto>);

            return Ok(malfunctionDtos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IHttpActionResult CreateMalfunction(MalfunctionDto malfunctionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var malfunction = Mapper.Map<MalfunctionDto, Malfunction>(malfunctionDto);

            unitOfWork.Malfunctions.Add(malfunction);
            unitOfWork.Complete();

            malfunctionDto.Id = malfunction.Id;

            return Created(new Uri(Request.RequestUri + "/" + malfunction.Id), malfunctionDto);
        }
    }
}