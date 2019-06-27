using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using ChildPro.Models;
using PagedList;

namespace ChildPro.Controllers
{
    public class CourseController : Controller
    {
        LessProEntities dbContext = new LessProEntities();
        CourseViewModel courseviewmodel = new CourseViewModel();
        CourseManager courseManager = new CourseManager();
        VideoManager videoManager = new VideoManager();
        Course_ClassManager classManager = new Course_ClassManager();
        TeacherManager teacherManager = new TeacherManager();
        Comment_CourseManager comManager = new Comment_CourseManager();
        Replys_CourseManager repManager = new Replys_CourseManager();
        UserManager userManager = new UserManager();

        //课程主页
        // GET: Course
        public ActionResult Index()
        {
            //获取最新最热推荐课程
            var c1 = courseManager.GetMostCourse().Take(8);
            var new1 = courseManager.GetNewCourse().Take(8);
            var hot = courseManager.GetHotCourse().Take(8);
            var t1 = dbContext.CourseType.ToList();
            var model = new CourseViewModel() { course = c1, GetNewCourse = new1, GetHotCourse = hot ,coursetype = t1};
            return View(model);
        }

        #region//课程详情页
        public ActionResult Datails(int? id, bool? join , int? teacherid)
        {
            int userid = 0;
            if (Session["userid"] != null)
            {
                userid = (int)Session["userid"];
            }
            bool coll = false;
            //var v1 = videoManager.GetVideoById(videoid);
            var a1 = classManager.GetCourseById(id);
            var c1 = courseManager.GetCourseById1(id);
            var t1 = teacherManager.GetTeacher(teacherid);
            var new1 = courseManager.GetNewCourse().Take(4);
            ViewBag.num = dbContext.Comment_Course.Where(b => b.Course_Id == id).Count();

            //Course c = dbContext.Course.Include("User").Where(b => b.CourseID == id).FirstOrDefault();
            Add_Course add1 = dbContext.Add_Course.Where(b => b.UserId == userid && b.CourseId == id).FirstOrDefault();
            Add_Course add = dbContext.Add_Course.Where(b => b.UserId == userid && b.CourseId == id).FirstOrDefault();
            Collect co = dbContext.Collect.Where(b => (b.User_Id == userid && b.Course_Id == id)).FirstOrDefault();
            //判断课程是否被收藏
            if (co != null)
            {
                coll = true;
            }
            //判断课程是否被加入
            if (add != null)
            {
                join = true;
            }
            else
            {
                join = false;
                Add_Course a = new Add_Course()
                {
                    UserId = userid,
                    CourseId = (int)id
                };
            }
            dbContext.SaveChanges();
            var model = new CourseViewModel() { coursetable = c1, isC = coll , isJ = (bool)join,/*video = v1,*/ courseclass = a1 , teachers = t1 , GetNewCourse = new1 };
            return View(model);
        }

        //加入课程分部视图
        public ActionResult JoinCourse(int? id , bool? join)
        {
            int userid = 0;
            if (Session["userid"] != null)
            {
                userid = (int)Session["userid"];
            }
            Add_Course add = dbContext.Add_Course.Where(b => b.UserId == userid && b.CourseId == id).FirstOrDefault();
            //判断课程是否被加入
            if (add != null)
            {
                join = true;
            }
            else
            {                
                Add_Course a = new Add_Course()
                {
                    UserId = userid,
                    CourseId = (int)id
                };
                dbContext.Add_Course.Add(a);
            }
            dbContext.SaveChanges();
            var c1 = courseManager.GetCourseById1(id);           
            var model = new CourseViewModel() { coursetable = c1};
            return PartialView(model);
        }

