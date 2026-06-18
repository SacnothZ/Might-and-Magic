using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skeleton : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Transform player;
    LevelCompletePrefab levelCompletePrefab;

    public bool isDead = false;

    [Header("Stats")]
    public int hp = 20;
    public int damage = 10;
    public EnemyDrops itemDrop;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;

    [Header("Patrol")]
    public float patrolDistance = 3f;
    Vector2 startPosition;
    public bool movingRight = true;

    public bool waiting = false;
    [Range(0.5f, 1)] public float waitTime = 0.7f;
    public float waitTimer = 0f;
    public float attackCooldown = 1f;
    public float nextAttackTime = 0f;

    [Header("Attack")]
    public Coroutine attacking;
    public EnemySwordAttack swordCollision;

    [Header("Sounds")]
    public AudioClip deathSound;
    public AudioClip hurtSound;
    public AudioClip attackSound;
    
    


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        

        startPosition = transform.position;

        if (SceneManager.GetActiveScene().name == "1-Level 1" || SceneManager.GetActiveScene().name == "2-Level 2")
        {
            levelCompletePrefab = GameObject.Find("CastleDoor").GetComponent<LevelCompletePrefab>();
        }

        GameObject hero = GameObject.FindGameObjectWithTag("Player");
        swordCollision = GetComponentInChildren<EnemySwordAttack>();
        itemDrop = GetComponentInChildren<EnemyDrops>();
        
        if (hero != null)
        {
            player = hero.transform;
        }
    }

    void Update()
    {
        if (isDead) return;

        float speed = Mathf.Abs(rb.linearVelocity.x);
        anim.SetFloat("Speed", speed);

        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);


        if (distanceToPlayer > detectionRange)
        {
            PatrolWithPause();
        }
        else if (distanceToPlayer > attackRange)
        {
            FollowPlayer();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;

            if (Time.time > nextAttackTime)
            {
                nextAttackTime = Time.time + attackCooldown;
                if (isDead != true)
                    Attack();
            }
        }

        if ((SceneManager.GetActiveScene().name == "1-Level 1" || SceneManager.GetActiveScene().name == "2-Level 2") && levelCompletePrefab.levelComplete == true)
        {
            Destroy(gameObject);
        }



    }

    void PatrolWithPause()
    {
        if (waiting)
        {
            rb.linearVelocity = Vector2.zero;

            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                waiting = false;
                movingRight = !movingRight;
                Flip();
            }

            return;
        }

        float direction = movingRight ? 1f : -1f;

        rb.linearVelocity = new Vector2(
            direction * moveSpeed,
            rb.linearVelocity.y
        );

        if (movingRight && transform.position.x >= startPosition.x + patrolDistance)
        {
            StartWait();
        }
        else if (!movingRight && transform.position.x <= startPosition.x - patrolDistance)
        {
            StartWait();
        }
    }

    void StartWait()
    {
        waiting = true;
        waitTimer = waitTime;

        rb.linearVelocity = Vector2.zero;
    }

    void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        rb.linearVelocity = new Vector2(direction.x * moveSpeed,rb.linearVelocity.y);
        

        if (direction.x > 0 && !movingRight)
        {
            movingRight = true;
            Flip();
        }
        else if (direction.x < 0 && movingRight)
        {
            movingRight = false;
            Flip();
        }
    }

    void Attack()
    {
        HeroKnight hero = player.GetComponent<HeroKnight>();
        if (!isDead && !hero.isDead)
        {
            anim.SetTrigger("Attack");
            SoundFxManager.instance.PlaySoundFxClip(attackSound, transform, 1f);
            attacking = StartCoroutine(swordCollision.SwordCollider());
        }


    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;
        if (attacking != null)
         StopCoroutine(attacking);
        hp -= amount;


        if (hp <= 0)
        {
            Die();
        }
        else if (hp > 0)
        {
            SoundFxManager.instance.PlaySoundFxClip(hurtSound, transform, 0.5f);
            anim.SetTrigger("Hurt");
        }

    }

    void Die()
    {
        isDead = true;

        rb.linearVelocity = Vector2.zero;

        anim.SetBool("Death", true);

        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        SoundFxManager.instance.PlaySoundFxClip(deathSound, transform, 0.5f);

        if (gameObject.name == "Level2-Skeleton(Clone)" || gameObject.name == "Level2-Skeleton")
        {
            itemDrop.NormalItemDrop();
            GameManager.gameManager.score += 20;
        }
        else
        {
            itemDrop.PowerPotionDrop();
            GameManager.gameManager.score += 150;
        }




        Destroy(gameObject, 2f);
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }





}
