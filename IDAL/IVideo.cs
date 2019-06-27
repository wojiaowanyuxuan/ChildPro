using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IDAL
{
    public interface IVideo
    {
        IEnumerable<Video> GetVideo();
        IEnumerable<Video> GetVideoById(int? id);
        IEnumerable<Video> GetClassById(int? id);
    }
}
