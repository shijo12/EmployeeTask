using System.Threading.Tasks;
using System.Web.Http;
using EmployeeTask.Dto.AddDto;
using EmployeeTask.Dto.UpdateDto;
using EmployeeTask.Manager.Repository;

namespace EmployeeTask.Api.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly EmployeeRepository _empRepository = new EmployeeRepository();

        //start https://localhost:port/swagger/
        // GET api/employees
        public async Task<IHttpActionResult> GetEmployees()
        {
            var employees = await _empRepository.GetEmployees();
            return Ok(employees);
        }

        //Get api/employees/{id}
        public async Task<IHttpActionResult> GetEmployeeById(int id)
        {
            var employee = await _empRepository.GetEmployeeById(id);
            return Ok(employee);
        }
        
        //Delete api/employees/{id}
        public IHttpActionResult DeleteEmployee(int id)
        {
            var hasDeleted = _empRepository.DeleteEmployee(id);
            return Ok(hasDeleted);
        }
        
        //Post api/employees/{EmployeeAddDto}
        public async Task<IHttpActionResult> PostNewEmployee(EmployeeAddDto employeeDto)
        {
            var employees = await _empRepository.AddEmployee(employeeDto);
            return Ok(employees);
        }
        
        //Put api/employees/{EmployeeUpdateDto}
        public async Task<IHttpActionResult> PutUpdateEmployee(EmployeeUpdateDto employeeUpdateDto)
        {
            var employee = await _empRepository.UpdateEmployee(employeeUpdateDto);
            return Ok(employee);
        }
    }
}