using CrudOpration.Entity;

namespace CrudOpration.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeEntity>> GetEmployees();
        Task<EmployeeEntity> GetEmployeeById(int id);
        Task<int> InsertEmployee(EmployeeEntity employee);
        Task<bool> UpdateEmployee(EmployeeEntity employee);
        Task<bool> DeleteEmployee(int id);
    }
}
