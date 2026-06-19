using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCamera : MonoBehaviour
{
    private HeroKnight player;
    [Header("Camera Limits (Left-Right): ")]
    [SerializeField][Range(-100,500)] private float LeftLimitX; // Camera limits
    [SerializeField][Range(-100,500)] private float RightLimitX;
    [SerializeField][Range(-100, 500)] private float UpperLimitY; // Camera limits
    [SerializeField][Range(-100, 500)] private float LowerLimitY;



    void Start()
    {
        player = GameObject.Find("Player").GetComponent<HeroKnight>();

        
        if (SceneManager.GetActiveScene().name == "1-Level 1")      // Set limits based on scene
        {
            LeftLimitX = -6.5f;
            RightLimitX = 300f;
        }
        else if (SceneManager.GetActiveScene().name == "2-Level 2")
        {
            LeftLimitX = -20f;
            RightLimitX = 300f;
            LowerLimitY = -0.7f;

        }
        else if (SceneManager.GetActiveScene().name == "3-Level 3")
        {
            LeftLimitX = -80f;
            RightLimitX = 500f;
            LowerLimitY = -0.7f;

        }
    }

    void LateUpdate()
    {
        float cameraX = Mathf.Max(player.transform.position.x, LeftLimitX);   // left LIMIT follow for camera= max ,min, clamp=both
        float cameraY = Mathf.Max(player.transform.position.y, LowerLimitY);
        Vector3 offset = new Vector3(3f, 0f, 0f);
        transform.position = new Vector3(cameraX, cameraY, transform.position.z) + offset;

    }
}