using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Animator _musicAnimator;
    [SerializeField] private Animator _fadeAnimator;
    [SerializeField] private Button _startButton;
#pragma warning restore 0649

    private void Awake()
    {
        _startButton.onClick.AddListener(() => StartGame());
    }

    private void StartGame()
    {
        _musicAnimator.SetBool("Open", false);
        _fadeAnimator.SetBool("Open", false);

        IEnumerator Co()
        {
            yield return new WaitForSeconds(1);
            SceneManager.OpenGameScene();
        }
        StartCoroutine(Co());
    }
}
