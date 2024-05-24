using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Timer Settings")]
    [Tooltip("The time length of the game in seconds")]
    public int gameLength = 60;

    [Header("Timer References")]
    public TMP_Text timerText;

    public delegate void OnGameEnd();
    public static event OnGameEnd onGameEnd;

    private IEnumerator Start()
    {
        int gameTime = gameLength;
        yield return null;

        while (gameTime > 0)
        {
            yield return new WaitForSeconds(1);

            gameTime--;
            timerText.text = gameTime.ToString();
        }

        onGameEnd?.Invoke();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PlaytestLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
