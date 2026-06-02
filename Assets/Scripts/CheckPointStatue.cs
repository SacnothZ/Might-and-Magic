using JetBrains.Annotations;
using UnityEngine;

public class CheckPointStatue : MonoBehaviour
{
    HeroKnight hero;
    Checkpoint checkpointLocation;

    [Header("Checkpoint Stats: ")]
    bool checkpointUsed = false;
    void Start()
    {
        hero = GameObject.Find("Player").GetComponent<HeroKnight>();
        checkpointLocation = GameObject.Find("CheckpointLocation").GetComponent<Checkpoint>();
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!checkpointUsed)
        {
            checkpointLocation.currentCheckpointLocation = hero.transform.position;
            checkpointUsed = true;
        }
    }
}
