using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Turing_Assignment_1.Models;

namespace Turing_Assignment_1.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public IActionResult Index()
        {
            NorthwindDbContext context = new NorthwindDbContext();
            List<Employee> employees = context.Employees.ToList();
            return View(employees);
        }

        // GET: Employees/Orders/{id}
        public IActionResult Orders(int id)
        {
            NorthwindDbContext context = new NorthwindDbContext();
            Employee employee = context.Employees.FirstOrDefault(e => e.EmployeeId == id);

            if (employee != null)
            {
                ViewBag.EmployeeName =  $"{employee.FirstName} {employee.LastName}";
            }

            List<Order> orders = context.Orders.Where(o => o.EmployeeId == id).ToList();

            return View(orders);

        }


        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST Employees/Create
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    NorthwindDbContext context = new NorthwindDbContext();
                    Employee newEmployee = new Employee();
                    newEmployee.FirstName = employee.FirstName;
                    newEmployee.LastName = employee.LastName;
                    newEmployee.Title = employee.Title;

                    context.Employees.Add(newEmployee);
                    context.SaveChanges();

                    TempData["SuccessMessage"] = "Saved successfully.";
                    // redirect to index page after adding new employee
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["ErrorMessage"] = "Somethwing wrong happened!";
                }
            }

            return View();
        }
    }
}
