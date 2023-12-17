using System;
using System.Collections.Generic;
using System.Reflection;
using LinearAlgebra.Tool.Lexers.Tokens;
using LinearAlgebra.Views;
using CallToken = LinearAlgebra.Tool.Lexers.Tokens.CallToken;

using static System.Console;

namespace LinearAlgebra.Tool.Lexers
{
    public class Lexer
    {
        private List<Token> _ts = new List<Token>();

        private int _p;

        public void Reset()
        {
            _ts = new List<Token>();
            _p = 0;
        }

        public void Add(TokenType type)
        {
            _ts.Add(new Token(type));
        }

        public void AddConstant(object value)
        {
            _ts.Add(new ConstantToken(value));
        }

        public void AddCall(MethodInfo method)
        {
            _ts.Add(new CallToken(method));
        }

        public void Pop()
        {
            _ts.RemoveAt(_ts.Count - 1);
        }

        public void Display()
        {
            Write("[当前运算式]：>\t");
            foreach (var i in _ts)
            {
                switch (i.Type)
                {
                    case TokenType.Constant:
                        var value = (i as ConstantToken)?.Value;
                        if (value is double)
                            Write(value);
                        else
                            Write("[A]");
                        break;
                    case TokenType.Call:     Write(Calculator.ToDisplayName((i as CallToken)?.Method)); break;
                    case TokenType.Add:      Write(" + "); break;
                    case TokenType.Subtract: Write(" - "); break;
                    case TokenType.Multiply: Write(" * "); break;
                    case TokenType.L:        Write("("); break;
                    case TokenType.R:        Write(")"); break;
                    case TokenType.Separate: Write(" , "); break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            WriteLine("\n");
        }

        public Token Next() => _ts[_p++];

        public T Next<T>() where T : Token => (T) _ts[_p++];

        public Token Next(TokenType type)
        {
            if (Peek().Type != type) throw new TargetParameterCountException();
            return Next();
        }

        public Token Peek() => _ts[_p];

        public bool CanMove() => _p < _ts.Count;
    }
}
