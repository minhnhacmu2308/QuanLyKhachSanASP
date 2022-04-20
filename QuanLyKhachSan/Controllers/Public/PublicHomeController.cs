using QuanLyKhachSan.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Public
{
    public class PublicHomeController : Controller
    {
        RoomDao roomDao = new RoomDao();
        ServiceDao serviceDao = new ServiceDao();
        // GET: PublicHome
        public ActionResult Index()
        {
            ViewBag.ListRoomTop5 = roomDao.GetRoomTop5();
            ViewBag.ListRoomDiscount = roomDao.GetRoomDiscount();
            ViewBag.ListService = serviceDao.GetServicesTop5();
            ViewBag.active = "home";
            return View();
        }
    }
}