using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ChildPro.Models
{
	public class ViewModel
	{
		//用一个viewmodel表示一个评论以及该评论下的所有回复 以及点赞详情
		public Comment c { get; set; }
		public IEnumerable<Replys> Rs { get; set; }
		public int PraStatus;
		public IList<Replys> Ps;
	}
}