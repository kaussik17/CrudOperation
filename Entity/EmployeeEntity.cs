using System.ComponentModel.DataAnnotations.Schema;

namespace CrudOpration.Entity
{
    [Table("Employee")]
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }
}
