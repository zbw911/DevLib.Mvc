using System.Data.Entity;
using Data.Models.Mapping;

namespace Data.Models
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
