using System;
using System.Collections.Generic;
using System.Linq;

namespace LinearAlgebra.Services
{
    public class MatrixReductionService
    {
        public List<Row> Rows { get; set; }
        public int M { get; set; }
        public int N { get; set; }

        public double[][] Compute(double[][] matrix)
        {
            Initialize(matrix);
            for (var j = 0; j < M; j++)
            {
                var aim = SearchSubtracted(j, out var index);
                if (aim == null) continue;
                aim.CanSubtracted = false;
                aim.Data = aim.Data.Select(i => i / aim.Data[j]).ToArray();
                for (var i = 0; i < N; i++)
                {
                    if (i == index) continue;
                    var source = Rows[i].Data;
                    var n = source[j];
                    for (var k = 0; k < M; k++)
                    {
                        source[k] -= n * aim.Data[k];
                    }
                }
            }

            return Rows.Select(i => i.Data).ToArray();
        }

        private void Initialize(IReadOnlyList<double[]> matrix)
        {
            M = matrix[0].Length;
            N = matrix.Count;
            Rows = new List<Row>();
            for (var i = 0; i < N; i++)
            {
                Rows.Add(new Row(matrix[i]));
            }
        }

        private Row SearchSubtracted(int column,out int index)
        {
            for (var i = 0; i < N; i++)
            {
                if (!Rows[i].IsSubtracted(column)) continue;
                index = i;
                return Rows[i];
            }

            index = -1;
            return null;
        }

        public class Row
        {
            public double[] Data { get; set; }

            public bool CanSubtracted { get; set; }

            public Row(double[] data)
            {
                Data = data;
                CanSubtracted = true;
            }

            public bool IsSubtracted(int column) => CanSubtracted && Math.Abs(Data[column]) > 0.00001;
        }
    }
}
