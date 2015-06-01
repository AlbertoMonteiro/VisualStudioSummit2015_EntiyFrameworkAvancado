using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace VisualStudioSummitDemo.Models
{
    public class DemoContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactDealer> ContactDealers { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
