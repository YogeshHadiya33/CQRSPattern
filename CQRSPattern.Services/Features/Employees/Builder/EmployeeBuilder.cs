using CQRSPattern.Common.Builder.Collection;
using CQRSPattern.Entity.Employee.Database;

namespace CQRSPattern.Services.Features.Employees.Builder;

public class EmployeeBuilder : CollectionBuilder<Employee>, IEmployeeBuilder
{
    public EmployeeBuilder()
    {
        Reset();
    }

    public IEmployeeBuilder AddExistingEmployee(Employee employee)
    {
        Collection = employee;
        return this;
    }

    public IEmployeeBuilder AddFirstName(string firstName)
    {
        Collection.FirstName = firstName;
        return this;
    }

    public IEmployeeBuilder AddLastName(string lastName)
    {
        Collection.LastName = lastName;
        return this;
    }

    public IEmployeeBuilder AddAddress(string address)
    {
        Collection.Address = address;
        return this;
    }

    public IEmployeeBuilder AddEmail(string email)
    {
        Collection.Email = email;
        return this;
    }

    public IEmployeeBuilder AddPhoneNumber(string phoneNumber)
    {
        Collection.Phone = phoneNumber;
        return this;
    }

    public IEmployeeBuilder AddDepartment(string department)
    {
        Collection.Department = department;
        return this;
    }

    public IEmployeeBuilder AddDesignation(string designation)
    {
        Collection.Designation = designation;
        return this;
    }

    public IEmployeeBuilder AddCreatedDetails(string userId)
    {
        Collection.CreatedBy = userId;
        Collection.CreatedOn = DateTime.Now;
        return this;
    }

    public IEmployeeBuilder AddUpdatedDetails(string userId)
    {
        Collection.UpdatedBy = userId;
        Collection.UpdatedOn = DateTime.Now;
        return this;
    }

    protected override void Reset()
    {
        Collection = new Employee
        {
        };
    }
}