using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTask.Data;
using EmployeeTask.Dto.AddDto;
using EmployeeTask.Dto.UpdateDto;
using EmployeeTask.Manager.Interfaces;
using EmployeeTask.Model.ViewModels;

namespace EmployeeTask.Manager.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmpModelFirst _db = new EmpModelFirst();
        public async Task<IEnumerable<EmployeeViewModel>> GetEmployees()
        {
            var employees = await _db.Employees
                .Include("Departments")
                .Select(e => new EmployeeViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Gender = e.Gender,
                Contact = e.Contact,
                Address = e.Address,
                DepartmentId = e.DepartmentId,
                DepartmentName = _db.Departments
                    .FirstOrDefault(d => d.Id == e.DepartmentId).Name
            }).ToListAsync();

            return employees;

        }

        public async Task<EmployeeViewModel> GetEmployeeById(int id)
        {
            var employee = await _db.Employees
                .SingleOrDefaultAsync(e => e.Id == id);
            
            var emp = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Gender = employee.Gender,
                Contact = employee.Contact,
                Address = employee.Address,
                DepartmentId = employee.DepartmentId,
                DepartmentName = _db.Departments
                    .FirstOrDefault(d => d.Id == employee.DepartmentId)?
                    .Name
            };

            return emp;
        }

         public bool DeleteEmployee(int id)
         {
             var employee = _db.Employees.SingleOrDefault(e => e.Id == id);
             if (employee != null)
             {
                 var query = _db.Employees.Remove(employee);
             }
             return (_db.SaveChanges() > 0);
         }

         public async Task<EmployeeViewModel> UpdateEmployee(EmployeeUpdateDto employeeUpdateDto)
         {
             var employee = await _db.Employees.Include("Department").SingleOrDefaultAsync(e => e.Id == employeeUpdateDto.Id);
             employee.Name = employeeUpdateDto.Name;
             employee.Gender = employeeUpdateDto.Gender;
             employee.Contact = employeeUpdateDto.Contact;
             employee.Address = employeeUpdateDto.Address;
             employee.DepartmentId = _db.Departments
                 .FirstOrDefault(d => d.Name == employeeUpdateDto.DepartmentName)
                 .Id;
             
             await _db.SaveChangesAsync();
             
             return await GetEmployeeById(employee.Id);

         }


         public async Task<IEnumerable<EmployeeViewModel>> AddEmployee(EmployeeAddDto employeeDto)
         {

             var newEmployee = new Employee
             {
                 Name = employeeDto.Name,
                 Gender = employeeDto.Gender,
                 Contact = employeeDto.Contact,
                 Address = employeeDto.Address,
                 DepartmentId = _db.Departments
                     .FirstOrDefault(d => d.Name == employeeDto.DepartmentName)
                     .Id
             };

             var query = _db.Employees.Add(newEmployee);
             await _db.SaveChangesAsync();

             return await GetEmployees();
         }
    }
}