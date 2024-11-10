using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playAudio()
    {
        audioSource.Play();
    }
    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        audioSource.Play();

        if (GameManager.Instance.levelNumber != 4)
        {
            Debug.Log(GameManager.Instance.levelNumber);
            SceneManager.LoadScene(GameManager.Instance.levelScenes[GameManager.Instance.levelNumber]);
        }
        else
        {
            Mmenu();
        }
    }

    public void LoadNextComic()
    {
        GameManager.Instance.levelNumber++;
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        if (GameManager.Instance.levelNumber == 0)
        {
            GameManager.Instance.levelNumber = 1;
        }
        else if (GameManager.Instance.levelNumber == 1)
        {
            GameManager.Instance.levelNumber = 2;
        }
        else if (GameManager.Instance.levelNumber == 2)
        {
            GameManager.Instance.levelNumber = 3;
        }
        else if (GameManager.Instance.levelNumber == 3)
        {
            GameManager.Instance.levelNumber = 4;
        }

        SceneManager.LoadScene(1);
    }


    public void Finale()
    {
        audioSource.Play();
        Debug.Log(GameManager.Instance.levelNumber);
        GameManager.Instance.conc = true;
        GameManager.Instance.levelNumber = 5;
        SceneManager.LoadScene(1);

    }
    public void SelectTut()
    {
        audioSource.Play();
        GameManager.Instance.levelNumber = 0;
        SceneManager.LoadScene(1);

    }

    public void PlaySelectedLevel()
    {
        SceneManager.LoadScene(GameManager.Instance.levelNumber);
    }

    public void SelectT1()
    {
        audioSource.Play();
        Time.timeScale = 1;
        GameManager.Instance.levelNumber = 1;
        SceneManager.LoadScene(1);

    }
    public void SelectT2()
    {
        audioSource.Play();
        Time.timeScale = 1;
        GameManager.Instance.levelNumber = 2;
        SceneManager.LoadScene(1);

    }
    public void SelectT3()
    {
        audioSource.Play();
        Time.timeScale = 1;
        GameManager.Instance.levelNumber = 3;
        SceneManager.LoadScene(1);

    }
    public void Mmenu()
    {
        audioSource.Play();
        SceneManager.LoadScene(0);

    }
    public void restart()
    {
        audioSource.Play();
        GameManager.Instance.GameOver(GameFinished.Failure);
        Time.timeScale = 1;
        SceneManager.LoadScene(GameManager.Instance.levelScenes[GameManager.Instance.levelNumber]);
    }

    public void quit()
    {
        audioSource.Play();
        Application.Quit();

    }

    public void pause()
    {
        if (GameManager.Instance.isGameOver)
        {
            Debug.Log("Game is Over, Not Pausing");
            return;
        }

        audioSource.Play();
        GameManager.Instance.TogglePause();
    }

    public void resume()
    {
        audioSource.Play();
        GameManager.Instance.ResumeGame();
        
    }

    public void toggleMuteSfx()
    {
        audioSource.Play();
        if (AudioManager.Instance.SFXisMuted)
        {
            AudioManager.Instance.SFXisMuted =false;
        }
        else
        {
            AudioManager.Instance.SFXisMuted = true;
        }
        AudioManager.Instance.SetSFXMute();
    }
    
    public void toggleMuteMusic()
    {
        Debug.Log(AudioManager.Instance.MusicisMuted);
        audioSource.Play();
        if (AudioManager.Instance.MusicisMuted)
        {
            AudioManager.Instance.MusicisMuted =false;
        }
        else
        {
            AudioManager.Instance.MusicisMuted = true;
        }

        AudioManager.Instance.SetMusicMute();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
