using ITAssetManagement.Core.Entities;

namespace ITAssetManagement.Core.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee> GetByIdAsync(int id);
    Task<Employee> GetByEmployeeNumberAsync(string employeeNumber);
    Task<Employee> AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(int id);
    Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string department);
    Task<IEnumerable<Asset>> GetAssignedAssetsAsync(int employeeId);
    Task<bool> HasAssignedAssetsAsync(int employeeId);
}
