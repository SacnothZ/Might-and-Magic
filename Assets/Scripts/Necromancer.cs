using System.Collections;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    HeroKnight hero;
    EnemyDrops itemDrop;

    Animator anim;
    Rigidbody2D rb;
    Transform player;

    [Header("Stats")]
    public int hp = 10;
    public bool isDead = false;

    [Header("Range")]
    public float detectionRangeX = 10f;
    public float detectionRangeY = 2f;
    

    [Header("Attack")]
    public GameObject shadowBoltPrefab;
    public Transform shadowBoltPoint;
    public float attackCooldown = 2f;
    float nextAttackTime;
    bool facingRight = true;

    [Header("Audio: ")]
    public AudioClip attackSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;





    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        itemDrop = GetComponent<EnemyDrops>();

        hero = GameObject.Find("Player").GetComponent<HeroKnight>();

        if (hero != null)
        {
            player = hero.transform;
        }
    }

    void Update()
    {
        if (isDead) return;
        if (player == null) return;

        FacePlayer();

        float distanceX = Mathf.Abs(transform.position.x - player.position.x);
        float distanceY = Mathf.Abs(transform.position.y - player.position.y);


        if (distanceX <= detectionRangeX && distanceY <= detectionRangeY)
        {
            rb.linearVelocity = Vector2.zero;
            if (Time.time > nextAttackTime)
            {
                nextAttackTime = Time.time + attackCooldown;

                Attack();
            }
        }
    }

    void Attack()
    {
        StartCoroutine(Attacking());
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        hp -= damage;
        if (hp > 0)
        {
            SoundFxManager.instance.PlaySoundFxClip(hurtSound, transform, 0.5f);
        }

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        rb.gravityScale = 2f;       // going down
        rb.linearVelocity = Vector2.zero;   //stop fly
        rb.bodyType = RigidbodyType2D.Static;       // not detecting protectiles if set on static, at death set static to prevent going under text ocnce collider disabled.

        anim.SetTrigger("Death");

        GetComponent<Collider2D>().enabled = false;

        itemDrop.KeyDrop();
        SoundFxManager.instance.PlaySoundFxClip(deathSound, transform, 0.5f);
        Destroy(gameObject, 1f);

    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
    }

    void FacePlayer()
    {
        if (player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }


    IEnumerator Attacking()
    {

        if (hero.isDead != true)
        {
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(0.5f);
            SoundFxManager.instance.PlaySoundFxClip(attackSound, transform, 1f);
            GameObject shadowBall = Instantiate(shadowBoltPrefab, shadowBoltPoint.position, transform.rotation);
            ShadowBolt sb = shadowBall.GetComponent<ShadowBolt>();
            sb.direction = facingRight ? 1f : -1f;
        }
        
    }


}
