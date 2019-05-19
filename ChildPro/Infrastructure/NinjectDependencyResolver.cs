using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Models.Abstract;
using Models.Concrete;

namespace ChildPro.Infrastructure
{
	public class NinjectDependencyResolver: IDependencyResolver
	{
		private IKernel kernel;
		public NinjectDependencyResolver(IKernel kernelParam)
		{
			kernel = kernelParam;
			//添加绑定
			AddBindings();
		}
		public object GetService(Type serviceType)
		{
			return kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}
		private void AddBindings()
		{
			// 添加绑定 --用户
			kernel.Bind<IUserInfoRepository>().To<EFUserInfoRepository>();
			// 添加绑定 --论坛帖子
			kernel.Bind<IPostRepository>().To<EFPostRepository>();
			//添加绑定 --评论
			kernel.Bind<ICommentRepository>().To<EFCommentRepository>();
			// --回复
			kernel.Bind<IReplyRepository>().To<EFReplyRepository>();
			//点赞
			kernel.Bind<IPraisedRepository>().To<EFPraisedRepository>();
			//关注
			kernel.Bind<IFollowRepository>().To<EFFollowRepository>();
		}
	}
}