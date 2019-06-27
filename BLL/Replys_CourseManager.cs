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
    public class Replys_CourseManager
    {
        IReplys_Course irep = DataAccess.CreateReply();
        public void AddReply(int comid, int rep_userid, int rep_to_userid, string data, string content, int type)
        {
            irep.AddReply(comid, rep_userid, rep_to_userid, content, data, type);
        }
    }
}
