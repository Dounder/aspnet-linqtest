using LinqCrudTest.Entities;

namespace LinqCrudTest.DTOs
{
    public class CreateEmployeeDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int? CompanyId { get; set; }
    }
}
