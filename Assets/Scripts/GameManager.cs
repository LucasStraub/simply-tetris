using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private CanvasController _canvasController;
    [SerializeField] private FogController _fogController;
    [SerializeField] private AudioController _audioController;

    // Settings
    private GameSettings _gameSettings;
    private AudioSettings _audioSettings;

    // Game Matrices
    private int[,] _matrix;
    private TileController[,] _tilesMatrix;

    // Tetromino Values
    private int _nextType;
    private int _curType;
    private int[,] _curMatrix;
    private Vector2Int _curOffset;

    // Score
    private int _score = 0;
    private int _highScore = 0;

    // Input Controlls
    private float _lastDropTime;
    private float _lastHorizontalMoveTime;

    // PlayerPrefs
    private const string HIGH_SCORE_KEY = "HighScore";

    // State
    private bool _gameEnded = false;

    #region Unity
    private void Awake()
    {
        // Loads Resources
        _gameSettings = Resources.Load<GameSettings>("GameSettings");
        _audioSettings = Resources.Load<AudioSettings>("AudioSettings");

        // Iniciates Matrices
        _matrix = new int[_gameSettings.Width, _gameSettings.Height];
        _tilesMatrix = new TileController[_gameSettings.Width, _gameSettings.Height];

        // Loads High Score
        _highScore = LoadHighScore();
    }

    public void Start()
    {
        _fogController.SetActive(true);
        SetScore(0);

        InstatiateTiles();

        RandomizeNextTetromino();
        SpawnNextTetromino();
    }

    private void Update()
    {
        InputUpdate();
    }

    private void OnDrawGizmos()
    {
        if (_gameSettings != null)
        {
            var w = _gameSettings.Width;
            var h = _gameSettings.Height;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(new Vector2(w / 2 - 0.5f, h / 2 - 0.5f), new Vector3(w, h, 1));
        }
    }
    #endregion

    #region Input Handlers
    private void InputUpdate()
    {
        if (_gameEnded)
            return;

        if (Input.GetKey(KeyCode.LeftArrow) && _lastHorizontalMoveTime + 1 / _gameSettings.HorizontalSpeed < Time.timeSinceLevelLoad)
        {
            _lastHorizontalMoveTime = Time.timeSinceLevelLoad;
            Move(Vector2Int.left);
        }

        if (Input.GetKey(KeyCode.RightArrow) && _lastHorizontalMoveTime + 1 / _gameSettings.HorizontalSpeed < Time.timeSinceLevelLoad)
        {
            _lastHorizontalMoveTime = Time.timeSinceLevelLoad;
            Move(Vector2Int.right);
        }

        if (Input.GetKeyDown(KeyCode.X))
            Rotate(true);

        if (Input.GetKeyDown(KeyCode.Z))
            Rotate(false);

        var tilesPerSecond = Input.GetKey(KeyCode.DownArrow) ? _gameSettings.SoftDropSpeed : _gameSettings.DropSpeed;
        var mod = _gameSettings.ScorePerRow / _gameSettings.ScoreSpeedMod;
        tilesPerSecond *= (_score + mod) / mod;

        if (_lastDropTime + 1 / tilesPerSecond < Time.timeSinceLevelLoad)
        {
            _lastDropTime = Time.timeSinceLevelLoad;
            Move(Vector2Int.down, true);
        }
    }
    #endregion

    #region TileBehaviour
    /// <summary>
    /// Instantiate Tiles in the scene and stores it on _matrixTiles
    /// </summary>
    private void InstatiateTiles()
    {
        _matrix.Foreach((o, x, y) =>
        {
            _tilesMatrix[x, y] = Instantiate(_gameSettings.TilePrefab, new Vector2(x, y), Quaternion.identity, transform);
        });
    }

    /// <summary>
    /// Set Tiles as TetrominoType
    /// </summary>
    /// <param name="positions"></param>
    /// <param name="type"></param>
    private void SetTiles(List<Vector2Int> positions, int type)
    {
        positions.ForEach(o => 
        {
            _matrix[o.x, o.y] = type;
            _tilesMatrix[o.x, o.y].SetTile(type);
        });
    }
    #endregion

    #region Tetromino Transformations
    /// <summary>
    /// Randomizes the next tetromino
    /// </summary>
    private void RandomizeNextTetromino()
    {
        _nextType = Tetromino.GetRandomType();
        _canvasController.SetNext(Tetromino.GetStringByType(_nextType));
    }

    /// <summary>
    /// Spawns next tetromino
    /// </summary>
    private void SpawnNextTetromino()
    {
        _curType = _nextType;
        _curMatrix = Tetromino.GetMatrixByType(_nextType);

        RandomizeNextTetromino();

        var matrix = _curMatrix;
        var tOffset = new Vector2Int(matrix.GetLength(0) / 2, 0);

        matrix.Foreach((o, x, y) => 
        {
            if (o > 0 && y > tOffset.y)
                tOffset.y = y;
        });
        
        _curOffset = new Vector2Int(_gameSettings.Width / 2 - tOffset.x,
                                  _gameSettings.Height - tOffset.y - 1);

        var positions = GetFilledPositions(matrix, _curOffset);

        if (AreValidPositions(positions))
            SetTiles(positions, _curType);
        else
            EndGame();
    }

    /// <summary>
    /// Rotates current Tetromino
    /// </summary>
    /// <param name="clockwise"></param>
    private void Rotate(bool clockwise)
    {
        PlaySound(_audioSettings.RotateSound, _audioSettings.RotateSoundVolume);
        MoveAndRotate(_curOffset, _curMatrix.Rotate90(clockwise));
    }

    /// <summary>
    /// Moces current Tetromino
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="snap"></param>
    private void Move(Vector2Int dir, bool snap = false)
    {
        PlaySound(_audioSettings.MoveSound, _audioSettings.MoveSoundVolume);
        MoveAndRotate(_curOffset + dir, _curMatrix, snap);
    }

    /// <summary>
    /// Moves or rotates current Tetromino based on variables
    /// </summary>
    /// <param name="newOffset"></param>
    /// <param name="newMatrix"></param>
    /// <param name="snap"></param>
    private void MoveAndRotate(Vector2Int newOffset, int[,] newMatrix, bool snap = false)
    {
        // Get current piece position
        var curPositions = GetFilledPositions(_curMatrix, _curOffset);

        // Clear current piece position
        SetTiles(curPositions, 0);

        // Get new positions
        var nextPositions = GetFilledPositions(newMatrix, newOffset);

        if (AreValidPositions(nextPositions))
        {
            // Sets new positions with correct index
            SetTiles(nextPositions, _curType);

            _curOffset = newOffset;
            _curMatrix = newMatrix;            
        }
        else
        {
            // Sets original positions with original index
            SetTiles(curPositions, _curType);

            if (snap)
            {
                PlaySound(_audioSettings.SnapSound, _audioSettings.SnapSoundVolume);

                for (var i = newMatrix.GetLength(1) - 1; i >= 0; i--)
                {
                    var index = newOffset.y + 1 + i;
                    if (index >= 0 && index < _gameSettings.Height && _matrix.AllOnRow(index, o => o > 0))
                    {
                        AddScore(_gameSettings.ScorePerRow);
                        PlaySound(_audioSettings.CleanRowSound, _audioSettings.CleanRowSoundVolume);
                        ClearRow(index);
                        MoveMatrixDown(index);
                    }
                }

                SpawnNextTetromino();
            }
        }
    }
    #endregion

    #region Matrix
    /// <summary>
    /// Clears rows by index
    /// </summary>
    /// <param name="index"></param>
    private void ClearRow(int index)
    {
        _matrix.ForeachOnRow(index, o => o = 0);
        _tilesMatrix.ForeachOnRow(index, o => o.SetTile(0));
    }

    /// <summary>
    /// Moves matrix itens down by one, starting by startIndex
    /// </summary>
    /// <param name="startIndex"></param>
    private void MoveMatrixDown(int startIndex)
    {
        for (var i = 0; i < _gameSettings.Width; i++)
        {
            for (var j = startIndex; j < _gameSettings.Height - 1; j++)
            {
                _matrix[i, j] = _matrix[i, j + 1];
                _tilesMatrix[i, j].SetTile(_matrix[i, j + 1]);
            }
            _matrix[i, _gameSettings.Height - 1] = 0;
            _tilesMatrix[i, _gameSettings.Height - 1].SetTile(0);
        }
    }

    /// <summary>
    /// Gets matrix positions if they are filled with a Tetromino tile
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    private List<Vector2Int> GetFilledPositions(int[,] matrix, Vector2Int offset = default)
    {
        var list = new  List<Vector2Int>();
        matrix.Foreach((o, x, y) => 
        {
            if (o > 0)
                list.Add(new Vector2Int(x, y) + offset);
        });
        return list;
    }

    /// <summary>
    /// Cheks if matrix positions are valid positions to be seted
    /// </summary>
    /// <param name="positions"></param>
    /// <returns></returns>
    private bool AreValidPositions(IEnumerable<Vector2Int> positions)
    {
        return positions.All(o => 
            o.x >= 0 && o.x < _gameSettings.Width &&
            o.y >= 0 && o.y < _gameSettings.Height && 
            _matrix[o.x, o.y] < 1);
    }
    #endregion

    #region End
    /// <summary>
    /// Ends Game
    /// </summary>
    private void EndGame()
    {
        _gameEnded = true;

        PlaySound(_audioSettings.GameOverSound, _audioSettings.GameOverSoundVolume);

        _fogController.SetActive(false);
        _canvasController.SetGameplayCanvasActive(false);
        IEnumerator Co()
        {
            yield return new WaitForSeconds(1);
            _canvasController.SetGameOverCanvasActive(true, _score, _highScore, () =>
            {
                _audioController.SetMusicActive(false);
            });
        }
        StartCoroutine(Co());
    }
    #endregion

    #region Sound
    private void PlaySound(AudioClip clip, float volume)
    {
        _audioController.Play(clip, volume);
    }
    #endregion

    #region Scores
    private void AddScore(int scorePerRow)
    {
        SetScore(_score + scorePerRow);
    }

    private void SetScore(int score)
    {
        _score = score;
        if (_score > _highScore)
        {
            _highScore = _score;
            SaveHighScore(score);
        }
        _canvasController.SetScore(score);
    }
    #endregion

    #region Player Prefs
    private int LoadHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE_KEY);
    }

    private void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
        PlayerPrefs.Save();
    }

    [ContextMenu("CleanPlayePrefs")]
    public void CleanPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    #endregion
}