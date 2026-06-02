using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLeft : MonoBehaviour
{
    
   
    

    public TMP_Text timeText;

    void Start()
    {
        StartCoroutine(Timer());
    }

   public  IEnumerator Timer()
    {
        while (GameManager.gameManager.timeRemaining > 0)
        {
            timeText.text = "Time: " + GameManager.gameManager.timeRemaining;
            yield return new WaitForSeconds(1f);
            GameManager.gameManager.timeRemaining -= 1;
        }
        timeText.text = "Time: 0";
    }


}