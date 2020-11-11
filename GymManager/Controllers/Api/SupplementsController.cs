using GymManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GymManager.Controllers.Api
{
    public class SupplementsController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public SupplementsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetSupplements()
        {
            var supplementsDto = unitOfWork.Supplements
                .GetSupplementsWithFlavorsAndTypes();

            return Ok(supplementsDto);
        }
    }
}
