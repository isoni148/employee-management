using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementMVC.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee Name is required")]
        public string EmployeeName { get; set; }

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(1, 1000000, ErrorMessage = "Salary must be greater than 0")]
        public decimal Salary { get; set; }
    }
}