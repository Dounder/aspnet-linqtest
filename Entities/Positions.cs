using System.ComponentModel.DataAnnotations;

namespace LinqCrudTest.Entities
{
    public class Positions
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:120, MinimumLength =2)]
        public string Name { get; set; }
    }
}
