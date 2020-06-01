using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmpContServ.Repository;
using EmpContServ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

namespace EmpContServ.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptController : ControllerBase
    {
        static IConfigurationBuilder configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
        static IConfigurationRoot config = configBuilder.Build();
        DeptRepository deptRepository = new DeptRepository(config);

        // GET: api/Dept
        [HttpGet]
        public IEnumerable<Dept> Get()
        {
            return deptRepository.GetAll();
        }

        // GET: api/Dept/3
        [HttpGet("{id}")]
        public Dept Get(int id)
        {
            return deptRepository.Get(id);
        }

        // POST: api/Dept
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Dept value)
        {
            try
            {
                deptRepository.Create(value.Name);
                return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
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

        // PUT: api/Dept/
        [HttpPut]
        public HttpResponseMessage Put([FromBody] Dept value)
        {
            try
            {
                deptRepository.Update(value.Id, value.Name);
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

        // DELETE: api/Dept/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                int res = deptRepository.Delete(id);
                if(res == 0)//If no rows were affected
                {
                    throw new Exception("Удаления не произведено!");
                }
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
