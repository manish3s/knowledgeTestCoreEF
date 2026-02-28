using KnowledgeTestCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static KnowledgeTestCore.DTOs.EmployeeDto;

namespace KnowledgeTestCore.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            return employee is not null
                ? Ok(employee)
                : NotFound(new { message = "Employee not found" });
        }

        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> Search(string keyword)
        {
            var employees = await _employeeService.SearchAsync(keyword);
            return Ok(employees);
        }

        [HttpGet("department/{department}")]
        public async Task<IActionResult> GetByDepartment(string department)
        {
            var employees = await _employeeService.GetByDepartmentAsync(department);
            return Ok(employees);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateEmployeeRequest request)
        {
            var employee = await _employeeService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById),
                new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateEmployeeRequest request)
        {
            var employee = await _employeeService.UpdateAsync(id, request);
            return employee is not null
                ? Ok(employee)
                : NotFound(new { message = "Employee not found" });
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Patch(int id, PatchEmployeeRequest request)
        {
            var employee = await _employeeService.PatchAsync(id, request);
            return employee is not null
                ? Ok(employee)
                : NotFound(new { message = "Employee not found" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteAsync(id);
            return result
                ? Ok(new { message = "Employee deleted" })
                : NotFound(new { message = "Employee not found" });
        }
    }
}
