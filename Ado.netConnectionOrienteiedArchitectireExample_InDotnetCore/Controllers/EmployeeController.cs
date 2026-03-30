using Ado.netConOrienteiedArchitectire_InDotnetCore.Models;
using Ado.netConOrienteiedArchitectire_InDotnetCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ado.netConOrienteiedArchitectire_InDotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        EmployeeServices employeeServices=new EmployeeServices();

        [HttpGet]
        [Route("GetAllEmployee")]
        //we can write shortcut this way also [HttpGet("GetAllEmployee")] instead of writing [HttpGet] and [Route("GetAllEmployee")] separately.
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            { 
//Here try block is used to handle the exceptions that may occur during the execution of the code. If any exception occurs, it will be caught in the catch block and we can return a status code with a message to the client.
                var empdata = await employeeServices.GetAllEmployee();
                if (empdata == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, empdata);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

        [HttpGet]
        [Route("GetEmployeeById/{empid}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int empid)
        {
            try
            {
                var empdata = await employeeServices.GetEmployeeByEmpid(empid);
                if (empdata == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, empdata);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }



        [HttpPost]//insert purpose use this Addemployee
        [Route("AddEmployee")]//routename describe here.
        public async Task<IActionResult> Post([FromBody] EmployeeDto empdto)
        {
            try
            {
                //!ModelState.IsValid is used to check whether the model state is valid or not. If the model state is not valid, it will return a status code with a message to the client. If the model state is valid, it will proceed to add the employee data to the database.
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {//oldway:var result=obj.AddEmployee(empdto);
                    var employeeData = await employeeServices.AddEmployee(empdto);
                    return StatusCode(StatusCodes.Status201Created, "employee added sucessfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

        [HttpPut]//update the data purpose we are used.
        [Route("UpdateEmployee")]
        public async Task<IActionResult> Put([FromBody] EmployeeDto empdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var res = await employeeServices.UpdateEmployee(empdto);
                    return StatusCode(StatusCodes.Status200OK, "employee updated sucessfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }

        [HttpDelete]
        [Route("DeleteEmployeeByEmpid")]
        public async Task<IActionResult> delete(int empid)
        {
            if (empid < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var res = await employeeServices.DeleteEmployeeByEmpid(empid);
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "not found");
                }
                else
                {//if the value is deleted successfully ,we are returnuing the status code 200 with the message content deleted successfully to the client.
                    return StatusCode(StatusCodes.Status200OK, "content deleted successfully");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
    }
}
