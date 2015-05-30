using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using VisualStudioSummitDemo.Interceptors.MultiTenant.CommandHandlers;

namespace VisualStudioSummitDemo.Interceptors.SoftDelete.CommandHandlers
{
    public class DbDeleteCommandTreeHandler : CommandTreeHandlerBase
    {
        private const string COLUMN_NAME = "Inativo";

        protected override bool CanHandle(DbCommandTree command)
        {
            return command is DbDeleteCommandTree;
        }

        protected override DbCommandTree Handle(DbCommandTree command)
        {
            var deleteCommand = command as DbDeleteCommandTree;
            var column = GetColumn(deleteCommand);

            if (column != null)
            {
                var setClause = DbExpressionBuilder.SetClause(deleteCommand.Target.VariableType.Variable(COLUMN_NAME).Property(column), DbExpression.FromBoolean(true));

                return new DbUpdateCommandTree(
                    deleteCommand.MetadataWorkspace,
                    deleteCommand.DataSpace,
                    deleteCommand.Target,
                    deleteCommand.Predicate,
                    new List<DbModificationClause> { setClause }.AsReadOnly(),
                    null);
            }
            return null;
        }

        private static EdmProperty GetColumn(DbDeleteCommandTree deleteCommand)
        {
            return deleteCommand.Target.VariableType.EdmType.MetadataProperties
                                .Select(x => x.Value)
                                .OfType<IReadOnlyCollection<EdmMember>>()
                                .SelectMany(x => x)
                                .FirstOrDefault(x => x.Name.EndsWith(COLUMN_NAME)) as EdmProperty;
        }
    }
}