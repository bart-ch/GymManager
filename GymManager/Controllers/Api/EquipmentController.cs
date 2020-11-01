using AutoMapper;
using GymManager.Dtos;
using GymManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GymManager.Controllers.Api
{
    public class EquipmentController : ApiController
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IHttpActionResult GetEquipment()
        {
            var equipmentDtos = context.Equipment.ToList().Select(Mapper.Map<Equipment, EquipmentDto>);

            return Ok(equipmentDtos);
        }        
        
        public IHttpActionResult GetSingleEquipment(int id)
        {
            var equipment = context.Equipment.SingleOrDefault(e => e.Id == id);

            if (equipment == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Equipment,EquipmentDto>(equipment));
        }
    }
}
