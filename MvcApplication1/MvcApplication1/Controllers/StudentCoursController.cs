using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class StudentCoursController : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();

        //
        // GET: /StudentCours/

        public ActionResult Index()
        {
            var studentcourses = db.StudentCourses.Include(s => s.Cours).Include(s => s.Student);
            return View(studentcourses.ToList());
        }

        //
        // GET: /StudentCours/Details/5

        public ActionResult Details(Guid id, Guid Yer)
        {
            List<StudentCours> studentcours = db.StudentCourses.Where(H => H.StudentID == (id) && H.CourseID == (Yer)).ToList();
            if (studentcours == null)
            {
                return HttpNotFound();
            }
            return View(studentcours[0]);
        }

        //
        // GET: /StudentCours/Create

        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name");
            return View();
        }

        //
        // POST: /StudentCours/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentCours studentcours)
        {
            if (ModelState.IsValid)
            {
                //studentcours.StudentID = Guid.NewGuid();
                db.StudentCourses.Add(studentcours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", studentcours.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentcours.StudentID);
            return View(studentcours);
        }

        //
        // GET: /StudentCours/Edit/5

        public ActionResult Edit(Guid id, Guid Yer)
        {
            StudentCours studentcours = db.StudentCourses.Where(H => H.StudentID == (id) && H.CourseID == (Yer)).ToList()[0];

            if (studentcours == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", studentcours.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentcours.StudentID);
            return View(studentcours);
        }

        //
        // POST: /StudentCours/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentCours studentcours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentcours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", studentcours.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", studentcours.StudentID);
            return View(studentcours);
        }

        //
        // GET: /StudentCours/Delete/5

        public ActionResult Delete(Guid id, Guid Yer)
        {
            StudentCours studentcours  = db.StudentCourses.Where(H => H.StudentID == (id) && H.CourseID == (Yer)).ToList()[0];

            if (studentcours == null)
            {
                return HttpNotFound();
            }
            return View(studentcours);
        }

        //
        // POST: /StudentCours/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            StudentCours studentcours = db.StudentCourses.Find(id);
            db.StudentCourses.Remove(studentcours);
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