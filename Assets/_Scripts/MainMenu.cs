using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

// Define the MenuPanel enum
public enum MenuPanel
{
    MainMenu,
    GameMode,
    Settings,
    SongSelect,
    DifficultySelect
}

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gameModePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject songSelectMenuPanel;
    [SerializeField] private GameObject difficultySelectPanel;
    public Dictionary<MenuPanel, GameObject> menuPanels = new Dictionary<MenuPanel, GameObject>();

    [Header("Panel Selection")]
    public MenuPanel currentPanel; // This will show up in the Inspector

    [Header("Sounds")]
    public AudioClip mmMusic;
    public AudioClip buttonClickSFX;
    public AudioClip gameStartClickSFX;
    private Coroutine previewCoroutine;
    private void Awake()
    {
        // Populate the dictionary with references to the panels
        menuPanels[MenuPanel.MainMenu] = mainMenuPanel;
        menuPanels[MenuPanel.GameMode] = gameModePanel;
        menuPanels[MenuPanel.Settings] = settingsPanel;
        menuPanels[MenuPanel.SongSelect] = songSelectMenuPanel;
        menuPanels[MenuPanel.DifficultySelect] = difficultySelectPanel;
    }
    private void Start()
    {
        AudioManager.Instance.PlayMusic(mmMusic);
        ShowMainMenu();
        LoadVolumeSettings();
    }

    private void OnDisable()
    {
        SaveVolumeSettings();
    }

 
    private void SetActivePanel(MenuPanel activePanel)
    {
        foreach (var panel in menuPanels)
        {
            menuPanels[panel.Key].SetActive(panel.Key == activePanel);
        }
        currentPanel = activePanel; // Update the current panel for reference
    }
    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("musicVolume");
            float sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
            AudioManager.Instance.SetMusicVolume(musicVolume);
            AudioManager.Instance.SetSFXVolume(sfxVolume);
        }
    }

    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("musicVolume", AudioManager.Instance.GetMusicVolume());
        PlayerPrefs.SetFloat("sfxVolume", AudioManager.Instance.GetSFXVolume());
        PlayerPrefs.Save();
    }

    public void ShowMainMenu()
    {
        SetActivePanel(MenuPanel.MainMenu);
    }

    public void ShowGameModePanel()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        SetActivePanel(MenuPanel.GameMode);
    }

    public void OnSettingsButtonClicked()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        SetActivePanel(MenuPanel.Settings);
    }

    public void OnPlayButtonClicked()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        ShowGameModePanel();
    }

    public void OnQuitButtonClicked()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        Application.Quit();
    }

    private void OnBackButtonClicked(MenuPanel panelToShow)
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);

        foreach (var panel in menuPanels.Values)
        {
            panel.SetActive(false);
        }

        menuPanels[panelToShow].SetActive(true);
    }

    public void OnBackToMainMenu()
    {
        OnBackButtonClicked(MenuPanel.MainMenu);
    }

    public void OnBackToGameMode()
    {
        OnBackButtonClicked(MenuPanel.GameMode);
    }

    public void OnBackToSettings()
    {
        OnBackButtonClicked(MenuPanel.Settings);
    }

    public void OnBackToSongSelect()
    {
        OnBackButtonClicked(MenuPanel.SongSelect);
    }

    public void OnBackToDifficultySelect()
    {
        OnBackButtonClicked(MenuPanel.DifficultySelect);
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        LoadingScreen.Instance.NowLoading();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
