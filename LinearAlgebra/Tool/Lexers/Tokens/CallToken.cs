using System.Reflection;

namespace LinearAlgebra.Tool.Lexers.Tokens
{
    public class CallToken : Token
    {
        public MethodInfo Method { get; set; }

        public CallToken(MethodInfo method) : base(TokenType.Call)
        {
            Method = method;
        }
    }
}
