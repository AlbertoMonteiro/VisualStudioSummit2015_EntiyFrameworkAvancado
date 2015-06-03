using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using VisualStudioSummitDemo.Interceptors.Auditoria;
using VisualStudioSummitDemo.Interceptors.MultiTenant;
using VisualStudioSummitDemo.Interceptors.SoftDelete;
using VisualStudioSummitDemo.Migrations.CustomOperations;

namespace VisualStudioSummitDemo.Models
{
    public class DemoContextConfiguration : DbConfiguration
    {
        public DemoContextConfiguration()
        {
            SetMigrationSqlGenerator(SqlProviderServices.ProviderInvariantName, () => new CustomSqlServerMigrationSqlGenerator());
            
            AddInterceptor(new SoftDeleteInterceptor());
            
            AddInterceptor(new MultiTenantInterceptor());
            AddInterceptor(new MultiTenantTreeInterceptor());

            Action<AuditEntry> action = x => Debug.WriteLine(x.ToString(), "AuditingCommandInterceptor");
            AddInterceptor(new AuditingCommandInterceptor(action, ConfigurationManager.ConnectionStrings["DemoContext"].ConnectionString));
        }
    }
}