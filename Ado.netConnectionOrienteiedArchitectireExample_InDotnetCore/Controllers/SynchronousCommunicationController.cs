using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ado.netConOrienteiedArchitectire_InDotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SynchronousCommunicationController : ControllerBase
    {
        /*
 =>In .NET, 2 main types of communication patterns are commonly used: Synchronous and Asynchronous communication.
         Synchronous and Asynchronous communication mainly describe how your code calls APIs, services,
         or methods and how it waits for results.
 1.synchronous communication:
 =========================
 1. In synchronous communication, the User waits for the called method to complete 
 before proceeding to the next line of code.It is a blocking operation,
 meaning that the thread executing the code will be blocked until the operation is complete.

         (or)
 In synchronous execution, User should   waits until the previous task completes.
 ==================================================================================
 Key Points
 ===============
 In synchronous communication we are not used any "async" and "await" and "Task"  keywords.
 =>If you try to call any datbase opertion method it will block one thread to be complete the process.
 =>upto the time of completion of the process, the thread will be busy and it will not be able to do any other work.
 =>user should wait until the process is completed, then only it will be able to do any other work.
 =>Synchronous communication Executes line by line and Simple to understand.
 =>Synchronous communication is Slow for long operations Like (API/DB calls)
 =>ASynchronous communication is Fast for long operations Like (API/DB calls),because it does not block the thread while waiting for the operation to complete.
 1.In Synchronous Communication use Synchronous Methods Only(It menas Without using async,await,Task Keywords in method Preparation Time)
 2.In Asynchronus Communication use Asynchronous Methods Only(it means using Async,await,Task Keywords in Method Preaprtion Time)
 =======================================================================================
         */
    }
}
