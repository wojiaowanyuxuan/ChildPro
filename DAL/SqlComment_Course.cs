using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Models;

namespace DAL
{
    public class SqlComment_Course : IComment_Course
    {
        LessProEntities dbContext = new LessProEntities();
        public void AddComment(int userid, int courseid, string date, string content)
        {
            Comment_Course com = new Comment_Course()
            {
                User_Id = userid,
                Com_Content = content,
                Com_Date = date,
                Course_Id = courseid,
                Com_Praise_Num = 0,
            };
            try
            {
                dbContext.Comment_Course.Add(com);
                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
