using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using IDAL;

namespace DAL
{
    public class SqlUser : IUser
    {
        LessProEntities dbContext = new LessProEntities();
        //关注的人
        public IEnumerable<View_Guanzhu> CountGuanzhu(int? uid)
        {
            var userA = from u in dbContext.View_Guanzhu
                        where u.UserA == uid
                        select u;
            return userA;
        }
        //关注我的人
        public IEnumerable<View_Guanzhu> CountGuanzhu1(int? uid)
        {
            var userB = from u in dbContext.View_Guanzhu
                        where u.UserB == uid
                        select u;
            return userB;
        }

        //关注
        public void Guanzhu(Guanzhu us)
        {
            dbContext.Guanzhu.Add(us);
            dbContext.SaveChanges();
        }

        public IEnumerable<User> IEGetUsersById(int? uid)
        {
            var user = dbContext.User.Where(c => c.UserID == uid);
            return user;
        }

        public void QuxiaoGuanzhu(int? userA, int? userB)
        {
            var us = from u in dbContext.Guanzhu
                       where u.UserA == userA && u.UserB == userB
                       select u;
            dbContext.Guanzhu.Remove(us.FirstOrDefault());
            dbContext.SaveChanges();
        }
    }
}
