using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageCompany.Models
{
    public interface IReporistory
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Department> GetDepartmentByName(string Name);
        Task<Employee> GetEmployeeByName(string Name);
        Task<Employee> FindEmployee(int Id);
        Task<Department> AddDepartment(Department department);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(Employee employee);
    }
}
