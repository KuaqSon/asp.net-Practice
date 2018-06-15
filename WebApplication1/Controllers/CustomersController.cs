using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class CustomersController : Controller
    {
        private CustomerDBContext db = new CustomerDBContext();

        //GET: Customer 
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        [HttpGet]
        public ActionResult Create(string Title)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }




    }
}