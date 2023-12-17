using System;
using System.Reflection;

namespace LinearAlgebra.Tool.Parsers.Expressions
{
    public abstract class Expression
    {
        public ExpressionType NodeType { get; set; }

        public static BinaryExpression Add(Expression left, Expression right) => new BinaryExpression(ExpressionType.Add, left, right);

        public static BinaryExpression Multiply(Expression left, Expression right) => new BinaryExpression(ExpressionType.Multiply, left, right);

        public static BinaryExpression Subtract(Expression left, Expression right) => new BinaryExpression(ExpressionType.Subtract, left, right);

        public static ConstantExpression Constant(object value, Type type) => new ConstantExpression(value, type);

        public static CallExpression Call(MethodInfo method, Expression[] parameters) => new CallExpression(method, parameters);
    }

    public enum ExpressionType
    {
        Constant,
        Add,
        Multiply,
        Subtract,
        Call,
    }
}
