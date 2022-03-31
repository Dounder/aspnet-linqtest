using AutoMapper;
using LinqCrudTest.DTOs;
using LinqCrudTest.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinqCrudTest.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public EmployeeController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto.WithCompanyIdAndPositionId>>> Get()
        {
            var employees = await context.Employees
                .Include(x => x.Company)
                .Include(x => x.Position)
                .ToListAsync();
            return mapper.Map<List<EmployeeDto.WithCompanyIdAndPositionId>>(employees);
        }

        [HttpGet("{id:int}", Name = "getEmployeeById")]
        public async Task<ActionResult<EmployeeDto.WithCompanyAndPosition>> Get(int id)
        {
            var employee = await context.Employees
                .Include(x => x.Company)
                .Include(x => x.Position)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null) return NotFound();

            return mapper.Map<EmployeeDto.WithCompanyAndPosition>(employee);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateEmployeeDto createEmployee)
        {
            var companyExist = await context.Companies.AnyAsync(x => x.Id == createEmployee.CompanyId);
            if (!companyExist) return BadRequest("Company ID dont exits");

            var positionExist = await context.Positions.AnyAsync(x => x.Id == createEmployee.PositionId);
            if (!positionExist) return BadRequest("Position ID dont exits");

            var employee = mapper.Map<Employee>(createEmployee);

            context.Add(employee);
            await context.SaveChangesAsync();

            var employeeDb = await context.Employees
                .Include(x => x.Company)
                .Include(x => x.Position)
                .FirstOrDefaultAsync(x => x.Id == employee.Id);

            var employeeDto = mapper.Map<EmployeeDto.WithCompanyAndPosition>(employeeDb);
            return new CreatedAtRouteResult("getEmployeeById", new { id = employee.Id }, employeeDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateEmployeeDto createEmployee)
        {
            var companyExist = await context.Companies.AnyAsync(x => x.Id == createEmployee.CompanyId);
            if (!companyExist) return BadRequest("Company ID dont exits");

            var positionExist = await context.Positions.AnyAsync(x => x.Id == createEmployee.PositionId);
            if (!positionExist) return BadRequest("Position ID dont exits");

            var employee = await context.Employees
                .Include(x => x.Company)
                .Include(x => x.Position)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null) return NotFound();

            employee = mapper.Map(createEmployee, employee);
            await context.SaveChangesAsync();
            var employeeDto = mapper.Map<EmployeeDto.WithCompanyAndPosition>(employee);
            return new CreatedAtRouteResult("getEmployeeById", new { id = employee.Id }, employeeDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var employee = await context.Employees.AnyAsync(x => x.Id == id);

            if (!employee) return NotFound();

            context.Remove(new Employee() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
