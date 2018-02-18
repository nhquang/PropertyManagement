using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PropertyManagement.Models;

namespace PropertyManagement.Controllers
{
    public class ProblemController : Controller
    {
        PropManagementDBEntities dBEntities = new PropManagementDBEntities();
        // GET: Problem
        public ActionResult Index()
        {
            return View(dBEntities.Problems.ToList());
        }

        // GET: Problem/Details/5
        public ActionResult Details(int id)
        {
            return View(dBEntities.Problems.Include(c => c.Property).SingleOrDefault(t => t.Id == id));
        }

        // GET: Problem/Create
        public ActionResult Create()
        {
            ViewBag.list = dBEntities.Properties.ToList();

            return View();
        }

        // POST: Problem/Create
        [HttpPost]
        public ActionResult Create(Problem problem)
        {
            try
            {
                // TODO: Add insert logic here
                Problem temp = new Problem();
                temp.Name = problem.Name;
                temp.PropID = problem.PropID;
                temp.Description = problem.Description;
                temp.FixingDate = problem.FixingDate;
                temp.FixingPrice = problem.FixingPrice;
                temp.ReusingDate = problem.ReusingDate;
                temp.Property = dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id == problem.PropID);
                dBEntities.Problems.Add(temp);
                dBEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Problem/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.list = dBEntities.Properties.ToList();
            return View(dBEntities.Problems.Include(c => c.Property).SingleOrDefault(t => t.Id == id));
        }

        // POST: Problem/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Problem problem)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return RedirectToAction("edit", new { id = problem.Id });
                }

                if (id != problem.Id)
                {

                    return RedirectToAction("index");
                }
                // TODO: Add update logic here
                Problem o = dBEntities.Problems.Include(c => c.Property).SingleOrDefault(t => t.Id == id);
                
                o.Name = problem.Name;
                o.Description = problem.Description;
                o.FixingDate = problem.FixingDate;
                o.ReusingDate = problem.ReusingDate;
                o.FixingPrice = problem.FixingPrice;
                o.PropID = problem.PropID;
                o.Property = dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id == problem.PropID);
                dBEntities.SaveChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Problem/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dBEntities.Problems.Include(c => c.Property).SingleOrDefault(t => t.Id == id));
        }

        // POST: Problem/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var o = dBEntities.Problems.Include(c => c.Property).SingleOrDefault(t => t.Id == id);
                dBEntities.Problems.Remove(o);
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
