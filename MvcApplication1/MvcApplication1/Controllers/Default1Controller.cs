using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class Default1Controller : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            var studentgrades = db.StudentGrades.Include(s => s.CourseGrade).Include(s => s.Student);
            return View(studentgrades.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(Guid id )
        {
            StudentGrade studentgrade = db.StudentGrades.Find(id);
            if (studentgrade == null)
            {
                return HttpNotFound();
            }
            return View(studentgrade);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            ViewBag.GradeID = new SelectList(db.CourseGrades, "GradeID", "Name");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name");
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentGrade studentgrade)
        {
            if (ModelState.IsValid)
            {
                studentgrade.StudentID = Guid.NewGuid();
                db.StudentGrades.Add(studentgrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GradeID = new SelectList(db.CourseGrades, "GradeID", "Name", studentgrade.GradeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentgrade.StudentID);
            return View(studentgrade);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(Guid id)
        {
            StudentGrade studentgrade = db.StudentGrades.Find(id);
            if (studentgrade == null)
            {
                return HttpNotFound();
            }
            ViewBag.GradeID = new SelectList(db.CourseGrades, "GradeID", "Name", studentgrade.GradeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentgrade.StudentID);
            return View(studentgrade);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentGrade studentgrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentgrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GradeID = new SelectList(db.CourseGrades, "GradeID", "Name", studentgrade.GradeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentgrade.StudentID);
            return View(studentgrade);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(Guid id)
        {
            StudentGrade studentgrade = db.StudentGrades.Find(id);
            if (studentgrade == null)
            {
                return HttpNotFound();
            }
            return View(studentgrade);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            StudentGrade studentgrade = db.StudentGrades.Find(id);
            db.StudentGrades.Remove(studentgrade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}