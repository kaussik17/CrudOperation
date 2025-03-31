using CrudOpration.Entity;
using CrudOpration.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CrudOpration.Controller
{
    [Route("api/ef/[controller]")]
    [ApiController]
    public class TempEmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository; // ADO.NET
        private readonly ITempEmployeeRepository _tempEmployeeRepository; // Entity Framework

        public TempEmployeeController(IEmployeeRepository employeeRepository, ITempEmployeeRepository tempEmployeeRepository)
        {
            _employeeRepository = employeeRepository;
            _tempEmployeeRepository = tempEmployeeRepository;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _tempEmployeeRepository.GetAllEmployees(); // Using EF
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
                var employee = await _tempEmployeeRepository.GetEmployeeById(id); // Using EF
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

                var newEmployeeId = await _tempEmployeeRepository.InsertEmployee(employee); // Using EF
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

                var updated = await _tempEmployeeRepository.UpdateEmployee(employee); // Using EF
                if (!updated)
                    return NotFound($"Employee with Id = {id} not found");

                return NoContent();
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
                var deleted = await _tempEmployeeRepository.DeleteEmployee(id); // Using EF
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
