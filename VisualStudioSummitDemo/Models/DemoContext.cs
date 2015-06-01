using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.IO;
using VisualStudioSummitDemo.Interceptors.Auditoria;
using VisualStudioSummitDemo.Interceptors.MultiTenant;
using VisualStudioSummitDemo.Interceptors.SoftDelete;

namespace VisualStudioSummitDemo.Models
{
    public class DemoContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
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
            AddInterceptor(new SoftDeleteInterceptor());
            AddInterceptor(new MultiTenantInterceptor());
            AddInterceptor(new MultiTenantTreeInterceptor());
            AddInterceptor(new AuditingCommandInterceptor(x => Debug.WriteLine(x.ToString(), "AuditingCommandInterceptor"), ConfigurationManager.ConnectionStrings["DemoContext"].ConnectionString));
        }
    }
}
