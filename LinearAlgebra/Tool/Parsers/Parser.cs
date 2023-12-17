using System.Collections.Generic;
using LinearAlgebra.Tool.Lexers;
using LinearAlgebra.Tool.Lexers.Tokens;
using LinearAlgebra.Tool.Parsers.Expressions;

namespace LinearAlgebra.Tool.Parsers
{
    public class Parser
    {
        public Lexer Lexer { get; set; }

        public Parser()
        {
            Lexer = new Lexer();
        }

        public Expression Parse() => ParseAddAndSubtract();

        public Expression ParseAddAndSubtract()
        {
            var e = ParseMultiply();
            while (Lexer.CanMove() && (Lexer.Peek().Type == TokenType.Add || Lexer.Peek().Type == TokenType.Subtract))
            {
                switch (Lexer.Next().Type)
                {
                    case TokenType.Add: e = Expression.Add(e, ParseAddAndSubtract()); break;
                    case TokenType.Subtract: e = Expression.Subtract(e, ParseAddAndSubtract()); break;
                }
            }

            return e;
        }

        public Expression ParseMultiply()
        {
            var e = ParseOperand();
            while (Lexer.CanMove() && Lexer.Peek().Type == TokenType.Multiply)
            {
                Lexer.Next();
                e = Expression.Multiply(e, ParseMultiply());
            }

            return e;
        }

        public Expression ParseOperand()
        {
            if (IsBracket()) return ParseBracket();
            return IsCall() ? ParseCall() : ParseConstant();
        }

        public Expression ParseBracket()
        {
            Lexer.Next();
            var e = Parse();
            Lexer.Next(TokenType.R);
            return e;
        }

        public Expression ParseCall()
        {
            var token = Lexer.Next<CallToken>();
            var es = new List<Expression>();
            Lexer.Next(TokenType.L);
            while (true)
            {
                es.Add(Parse());
                if (Lexer.Peek().Type != TokenType.Separate) break;
                Lexer.Next();
            }

            Lexer.Next(TokenType.R);
            return Expression.Call(token.Method, es.ToArray());
        }

        public Expression ParseConstant()
        {
            var token = Lexer.Next<ConstantToken>();
            return Expression.Constant(token.Value, token.Value.GetType());
        }

        private bool IsBracket() => Lexer.CanMove() && Lexer.Peek().Type == TokenType.L;

        private bool IsCall() => Lexer.CanMove() && Lexer.Peek().Type == TokenType.Call;
    }
}
