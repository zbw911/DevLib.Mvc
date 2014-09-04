//using Dev.Framework.Cache;
using Dev.Web.CompositionRootBase;
using Ninject;
using Ninject.Web.Common;

namespace CompositionRoot
{
    internal class RegisterCache : IRegister
    {
        #region IRegister Members

        public void Register()
        {
            ////暂时先使用 httpruntime cache
            //this.Kernel.Bind<ICacheState>().To<Dev.Framework.Cache.Impl.HttpRuntimeCache>().InRequestScope();
            //this.Kernel.Bind<ICacheWraper>().To<CacheWraper>().InRequestScope();
        }

        public IKernel Kernel { get; set; }

        #endregion
    }
}