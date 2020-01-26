using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Customer
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }
       
        public ActionResult Details(int id)
        {
            var customer = db.Customers.Include(c =>c.MembershipType).SingleOrDefault(c => c.Id == id);

            if(customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }
        
        
        public ActionResult Create()
        {

            var memberships = db.MemberShipTypes.ToList();

            var viemodel = new CustomerViewModel
            {
                MembershipTypes = memberships
            };


            return View(viemodel);
        }
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}