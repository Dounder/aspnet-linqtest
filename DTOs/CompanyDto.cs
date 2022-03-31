using LinqCrudTest.Entities;
using System.ComponentModel.DataAnnotations;

namespace LinqCrudTest.DTOs
{
    public class CompanyDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 120, MinimumLength = 5)]
        public string? Name { get; set; }

        public class WithEmployees : CompanyDto
        {
            public List<EmployeeDto>? Employees { get; set; }
        }
    }
}
