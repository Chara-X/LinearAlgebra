using System.Reflection;

namespace LinearAlgebra.Tool.Parsers.Expressions
{
    public class CallExpression : Expression
    {
        public MethodInfo Method { get; set; }

        public Expression[] Parameters { get; set; }

        public CallExpression(MethodInfo method, Expression[] parameters)
        {
            NodeType = ExpressionType.Call;
            Method = method;
            Parameters = parameters;
        }
    }
}
