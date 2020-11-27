using AutoMapper;
using GymManager.Attributes;
using GymManager.Core;
using GymManager.Core.Domain;
using GymManager.Dtos;
using System.Linq;
using System.Web.Http;

namespace GymManager.Controllers.Api
{
    [Authorize(Roles = RoleName.CanManageEmployees)]
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

        [HttpPut]
        [ValidateAntiForgeryToken]
        public IHttpActionResult UpdateEmployee(string id, ApplicationUserDto applicationUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var employeeInDb = unitOfWork.Employees.SingleOrDefault(e => e.Id == id);
            if (employeeInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(applicationUserDto, employeeInDb);

            unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteEmployee(string id)
        {
            var employeeInDb = unitOfWork.Employees.SingleOrDefault(e => e.Id == id);

            if (employeeInDb == null)
            {
                return NotFound();
            }

            unitOfWork.Employees.Remove(employeeInDb);
            unitOfWork.Complete();

            return Ok();
        }
    }
}
