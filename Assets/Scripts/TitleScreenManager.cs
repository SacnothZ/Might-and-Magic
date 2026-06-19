using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public TMP_Text pressAnyKeyText;
    public float blinkDuration = 0.5f;

    private bool keyPressed = false;

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject levelSelectorPanel;
    public GameObject optionsPanel;

    [Header("Main Menu Buttons")]
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;

    [Header("Level Buttons")]
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button backFromStageButton;
    public bool levelSelectorOn = false;

    [Header("Options Buttons")]
    public Button backFromOptionsButton;

    [Header("Options UI")]
    public Slider soundSlider;
    public Slider musicSlider;
    public Toggle fullscreenToggle;
    public bool optionsMenuOn = false;

    void Start()
    {
        mainMenuPanel.SetActive(false);
        levelSelectorPanel.SetActive(false);
        optionsPanel.SetActive(false);
        levelSelectorOn = false;
        optionsMenuOn = false;

        StartCoroutine(PressAnyKeyBlink());

        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        fullscreenToggle.isOn = Screen.fullScreen;

        soundSlider.onValueChanged.AddListener(SetSoundVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    void Update()
    {
        if (!keyPressed && Input.anyKeyDown)
        {
            keyPressed = true;
            pressAnyKeyText.enabled = false;
            mainMenuPanel.SetActive(true);
        }
        if(Input.GetKeyDown("escape") && (optionsMenuOn || levelSelectorOn))
        {
            BackToMainMenu();
        }
    }



    IEnumerator PressAnyKeyBlink()
    {
        while (!keyPressed)
        {
            pressAnyKeyText.enabled = !pressAnyKeyText.enabled;
            yield return new WaitForSeconds(blinkDuration);
        }
    }



    ////////////////
    // Main Buttons
    ////////////////


    public void PlayButton()
    {
        mainMenuPanel.SetActive(false);
        levelSelectorPanel.SetActive(true);
        levelSelectorOn = true;
        optionsPanel.SetActive(false);
    }

    public void OptionsButton()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        optionsMenuOn = true;
        levelSelectorPanel.SetActive(false);
        
    }

    public void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelSelectorPanel.SetActive(false);
        optionsPanel.SetActive(false);
        optionsMenuOn = false;
        levelSelectorOn = false;
    }

    public void ExitButton()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;        // Shuts down if in unity editor
#endif
    }

    ////////////////
    // Level Select
    ////////////////

    public void LoadLevel1New()
    {
        GameManager.gameManager.heroLives = 4;
        GameManager.gameManager.LoadLevel1();
    }

    public void LoadLevel2New()
    {
        GameManager.gameManager.heroLives = 4;
        GameManager.gameManager.ResetScore();
        GameManager.gameManager.LoadLevel2();
    }

    public void LoadLevel3New()
    {
        GameManager.gameManager.heroLives = 4;
        GameManager.gameManager.ResetScore();
        GameManager.gameManager.ResetKeys();
        GameManager.gameManager.LoadLevel3();
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












}