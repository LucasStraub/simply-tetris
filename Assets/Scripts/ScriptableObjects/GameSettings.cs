using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
#pragma warning disable 0649
    [Header("Grid")]
    [SerializeField] private int _width = 10;
    public int Width => _width;
    [SerializeField] private int _height = 20;
    public int Height => _height;

    [Header("Tile")]
    [SerializeField] private TileController _tilePrefab;
    public TileController TilePrefab => _tilePrefab;

    [Header("Speed")]
    [Tooltip("Tiles per second")]
    [SerializeField] private float _dropSpeed = 1;
    public float DropSpeed => _dropSpeed;
    [Tooltip("Tiles per second")]
    [SerializeField] private float _softDropSpeed = 10;
    public float SoftDropSpeed => _softDropSpeed;
    [Tooltip("Tiles per second")]
    [SerializeField] private float _horizontalSpeed = 10;
    public float HorizontalSpeed => _horizontalSpeed;

    [Header("Score")]
    [SerializeField] private int _scorePerRow = 100;
    public int ScorePerRow => _scorePerRow;
    [Tooltip("Speed will encrease by for each row cleaned")]
    [SerializeField] private float _scoreSpeedMod = 0.01f;
    public float ScoreSpeedMod => _scoreSpeedMod;
#pragma warning restore 0649
}