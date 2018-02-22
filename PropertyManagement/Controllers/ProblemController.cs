using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PropertyManagement.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace PropertyManagement.Controllers
{
    public class ProblemController : Controller
    {
        PropManagementDBEntities dBEntities = new PropManagementDBEntities();
        // GET: Problem
        public ActionResult Index()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                var currId = User.Identity.GetUserId();
                var props = dBEntities.Properties.Where(c => c.UserId == currId).ToList();
                var probs = new List<Problem>();
                foreach(var item in props)
                {
                    foreach(var item2 in item.Problems)
                    {
                        probs.Add(item2);
                    }
                }
                return View(probs);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // GET: Problem/Details/5
        public ActionResult Details(int id)
        {
            var currId = User.Identity.GetUserId();
            var problemOwnerId = dBEntities.Problems.Find(id).Property.UserId;
            if (String.Compare(currId, problemOwnerId) == 0)
            {

                return View(dBEntities.Problems.Include(c => c.Property).SingleOrDefault(t => t.Id == id));
            }
            else
            {
                return HttpNotFound();
            }
        }

        // GET: Problem/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currOwner = User.Identity.GetUserId();
                ViewBag.list = dBEntities.Properties.Where(c => c.UserId == currOwner).ToList();
                
                return View();
            }
            else
            {
                return HttpNotFound();
            }
            
        }

        // POST: Problem/Create
        [HttpPost]
        public ActionResult Create(Problem problem)
        {
            try
            {
                // TODO: Add insert logic here
                var temp = new Problem();
                temp.Name = problem.Name;
                temp.FixingPrice = problem.FixingPrice;
                temp.Description = problem.Description;
                temp.FixingDate = problem.FixingDate;
                temp.ReusingDate = problem.ReusingDate;
                temp.PropID = problem.PropID;
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
            var problemOwnerId = dBEntities.Problems.Include(c => c.Property).SingleOrDefault(t => t.Id == id).Property.UserId;
            var currUserId = User.Identity.GetUserId();
            if(String.Compare(problemOwnerId,currUserId) == 0)
            {
                ViewBag.list = dBEntities.Properties.Where(c => c.UserId == currUserId).ToList();
                return View(dBEntities.Problems.Include(c => c.Property).SingleOrDefault(t => t.Id == id));
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Problem/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Problem problem)
        {
            try
            {
                // TODO: Add update logic here
                var temp = dBEntities.Problems.Include(c => c.Property).SingleOrDefault(t => t.Id == id);
                temp.Name = problem.Name;
                temp.Description = problem.Description;
                temp.FixingPrice = problem.FixingPrice;
                temp.FixingDate = problem.FixingDate;
                temp.ReusingDate = problem.ReusingDate;
                temp.PropID = problem.PropID;
                temp.Property = dBEntities.Properties.Include(c => c.Problems).SingleOrDefault(t => t.Id == problem.PropID);
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
            var currId = User.Identity.GetUserId();
            var problemOwnerId = dBEntities.Problems.Find(id).Property.UserId;
            if (String.Compare(currId, problemOwnerId) == 0)
            {

                return View(dBEntities.Problems.Include(c => c.Property).SingleOrDefault(t => t.Id == id));
            }
            else
            {
                return HttpNotFound();
            }
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
