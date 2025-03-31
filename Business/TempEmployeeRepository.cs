using CrudOpration.Data;
using CrudOpration.Entity;
using CrudOpration.Repository;
using Microsoft.EntityFrameworkCore;

namespace CrudOpration.Business
{
    public class TempEmployeeRepository : ITempEmployeeRepository
    {
        private readonly AppDbContext _context;

        public TempEmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<EmployeeEntity> GetEmployeeById(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<int> InsertEmployee(EmployeeEntity employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.Id;
        }

        public async Task<bool> UpdateEmployee(EmployeeEntity employee)
        {
            _context.Employees.Update(employee);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            return await _context.SaveChangesAsync() > 0;
        }
    }

}
