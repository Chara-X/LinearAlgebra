using LinearAlgebra.Attributes;
using LinearAlgebra.Services;

namespace LinearAlgebra.Controllers
{
    public class LinearAlgebraController
    {
        private readonly MatrixService _matrix = new MatrixService();
        private readonly MatrixReductionService _matrixReduction = new MatrixReductionService();
        private readonly ReverseOrdinalService _reverseOrdinal = new ReverseOrdinalService();

        [DisplayName("逆序数")]
        public double ReverseOrdinal([DisplayName("数组")]double[] array) => _reverseOrdinal.Compute(array);

        [DisplayName("行列式")]
        public double MatrixDeterminant([DisplayName("矩阵")]double[][] matrix) => _matrix.ComputeDeterminant(matrix);

        [DisplayName("伴随矩阵")]
        public double[][] MatrixAdjacent([DisplayName("矩阵")]double[][] matrix) => _matrix.ComputeAdjacent(matrix);

        [DisplayName("余子项")]
        public double MatrixSubItem([DisplayName("矩阵")]double[][] matrix, [DisplayName("行索引")] double row, [DisplayName("列索引")] double col) => _matrix.ComputeSubItem(matrix, (int)row, (int)col);

        [DisplayName("代数余子项")]
        public double MatrixAlgebraSubItem([DisplayName("矩阵")]double[][] matrix, [DisplayName("行索引")] double row, [DisplayName("列索引")] double col) => _matrix.ComputeAlgebraSubItem(matrix, (int)row, (int)col);

        [DisplayName("转置矩阵")]
        public double[][] MatrixTranspose([DisplayName("矩阵")]double[][] matrix) => _matrix.ComputeTranspose(matrix);

        [DisplayName("逆矩阵")]
        public double[][] MatrixInverse([DisplayName("矩阵")]double[][] matrix) => _matrix.ComputeInverse(matrix);

        [DisplayName("秩")]
        public double MatrixRank([DisplayName("矩阵")]double[][] matrix) => _matrix.ComputeRank(matrix);

        [DisplayName("最简形矩阵")]
        public double[][] MatrixReduction([DisplayName("矩阵")] double[][] matrix) => _matrixReduction.Compute(matrix);

        public double[][] MatrixMultiply(double[][] map1, double[][] map2) => _matrix.ComputeMultiply(map1, map2);

        public double[][] MatrixMultiply(double[][] matrix,double k) => _matrix.ComputeMultiply(matrix, k);

        public double[][] MatrixAdd(double[][] matrix1, double[][] matrix2) => _matrix.ComputeAdd(matrix1, matrix2);

        public double[][] MatrixSubtract(double[][] matrix1, double[][] matrix2) => _matrix.ComputeSubtract(matrix1, matrix2);
    }
}
