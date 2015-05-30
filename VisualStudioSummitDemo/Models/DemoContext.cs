using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using VisualStudioSummitDemo.Interceptors;
using VisualStudioSummitDemo.Interceptors.MultiTenant;

namespace VisualStudioSummitDemo.Models
{
    public class DemoContext : DbContext
    { 
        public DemoContext()
        {
            Database.Log += s => File.AppendAllText(@"C:\temp\eflog.txt", s);
        }

        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class DemoContextConfiguration : DbConfiguration
    {
        public DemoContextConfiguration()
        {
            //AddInterceptor(new SoftDeleteInterceptor());
            AddInterceptor(new MultiTenantInterceptor());
            AddInterceptor(new MultiTenantTreeInterceptor());
        }
    }
}
