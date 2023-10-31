using DoAnNhom1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnNhom1.Controllers
{
    public class UserController : Controller
    {
        GREntities2 db = new GREntities2();
        // GET: User
        public ActionResult Index()
        {
            if (Session["NameUser"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else

            {
                return View(db.AdminUser.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AdminUser user)
        {
            try
            {
                db.AdminUser.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error");
            }
        }
        public ActionResult Edit(int id)
        {
            return View(db.AdminUser.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, AdminUser user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["nofi"] = "Update Success";
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var admin = db.AdminUser.Where(s => s.ID == id).FirstOrDefault();
            if (admin == null)
            {
                return HttpNotFound();
            }
            db.Entry(admin).State = EntityState.Deleted;
            if (db.SaveChanges() > 0)
            {
                TempData["nofi"] = "User Deleted";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}