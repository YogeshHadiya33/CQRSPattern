using System.Linq.Expressions;
using CQRSPattern.Entity.Employee.Database;
using CQRSPattern.Repository.Repository;

namespace CQRSPattern.Services.Features.Employees.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IGenericRepository<Employee> _employeeRepository;

    public EmployeeRepository(IGenericRepository<Employee> employeeRepository)
        => _employeeRepository = employeeRepository;

    public async Task<Employee?> Get(int id)
        => await _employeeRepository.GetAsync(id);

    public async Task<Employee?> Get(Expression<Func<Employee, bool>> predicate)
        => await _employeeRepository.GetAsync(predicate);

    public async Task<IEnumerable<Employee>> GetAll()
        => await _employeeRepository.GetAllAsync();

    public async Task<Employee> Create(Employee employee)
        => await _employeeRepository.AddAsync(employee);

    public async Task<Employee> Update(Employee employee)
        => await _employeeRepository.UpdateAsync(employee);

    public async Task<bool> Delete(int id)
        => await _employeeRepository.DeleteAsync(id);
}