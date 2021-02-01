using System;

public static class MatrixExtensions
{
    public static T[,] Transpose<T>(this T[,] matrix)
    {
        var width = matrix.GetLength(0);
        var height = matrix.GetLength(1);
        var result = new T[height, width];

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                result[j, i] = matrix[i, j];
            }
        }
        return result;
    }

    public static T[,] FlipVertical<T>(this T[,] matrix)
    {
        var width = matrix.GetLength(0);
        var height = matrix.GetLength(1);
        var result = new T[width, height];

        for (var i = 0; i < width; i++)
        {
            for (int j = 0, k = height - 1; j <= k; j++, k--)
            {
                result[i, j] = matrix[i, k];
                result[i, k] = matrix[i, j];
            }
        }
        return result;
    }

    public static T[,] Rotate90<T>(this T[,] matrix, bool clockwise = true)
    {
        if (clockwise)
            return matrix.Transpose().FlipVertical();
        else
            return matrix.FlipVertical().Transpose();
    }

    //public static string ToMatrixString<T>(this T[,] matrix)
    //{
    //    var result = "";
    //    for (int j = 0; j < matrix.GetLength(1); j++)
    //    {
    //        for (int i = 0; i < matrix.GetLength(0); i++)
    //        {
    //            result += matrix[i, j] + ", ";
    //        }
    //        result += "\n";
    //    }
    //    return result;
    //}

    public static void Foreach<T>(this T[,] matrix, Action<T, int, int> action)
    {
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                action.Invoke(matrix[i, j], i, j);
            }
        }
    }

    public static void ForeachOnRow<T>(this T[,] matrix, int index, Action<T> action)
    {
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            action.Invoke(matrix[i, index]);
        }
    }

    //public static void ForeachOnColumn<T>(this T[,] matrix, int index, Action<T> action)
    //{
    //    for (var i = 0; i < matrix.GetLength(0); i++)
    //    {
    //        action.Invoke(matrix[index, i]);
    //    }
    //}

    //public static bool AnyOnRow<T>(this T[,] matrix, int index, Func<T, bool> predicate)
    //{
    //    for (var i = 0; i < matrix.GetLength(0); i++)
    //    {
    //        if (predicate(matrix[index, i]))
    //            return true;
    //    }
    //    return false;
    //}

    public static bool AllOnRow<T>(this T[,] matrix, int index, Func<T, bool> predicate) // x > 0
    {
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            if (!predicate(matrix[i, index]))
                return false;
        }
        return true;
    }
}