using System.Data.Entity.Core.Common.CommandTrees;

namespace VisualStudioSummitDemo.Interceptors.MultiTenant.CommandHandlers
{
    public class DbDeleteCommandTreeHandler : CommandTreeHandlerBase
    {
        protected override bool CanHandle(DbCommandTree command)
        {
            return command is DbDeleteCommandTree;
        }

        protected override DbCommandTree Handle(DbCommandTree command)
        {
            var dbDeleteCommandTree = command as DbDeleteCommandTree;
            return new DbDeleteCommandTree(dbDeleteCommandTree.MetadataWorkspace,
                dbDeleteCommandTree.DataSpace,
                dbDeleteCommandTree.Target,
                dbDeleteCommandTree.Predicate.Accept(new MultiTenantQueryVisitor()));
        }
    }
}