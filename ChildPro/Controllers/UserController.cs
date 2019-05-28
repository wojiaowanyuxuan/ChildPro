using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Abstract;
using ChildPro.Models;

namespace ChildPro.Controllers
{
    public class UserController : Controller
    {
		private IUserInfoRepository user_repository;
		private IFollowRepository follow_repository;
		private LessProEntities lpe = new LessProEntities();
		//解析依赖项 
		public UserController(IUserInfoRepository rep1,IFollowRepository rep2)
		{
			user_repository = rep1;
			follow_repository = rep2;
		}

        // GET: User
        public ActionResult Index(int userid)
        {
			//当前登录用户的id 
			bool isOwner = false;
			bool isAttention = false;
			int now_userid = (int)Session["userid"];
			if (now_userid == userid)
				isOwner = true;

			User u = lpe.User.Where(e => e.UserID == userid).FirstOrDefault();
			//用户发布过的帖子 
			IEnumerable<Post> pub_posts = lpe.Post.Include("User").Where(e=>e.Accessibility==3).Where(e => e.User_Id == userid);
			//用户评论过的帖子
			IEnumerable<Comment> coms = lpe.Comment.Where(e => e.User_Id == userid);
			//非用户发布帖子 评论过的帖子不包含自己发布的帖子
			IEnumerable<Post> pubno_posts = lpe.Post.Where(e => e.User_Id != userid);
			var query_1 = (from c in coms
						 join P in pubno_posts
						 on c.Post_Id equals P.PostID
						 select P).ToList();
			IEnumerable<Post> com_posts = query_1;

			//用户收藏的帖子 也不包含自己发布的帖子
			IEnumerable<Collect> colls = lpe.Collect.Where(e => e.User_Id == userid);
			var query_2 = (from c in colls
						   join P in pubno_posts
						   on c.Post_Id equals P.PostID
						   select P).ToList();
			IEnumerable<Post> coll_posts = query_2;

			//判断 当前登陆用户是否关注了访问的用户
			Follow f = lpe.Follow.Where(e => (e.Followed_Person == userid && e.Fans == now_userid)).FirstOrDefault();
			if (f != null)
			{
				isAttention = true;
			}
			userViewModel uvm = new userViewModel()
			{
				u=u,
				IsOwner=isOwner,
				IsAttention=isAttention,
				pub_posts = pub_posts,
				com_posts = com_posts,
				coll_posts = coll_posts,
				All_Fans=follow_repository.GetAllFans(userid),
				All_Attention=follow_repository.GetAllAttention(userid)
			};
            return View(uvm);
        }

		[HttpPost]
		public JsonResult Attention(int Followed_Userid,int tag)
		{
			tip t = null;
			int fans_id = (int)Session["userid"];
			if (tag == 1)
			{
				//关注
				follow_repository.AddAttention(Followed_Userid, fans_id);
				t = new tip()
				{
					message = "关注成功",
					code = 200
				};
			}
			else
			{
				Follow f = lpe.Follow.Where(e => (e.Followed_Person == Followed_Userid && e.Fans == fans_id)).FirstOrDefault();
				lpe.Follow.Remove(f);
				t = new tip()
				{
					message = "取消关注成功",
					code = 200
				};
			}
			return base.Json(t);
		}

		//信息设置
		[HttpGet]
		public ActionResult UserSetting()
		{
			int userid = (int)Session["userid"];
			User u = lpe.User.Find(userid);
			return View(u);
		}

		[HttpPost]
		public JsonResult UserSetting(string username=null,string phone=null,string signature=null,string birthday=null,string sex =null)
		{
			int userid = (int)Session["userid"];
			tip t = null;
			try
			{
				user_repository.SaveEditor(userid, username, phone, signature, birthday, sex);
				t = new tip()
				{
					message = "编辑成功",
					code = 200
				};
			}
			catch(Exception e)
			{
				throw e;
			}
			return base.Json(t);
		}

		//上传头像
		[HttpPost]
		public JsonResult UploadHeaderPic(HttpPostedFileBase file = null)
		{
			int userid = (int)Session["userid"];
			User u = lpe.User.Find(userid);
			tip t = null;
			if(file!=null && u != null)
			{
				u.ImageData = new byte[file.ContentLength];
				u.ImageMineType = file.ContentType;
				file.InputStream.Read(u.ImageData, 0, file.ContentLength);
				try
				{
					lpe.SaveChanges();
					t = new tip()
					{
						message = "上传成功",
						code = 200
					};
				}
				catch(Exception e)
				{
					throw e;
				}
			}
			else
			{
				t = new tip()
				{
					message = "上传失败",
					code = 200
				};
			}
			return base.Json(t);
			
		}

		//获取自定义头像
		public FileContentResult GetImage(int userid)
		{
			User u = user_repository.Users.Where(e => e.UserID == userid).FirstOrDefault();
			if (u != null)
			{
				//将二进制数据转换为图像
				return File(u.ImageData, u.ImageMineType);
			}
			else
			{
				return null;
			}
		}


    }
}