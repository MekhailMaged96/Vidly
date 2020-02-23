using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Vidly.Models;


namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
    
        private ApplicationDbContext db = new ApplicationDbContext();

        //Get  /api/customers
        public IHttpActionResult GetCustomers()
        {

            var customers = db.Customers.Include(c => c.MembershipType).ToList();

            if (customers.Count == 0)
            {
                return NotFound();
            }
            return Ok(customers);
        }

        //Get  /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = db.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        //Post  /api/customers
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" +customer.Id),customer);
        }

        //Put  /api/customers/1
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateCustomer(Customer customer,int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerindb = db.Customers.SingleOrDefault(c => c.Id == id);

            if (customerindb == null)
            {
                return NotFound();
            }

            customerindb.BirthDate = customer.BirthDate;
            customerindb.Name = customer.Name;
            customerindb.MembershipTypeId = customer.MembershipTypeId;
            customerindb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            db.SaveChanges();

            return Ok();

        }

        //Delete  /api/customers/1
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerindb = db.Customers.SingleOrDefault(c => c.Id == id);

            if (customerindb == null)
            {
                return NotFound();
            }
            db.Customers.Remove(customerindb);
            db.SaveChanges();
            return Ok();
        }

    }
}
