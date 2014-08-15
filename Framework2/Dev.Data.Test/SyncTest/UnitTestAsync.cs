using System;
using System.Threading.Tasks;
using Dev.Demo.Entities2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Data.Test.SyncTest
{
    [TestClass]
    public class UnitTestAsync
    {
        [TestMethod]
        public void TestMethod1()
        {
            ContextInit.Init();

            GenericRepository<Admintype> admin = new GenericRepository<Admintype>("DefaultDb");

            var count = admin.CountAsync();


            admin.Add(new Admintype
            {
                Purviews = "aaa",
                System = true,
                Typename = "typename"
            });
            Task.WaitAll(count);

            var change = admin.UnitOfWork.SaveChangesAsync();

            Task.WaitAll(change);
            var count2 = admin.CountAsync();

            Task.WaitAll(count2);

            Assert.AreEqual(count.Result + 1, count2.Result);
        }



    }
}
