using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeTask.Dto.AddDto;
using EmployeeTask.Dto.UpdateDto;
using EmployeeTask.Model.ViewModels;

namespace EmployeeTask.Manager.Interfaces
{
    interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeViewModel>> GetEmployees();
        Task<EmployeeViewModel> GetEmployeeById(int id);
        Task<IEnumerable<EmployeeViewModel>> AddEmployee(EmployeeAddDto employeeAddDto);
        bool DeleteEmployee(int id);
        Task<EmployeeViewModel> UpdateEmployee(EmployeeUpdateDto employeeUpdateDto);
    }
}
