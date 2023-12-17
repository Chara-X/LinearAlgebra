using System;
using LinearAlgebra.Views;
// ReSharper disable FunctionNeverReturns

namespace LinearAlgebra
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var view = new Calculator();
            while (true)
            {
                view.AddOperand();
                view.WriteResult();
                Console.ReadKey();
            }
        }
    }
}
