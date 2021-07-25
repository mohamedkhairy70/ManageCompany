using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageCompany.Models
{
    public class Reporistory : IReporistory
    {
        private readonly ManageCompanyContext context;

        public Reporistory(ManageCompanyContext context)
        {
            this.context = context;
        }

        // AddDepartment( new department )
        // and save new department
        public async Task<Department> AddDepartment(Department department)
        {
            if (!context.Departments.Where(d => d.Name == department.Name).Any())
            {
                await context.Departments.AddAsync(department);
                await context.SaveChangesAsync();
                return department;
            }
            else
            {
                return null;
            }
        }
        // AddEmployee( new employee )
        // and save new Employee
        public async Task<Employee> AddEmployee(Employee employee)
        {
            if (!context.Employees.Where(d => d.Name == employee.Name).Any())
            {
                await context.Employees.AddAsync(employee);
                await context.SaveChangesAsync();
                return employee;
            }
            else
            {
                return null;
            }
        }


        // check Department Async By Name
        public async Task<Department> GetDepartmentByName(string Name) => await context.Departments.Where(d => d.Name == Name).FirstOrDefaultAsync();

        // Get all Department Async
        public async Task<IEnumerable<Department>> GetDepartments() => await context.Departments.ToListAsync();

        // check Employee Async By Name
        public async Task<Employee> GetEmployeeByName(string Name) => await context.Employees.Where(d => d.Name == Name).FirstOrDefaultAsync();

        // Get all Employee Async
        public async Task<IEnumerable<Employee>> GetEmployees() => await context.Employees.Include(d=>d.Department).ToListAsync();

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            if (context.Employees.Where(d => d.Name == employee.Name).Any())
            {
                context.Employees.Update(employee);
                await context.SaveChangesAsync();
                return employee;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> DeleteEmployee(Employee employee)
        {
            if (context.Employees.Where(d => d.Name == employee.Name).Any())
            {
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Employee> FindEmployee(int Id) => await context.Employees.FindAsync(Id);
    }
}
