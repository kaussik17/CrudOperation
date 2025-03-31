using CrudOpration.Entity;
using CrudOpration.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudOpration.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, ITempEmployeeRepository tempEmployeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/employee/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeById(id);
                if (employee == null)
                    return NotFound($"Employee with Id = {id} not found");

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeEntity employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest("Employee data is null");

                var newEmployeeId = await _employeeRepository.InsertEmployee(employee);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployeeId }, employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeEntity employee)
        {
            try
            {
                if (employee == null || id != employee.Id)
                    return BadRequest("Invalid employee data");

                var updated = await _employeeRepository.UpdateEmployee(employee);
                if (!updated)
                    return NotFound($"Employee with Id = {id} not found");

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var deleted = await _employeeRepository.DeleteEmployee(id);
                if (!deleted)
                    return NotFound($"Employee with Id = {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
