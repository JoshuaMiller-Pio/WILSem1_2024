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
        SFXisMuted = true;
        MusicisMuted = true;
    }

    public void SetMusicMute()
    {
        if (MusicisMuted)
        {
            audioMixer.SetFloat(MusicVolumeParam, 0);
        }
        else
        {
            audioMixer.SetFloat(MusicVolumeParam, -80);
        }
    }

    public void SetSFXMute()
    {
        if (SFXisMuted)
        {
            audioMixer.SetFloat(SFXVolumeParam, 0);
        }
        else
        {
            audioMixer.SetFloat(SFXVolumeParam, -80);
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