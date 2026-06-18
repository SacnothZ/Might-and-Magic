using UnityEngine;

public class Wizard : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Transform player;
    HeroKnight hero;

    [Header("Stats")]
    public int hp = 10;
    public bool isDead = false;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float detectionRange = 10f;
    public float attackRange = 4f;

    [Header("Attack")]
    public GameObject bFireballPrefab;
    public Transform bFirePoint;
    public float attackCooldown = 2f;
    float nextAttackTime;
    bool facingRight = true;

    [Header("Audio")]
    public AudioClip magicSound;
    public AudioClip deathSound;

  

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

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

        float distance = Vector2.Distance(transform.position, player.position);


        if (distance <= detectionRange)
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
        if (hero.isDead != true)
        {
            anim.SetTrigger("Attack");
            SoundFxManager.instance.PlaySoundFxClip(magicSound, transform, 1f);
            GameObject bigFireBall = Instantiate(bFireballPrefab, bFirePoint.position, transform.rotation);
            BigFireBall fb = bigFireBall.GetComponent<BigFireBall>();
            fb.direction = facingRight ? 1f : -1f;
        }

    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        hp -= damage;

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
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");

        GetComponent<Collider2D>().enabled = false;

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



}