using UnityEngine;

public class HeroMagic : MonoBehaviour
{
    HeroKnight hero;

    [Header("Magic Stats: ")]
    public float projectileSpeed = 15f;
    public float direction = 1f;
    [Header("Audio: ")]
    public AudioClip impactSound;


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
                SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);

            }

            Skeleton skeleton = other.GetComponent<Skeleton>();

            if (skeleton != null)
            {
                skeleton.TakeDamage(20);
                SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);

            }

            Necromancer necromancer = other.GetComponent<Necromancer>();
            if (necromancer != null)
            {
                necromancer.TakeDamage(10);
                SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);
            }

            FlyingEye flyingEye = other.GetComponent<FlyingEye>();
            if (flyingEye != null)
            {
                flyingEye.TakeDamage(10);
                SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);

            }



            Wizard wizard = other.GetComponent<Wizard>();
            if (wizard != null)
            {
                wizard.TakeDamage(0);
                SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);

            }
            Destroy(gameObject);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Goblin goblin = collision.gameObject.GetComponent<Goblin>();

            if (goblin != null)
            {
                goblin.TakeDamage(10);
                SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);

            }

            Skeleton skeleton = collision.gameObject.GetComponent<Skeleton>();

            if (skeleton != null)
            {
                skeleton.TakeDamage(20);
                SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);

            }

            Necromancer necromancer = collision.gameObject.GetComponent<Necromancer>();
            if (necromancer != null)
            {
                necromancer.TakeDamage(0);
                SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);
            }

            FlyingEye flyingEye = collision.gameObject.GetComponent<FlyingEye>();
            if (flyingEye != null)
            {
                flyingEye.TakeDamage(10);
                SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);

            }



            Wizard wizard = collision.gameObject.GetComponent<Wizard>();
            if (wizard != null)
            {
                wizard.TakeDamage(0);
                SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);

            }
            Destroy(gameObject);
        }


        if (collision.gameObject.CompareTag("Ground"))
            Destroy(gameObject);
    }

}
