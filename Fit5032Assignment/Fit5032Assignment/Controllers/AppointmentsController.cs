using System;
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
    public class AppointmentsController : Controller
    {
        private Entities db = new Entities();
        private String userId;

        // GET: Appointments
        [Authorize]
        public ActionResult Index()
        {
            userId = User.Identity.GetUserId();
            var appointmentSet = db.AppointmentSet.Where(p => p.PatientPatient_id == userId).Include(a => a.Patient).Include(a => a.Doctor);
            if (User.IsInRole("Staff"))
            {
                appointmentSet = db.AppointmentSet.Include(a => a.Patient).Include(a => a.Doctor);
            }
            if(User.IsInRole("Doctor"))
            {
                appointmentSet = db.AppointmentSet.Where(d => d.DoctorDoctor_Id == userId).Include(a => a.Patient).Include(a => a.Doctor);
            }
            return View(appointmentSet.ToList());
        }

        [Authorize]
        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.AppointmentSet.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        [Authorize]
        // GET: Appointments/Create
        public ActionResult Create()
        {
            userId = User.Identity.GetUserId();
            var patient = db.PatientSet.Where(p => p.Patient_id == userId);
            ViewBag.PatientPatient_id = new SelectList(patient, "Patient_id", "F_name");
            if (User.IsInRole("Staff"))
            {
                ViewBag.PatientPatient_id = new SelectList(db.PatientSet, "Patient_id", "F_name");
            }
            ViewBag.DoctorDoctor_id = new SelectList(db.DoctorSet, "Doctor_id", "F_name");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Appointment_Id,Appointment_date,Appointment_time,PatientPatient_id,DoctorDoctor_Id")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.AppointmentSet.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatientPatient_id = new SelectList(db.PatientSet, "Patient_id", "F_name", appointment.PatientPatient_id);
            ViewBag.DoctorDoctor_Id = new SelectList(db.DoctorSet, "Doctor_Id", "F_name", appointment.DoctorDoctor_Id);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.AppointmentSet.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            userId = User.Identity.GetUserId();
            var patient = db.PatientSet.Where(p => p.Patient_id == userId);
            ViewBag.PatientPatient_id = new SelectList(patient, "Patient_id", "F_name", appointment.PatientPatient_id);
            if (User.IsInRole("Staff") || User.IsInRole("Doctor"))
            {
                ViewBag.PatientPatient_id = new SelectList(db.PatientSet, "Patient_id", "F_name", appointment.PatientPatient_id);
            }
            ViewBag.DoctorDoctor_Id = new SelectList(db.DoctorSet, "Doctor_Id", "F_name", appointment.DoctorDoctor_Id);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Appointment_Id,Appointment_date,Appointment_time,PatientPatient_id,DoctorDoctor_Id")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientPatient_id = new SelectList(db.PatientSet, "Patient_id", "F_name", appointment.PatientPatient_id);
            ViewBag.DoctorDoctor_Id = new SelectList(db.DoctorSet, "Doctor_Id", "F_name", appointment.DoctorDoctor_Id);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.AppointmentSet.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.AppointmentSet.Find(id);
            db.AppointmentSet.Remove(appointment);
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
