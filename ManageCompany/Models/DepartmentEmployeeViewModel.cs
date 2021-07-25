using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageCompany.Models
{
    public class DepartmentEmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] Image { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
        public byte[] DepartmentLogo { get; set; }
        
    }
}
