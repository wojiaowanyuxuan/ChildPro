using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IDAL
{
    public interface IUser
    {
        IEnumerable<User> IEGetUsersById(int? uid);
        IEnumerable<View_Guanzhu> CountGuanzhu(int? uid);
        IEnumerable<View_Guanzhu> CountGuanzhu1(int? uid);
        void Guanzhu(Guanzhu us);
        void QuxiaoGuanzhu(int? userA, int? userB);
    }
}
