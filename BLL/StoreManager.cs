using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DALFactory;
using IDAL;

namespace BLL
{
    public class StoreManager
    {
        IStore icomment = DataAccess.GetComment();
        //IStore iorder = DataAccess.GetOrder_UserId();
        //获取评论
        public Comment_Product GetCommentByID(int? id)
        {
            Comment_Product comment_Product = icomment.GetCommentByPro_ID(id);
            return comment_Product;
        }
        //public Order GetOrderByUser_Id(int? id)
        //{
        //    Order order = icomment.GetOrderByUser_Id(id);
        //    return order;
        //}
    }
}
