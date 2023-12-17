using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace LinearAlgebra.Views
{
    public class Home
    {
        public void WriteMatrix(double[][] matrix)
        {
            foreach (var i in matrix)
            {
                foreach (var j in i)
                {
                    Write(j + "\t");
                }

                WriteLine();
            }
        }

        public double[][] ReadMatrix()
        {
            WriteLine("[输入矩阵]：");
            var res = new List<double[]>();
            while (true)
            {
                var array = ReadArray(false);
                if (array.Length == 0) break;
                res.Add(array);
            }

            return res.ToArray();
        }

        public double[] ReadArray(bool tip)
        {
            if (tip) WriteLine("[输入数组]：");
            return ReadLine()?.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
        }

        public int ReadNumber()
        {
            WriteLine("[输入数字]");
            return int.Parse(ReadLine() ?? throw new InvalidOperationException());
        }

        public double ReadDouble()
        {
            WriteLine("[输入数字]");
            return double.Parse(ReadLine() ?? throw new InvalidOperationException());
        }
    }
}
