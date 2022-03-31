using LinqCrudTest.Entities;

namespace LinqCrudTest.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public class WithCompanyAndPosition : EmployeeDto
        {
            public CompanyDto? Company { get; set; }
            public PositionDto? Position { get; set; }

        }
        public class WithCompanyIdAndPositionId : EmployeeDto
        {
            public int? CompanyId { get; set; }
            public int? PositionId { get; set; }
        }
    }
}
