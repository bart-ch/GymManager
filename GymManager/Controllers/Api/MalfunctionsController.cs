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

        public IHttpActionResult GetMalfunction(int id)
        {
            var malfunction = unitOfWork.Malfunctions.SingleOrDefault(m => m.Id == id);
            if (malfunction == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Malfunction, MalfunctionDto>(malfunction));
        }

        [Route("api/equipment/{equipmentId}/malfunctions")]
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

            if (!malfunctionDto.IsRepaired)
            {
                var equipmentInDb = unitOfWork.Equipment.Get(malfunctionDto.EquipmentId);
                equipmentInDb.IsOperational = false;
            }

            var malfunction = Mapper.Map<MalfunctionDto, Malfunction>(malfunctionDto);

            unitOfWork.Malfunctions.Add(malfunction);
            unitOfWork.Complete();

            malfunctionDto.Id = malfunction.Id;

            return Created(new Uri(Request.RequestUri + "/" + malfunction.Id), malfunctionDto);
        }
    }
}