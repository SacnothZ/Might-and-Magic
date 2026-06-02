using UnityEngine;

public class HeroMagic : MonoBehaviour
{
    HeroKnight hero;

    [Header("Magic Stats: ")]
    public float projectileSpeed = 15f;
    public float direction = 1f;


    void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
        Destroy(gameObject, 3f);
    }

    
    void Update()
    {
        transform.Translate(Vector2.right * direction * projectileSpeed  * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Goblin goblin = other.GetComponent<Goblin>();

            if (goblin != null)
            {
                goblin.TakeDamage(10);
                
            }

            Skeleton skeleton = other.GetComponent<Skeleton>();

            if (skeleton != null)
            {
                skeleton.TakeDamage(20);
                
            }

            Necromancer necromancer = other.GetComponent<Necromancer>();
            if (necromancer != null)
            {
                necromancer.TakeDamage(10);
            }

            FlyingEye flyingEye = other.GetComponent<FlyingEye>();
            if (flyingEye != null)
            {
                flyingEye.TakeDamage(10);
                
            }



            Wizard wizard = other.GetComponent<Wizard>();
            if (wizard != null)
            {
                wizard.TakeDamage(0);
                
            }
            Destroy(gameObject);
        }
        
    }

}
