public struct Tetromino
{
    private static int[][,] _matrixes = new[]
    {
        new int[3, 3] // L
        {
            { 0, 0, 0 },
            { 1, 1, 1 },
            { 1, 0, 0 },
        },
        new int[3, 3] // J
        {
            { 0, 0, 0 },
            { 1, 1, 1 },
            { 0, 0, 1 },
        },
        new int[3, 3] // Z
        {
            { 0, 0, 0 },
            { 1, 1, 0 },
            { 0, 1, 1 },
        },
        new int[3, 3] // S
        {
            { 0, 0, 0 },
            { 0, 1, 1 },
            { 1, 1, 0 },
        },
        new int[3, 3] // T
        {
            { 0, 0, 0 },
            { 1, 1, 1 },
            { 0, 1, 0 },
        },
        new int[4, 4] // I
        {
            { 0, 0, 0, 0 },
            { 1, 1, 1, 1 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
        },
        new int[2, 2] // O
        {
            { 1, 1 },
            { 1, 1 },
        },
    };

    public static int[,] GetMatrixByType(int type)
    {
        return type - 1 < _matrixes.Length ? _matrixes[type - 1] : null;
    }

    public static string GetStringByType(int type)
    {
        var matrix = GetMatrixByType(type);
        var result = "";
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i,j] > 0)
                    result += ((char)0x25A0).ToString();
                else
                    result += ((char)0x25A1).ToString();
            }
            result += "\n";
        }
        return result;
    }

    public static int GetRandomType()
    {
        return UnityEngine.Random.Range(0, _matrixes.Length) + 1;
    }
}