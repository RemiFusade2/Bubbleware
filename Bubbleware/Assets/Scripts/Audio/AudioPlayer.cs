using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header ("Audio Clips")]
    public AudioClip [] sfxClips; // Array of SFX for different events
    public bool is3D = false;   // Flag for 2D/3D sound

    private AudioSource audioSource;

    private void Awake ()
    {
        audioSource = gameObject.AddComponent<AudioSource> ();
        audioSource.outputAudioMixerGroup = AudioManager.Instance.m_sfxMixerGroup;
        audioSource.spatialBlend = is3D ? 1.0f : 0.0f; // Adjust spatial blend
    }

    // Play a specific sound effect by index
    public void PlaySFX (int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= sfxClips.Length) return;
        audioSource.PlayOneShot (sfxClips [clipIndex]);
    }

    // Play a custom one-shot sound effect
    public void PlaySFX (AudioClip clip)
    {
        if (clip == null) return;
        audioSource.PlayOneShot (clip);
    }
}
