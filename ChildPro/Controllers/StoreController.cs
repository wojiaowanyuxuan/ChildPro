using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using BLL;
using Models;
using ChildPro.Models;

namespace ChildPro.Controllers
{
    public class StoreController : Controller
    {
        private LessProEntities db = new LessProEntities();
        ProductManager productManager = new ProductManager();
        StoreManager storeManager = new StoreManager();
       

        // GET: Store
        public ActionResult Index()
        {
            IEnumerable<Product> Better_Products1 = productManager.GetProduct().OrderByDescending(e => e.Sales_Num).Take(1);
            IEnumerable<Product> Better_Products2 = productManager.GetProduct().OrderByDescending(e => e.Sales_Num).Skip(1).Take(1);
            IEnumerable<Product> Better_Products3 = productManager.GetProduct().OrderByDescending(e => e.Sales_Num).Skip(2).Take(1);
            IEnumerable<Product> Products_Center = productManager.GetProduct().Skip(rand()).Take(8);
            if (Session["userid"] != null)
            {
                ViewBag.id = (int)Session["userid"];
            }
            //var products = productManager.GetProduct().Skip(rand()).Take(8);
            return View(
                new StoreViewModel
                {
                    better_products1 = Better_Products1,
                    better_products2 = Better_Products2,
                    better_products3 = Better_Products3,
                    products_center = Products_Center
                });
        }
        public int rand()
        {
            Random r = new Random();
            return r.Next(1, 4);
        }

        //GEt:Product Center
        public ActionResult ProductCenter()
        {
            IEnumerable<Product> Products_Center_WenJu = productManager.GetProduct().Where(e => e.Product_Type == "文具").Take(8);
            IEnumerable<Product> Products_Center_ShuJi = productManager.GetProduct().Where(e => e.Product_Type == "书籍").Take(8);
            IEnumerable<Product> Products_Center_WanJu = productManager.GetProduct().Where(e => e.Product_Type == "玩具").Take(8);
            return View(
                new StoreViewModel
                {
                    products_center_wenju = Products_Center_WenJu,
                    products_center_shuji = Products_Center_ShuJi,
                    products_center_wanju = Products_Center_WanJu
                }
                );
        }
        // GET: Store/Details/5
        public ActionResult ProductDetails(int? id)
        {
            var userid = -1;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productManager.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (Session["userid"] != null)
            {
                userid = (int)Session["userid"];
            }
           
            //Comment_Product comment_Product = storeManager.GetCommentByID(id);
            IEnumerable<Product_Norms> Pro_Norms = db.Product_Norms.Where(e => e.Product_ID == id);
            IEnumerable<Product> TuiJian = db.Product.Where(e => e.Product_Type == product.Product_Type).Take(12);
            IEnumerable<Comment_Product> Pro_CoM = db.Comment_Product.Where(e => e.Product_ID == id).Take(15);
            IEnumerable<Adress> Adresses = db.Adress.Where(e => e.UserID == userid);
            User Users = db.User.Find(userid);
            return View(
                new ProductDetailViewModel
                {
                    pro_no = Pro_Norms,
                    pro_detail = product,
                    tuijian=TuiJian,
                    pro_com=Pro_CoM,
                    adresses = Adresses,
                    users=Users

                }
                );
        }

        //Store/Cart

        //加购物车
        [HttpPost]
        public JsonResult AddToCart(int pro_id, int type_id, int num)
        {
            tip t = null;
            var datetime = System.DateTime.Now;
            Cart c = new Cart()
            {
                Product_ID = pro_id,
                Pro_Num = num,
                Pro_Norm_id = type_id,
                UserID = (int)Session["userid"],
                Time = datetime,
                Flag = 0
            
            };
            db.Cart.Add(c);
            try
            {
                db.SaveChanges();
                t = new tip
                {
                    message = "添加成功"
                };
            }
            catch (Exception e)
            {
                throw e;
            }
            return base.Json(t);
        }

        //立即购买
        [HttpPost]
        public JsonResult AddToOrder(int pro_id, int type_id, int num, string address, string marks, string name, string tele)
        {
            tip t = null;
            var datetime = System.DateTime.Now;
          
            Order_Detail b = new Order_Detail()
            {
                User_ID = (int)Session["userid"],
                Product_ID =pro_id,
                Pay_Time=datetime,
                Pro_Norm_ID=type_id,
                Pro_Num=num,
                Users_Remarks=marks,
                Adress=address.Trim(),

            };
            db.Order_Detail.Add(b);
            db.SaveChanges();
            try
            {     
                t = new tip
                {
                    message = "下单成功"
                };
            }
            catch (Exception d)
            {
                throw d;
            }
            return base.Json(t);
        }

        //订单详情
        public ActionResult OrderDetail(int? id)
        {
            Order_Detail order_Details = db.Order_Detail.Find(id);
            return View(
                new StoreViewModel
                {
                Order_Details=order_Details
                });
        }
        [HttpPost]
        public JsonResult Comment(string comment,int pro_id)
        {
            tip t = null;
            var datetime = System.DateTime.Now;

            Comment_Product z = new Comment_Product()
            {
                UserID = (int)Session["userid"],
                Com_Content=comment,
                Com_Data=datetime,
                Product_ID=pro_id
            };
            db.Comment_Product.Add(z);
            try
            {
                db.SaveChanges();
                t = new tip
                {
                    message = "评论成功"
                };
            }
            catch (Exception d)
            {
                throw d;
            }
            return base.Json(t);
        }


    }

    public class tip
    {
        public string message { get; set; }
        public int code { get; internal set; }
    }
}