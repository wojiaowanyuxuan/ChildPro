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
		}
	}
}