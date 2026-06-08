using UnityEngine;

public class DeathPoint : MonoBehaviour
{
    HeroKnight hero;
    void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
    }

    
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("Player"))
            hero.heroHealth = 0;
    }


}
