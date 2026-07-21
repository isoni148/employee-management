using Microsoft.AspNetCore.Mvc;
using EmployeeManagementMVC.Models;
using EmployeeManagementMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var employees = _context.Employees
                        .Include(e => e.Department)
                        .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                employees = employees.Where(e =>
     e.EmployeeName.Contains(searchString.Trim()) ||
     (e.Department != null &&
      e.Department.DepartmentName.Contains(searchString)));
            }

            return View(employees
    .OrderBy(e => e.EmployeeName)
    .ToList());
        }

        //public IActionResult Dashboard()
        //{
        //    DashboardViewModel dashboard = new DashboardViewModel();

        //    dashboard.TotalEmployees = _context.Employees.Count();

        //    dashboard.TotalITEmployees = _context.Employees
        //        .Include(e => e.Department)
        //        .Count(e => e.Department.DepartmentName == "IT");

        //    dashboard.TotalHREmployees = _context.Employees
        //        .Include(e => e.Department)
        //        .Count(e => e.Department.DepartmentName == "HR");

        //    dashboard.TotalFinanceEmployees = _context.Employees
        //        .Include(e => e.Department)
        //        .Count(e => e.Department.DepartmentName == "Finance");

        //    return View(dashboard);
        //}


        public IActionResult Dashboard()
        {
            DashboardViewModel dashboard = new DashboardViewModel();

            dashboard.TotalEmployees = _context.Employees.Count();
            dashboard.TotalITEmployees = _context.Employees
                .Count(e => e.DepartmentId == 1);

            dashboard.TotalHREmployees = _context.Employees
                .Count(e => e.DepartmentId == 2);

            dashboard.TotalFinanceEmployees = _context.Employees
                .Count(e => e.DepartmentId == 3); ;

            return View(dashboard);
        }



        //public IActionResult Dashboard()
        //{
        //    DashboardViewModel dashboard = new DashboardViewModel();

        //    dashboard.TotalEmployees = 0;
        //    dashboard.TotalITEmployees = 0;
        //    dashboard.TotalHREmployees = 0;
        //    dashboard.TotalFinanceEmployees = 0;

        //    return View(dashboard);
        //}
        // GET: Employee/Create
        public IActionResult Create()
        {
            ViewBag.Department = new SelectList(
                _context.Department,
                "DepartmentId",
                "DepartmentName");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Department = new SelectList(
                    _context.Department,
                    "DepartmentId",
                    "DepartmentName");

                return View(employee);
            }

            _context.Employees.Add(employee);
            _context.SaveChanges();

            TempData["Success"] = "Employee added successfully.";

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Department = new SelectList(
                _context.Department,
                "DepartmentId",
                "DepartmentName",
                employee.DepartmentId);

            return View(employee);
        }

        // POST: Employee/Edit
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Department = new SelectList(
                    _context.Department,
                    "DepartmentId",
                    "DepartmentName",
                    employee.DepartmentId);

                return View(employee);
            }

            var existingEmployee = _context.Employees.Find(employee.EmployeeId);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.EmployeeName = employee.EmployeeName;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.DepartmentId = employee.DepartmentId;

            _context.SaveChanges();

            TempData["Success"] = "Employee updated successfully.";

            return RedirectToAction(nameof(Index));
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
            var existingEmployee = _context.Employees.Find(employee.EmployeeId);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(existingEmployee);
            _context.SaveChanges();

            TempData["Success"] = "Employee deleted successfully.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var employee = _context.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
    }
}