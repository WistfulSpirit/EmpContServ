using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmpContServ.Repository;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EmpContServ.Controllers
{
    public class DeptController : Controller
    {
        static IConfigurationBuilder configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
        static IConfigurationRoot config = configBuilder.Build();
        DeptRepository deptRepository = new DeptRepository(config);

        // GET: Dept
        public ActionResult Index()
        {
            return View(deptRepository.GetAll());
        }

        // GET: Dept/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dept/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dept/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string str = collection["Name"].ToString();
                deptRepository.Create(str);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dept/Edit/5
        public ActionResult Edit(int id)
        {
            return View(deptRepository.Get(id));
        }

        // POST: Dept/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                deptRepository.Update(id, collection["Name"].ToString());
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dept/Delete/5
        public ActionResult Delete(int id)
        {
            return View(deptRepository.Get(id));
        }

        // POST: Dept/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                deptRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(deptRepository.Delete(id));
            }
        }
    }
}