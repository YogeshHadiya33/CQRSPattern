using System.Linq.Expressions;
using CQRSPattern.Entity.Employee.Database;

namespace CQRSPattern.Services.Features.Employees.Repositories;

public interface IEmployeeRepository
{
    Task<Employee> Create(Employee employee);
    Task<bool> Delete(int id);
    Task<IEnumerable<Employee>> GetAll();
    Task<Employee?> Get(int id);
    Task<Employee?> Get(Expression<Func<Employee, bool>> predicate);
    Task<Employee> Update(Employee employee);
}