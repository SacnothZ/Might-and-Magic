using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletePrefab : MonoBehaviour
{
    HeroKnight hero;
    TimeLeft timer;
    public GameObject levelCompleteCanvas;
    public AudioClip levelCompleteSound;
    
    

    [Header("Scores Uis :")]
    public TMP_Text plainScore;
    public TMP_Text bonusScore;
    public TMP_Text totalScore;



    public bool levelComplete;



    void Start()
    {
        levelComplete = false;
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
        timer = GameObject.Find("TimeLeftUi").GetComponent<TimeLeft>();
    }


    void Update()
    {
        if (levelComplete && Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "1-Level 1")
        {
            GameManager.gameManager.LoadLevel2();
        }

        else if (levelComplete && Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "2-Level 2")
        {
            GameManager.gameManager.LoadLevel3();
        }
        else if (levelComplete && Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "3-Level 3")
          GameManager.gameManager.LoadTitleScreen();
    }













    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag("CastleEntrance") && collision.gameObject.CompareTag("Player"))      // scenes must have tags in per scene.
        {
            levelComplete = true;
            hero.stopMoving = true;
            hero.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            levelCompleteCanvas.SetActive(true);
            timer.StopCoroutine(timer.timerCoroutine);


            plainScore.text = GameManager.gameManager.score.ToString();
            GameManager.gameManager.extraScore = 10 * GameManager.gameManager.timeRemaining;
            bonusScore.text = GameManager.gameManager.extraScore.ToString();
            totalScore.text = (GameManager.gameManager.score + GameManager.gameManager.extraScore).ToString();
            GameManager.gameManager.score += GameManager.gameManager.extraScore;
            MusicManager.instance.musicSource.Stop();
            SoundFxManager.instance.PlaySoundFxClip(levelCompleteSound, transform, 1f);







        }
        else if (CompareTag("CastleDoor") && (collision.gameObject.CompareTag("Player") && GameManager.gameManager.keyAmount == 3))
        {
            levelComplete = true;
            hero.stopMoving = true;
            hero.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            levelCompleteCanvas.SetActive(true);
            timer.StopCoroutine(timer.timerCoroutine);


            plainScore.text = GameManager.gameManager.score.ToString();
            GameManager.gameManager.extraScore = 10 * GameManager.gameManager.timeRemaining;
            bonusScore.text = GameManager.gameManager.extraScore.ToString();
            totalScore.text = (GameManager.gameManager.score + GameManager.gameManager.extraScore).ToString();
            GameManager.gameManager.score += GameManager.gameManager.extraScore;
            MusicManager.instance.musicSource.Stop();
            SoundFxManager.instance.PlaySoundFxClip(levelCompleteSound, transform, 1f);

        }




    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Princess") && (collision.gameObject.CompareTag("Player")))
        {
            levelComplete = true;
            hero.stopMoving = true;
            hero.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            levelCompleteCanvas.SetActive(true);
            timer.StopCoroutine(timer.timerCoroutine);


            plainScore.text = GameManager.gameManager.score.ToString();
            GameManager.gameManager.extraScore = 10 * GameManager.gameManager.timeRemaining;
            bonusScore.text = GameManager.gameManager.extraScore.ToString();
            totalScore.text = (GameManager.gameManager.score + GameManager.gameManager.extraScore).ToString();
            GameManager.gameManager.score += GameManager.gameManager.extraScore;
            MusicManager.instance.musicSource.Stop();
            SoundFxManager.instance.PlaySoundFxClip(levelCompleteSound, transform, 1f);
            GameManager.gameManager.hasHighScore = true;
            if (GameManager.gameManager.highScore < GameManager.gameManager.score)
            {
                GameManager.gameManager.highScore = GameManager.gameManager.score;
            }

        }
    }
}
