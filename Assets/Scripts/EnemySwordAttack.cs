using System.Collections;
using UnityEngine;

public class EnemySwordAttack : MonoBehaviour
{
    public BoxCollider2D swordCollider;
    [Range(0.5f, 1)] public float collisonEnableWait = 0.5f;
    [Range(0.1f, 1)] public float collisonEnableDuration = 0.1f;

    [Header("Audio")]
    public AudioClip attackSound;
    public AudioClip impactSound;



    void Start()
    {
        swordCollider = GetComponent<BoxCollider2D>();
        swordCollider.enabled = false;
    }



    public IEnumerator SwordCollider()
    {

            yield return new WaitForSeconds(collisonEnableWait);
            SoundFxManager.instance.PlaySoundFxClip(attackSound, transform, 1f);
            swordCollider.enabled = true;
            yield return new WaitForSeconds(collisonEnableDuration);
            swordCollider.enabled = false;
    }




    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            HeroKnight hero = other.GetComponent<HeroKnight>();

            if (hero != null)
            {
                Vector2 toPlayer = (hero.transform.position - transform.position).normalized; // where is the hero compared to the enemy
                Vector2 playerForward = hero.transform.right * hero.transform.localScale.x; //witch direction hero looks at

                float dot = Vector2.Dot(playerForward,toPlayer);            // do they look at the same direction ? -1=no 1=yes
                bool isFromFront = dot < 0.0f;


                if (hero.isBlocking && isFromFront)
                {
                    SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);
                    return;
                }

                ///////////////////////////////
                // Damange based on enemy type
                ///////////////////////////////
                 
                if (transform.parent.name == "Level1-Goblin" || transform.parent.name == "Level1-Goblin(Clone)")
                {
                    SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);
                    hero.Hurt(10);
                }
                else if (transform.parent.name == "Level2-Skeleton" || transform.parent.name == "Level1-RedSkeleton" || transform.parent.name == "Level2-Skeleton(Clone)" || transform.parent.name == "Level1-RedSkeleton(Clone)")
                {
                    SoundFxManager.instance.PlaySoundFxClip(impactSound, transform, 1f);
                    hero.Hurt(25);
                }
                

            }
        }
    }


}
