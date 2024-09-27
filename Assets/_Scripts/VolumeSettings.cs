using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // PlayerPrefs.DeleteAll(); // Optional: Uncomment if you want to reset settings

        // Load saved volume settings if they exist
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetInitialVolume();
        }

        // Add listeners to sliders to update volume in real-time
        musicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        sfxSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;

        // Call AudioManager's method to set the music volume
        AudioManager.Instance.SetMusicVolume(volume);

        // Save the volume setting
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;

        // Call AudioManager's method to set the SFX volume
        AudioManager.Instance.SetSFXVolume(volume);

        // Save the volume setting
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume()
    {
        // Load saved volume levels
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        // Apply loaded settings
        SetMusicVolume();
        SetSFXVolume();
    }

    private void SetInitialVolume()
    {
        // Define initial volume levels
        float initialMusicVolume = 1.0f;
        float initialSFXVolume = 1.0f;

        // Set sliders to initial levels
        musicSlider.value = initialMusicVolume;
        sfxSlider.value = initialSFXVolume;

        // Apply initial settings
        SetMusicVolume();
        SetSFXVolume();
    }
}
