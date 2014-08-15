using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Dev.Data.Test.DoMain2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Data.Test
{
    using System.Data.Entity;
    using System.IO;

    using Dev.Data.Configuration;
    using Dev.Data.ContextStorage;
    using Dev.Data.Test.Reponsitory;
    using Dev.Demo.Entities2.Models;

    using Infrastructure.Tests.Data;

    /// <summary>
    /// Test_AdminRepository 的摘要说明
    /// </summary>
    [TestClass]
    public class TestAdminRepository
    {
        public TestAdminRepository()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private static IAdminRepository _adminRepository;
        private TestContext _testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {

            //AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            ////DbContextManager.InitStorage(new SimpleDbContextStorage());

            //CommonConfig.Instance()
            //            .ConfigureDbContextStorage(new SimpleDbContextStorage())
            //            .ConfigureData<MyDbContext>("DefaultConnection")
            //            .ConfigureData<SysManagerContext>("DefaultConnection1");

            //config.ConfigureData<MyDbContext>("DefaultConnection");
            ContextInit.Init();
            _adminRepository = new AdminRepository("DefaultConnection1");
        }

        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        [TestInitialize()]
        public void MyTestInitialize()
        {


        }

        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //AdminRepository.AddAdmin(1, "aa", "bb", "cc@1.com");
            _adminRepository.GetAll();
        }



        [TestMethod]
        public void AllTest()
        {
            Test_Create_Db();


            DropDatabase();
        }


        public void Test_Create_Db()
        {
            //AdminRepository.AddAdmin(1, "aa", "bb", "cc@1.com");



            var context = DbContextManager.CurrentFor("DefaultConnection1");

            if (!context.Database.Exists())
            {
                context.Database.Create();
            }


            _adminRepository.GetAll();
            DbContextManager.CloseAllDbContexts();
            //context.Database.Delete();
        }


        public void DropDatabase()
        {
            var context = DbContextManager.CurrentFor("DefaultConnection1");

            if (context.Database.Exists())
            {
                if (!context.Database.Delete())
                {
                    Assert.Fail("drop failed");
                }

            }
            else
            {
                Assert.Fail("数据库不存在");
            }
        }
    }
}
