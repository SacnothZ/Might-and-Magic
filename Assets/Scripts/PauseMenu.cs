using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool menuActive = false;
    private GameObject scoreUI;
    private GameObject TimeUI;


    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject menuImage;
    public GameObject optionsPanel;

    [Header("Main Menu Buttons")]
    public Button returnButton;
    public Button optionsButton;
    public Button mainMenuButton;

    [Header("Options Buttons")]
    public Button backFromOptionsButton;
    public bool optionsMenuOn = false;

    [Header("Options UI")]
    public Slider soundSlider;
    public Slider musicSlider;
    public Toggle fullscreenToggle;

    void Start()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);


        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        fullscreenToggle.isOn = Screen.fullScreen;

        soundSlider.onValueChanged.AddListener(SetSoundVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    void Update()
    {
        //////////
        //Pause 
        //////////
        if (!menuActive && Input.GetKeyDown("escape"))
        {
            mainMenuPanel.SetActive(true);
            menuImage.SetActive(true);
            menuActive = true;
            PauseOrUnpauseGame();

        }
        else if (menuActive && Input.GetKeyDown("escape") && !optionsMenuOn)
        {
            mainMenuPanel.SetActive(false);
            menuImage.SetActive(false);
            menuActive = false;
            PauseOrUnpauseGame();
        }
        else if (menuActive && Input.GetKeyDown("escape") && optionsMenuOn)
        {
            mainMenuPanel.SetActive(true);
            menuImage.SetActive(true);
            optionsPanel.SetActive(false);
            optionsMenuOn = false;
            
        }

    }



    



    ////////////////
    // Main Buttons 
    ////////////////

    public void ReturnToGameButton()
    {
        mainMenuPanel.SetActive(false);
        menuImage.SetActive(false);
        optionsPanel.SetActive(false);
        Time.timeScale = 1f;
        menuActive = false;

    }

    public void OptionsButton()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        optionsMenuOn = true;
    }

    public void BackToMainMenu()
    {
        mainMenuPanel.SetActive(false);
        menuImage.SetActive(false);
        optionsPanel.SetActive(false);
        Time.timeScale = 1f;
        menuActive = false;
        GameManager.gameManager.LoadTitleScreen();
    }




    ////////////////
    //Option Buttons 
    ////////////////


    public void SetSoundVolume(float value)
    {
        PlayerPrefs.SetFloat("SoundVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void BackButtonOption()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        optionsMenuOn = false;
    }


    ////////////////
    // Pause
    ////////////////
   
    public void PauseOrUnpauseGame()
    {
        if (menuActive)
        {
            Time.timeScale = 0f;
        }
        else if (!menuActive)
        {
            Time.timeScale = 1f;
        }

    }









}

