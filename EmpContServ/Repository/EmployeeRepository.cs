using EmpContServ.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmpContServ.Repository
{
    public class EmployeeRepository
    {
        //TODO: Remove OUTPUT.ID from commands 
        SqlConnection connection;

        public EmployeeRepository(IConfiguration config)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            if (connectionString.Contains("[DataDirectory]"))
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
                connectionString = connectionString.Replace("[DataDirectory]", path);
            }
            connection = new SqlConnection(connectionString);
        }
        /// <summary>
        /// Get all records from Employee table
        /// </summary>
        /// <returns><see cref="List{Employee}"/>List of Employee instances</returns>
        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();
            string command = "SELECT * FROM Employee";
            using (SqlCommand sqlCom = new SqlCommand(command, connection))
            {
                connection.Open();
                var reader = sqlCom.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.Id = (int)reader["Id"];
                        employee.Name = (string)reader["Name"];
                        employee.Surname = (string)reader["Surname"];
                        employee.Patronymic = (string)reader["Patronymic"];
                        employee.Birthday = (DateTime)reader["Birthday"];
                        employee.Address = (string)reader["Address"];
                        employee.About = (string)reader["About"];
                        employee.Dept_Id = (int)reader["Dept_Id"];
                        employees.Add(employee);
                    }
                }
                connection.Close();
            }
            return employees;
        }
        /// <summary>
        /// Get All Employess of one department
        /// </summary>
        /// <param name="Dept_Id">Department id</param>
        /// <returns></returns>
        public List<Employee> GetAll(int Dept_Id)
        {
            List<Employee> employees = new List<Employee>();
            string command = "SELECT * FROM Employee WHERE Dept_Id = " + Dept_Id;
            using (SqlCommand sqlCom = new SqlCommand(command, connection))
            {
                connection.Open();
                var reader = sqlCom.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.Id = (int)reader["Id"];
                        employee.Name = (string)reader["Name"];
                        employee.Surname = (string)reader["Surname"];
                        employee.Patronymic = (string)reader["Patronymic"];
                        employee.Birthday = (DateTime)reader["Birthday"];
                        employee.Address = (string)reader["Address"];
                        employee.About = (string)reader["About"];
                        employee.Dept_Id = (int)reader["Dept_Id"];
                        employees.Add(employee);
                    }
                }
                connection.Close();
            }
            return employees;
        }

        /// <summary>
        /// Get employee by given id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee Get(int id)
        {
            Employee employee = null;
            string cmd = "SELECT * FROM Employee WHERE Id = " + id;
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                command.Parameters.Add(new SqlParameter("@mId", id));
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    employee = new Employee();
                    reader.Read();
                    employee.Id = (int)reader["Id"];
                    employee.Name = (string)reader["Name"];
                    employee.Surname = (string)reader["Surname"];
                    employee.Patronymic = (string)reader["Patronymic"];
                    employee.Birthday = (DateTime)reader["Birthday"];
                    employee.Address = (string)reader["Address"];
                    employee.About = (string)reader["About"];
                    employee.Dept_Id = (int)reader["Dept_Id"];
                }
                connection.Close();
            }
            return employee;
        }

        /// <summary>
        /// Create Employee 
        /// </summary>
        /// <param name="emp">Employee instance</param>
        /// <returns>id of inserted Employee</returns>
        public int Create(Employee emp)
        {
            int id = -1;
            string cmd = $"INSERT INTO Employee ([Name], Surname, Patronymic, Birthday, [Address], About, Dept_Id)  OUTPUT INSERTED.ID" +
                $" VALUES (@Name, @Surname, @Patronymic, @Birthday, @Address, @About, @Dept_Id)";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                command.Parameters.AddWithValue("@Name", emp.Name);
                command.Parameters.AddWithValue("@Surname", emp.Surname);
                command.Parameters.AddWithValue("@Patronymic", (object)emp.Patronymic ?? DBNull.Value);
                command.Parameters.AddWithValue("@Birthday", emp.Birthday);
                command.Parameters.AddWithValue("@Address", (object)emp.Address ?? DBNull.Value);
                command.Parameters.AddWithValue("@About", (object)emp.About ?? DBNull.Value);
                command.Parameters.AddWithValue("@Dept_Id", emp.Dept_Id);
                connection.Open();
                id = (int)command.ExecuteScalar();
                connection.Close();
            }
            return id;
        }
        /// <summary>
        ///  Update employee with given id 
        /// </summary>
        /// <param name="emp">employee instance</param>
        /// <returns>Number of rows affected or -1 in casew of failure</returns>
        public int Update(Employee emp)
        {
            int rRow = -1;
            string cmd = $"UPDATE Employee SET [Name] = @Name, Surname = @Surname, Patronymic = @Patronymic," +
                $" Birthday = @Birthday, [Address] = @Address, About = @About, Dept_Id = @Dept_Id WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                command.Parameters.AddWithValue("@Id", emp.Id);
                command.Parameters.AddWithValue("@Name", emp.Name);
                command.Parameters.AddWithValue("@Surname", emp.Surname);
                command.Parameters.AddWithValue("@Patronymic", emp.Patronymic);
                command.Parameters.AddWithValue("@Birthday", emp.Birthday);
                command.Parameters.AddWithValue("@Address", emp.Address);
                command.Parameters.AddWithValue("@About", emp.About);
                command.Parameters.AddWithValue("@Dept_Id", emp.Dept_Id);
                connection.Open();
                rRow = command.ExecuteNonQuery();
                connection.Close();
            }
            return rRow;
        }
        /// <summary>
        /// Removes Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Number of rows affected or -1 in casew of failure</returns>
        public int Delete(int id)
        {
            int rRow = -1;
            string cmd = "DELETE FROM Employee WHERE Id =" + id;
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return rRow;
        }
    }
}


