using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Daos
{
    public class ServiceDao
    {
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();

        public List<Service> GetServices()
        {
            return myDb.services.ToList();
        }

        public List<Service> GetServicesTop5()
        {
            return myDb.services.Take(5).ToList();
        }

        public int GetCostById(int id)
        {
            return myDb.services.FirstOrDefault(x => x.idService == id).cost;
        }

    }
}