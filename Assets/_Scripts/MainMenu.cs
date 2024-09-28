using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

// Define the MenuPanel enum
public enum MenuPanel
{
    Menu,
    StartGame,
    Settings,
    StoreShed,
    GameScene
}

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject startGamePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject storeShedPanel;
    [SerializeField] private GameObject gameScenePanel;

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
        menuPanels[MenuPanel.Menu] = menuPanel;
        menuPanels[MenuPanel.StartGame] = startGamePanel;
        menuPanels[MenuPanel.Settings] = settingsPanel;
        menuPanels[MenuPanel.StoreShed] = storeShedPanel;
        menuPanels[MenuPanel.GameScene] = gameScenePanel;
    }
    private void Start()
    {
       // Add LoadingScreen
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
        SetActivePanel(MenuPanel.Menu);
    }

    public void ShowStartGamePanel()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        SetActivePanel(MenuPanel.StartGame);
    }

    public void OnSettingsButtonClicked()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        SetActivePanel(MenuPanel.Settings);
    }

    public void OnPlayButtonClicked()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
        ShowStartGamePanel();
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
        OnBackButtonClicked(MenuPanel.Menu);
    }

    public void OnBackToStart()
    {
        OnBackButtonClicked(MenuPanel.StartGame);
    }

    public void OnBackToSettings()
    {
        OnBackButtonClicked(MenuPanel.Settings);
    }

    public void RestartGameScene()
    {

    }
    public void ResumeGameScene()
    {

    }
    public void PauseGameScene()
    {

    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        LoadingScreen.Instance.NowLoading();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
