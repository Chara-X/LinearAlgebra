using System;
using LinearAlgebra.Attributes;

namespace LinearAlgebra.Controllers
{
    public class AlgorithmController
    {
        public LinearAlgebraController Controller { get; set; }

        public AlgorithmController()
        {
            Controller = new LinearAlgebraController();
        }

        [DisplayName("幂")]
        public object Power([DisplayName("底数")] object l, [DisplayName("次数")] double n)
        {
            if (l is double a) return Math.Pow(a, n);
            var res = (l as double[][])?.Clone();
            var target = (double[][])l;
            for (var i = 0; i < n - 1; i++)
            {
                res = ExecuteMultiply(res, target);
            }

            return res;
        }

        private object ExecuteMultiply(object l, object r)
        {
            if (l is double a)
            {
                switch (r)
                {
                    case double b:
                        return a * b;
                    case double[][] c:
                        return Controller.MatrixMultiply(c, a);
                }
            }

            if (l is double[][] d && r is double[][] e)
                return Controller.MatrixMultiply(d, e);
            return ExecuteMultiply(r, l);
        }
    }
}
