using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PropertyManagement.Models;
using Microsoft.AspNet.Identity;

namespace PropertyManagement.Controllers
{
    public class PropertyController : Controller
    {
        PropManagementDBEntities dBEntities = new PropManagementDBEntities();
        // GET: Property

        public ActionResult Index()
        {
            var currId = User.Identity.GetUserId();
            if (User.Identity.IsAuthenticated)
            {
                return View(dBEntities.Properties.Where(c => c.UserId == currId).ToList());
            }
            else
            {
                return HttpNotFound();
            }
        }

        // GET: Property/Details/5
        public ActionResult Details(int id)
        {
            var currId = User.Identity.GetUserId();
            var propOwnerId = dBEntities.Properties.Find(id).UserId;
            if (String.Compare(currId, propOwnerId) == 0)
            {
                return View(dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id == id));
            }
            else
            {
                return HttpNotFound();
            }
        }

        // GET: Property/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Property/Create
        [HttpPost]
        public ActionResult Create(Property property)
        {
            try
            {
                // TODO: Add insert logic here
                var temp = new Property();
                temp.Name = property.Name;
                temp.Description = property.Description;
                temp.Date = property.Date;
                temp.ExpirationDate = property.ExpirationDate;
                temp.Price = property.Price;
                temp.UserId = User.Identity.GetUserId();
                dBEntities.Properties.Add(temp);
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
            if (User.Identity.IsAuthenticated)
            {
                var currId = User.Identity.GetUserId();
                var propOwnerId = dBEntities.Properties.Find(id).UserId;
                if (String.Compare(currId, propOwnerId) == 0)
                {
                    return View(dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id==id));
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Property/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Property property)
        {
            try
            {
                // TODO: Add update logic here
                var temp = dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id == id);
                temp.Name = property.Name;
                temp.Price = property.Price;
                temp.Description = property.Description;
                temp.Date = property.Date;
                temp.ExpirationDate = property.ExpirationDate;
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
            var currId = User.Identity.GetUserId();
            var propOwnerId = dBEntities.Properties.Find(id).UserId;
            if (String.Compare(currId, propOwnerId) == 0)
            {
                if(dBEntities.Properties.Find(id).ExpirationDate == null)
                {
                    ViewBag.stillInUse = true;
                }
                else
                {
                    ViewBag.stillInUse = false;
                }
                return View(dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id == id));
            }
            else
            {
                return HttpNotFound();
            }
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
