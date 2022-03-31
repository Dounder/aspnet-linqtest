using System.ComponentModel.DataAnnotations;

namespace LinqCrudTest.Entities
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 120, MinimumLength = 5)]
        public string? Name { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
