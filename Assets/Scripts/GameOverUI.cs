using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverUi;

    private bool isGameOver = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!isGameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) ||Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.gameManager.LoadTitleScreen();
        }
    }

    public void GameOver()
    {
        gameOverUi.SetActive(true);
        isGameOver = true;
    }
}