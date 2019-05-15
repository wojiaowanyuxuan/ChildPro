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
			int userid = (int)Session["userid"];
			bool coll = false;
			Post p = lpe.Post.Include("User").Where(e => e.PostID == postid).FirstOrDefault();
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
			IEnumerable<ViewModel> vmss = GetComAndRep(postid);
			return PartialView(vmss.Reverse());
		}

		//对帖子的评论回复
		[HttpPost]
		public ActionResult CommentSummery(int Com_type,int postid,int? rep_to_userid,int? Com_id,string Com_content=null,string date=null)
		{
			int userid = (int)Session["userid"];
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
			IEnumerable<Comment> coms = lpe.Comment.Include("User").Where(e => e.Post_Id == postid);
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
				IQueryable<Replys> rs = lpe.Replys.Include("User").Where(e => e.Com_Id == com.CommentID);
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
		

    }
}