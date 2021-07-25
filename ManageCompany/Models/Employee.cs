using System;
using System.ComponentModel.DataAnnotations;


namespace ManageCompany.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        public byte[] Image { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
