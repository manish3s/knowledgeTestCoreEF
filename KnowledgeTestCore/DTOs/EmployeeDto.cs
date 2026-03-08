namespace KnowledgeTestCore.DTOs
{
    public class EmployeeDto
    {

        //good to go
        //good to go
        //good to go
          string MyProperty ="HR";


        public record CreateEmployeeRequest(
           string FirstName,
           string LastName,
           string Email,
           string Department,
           string Position,
           decimal Salary,
           DateTime JoinDate
       );

        public record UpdateEmployeeRequest(
            string FirstName,
            string LastName,
            string Email,
            string Department,
            string Position,
            decimal Salary
        );

        public record PatchEmployeeRequest(
            string? FirstName,
            string? LastName,
            string? Email,
            string? Department,
            string? Position,
            decimal? Salary,
            bool? IsActive
        );
    }
}
