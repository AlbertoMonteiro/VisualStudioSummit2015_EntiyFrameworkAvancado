using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using VisualStudioSummitDemo.Interceptors.MultiTenant.CommandHandlers;

namespace VisualStudioSummitDemo.Interceptors.MultiTenant
{
    public class MultiTenantTreeInterceptor : IDbCommandTreeInterceptor
    {
        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
        {
            if (interceptionContext.OriginalResult.DataSpace == DataSpace.SSpace)
            {
                var commandTreeHandler = new DbSelectCommandTreeHandler();
                commandTreeHandler.SetNextHandler(new DbDeleteCommandTreeHandler())
                                  .SetNextHandler(new DbInsertCommandTreeHandler())
                                  .SetNextHandler(new DbUpdateCommandTreeHandler());
                var dbCommandTree = commandTreeHandler.HandleRequest(interceptionContext.Result);
                if (dbCommandTree != null)
                    interceptionContext.Result = dbCommandTree;
            }
        }
    }
}