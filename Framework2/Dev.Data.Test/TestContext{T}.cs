﻿using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace Dev.Data.Test
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.IO;
    using System.Reflection;

    using Dev.Data.ContextStorage;
    using Dev.Data.Infras;

    using Infrastructure.Tests.Data;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestContext_T
    {

        [ClassInitialize]
        public static void CalssInit(TestContext testcontext)
        {
            //AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            //DbContextManager.InitStorage(new SimpleDbContextStorage());
            //DbContextManager.Init("DefaultDb", new[] { "Dev.Data.Test" }, true);

            ContextInit.Init();
        }

        #region Fields

        private ICustomerRepository customerRepository;

        private IRepository repository;

        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void ObjFuncs()
        {
            var mydbcontext = new MyDbContext("DefaultDb");
            ObjectContext context = ((IObjectContextAdapter)mydbcontext).ObjectContext;

            EntityContainer container = context.MetadataWorkspace.GetEntityContainer(
                context.DefaultContainerName, DataSpace.CSpace);

            foreach (EntitySetBase baseEntitySet in container.BaseEntitySets)
            {
                Console.WriteLine(baseEntitySet.ElementType.Name);
            }
        }

        [TestMethod]
        //这个方法不能用在 CF 中
        public void ObjFuncs1()
        {
            var mydbcontext = new MyDbContext("DefaultDb");
            ObjectContext context = ((IObjectContextAdapter)mydbcontext).ObjectContext;

            //var name = GetEntitySetName<Customer>(context);
            //Console.WriteLine(name);
        }

        [TestInitialize]
        public void SetUp()
        {
            this.customerRepository = new CustomerRepository("DefaultConnection");
            this.repository = new GenericRepository("DefaultConnection");
        }

        [TestMethod]
        public void TestMethod1()
        {
            //var dbcontext = DbContextManager.CurrentFor<Customer>();

            //Assert.IsNotNull(dbcontext);
        }

        [TestMethod]
        public void TwoContext()
        {
            //    DbContextManager.Init("DefaultDb", new[] { "Dev.Data.Test" });

            //    var dbcontext = DbContextManager.CurrentFor<Customer>();

            //    Assert.IsNotNull(dbcontext);
        }

        #endregion

        //这个方法不能用在 CF 中

        #region Methods

        private static string GetEntitySetName<T>(ObjectContext context)
        {
            PropertyInfo entitySetType =
                context.GetType()
                       .GetProperties()
                       .Where(
                           p =>
                           p.PropertyType.IsGenericType && p.PropertyType.Name.StartsWith("ObjectSet")
                           && p.PropertyType.GetGenericArguments()[0].Name == typeof(T).Name)
                       .Single();
            return entitySetType.Name;
        }

        #endregion
    }
}