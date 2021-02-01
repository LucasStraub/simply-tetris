using UnityEngine;

public static class SceneManager
{
    public static void OpenGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public static void OpenMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
