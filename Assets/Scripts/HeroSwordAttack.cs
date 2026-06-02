using System.Collections;
using UnityEngine;

public class HeroSwordAttack : MonoBehaviour
{
    private BoxCollider2D swordCollider;
    HeroKnight hero;
    [Range(0.5f, 1)] public float collisonEnableWait = 0.5f;
    [Range(0.1f, 1)] public float collisonEnableDuration = 0.1f;


    void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
        swordCollider = GetComponent<BoxCollider2D>();
        swordCollider.enabled = false;
    }

    void Update()
    {

        
    }


    public IEnumerator SwordColliderInstant()
    {

        
        swordCollider.enabled = true;
        yield return new WaitForSeconds(collisonEnableDuration);
        swordCollider.enabled = false;
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
                skeleton.TakeDamage(5);
            }

            FlyingEye flyingEye = other.GetComponent<FlyingEye>();
            if (flyingEye != null)
            {
                flyingEye.TakeDamage(10);
            }

            Necromancer necromancer = other.GetComponent<Necromancer>();
            if(necromancer != null)
            {
                necromancer.TakeDamage(10);
            }

            Wizard wizard = other.GetComponent<Wizard>();
            if(wizard != null)
            {
                wizard.TakeDamage(10);
            }

        }
    }



}
