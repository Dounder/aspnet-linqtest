using LinqCrudTest.Entities;

namespace LinqCrudTest.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public class WithCompany : EmployeeDto
        {
            public CompanyDto? Company { get; set; }

        }
        public class WithCompanyId : EmployeeDto
        {
            public int? CompanyId { get; set; }
        }
    }
}
