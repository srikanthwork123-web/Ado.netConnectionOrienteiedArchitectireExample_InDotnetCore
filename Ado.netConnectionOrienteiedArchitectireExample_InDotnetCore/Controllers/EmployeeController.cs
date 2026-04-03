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
        /*
we are creating an object of EmployeeServices class to access the methods of EmployeeServices class in our controller. 
This is called tight coupling because the controller is directly dependent on the service class. 
 If we want to change the implementation of the service class, 
 we will also have to change the controller class.
 This is not a good practice because it makes our code less maintainable and less testable.
 To avoid tight coupling,
we can use dependency injection to inject the service class into the controller class. with the help of intervface we can avoid tightly coupling.
 This way, we can change the implementation of the service class without changing the controller class.
                // employeecontoler tightly coupled with EmployeeServices
//oldway of accessing the class:
(don't use this process.don't create the object of the class directly in the controller class because it will create tight coupling between the controller and service class.)
                ///EmployeeServices obj=new EmployeeServices()

                EmployeeServices employeeServices =new EmployeeServices();
        */
        EmployeeServices employeeServices = new EmployeeServices();
        //if you create direact object of the class and if you use obj.methods in your Api method it is called tightly coupling.
        //To avoid the tightly coupling between the controller and service class,
        //we can use dependency injection to inject the service class into the controller class.

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
        {//Task represents aynchronous opertion that can return a value.
         //In this case, the method returns a Task<IActionResult>,
         //which means it will eventually produce an IActionResult when the asynchronous operation is complete.
         //The async keyword indicates that the method contains asynchronous code and can use the await keyword to wait for asynchronous operations to complete without blocking the thread.
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
        {//angular or react or mobile team will send the data to our api.
         //that is called input Payload .(or)Request body(or)input paramtertes
            if (empid < 0)
            {//StatusCode() is a method.this method having 2 paramters.1st one is statuscode need to pass 2.pass the userfriendly message.
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
