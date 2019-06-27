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
    public class ForumController : Controller
    {
		private IPostRepository post_repository;
		private IUserInfoRepository user_repository;
		private ICommentRepository comment_repository;
		private IReplyRepository reply_repository;
		private IPraisedRepository praised_repository;
		private LessProEntities lpe = new LessProEntities();
		// 解析依赖项获取接口实例
		public ForumController(IPostRepository rep1,IUserInfoRepository rep2,ICommentRepository rep3,IReplyRepository rep4,IPraisedRepository rep5)
		{
			post_repository = rep1;
			user_repository = rep2;
			comment_repository = rep3;
			reply_repository = rep4;
			praised_repository = rep5;
		}

        // GET: Forum
        public ActionResult Index()
        {
			if (Session["userid"] != null)
			{
				ViewBag.userid = (int)Session["userid"];
			}
			return View();
        }

		[HttpGet]
		public ActionResult PostSummery()
		{
			//普通用户只能看到accessibility为3的帖子
			IEnumerable<Post> posts= lpe.Post.Include("User").Where(e=>e.Accessibility==3).OrderByDescending(e => e.Post_heat);
			//启用贪婪加载
			if (Session["admin"] != null)
			{
				//管理员能看到所有帖子
				posts = lpe.Post.Include("User").OrderByDescending(e => e.Post_heat);
			}
			AdminPermission ap = new AdminPermission()
			{
				posts = posts,
				a = (Admin)Session["admin"]
			};
			return PartialView(ap);
		}

		[HttpPost]
		public ActionResult PostSummery(int sort_by,int? post_type,string title=null,string content=null,string date=null,string tag=null)
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
					Post_Type=post_type,
					//初始发布的帖子 可访问程度为3
					Accessibility=3
				};
				//存入数据库默认按照热度排序 
				post_repository.WritePost(p);		
				posts = lpe.Post.Include("User").OrderByDescending(e => e.Post_heat);
			}
			else if(sort_by == 2)
			{
				//按时间从最近开始排序
				posts = lpe.Post.Include("User").Where(e=>e.Accessibility==3).OrderByDescending(e => e.PostID);
			}
			else
			{
				posts = lpe.Post.Include("User").Where(e=>e.Accessibility==3).OrderByDescending(e => e.Post_heat);
			}
			//管理员看到的帖子均按照时间排列
			if (Session["admin"] != null)
				posts = lpe.Post.Include("User").OrderByDescending(e => e.PostID);
			AdminPermission ap = new AdminPermission()
			{
				posts = posts,
				a = null
			};
			return PartialView(ap);
		}

		public ActionResult PostDetails(int postid)
		{
			int userid = 0;
			if (Session["userid"] != null)
			{
			    userid = (int)Session["userid"];
			}
			bool coll = false;
			Post p = lpe.Post.Include("User").Where(e => e.PostID == postid).FirstOrDefault();
			//增加帖子热度
			p.Post_heat += 10;
			lpe.SaveChanges();
			Collect c = lpe.Collect.Where(e => (e.User_Id == userid && e.Post_Id == postid)).FirstOrDefault();
			if (c != null)
			{
				//帖子已被收藏
				coll = true;
			}
			PostViewModel PVM = new PostViewModel()
			{
				p = p,
				isC = coll
			};
			return View(PVM);
		}

		//展示所有评论回复
		[HttpGet]
		public ActionResult CommentSummery(int postid)
		{
			if (Session["admin"] != null)
			{
				ViewBag.admin = (Admin)Session["admin"];
			}
			IEnumerable<ViewModel> vmss = GetComAndRep(postid);
			return PartialView(vmss);
		}

		//对帖子的评论回复
		[HttpPost]
		public ActionResult CommentSummery(int Com_type,int postid,int? rep_to_userid,int? Com_id,string Com_content=null,string date=null)
		{
			int userid = 0;
			if (Session["userid"] != null)
			{
				userid = (int)Session["userid"];
			}
			if (Session["admin"] != null)
			{
				ViewBag.admin = (Admin)Session["admin"];
			}
			// Com_type==1表示对帖子的评论 Com_type==2表示对楼主的回复 Com_type==3表示对其他人的回复
			switch (Com_type)
			{
				case 1:
					comment_repository.AddComment(userid,postid, date, Com_content);
					break;
				case 2:
					reply_repository.AddReply((int)Com_id, userid, (int)rep_to_userid, date, Com_content, 1);
					break;
				case 3:
					reply_repository.AddReply((int)Com_id, userid, (int)rep_to_userid, date, Com_content, 2);
					break;
				case 4:
					//删除评论逻辑
					comment_repository.RemoveComment((int)Com_id);
					break;
				default:
					break;
					
			}
			IEnumerable<ViewModel> vmss = GetComAndRep(postid);
			return PartialView(vmss.Reverse());
		}

		//点赞
		[HttpPost]
		public JsonResult dop(int creNum,int? Comid,int? Repid)
		{
			tip t;
			int userid = (int)Session["userid"];
			//对评论的点赞
			if (Comid != null)
			{
				praised_repository.ForCom(creNum, (int)Comid, userid);
			}
			else
			{
				praised_repository.ForRep(creNum, (int)Repid, userid);
			}
			t = new tip()
			{
				message = "操作成功",
				code = 200
			};
			return base.Json(t);
		}

		//获取所有评论回复与当前用户的联系
		public IEnumerable<ViewModel> GetComAndRep(int postid)
		{
			int now_userid = 0;
			if (Session["user"] != null)
			{
				now_userid = (int)Session["userid"];
			}
			//查找当前帖子下所有评论
			IEnumerable<Comment> coms = lpe.Comment.Include("User").Where(e => e.Post_Id == postid).OrderByDescending(e=>e.Com_Praise_Num);
			IList<ViewModel> vms = new List<ViewModel>();
			foreach(var com in coms)
			{
				//status为2表示未被点赞
				int status = 2;
				Praised p = lpe.Praised.Where(e => (e.Com_Id == com.CommentID && e.User_Id == now_userid)).FirstOrDefault();

				//查找当前用户点赞过的所有回复
				IQueryable<Praised> pp = lpe.Praised.Where(e => e.User_Id == now_userid);
				var query = (from P in pp
							 join R in lpe.Replys
							 on P.Rep_Id equals R.ReplyID
							 select R).ToList();
				IList<Replys> pr = query;
				if (p != null)
				{
					//说明该评论被当前用户点赞过
					status = 1;
				}

				//获取该评论所有回复
				IQueryable<Replys> rs = lpe.Replys.Include("User").Where(e => e.Com_Id == com.CommentID).OrderByDescending(e=>e.ReplyID);
				//初始化一个viewmodel实例
				ViewModel vm = new ViewModel()
				{
					c = com,  //一条评论
					Rs = rs, //这条评论的所有回复
					PraStatus = status, //这条评论有没有被当前用户点赞
					Ps = pr //
				};
				vms.Add(vm);
			}
			IEnumerable<ViewModel> vmss = vms;
			return vmss;
		}

		//分类帖子页
		public ActionResult Topic()
		{
			IEnumerable<Post> postlists = null;
			string sid = RouteData.Values["id"].ToString();
			int id;
			if(int.TryParse(sid,out id))
			{
				switch (id)
				{
					case 1:
						ViewBag.src = "//edu-image.nosdn.127.net/fa8a4ea7-fc09-4c31-852c-e1aee620e3a4.png";
						ViewBag.title = "编程宝典";
						ViewBag.content = "欢迎在这里分享和获取编程技巧，优秀作品教案，以及各类高级功能，比如云变量、自定义、扩展功能等。";
						break;
					case 2:
						ViewBag.src = "//edu-image.nosdn.127.net/efae8b2f-4ab0-4d65-84c6-4818b3558994.png";
						ViewBag.title = "你问我答";
						ViewBag.content = "在Scratch的学习或创作中，如果遇到了无法解决的疑难问题，可以随时在这里发帖寻求帮助， 发帖时请注意描述完整你的问题。";
						break;
					case 3:
						ViewBag.src = "//edu-image.nosdn.127.net/f1dcac85-887c-4af7-ae32-18010a642683.png";
						ViewBag.title = "作品Show";
						ViewBag.content = "好的作品值得被更多的人关注，来这里Show出你的得意之作 ，或者向大家推荐你最欣赏的scratch作品吧！ 帖子格式：【作品秀】作品名称+推荐语";
						break;
					case 4:
						ViewBag.src = "//edu-image.nosdn.127.net/ec30c2fd-8e1a-437e-b0de-b59b4d8b5a2f.jpg";
						ViewBag.title = "站务反馈";
						ViewBag.content = "使用卡搭社区遇到的bug反馈、举报/投诉信息集聚地，也可以展望你需要的功能或建议。 同时，社区的更新内容及官方活动也将汇聚于此。同步论坛更新内容、发布官方活动。";
						break;
					default:
						break;
				}
				postlists = lpe.Post.Include("User").Where(e => e.Post_Type == id);
			}
			return View(postlists);
		}


		public ActionResult Search(string key)
		{
			// 根据帖子标题和tag来匹配关键字
			IEnumerable<Post> ps = lpe.Post.Where(e => e.Post_Title.IndexOf(key) != -1 || e.Post_Tag.IndexOf(key) != -1);
			ViewBag.key = key;
			return View("Search",ps);
		}

		//收藏帖子
		[HttpPost]
		public JsonResult CollectPost(int postid,int tag)
		{
			int userid = (int)Session["userid"];
			tip t = null;
			Collect co = lpe.Collect.Where(e => (e.Post_Id == postid && e.User_Id == userid)).FirstOrDefault();
			if (tag == 1)
			{
				Collect c = new Collect()
				{
					Collect_Type = "论坛帖子",
					User_Id = userid,
					Post_Id = postid
				};
				lpe.Collect.Add(c);
				t = new tip()
				{
					message = "收藏成功",
					code = 200
				};
			}
			else if(co!=null)
			{
				lpe.Collect.Remove(co);
				t = new tip()
				{
					message = "取消收藏成功",
					code = 200
				};
			}

			try
			{
				lpe.SaveChanges();
			}
			catch (Exception e)
			{
				throw e;
			}
			return base.Json(t);
		}
		

		public ActionResult UserHeaderSummery1()
		{
			return PartialView((User)Session["user"]);
		}
		public ActionResult UserHeaderSummery2()
		{
			return PartialView((User)Session["user"]);
		}


	}
}