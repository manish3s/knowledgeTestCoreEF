using Dapper;
using KnowledgeTestCore.Models;
using Microsoft.Data.SqlClient;

namespace KnowledgeTestCore.Services
{
    public class EmployeeReadService : IEmployeeReadService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmployeeReadService> _logger;

        public EmployeeReadService(
            IConfiguration config,
            ILogger<EmployeeReadService> logger)
        {
            _config = config;
            _logger = logger;
        }

        // ✅ Get connection
        private SqlConnection GetConnection()
        {
            return new SqlConnection(
                _config.GetConnectionString("DefaultConnection"));
        }

        // ✅ 1. Get all employees FAST
        public async Task<IEnumerable<Employee>> GetAllFastAsync()
        {
            _logger.LogInformation("📋 Dapper: Getting all employees");

            using var connection = GetConnection();

            var sql = @"
                SELECT Id, FirstName, LastName, Email,
                       Department, Position, Salary,
                       JoinDate, IsActive
                FROM Employees
                WHERE IsActive = 1
                ORDER BY FirstName";

            return await connection.QueryAsync<Employee>(sql);
        }

        // ✅ 2. Search employees FAST
        public async Task<IEnumerable<Employee>> SearchFastAsync(
            string keyword)
        {
            _logger.LogInformation(
                "🔍 Dapper: Searching employees with {Keyword}", keyword);

            using var connection = GetConnection();

            // ✅ Always use parameters - never string concat!
            var sql = @"
                SELECT Id, FirstName, LastName, Email,
                       Department, Position, Salary,
                       JoinDate, IsActive
                FROM Employees
                WHERE IsActive = 1
                AND (
                    FirstName  LIKE @Keyword OR
                    LastName   LIKE @Keyword OR
                    Department LIKE @Keyword OR
                    Position   LIKE @Keyword OR
                    Email      LIKE @Keyword
                )
                ORDER BY FirstName";

            return await connection.QueryAsync<Employee>(sql,
                new { Keyword = $"%{keyword}%" });
        }

        // ✅ 3. Department Summary (complex query!)
        public async Task<IEnumerable<DepartmentSummary>>
            GetDepartmentSummaryAsync()
        {
            _logger.LogInformation("📊 Dapper: Getting department summary");

            using var connection = GetConnection();

            var sql = @"
                SELECT 
                    Department,
                    COUNT(*)        AS TotalEmployees,
                    AVG(Salary)     AS AverageSalary,
                    MAX(Salary)     AS HighestSalary,
                    MIN(Salary)     AS LowestSalary
                FROM Employees
                WHERE IsActive = 1
                GROUP BY Department
                ORDER BY AverageSalary DESC";

            return await connection
                .QueryAsync<DepartmentSummary>(sql);
        }

        // ✅ 4. High Salary Employees
        public async Task<IEnumerable<Employee>> GetHighSalaryAsync(
            decimal minSalary)
        {
            _logger.LogInformation(
                "💰 Dapper: Getting employees with salary > {MinSalary}",
                minSalary);

            using var connection = GetConnection();

            var sql = @"
                SELECT Id, FirstName, LastName, Email,
                       Department, Position, Salary,
                       JoinDate, IsActive
                FROM Employees
                WHERE IsActive = 1
                AND Salary >= @MinSalary
                ORDER BY Salary DESC";

            return await connection.QueryAsync<Employee>(sql,
                new { MinSalary = minSalary });
        }

        // ✅ 5. Employee Report (multiple aggregations)
        public async Task<EmployeeReport> GetReportAsync()
        {
            _logger.LogInformation("📈 Dapper: Generating employee report");

            using var connection = GetConnection();

            var sql = @"
                SELECT
                    COUNT(*)                    AS TotalEmployees,
                    SUM(CASE WHEN IsActive = 1 
                        THEN 1 ELSE 0 END)      AS ActiveEmployees,
                    AVG(Salary)                 AS AverageSalary,
                    SUM(Salary)                 AS TotalSalaryBudget,
                    (
                        SELECT TOP 1 Department
                        FROM Employees
                        WHERE IsActive = 1
                        GROUP BY Department
                        ORDER BY AVG(Salary) DESC
                    )                           AS HighestPaidDepartment
                FROM Employees";

            return await connection
                .QueryFirstOrDefaultAsync<EmployeeReport>(sql)
                ?? new EmployeeReport();
        }
    }
}