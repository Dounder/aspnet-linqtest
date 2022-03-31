using System.ComponentModel.DataAnnotations;

namespace LinqCrudTest.DTOs
{
    public class CreatePositionsDto
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string? Name { get; set; }
    }
}
