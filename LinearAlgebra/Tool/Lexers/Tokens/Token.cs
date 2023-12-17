namespace LinearAlgebra.Tool.Lexers.Tokens
{
    public class Token
    {
        public TokenType Type { get; set; }

        public Token(TokenType type)
        {
            Type = type;
        }
    }

    public enum TokenType
    {
        Constant,

        Call,

        Add,
        Subtract,
        Multiply,
        L,
        R,
        Separate,
    }
}