using System;
using System.Text;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.Entity;
using System.Linq;
using AutoMapper;

namespace WebApplication1.Controllers
{
    public class EmployeesController : Controller
    {
        private MovieDbContext _db;// = new MovieDbContext();

        public EmployeesController(MovieDbContext db)
        {
            _db = db;
        }

        public ActionResult Index ()
        {
            var customer = _db.Customers.First();

            var model = AutoMapper.Mapper.Map<Employee>(customer);
            return View(model);
        }
        
        // GET: /AjaxStuff/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AjaxStuff/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content("Sorry, this method can't be called only from AJAX.");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Employees.Add(employee);
                    _db.SaveChanges();
                    return Content("Record added successfully !");
                }
                else
                {
                    StringBuilder strB = new StringBuilder(500);
                    foreach (ModelState modelState in ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            strB.Append(error.ErrorMessage + ".");
                        }
                    }
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content(strB.ToString());
                }
            }
            catch (Exception ee)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content("Sorry, an error occured." + ee.Message);
            }
        }
    }
}