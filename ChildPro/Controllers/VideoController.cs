using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using ChildPro.Models;

namespace ChildPro.Controllers
{
    public class VideoController : Controller
    {
        LessProEntities dbContext = new LessProEntities();
        // GET: Video
        public ActionResult Index()
        {
            Video v = dbContext.Video.FirstOrDefault();
            return View(v);
        }
    }
}