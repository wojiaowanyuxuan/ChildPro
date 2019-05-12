using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Abstract;

namespace ChildPro.Controllers
{
    public class ForumController : Controller
    {
		private IPostRepository post_repository;
		private IUserInfoRepository user_repository;
		private LessProEntities lpe = new LessProEntities();
		// 解析依赖项获取接口实例
		public ForumController(IPostRepository rep1,IUserInfoRepository rep2)
		{
			post_repository = rep1;
			user_repository = rep2;
		}

        // GET: Forum
        public ActionResult Index()
        {
			return View();
        }

		[HttpGet]
		public ActionResult PostSummery()
		{
			//启用贪婪加载
			IEnumerable<Post> posts = lpe.Post.Include("User").OrderByDescending(e => e.Post_heat);
			return PartialView(posts);
		}

		[HttpPost]
		public ActionResult PostSummery(int sort_by,string title=null,string content=null,string date=null,string tag=null)
		{
			IEnumerable<Post> posts = null;
			if (sort_by == 1)
			{
				Post p = new Post()
				{
					User_Id = (int)Session["userid"],
					Post_Title = title,
					Post_Content = content,
					Post_Date = date,
					Post_Tag = tag,
					Post_heat = 1,
				};
				//存入数据库默认按照热度排序 
				post_repository.WritePost(p);		
				posts = lpe.Post.Include("User").OrderByDescending(e => e.Post_heat);
			}
			else if(sort_by == 2)
			{
				//按时间从最近开始排序
				posts = lpe.Post.Include("User").OrderByDescending(e => e.PostID);
			}
			else
			{
				posts = lpe.Post.Include("User").OrderByDescending(e => e.Post_heat);
			}
			
			return PartialView(posts);
		}

		public ActionResult PostDetails(int postid)
		{
			Post p = lpe.Post.Include("User").Where(e => e.PostID == postid).FirstOrDefault();
			return View(p);
		}
    }
}