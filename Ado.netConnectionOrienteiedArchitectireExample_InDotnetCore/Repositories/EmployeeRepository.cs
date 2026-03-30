using Ado.netConOrienteiedArchitectire_InDotnetCore.Models;
using System.Data;
using Microsoft.Data.SqlClient;
namespace Ado.netConOrienteiedArchitectire_InDotnetCore.Repositories
{
    public class EmployeeRepository
    {

string connectionString= "data source=DESKTOP-13B42NJ;integrated security=yes;Encrypt=True;TrustServerCertificate=True;initial catalog=hotelmanagement";
        //if you are async method then you have to use async keyword in method signature and return type should be Task or Task<T> where T is the type of data you want to return from the method.
    //async and Await keywords used.
    //All database communication storedprocedure calling logic we need to write in only Repository class only
        public async Task<List<Employee>> GetAllEmployee()//this one asyncrounus method and will return list of employee and we have to use await keyword when we call this method
        {
            List<Employee> lstemp = new List<Employee>();
            {
// using (SqlConnection con = new SqlConnection(connectionString))//we can also set connection string in constructor of sqlconnection class
                //(or) we can set connection string like this
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString= connectionString;//we can also set connection string like this
                    con.Open();//open method will open the connection to the database
                    SqlCommand cmd = new SqlCommand("Usp_GetEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();//will return results of select statement
                    while (reader.Read())//while loop will run until there are records to read from the database
                    {
                        Employee emp = new Employee();
                        emp.empid = Convert.ToInt32(reader["empid"]);
                        emp.empname = Convert.ToString(reader["empname"]);
                        emp.empsalary = Convert.ToInt32(reader["empsalary"]);
                        lstemp.Add(emp);//adding the employee object to the list of employee
                    }
                    con.Close();//close method will close the connection to the database
                }
                return lstemp;//returning the list of employee
            }
        }

        public async Task<Employee> GetEmployeeByEmpid(int empid)
        {
            Employee emp = new Employee();//1 object  will store 1 record only
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Usp_GetEmployeeId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", empid);//we have to pass empid as parameter to the stored procedure
                SqlDataReader dr = await cmd.ExecuteReaderAsync();//will return results of select statement
                while (dr.Read())
                {
                    emp.empid = Convert.ToInt32(dr["empid"]);
                    emp.empname = Convert.ToString(dr["empname"]);
                    emp.empsalary = Convert.ToInt32(dr["empsalary"]);
                }
            }
            return emp;
        }


        public async Task<bool> AddEmployee(Employee empdetail)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Usp_AddEmployee_New", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empname", empdetail.empname);
                cmd.Parameters.AddWithValue("@empsalary", empdetail.empsalary);
                con.Open();//we must open the connection manualay
                await cmd.ExecuteNonQueryAsync();
                con.Close();//we must close the connection.
            }
            return true;//boolean value will return true if the employee is added successfully to the database
        }
        public async Task<bool> UpdateEmployee(Employee empdetail)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Usp_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", empdetail.empid);
                cmd.Parameters.AddWithValue("@empsalary", empdetail.empsalary);
                cmd.Parameters.AddWithValue("@empname", empdetail.empname);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            return true;
        }

        public async Task<bool> DeleteEmployeeByEmpid(int empid)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("Usp_DeleteEmployee", con);//Usp_DeleteEmployee
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", empid);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            return true;
        }
    }
}
