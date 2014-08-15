using System.Data.Entity.ModelConfiguration;

namespace Data.Models.Mapping
{
    public class AdminMap : EntityTypeConfiguration<Admin>
    {
        public AdminMap()
        {
            // Primary Key
            this.HasKey(t => t.AdminId);



            this.Property(t => t.Pwd)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Uname)
                .IsRequired()
                .HasMaxLength(20);




            this.Property(t => t.Loginip)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Admin");
            this.Property(t => t.AdminId).HasColumnName("AdminId");
            this.Property(t => t.Typeid).HasColumnName("Typeid");

            this.Property(t => t.Pwd).HasColumnName("Pwd");
            this.Property(t => t.Uname).HasColumnName("Uname");

            this.Property(t => t.Loginip).HasColumnName("Loginip");

            // Relationships
            this.HasRequired(t => t.Admintype)
                .WithMany(t => t.Admins)
                .HasForeignKey(d => d.Typeid);

        }
    }
}
