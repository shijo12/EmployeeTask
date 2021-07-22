using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeTask.Dto.AddDto
{
    public class EmployeeAddDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        
        public string DepartmentName { get; set; }
    }
}