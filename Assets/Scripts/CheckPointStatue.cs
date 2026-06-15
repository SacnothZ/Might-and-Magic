using JetBrains.Annotations;
using UnityEngine;

public class CheckPointStatue : MonoBehaviour
{

    HeroKnight hero;
    SpriteRenderer sprite;
    public bool thisCheckpointReached = false;

    void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
        sprite = GetComponent<SpriteRenderer>();
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
        }
    }

}
