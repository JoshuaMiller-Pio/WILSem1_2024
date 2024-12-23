using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Canvas References")]
    public GameObject gameOver,gameWin;
    public GameObject pauseMenu;

    [Header("Game Over")]
    public TMP_Text gameOverMessage_UI;
    public string victoryMessage = "Victory";
    public string failureMessage = "Failure";

    [Header("Scenes")]
    public string[] levelScenes;

    public int levelNumber = 0;
    //conclusion
    public bool conc = false;

    [Header("Timer Settings")]
    [Tooltip("The time length of the game in seconds")]
    public int gameLength = 60;

    [Header("Timer References")]
    public TMP_Text timerText;

    [HideInInspector]public bool isGameOver = false;

    public delegate void OnGameEnd(GameFinished finishedType);
    public static OnGameEnd onGameEnd;

    private bool isPaused = false;

    private void OnEnable()
    {
        onGameEnd += GameOver;
    }

    private void OnDisable()
    {
        onGameEnd -= GameOver;
    }

    private new void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void startTimer()
    {
        isGameOver = false;
        if (levelNumber == 0)//If tutorial level, cancel timer
        {
            return;
        }

        StartCoroutine(GameTimer());
    }

    private IEnumerator GameTimer()
    {
        Debug.Log("Game Timer Called");
        int gameTime = gameLength;

        while (gameTime >= 0)
        {
            if (!isPaused && timerText != null)
            {
                timerText.text = gameTime.ToString();
            }

            yield return new WaitForSeconds(1);

            gameTime--;
        }

        onGameEnd?.Invoke(GameFinished.Victory);
    }

    public  void TogglePause()
    {
        if (Instance.isPaused)
        {
            Instance.ResumeGame();
        }
        else
        {
            Instance.PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver(GameFinished finishedType)
    {
        Time.timeScale = 0;
        isGameOver = true;

        StopAllCoroutines();

        if (finishedType == GameFinished.Victory)
        {
            gameOverMessage_UI.text = victoryMessage;
            gameOver.SetActive(true);
        }
        else
        {
            gameOverMessage_UI.text = failureMessage;
            gameOver.SetActive(true);
        }
    }
}

public enum GameFinished
{
    Victory,
    Failure
}
