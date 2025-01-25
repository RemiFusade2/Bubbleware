using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton for global access

    [Header ("Audio Mixer Groups")]
    public AudioMixerGroup m_musicMixerGroup = null;
    public AudioMixerGroup m_sfxMixerGroup = null;
    public AudioMixerGroup m_ambientMixerGroup = null;

    [Header ("Audio Sources")]
    public AudioSource m_musicSource = null;
    public AudioSource m_ambientSource = null;

    private void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad (gameObject); // Keep AudioManager across scenes
        }
        else
        {
            Destroy (gameObject);
        }
    }

    // Play a looping background music track
    public void PlayMusic (AudioClip clip)
    {
        if (clip == null || m_musicSource.clip == clip) return;
        m_musicSource.clip = clip;
        m_musicSource.loop = true;
        m_musicSource.outputAudioMixerGroup = m_musicMixerGroup;
        m_musicSource.Play ();
    }

    // Play ambient looping sound
    public void PlayAmbient (AudioClip clip)
    {
        if (clip == null || m_ambientSource.clip == clip) return;
        m_ambientSource.clip = clip;
        m_ambientSource.loop = true;
        m_ambientSource.outputAudioMixerGroup = m_ambientMixerGroup;
        m_ambientSource.Play ();
    }

    // Stop the current music or ambient track
    public void StopMusic () => m_musicSource.Stop ();
    public void StopAmbient () => m_ambientSource.Stop ();

    // Set Mixer Volume Levels
    public void SetMusicVolume (float volume) => m_musicMixerGroup.audioMixer.SetFloat ("MusicVolume", Mathf.Log10 (volume) * 20);
    public void SetSFXVolume (float volume) => m_sfxMixerGroup.audioMixer.SetFloat ("SFXVolume", Mathf.Log10 (volume) * 20);
    public void SetAmbientVolume (float volume) => m_ambientMixerGroup.audioMixer.SetFloat ("AmbientVolume", Mathf.Log10 (volume) * 20);
}
