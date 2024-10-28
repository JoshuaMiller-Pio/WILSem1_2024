using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Mixer Reference")]
    public AudioMixer audioMixer;

    private const string MusicVolumeParam = "MusicVolume";
    private const string SFXVolumeParam = "SFXVolume";

    public void SetMusicMute(bool isMuted)
    {
        audioMixer.SetFloat(MusicVolumeParam, isMuted ? -80f : 0f); 
    }

    public void SetSFXMute(bool isMuted)
    {
        audioMixer.SetFloat(SFXVolumeParam, isMuted ? -80f : 0f);
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