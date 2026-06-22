using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public HeroKnight hero;

    

    [Header("Canvas stats: ")]
    public int score;
    public int extraScore;
    public int timeRemaining = 300;

    [Header("High Score: ")]
    public int highScore = 0;
    public bool hasHighScore = false;
    
    [Header("Hero specific: ")]
    public int heroLives = 4;
    [Header("Checkpoint: ")]
    public Vector2 heroCheckpointLocation;
    [Header("Keys: ")]
    public TMP_Text keyText;
    public int keyAmount = 0;
    public bool levelEnded = false;



    private void Awake()
    {
        if (gameManager == null)   // Checking for game manager
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);      // Stays globally
        }
        else
        {
            Destroy(gameObject);        // No additional game manager
        }
    }


    private void Update()
    {
        //if(gameOver && (Input.GetKeyDown("escape") || Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) )
        //{
        //    LoadTitleScreen();
        //}

    }










    public void AddScore(int amount)
    {
        score += amount;       
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void ResetTime()
    {
        timeRemaining = 300;
    }
    public void ResetKeys()
    {
        keyAmount = 0;
    }



    ////////////////////////////////////////////////
    //On New Scene Load Data (for GameOver and Keys)
    ////////////////////////////////////////////////



    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
   
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {


        if (scene.name == "2-Level 2")
        {
            GameObject keyUiObject = GameObject.Find("KeysUI");
            if (keyUiObject != null)
            {
                keyText = keyUiObject.GetComponent<TMP_Text>();
                RefreshKeyInfo();
            }
           
        }



    }

    public void RefreshKeyInfo()
    {
        keyText.text = "Keys: " + keyAmount + " / 3";
    }













    public void CompleteLevel1()
    {
        
        LoadLevel2();
        
    }

    public void CompleteLevel2()
    {
        
        LoadLevel3();
        
    }
    public void CompleteLevel3()
    {
        
        LoadTitleScreen();
        
    }





    public void LoadTitleScreen()
    {

        ResetTime();
        ResetScore();
        ResetKeys();
        SceneManager.LoadScene("0-Title Screen");
    }
    public void LoadLevel1()
    {
 
        ResetScore();
        ResetTime();
        ResetKeys();
        SceneManager.LoadScene("1-Level 1");
    }


    public void LoadLevel2()
    {
  
        ResetTime();
        ResetKeys();
        SceneManager.LoadScene("2-Level 2");
    }
    public void LoadLevel3()
    {

        ResetTime();
        SceneManager.LoadScene("3-Level 3");
    }

        

    
    


}