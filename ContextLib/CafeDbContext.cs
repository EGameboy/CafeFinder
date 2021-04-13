using Microsoft.EntityFrameworkCore;
using System.IO;
//using Microsoft.EntityFrameworkCore.Proxies;

namespace Packt.Shared {
    public class CafeDbContext: DbContext {
        public DbSet<Cafe> Cafe { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            DirectoryInfo di = new DirectoryInfo(System.Environment.CurrentDirectory);
            string path = $"{di.Parent.FullName}\\CafeDatabase\\Cafe.db";
            //string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "\\CafeDatabase\\Cafe.db");

            optionsBuilder
                //.UseLazyLoadingProxies()
                .UseSqlite($"Filename={path}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //modelBuilder.Entity<Cafe>()
            //    .HasNoKey();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cafe>().ToTable("Cafe");
        }


        public CafeDbContext(DbContextOptions<CafeDbContext> options)
            :base(options) {

            Database.EnsureCreated();
        }
        public CafeDbContext() {}
    }
}