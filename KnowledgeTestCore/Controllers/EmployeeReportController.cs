using KnowledgeTestCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeTestCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeReportController : ControllerBase
    {
        private readonly IEmployeeReadService _readService;
        private readonly ILogger<EmployeeReportController> _logger;

        public EmployeeReportController(
            IEmployeeReadService readService,
            ILogger<EmployeeReportController> logger)
        {
            _readService = readService;
            _logger = logger;
        }

        // GET api/employeereport/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _readService.GetAllFastAsync();
            return Ok(employees);
        }

        // GET api/employeereport/search/john ok
        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> Search(string keyword)
        {
            if (keyword is null or { Length: < 2 })
                return BadRequest(new
                {
                    message = "Keyword must be at least 2 characters"
                });

            var employees = await _readService.SearchFastAsync(keyword);
            return Ok(employees);
        }

        // GET api/employeereport/departments
        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartmentSummary()
        {
            var summary = await _readService.GetDepartmentSummaryAsync();
            return Ok(summary);
        }

        // GET api/employeereport/highsalary/50000
        [HttpGet("highsalary/{minSalary}")]
        public async Task<IActionResult> GetHighSalary(decimal minSalary)
        {
            if (minSalary <= 0)
                return BadRequest(new
                {
                    message = "Minimum salary must be greater than 0"
                });

            var employees = await _readService.GetHighSalaryAsync(minSalary);
            return Ok(employees);
        }

        // GET api/employeereport/report
        [HttpGet("report")]
        public async Task<IActionResult> GetReport()
        {
            var report = await _readService.GetReportAsync();
            return Ok(report);
        }
    }
}