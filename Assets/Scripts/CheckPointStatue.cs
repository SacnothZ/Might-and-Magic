using JetBrains.Annotations;
using UnityEngine;

public class CheckPointStatue : MonoBehaviour
{

    HeroKnight hero;
    public bool thisCheckpointReached = false;

    void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
    }

   
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!thisCheckpointReached)
        {
            GameManager.gameManager.heroCheckpointLocation = new Vector2(hero.transform.position.x, hero.transform.position.y);
            thisCheckpointReached = true;
        }
    }

}
