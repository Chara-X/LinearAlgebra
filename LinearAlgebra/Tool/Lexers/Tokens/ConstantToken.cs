namespace LinearAlgebra.Tool.Lexers.Tokens
{
    public class ConstantToken:Token
    {
        public object Value { get; set; }

        public ConstantToken(object value) : base(TokenType.Constant)
        {
            Value = value;
        }
    }
}
