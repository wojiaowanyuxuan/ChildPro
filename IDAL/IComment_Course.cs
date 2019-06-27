using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IDAL
{
    public interface IComment_Course
    {
        void AddComment(int userid, int courseid, string date, string content);

    }
}
