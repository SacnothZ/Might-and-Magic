using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Consumables : MonoBehaviour
{
    HeroKnight hero;
    

    void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
        
    }

    //////////////////////
    // Collison (consume)
    //////////////////////
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Health") && collision.gameObject.CompareTag("Player"))
        {
            if (hero.heroHealth <= 80 && !hero.isDead)
            {
                hero.heroHealth += 20;
                Destroy(gameObject);
            }
            else if (hero.heroHealth > 80 && !hero.isDead)
            {
                hero.heroHealth = hero.heroHealth + (100 - hero.heroHealth);
                Destroy(gameObject);
            }

        }
        else if (CompareTag("Mana") && collision.gameObject.CompareTag("Player"))
        {
            if (hero.heroMana <= 80 && !hero.isDead)
            {
                hero.heroMana += 20;
                Destroy(gameObject);
            }
            else if (hero.heroMana > 80 && !hero.isDead)
            {
                hero.heroMana = hero.heroMana + (100 - hero.heroMana);
                Destroy(gameObject);
            }
        }
        else if (CompareTag("Power") && collision.gameObject.CompareTag("Player"))
        {
            if (!hero.isDead)
            {
                hero.StartCoroutine(hero.PowerPotion());        // Hero must handle executions, otherwise values will not turn back.
                Destroy(gameObject);
            }
        }
        else if (CompareTag("Diamond") && collision.gameObject.CompareTag("Player"))
        {
            if (!hero.isDead)
            {
                GameManager.gameManager.score += 100;
                Destroy(gameObject);
            }
        }
        else if (CompareTag("Key") && collision.gameObject.CompareTag("Player"))
        {
            if (!hero.isDead)
            {
                GameManager.gameManager.keyAmount += 1;
                GameManager.gameManager.RefreshKeyInfo();
                Destroy(gameObject);
            }
        }




    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag("Key") && collision.gameObject.CompareTag("Player"))
        {
            if (!hero.isDead)
            {
                GameManager.gameManager.keyAmount += 1;
                GameManager.gameManager.RefreshKeyInfo();
                Destroy(gameObject);
            }
        }








    }
}
