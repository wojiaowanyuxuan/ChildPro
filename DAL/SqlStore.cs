using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using IDAL;

namespace DAL
{
    class SqlStore : IStore
    {
        LessProEntities db = new LessProEntities();
        //获取评论
        public Comment_Product GetCommentByPro_ID(int? id)
        {
            Comment_Product comment_Product = db.Comment_Product.Find(id);
            return comment_Product;
        }
        //
        //public Order GetOrderByUser_Id(int? id)
        //{
        //    Order order = db.Order.Find(id);
        //    return order;
        //}
    }
}
