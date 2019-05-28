using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models.Abstract
{
	public interface ICommentRepository
	{
		//增加评论方法
		void AddComment(int userid,int postid,string date,string com_content);

		//删除评论
		void RemoveComment(int comid);
	}
}
