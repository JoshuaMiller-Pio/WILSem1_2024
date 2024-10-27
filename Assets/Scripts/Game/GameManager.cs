using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Canvas References")]
    public GameObject gameOver;

    [Header("Game Over")]
    public TMP_Text gameOverMessage_UI;
    public string victoryMessage = "Victory";
    public string failureMessage = "Failure";

    public int levelNumber=0;
    public bool conc= false;
    [Header("Timer Settings")]
    [Tooltip("The time length of the game in seconds")]
    public int gameLength = 60;

    [Header("Timer References")]
    public TMP_Text timerText;

    public delegate void OnGameEnd(GameFinished finishedType);
    public static OnGameEnd onGameEnd;

    private void OnEnable()
    {
        onGameEnd += GameOver;
    }

    private void OnDisable()
    {
        onGameEnd -= GameOver;
    }

    private IEnumerator Start()
    {
        Time.timeScale = 1;
        int gameTime = gameLength;

        yield return null;

        while (gameTime >= 0)
        {
            if (timerText != null)
            {
                timerText.text = gameTime.ToString();

            }

            yield return new WaitForSeconds(1);

            gameTime--;
        }

        onGameEnd?.Invoke(GameFinished.Victory);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver(GameFinished finishedType)
    {
        Time.timeScale = 0;

        gameOver.SetActive(true);

        if (finishedType == GameFinished.Victory)
        {
            gameOverMessage_UI.text = victoryMessage;
        }
        else
        {
            gameOverMessage_UI.text = failureMessage;
        }
    }
}

public enum GameFinished
{
    Victory,
    Failure
}
