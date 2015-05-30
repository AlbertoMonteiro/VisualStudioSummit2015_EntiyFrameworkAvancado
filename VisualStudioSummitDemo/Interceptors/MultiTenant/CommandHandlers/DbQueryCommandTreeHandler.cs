using System.Data.Entity.Core.Common.CommandTrees;

namespace VisualStudioSummitDemo.Interceptors.MultiTenant.CommandHandlers
{
    public class DbQueryCommandTreeHandler : CommandTreeHandlerBase
    {
        protected override bool CanHandle(DbCommandTree command)
        {
            return command is DbQueryCommandTree;
        }

        protected override DbCommandTree Handle(DbCommandTree command)
        {
            var queryCommand = command as DbQueryCommandTree;
            return new DbQueryCommandTree(
                queryCommand.MetadataWorkspace,
                queryCommand.DataSpace,
                queryCommand.Query.Accept(new MultiTenantQueryVisitor()));
        }
    }
}