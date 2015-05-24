using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace VisualStudioSummitDemo.Interceptors
{
    public class SoftDeleteQueryVisitor : DefaultExpressionVisitor
    {
        public override DbExpression Visit(DbScanExpression expression)
        {
            var column = expression.Target.ElementType.Members.SingleOrDefault(x => x.Name.EndsWith("Inativo")) as EdmProperty;
            if (column == null) return base.Visit(expression);
            
            var biding = expression.Bind();
            var naoEstaInativo = biding.VariableType
                .Variable(biding.VariableName)
                .Property(column)
                .NotEqual(DbExpression.FromBoolean(true));
            return biding.Filter(naoEstaInativo);
        }
    }
}