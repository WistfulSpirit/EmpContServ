using EmpContServ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EmpContServ.Repository
{
    public class DeptRepository
    {
        SqlConnection connection;

        public DeptRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            if (connectionString.Contains("[DataDirectory]"))
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
                connectionString = connectionString.Replace("[DataDirectory]", path);
            }
            connection = new SqlConnection(connectionString);
        }
        /// <summary>
        /// Get all records from Dept table
        /// </summary>
        /// <returns><see cref="List{Dept}"/>List of Dept instances</returns>
        public List<Dept> GetAll()
        {
            List<Dept> depts = new List<Dept>();
            string cmd = "SELECT Id, [Name], " +
                "(SELECT COUNT(*) FROM Employee WHERE Dept_Id = Dept.Id) as EmpCount " +
                "FROM Dept";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Dept dept = new Dept();
                        dept.Id = (int)reader["Id"];
                        dept.Name = (string)reader["Name"];
                        dept.EpmployeesCount = (int)reader["EmpCount"];
                        depts.Add(dept);
                    }
                }
                connection.Close();
            }
            return depts;
        }
        /// <summary>
        /// Get department from Dept table by given id
        /// </summary>
        /// <param name="id">Id of department</param>
        /// <returns>instance of Dept</returns>
        public Dept Get(int id)
        {
            Dept dept = null;
            string cmd = "SELECT Id, [Name], " +
                "(SELECT COUNT(*) FROM Employee WHERE Dept_Id = Dept.Id) as EmpCount " +
                "FROM Dept WHERE Id = " + id;
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dept = new Dept();
                    reader.Read();
                    dept.Id = (int)reader["Id"];
                    dept.Name = (string)reader["Name"];
                    dept.EpmployeesCount = (int)reader["EmpCount"];
                }
                connection.Close();
            }
            return dept;
        }
        /// <summary>
        /// Insert into dept table row with given name
        /// </summary>
        /// <param name="Name">Name of department</param>
        /// <returns>Id of inserted row. -1 if insert failed</returns>
        public int Create(string Name)
        {
            int id = -1;
            string cmd = $"INSERT INTO Dept ([Name]) OUTPUT INSERTED.ID VALUES (N'{Name}')";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                connection.Open();
                id = (int)command.ExecuteScalar();
                connection.Close();
            }
            return id;
        }
        /// <summary>
        /// Updates row in Dept table 
        /// </summary>
        /// <param name="id">id of editing row</param>
        /// <param name="Name">new department name</param>
        /// <returns>Number of rows affected or -1 in case of failure</returns>
        public int Update(int id, string Name)
        {
            int rRow = -1;
            string cmd = "UPDATE Dept SET Name=@Name WHERE Id =@Id";
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Id", id);
                rRow = command.ExecuteNonQuery();
                connection.Close();
            }
            return rRow;
        }
        /// <summary>
        /// Delete rows in Employee table with given department id and removes this department from Dept table
        /// </summary>
        /// <param name="id">Department id</param>
        /// <param name="Name">Department name</param>
        /// <returns>Number of rows affected. -1 in case of failure</returns>
        public int Delete(int id)
        {
            int rRow = -1;
            string cmd = "DELETE FROM Employee WHERE Dept_Id = " + id +
                "; DELETE FROM Dept WHERE Id =" + id;
            using (SqlCommand command = new SqlCommand(cmd, connection))
            {
                connection.Open();
                rRow = command.ExecuteNonQuery();
                connection.Close();
            }
            return rRow;
        }
    }
}
