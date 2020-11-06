using AutoMapper;
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
    public class TypesController : ApiController
    {
        private readonly ApplicationDbContext context;

        public TypesController()
        {
            context = new ApplicationDbContext();
        }

        public IHttpActionResult GetTypes()
        {
            var typeDtos = context.Types.ToList().Select(Mapper.Map<Models.Type,TypeDto>);

            return Ok(typeDtos);
        }


    }
}
