using UnityEngine;

public class BigFireBall : MonoBehaviour
{
    [Header("Stats: ")]
    public float projectileSpeed = 5f;
    public float protectileDuration = 8;
    public float direction = 1f;
    public float damage = 40f;



    HeroKnight hero;


    void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();

        // Setting the direction of prefab at spawn.through scales.
        if(direction<0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }


        Destroy(gameObject, protectileDuration);

    }


    void Update()
    {

        transform.Translate(Vector2.right * projectileSpeed * direction * Time.deltaTime);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {


            if (hero != null && hero.isDead!=true)
            {
                hero.heroHealth -= damage; 
                hero.m_animator.SetTrigger("Hurt");
                Destroy(gameObject);
            }

        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }




}
