using AutoMapper;
using GymManager.Dtos;
using GymManager.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.Design;
using GymManager.Attributes;
using GymManager.Core.Domain;
using GymManager.Core;

namespace GymManager.Controllers.Api
{
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
                .GetSingleOrDefaultEquipmentWithAreaAndType(id);

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
                BadRequest();
            }

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
                BadRequest();
            }
            var equipmentInDb = unitOfWork.Equipment
                .GetSingleOrDefaultEquipmentWithAreaAndType(id);

            if (equipmentInDb == null)
            {
                return BadRequest();
            }

            Mapper.Map(equipmentDto, equipmentInDb);

            unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public  IHttpActionResult DeleteEquipment(int id)
        {
            var equipmentInDb = unitOfWork.Equipment
                .GetSingleOrDefaultEquipmentWithAreaAndType(id);

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
