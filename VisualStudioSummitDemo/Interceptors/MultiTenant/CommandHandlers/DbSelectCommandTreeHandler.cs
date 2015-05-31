using System.Data.Entity.Core.Common.CommandTrees;
using VisualStudioSummitDemo.Interceptors.CommandHandlers;

namespace VisualStudioSummitDemo.Interceptors.MultiTenant.CommandHandlers
{
    public class DbSelectCommandTreeHandler : ICommandTreeHandler<DbCommandTree>
    {
        public DbCommandTree HandleRequest(DbCommandTree queryCommand)
        {
            var queryCommandTree = queryCommand as DbQueryCommandTree;
            return new DbQueryCommandTree(
                queryCommandTree.MetadataWorkspace,
                queryCommandTree.DataSpace,
                queryCommandTree.Query.Accept(new MultiTenantQueryVisitor()));
        }
    }
}