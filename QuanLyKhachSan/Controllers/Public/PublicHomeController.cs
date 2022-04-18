using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Public
{
    public class PublicHomeController : Controller
    {
        // GET: PublicHome
        public ActionResult Index()
        {
            return View();
        }
    }
}