using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PropertyManagement.Models;

namespace PropertyManagement.Controllers
{
    public class ProblemsController : Controller
    {
        PropertyManagementDB o = new PropertyManagementDB();
        
        // GET: Problems
        public ActionResult Index()
        {
            return View(o.Problems.ToList());
        }

        // GET: Problems/Details/5
        public ActionResult Details(int id)
        {
            return View(o.Problems.Include(c => c.Property).SingleOrDefault(t => t.Id == id));
        }

        // GET: Problems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Problems/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Problems/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Problems/Edit/5
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

        // GET: Problems/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Problems/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
