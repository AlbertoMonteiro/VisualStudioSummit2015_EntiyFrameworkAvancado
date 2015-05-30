using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace VisualStudioSummitDemo.Interceptors.MultiTenant
{
    public class MultiTenantQueryVisitor : DefaultExpressionVisitor
    {
        private readonly long id;

        public MultiTenantQueryVisitor(long id = 0)
        {
            this.id = id;
        }

        public override DbExpression Visit(DbScanExpression expression)
        {
            var column = expression.Target.ElementType.Members.SingleOrDefault(x => x.Name.EndsWith("TenantId")) as EdmProperty;
            if (column == null) return base.Visit(expression);

            var biding = expression.Bind();
            var porEmpresa = biding.VariableType
                .Variable(biding.VariableName)
                .Property(column)
                .Equal(column.TypeUsage.Parameter("tenantId"));
            return biding.Filter(porEmpresa);
        }

        public override DbExpression Visit(DbConstantExpression expression)
        {
            return id != 0 ? DbExpressionBuilder.Constant(id) : expression;
        }
    }
}