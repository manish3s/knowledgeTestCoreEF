using KnowledgeTestCore.Data;
using KnowledgeTestCore.DTOs;
using KnowledgeTestCore.Models;
using Microsoft.EntityFrameworkCore;
using static KnowledgeTestCore.DTOs.EmployeeDto;

namespace KnowledgeTestCore.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _db;

        public EmployeeService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _db.Employees
                .Where(e => e.IsActive)
                .OrderBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _db.Employees
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Employee>> SearchAsync(string keyword)
        {
            return await _db.Employees
                .Where(e => e.FirstName.Contains(keyword) ||
                            e.LastName.Contains(keyword) ||
                            e.Department.Contains(keyword) ||
                            e.Position.Contains(keyword))
                .ToListAsync();
        }

        public async Task<List<Employee>> GetByDepartmentAsync(string department)
        {
            return await _db.Employees
                .Where(e => e.Department == department && e.IsActive)
                .OrderBy(e => e.Salary)
                .ToListAsync();
        }

        public async Task<Employee> CreateAsync(CreateEmployeeRequest request)
        {
            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Department = request.Department,
                Position = request.Position,
                Salary = request.Salary,
                JoinDate = request.JoinDate,
                IsActive = true
            };

            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateAsync(int id, UpdateEmployeeRequest request)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee is null) return null;

            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Email = request.Email;
            employee.Department = request.Department;
            employee.Position = request.Position;
            employee.Salary = request.Salary;

            await _db.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> PatchAsync(int id, PatchEmployeeRequest request)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee is null) return null;

            if (request.FirstName is not null) employee.FirstName = request.FirstName;
            if (request.LastName is not null) employee.LastName = request.LastName;
            if (request.Email is not null) employee.Email = request.Email;
            if (request.Department is not null) employee.Department = request.Department;
            if (request.Position is not null) employee.Position = request.Position;
            if (request.Salary is not null) employee.Salary = request.Salary.Value;
            if (request.IsActive is not null) employee.IsActive = request.IsActive.Value;

            await _db.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee is null) return false;

            employee.IsActive = false;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}