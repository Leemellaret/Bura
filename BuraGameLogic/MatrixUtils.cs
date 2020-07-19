using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuraGameLogic
{
    static class MatrixUtils
    {
        public static bool HasFullDiagonal(bool[][] matrix)
        {
            int c = matrix.Length;
            bool[] usedRows = new bool[c];
            int[] costsOfColumns = GetCostsOfColumns(matrix);

            for (int i = 0; i < c; i++)
            {
                int index = FindLowestSatisfyingRowIndex(matrix, i, usedRows, costsOfColumns);
                if (index == -1)
                    return false;
                usedRows[index] = true;
            }

            return true;
        }

        static int FindLowestSatisfyingRowIndex(bool[][] matrix, int column, bool[] usedRows, int[] costsOfColumns)
        {
            int minMeasure = costsOfColumns.Sum()+1;
            int indexOfMin = -1;
            for (int i = 0; i < matrix.Length; i++)
            {
                if (!matrix[i][column] || usedRows[i]) continue;

                int measure = MeasureOfRow(matrix, i, column, costsOfColumns);
                if (measure < minMeasure)
                {
                    minMeasure = measure;
                    indexOfMin = i;
                }
            }

            return indexOfMin;
        }

        static int MeasureOfRow(bool[][] matrix, int row, int startColumn, int[] costsOfColumns)
        {
            int measure = 0;
            for (int i = startColumn; i < matrix.Length; i++)
            {
                measure += (matrix[row][i] ? 1 : 0) * costsOfColumns[i];
            }

            return measure;
        }

        static int[] GetCostsOfColumns(bool[][] matrix)
        {
            int len = matrix.Length;

            int[] costs = new int[len];
            for (int i = 0; i < len; i++)
                costs[i] = 1;

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    costs[j] += matrix[i][j] ? 0 : (1 + j);
                }
            }

            return costs;
        }
    }
}
