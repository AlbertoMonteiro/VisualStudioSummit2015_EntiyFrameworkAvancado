using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using VisualStudioSummitDemo.Interceptors.CommandHandlers;

namespace VisualStudioSummitDemo.Interceptors.MultiTenant.CommandHandlers
{
    public class DbInsertCommandTreeHandler : ICommandTreeHandler<DbCommandTree>
    {
        private const string COLUMN_NAME = "TenantId";

        public DbCommandTree HandleRequest(DbCommandTree commandTree)
        {
            var insertCommandTree = commandTree as DbInsertCommandTree;
            var dbSetClauses = insertCommandTree.SetClauses.Cast<DbSetClause>().ToList();

            var setClause = dbSetClauses.FirstOrDefault(HasColumn);
            if (setClause != null)
            {
                dbSetClauses.RemoveAll(HasColumn);
                dbSetClauses.Add(CreateSetClause(setClause));

                return new DbInsertCommandTree(commandTree.MetadataWorkspace,
                                               commandTree.DataSpace,
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