using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using VisualStudioSummitDemo.Interceptors;

namespace VisualStudioSummitDemo.Models
{
    public class DemoContext : DbContext
    { 
        public DemoContext()
        {
            Database.Log += s => File.AppendAllText(@"C:\temp\eflog.txt", s);
            DbInterception.Add(new SoftDeleteInterceptor());
        }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
