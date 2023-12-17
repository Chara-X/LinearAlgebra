using System;

namespace LinearAlgebra.Tool.Parsers.Expressions
{
    public class ConstantExpression : Expression
    {
        public object Value { get; set; }
        public Type Type { get; set; }

        public ConstantExpression(object value, Type type)
        {
            Type = type;
            NodeType = ExpressionType.Constant;
            Value = value;
        }
    }
}