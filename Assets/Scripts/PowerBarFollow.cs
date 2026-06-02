using UnityEngine;

public class FollowPlayerWorldUI : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 3f, 0);

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (player == null) return;

        transform.position = player.position + offset;
    }
}
