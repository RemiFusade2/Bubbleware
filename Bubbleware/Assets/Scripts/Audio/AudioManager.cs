using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton for global access

    [Header ("Audio Mixer Groups")]
    public AudioMixerGroup m_musicMixerGroup = null;
    public AudioMixerGroup m_sfxMixerGroup;
    public AudioMixerGroup m_ambientMixerGroup = null;

    [Header ("Audio Sources")]
    public AudioSource m_musicSource = null;
    public AudioSource m_ambientSource = null;

    public AudioPlayer m_globalSfx = null;

    [Header ("Audio Clips")]
    public List<SceneAudioMapping> sceneAudioMappings; // List of scene-to-audio mappings

    private string currentActiveSceneName = ""; // Keeps track of the currently active scene

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


    // Called when a new scene is detected as active
    public void OnSceneActivated (string sceneName)
    {
        Debug.Log ("Active scene changed to: " + sceneName);
        AudioClip clip = GetMusicClipForScene (sceneName);
        if (clip != null)
        {
            PlayMusic (clip);
        }
        else
        {
            Debug.LogWarning ("No music clip found for scene: " + sceneName);
        }
    }

    // Get the music clip for the active scene
    private AudioClip GetMusicClipForScene (string sceneName)
    {
        foreach (var mapping in sceneAudioMappings)
        {
            if (mapping.sceneName == sceneName)
            {
                return mapping.musicClip;
            }
        }

        return null; // No matching clip found
    }


// Play a looping background music track
public void PlayMusic (AudioClip clip)
    {
        if (clip == null || m_musicSource.clip == clip)
        {
            Debug.Log ("no clips loaded");
            return;
        }
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

    
// Helper class for scene-to-audio mapping
[System.Serializable]
public class SceneAudioMapping
{
    public string sceneName;             // Name of the scene
    public GameObject sceneRootObject;   // Root object of the scene to monitor its active state
    public AudioClip musicClip;          // Music clip for the scene
}


}
