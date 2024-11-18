using Microsoft.EntityFrameworkCore;
using CEMS_Server.Models;

namespace CEMS_Server.AppContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<cems_position> cems_position { get; set; }
        public DbSet<cems_user> cems_user { get; set; }
        public DbSet<cems_role> cems_role { get; set; }
        public DbSet<cems_company> cems_company { get; set; }
        public DbSet<cems_department> cems_department { get; set; }
        public DbSet<cems_section> cems_section { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cems_user>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.usr_rol_id);

            modelBuilder.Entity<cems_user>()
                .HasOne(u => u.Position)       // เชื่อมกับ cems_position
                .WithMany()                    // เนื่องจาก cems_position ไม่ได้มี Navigation Property ไปที่ cems_user
                .HasForeignKey(u => u.usr_pst_id); // ใช้ usr_pst_id เป็น Foreign Key

            modelBuilder.Entity<cems_user>()
                .HasOne(u => u.Department)
                .WithMany()
                .HasForeignKey(u => u.usr_dpt_id);

            modelBuilder.Entity<cems_user>()
                .HasOne(u => u.Company)
                .WithMany()
                .HasForeignKey(u => u.usr_cpn_id);

            modelBuilder.Entity<cems_user>()
                .HasOne(u => u.Section)
                .WithMany()
                .HasForeignKey(u => u.usr_st_id);

            base.OnModelCreating(modelBuilder);
        }
    }
}