using System.Data.Entity.Core.Common.CommandTrees;
using VisualStudioSummitDemo.Interceptors.CommandHandlers;

namespace VisualStudioSummitDemo.Interceptors.MultiTenant.CommandHandlers
{
    public class DbDeleteCommandTreeHandler : ICommandTreeHandler<DbCommandTree>
    {
        public DbCommandTree HandleRequest(DbCommandTree commandTree)
        {
            var deleteCommandTree = commandTree as DbDeleteCommandTree;
            return new DbDeleteCommandTree(deleteCommandTree.MetadataWorkspace,
                deleteCommandTree.DataSpace,
                deleteCommandTree.Target,
                deleteCommandTree.Predicate.Accept(new MultiTenantQueryVisitor()));
        }
    }
}