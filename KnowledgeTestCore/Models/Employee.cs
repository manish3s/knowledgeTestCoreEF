using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeTestCore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]  // ← add this!
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
