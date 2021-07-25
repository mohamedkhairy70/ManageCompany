using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageCompany.Models
{
    public class ManageCompanyMappingProfile : Profile
    {
        public ManageCompanyMappingProfile()
        {
            CreateMap<Employee, DepartmentEmployeeViewModel>()
                .ReverseMap()
                .ForMember(m => m.Department, opt => opt.Ignore());
        }
    }
}
