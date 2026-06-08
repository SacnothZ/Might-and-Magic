using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletePrefab : MonoBehaviour
{
    HeroKnight hero;
    TimeLeft timer;
    public GameObject levelCompleteCanvas;

    
    
    public bool levelComplete;



    void Start()
    {
        levelComplete = false;
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
    }


    void Update()
    {

        timer = GameObject.Find("TimeLeftUi").GetComponent<TimeLeft>();

        if (levelComplete && Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "1-Level 1")
        {
            GameManager.gameManager.LoadLevel2();
        }

        else if (levelComplete && Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "2-Level 2")
        {
            GameManager.gameManager.LoadLevel3();
        }
        // else if (levelComplete && Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "3-Level3")
        //  GameManager.gameManager.LoadTitleScreen();





    }













    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag("CastleEntrance") && collision.gameObject.CompareTag("Player"))      // scenes must have tags in per scene.
        {
            levelComplete = true;
            hero.stopMoving = true;
            levelCompleteCanvas.SetActive(true);
            StopCoroutine(timer.Timer());
            GameManager.gameManager.extraScore = 10 * GameManager.gameManager.timeRemaining;
            GameManager.gameManager.score += GameManager.gameManager.extraScore;
           
        }
        else if (CompareTag("CastleDoor") && (collision.gameObject.CompareTag("Player") && GameManager.gameManager.keyAmount == 3))
        {
            levelComplete = true;
            hero.stopMoving = true;
            levelCompleteCanvas.SetActive(true);
            StopCoroutine(timer.Timer());
            GameManager.gameManager.extraScore = 10 * GameManager.gameManager.timeRemaining;
            GameManager.gameManager.score += GameManager.gameManager.extraScore;
          
        }
    }




}
