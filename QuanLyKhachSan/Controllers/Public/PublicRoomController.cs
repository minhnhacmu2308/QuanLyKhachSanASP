﻿using QuanLyKhachSan.Daos;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Public
{
    public class PublicRoomController : Controller
    {
        RoomDao roomDao = new RoomDao();
        ServiceDao serviceDao = new ServiceDao();
        BookingDao bookingDao = new BookingDao();
        BookingServiceDao bookingServiceDao = new BookingServiceDao();
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();
        // GET: PublicRoom
        public ActionResult Index(int page)
        {
            if (page == 0)
            {
                page = 1;
            }
            ViewBag.List = roomDao.GetRoomsBlank(page, 10);
            ViewBag.tag = page;
            ViewBag.pageSize = roomDao.GetNumberRoom();
            return View();
        }

        public ActionResult DetailRoom(int id,string mess)
        {
            ViewBag.mess = mess;
            Room obj = roomDao.GetDetail(id);
            obj.view = obj.view + 1;
            myDb.SaveChanges();
            ViewBag.Room = obj;
            ViewBag.ListService = serviceDao.GetServices();
            ViewBag.ListRoomRelated = roomDao.GetRoomByType(obj.idType);
            return View();
        }

        [HttpPost]
        public ActionResult Booking(Booking booking,int[] idService)
        {
            User user = (User)Session["USER"];
            string action = "DetailRoom/" + booking.idRoom;
            if (user == null)
            {              
                return RedirectToAction(action, new { mess = "ErrorLogin" });
            }
            else
            {
                Booking checkExist = bookingDao.CheckBooking(booking.idRoom);
                int priceService = 0;
                if (idService != null)
                {                 
                    for (int i = 0; i < idService.Count(); i++)
                    {

                        priceService += serviceDao.GetCostById(idService[i]);
                    }
                }
                
                if (checkExist == null || (checkExist != null && DateTime.Now > DateTime.Parse(checkExist.checkOutDate)))
                {
                    DateTime dateCheckout = DateTime.Parse(checkExist.checkOutDate);
                    int numberBooking = DateTime.Now.Day - dateCheckout.Day;
                    Room room = roomDao.GetDetail(booking.idRoom);
                    booking.idUser = user.idUser;
                    booking.createdDate = DateTime.Now;
                    booking.isPayment = false;
                    booking.status = 0;
                    booking.totalMoney = (room.cost * numberBooking - room.cost * numberBooking * room.discount / 100) + priceService;
                    bookingDao.Add(booking);
                    if(idService != null)
                    {
                       for(int i = 0; i < idService.Count(); i++)
                       {
                            BookingService obj = new BookingService
                            {
                                idService = idService[i],
                                idBooking = booking.idBooking
                            };
                            bookingServiceDao.Add(obj);
                       }
                    }
                    return RedirectToAction(action, new { mess = "Success" });
                }
                else
                {
                    return RedirectToAction(action, new { mess = "ErrorExist" });
                }
            }
        }
              
        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            string name = form["name"];
            int idType = Int32.Parse(form["idType"]);
            return RedirectToAction("Search", new { page = 0, name = name, idType = idType });
        }

        [HttpGet]
        public ActionResult Search(int page,string name , int idType)
        {
            if (page == 0)
            {
                page = 1;
            }
            if (name == "" && idType != 0)
            {
                ViewBag.List = roomDao.SearchByType(page, 2, idType);
                ViewBag.tag = page;
                ViewBag.key = 1;
                ViewBag.idType = idType;
                ViewBag.pageSize = roomDao.GetNumberRoomByType(idType);
            }
            else if(name != "" && idType == 0)
            {
                ViewBag.List = roomDao.SearchByName(page, 2, name);
                ViewBag.tag = page;
                ViewBag.key = 2;
                ViewBag.name = name;
                ViewBag.pageSize = roomDao.GetNumberRoomByName(name);
            } else if (name != "" && idType != 0)
            {
                ViewBag.List = roomDao.SearchByTypeAndName(page, 2,idType, name);
                ViewBag.tag = page;
                ViewBag.key = 3;
                ViewBag.name = name;
                ViewBag.idType = idType;
                ViewBag.pageSize = roomDao.GetNumberRoomByNameAndType(name,idType);
            }
            else if (name == "" && idType == 0)
            {
                RedirectToAction("Index", "PublicHome");
            }
            return View();
        }
    }
}