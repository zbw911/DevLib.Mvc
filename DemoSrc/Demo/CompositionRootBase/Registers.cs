using System.Linq;
using Dev.Comm;
using Dev.Data;
using Dev.Data.Configuration;
using Dev.Data.Infras;
using Dev.Web.CompositionRootBase;
using Ninject;
using Ninject.Web.Common;

namespace CompositionRoot
{
    public class Registers : RegisterContextBase, IRegister
    {
        #region Constants

        private const string DefaultConnection = "DefaultConnection";

        #endregion

        //private const string SysManagerContext = "SysManagerContext1";

        #region IRegister Members

        public override IKernel Kernel { get; set; }

        public override void Register()
        {

            #region Db Config

            //配置数据库连接
            CommonConfig.Instance()
                .ConfigureData<Data.Models.SysManagerContext>(DefaultConnection);

            #endregion



            this.Kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>)).InRequestScope()/*.InRequestScope()*/.WithConstructorArgument("connectionStringName", DefaultConnection);


            #region Service  Register



            //统一绑定的方法
            var alldomain = AssemblyManager.GetTypes()
   .Where(x => !string.IsNullOrEmpty(x.Namespace) && x.Namespace.StartsWith("Service")).ToList();

            var interfaceServces = alldomain.Where(x => x.Name.StartsWith("I") && x.Name.EndsWith("Service") && x.IsInterface).ToList();


            foreach (var interfaceServce in interfaceServces)
            {
                var implService = alldomain.FirstOrDefault(x => x.Namespace == interfaceServce.Namespace && !x.IsInterface && interfaceServce.IsAssignableFrom(x));

                if (implService != null)
                {
                    //var b = interfaceServce.IsAssignableFrom(implService);

                    var obj = Kernel.TryGet(interfaceServce);
                    //如果不存在才绑定
                    if (obj == null)
                        Kernel.Bind(interfaceServce).To(implService);
                }
            }


            #endregion






        }



        #endregion
    }


}