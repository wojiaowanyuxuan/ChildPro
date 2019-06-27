using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IDAL
{
    public interface IStore
    {
        //获取评论
        Comment_Product GetCommentByPro_ID(int? id);

        //
        //Order GetOrderByUser_Id(int? id);
    }
}
