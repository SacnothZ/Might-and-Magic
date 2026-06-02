using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    HeroKnight hero;
    public Vector2 currentCheckpointLocation;
    
    void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
        currentCheckpointLocation = new Vector2(hero.transform.position.x, hero.transform.position.y);     //save checkpoint at start.
    }

    
    void Update()
    {
        
    }


    public void StartingCheckpoint()
    {
        
    }



}
