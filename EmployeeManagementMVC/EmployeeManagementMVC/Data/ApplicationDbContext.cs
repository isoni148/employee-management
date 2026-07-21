using Microsoft.EntityFrameworkCore;
using EmployeeManagementMVC.Models;

namespace EmployeeManagementMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Department { get; set; }
    }
}