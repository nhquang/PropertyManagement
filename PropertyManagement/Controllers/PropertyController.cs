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
            return View(dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id ==  id));
        }

        // POST: Property/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Property property)
        {
            try
            {
                // TODO: Add update logic here
                if (!ModelState.IsValid)
                {

                    return RedirectToAction("edit", new { id = property.Id });
                }

                if (id != property.Id)
                {

                    return RedirectToAction("index");
                }
                var o = dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id == id);
                o.Name = property.Name;
                o.Price = property.Price;
                o.Date = property.Date;
                o.ExpirationDate = property.ExpirationDate;
                dBEntities.SaveChanges();
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
            if(dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id == id).ExpirationDate == null)
            {
                ViewBag.stillInUse = true;
            }
            else
            {
                ViewBag.stillInUse = false;
            }
            return View(dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id == id));
        }

        // POST: Property/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Property removedProperty = dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id == id);
                
                if (removedProperty.Problems.Count > 0)
                {
                    
                    var x = dBEntities.Problems.Where(c => c.PropID == id);
                    dBEntities.Problems.RemoveRange(x);
                    dBEntities.SaveChanges();

                }
                dBEntities.Properties.Remove(removedProperty);
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
