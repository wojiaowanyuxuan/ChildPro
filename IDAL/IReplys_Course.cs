using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IDAL
{
    public interface IReplys_Course
    {
        void AddReply(int comid, int rep_userid, int rep_to_userid, string data, string content, int type);
    }
}
