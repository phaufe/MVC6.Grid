using System;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public class GuidFilter : BaseGridFilter
    {
        public override Expression Apply(Expression expression)
        {
            Object value = null;
            if (String.IsNullOrEmpty(Value))
            {
                if (Nullable.GetUnderlyingType(expression.Type) == null)
                    expression = Expression.Convert(expression, typeof(Nullable<>).MakeGenericType(expression.Type));
            }
            else if ((value = GetTypedValue()) == null)
            {
                return null;
            }

            switch (Method)
            {
                case "equals":
                    return Expression.Equal(expression, Expression.Constant(value, expression.Type));
                case "not-equals":
                    return Expression.NotEqual(expression, Expression.Constant(value, expression.Type));
                default:
                    return null;
            }
        }

        private Object GetTypedValue()
        {
            if (Guid.TryParse(Value, out Guid guid))
                return guid;

            return null;
        }
    }
}
