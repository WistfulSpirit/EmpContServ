using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmpContServ.Repository;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmpContServ.Controllers
{
    public class EmployeeController : Controller
    {
        static IConfigurationBuilder configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
        static IConfigurationRoot config = configBuilder.Build();
        EmployeeRepository employeeRepository = new EmployeeRepository(config);
        DeptRepository deptRepository = new DeptRepository(config);
        static int Dept_Id = -1;

        // GET: Employee/Index/1
//        [Route("{controller}/Index/id")]
        public ActionResult Index(int id)
        {
            //TODO: Get by dept_id
            //TODO: "Back To list" on views add Dept_Id to link, Maybe pass it in Viewbag or smthng
            //TODO: DropDownList with Depts on create/Edit Employee 
            //TODO: Fully debug Employee Views
            //TODO: Think about full trycatch removal 
            //TODO: Add comments!
            Dept_Id = id;
            ViewBag.DeptName = deptRepository.Get(Dept_Id).Name;
            return View(employeeRepository.GetAll(id));
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.DeptName = deptRepository.Get(Dept_Id).Name;
            ViewBag.DeptId = Dept_Id;
            
            return View(employeeRepository.Get(id));
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.DeptList = new SelectList(deptRepository.GetAll(), "Id", "Name", Dept_Id);
            ViewBag.DeptId = Dept_Id;
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                ViewBag.DeptId = Dept_Id;
                employeeRepository.Create(new Models.Employee
                {
                    Name = (string)collection["Name"],
                    Surname      = (string)collection["Surname"] ,
                    Patronymic   = (string)collection["Patronymic"], 
                    Birthday     = Convert.ToDateTime(collection["Birthday"]),
                    Address      = (string)collection["Address"],
                    About        = (string)collection["About"],
                    Dept_Id      = Convert.ToInt32(collection["Dept_Id"])

                });
                return RedirectToAction(nameof(Index), new { id = Dept_Id });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.DeptId = Dept_Id;
            ViewBag.DeptList = new SelectList(deptRepository.GetAll(), "Id", "Name", Dept_Id);
            return View(employeeRepository.Get(id));
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                ViewBag.DeptId = Dept_Id;
                employeeRepository.Update(new Models.Employee
                {
                    Id = id,
                    Name = (string)collection["Name"],
                    Surname = (string)collection["Surname"],
                    Patronymic = (string)collection["Patronymic"],
                    Birthday = Convert.ToDateTime(collection["Birthday"]),
                    Address = (string)collection["Address"],
                    About = (string)collection["About"],
                    Dept_Id = Convert.ToInt32(collection["Dept_Id"])
                });
                return RedirectToAction(nameof(Index), new { id = Dept_Id });
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.DeptId = Dept_Id;
            return View(employeeRepository.Get(id));
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                ViewBag.DeptId = Dept_Id;
                employeeRepository.Delete(id);

                return RedirectToAction(nameof(Index), new { id = Dept_Id });
            }
            catch
            {
                return View();
            }
        }
    }
}