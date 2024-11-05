using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioSource audio;
    // Start is called before the first frame update
    void OnEnable()
    {
        audio = GetComponent<AudioSource>();
    }

    public void playAudio()
    {
        audio.Play();
    }
    public void LoadnextScene()
    {
        Time.timeScale = 1;
        audio.Play();

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
        audio.Play();
        Debug.Log(GameManager.Instance.levelNumber);
        GameManager.Instance.conc = true;
        GameManager.Instance.levelNumber = 4;
        SceneManager.LoadScene(1);

    }
    public void SelectTut()
    {
        audio.Play();
        SceneManager.LoadScene(1);

    }

    public void SelectT1()
    {
        audio.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        GameManager.Instance.levelNumber = 1;

    }
    public void SelectT2()
    {
        audio.Play();
        Time.timeScale = 1;
        GameManager.Instance.levelNumber = 2;
        Debug.Log(GameManager.Instance.levelNumber);
        SceneManager.LoadScene(1);

    }
    public void SelectT3()
    {
        Time.timeScale = 1;
        audio.Play();
        GameManager.Instance.levelNumber = 3;
        SceneManager.LoadScene(1);

    }
    public void Mmenu()
    {
        audio.Play();
        SceneManager.LoadScene(0);

    }
    public void restart()
    {
        audio.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(GameManager.Instance.levelNumber + 2);

    }
    public void quit()
    {
        audio.Play();
        Application.Quit();

    }

    public void pause()
    {
            audio.Play();
            GameManager.Instance.TogglePause();
        
    }
    public void resume()
    {
        audio.Play();
        GameManager.Instance.ResumeGame();
        
    }

    public void toggleMuteSfx()
    {
        audio.Play();
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
        audio.Play();
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
