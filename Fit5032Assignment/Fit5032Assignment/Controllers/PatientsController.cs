﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fit5032Assignment.Models;
using Microsoft.AspNet.Identity;

namespace Fit5032Assignment.Controllers
{
    public class PatientsController : Controller
    {
        private Entities db = new Entities();

        // GET: Patients
        public ActionResult Index()
        {
            return View(db.PatientSet.ToList());
        }

        // GET: Patients/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.PatientSet.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            Patient patient = new Patient();
            patient.Patient_id = User.Identity.GetUserId();
            return View(patient);
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Patient_id,F_name,L_name,PhoneNumber,Birthday")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                AspNetUserRoles aspNetUserRoles = new AspNetUserRoles();
                aspNetUserRoles.UserId = User.Identity.GetUserId();
                aspNetUserRoles.RoleId = "3";
                db.AspNetUserRoles.Add(aspNetUserRoles);
                db.PatientSet.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Login","Account");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.PatientSet.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Patient_id,F_name,L_name,PhoneNumber,Birthday")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.PatientSet.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Patient patient = db.PatientSet.Find(id);
            db.PatientSet.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
