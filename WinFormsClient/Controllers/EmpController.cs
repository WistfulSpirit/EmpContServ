using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WinFormsClient.Models;

namespace WinFormsClient.Controllers
{
    class EmpController
    {
        private HttpClient client;

        public EmpController(HttpClient Client)
        {
            client = Client;
        }


        public async Task<List<Employee>> GetAsync(int dept_id)
        {
            HttpResponseMessage response = await client.GetAsync("/api/Employee/GetAll/" + dept_id);
            response.EnsureSuccessStatusCode(); // Throw on error code.
            var emps = JsonConvert.DeserializeObject<List<Employee>>(response.Content.ReadAsStringAsync().Result);
            return emps;
        }

        public async Task CreateAsync(Employee emp)
        {
            var response = await client.PostAsync("/api/Employee/", new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode(); // Throw on error code.
        }

        public async Task EditAsync(Employee emp)
        {
            var response = await client.PutAsync("/api/Employee/", new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode(); // Throw on error code.
        }

        public async Task DeleteAsync(int id)
        {
            var response = await client.DeleteAsync("/api/Employee/" + id);
            response.EnsureSuccessStatusCode(); // Throw on error code.
        }

    }
}
