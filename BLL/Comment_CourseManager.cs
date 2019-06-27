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
     public class Comment_CourseManager
    {
        IComment_Course icom = DataAccess.CreateComment();
        public void AddComment(int userid, int courseid, string date, string content)
        {
            icom.AddComment(userid, courseid, date, content);
        }
    }
}
