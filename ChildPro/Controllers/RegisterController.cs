using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Abstract;

namespace ChildPro.Controllers
{
    public class RegisterController : Controller
    {
		private IUserInfoRepository UIRepository;
		public RegisterController(IUserInfoRepository rep)
		{
			UIRepository = rep;
		}
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
    }
}