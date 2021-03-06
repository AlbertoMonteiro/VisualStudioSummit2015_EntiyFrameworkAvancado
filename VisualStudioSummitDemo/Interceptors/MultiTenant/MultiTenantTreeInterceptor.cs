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
                var commandTreeHandler = CommandTreeFactory.GetCommandTreeHandler(interceptionContext.Result.CommandTreeKind);
                if (commandTreeHandler != null)
                {
                    var dbCommandTree = commandTreeHandler.HandleRequest(interceptionContext.Result);
                    if (dbCommandTree != null)
                        interceptionContext.Result = dbCommandTree;
                }
            }
        }
    }
}