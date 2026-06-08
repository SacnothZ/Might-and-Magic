using System.Collections;
using UnityEngine;

public class EnemySwordAttack : MonoBehaviour
{
    public BoxCollider2D swordCollider;
    [Range(0.5f, 1)] public float collisonEnableWait = 0.5f;
    [Range(0.1f, 1)] public float collisonEnableDuration = 0.1f;




    void Start()
    {
        swordCollider = GetComponent<BoxCollider2D>();
        swordCollider.enabled = false;
    }



    public IEnumerator SwordCollider()
    {

            yield return new WaitForSeconds(collisonEnableWait);
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
                    return;
                }

                ///////////////////////////////
                // Damange based on enemy type
                ///////////////////////////////
                 
                if (transform.parent.name == "Level1-Goblin" || transform.parent.name == "Level1-Goblin(Clone)")
                {
                    hero.heroHealth -= 10;
                    hero.m_animator.SetTrigger("Hurt");
                }
                else if (transform.parent.name == "Level2-Skeleton" || transform.parent.name == "Level1-RedSkeleton" || transform.parent.name == "Level2-Skeleton(Clone)" || transform.parent.name == "Level1-RedSkeleton(Clone)")
                {
                    hero.heroHealth -= 25;
                    hero.m_animator.SetTrigger("Hurt");
                }
                

            }
        }
    }


}
