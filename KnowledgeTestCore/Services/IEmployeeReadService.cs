using KnowledgeTestCore.Models;

namespace KnowledgeTestCore.Services
{
    public interface IEmployeeReadService
    {
        // Simple read
        Task<IEnumerable<Employee>> GetAllFastAsync();

        // Search with Dapper
        Task<IEnumerable<Employee>> SearchFastAsync(string keyword);

        // Department summary (complex query)
        Task<IEnumerable<DepartmentSummary>> GetDepartmentSummaryAsync();

        // High salary employees
        Task<IEnumerable<Employee>> GetHighSalaryAsync(decimal minSalary);

        // Employee report
        Task<EmployeeReport> GetReportAsync();
    }

    // Extra models for Dapper results
    public class DepartmentSummary
    {
        public string Department { get; set; } = string.Empty;
        public int TotalEmployees { get; set; }
        public decimal AverageSalary { get; set; }
        public decimal HighestSalary { get; set; }
        public decimal LowestSalary { get; set; }
    }

    public class EmployeeReport
    {
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public decimal AverageSalary { get; set; }
        public decimal TotalSalaryBudget { get; set; }
        public string HighestPaidDepartment { get; set; } = string.Empty;
    }
}