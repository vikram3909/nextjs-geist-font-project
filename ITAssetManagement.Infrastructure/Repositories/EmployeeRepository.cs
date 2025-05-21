using ITAssetManagement.Core.Entities;
using ITAssetManagement.Core.Interfaces;
using ITAssetManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ITAssetManagement.Infrastructure.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Employee> GetByEmployeeNumberAsync(string employeeNumber)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);
    }

    public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string department)
    {
        return await _dbSet.Where(e => e.Department == department).ToListAsync();
    }

    public async Task<IEnumerable<Asset>> GetAssignedAssetsAsync(int employeeId)
    {
        var employee = await _dbSet
            .Include(e => e.AssignedAssets)
            .FirstOrDefaultAsync(e => e.Id == employeeId);

        return employee?.AssignedAssets ?? new List<Asset>();
    }

    public async Task<bool> HasAssignedAssetsAsync(int employeeId)
    {
        return await _context.Assets.AnyAsync(a => a.AssignedToId == employeeId);
    }
}
