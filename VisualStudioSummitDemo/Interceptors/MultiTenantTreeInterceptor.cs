using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

namespace VisualStudioSummitDemo.Interceptors
{
    public class MultiTenantTreeInterceptor : IDbCommandTreeInterceptor
    {
        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
        {
            if (interceptionContext.OriginalResult.DataSpace == DataSpace.SSpace)
            {
                var queryCommand = interceptionContext.Result as DbQueryCommandTree;
                if (queryCommand != null)
                {
                    var keyValuePairs = interceptionContext.Result.Parameters.ToList();

                    var dbQueryCommandTree = new DbQueryCommandTree(
                        queryCommand.MetadataWorkspace,
                        queryCommand.DataSpace,
                        queryCommand.Query.Accept(new MultiTenantQueryVisitor()));

                    interceptionContext.Result = dbQueryCommandTree;
                }

            }
        }
    }
}