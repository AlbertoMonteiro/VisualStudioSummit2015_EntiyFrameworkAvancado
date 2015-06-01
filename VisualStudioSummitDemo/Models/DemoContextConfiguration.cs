using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using VisualStudioSummitDemo.Interceptors.Auditoria;
using VisualStudioSummitDemo.Interceptors.MultiTenant;
using VisualStudioSummitDemo.Interceptors.SoftDelete;

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