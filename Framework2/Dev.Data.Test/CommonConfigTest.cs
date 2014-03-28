﻿namespace Dev.Data.Test
{
    using System;
    using System.IO;
    using System.Linq;

    using Dev.Data.Configuration;
    using Dev.Data.ContextStorage;
    using Dev.Data.Test.Domain;
    using Dev.Demo.Entities2.Models;

    using Infrastructure.Tests.Data;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     CommonConfigTest 的摘要说明
    /// </summary>
    [TestClass]
    public class CommonConfigTest
    {
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #region Fields

        private ICustomerRepository customerRepository;

        #endregion

        #region Public Properties

        /// <summary>
        ///     获取或设置测试上下文，该上下文提供
        ///     有关当前测试运行及其功能的信息。
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void TestMethod1()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            //DbContextManager.InitStorage(new SimpleDbContextStorage());

            CommonConfig.Instance()
                        .ConfigureDbContextStorage(new SimpleDbContextStorage())
                        .ConfigureData<MyDbContext>("DefaultConnection")
                        .ConfigureData<MyDbContext>("DefaultConnection1");

            //config.ConfigureData<MyDbContext>("DefaultConnection");

            this.customerRepository = new CustomerRepository("DefaultConnection");
            for (int i = 0; i < 10; i++)
            {
                this.customerRepository.Add(
                    new Customer { Firstname = "zbw911", Inserted = DateTime.Now, Lastname = "null" });
            }

            int list = this.customerRepository.GetQuery<Customer>().Count();
            this.customerRepository.UnitOfWork.SaveChanges();
            Console.WriteLine(list);


            var v = this.customerRepository.GetQuery<Customer>().AsEnumerable();

            foreach (var customer in v)
            {
                Console.WriteLine(customer.Id);
            }

            //
            // TODO: 在此处添加测试逻辑
            //
        }

        [TestMethod]
        public void TestDomainAndDomain2()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            //DbContextManager.InitStorage(new SimpleDbContextStorage());

            CommonConfig.Instance()
                        .ConfigureDbContextStorage(new SimpleDbContextStorage())
                        .ConfigureData<MyDbContext>("DefaultConnection")
                        .ConfigureData<SysManagerContext>("DefaultConnection1");

            //config.ConfigureData<MyDbContext>("DefaultConnection");

            this.customerRepository = new CustomerRepository("DefaultConnection");
            for (int i = 0; i < 10; i++)
            {
                this.customerRepository.Add(
                    new Customer { Firstname = "zbw911", Inserted = DateTime.Now, Lastname = "null" });
            }

            int list = this.customerRepository.GetQuery<Customer>().Count();
            this.customerRepository.UnitOfWork.SaveChanges();
            Console.WriteLine(list);

            //
            // TODO: 在此处添加测试逻辑
            //
        }

        #endregion
    }
}