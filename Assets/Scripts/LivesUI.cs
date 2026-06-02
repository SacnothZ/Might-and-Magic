using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public TMP_Text livesText;

    void Update()
    {
        if (gameObject.name == "LivesUi")
            livesText.text = "Lives: " + GameManager.gameManager.heroLives;  
    }




}

