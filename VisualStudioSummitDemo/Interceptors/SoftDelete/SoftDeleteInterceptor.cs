using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using VisualStudioSummitDemo.Interceptors.SoftDelete.CommandHandlers;

namespace VisualStudioSummitDemo.Interceptors.SoftDelete
{
    public class SoftDeleteInterceptor : IDbCommandTreeInterceptor
    {
        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
        {
            if (interceptionContext.OriginalResult.DataSpace == DataSpace.SSpace)
            {
                var commandTreeHandler = new DbDeleteCommandTreeHandler();
                commandTreeHandler.SetNextHandler(new DbSelectCommandTreeHandler());
                var dbCommandTree = commandTreeHandler.HandleRequest(interceptionContext.Result);
                if (dbCommandTree != null)
                    interceptionContext.Result = dbCommandTree;
            }
        }
    }
}
