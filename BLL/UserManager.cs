using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DALFactory;
using IDAL;

namespace BLL
{
    public class UserManager
    {
        IUser iuser = DataAccess.CreateUser();
        //关注的人
        public IEnumerable<View_Guanzhu> CountGuanzhu(int? uid)
        {
            var userA = iuser.CountGuanzhu(uid);
            return userA;
        }
        //关注我的人
        public IEnumerable<View_Guanzhu> CountGuanzhu1(int? uid)
        {
            var userB = iuser.CountGuanzhu1(uid);
            return userB;
        }

        //关注
        public void Guanzhu(Guanzhu us)
        {
            iuser.Guanzhu(us);
        }
        //取消关注
        public void QuxiaoGuanzhu(int? userA, int? userB)
        {
            iuser.QuxiaoGuanzhu(userA, userB);
        }
        public IEnumerable<User> IEGetUsersById(int? uid)
        {
            var user = iuser.IEGetUsersById(uid);
            return user;
        }
    }
}
