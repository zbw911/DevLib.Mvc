using System.Data.Entity;
using Dev.Demo.Entities2.Models;
using Dev.Demo.Entities2.Models.Mapping;

namespace Dev.Data.Test.DoMain2.Models
{
    public partial class SysManagerContext : DbContext
    {
        static SysManagerContext()
        {

            Database.SetInitializer<SysManagerContext>(null);
        }

        public SysManagerContext()
            : base()
        {
            //this.Database.CreateIfNotExists();
        }

        public SysManagerContext(string connStringName)
            : base(connStringName)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
            this.Database.CreateIfNotExists();
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Admintype> Admintypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new AdminMap());
            modelBuilder.Configurations.Add(new AdmintypeMap());
        }
    }


}
