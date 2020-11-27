using AutoMapper;
using GymManager.Core;
using GymManager.Core.Domain;
using GymManager.Dtos;
using System.Linq;
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

        public IHttpActionResult GetEmployee(string id)
        {
            var employee = unitOfWork.Employees.SingleOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }


            return Ok(Mapper.Map<ApplicationUser, ApplicationUserDto>(employee));
        }
    }
}
