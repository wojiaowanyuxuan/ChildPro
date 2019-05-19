using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models; 

namespace ChildPro.Models
{
	public class userViewModel
	{
		public User u { get; set; }
		//访问的是否是当前登录用户主页
		public bool IsOwner { get; set; }
		//当前用户评论过的帖子
		public IEnumerable<Post> com_posts { get; set; }

		//用户发表过的帖子
		public IEnumerable<Post> pub_posts { get; set; }

		//用户收藏的帖子
		public IEnumerable<Post> coll_posts { get; set; }

		//我是否关注了他/她
		public bool IsAttention { get; set; }
		//用户粉丝
		public IEnumerable<Follow> All_Fans { get; set; }

		//用户关注的人
		public IEnumerable<Follow> All_Attention { get; set; }
	}
}