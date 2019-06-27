using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Models;

namespace DAL
{
    public class SqlReplys_Course : IReplys_Course
    {
        LessProEntities dbContext = new LessProEntities();
        public void AddReply(int comid, int rep_userid, int rep_to_userid, string data, string content, int type)
        {
            Replys_Course rep = new Replys_Course()
            {
                Com_Id_Course = comid,
                Rep_Content = content,
                Rep_UserId = rep_userid,
                Rep_To_UserId = rep_to_userid,
                Rep_Date = data,
                Rep_Type = type
            };
            try
            {
                dbContext.Replys_Course.Add(rep);
                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
       }
    }
}
