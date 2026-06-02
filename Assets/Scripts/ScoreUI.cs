using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;

    void Update()
    {
        if (gameObject.name == "ScoreUi")
            scoreText.text = "Score: " + GameManager.gameManager.score;     //updates score (data saved from Gamemanager).
    }




}
