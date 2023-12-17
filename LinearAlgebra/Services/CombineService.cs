using System.Collections.Generic;
using System.Linq;

namespace LinearAlgebra.Services
{
    public class CombineService
    {
        public IEnumerable<int[]> Compute(int[] source, int n)
        {
            var res = new List<List<int>>();
            Enum(source, 0, n, new List<int>(), res);
            return res.Select(i => i.ToArray());
        }

        private static void Enum(IReadOnlyList<int> source, int index, int n, List<int> current, ICollection<List<int>> result)
        {
            if (n == 0)
            {
                result.Add(current);
                return;
            }
            var count = source.Count - n + 1;
            for (var i = index; i < count; i++)
            {
                var temp = new List<int>(current) {source[i]};
                Enum(source, i + 1, n - 1, temp, result);
            }
        }
    }
}
