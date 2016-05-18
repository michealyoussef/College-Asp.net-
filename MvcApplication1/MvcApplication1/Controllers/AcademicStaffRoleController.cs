using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class AcademicStaffRoleController : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();

        //
        // GET: /AcademicStaffRole/

        public ActionResult Index()
        {
            return View(db.AcademicStaffRoles.ToList());
        }


        public ActionResult GetStaff(Guid id)
        {
            AcademicStaffRole academicstaffrole = db.AcademicStaffRoles.Find(id);
            return RedirectToAction("academicstaffroleindex", "AcademicStaff", academicstaffrole);
        }


        //
        // GET: /AcademicStaffRole/Details/5

        public ActionResult Details(Guid id)
        {
            AcademicStaffRole academicstaffrole = db.AcademicStaffRoles.Find(id);
            if (academicstaffrole == null)
            {
                return HttpNotFound();
            }
            return View(academicstaffrole);
        }

        //
        // GET: /AcademicStaffRole/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AcademicStaffRole/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AcademicStaffRole academicstaffrole)
        {
            if (ModelState.IsValid)
            {
                academicstaffrole.RoleID = Guid.NewGuid();
                db.AcademicStaffRoles.Add(academicstaffrole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(academicstaffrole);
        }

        //
        // GET: /AcademicStaffRole/Edit/5

        public ActionResult Edit(Guid id)
        {
            AcademicStaffRole academicstaffrole = db.AcademicStaffRoles.Find(id);
            if (academicstaffrole == null)
            {
                return HttpNotFound();
            }
            return View(academicstaffrole);
        }

        //
        // POST: /AcademicStaffRole/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AcademicStaffRole academicstaffrole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academicstaffrole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(academicstaffrole);
        }

        //
        // GET: /AcademicStaffRole/Delete/5

        public ActionResult Delete(Guid id)
        {
            AcademicStaffRole academicstaffrole = db.AcademicStaffRoles.Find(id);
            if (academicstaffrole == null)
            {
                return HttpNotFound();
            }
            return View(academicstaffrole);
        }

        //
        // POST: /AcademicStaffRole/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AcademicStaffRole academicstaffrole = db.AcademicStaffRoles.Find(id);
            db.AcademicStaffRoles.Remove(academicstaffrole);
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