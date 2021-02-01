using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
#pragma warning disable 0649
    [Header("Gameplay")]
    [SerializeField] private Canvas _gameplayCanvas;
    [SerializeField] private Text _score;
    [SerializeField] private Text _next;
    [SerializeField] private Animator _gamePlayFadeAnimator;

    [Header("Game Over")]
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private Text _highScore;
    [SerializeField] private Text _overScore;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Animator _gameOverFadeAnimator;
#pragma warning restore 0649

    private Action _onMenuClicked;

    private void Awake()
    {
        _menuButton.onClick.AddListener(OpenMenuScene);
    }

    public void SetScore(int score)
    {
        _score.text = score.ToString();
    }

    public void SetNext(TetrominoType tetrominoType)
    {
        _next.text = Tetromino.GeStringByType(tetrominoType);
    }

    public void SetGameplayCanvasActive(bool value)
    {
        _gamePlayFadeAnimator.SetBool("Open", value);
    }

    public void SetGameOverCanvasActive(bool value, int score, int highScore, Action onMenuClicked = null)
    {
        _onMenuClicked = onMenuClicked;

        _overScore.text = score.ToString();
        _highScore.text = highScore.ToString();

        _gameOverCanvas.gameObject.SetActive(value);
    }

    private void OpenMenuScene()
    {
        _gameOverFadeAnimator.SetBool("Open", false);

        _onMenuClicked?.Invoke();

        IEnumerator Co()
        {
            yield return new WaitForSeconds(1);
            SceneManager.OpenMenuScene();
        }
        StartCoroutine(Co());
    }
}
