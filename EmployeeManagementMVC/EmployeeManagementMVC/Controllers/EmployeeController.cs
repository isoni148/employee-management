using Microsoft.AspNetCore.Mvc;
using EmployeeManagementMVC.Models;
using EmployeeManagementMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            var employees = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e =>
                    e.EmployeeName.Contains(searchString) ||
                    e.Department.Contains(searchString));
            }

            return View(employees.ToList());
        }

        public IActionResult Dashboard()
        {
            DashboardViewModel dashboard = new DashboardViewModel();

            dashboard.TotalEmployees = _context.Employees.Count();

            dashboard.TotalITEmployees =
                _context.Employees.Count(e => e.Department == "IT");

            dashboard.TotalHREmployees =
                _context.Employees.Count(e => e.Department == "HR");

            dashboard.TotalFinanceEmployees =
                _context.Employees.Count(e => e.Department == "Finance");

            return View(dashboard);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Edit
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
    }
}