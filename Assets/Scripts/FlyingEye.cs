using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Transform player;
    

    [Header("Stats")]
    public int hp = 3;
    public bool isDead = false;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float detectionRange = 10f;
    public float attackRange = 4f;

    [Header("Attack")]
    public GameObject fireballPrefab;
    public Transform firePoint;

    public float attackCooldown = 2f;
    float nextAttackTime;

    bool facingRight = true;

    HeroKnight hero;

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


        anim.SetBool("Flying", distance > attackRange);     // While not in attack range, flying.




        if (distance <= detectionRange && distance > attackRange)
        {
            FollowPlayer();
        }
        else if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;

            if (Time.time > nextAttackTime && hero.heroHealth > 0)
            {
                nextAttackTime = Time.time + attackCooldown;

                Attack();
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }





    void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        GameObject smallFireBall = Instantiate(fireballPrefab, firePoint.position, transform.rotation);
        SmallFireBall fb =smallFireBall.GetComponent<SmallFireBall>();
        fb.direction = facingRight ? 1f : -1f;
    
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

        anim.SetTrigger("Death");

        GetComponent<Collider2D>().enabled = false;

        GameManager.gameManager.score += 50;
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