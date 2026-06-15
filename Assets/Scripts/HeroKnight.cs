using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour
{


    public GameOverUI gameoverui;


    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] GameObject m_slideDust;
   
    




    public Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;

    public bool isDead = false;
    public HeroSwordAttack swordCollider;
    public bool stopMoving = false;

    [Header("Hero Stats: ")]
    public float heroHealth = 100;
    public float heroMana = 100;
    public float heroDamage = 10;
    public float m_speed = 7f;
    public float m_jumpForce = 11f;

    [Header("Power Bar: ")]
    public GameObject powerBar;
    public float powerDuration = 15f;
    public float powerTimer = 0f;

    [Header("Blocking: ")]
    public bool isBlocking = false;

    [Header("Magic")]
    public GameObject heroMagicPrefab;
    public Transform firePoint;
    public float magicCooldown = 1f;
    public float nextMagicTime = 0f;

 





    void Start()
    {
        swordCollider = GetComponentInChildren<HeroSwordAttack>();
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        gameoverui = FindAnyObjectByType<GameOverUI>(FindObjectsInactive.Include);     // Finding inaktiv objects to prevent null.
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        GameManager.gameManager.heroCheckpointLocation = new Vector2(transform.position.x, transform.position.y);
    }




    void Update()
    {
        if (isDead)
            return;

        m_timeSinceAttack += Time.deltaTime;    // Increase timer that controls attack combo


        if (m_rolling)                          // Increase timer that checks roll duration
            m_rollCurrentTime += Time.deltaTime;


        if (m_rollCurrentTime > m_rollDuration) // Disable rolling if timer extends duration
            m_rolling = false;


        if (!m_grounded && m_groundSensor.State())  //Check if character just landed on the ground
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }


        if (m_grounded && !m_groundSensor.State())  //Check if character just started falling
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }


        float inputX = Input.GetAxis("Horizontal");     //Left-Right Input


        if (inputX > 0 && m_facingDirection == -1 && !stopMoving)      // Flipping character (scale based), scales children objects too.
        {
            m_facingDirection = 1;
            Flip();
        }
        else if (inputX < 0 && m_facingDirection == 1 && !stopMoving)
        {
            m_facingDirection = -1;
            Flip();
        }

        ////////////////////
        // Moving (Physics)
        ////////////////////
        if (!m_rolling && !isBlocking && !stopMoving)
            m_body2d.linearVelocity = new Vector2(inputX * m_speed, m_body2d.linearVelocity.y);
        else if (isBlocking && m_grounded)
            m_body2d.linearVelocity = new Vector2(0, m_body2d.linearVelocity.y);

        m_animator.SetFloat("AirSpeedY", m_body2d.linearVelocity.y);    //Set AirSpeed in animator


        ///////////
        //Death
        ///////////
        if (heroHealth <= 0)
        {
            Die();
        }

        ///////////
        //Attack
        ///////////
        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.5f && !m_rolling && !stopMoving)
        {
            Attack();
        }

        ///////////
        //Magic
        ///////////

        else if (Input.GetKeyDown("e") && ((!isBlocking || !isDead)) && !stopMoving)
        {
            Casting();
        }

        ///////////
        // Block
        ///////////
        else if (Input.GetMouseButtonDown(1) && !m_rolling && !stopMoving)
        {
            Block();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            NoBlock();
        }



        ///////////
        //Jump
        ///////////
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling && !isBlocking && !stopMoving)
        {
            Jump();
        }

        ///////////////////
        //Run (animation)
        ///////////////////
        else if (Mathf.Abs(inputX) > Mathf.Epsilon && !stopMoving)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }
        ///////////////////
        //Idle (animation)
        ///////////////////
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }



    }









    //////////////////////////////
    //Death and revival function
    //////////////////////////////

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;
        heroHealth = 0;
        m_body2d.linearVelocity = Vector2.zero;
        m_animator.SetTrigger("Death");
        StartCoroutine(Revive());
    }

    public IEnumerator Revive()
    {
        yield return new WaitForSeconds(2);
        if (GameManager.gameManager.heroLives > 0)
        {
            heroHealth = 100;
            heroMana = 100;
            isDead = false;
            transform.position = GameManager.gameManager.heroCheckpointLocation;
            m_animator.Play("Idle");
            GameManager.gameManager.heroLives--;
        }
        else
        {
            gameoverui.GameOver();
        }

    }





    ///////////////////////////////
    // Attack function
    ///////////////////////////////
    public void Attack()
    {
        if (Time.timeScale == 0f)
            return;
        StartCoroutine(swordCollider.SwordColliderInstant());
        m_currentAttack++;

        // Loop back to one after third attack
        if (m_currentAttack > 3)
            m_currentAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (m_timeSinceAttack > 1.0f)
            m_currentAttack = 1;
        
        m_animator.SetTrigger("Attack" + m_currentAttack);  // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        m_timeSinceAttack = 0.0f;       // Reset timer
    }
    ///////////////////////////////
    // Block function
    ///////////////////////////////
    public void Block()
    {
        if (Time.timeScale == 0f)
            return;
        isBlocking = true;
        m_animator.SetTrigger("Block");
        m_animator.SetBool("IdleBlock", true);
    }
    public void NoBlock()
    {
        if (Time.timeScale == 0f)
            return;
        isBlocking = false;
        m_animator.SetBool("IdleBlock", false);
    }

    public void Jump()
    {
        if (Time.timeScale == 0f)
            return;
        m_animator.SetTrigger("Jump");
        m_grounded = false;
        m_animator.SetBool("Grounded", m_grounded);
        m_body2d.linearVelocity = new Vector2(m_body2d.linearVelocity.x, m_jumpForce);
        m_groundSensor.Disable(0.2f);
    }

    public void Hurt()
    {
        m_animator.SetTrigger("Hurt");
    }





    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
    }




    ///////////////////////////////
    // Power (for potion script)
    ///////////////////////////////
    public IEnumerator PowerPotion()
    {
        float originalSpeed = m_speed;
        float originalJump = m_jumpForce;

        m_speed *= 2f;
        m_jumpForce *= 2f;

        powerTimer = powerDuration;     // Timer gets duration
        powerBar.SetActive(true);       // Enable bar visibility

        while (powerTimer > 0)
        {
            powerTimer -= Time.deltaTime;
            yield return null;
        }

        powerTimer = 0;
        powerBar.SetActive(false);      // Disable bar visibility

        m_speed = originalSpeed;
        m_jumpForce = originalJump;
    }


    ///////////////////////////////
    // Magic function
    ///////////////////////////////
    public void Casting()
    {
        if (Time.time < nextMagicTime || Time.timeScale == 0f)      // pause or cooldown
            return;
        if (heroMana >= 20)
        {
            nextMagicTime = Time.time + magicCooldown;
            StartCoroutine(CastingPause());

            m_animator.SetTrigger("Magic");
            heroMana -= 20;
            GameObject heroMagic = Instantiate(heroMagicPrefab, firePoint.position, transform.rotation);
            HeroMagic hm = heroMagic.GetComponent<HeroMagic>();
            hm.direction = m_facingDirection;

            if (m_facingDirection == -1)
            {
                Vector3 scale = heroMagic.transform.localScale;
                scale.x *= -1;
                heroMagic.transform.localScale = scale;
            }
        }

    }


    private IEnumerator CastingPause()
    {
        stopMoving = true;
        m_body2d.linearVelocity = new Vector2(0, m_body2d.linearVelocity.y); 
        yield return new WaitForSeconds(0.3f);
        stopMoving = false;
    }







}
