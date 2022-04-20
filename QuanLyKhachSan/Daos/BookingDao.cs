using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Daos
{
    public class BookingDao
    {
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();

        public void Add(Booking booking)
        {
            myDb.bookings.Add(booking);
            myDb.SaveChanges();
        }

        public Booking CheckBooking(int idRoom)
        {
            return myDb.bookings.FirstOrDefault(x => x.idRoom == idRoom  && x.status != 2);
        }

        public List<Booking> GetBookingsByIdUser(int idUser)
        {
            return myDb.bookings.Where(x => x.idUser == idUser).ToList();
        }

        public Booking GetBookingById(int id)
        {
            return myDb.bookings.FirstOrDefault(x => x.idBooking == id);
        }

    }
}