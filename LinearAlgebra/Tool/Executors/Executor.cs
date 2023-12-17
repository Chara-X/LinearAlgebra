using System;
using System.Linq;
using LinearAlgebra.Controllers;
using LinearAlgebra.Tool.Parsers.Expressions;

namespace LinearAlgebra.Tool.Executors
{
    public class Executor
    {
        public LinearAlgebraController Controller { get; set; }

        public Executor()
        {
            Controller = new LinearAlgebraController();
        }

        public object Execute(Expression e)
        {
            switch (e.NodeType)
            {
                case ExpressionType.Constant: return (e as ConstantExpression)?.Value;
                case ExpressionType.Subtract: return ExecuteSubtract(e as BinaryExpression);
                case ExpressionType.Add: return ExecuteAdd(e as BinaryExpression);
                case ExpressionType.Multiply: return ExecuteMultiply(e as BinaryExpression);
                case ExpressionType.Call: return ExecuteCall(e as CallExpression);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ExecuteAdd(BinaryExpression e)
        {
            var l = Execute(e.Left);
            var r = Execute(e.Right);
            if (l is double a)
                return a - (double)r;
            return Controller.MatrixAdd((double[][])l, (double[][])r);
        }

        public object ExecuteSubtract(BinaryExpression e)
        {
            var l = Execute(e.Left);
            var r = Execute(e.Right);
            if (l is double a)
                return a - (double)r;
            return Controller.MatrixSubtract((double[][])l, (double[][])r);
        }

        public object ExecuteCall(CallExpression e)
        {
            return e.Method.Invoke(
                Activator.CreateInstance(e.Method.DeclaringType ?? throw new InvalidOperationException()),
                e.Parameters.Select(Execute).ToArray());
        }

        public object ExecuteMultiply(BinaryExpression e)
        {
            return ExecuteMultiply(Execute(e.Left), Execute(e.Right));
        }

        private object ExecuteMultiply(object l, object r)
        {
            if (l is double a)
            {
                switch (r)
                {
                    case double b:
                        return a * b;
                    case double[][] c:
                        return Controller.MatrixMultiply(c, a);
                }
            }

            if (l is double[][] d && r is double[][] e)
                return Controller.MatrixMultiply(d, e);
            return ExecuteMultiply(r, l);
        }
    }
}
