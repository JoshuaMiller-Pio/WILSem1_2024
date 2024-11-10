using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Mixer Reference")]
    public AudioMixer audioMixer;

    private const string MusicVolumeParam = "Music";
    private const string SFXVolumeParam = "SFX";
    public bool SFXisMuted=false, MusicisMuted=false;

    private void Start()
    {
        SFXisMuted = false;
        MusicisMuted = false;
    }

    public void SetMusicMute()
    {
        if (MusicisMuted)
        {
            audioMixer.SetFloat(MusicVolumeParam, 0);
            MusicisMuted = false;
        }
        else
        {
            audioMixer.SetFloat(MusicVolumeParam, -80);
            MusicisMuted = true;
        }
    }

    public void SetSFXMute()
    {
        if (SFXisMuted)
        {
            audioMixer.SetFloat(SFXVolumeParam, 0);
            SFXisMuted = false;
        }
        else
        {
            audioMixer.SetFloat(SFXVolumeParam, -80);
            SFXisMuted = true;
        }
  }


    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(MusicVolumeParam, Mathf.Clamp(volume, -80f, 0f));
    }


    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(SFXVolumeParam, Mathf.Clamp(volume, -80f, 0f));
    }
}