using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class MaterialTypesController : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();

        //
        // GET: /MaterialTypes/

        public ActionResult Index()
        {
            return View(db.MaterialTypes.ToList());
        }

        //
        // GET: /MaterialTypes/Details/5

        public ActionResult Details(Guid id )
        {
            MaterialType materialtype = db.MaterialTypes.Find(id);
            if (materialtype == null)
            {
                return HttpNotFound();
            }
            return View(materialtype);
        }

        //
        // GET: /MaterialTypes/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MaterialTypes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MaterialType materialtype)
        {
            if (ModelState.IsValid)
            {
                materialtype.MaterialTypeID = Guid.NewGuid();
                db.MaterialTypes.Add(materialtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(materialtype);
        }

        //
        // GET: /MaterialTypes/Edit/5

        public ActionResult Edit(Guid id)
        {
            MaterialType materialtype = db.MaterialTypes.Find(id);
            if (materialtype == null)
            {
                return HttpNotFound();
            }
            return View(materialtype);
        }

        //
        // POST: /MaterialTypes/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MaterialType materialtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materialtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(materialtype);
        }

        //
        // GET: /MaterialTypes/Delete/5

        public ActionResult Delete(Guid id)
        {
            MaterialType materialtype = db.MaterialTypes.Find(id);
            if (materialtype == null)
            {
                return HttpNotFound();
            }
            return View(materialtype);
        }

        //
        // POST: /MaterialTypes/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MaterialType materialtype = db.MaterialTypes.Find(id);
            db.MaterialTypes.Remove(materialtype);
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