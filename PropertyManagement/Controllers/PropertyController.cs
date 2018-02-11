using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PropertyManagement.Models;

namespace PropertyManagement.Controllers
{
    public class PropertyController : Controller
    {
        PropManagementDBEntities dBEntities = new PropManagementDBEntities();
        // GET: Property
        public ActionResult Index()
        {
            return View(dBEntities.Properties.ToList());
        }

        // GET: Property/Details/5
        public ActionResult Details(int id)
        {
            return View(dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id == id));
        }

        // GET: Property/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Property/Create
        [HttpPost]
        public ActionResult Create(Property property)
        {
            try
            {
                // TODO: Add insert logic here
                dBEntities.Properties.Add(property);
                dBEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Property/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Property/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Property/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id == id));
        }

        // POST: Property/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Property remove = dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id == id);
                dBEntities.Properties.Remove(remove);
                dBEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
