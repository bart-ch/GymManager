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

namespace GymManager.Controllers.Api
{
    public class EquipmentController : ApiController
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IHttpActionResult GetEquipment()
        {
            var equipmentDtos = context.Equipment
                .Include(e => e.Type)
                .Include(e => e.Area)
                .ToList()
                .Select(Mapper.Map<Equipment, EquipmentDto>);

            return Ok(equipmentDtos);
        }        
        
        public IHttpActionResult GetSingleEquipment(int id)
        {
            var equipment = context.Equipment
                .Include(e => e.Type)
                .Include(e => e.Area)
                .SingleOrDefault(e => e.Id == id);

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

            context.Equipment.Add(equipment);
            context.SaveChanges();

            equipmentDto.Id = equipment.Id;

            return Created(new Uri(Request.RequestUri + "/" + equipment.Id), equipmentDto);
        }

  
    }
}
