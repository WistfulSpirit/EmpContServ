using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmpContServ.Repository;
using EmpContServ.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;

namespace EmpContServ.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        static IConfigurationBuilder configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
        static IConfigurationRoot config = configBuilder.Build();
        EmployeeRepository employeeRepository = new EmployeeRepository(config);
        // GET: api/Employee/All/Id
        [HttpGet("GetAll/{id}")]
        public IEnumerable<Employee> GetAll(int id)
        {
            return employeeRepository.GetAll(id);
        }

        // POST: api/Employee
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Employee value)
        {
            try
            {
                employeeRepository.Create(value);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace('\n', ' ').Replace('\r', ' ');
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = msg
                };
            }
        }

        // PUT: api/Employee/5
        [HttpPut]
        public HttpResponseMessage Put([FromBody] Employee value)
        {
            try
            {
                employeeRepository.Update(value);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace('\n', ' ').Replace('\r', ' ');
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = msg
                };
            }
        }

        // DELETE: api/Empliyee/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                int res = employeeRepository.Delete(id);
                if (res == -1)
                    throw new Exception("Удаление не выполнено!");
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace('\n', ' ').Replace('\r', ' ');
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = msg
                };
            }
        }
    }
}
