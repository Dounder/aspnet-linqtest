using AutoMapper;
using LinqCrudTest.DTOs;
using LinqCrudTest.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinqCrudTest
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public CompaniesController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyDto>>> Get()
        {
            var companies = await context.Companies.ToListAsync();
            return mapper.Map<List<CompanyDto>>(companies);
        }

        [HttpGet("{id:int}", Name = "getCompanyById")]
        public async Task<ActionResult<CompanyDto.WithEmployees>> Get(int id)
        {
            var company = await context.Companies
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(company == null) return NotFound();

            return Ok(mapper.Map<CompanyDto.WithEmployees>(company));
        }
            

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCompanyDto createCompany)
        {
            var company = mapper.Map<Company>(createCompany);
            context.Add(company);
            //await context.SaveChangesAsync();
            var companyDto = mapper.Map<CompanyDto>(company);

            return new CreatedAtRouteResult("getCompanyById", new { id = company.Id }, companyDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateCompanyDto updateCompany)
        {
            var company = await context.Companies
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (company == null) return NotFound();

            company = mapper.Map(updateCompany, company);

            await context.SaveChangesAsync();
            var companyDto = mapper.Map<CompanyDto>(company);
            return new CreatedAtRouteResult("getCompanyById", new { id = company.Id }, companyDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var company = await context.Companies.AnyAsync(x => x.Id == id);

            if (!company) return NotFound();

            context.Remove(new Company() { Id = id});
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
