using System;
using System.Collections.Generic;
using System.Linq;

namespace LinearAlgebra.Services
{
    public class MatrixService
    {
        private readonly PermuteService _permuteService;
        private readonly ReverseOrdinalService _ordinalService;
        private readonly CombineService _combineService;

        public MatrixService()
        {
            _permuteService = new PermuteService();
            _ordinalService = new ReverseOrdinalService();
            _combineService = new CombineService();
        }

        public double ComputeDeterminant(double[][] matrix)
        {
            var n = matrix.Length;
            var res = _permuteService.Compute(Enumerable.Range(0, n).ToArray());
            var sum = 0.0;
            foreach (var indices in res)
            {
                var num = (double)(_ordinalService.Compute(indices.Select(i=>(double)i).ToArray()) % 2 == 0 ? 1 : -1);
                for (var i = 0; i < n; i++)
                {
                    var index = indices[i];
                    num *= matrix[i][index];
                }
                sum += num;
            }

            return sum;
        }

        public double ComputeSubItem(double[][] matrix, int row, int col)
        {
            var sub = new List<List<double>>();
            for (var i = 0; i < matrix.Length; i++)
            {
                if (i == row) continue;
                sub.Add(new List<double>());
                for (var j = 0; j < matrix[i].Length; j++)
                {
                    if (j == col) continue;
                    sub.Last().Add(matrix[i][j]);
                }
            }

            return ComputeDeterminant(sub.Select(i => i.ToArray()).ToArray());
        }

        public double ComputeAlgebraSubItem(double[][] matrix, int row, int col)
        {
            return ComputeSubItem(matrix, row, col) * ((row + col) % 2 == 0 ? 1 : -1);
        }

        public double[][] ComputeMultiply(double[][] map1, double[][] map2)
        {
            var n = map1.Length;
            var m = map2[0].Length;
            var s = map1[0].Length;
            var map3 = new double[n][];
            for (var i = 0; i < n; i++)
            {
                map3[i] = new double[m];
                for (var j = 0; j < m; j++)
                {
                    var sum = 0.0;
                    for (var k = 0; k < s; k++)
                    {
                        sum += map1[i][k] * map2[k][j];
                    }

                    map3[i][j] = sum;
                }
            }

            return map3;
        }

        public double[][] ComputeMultiply(double[][] matrix,double k)
        {
            var n = matrix.Length;
            var m = matrix[0].Length;
            var res = new double[n][];
            for (var i = 0; i < n; i++)
            {
                res[i] = new double[m];
                for (var j = 0; j < m; j++)
                {
                    res[i][j] = matrix[i][j] * k;
                }
            }

            return res;
        }

        public double[][] ComputeAdd(double[][] matrix1, double[][] matrix2)
        {
            var n = matrix1.Length;
            var m = matrix1[0].Length;
            var res = new double[n][];
            for (var i = 0; i < n; i++)
            {
                res[i] = new double[m];
                for (var j = 0; j < m; j++)
                {
                    res[i][j] = matrix1[i][j] + matrix2[i][j];
                }
            }

            return res;
        }

        public double[][] ComputeSubtract(double[][] matrix1, double[][] matrix2)
        {
            var n = matrix1.Length;
            var m = matrix1[0].Length;
            var res = new double[n][];
            for (var i = 0; i < n; i++)
            {
                res[i] = new double[m];
                for (var j = 0; j < m; j++)
                {
                    res[i][j] = matrix1[i][j] - matrix2[i][j];
                }
            }

            return res;
        }

        public double[][] ComputeTranspose(double[][] matrix)
        {
            var n = matrix.Length;
            var m = matrix[0].Length;
            var res = new double[m][];
            for (var i = 0; i < m; i++)
            {
                res[i] = new double[n];
                for (var j = 0; j < n; j++)
                {
                    res[i][j] = matrix[j][i];
                }
            }

            return res;
        }

        public double[][] ComputeInverse(double[][] matrix)
        {
            var determinant = ComputeDeterminant(matrix);
            return Math.Abs(determinant) < 0.000001 ? new double[0][] : ComputeAdjacent(matrix).Select(i => i.Select(j => j / determinant).ToArray()).ToArray();
        }

        public double[][] ComputeAdjacent(double[][] matrix)
        {
            var res = new double[matrix.Length][];
            for (var i = 0; i < matrix.Length; i++)
            {
                res[i] = new double[matrix[i].Length];
                for (var j = 0; j < matrix[i].Length; j++)
                {
                    res[i][j] = ComputeAlgebraSubItem(matrix, j, i);
                }
            }

            return res;
        }

        public int ComputeRank(double[][] matrix)
        { 
            var n = matrix.Length;
            var m = matrix[0].Length;
            var min = Math.Min(n, m);
            var rank = 0;
            for (var i = min; i >= 0; i--)
            {
                var r = _combineService.Compute(Enumerable.Range(0, n).ToArray(), i).ToArray();
                var c = _combineService.Compute(Enumerable.Range(0, m).ToArray(), i).ToArray();
                var completed = false;
                foreach (var rows in r)
                {
                    foreach (var cols in c)
                    {
                        var temp = new List<List<double>>();
                        foreach (var row in rows)
                        {
                            temp.Add(new List<double>());
                            foreach (var col in cols)
                            {
                                temp.Last().Add(matrix[row][col]);
                            }
                        }

                        var determinant = ComputeDeterminant(temp.Select(j => j.ToArray()).ToArray());
                        if ((Math.Abs(determinant) < 0.000001)) continue;
                        rank = i;
                        completed = true;
                    }
                }

                if (completed) break;
            }

            return rank;
        }
    }
}
