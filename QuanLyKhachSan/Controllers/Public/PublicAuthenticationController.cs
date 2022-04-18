using QuanLyKhachSan.Daos;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Public
{
    public class PublicAuthenticationController : Controller
    {
        // GET: PublicAuthentication
        UserDao userDao = new UserDao();
        // GET: AdminAuthentication
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            User user = new User()
            {
                userName = form["email"],
                password = form["password"]
            };
            string passwordMd5 = userDao.md5(form["password"]);
            bool checkLogin = userDao.checkLogin(user.userName, passwordMd5);
            if (checkLogin)
            {
                var userInformation = userDao.getUserByUserName(user.userName);             
                    Session.Add("USER", userInformation);
                return RedirectToAction("Index", "PublicHome");
            }
            else
            {
                ViewBag.mess = "Thông tin tài khoản hoặc mật khẩu không chính xác";
                return View("Login");
            }

        }

        [HttpPost]
        public ActionResult Register(User user)
        {

            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("User");
            return Redirect("/PublicHome/Index");
        }
    }
}