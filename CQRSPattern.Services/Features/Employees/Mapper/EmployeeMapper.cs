using AutoMapper;
using CQRSPattern.Entity.Employee.Bussiness;
using CQRSPattern.Entity.Employee.Database;

namespace CQRSPattern.Services.Features.Employees.Mapper;

public class EmployeeMapper : Profile
{
    public EmployeeMapper()
    {
        CreateMap<Employee, EmployeeModel>()
            .ForMember(x => x.CreatedOn, x => x.MapFrom(x => x.CreatedOn.ToString("dd-MMM-yyyy hh:mm tt ")))
            .ForMember(x => x.UpdatedOn, x => x.MapFrom(x => x.UpdatedOn.HasValue ? x.UpdatedOn.Value.ToString("dd-MMM-yyyy hh:mm tt ") : null));
    }
}