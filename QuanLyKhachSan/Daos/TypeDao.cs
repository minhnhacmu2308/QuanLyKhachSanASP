using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Daos
{
    public class TypeDao
    {
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();

        public List<QuanLyKhachSan.Models.Type> GetTypes()
        {
            return myDb.types.ToList();
        }
    }
}