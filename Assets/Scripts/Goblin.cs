using System.Collections;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Transform player;
    public EnemySwordAttack swordCollision;
    public bool isDead = false;
    EnemyDrops itemDrop;
    LevelCompletePrefab levelCompletePrefab;
    HeroKnight hero;

    [Header("Stats")]
    public int hp = 5;
    public int damage = 5;

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
    

    


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
        swordCollision = GetComponentInChildren<EnemySwordAttack>();
        itemDrop = GetComponent<EnemyDrops>();
        levelCompletePrefab = GameObject.Find("CastleDoor").GetComponent<LevelCompletePrefab>();

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






            if (distanceToPlayer > detectionRange && hero.heroHealth > 0)
            {
                PatrolWithPause();
            }
            else if (distanceToPlayer > attackRange && hero.heroHealth > 0)
            {
                FollowPlayer();
            }
            else
            {
                rb.linearVelocity = Vector2.zero;

                if (Time.time > nextAttackTime && hero.heroHealth > 0)
                {

                    nextAttackTime = Time.time + attackCooldown;
                    Attack();


                }
            }


        if (levelCompletePrefab.levelComplete == true)
        {
            Destroy(gameObject);
        }





    }

    /////////////////////////// 
    //Patrol
    /////////////////////////// 
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

        rb.linearVelocity = new Vector2(direction * moveSpeed,rb.linearVelocity.y);

        if (movingRight && transform.position.x >= startPosition.x + patrolDistance)
        {
            StartWait();
        }
        else if (!movingRight && transform.position.x <= startPosition.x - patrolDistance)
        {
            StartWait();
        }
    }


    /////////////////////////// 
    //Wait after patrol "ends"
    /////////////////////////// 
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
    ///////////////////////////
    //Attack
    ///////////////////////////
    void Attack()
    {
        anim.SetTrigger("Attack");

        HeroKnight hero = player.GetComponent<HeroKnight>();
        if(isDead!=true)
        StartCoroutine(swordCollision.SwordCollider());

    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        hp -= amount;

        if (hp <= 0)
        {
            Die();
        }
    }
    ///////////////////////////
    //Death
    ///////////////////////////
    void Die()
    {
        isDead = true;

        rb.linearVelocity = Vector2.zero;       //prevents moving

        anim.SetBool("Death", true);

        rb.bodyType = RigidbodyType2D.Static;       // to avoid going under text.
        GetComponent<Collider2D>().enabled = false; // to prevent player from colliding with enemy (need to set rigidbody to static to prevent "going under text")

        itemDrop.NormalItemDrop();
        GameManager.gameManager.score += 10;
        Destroy(gameObject, 2f);        // remove enemy GameObject after death in 2 seconds.
    }






    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;  // x needs to be "converted" to flip enemy to other direction.
        transform.localScale = scale;
    }





}