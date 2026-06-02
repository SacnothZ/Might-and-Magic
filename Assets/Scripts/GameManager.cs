using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public int score;
    public int timeRemaining = 300;
    public int extraScore;
    public int keyAmount = 0;
    public int heroLives = 4;
    

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