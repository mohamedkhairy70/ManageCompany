using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageCompany.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.IO;

namespace ManageCompany.Controllers
{
    public class DepartmentEmployeeController : Controller
    {
        private readonly IReporistory reporistory;
        private readonly IMapper mapper;
        private readonly ILogger<DepartmentEmployeeController> logger;

        public DepartmentEmployeeController(IReporistory reporistory,IMapper mapper,ILogger<DepartmentEmployeeController> logger)
        {
            this.reporistory = reporistory;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: DepartmentEmployee
        public async Task<IActionResult> Index()
        {
            try
            {
                var Employees = await reporistory.GetEmployees();

                var data = mapper.Map<IEnumerable<Employee>,IEnumerable<DepartmentEmployeeViewModel>>(Employees);
                return View(data);
            }
            catch
            {
                logger.LogError("Same thing Error in your request");
                return BadRequest();
            }
            
        }

        // GET: DepartmentEmployee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentEmployeeAwiat = await reporistory.GetEmployees();
            var departmentEmployeeOne = departmentEmployeeAwiat.FirstOrDefault(m => m.Id == id);
            if (departmentEmployeeOne == null)
            {
                return NotFound();
            }

            return View(mapper.Map<Employee,DepartmentEmployeeViewModel>(departmentEmployeeOne));
        }

        // GET: DepartmentEmployee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DepartmentEmployee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentEmployeeViewModel departmentEmployeeViewModel)
        {
            foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                ms.Close();
                ms.Dispose();
                if (file.Name == nameof(departmentEmployeeViewModel.DepartmentLogo))
                {
                    departmentEmployeeViewModel.DepartmentLogo = ms.ToArray();
                }
                else if(file.Name == nameof(departmentEmployeeViewModel.Image))
                {
                    departmentEmployeeViewModel.Image = ms.ToArray();
                }
            }
            if (ModelState.IsValid)
            {

                var department = await reporistory.GetDepartmentByName(departmentEmployeeViewModel.DepartmentName);
                
                if(department == null)
                {
                    department = new Department
                    {
                        Logo = departmentEmployeeViewModel.DepartmentLogo,
                        Name = departmentEmployeeViewModel.DepartmentName,
                        Description = departmentEmployeeViewModel.DepartmentDescription
                    };
                    await reporistory.AddDepartment(department);
                }
                var employee = await reporistory.GetEmployeeByName(departmentEmployeeViewModel.Name);

                if (employee == null && department != null)
                {
                    var newEmployee = new Employee
                    {
                        Name = departmentEmployeeViewModel.Name,
                        Image = departmentEmployeeViewModel.Image,
                        DateOfBirth = departmentEmployeeViewModel.DateOfBirth,
                        Department = department
                    };
                    await reporistory.AddEmployee(newEmployee);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departmentEmployeeViewModel);
        }

        // GET: DepartmentEmployee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentEmployeeAwiat = await reporistory.GetDepartments();
            var departmentEmployeeOne = departmentEmployeeAwiat.FirstOrDefault(m => m.Id == id);
            if (departmentEmployeeOne == null)
            {
                return NotFound();
            }
            return View(departmentEmployeeOne);
        }

        // POST: DepartmentEmployee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentEmployeeViewModel departmentEmployeeViewModel)
        {
            if (id != departmentEmployeeViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(departmentEmployeeViewModel);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentEmployeeViewModelExists(departmentEmployeeViewModel.Id).Result)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departmentEmployeeViewModel);
        }

        // GET: DepartmentEmployee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentEmployeeAwiat = await reporistory.GetDepartments();
            var departmentEmployeeOne = departmentEmployeeAwiat.FirstOrDefault(m => m.Id == id);
            if (departmentEmployeeOne == null)
            {
                return NotFound();
            }

            return View(departmentEmployeeOne);
        }

        // POST: DepartmentEmployee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var departmentEmployeeViewModel = await _context.DepartmentEmployeeViewModel.FindAsync(id);
            //_context.DepartmentEmployeeViewModel.Remove(departmentEmployeeViewModel);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DepartmentEmployeeViewModelExists(int id)
        {
            var departmentEmployeeAwiat = await reporistory.GetDepartments();
            var departmentEmployeeOne = departmentEmployeeAwiat.Any(m => m.Id == id);
            return departmentEmployeeOne;
        }
    }
}
