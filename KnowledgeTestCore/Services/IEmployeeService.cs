using KnowledgeTestCore.Models;
using static KnowledgeTestCore.DTOs.AuthDto;
using static KnowledgeTestCore.DTOs.EmployeeDto;

namespace KnowledgeTestCore.Services
{
    public interface IEmployeeService
    {
       
            Task<List<Employee>> GetAllAsync();
            Task<Employee?> GetByIdAsync(int id);
            Task<List<Employee>> SearchAsync(string keyword);
            Task<List<Employee>> GetByDepartmentAsync(string department);
            Task<Employee> CreateAsync(CreateEmployeeRequest request);
            Task<Employee?> UpdateAsync(int id, UpdateEmployeeRequest request);
            Task<Employee?> PatchAsync(int id, PatchEmployeeRequest request);
            Task<bool> DeleteAsync(int id);
        
    }
}
