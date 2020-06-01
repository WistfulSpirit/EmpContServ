using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsClient.Models;

namespace WinFormsClient.Controllers
{
    class DeptController
    {
        private HttpClient client;

        public DeptController(HttpClient Client)
        {
            client = Client;
        }

        /// <summary>
        /// Get all departments 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Dept>> GetAsync()
        {
            HttpResponseMessage response = await client.GetAsync("/api/Dept/");
            response.EnsureSuccessStatusCode(); // Throw on error code.
            var emps = JsonConvert.DeserializeObject<List<Dept>>(response.Content.ReadAsStringAsync().Result);
            return emps;
        }

        public async Task<Dept> GetAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync("/api/Dept/"+id);
            response.EnsureSuccessStatusCode(); // Throw on error code.
            var emps = JsonConvert.DeserializeObject<Dept>(response.Content.ReadAsStringAsync().Result);
            return emps;
        }

        public async Task CreateAsync(Dept dept)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dept), Encoding.UTF8, "application/json");
            Clipboard.SetText(await content.ReadAsStringAsync());
            var response = await client.PostAsync("api/Dept/", new StringContent(JsonConvert.SerializeObject(dept), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode(); // Throw on error code.
        }

        public async Task EditAsync(Dept dept)
        {
            var response = await client.PutAsync("api/Dept", new StringContent(JsonConvert.SerializeObject(dept), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode(); // Throw on error code.
        }

        public async Task DeleteAsync(int id)
        {
            var response = await client.DeleteAsync("api/Dept/" + id);
            response.EnsureSuccessStatusCode(); // Throw on error code.
        }
    }
}
