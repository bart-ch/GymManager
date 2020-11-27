using AutoMapper;
using GymManager.Core;
using GymManager.Core.Domain;
using GymManager.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GymManager.Controllers.Api
{
    [Authorize]
    public class SupplementTypesController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public SupplementTypesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetSupplementTypes()
        {
            var supplementTypeDtos = unitOfWork.SupplementTypes
                .GetAll()
                .Select(Mapper.Map<SupplementType,SupplementTypeDto>);

            return Ok(supplementTypeDtos);
        }
    }
}
