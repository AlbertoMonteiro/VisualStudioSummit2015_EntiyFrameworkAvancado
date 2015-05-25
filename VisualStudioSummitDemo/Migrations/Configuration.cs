using VisualStudioSummitDemo.Models;

namespace VisualStudioSummitDemo.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DemoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DemoContext context)
        {
            context.Tenants.AddOrUpdate(new Tenant { Id = 1, Name = "Empresa 1" }, new Tenant { Id = 2, Name = "Empresa 2" });
        }
    }
}
