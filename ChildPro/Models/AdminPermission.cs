using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ChildPro.Models
{
	public class AdminPermission
	{
		public IEnumerable<Post> posts { get; set; }

		public Admin a { get; set; }
	}
}