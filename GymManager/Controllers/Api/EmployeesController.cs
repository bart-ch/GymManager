using GymManager.Core;
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
            var employyes = unitOfWork.Employees.GetAll();
            var employeeDtos = new List<ApplicationUserDto>();

            foreach (var employee in employyes)
            {
                employeeDtos.Add(new ApplicationUserDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Surname = employee.Surname,
                    JobTitle = employee.JobTitle,
                    Email = employee.Email
                }) ;
            }

            return Ok(employeeDtos);
        }
    }
}
