using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Daos
{
    public class UserDao
    {

        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();
        public bool checkLogin(string userName, string password)
        {
            var obj = myDb.users.FirstOrDefault(x => x.userName == userName && x.password == password);
            if (obj == null) { return false; }
            return true;
        }

        public User getUserByUserName(string userName)
        {
            return myDb.users.FirstOrDefault(x => x.userName.Equals(userName));
        }

        public User getInfor(int id)
        {
            return myDb.users.FirstOrDefault(x => x.idUser == id);
        }

        public List<User> getListEmployee() { return myDb.users.Where(x => x.idRole == 1).ToList(); }

        public void add(User user)
        {
            myDb.users.Add(user);
            myDb.SaveChanges();
        }

        public bool checkExistUsername(string userName)
        {
            var obj = myDb.users.FirstOrDefault(x => x.userName == userName);
            if (obj != null) { return true; }
            return false;
        }

        public void delete(int id)
        {
            var obj = myDb.users.FirstOrDefault(x => x.idUser == id);
            myDb.users.Remove(obj);
            myDb.SaveChanges();
        }

        public void update(User user)
        {
            var obj = myDb.users.FirstOrDefault(x => x.idUser == user.idUser);
            obj.fullName = user.fullName;
            obj.userName = user.userName;
            obj.password = user.password;
            obj.address = user.address;
            obj.phoneNumber = user.phoneNumber;
            obj.gender = user.gender;
            myDb.SaveChanges();
        }
    }
}