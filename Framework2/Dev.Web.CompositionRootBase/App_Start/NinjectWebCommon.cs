// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2013��02��02�� 17:15
// 
// �޸��ڣ�2013��02��05�� 17:32
// �ļ�����NinjectWebCommon.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************

using System.Linq;
using System.Web;
using Dev.Comm;
using Dev.Data;
using Dev.Data.Configuration;
using Dev.Data.ContextStorage;
using Dev.Web.CompositionRootBase.App_Start;

//using WebActivator;

[assembly: PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
//[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace Dev.Web.CompositionRootBase.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using Dev.Comm.Web.Mvc.Formatter;
    using Dev.Crosscutting.Adapter.Adapter;
    using Dev.Crosscutting.Adapter.NetFramework.Adapter;
    using Dev.Web.CompositionRootBase.Tools;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        #region Static Fields

        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

            bootstrapper.Initialize(CreateKernel);

            AutoMapperBootrapper.Start();
        }

        /// <summary>
        ///     Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            //����Lazy<>
            kernel.Load<LazyBinding>();

            RegisterData(kernel);

            RegisterServices(kernel);

            RegsPlus(kernel);

            //�Զ��ͷ�����
            DynamicModuleUtility.RegisterModule(typeof(WebDbContextStorageRleaseModule));

            return kernel;
        }

        private static void RegisterData(IKernel kernel)
        {
            //ʹ�� httpcontext ���д洢 context
            kernel.Bind<IDbContextStorage>().To<WebDbContextStorage>().InRequestScope();
            CommonConfig.Instance().ConfigureDbContextStorage(kernel.Get<IDbContextStorage>());
        }


        /// <summary>
        ///     Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {


            //Jsonp Support
            GlobalConfiguration.Configuration.Formatters.Insert(0, new JsonpMediaTypeFormatter());

            //for web api
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            //for mvc controllers
            var controllerActivator = new NinjectControllerActivator(kernel);
            var controllerFactory = new DefaultControllerFactory(controllerActivator);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);


        }


        private static void RegsPlus(IKernel kernel)
        {
            //IEnumerable<IRegister> registers = AssemblyManager.GetTypeInstances<IRegister>();

            //if (registers != null && registers.Any())
            //    foreach (IRegister register in registers)
            //    {
            //        register.Kernel = kernel;
            //        register.Register();
            //    }

            RegType<IRegister>(kernel);
            RegType<IRegister2>(kernel);
            RegType<IRegister3>(kernel);
            RegType<IRegister4>(kernel);
            RegType<IRegister5>(kernel);

        }


        private static void RegType<T>(IKernel kernel) where T : IRegister0
        {
            IEnumerable<object> registers = AssemblyManager.GetTypeInstances(typeof(T));

            if (registers != null && registers.Any())
                foreach (IRegister0 register in registers)
                {
                    register.Kernel = kernel;
                    register.Register();
                }
        }

        ///// <summary>
        ///// ����ӳ��,
        ///// </summary>
        ///// <param name="kernel"></param>
        //private static void RegisterMapper(IKernel kernel)
        //{

        //}



        #endregion
    }
}