namespace LinqCrudTest.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
