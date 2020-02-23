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
        // GET: Custome
        [Authorize]
        public ActionResult Index()
        {
          
            return View();
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
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Customers");
            }
            var viewmodel = new CustomerViewModel
            {
                Customer = customer,
                MembershipTypes = db.MemberShipTypes.ToList(),
            };

            return View(viewmodel);
        }

        public ActionResult Edit(int id)
        {
            var customer = db.Customers.Include(m => m.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var customerform = new CustomerViewModel
            {
                Customer = customer,
                MembershipTypes = db.MemberShipTypes.ToList()
            };

            return View(customerform);
        }
        [HttpPost]

        public ActionResult Edit(Customer customer)
        {
            var customerindb = db.Customers.Include(m => m.MembershipType).SingleOrDefault(c => c.Id == customer.Id);
            if (ModelState.IsValid)
            {
                if (customerindb == null)
                {
                    return HttpNotFound();
                }
                customerindb.Name = customer.Name;
                customerindb.BirthDate = customer.BirthDate;
                customerindb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerindb.MembershipTypeId = customer.MembershipTypeId;
                db.SaveChanges();

                return RedirectToAction("Index", "Customers");
            }
            var viewmodel = new CustomerViewModel
            {
                Customer = customerindb,
                MembershipTypes = db.MemberShipTypes.ToList()
            };
            return View(viewmodel);
          
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}