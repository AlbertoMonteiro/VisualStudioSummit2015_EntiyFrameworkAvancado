using System.Data.Entity.Core.Common.CommandTrees;
using VisualStudioSummitDemo.Interceptors.MultiTenant.CommandHandlers;

namespace VisualStudioSummitDemo.Interceptors.SoftDelete.CommandHandlers
{
    public class DbSelectCommandTreeHandler : CommandTreeHandlerBase
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
                queryCommand.Query.Accept(new SoftDeleteQueryVisitor()));
        }
    }
}