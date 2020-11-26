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
    public class EmployeesController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public EmployeesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetEmployees()
        {
            var employeeDtos = unitOfWork.Employees.GetAll()
                .Select(Mapper.Map<ApplicationUser, ApplicationUserDto>);

            return Ok(employeeDtos);
        }
    }
}
