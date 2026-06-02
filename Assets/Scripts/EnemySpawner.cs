using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    LevelCompletePrefab levelCompletePrefab;


    [Header("Enemy Prefab ")]
    public GameObject enemyPrefab;
    [Header("Hero position: ")]
    public Transform hero;

    [Header("Settings: ")]
    [Range(1,100)]public float detectRadius = 10f;
    [Range(1,20)] public float respawnCooldown = 10f;

    [Header("Data: ")]
    [SerializeField] private GameObject currentEnemy;
    [SerializeField] private float respawnTimer;
    [SerializeField] private bool waitingForRespawn;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "1-Level 1" || SceneManager.GetActiveScene().name == "2-Level 2")
        {
            levelCompletePrefab = GameObject.Find("CastleDoor").GetComponent<LevelCompletePrefab>();
        }
         
    }

    void Update()
    {
        
        if (currentEnemy != null)       // if enemy exists rest will not execute
            return;

        
        if (!waitingForRespawn)     //if enemy died(due destroyobject) cooldown refreshes
        {
            waitingForRespawn = true;
            respawnTimer = respawnCooldown;
        }

        
        respawnTimer -= Time.deltaTime;     // cooldown countdown

        if (respawnTimer > 0f)
            return;

        
        float distance = Vector3.Distance(transform.position, hero.position);       //spawnpoint and Hero position distance

        
        if (distance > detectRadius)  // if outside spawnpoint, enemy spawns.
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if ((SceneManager.GetActiveScene().name == "1-Level 1" || SceneManager.GetActiveScene().name == "2-Level 2") &&  levelCompletePrefab.levelComplete == false)
        {
            currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);       //Q.identity-> Zero rotation.
            waitingForRespawn = false;
        }
        else
            currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);       //Q.identity-> Zero rotation.
            waitingForRespawn = false;

    }

    // Editor radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;       //color of draw (visible on editor)
        Gizmos.DrawWireSphere(transform.position, detectRadius);        // as a sphere
    }
}
