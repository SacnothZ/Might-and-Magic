using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;

    void Update()
    {
        if (gameObject.name == "ScoreUi")
            scoreText.text = "Score: " + GameManager.gameManager.score;     //updates score (data saved from Gamemanager).

        if (gameObject.name == "HighScoreUi" && GameManager.gameManager.hasHighScore)
            scoreText.text = "High Score: " + GameManager.gameManager.highScore;     //updates high score (data saved from Gamemanager).
    }




}
