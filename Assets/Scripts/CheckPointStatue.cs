using JetBrains.Annotations;
using UnityEngine;

public class CheckPointStatue : MonoBehaviour
{

    HeroKnight hero;
    SpriteRenderer sprite;
    public bool thisCheckpointReached = false;




    [Header("Audio")]
    public AudioClip checkpointReachedSound;

    [Header("Canvas")]
    public GameObject checkpointCanvas;

    void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
        sprite = GetComponent<SpriteRenderer>();
        checkpointCanvas.SetActive(false);
    }

   
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!thisCheckpointReached && collision.gameObject.CompareTag("Player"))
        {
            GameManager.gameManager.heroCheckpointLocation = new Vector2(hero.transform.position.x, hero.transform.position.y);
            thisCheckpointReached = true;
            sprite.color = Color.white;
            hero.hasCheckpoint= true;
            SoundFxManager.instance.PlaySoundFxClip(checkpointReachedSound, transform, 1f);
            checkpointCanvas.SetActive(true);
            Invoke("HideCheckpointCanvas", 1f);
        }
    }

    void HideCheckpointCanvas()
    {
        checkpointCanvas.SetActive(false);
    }

}
