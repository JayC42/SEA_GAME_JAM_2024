using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Instance
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    #region Fields
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;
    private float musicVolume = 1;
    // Multiple musics
    private bool firstMusicSourceIsActive;
    #endregion
    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetSFXVolume()
    {
        return sfxSource.volume;
    }
    private void Awake()
    {
        // Keep this instance alive throughout the whole game
        DontDestroyOnLoad(this.gameObject);

        // Create audio sources, and save them as references
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource2 = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        // Make sure to enable loop on music sources
        musicSource.loop = true;
        musicSource2.loop = true;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }
    public void PlayMusicWithFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        // Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsActive) ? musicSource : musicSource2;

        StartCoroutine(UpdateMusicWithFade(activeSource, musicClip, transitionTime));
    }
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        // Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsActive) ? musicSource : musicSource2;
        AudioSource newSource = (firstMusicSourceIsActive) ? musicSource2 : musicSource;

        // Swap the source
        firstMusicSourceIsActive = !firstMusicSourceIsActive;

        // Set the fields of the audio source, then start the coroutine to crossfade
        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, musicClip, transitionTime));
    }
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip music, float transitionTime)
    {
        // Make sure the source is active and playing
        if (!activeSource.isPlaying)
            activeSource.Play();

        float t = 0.0f;

        // Fade out
        for (t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (musicVolume - ((t / transitionTime) * musicVolume));
            yield return null;
        }

        // Swap to the new music clip
        activeSource.Stop();
        activeSource.clip = music;
        // Gotta restart after changing the clip
        activeSource.Play();

        // Fade in
        for (t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime) * musicVolume;
            yield return null;
        }

        // Make sure we don't end up with a weird float value
        activeSource.volume = musicVolume;
    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, AudioClip music, float transitionTime)
    {
        // Make sure the source is active and playing
        if (!original.isPlaying)
            original.Play();

        newSource.Stop();
        newSource.clip = music;
        newSource.Play();

        float t = 0.0f;

        for (t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        {
            original.volume = (musicVolume - ((t / transitionTime) * musicVolume));
            newSource.volume = (t / transitionTime) * musicVolume;
            yield return null;
        }

        // Make sure we don't end up with a weird float value
        original.volume = 0;
        newSource.volume = musicVolume;

        original.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource == null || musicSource2 == null)
        {
            Debug.LogError("Audio sources are not initialized.");
            return; // Early exit if audio sources are not initialized
        }

        musicVolume = volume;
        musicSource.volume = volume;
        musicSource2.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource == null)
        {
            Debug.LogError("SFX source is not initialized.");
            return; // Early exit if the SFX source is not initialized
        }

        sfxSource.volume = volume;
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    }
    public void PauseMusic()
    {
        musicSource.Pause();
        musicSource2.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
        musicSource2.clip = null; // Reset the second music source
    }
    public void StopMusicPreview()
    {
        musicSource2.clip = null;
        musicSource2.Stop();
        musicSource.UnPause();
    }
    public void StopAllMusic()
    {
        musicSource.Stop();
        musicSource2.Stop();
        musicSource.clip = null;
        musicSource2.clip = null;
    }

}