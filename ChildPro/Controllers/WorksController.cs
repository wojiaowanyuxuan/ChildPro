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
    public class WorksController : Controller
    {
        // GET: Works
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}