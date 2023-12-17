namespace LinearAlgebra.Tool.Parsers.Expressions
{
    public class BinaryExpression : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }

        public BinaryExpression(ExpressionType nodeType, Expression left, Expression right)
        {
            NodeType = nodeType;
            Left = left;
            Right = right;
        }
    }
}