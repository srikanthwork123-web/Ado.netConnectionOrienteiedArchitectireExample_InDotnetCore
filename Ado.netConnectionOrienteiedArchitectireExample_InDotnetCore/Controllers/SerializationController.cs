using Ado.netConOrienteiedArchitectire_InDotnetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;//Import the namespace for JSON serialization and deserialization. It provides classes and methods for working with JSON data in .NET applications.
namespace Ado.netConOrienteiedArchitectire_InDotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerializationController : ControllerBase
    {
        //Serialization is aprocess of converting data from one format to another format.
        //InSerialization:we can convert data from  object to JSON Format:

        [HttpGet("SerializationExample")]
        //url:http://localhost:5000/api/Serialization/SerializationExample
        public async Task<IActionResult> GetSerialization()
        {
            //IActionResult IS A RETURN TYPE .IT WILL RETURN STATUS CODE AND DATA TO THE CLIENT.
            //In dotnet core every api method must be return status code.
            //if any data you can return with the help of status code methods like(ok(),status(),BadRequst()....
            //this is one way of creaing the object for employee class and assigning the values to the properties of employee class.

            EmployeeData employee = new EmployeeData();
            employee.Id = 1;
            employee.Name = "John Doe";
            employee.IsActive = true;
            employee.Skills = new List<string> { "C#", ".NET", "SQL" };//Assign the dummy data to list like this way
            //second way of creating object for employee class and assigning the values to the properties of employee class.
            EmployeeData employeeObj = new EmployeeData()
            {//Property=Value,
                Id = 1,
                Name = "John",
                IsActive = true,
                Skills = new List<string> { "C#", ".NET", "SQL" }
            };
            //By default Dotnet core retrun the data in JSON fORMAT.Json is light weight data format and it is very faster  and it is easy to read and write.
            //To exchange the data between client and server we use JSON format. It is a text format that is used to represent the data in a structured way. It is easy to read and write and it is language independent. It is widely used in web applications to exchange data between client and server.
            //Whoever use your api,theycan easily understand and integrate this api response in frontend applications like react,angular,Mobile Application(Android,ios) etc.
            /*below is the Json Format of employeeObj
             {
                "id": 1,
                "name": "John",
                "isActive": true,
                "skills": [
                    "C#",
                    ".NET",
                    "SQL"
                ]
            }
             */
            //Here we are converting the employeeObj to JSON format using the JsonSerializer.Serialize method. It takes an object as a parameter and returns a JSON string representation of that object.
            string employeejson = JsonSerializer.Serialize(employeeObj);//object to json format convert here using Serialize method of JsonSerializer class.

            //return Ok(employeeObj);
            return Ok(employeejson);
        }

         [HttpGet("DeserializeExample")]
        //URl:http://localhost:5000/api/Serialization/DeserializeExample
        public async Task<IActionResult> GetDeserialization()
        {
            EmployeeData employeeObj = new EmployeeData()
            {//Property=Value,
                Id = 1,
                Name = "John",
                IsActive = true,
                Skills = new List<string> { "C#", ".NET", "SQL" }//Assign the dummy data to list like this way
            };
            string employeejson = JsonSerializer.Serialize(employeeObj);//object to json format convert here using Serialize method of JsonSerializer class.
            //Here we are converting the JSON string back to an Employee object using the JsonSerializer.Deserialize method. It takes a JSON string and a type as parameters and returns an object of the specified type.
            EmployeeData employeeJSONObj = JsonSerializer.Deserialize<EmployeeData>(employeejson);//json format to object FORMAT convert here using Deserialize method of JsonSerializer class.
            //we can read the each obect property value like this way after deserialization.
            int id = employeeJSONObj.Id;
            String name = employeeJSONObj.Name;
            bool isActive = employeeJSONObj.IsActive;
            //By default Dotnet core convert and  return the object data into json format.
            return Ok(employeeJSONObj);

        }
    }
}
/*In this code, we have created a SerializationController with two API endpoints: GetSerialization and GetDeserialization.

 JSON:
=======
JSON FullForm is JavaScript Object Notation. It is a lightweight data Exchange format 
In dotnet core we use JSON format to exchange the data between client and server. 
It is a text format that is used to represent the data in a structured way.
It is easy to read and write .
It is  used in web applications like angular/react/mobile applications  to exchange data between client and server.
JsonSerializer is a class in the System.Text.Json namespace that provides methods for serializing and deserializing objects to and from JSON format. 
It is a built-in class in .NET Core and .NET 5+ that allows you to easily convert objects to JSON strings and vice versa.
JsonSerializer.Serialize() → Convert objectData to JSON Format
JsonSerializer.Deserialize() → JSON Format to object Format.
=========
JSON uses camelCase by default in .NET Core.
Property names are case-insensitive when deserializing.
Strings must be in double quotes.
 ============
Basic JSON Syntax

{
"id": 1,
"name": "John",
"isActive": true,
"skills": ["C#", ".NET", "SQL"]
}
Key Rules
	• Data is in key-value pairs 
	• Keys are always strings (in quotes) 
	• Values can be: 
		○ string → "John" 
		○ number → 1 
		○ boolean → true 
		○ array → [] 
		○ object → {} 

🔹 Model Class in .NET Core
In .NET Core, JSON is mapped to a C# class (Model).
Example Model

public class Employee
{
public int Id { get; set; }
public string Name { get; set; }
public bool IsActive { get; set; }
public List<string> Skills { get; set; }
}

🔹 Equivalent JSON for this Model

{
"id": 1,
"name": "John",
"isActive": true,
"skills": ["C#", ".NET", "SQL"]
}
👉 Property names usually match JSON keys (case-insensitive by default).

🔹 Serialization (C# → JSON)
Convert object to JSON:
==========================
using System.Text.Json;

var emp = new Employee
{
Id = 1,
Name = "John",
IsActive = true,
Skills = new List<string> { "C#", ".NET", "SQL" }
};

string json = JsonSerializer.Serialize(emp);

Console.WriteLine(json);
Output

{"id":1,"name":"John","isActive":true,"skills":["C#",".NET","SQL"]}

🔹 Deserialization (JSON → C#)
=====================================
Convert JSON to object:

string json = "{\"id\":1,\"name\":\"John\",\"isActive\":true,\"skills\":[\"C#\",\".NET\",\"SQL\"]}";

Employee emp = JsonSerializer.Deserialize<Employee>(json);

Console.WriteLine(emp.Name);

🔹 Using JSON in ASP.NET Core Web API
Example Controller

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
[HttpPost]
public IActionResult Create(Employee emp)
{
return Ok(emp);
}
}
Request JSON

{
"id": 1,
"name": "John",
"isActive": true,
"skills": ["C#", ".NET"]
}
👉 ASP.NET Core automatically:
	• Converts JSON → Model (Deserialization) 
	• Converts Model → JSON (Serialization) 

*/