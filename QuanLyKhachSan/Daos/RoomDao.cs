
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Daos
{
    public class RoomDao
    {
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();


        public List<Room> GetRooms()
        {
            return myDb.rooms.ToList();
        }

        public List<Room> GetRoomTop5()
        {
            return myDb.rooms.OrderByDescending(x => x.view).Take(5).ToList();
        }

        public List<Room> GetRoomDiscount()
        {
            return myDb.rooms.OrderByDescending(x => x.discount).Take(5).ToList();
        }

        public Room GetDetail(int id)
        {
            return myDb.rooms.FirstOrDefault(x => x.idRoom == id);
        }

        public List<Room> GetRoomByType(int typeId)
        {
            return myDb.rooms.Where(x => x.idType == typeId).ToList();
        }

        public List<Room> GetRoomsBlank(int page, int pagesize)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status != 2).Select(x => x.idRoom).ToList();
            return myDb.rooms.Where(x => !arrIdRoom.Contains(x.idRoom)).ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }
        public int GetNumberRoom()
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status != 2).Select(x => x.idRoom).ToList();
            int total = myDb.rooms.Where(x => !arrIdRoom.Contains(x.idRoom)).ToList().Count;
            int count = 0;
            count = total / 10;
            if (total % 10 != 0)
            {
                count++;
            }
            return count;
        }
    }
}