        //收藏
        [HttpPost]
        public JsonResult CollectCourse(int? id , int tag)
        {
            int userid = (int)Session["userid"];
            tip t = null;
            Collect co = dbContext.Collect.Where(b => (b.Course_Id == id && b.User_Id == userid)).FirstOrDefault();
            if (tag == 1)
            {
                Collect c = new Collect()
                {
                    Collect_Type = "课程",
                    User_Id = userid,
                    Course_Id = id
                };
                dbContext.Collect.Add(c);
                t = new tip()
                {
                    message = "收藏成功",
                    code = 200
                };
            }
            else if (co != null)
            {
                dbContext.Collect.Remove(co);
                t = new tip()
                {
                    message = "取消收藏成功",
                    code = 200
                };
            }
            try
            {
                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return base.Json(t);
        }
        #endregion

        //获取视频的分部视图
        public ActionResult GetVideoList(int? id)
        {
            courseviewmodel.video = videoManager.GetClassById(id);
            return PartialView(courseviewmodel);
        }

		public ActionResult VideoList(int? id)
		{
			courseviewmodel.video = videoManager.GetClassById(id);
			return PartialView(courseviewmodel);
		}

		//我的课程
		public ActionResult Mycourse(int userid)
        {
            MyCourseViewModel mcv = new MyCourseViewModel();
            mcv.Uses1 = userManager.IEGetUsersById(userid);
			mcv.courses = dbContext.Add_Course.Where(e => e.UserId == userid);
            mcv.collect_courses = dbContext.Collect.Where(b => b.User_Id == userid && b.Collect_Type == "课程");
            ViewBag.userid = userid;
            mcv.User = dbContext.User.Where(c => c.UserID == userid).FirstOrDefault();
            Session["Guanzhu"] = 0;
            //关注的人数
            mcv.UserA = userManager.CountGuanzhu(userid).Count();
            //粉丝数
            mcv.UserB = userManager.CountGuanzhu1(userid).Count();
            //关注的人
            mcv.UsesAa = userManager.CountGuanzhu(userid);
            //粉丝
            mcv.UsesBb = userManager.CountGuanzhu1(userid);
            //判断是否为粉丝
            foreach (var item in userManager.CountGuanzhu1(userid))
            {
                if(Session["userid"] != null)
                {
                    if (item.UserA == userid)
                    {
                        Session["Guanzhu"] = 1;//表示已经关注
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return View(mcv);
        }
        #region 关注/取消关注       
        [HttpPost]
        public JsonResult Guanzhu(int UserB)
        {
            //Guanzhu gz = new Guanzhu();
            //gz.UserA = (int)Session["userid"];
            //gz.UserB = UserB;
            //userManager.Guanzhu(gz);
            //int a = userManager.CountGuanzhu1(UserB).Count();
            //return a;
            tip t = null;
            int userid = (int)Session["userid"];
            Guanzhu gz = new Guanzhu();
            Guanzhu g = new Guanzhu()
            {
                UserA = userid,
                UserB = UserB
            };
            dbContext.Guanzhu.Add(g);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            t = new tip()
            {
                message = "关注成功",
                code = 5
            };
            return base.Json(t);
        }
        public int QuxiaoGuanzhu(int UserB)
        {
            Guanzhu gz = new Guanzhu();
            int UserA = (int)Session["userid"];
            userManager.QuxiaoGuanzhu(UserA, UserB);
            int a = userManager.CountGuanzhu1(UserB).Count();
            return a;
        }
        #endregion

        #region//课程体系页
        public ActionResult System()
        {
            courseviewmodel.course = courseManager.GetCourse();
            courseviewmodel.teachers = teacherManager.GetTeacher();
            return View(courseviewmodel);
        }
        //分页展示类别下的课程
        public ActionResult GetCourseSystem(int? page , int? typeid)
        {
            var c1 = from c in dbContext.Course.OrderBy(c => c.Course_Time)
                     where c.CourseTypeID == typeid
                     select c;
            ViewBag.tyid = typeid;                     
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return PartialView(c1.ToPagedList(pageNumber, pageSize));
        }
        #endregion
     
        #region//展示所有评论回复
        [HttpGet]
        public ActionResult CommentSummery(int? courseid)
        {
            IEnumerable<CourseViewModel> modelss = GetComAndRep((int)courseid);
            return PartialView(modelss.Reverse());
        }

        //对课程的评论回复
        [HttpPost]
        public ActionResult CommentSummery(int type , int? courseid , int? rep_to_userid , int? comid , string content=null ,string date=null)
        {
            int userid = (int)Session["userid"];
            ViewBag.userid = userid;
            //type1为对课程的评论 2为对课程用户 3为评论用户
            switch(type)
            {
                case 1:
                    comManager.AddComment(userid, (int)courseid, date, content);
                    break;
                case 2:
                    repManager.AddReply((int)comid, userid, (int)rep_to_userid, date, content, 1);
                    break;
                case 3:
                    repManager.AddReply((int)comid, userid, (int)rep_to_userid, date, content, 2);
                    break;
                default:
                    break;
            }
            IEnumerable<CourseViewModel> modelss = GetComAndRep((int)courseid);
            return PartialView(modelss.Reverse());
        }
        #endregion

        #region //获取所有的评论回复与当前用户的联系
        public IEnumerable<CourseViewModel> GetComAndRep(int? courseid)
        {
            int now_userid = 0;
            if(Session["user"] != null)
            {
                now_userid = (int)Session["userid"];
            }
            //查找当前课程下的所有评论
            IEnumerable<Comment_Course> coms = dbContext.Comment_Course.Include("User").Where(e => e.Course_Id == courseid);
            IList<CourseViewModel> model = new List<CourseViewModel>();
            foreach(var comment in coms)
            {
                //获取该评论的所有回复
                
                IEnumerable<Replys_Course> rs = dbContext.Replys_Course.Include("User").Where(e => e.Com_Id_Course == comment.CommentID_Course);
                CourseViewModel models = new CourseViewModel()
                {
                    com = comment,//一条评论
                    rep = rs      //该评论的所有回复              
                };
                model.Add(models);
            }
            IEnumerable<CourseViewModel> modelss = model;
            return (modelss);
        }
        #endregion
    }
}