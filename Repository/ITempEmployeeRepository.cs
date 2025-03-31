using CrudOpration.Entity;

namespace CrudOpration.Repository
{
    public interface ITempEmployeeRepository
    {
        Task<IEnumerable<EmployeeEntity>> GetAllEmployees();
        Task<EmployeeEntity> GetEmployeeById(int id);
        Task<int> InsertEmployee(EmployeeEntity employee);
        Task<bool> UpdateEmployee(EmployeeEntity employee);
        Task<bool> DeleteEmployee(int id);
    }
}
