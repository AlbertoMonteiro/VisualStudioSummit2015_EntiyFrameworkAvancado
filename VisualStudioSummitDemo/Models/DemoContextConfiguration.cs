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
            AddInterceptor(new SoftDeleteInterceptor());
            AddInterceptor(new MultiTenantInterceptor());
            AddInterceptor(new MultiTenantTreeInterceptor());
            AddInterceptor(new AuditingCommandInterceptor(x => Debug.WriteLine(x.ToString(), "AuditingCommandInterceptor"), ConfigurationManager.ConnectionStrings["DemoContext"].ConnectionString));
        }
    }
}