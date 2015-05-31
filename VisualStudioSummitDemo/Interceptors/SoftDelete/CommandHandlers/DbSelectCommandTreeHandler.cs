using System.Data.Entity.Core.Common.CommandTrees;
using VisualStudioSummitDemo.Interceptors.CommandHandlers;
 
namespace VisualStudioSummitDemo.Interceptors.SoftDelete.CommandHandlers
{
    public class DbSelectCommandTreeHandler : CommandTreeHandlerBase<DbQueryCommandTree>
    {
        protected override bool CanHandle(DbCommandTree command)
        {
            return command is DbQueryCommandTree;
        }

        protected override DbQueryCommandTree Handle(DbCommandTree command)
        {
            var queryCommand = command as DbQueryCommandTree;
            return new DbQueryCommandTree(
                queryCommand.MetadataWorkspace,
                queryCommand.DataSpace,
                queryCommand.Query.Accept(new SoftDeleteQueryVisitor()));
        }
    }
}