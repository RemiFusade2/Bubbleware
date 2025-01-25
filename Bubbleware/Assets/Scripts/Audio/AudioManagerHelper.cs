using UnityEngine;

public static class AudioManagerHelper
{
    // Helper function to play SFX from a specific AudioPlayer
    public static void PlaySFXAtPosition (AudioClip clip, Vector3 position, float volume = 1.0f)
    {
        if (clip == null) return;

        GameObject tempGO = new GameObject ("TempAudio");
        tempGO.transform.position = position;

        AudioSource tempSource = tempGO.AddComponent<AudioSource> ();
        tempSource.clip = clip;
        tempSource.spatialBlend = 1.0f; // Ensure it's 3D
        tempSource.volume = volume;
        tempSource.outputAudioMixerGroup = AudioManager.Instance.m_sfxMixerGroup;

        tempSource.Play ();
        GameObject.Destroy (tempGO, clip.length); // Destroy after clip finishes
    }
}
