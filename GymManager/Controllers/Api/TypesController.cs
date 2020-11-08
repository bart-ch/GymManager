using AutoMapper;
using GymManager.Core;
using GymManager.Core.Domain;
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
        private readonly IUnitOfWork unitOfWork;

        public TypesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetTypes()
        {
            var typeDtos = unitOfWork.Types
                .GetAll()
                .Select(Mapper.Map<Core.Domain.Type,TypeDto>);

            return Ok(typeDtos);
        }
    }
}
