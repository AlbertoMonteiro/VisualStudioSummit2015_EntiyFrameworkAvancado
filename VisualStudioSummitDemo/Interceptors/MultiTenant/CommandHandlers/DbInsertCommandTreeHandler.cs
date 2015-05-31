using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;

namespace VisualStudioSummitDemo.Interceptors.MultiTenant.CommandHandlers
{
    public class DbInsertCommandTreeHandler : CommandTreeHandlerBase
    {
        private const string COLUMN_NAME = "TenantId";

        protected override bool CanHandle(DbCommandTree command)
        {
            return command is DbInsertCommandTree;
        }

        protected override DbCommandTree Handle(DbCommandTree command)
        {
            var insertCommandTree = command as DbInsertCommandTree;

            var dbSetClauses = insertCommandTree.SetClauses.Cast<DbSetClause>().ToList();

            var setClause = dbSetClauses.FirstOrDefault(HasColumn);
            if (setClause != null)
            {
                dbSetClauses.RemoveAll(HasColumn);
                dbSetClauses.Add(CreateSetClause(setClause));

                return new DbInsertCommandTree(insertCommandTree.MetadataWorkspace,
                    insertCommandTree.DataSpace,
                    insertCommandTree.Target,
                    dbSetClauses.Cast<DbModificationClause>().ToList().AsReadOnly(),
                    insertCommandTree.Returning);
            }
            return insertCommandTree;
        }

        private static DbSetClause CreateSetClause(DbSetClause expression)
        {
            var propertyExpression = expression.Property as DbPropertyExpression;
            return DbExpressionBuilder.SetClause(propertyExpression, expression.Value.Accept(new MultiTenantQueryVisitor(MultiTenantInterceptor.TentantId)));
            /* Fazendo dessa forma o SqlCommand vai ter o parametro "tenantId" duplicado.
             * var propertyExpression = expression.Property as DbPropertyExpression;
             * return DbExpressionBuilder.SetClause(propertyExpression, propertyExpression.ResultType.Parameter("tenantId"));
             */
        }

        private static bool HasColumn(DbSetClause setClause)
        {
            return (setClause.Property as DbPropertyExpression).Property.Name == COLUMN_NAME;
        }
    }
}