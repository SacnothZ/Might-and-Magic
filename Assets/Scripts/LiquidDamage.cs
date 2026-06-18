using UnityEngine;

public class LiquidDamage : MonoBehaviour
{
    HeroKnight hero;
    private float damageCooldown =0f;
   
    void Start()
    {
       hero = GameObject.Find("Player").GetComponent<HeroKnight>();
    }

   
    void Update()
    {
        if(damageCooldown>0)
            damageCooldown -= Time.deltaTime;
    }


    private void OnTriggerStay2D(Collider2D collision)      // need stay instead of enter.
    {

        if (collision.CompareTag("Player") && damageCooldown <= 0)
        {
            if (gameObject.CompareTag("Toxic"))
            {
                hero.Hurt(10);
                damageCooldown = 2f;
            }
            else if (gameObject.CompareTag("Lava"))
            {
                hero.heroHealth = 0;
                damageCooldown = 2f;
            }
        }
    }
}
