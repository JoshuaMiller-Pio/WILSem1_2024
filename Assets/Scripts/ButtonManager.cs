using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadnextScene()
    {
        Time.timeScale = 1;
        if (GameManager.Instance.levelNumber != 4)
        {
            SceneManager.LoadScene(GameManager.Instance.levelNumber + 2);

        }
        else
        {
            Mmenu();
        }
    }
    public void Finale()
    {
        Debug.Log(GameManager.Instance.levelNumber);
        GameManager.Instance.conc = true;
        GameManager.Instance.levelNumber = 4;
        SceneManager.LoadScene(1);

    }
    public void SelectTut()
    {
        SceneManager.LoadScene(1);
    }

    public void SelectT1()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(1);
        
        GameManager.Instance.levelNumber = 1;

    }
    public void SelectT2()
    {
        Time.timeScale = 1;

        GameManager.Instance.levelNumber = 2;
        Debug.Log(GameManager.Instance.levelNumber);
        SceneManager.LoadScene(1);

    }
    public void SelectT3()
    {
        Time.timeScale = 1;

        GameManager.Instance.levelNumber = 3;
        SceneManager.LoadScene(1);

    }
    public void Mmenu()
    {
        
        SceneManager.LoadScene(0);

    }
    public void restart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(GameManager.Instance.levelNumber + 2);

    }
    public void quit()
    {
        Application.Quit();

    }

    public void pause()
    {
        
            GameManager.Instance.TogglePause();
        
    }
    public void resume()
    {
        
        GameManager.Instance.ResumeGame();
        
    }

    public void toggleMuteSfx()
    {
        
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
