using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Models;

namespace DAL
{
    public class SqlVideo : IVideo
    {
        LessProEntities dbContext = new LessProEntities();
        public IEnumerable<Video> GetVideo()
        {
            var videos = dbContext.Video.ToList();
            return (videos);
        }

        public IEnumerable<Video> GetClassById(int? id)
        {
            var classid = from v in dbContext.Video
                          where v.ClassId == id
                          select v;
            return (classid);
        }
    
        public IEnumerable<Video> GetVideoById(int? id)
        {
            var videoid = from v in dbContext.Video
                          where v.VideoID == id
                          select v;
            return (videoid);
        }
    }
}
