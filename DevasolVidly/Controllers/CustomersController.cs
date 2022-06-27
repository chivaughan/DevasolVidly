using DevasolVidly.Models;
using DevasolVidly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace DevasolVidly.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                // Return the view if there are validation errors
                var viewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes
                };
                return View("CustomerForm",viewModel);
            }

            if (customer.Id ==0)
            {
                // It is a new customer. So we add it
                _context.Customers.Add(customer);
            }
            else
            {
                // Here, we know that it is an update/edit request
                var customerIndDB = _context.Customers.Single(c =>c.Id == customer.Id);
                customerIndDB.Name = customer.Name;
                customerIndDB.BirthDate = customer.BirthDate;
                customerIndDB.MembershipTypeId = customer.MembershipTypeId;
                customerIndDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            else
            {
                var viewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes
                };
                return View("CustomerForm", viewModel);
            }
        }
        public ActionResult CustomerForm()
        {
            // Fetch the mebership types from the database
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
                Customer = new Customer()
            };
            return View(viewModel);
        }
        // GET: Customers
        public ActionResult Index()
        {
            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            //var customersList = new CustomerListViewModel { Customers = customers };
            //return View(customersList);
            return View();
        }

        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {
            try
            {
                var customers = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
                return View(customers);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }
    }
}