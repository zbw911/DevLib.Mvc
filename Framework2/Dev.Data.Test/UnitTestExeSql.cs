using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Dev.Data.Configuration;
using Dev.Data.ContextStorage;
using Dev.Data.Test.Domain;
using Dev.Data.Test.DoMain2.Models;
using Dev.Demo.Entities2.Models;
using Infrastructure.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Data.Test
{
    class MyCustom
    {
        //public virtual string Firstname { get; set; }

        //public virtual DateTime Inserted { get; set; }

        public virtual string Lastname { get; set; }

        public int Id { get; set; }
    }
    [TestClass]
    public class UnitTestExeSql
    {
        private static ICustomerRepository customerRepository;
        private static SimpleDbContextStorage storeage = new SimpleDbContextStorage();

        static UnitTestExeSql()
        {
            //AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            ////DbContextManager.InitStorage(new SimpleDbContextStorage());

            //CommonConfig.Instance()
            //    .ConfigureDbContextStorage(storeage)
            //    .ConfigureData<MyDbContext>("DefaultConnection")
            //    .ConfigureData<SysManagerContext>("DefaultConnection1");
            ContextInit.Init();
            //config.ConfigureData<MyDbContext>("DefaultConnection");

            customerRepository = new CustomerRepository("DefaultConnection");
        }

        public UnitTestExeSql()
        {


        }
        [TestMethod]
        public void TestMethod1()
        {
            for (int i = 0; i < 10; i++)
            {
                customerRepository.Add(
                    new Customer { Firstname = "zbw911", Inserted = DateTime.Now, Lastname = "null" });
            }

            customerRepository.UnitOfWork.SaveChanges();

            int list = customerRepository.GetQuery<Customer>().Count();

            customerRepository.Delete<Customer>(x => x.Firstname == "zbw911");

            customerRepository.UnitOfWork.SaveChanges();

            int count2 = customerRepository.GetQuery<Customer>().Count();
            Console.WriteLine(list);


        }


        [TestMethod]
        public void ExeQueySqlTest()
        {
            var x = customerRepository.ExecuteSqlCommand("update  Customer set lastname={0}", new[] { "a" });



            Console.WriteLine(x);


            var list = customerRepository.SqlQuery(typeof(MyCustom), "select * from Customer", new[] { "" });



            foreach (MyCustom custom in list)
            {
                Console.WriteLine(custom.Id + "->" + custom.Lastname);
            }
        }

        class mytype
        {
            public int Id { get; set; }

            public string Lastname { get; set; }
        }
        [TestMethod]
        public void CustomTypefrotable()
        {


            var list = customerRepository.SqlQuery<mytype>("select * from Customer").ToList();



            foreach (mytype custom in list)
            {
                Console.WriteLine(custom.Id + "->" + custom.Lastname);
            }


        }


        [TestMethod]
        public void SQLAdd()
        {
            customerRepository.ExecuteSqlCommand("insert into Customer(Firstname,Inserted,Lastname) values({0},{1},{2})", "sqlInsert",
                                                 DateTime.Now, "sql'");

            Assert.AreEqual(1, customerRepository.Count<Customer>(x => x.Firstname == "sqlInsert"));

            var count = customerRepository.SqlQuery<int>("select count(*) as c from  Customer where Firstname= {0}", "sqlInsert");

            Assert.AreEqual(1, count.First());
            //clean
            customerRepository.Delete<Customer>(x => x.Firstname == "sqlInsert");
            customerRepository.UnitOfWork.SaveChanges();
            //new Customer { Firstname = "zbw911", Inserted = DateTime.Now, Lastname = "null" });

            CleanSqlInsert();
        }

        //[TestMethod]
        public void CleanSqlInsert()
        {
            customerRepository.Delete<Customer>(x => x.Firstname == "sqlInsert");
            customerRepository.UnitOfWork.SaveChanges();
        }
        [TestMethod]
        public void GetSet()
        {
            var type = Type.GetType("Dev.Data.Test.Domain.Customer");

            dynamic query = customerRepository.GetQuery(type);


            foreach (var id in query)
            {
                Console.WriteLine(id.Id);
            }

            Order s;


        }

        class customorder
        {
            public virtual int CustomerId { get; set; }

            public virtual DateTime OrderDate { get; set; }

            public virtual string Firstname { get; set; }

            public virtual DateTime Inserted { get; set; }

            public virtual string Lastname { get; set; }
        }

        [TestMethod]
        public void JoinTest()
        {
            var count = customerRepository.SqlQuery<customorder>("select * from  Customer join [order] on   Customer.id = [order].CustomerId ");

            foreach (var i in count)
            {
                Console.WriteLine(i.CustomerId);
                Console.WriteLine(i.Inserted);
            }

        }




        [TestMethod]
        public void MyTestMethod()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic["1"] = "1";

            Console.WriteLine(dic["1"]);
        }

        [TestMethod]
        public void MyTestMethod2()
        {
            string[] a = new[] { "1", "2" };

            var b = a.Select(x => Convert.ToInt16(x));

            foreach (var intse in b)
            {
                Console.WriteLine(intse);
            }
        }




    }
}