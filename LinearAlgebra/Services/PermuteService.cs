using System.Collections.Generic;
using System.Linq;

namespace LinearAlgebra.Services
{
    public class PermuteService
    {
        public IList<IList<int>> Compute(int[] array) => BackTrack(int.MinValue, array.ToList());

        private static IList<IList<int>> BackTrack(int num, IReadOnlyList<int> candidate)
        {
            if (candidate.Count == 0) return new List<IList<int>>(new List<IList<int>>() { new List<int>() { num } });
            var result = new List<IList<int>>();
            for (var i = 0; i < candidate.Count; i++)
            {
                var temp = new List<int>(candidate);
                temp.RemoveAt(i);
                result.AddRange(BackTrack(candidate[i], temp));
            }

            return result.Select(i =>
            {
                if (num != int.MinValue) i.Insert(0, num);
                return i;
            }).ToList();
        }
    }
}
