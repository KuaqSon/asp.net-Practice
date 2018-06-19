using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Net;
using PagedList;

namespace WebApplication1.Controllers
{
    public class CustomersController : Controller
    {
        private MovieDbContext db = new MovieDbContext();

        //GET: Customer 
        //public ActionResult Index()
        //{
        //    return View(db.Customers.ToList());
        //}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var customers = from s in db.Customers
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(s => s.ID);
                    break;
                case "Date":
                    customers = customers.OrderBy(s => s.Dob);
                    break;
                case "date_desc":
                    customers = customers.OrderByDescending(s => s.Dob);
                    break;
                default:  // Name ascending 
                    customers = customers.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var viewModel = customers.ToPagedList(pageNumber, pageSize);
            return View(viewModel);
        }
        
        [HttpGet]
        public ActionResult Create(string Title)
        {
            return View();
        }

        // Create New Customer
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

        //Get Details
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //GET Edit Customer
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if ( id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST Edit Customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        //GET Delete customer
        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = db.Customers.Find(id);
        //    if(customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}

        ////POST Delete customer
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
      
        //public ActionResult Delete()
        //{
        //    return PartialView("_ModalDelete",null);
        //}

       
        public ActionResult DeleteConfirm(int id) //--> no jump to Delete Page
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditModal (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Modal",customer);
        }

        [HttpPost]
        public ActionResult EditModal(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_CustomerRow", customer);
            }
            //return View(customer);
            return new EmptyResult();
            // return partialView -> rows
        }

        [HttpGet]
        public ActionResult CreateModal()
        {
            Customer customer = new Customer();
            return PartialView("_ModalAdd",customer);
        }

        [HttpPost]
        public ActionResult CreateModal(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return PartialView("_CustomerRow", customer);
            }
            return new EmptyResult();
        }

        public ActionResult Search(string searchString)
        {
            var customers = db.Customers.Select(x => x);
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.Name.Contains(searchString));
            }
            customers = customers.OrderBy(x => x.Name);
            var pageSize = 4;
            var pageNumber = 1;
            var listCustomer = customers.ToPagedList(pageNumber,pageSize);
            return PartialView("_CustomerList", listCustomer);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}