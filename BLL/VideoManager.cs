using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using IDAL;
using DALFactory;


namespace BLL
{
    public class VideoManager
    {
        IVideo ivideo = DataAccess.CreateVideo();

        public IEnumerable<Video> GetVideo()
        {
            var videos = ivideo.GetVideo();
            return (videos);
        }

        //通过classid来获取视频
        public IEnumerable<Video> GetClassById(int? id)
        {
            var classid = ivideo.GetClassById(id);
            return (classid);
        }

        public IEnumerable<Video> GetVideoById(int? id)
        {
            var videoid = ivideo.GetVideoById(id);
            return (videoid);
        }
    }
}
