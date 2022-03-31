using AutoMapper;
using LinqCrudTest.DTOs;
using LinqCrudTest.Entities;

namespace LinqCrudTest.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            // companies mapper
            CreateMap<Company, CompanyDto>();
            CreateMap<Company, CreateCompanyDto>().ReverseMap();
            CreateMap<Company, CompanyDto.WithEmployees>();
            // employees mapper
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto.WithCompany>();
            CreateMap<Employee, EmployeeDto.WithCompanyId>();
            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
        }
    }
}
