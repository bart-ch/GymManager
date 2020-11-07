using AutoMapper;
using GymManager.Core.Domain.Equipment;
using GymManager.Dtos;
using GymManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GymManager.Controllers.Api
{
    public class AreasController : ApiController
    {
        private readonly ApplicationDbContext context;

        public AreasController()
        {
            context = new ApplicationDbContext();
        }

        public IHttpActionResult GetAreas()
        {
            var areaDtos = context.Areas.ToList().Select(Mapper.Map<Area, AreaDto>);

            return Ok(areaDtos);
        }
    }
}
