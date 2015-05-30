using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

namespace VisualStudioSummitDemo.Interceptors.SoftDelete
{
    public class SoftDeleteInterceptor : IDbCommandTreeInterceptor
    {
        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
        {
            if (interceptionContext.OriginalResult.DataSpace == DataSpace.SSpace)
            {
                var queryCommand = interceptionContext.Result as DbQueryCommandTree;
                if (queryCommand != null)
                {
                    interceptionContext.Result = new DbQueryCommandTree(
                        queryCommand.MetadataWorkspace,
                        queryCommand.DataSpace,
                        queryCommand.Query.Accept(new SoftDeleteQueryVisitor()));
                }

                var deleteCommand = interceptionContext.Result as DbDeleteCommandTree;
                if (deleteCommand != null)
                {
                    var column = deleteCommand.Target.VariableType.EdmType.MetadataProperties
                        .Select(x => x.Value)
                        .OfType<IReadOnlyCollection<EdmMember>>()
                        .SelectMany(x => x)
                        .FirstOrDefault(x => x.Name.EndsWith("Inativo")) as EdmProperty;

                    if (column != null)
                    {
                        var setClause = DbExpressionBuilder.SetClause(deleteCommand.Target.VariableType.Variable("Inativo").Property(column), DbExpression.FromBoolean(true));

                        interceptionContext.Result = new DbUpdateCommandTree(
                            deleteCommand.MetadataWorkspace,
                            deleteCommand.DataSpace,
                            deleteCommand.Target,
                            deleteCommand.Predicate,
                            new List<DbModificationClause> { setClause }.AsReadOnly(),
                            null);
                    }
                }
            }
        }
    }
}
