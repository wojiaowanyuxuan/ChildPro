using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using ChildPro.Models;
using System.Data.Entity.Migrations;
using System.Net;
using System.Data.Entity;

namespace ChildPro.Controllers
{
    public class TeacherController : Controller
    {
        LessProEntities dbContext = new LessProEntities();
        TeacherManager teacherManager = new TeacherManager();
        TeacherViewModel tv = new TeacherViewModel();
        // GET: Teacher
        public ActionResult Index(int? teacherid)
        {            
            var t1 = teacherManager.GetTeacherById(teacherid);
            var c1 = from c in dbContext.Course
                     where c.TeacherId == teacherid
                     select c;
            var model = new TeacherViewModel() { teachers = t1 , courses = c1};
            return View(model);           
        }

        //普通用户看到的教师界面
        public ActionResult Index1(int? teacherid)
        {
            var t1 = teacherManager.GetTeacherById(teacherid);
            var c1 = from c in dbContext.Course
                     where c.TeacherId == teacherid
                     select c;
            var model = new TeacherViewModel() { teachers = t1, courses = c1 };
            return View(model);
        }

        //添加课程Add方法
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.CourseTypeID = new SelectList(dbContext.CourseType, "CourseTypeID", "CourseTypeName");           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "CourseID,CourseName,CourseType,ImageMineType,Course_Time,TeacherId,Number,Value,CourseTypeID")] Course course)
        {
            int teacherid = 0;
            if (Session["Teacher"] != null)
            {
                teacherid = (int)Session["teacherid"];
            }
            if (ModelState.IsValid)
            {
                dbContext.Course.Add(course);
                dbContext.SaveChanges();
                return RedirectToAction("Index",new { teacherid = teacherid });
            }
            ViewBag.CourseTypeID = new SelectList(dbContext.CourseType, "CourseTypeID", "CourseTypeName");
            return View(course);          
        }

        //修改
        public ActionResult Edit(int? id ,int? teacherid)
        {           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = dbContext.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }           
            ViewBag.CourseTypeID = new SelectList(dbContext.CourseType, "CourseTypeID", "CourseTypeName");
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,CourseName,CourseType,ImageMineType,Course_Time,TeacherId,Number,Value,CourseTypeID")] Course course,int ?teacherid)
        {           
            if (Session["Teacher"] != null)
            {
                teacherid = (int)Session["teacherid"];
            }
            if (ModelState.IsValid)
            {
                dbContext.Entry(course).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index",new { teacherid = teacherid });
            }
            
            ViewBag.CourseTypeID = new SelectList(dbContext.CourseType, "CourseTypeID", "CourseTypeName");
            return View(course);
        }
        //修改
        public ActionResult Delect(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = dbContext.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "CourseID,CourseName,CourseType,ImageMineType,Course_Time,TeacherId,Number,Value,CourseTypeID")] Course course)
        {
            int teacherid = 0;
            if (Session["Teacher"] != null)
            {
                teacherid = (int)Session["teacherid"];
            }
            if (ModelState.IsValid)
            {
                dbContext.Course.Remove(course);
                dbContext.SaveChanges();
                return RedirectToAction("Index", new { teacherid = teacherid });
            }
            ViewBag.CourseTypeID = new SelectList(dbContext.CourseType, "CourseTypeID", "CourseTypeName");
            return View(course);          
        }
    }
}