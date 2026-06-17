using UnityEngine;

public class FloatingPlatformCollider : MonoBehaviour
{
    public HeroKnight hero;
    public Rigidbody2D heroRb;
    private float originalMass;
    private void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
        heroRb = hero.GetComponent<Rigidbody2D>();
        originalMass = heroRb.mass;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            heroRb.mass = 1f;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            heroRb.mass = originalMass;
        }
    }


}
