using JetBrains.Annotations;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    public HeroKnight hero;
    [Header("Drops")]
    public GameObject healthPotionPrefab;
    public GameObject manaPotionPrefab;
    public GameObject powerPotionPrefab;
    public GameObject diamondPrefab;
    public GameObject keyPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
    }



    //////////////////////
    // Drop logic
    //////////////////////
    public void NormalItemDrop()
    {
        GameObject dropObject;

        if ((hero.heroHealth <= hero.heroMana) && (hero.heroHealth < 100))     // if lower or equal than mana, then health potion.
        {
            dropObject = healthPotionPrefab;
        }
        else if (hero.heroMana < hero.heroHealth)
        {
            dropObject = manaPotionPrefab;
        }
        else
            dropObject = diamondPrefab;

        Instantiate(dropObject, transform.position, transform.rotation);

    }

    public void PowerPotionDrop()
    {
        GameObject dropObject;
        dropObject = powerPotionPrefab;
        Instantiate(dropObject, transform.position, transform.rotation);
    }

    public void KeyDrop() 
    {
        GameObject dropObject;
        dropObject = keyPrefab;
        Instantiate(dropObject, transform.position, transform.rotation);
    }



}
