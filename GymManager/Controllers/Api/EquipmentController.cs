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
    [Authorize]
    public class EquipmentController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public EquipmentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetEquipment()
        {
            var equipmentDtos = unitOfWork.Equipment
                .GetEquipmentWithAreasAndTypes()
                .Select(Mapper.Map<Equipment, EquipmentDto>);

            return Ok(equipmentDtos);
        }        
        
        public IHttpActionResult GetSingleEquipment(int id)
        {
            var equipment = unitOfWork.Equipment
                .GetSingleOrDefaultEquipmentWithAreaAndType(e => e.Id == id);

            if (equipment == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Equipment,EquipmentDto>(equipment));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IHttpActionResult CreateEquipment(EquipmentDto equipmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            equipmentDto.IsOperational = true;
            var equipment = Mapper.Map<EquipmentDto, Equipment>(equipmentDto);

            unitOfWork.Equipment.Add(equipment);
            unitOfWork.Complete();

            equipmentDto.Id = equipment.Id;

            return Created(new Uri(Request.RequestUri + "/" + equipment.Id), equipmentDto);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public IHttpActionResult UpdateEquipment(int id, EquipmentDto equipmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var equipmentInDb = unitOfWork.Equipment.SingleOrDefault(e => e.Id == id);
            if (equipmentInDb == null)
            {
                return NotFound();
            }

            equipmentDto.IsOperational = true;

            Mapper.Map(equipmentDto, equipmentInDb);

            unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public  IHttpActionResult DeleteEquipment(int id)
        {
            var equipmentInDb = unitOfWork.Equipment.SingleOrDefault(e => e.Id == id);

            if (equipmentInDb == null)
            {
                return NotFound();
            }

            unitOfWork.Equipment.Remove(equipmentInDb);
            unitOfWork.Complete();

            return Ok();
        }
    }
}
