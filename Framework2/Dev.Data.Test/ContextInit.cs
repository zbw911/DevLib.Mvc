using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dev.Data.Configuration;
using Dev.Data.ContextStorage;

namespace Dev.Data.Test
{
    class ContextInit
    {

        private static bool inited = false;


        public static void Init()
        {
            if (inited)
                return;
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            //DbContextManager.InitStorage(new SimpleDbContextStorage());
            DbContextManager.Init("DefaultDb", new[] { "Dev.Data.Test" }, true);


            CommonConfig.Instance()
                       .ConfigureDbContextStorage(new SimpleDbContextStorage())
                       .ConfigureData<MyDbContext>("DefaultConnection")
                       .ConfigureData<MyDbContext>("DefaultConnection1");

        }
    }
}
