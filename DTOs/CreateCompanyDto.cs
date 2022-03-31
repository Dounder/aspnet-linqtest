using LinqCrudTest.Entities;
using System.ComponentModel.DataAnnotations;

namespace LinqCrudTest.DTOs
{
    public class CreateCompanyDto
    {
        [Required]
        [StringLength(maximumLength: 120, MinimumLength = 5)]
        public string? Name { get; set; }
        public List<int>? EmployeesIds { get; set; }
    }
}
