using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace VisualStudioSummitDemo.Interceptors.MultiTenant
{
    public class MultiTenantInterceptor : IDbCommandInterceptor
    {
        public static long TentantId;

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (command.Parameters.Contains("tenantId"))
                command.Parameters["tenantId"].Value = TentantId;
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }
    }
}
