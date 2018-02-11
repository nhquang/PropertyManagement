﻿using System;
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
            return View();
        }

        // POST: Problem/Create
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

        // GET: Problem/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Problem/Edit/5
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

        // GET: Problem/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Problem/Delete/5
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