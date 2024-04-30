using CQRSPattern.Entity.Employee.Database;

namespace CQRSPattern.Services.Features.Employees.Builder;

public interface IEmployeeBuilder
{
    IEmployeeBuilder AddExistingEmployee(Employee employee);
    IEmployeeBuilder AddAddress(string address);
    IEmployeeBuilder AddLastName(string lastName);
    IEmployeeBuilder AddDepartment(string department);
    IEmployeeBuilder AddDesignation(string designation);
    IEmployeeBuilder AddEmail(string email);
    IEmployeeBuilder AddFirstName(string name);
    IEmployeeBuilder AddPhoneNumber(string phoneNumber);
    IEmployeeBuilder AddCreatedDetails(string userId);
    IEmployeeBuilder AddUpdatedDetails(string userId);
    Employee Build();
}