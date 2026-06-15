using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    
    public HeroKnight heroData;
    public GameObject gameOverTextObject;
    public TimeLeft timer;
    public bool isGameOver = false;

    void Start()
    {
        heroData = GameObject.Find("Player").GetComponent<HeroKnight>();
        timer = GameObject.FindFirstObjectByType<TimeLeft>();

    }

    void Update()
    {
        if (GameManager.gameManager.timeRemaining <= 0 && !isGameOver)
        {
            GameOver();
        }
            
        if (!isGameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) ||Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            GameManager.gameManager.LoadTitleScreen();
        }
    }

    public void GameOver()
    {
        
        isGameOver = true;
        gameOverTextObject.SetActive(true);
        heroData.isDead = true;
        timer.StopCoroutine(timer.timerCoroutine);

    }
}