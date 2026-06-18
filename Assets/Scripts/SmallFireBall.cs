using UnityEngine;

public class SmallFireBall : MonoBehaviour
{
    [Header("Stats: ")]
    public float damage = 10f;
    public float projectileSpeed = 5f;
    public float direction = 1f;


    HeroKnight hero;

    private Vector2 moveDirection;
    bool facingRight;
    [Header("Audio")]
    public AudioClip impactSound;
    



    void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();

        moveDirection = (hero.transform.position - transform.position).normalized;

        if (direction < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;        //Following player rotation angle, z value.
       

        Destroy(gameObject, 5f);
        
    }

   
    void Update()
    {

        //////////////////
        // Move (Physics)
        //////////////////
        transform.Translate(moveDirection * projectileSpeed  * Time.deltaTime);

    }







    //////////////////
    // Collider
    //////////////////
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {


            Vector2 toPlayer = (hero.transform.position - transform.position).normalized; // where is the hero compared to the enemy
            Vector2 playerForward = hero.transform.right * hero.transform.localScale.x; //witch direction hero looks at
            float dot = Vector2.Dot(playerForward, toPlayer);            // do they look at the same direction ? -1=no 1=yes
            bool isFromFront = dot < 0.0f;




            if (hero != null && hero.isDead != true)
            {
                if (hero.isBlocking && isFromFront)
                {
                    SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);
                    Destroy(gameObject);
                    return;
                }
                hero.Hurt(damage);
                SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);
                Destroy(gameObject);
                
            }


        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {


            Vector2 toPlayer = (hero.transform.position - transform.position).normalized; // where is the hero compared to the enemy
            Vector2 playerForward = hero.transform.right * hero.transform.localScale.x; //witch direction hero looks at
            float dot = Vector2.Dot(playerForward, toPlayer);            // do they look at the same direction ? -1=no 1=yes
            bool isFromFront = dot < 0.0f;




            if (hero != null && hero.isDead != true)
            {
                if (hero.isBlocking && isFromFront)
                {
                    SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);
                    Destroy(gameObject);
                    return;
                }
                hero.Hurt(damage);
                SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);
                Destroy(gameObject);
            }


        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }


}
