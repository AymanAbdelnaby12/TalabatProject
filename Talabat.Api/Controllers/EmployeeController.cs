using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Interfaces_Or_Repository;
using Talabat.Core.Models;
using Talabat.Core.Specifications;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
       private readonly IGenericRepository<Employee> _employeeRepo;

        public EmployeeController(IGenericRepository<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployee()
        {
            var spec =new EmployeeWithDepartmentSpec();
            var Employee = await _employeeRepo.GetAllWithSpecAsync(spec);
            return Ok(Employee);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var spec = new EmployeeWithDepartmentSpec(id);
            var Employee = await _employeeRepo.GetByIdWithSpcAsync(spec);
            return Ok(Employee);
        }
    }
}
