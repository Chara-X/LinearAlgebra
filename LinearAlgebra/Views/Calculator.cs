using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LinearAlgebra.Attributes;
using LinearAlgebra.Controllers;
using LinearAlgebra.Tool.Executors;
using LinearAlgebra.Tool.Lexers;
using LinearAlgebra.Tool.Lexers.Tokens;
using LinearAlgebra.Tool.Parsers;
using static System.Console;

namespace LinearAlgebra.Views
{
    public class Calculator : Home
    {
        public Executor Executor { get; set; }

        public Parser Parser { get; set; }

        public Lexer Lexer => Parser.Lexer;

        public Dictionary<int, Constant> Constants { get; set; }

        public Dictionary<int, Operator> Operators { get; set; }

        public Dictionary<int, MethodInfo> Methods { get; set; }

        public Calculator()
        {
            Parser = new Parser();
            Executor = new Executor();
            Constants = new Dictionary<int, Constant>();
            Operators = new Dictionary<int, Operator>();
            Methods = new Dictionary<int, MethodInfo>();
            Load();
        }

        public void WriteOperands()
        {
            WriteLine("【操作数列表】");
            WriteLine("【1：\t常量】");
            WriteLine("【2：\t函数】");
            WriteLine("【3：\t括号】");
        }

        public void WriteConstants()
        {
            foreach (var i in Constants)
            {
                WriteLine($"【{i.Key}：\t{i.Value}】");
            }
        }

        public void WriteMethods()
        {
            foreach (var i in Methods)
            {
                WriteLine($"【{i.Key}：\t{ToDisplayName(i.Value)}】");
            }
        }

        public void WriteOperations()
        {
            WriteLine("【运算符列表】");
            foreach (var i in Operators)
            {
                WriteLine($"【{i.Key}：\t{i.Value}】");
            }
        }

        public void WriteParameter(ParameterInfo parameter)
        {
            WriteLine($"[参数]：> {ToDisplayName(parameter)}\n");
        }

        public void WriteResult()
        {
            var res = Executor.Execute(Parser.Parse());
            Lexer.Reset();
            WriteLine("[结果]：");
            if (res is int || res is double)
                WriteLine(res);
            else
                WriteMatrix(res as double[][]);
        }

        public void AddOperand()
        {
            Lexer.Display();
            WriteOperands();
            switch (ReadNumber())
            {
                case 1: AddConstant(); break;
                case 2: AddCall(); break;
                case 3: AddBracket(); break;
            }

            AddOperators();
        }

        public void AddConstant()
        {
            WriteConstants();
            switch (Constants[ReadNumber()])
            {
                case Constant.数字:;Lexer.AddConstant(ReadDouble()); break;
                case Constant.数组: Lexer.AddConstant(ReadArray(true)); break;
                case Constant.矩阵: Lexer.AddConstant(ReadMatrix()); break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AddCall()
        {
            WriteMethods();
            var method = Methods[ReadNumber()];
            Lexer.AddCall(method);
            Lexer.Add(TokenType.L);
            foreach (var i in method.GetParameters())
            {
                WriteParameter(i);
                AddOperand();
                Lexer.Add(TokenType.Separate);
            }

            Lexer.Pop();
            Lexer.Add(TokenType.R);
        }

        public void AddBracket()
        {
            Lexer.Add(TokenType.L);
            AddOperand();
            Lexer.Add(TokenType.R);
        }

        public void AddOperators()
        {
            Lexer.Display();
            WriteOperations();
            switch (Operators[ReadNumber()])
            {
                case Operator.加:Lexer.Add(TokenType.Add); break;
                case Operator.减:Lexer.Add(TokenType.Subtract); break;
                case Operator.乘:Lexer.Add(TokenType.Multiply); break;
                case Operator.返回:return;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            AddOperand();
        }

        public void Load()
        {
            var l = new List<Constant>
            {
                Constant.数字,
                Constant.数组,
                Constant.矩阵,
            };
            var r = new List<Operator>
            {
                Operator.加,
                Operator.减,
                Operator.乘,
                Operator.返回
            };
            for (var i = 0; i < l.Count; i++)
            {
                Constants.Add(i + 1, l[i]);
            }

            for (var i = 0; i < r.Count; i++)
            {
                Operators.Add(i + 1, r[i]);
            }

            var methods = new List<MethodInfo>();
            methods.AddRange(typeof(LinearAlgebraController).GetMethods()
                .Where(i => i.GetCustomAttribute<DisplayNameAttribute>() != null));
            methods.AddRange(typeof(AlgorithmController).GetMethods()
                .Where(i => i.GetCustomAttribute<DisplayNameAttribute>() != null));
            for (var i = 0; i < methods.Count; i++)
            {
                Methods.Add(i + 1, methods[i]);
            }
        }

        public static string ToDisplayName(MemberInfo member) => member.GetCustomAttribute<DisplayNameAttribute>().Name;

        public static string ToDisplayName(ParameterInfo parameter) => parameter.GetCustomAttribute<DisplayNameAttribute>().Name;

        public enum Constant
        {
            数字,
            数组,
            矩阵,
        }

        public enum Operator
        {
            加,
            减,
            乘,
            返回,
        }
    }
}
