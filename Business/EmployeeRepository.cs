using CrudOpration.Entity;
using CrudOpration.Repository;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudOpration.Business
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<EmployeeEntity>> GetEmployees()
        {
            var employees = new List<EmployeeEntity>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Employee_Select", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await conn.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    employees.Add(new EmployeeEntity
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Age = (int)reader["Age"],
                        Department = reader["Department"].ToString(),
                        Salary = (decimal)reader["Salary"]
                    });
                }
            }
            return employees;
        }

        public async Task<EmployeeEntity> GetEmployeeById(int id)
        {
            EmployeeEntity employee = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Employee_SelectById", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);

                await conn.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    employee = new EmployeeEntity
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Age = (int)reader["Age"],
                        Department = reader["Department"].ToString(),
                        Salary = (decimal)reader["Salary"]
                    };
                }
            }
            return employee;
        }

        public async Task<int> InsertEmployee(EmployeeEntity employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Employee_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                await conn.OpenAsync();
                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
        }

        public async Task<bool> UpdateEmployee(EmployeeEntity employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Employee_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", employee.Id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Employee_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);

                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }
    }
}
