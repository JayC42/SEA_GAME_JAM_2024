using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

// Define the MenuPanel enum
public enum MenuPanel
{
    LoadScene, 
    StartGame, 
    MainMenu,
    //Settings,
    StoreShed,
    GameScene
}

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Panels")]
    [SerializeField] private GameObject signBoardPanel; 
    [SerializeField] private GameObject loadPanel;
    [SerializeField] private GameObject startGamePanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject menuSettingsPanel;
    [SerializeField] private GameObject storeShedPanel;
    [SerializeField] private GameObject gameScenePanel;
    [SerializeField]  private GameObject pauseScreen;

    public Dictionary<MenuPanel, GameObject> menuPanels = new Dictionary<MenuPanel, GameObject>();

    [Header("Panel Selection")]
    public MenuPanel currentPanel; // This will show up in the Inspector

    [Header("Sounds")]
    public AudioClip ABGM;
    public AudioClip BBGM;
    public AudioClip CBGM;
    public AudioClip EBGM;
    public AudioClip dayEndSfx; 
    public AudioClip buttonClickSFX1;
    public AudioClip buttonClickSFX2;

    public AudioClip gameStartClickSFX;
    private Coroutine previewCoroutine;
    private void Awake()
    {
        // Populate the dictionary with references to the panels
        menuPanels[MenuPanel.LoadScene] = loadPanel;
        menuPanels[MenuPanel.StartGame] = startGamePanel;
        menuPanels[MenuPanel.MainMenu] = mainMenuPanel;
        //menuPanels[MenuPanel.Settings] = settingsPanel;
        menuPanels[MenuPanel.StoreShed] = storeShedPanel;
        menuPanels[MenuPanel.GameScene] = gameScenePanel;
    }
    private void Start()
    {
        //ShowLoading(); 
        //LoadVolumeSettings();
        ShowStartGamePanel();
    }

    private void OnDisable()
    {
        //SaveVolumeSettings();
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
    public void ShowLoading()
    {
        StartCoroutine(ShowLoadSceneCoroutine());
        AudioManager.Instance.PlayMusic(ABGM);
    }
    public void ShowStartGamePanel()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX2);
        SetActivePanel(MenuPanel.StartGame);
    }
    public void ShowMainMenu()
    {
        AudioManager.Instance.PlayMusic(BBGM);
        SetActivePanel(MenuPanel.MainMenu);
    }
    public void ShowSignBoard()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX1);
        signBoardPanel.SetActive(true);
    }
    public void HideSignBoard()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX1);
        signBoardPanel.SetActive(false);
    }
    
    public void ShowMenuSettingsPanel()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX2);
        menuSettingsPanel.SetActive(true);
    }
    public void HideMenuSettingsPanel()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX2);
        menuSettingsPanel.SetActive(false);
    }
    public void ShowBasementShopScene()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX1);
        SetActivePanel(MenuPanel.StoreShed);
    }
    public void ShowGameScene()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX2);
        SetActivePanel(MenuPanel.GameScene);
        GameManager.Instance.StartGame();
    }

    public void OnQuitButtonClicked()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX1);
        Application.Quit();
    }

    private void OnBackButtonClicked(MenuPanel panelToShow)
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX2);

        foreach (var panel in menuPanels.Values)
        {
            panel.SetActive(false);
        }

        menuPanels[panelToShow].SetActive(true);
    }
    private IEnumerator ShowLoadSceneCoroutine()
    {
        SetActivePanel(MenuPanel.LoadScene);
        yield return new WaitForSeconds(1f);
        SetActivePanel(MenuPanel.MainMenu);
    }

    public void RestartGameScene()
    {
        // Stop the current game
        GameManager.Instance.StopGame();

        // Reset UI elements
        UIManager.Instance.resetTimer();
        UIManager.Instance.UpdateDayDisplay();
        UIManager.Instance.UpdateMoneyDisplay();

        // Start a new game
        GameManager.Instance.StartGame();

        // Ensure we're showing the game scene panel
        SetActivePanel(MenuPanel.GameScene);

        // Play a sound effect for restarting (optional)
        AudioManager.Instance.PlaySFX(buttonClickSFX2);
    }

    public void ResumeGameScene()
    {
        Time.timeScale = 1;
        AudioManager.Instance.PlaySFX(buttonClickSFX2);
        GameManager.Instance.isPaused = false;
    }

    public void PauseGameScene()
    {
        Time.timeScale = 0;
        AudioManager.Instance.PlaySFX(buttonClickSFX2);
    }
}
