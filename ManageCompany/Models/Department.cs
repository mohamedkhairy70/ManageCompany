using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManageCompany.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Logo { get; set; }
        public List<Employee> Employee { get; set; } = new();
    }
}
