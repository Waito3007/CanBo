using DoAnNhom1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnNhom1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        GREntities2 db = new GREntities2();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AdminUser user)
        {
            var check = db.AdminUser.Where(s => s.NameUser == user.NameUser && s.PasswordUser == user.PasswordUser && s.RoleUser == "Admin").FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai Info hoặc bạn không có quyền vào trang này";
                return View("Index");

            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["NameUser"] = user.NameUser;
                Session["PasswordUser"] = user.PasswordUser;
                return RedirectToAction("Admin");
            }
        }
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Admin()
        {
            if (Session["NameUser"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else

            {
                return View();
            }
        }
    }

}