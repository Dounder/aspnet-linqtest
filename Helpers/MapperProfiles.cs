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
            CreateMap<Employee, EmployeeDto.WithCompanyAndPosition>();
            CreateMap<Employee, EmployeeDto.WithCompanyIdAndPositionId>();
            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();

            // positions mapper
            CreateMap<Positions, PositionDto>().ReverseMap();
            CreateMap<Positions, CreatePositionsDto>().ReverseMap();
        }
    }
}
