public struct Tetromino
{
    public TetrominoType Type;
    public int[,] Matrix;

    public Tetromino(TetrominoType type)
    {
        Type = type;
        Matrix = GetMatrixByType(type).Rotate90();
    }

    public static string GeStringByType(TetrominoType type)
    {
        // Text is set based on font Tetris Blocks chars.
        // The char selected is the invese (rotated 180 degress)
        // of the char wanted, this is due to font alignment.
        //https://www.dafont.com/pt/tetris-blocks.font
        switch (type)
        {
            case TetrominoType.L:
                return "O";
            case TetrominoType.J:
                return "N";
            case TetrominoType.Z:
                return "H";
            case TetrominoType.S:
                return "K";
            case TetrominoType.T:
                return "C";
            case TetrominoType.I:
                return "G";
            case TetrominoType.O:
                return "B";
        }
        return "";
    }

    public static Tetromino Random()
    {
        var rand = UnityEngine.Random.Range(1, 8);
        return new Tetromino((TetrominoType)rand);
    }

    private static int[,] GetMatrixByType(TetrominoType type)
    {
        switch (type)
        {
            case TetrominoType.L:
                return new int[3, 3]
                {
                    { 0, 0, 0 },
                    { 1, 1, 1 },
                    { 1, 0, 0 },
                };
            case TetrominoType.J:
                return new int[3, 3]
                {
                    { 0, 0, 0 },
                    { 1, 1, 1 },
                    { 0, 0, 1 },
                };
            case TetrominoType.Z:
                return new int[3, 3]
                {
                    { 0, 0, 0 },
                    { 1, 1, 0 },
                    { 0, 1, 1 },
                };
            case TetrominoType.S:
                return new int[3, 3]
                {
                    { 0, 0, 0 },
                    { 0, 1, 1 },
                    { 1, 1, 0 },
                };
            case TetrominoType.T:
                return new int[3, 3]
                {
                    { 0, 0, 0 },
                    { 1, 1, 1 },
                    { 0, 1, 0 },
                };
            case TetrominoType.I:
                return new int[4, 4]
                {
                    { 0, 0, 0, 0 },
                    { 1, 1, 1, 1 },
                    { 0, 0, 0, 0 },
                    { 0, 0, 0, 0 },
                };
            case TetrominoType.O:
            default:
                return new int[2, 2]
                {
                    { 1, 1 },
                    { 1, 1 },
                };
        }
    }
}

public enum TetrominoType
{
    None = 0,
    L = 1,
    J = 2,
    Z = 3,
    S = 4,
    T = 5,
    I = 6,
    O = 7,
}